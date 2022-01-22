/*
 * Implementacja funkcji CodeDecode jest oparta o komponenty będące częścią pakietu BouncyCastle - http://www.bouncycastle.org
 * W chwili obecnej poniższa implementacja jest kompatybilna z implementacją w delphi z pewnymi wyjątkami.
 * Obecne ustawienia przewidują aby klucz był kodowany funkcją skrótu RIPEMD-256 - domyślnie używana funkcja w delphi
 * ponadto wykorzystywany algorytm szyfrowowania to Blowfish z ustawionym trybem szyfrów blokowych na ECB 
 * (domyślnie w delphi tryb jest ustawiony na CTS ale w chwili obecnej nie było czasu na zmianę tego).
 * Szyfrogramy są kodowane do base64 jednakże te same ciągi zakodowane w Delphi oraz w C# mogą być różne, ze względu 
 * na różnicę w kodowaniu pustych znaków w bloku - w C# jest to pousty znak - 0 a delphi dokładane są znaki spacji.
 * Po odszyfrowaniu i wycięciu pustych znaków wiadomości są takie same.
 * 
 * License
 * Copyright (c) 2000 - 2011 The Legion Of The Bouncy Castle (http://www.bouncycastle.org)
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this 
 * software and associated documentation files (the "Software"), to deal in the Software 
 * without restriction, including without limitation the rights to use, copy, modify, merge, 
 * publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons 
 * to whom the Software is furnished to do so, subject to the following conditions: 
 * 
 * The above copyright notice and this permission notice shall be included in all copies or 
 * substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR 
 * PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
 * FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
 * OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 * DEALINGS IN THE SOFTWARE. 
 */

using System;
using System.Text;
using DomConsult.Org.BouncyCastle.Crypto.Digests;
using DomConsult.Org.BouncyCastle.Crypto;
using DomConsult.Org.BouncyCastle.Crypto.Engines;
using DomConsult.Org.BouncyCastle.Crypto.Parameters;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Security;


namespace DomConsult.CIPHER
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member    
    public static class SecurityManager
    {
        class Win32
        {
            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr CertOpenSystemStore(
                IntPtr hCryptProv,
                string storename);

            [DllImport("crypt32.dll", SetLastError = true)]
            public static extern bool CertCloseStore(
                IntPtr hCertStore,
                uint dwFlags);

            [DllImport("crypt32.dll", SetLastError = true)]
            public static extern bool CryptDecryptMessage(
             ref CRYPT_DECRYPT_MESSAGE_PARA pDecryptPara,
             byte[] pbEncryptedBlob,
             int cbEncryptedBlob,
             [In, Out] byte[] pbDecrypted,
             ref int pcbDecrypted,
             IntPtr ppXchgCert);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CRYPT_DECRYPT_MESSAGE_PARA
        {
            public int cbSize;
            public uint dwMsgAndCertEncodingType;
            public int cCertStore;
            public IntPtr rghCertStore;
        }
        const string DEFAULT_CERT_STORE_NAME = "MY";
        const uint PKCS_7_ASN_ENCODING = 0x00010000;
        const uint X509_ASN_ENCODING = 0x00000001;
        static readonly uint MY_ENCODING_TYPE = PKCS_7_ASN_ENCODING | X509_ASN_ENCODING;

        public static int EncodeString(string text, string key, out string encodedText)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    encodedText = string.Empty;
                    return 0;
                }

                encodedText = CodeDecode(true, key, text);
                return 0;
            }
            catch (Exception ex)
            {
                encodedText = ex.Message;
                return -1;
            }
        }

        public static int DecodeString(string encodedText, string key, out string outputString)
        {
            try
            {
                if (string.IsNullOrEmpty(encodedText))
                {
                    outputString = string.Empty;
                    return 0;
                }

                outputString = CodeDecode(false, key, encodedText);
                return 0;
            }
            catch(Exception ex)
            {
                outputString = ex.Message;
                return -1;
            }
        }

        public static int DecodeString(string encodedText, SecureString key, out string outputString)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(key);
                return DecodeString(encodedText, Marshal.PtrToStringUni(unmanagedString), out outputString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        private static string CodeDecode(bool encode, string inputKey, string inputMessage)
        {
            byte[] startKey = Encoding.ASCII.GetBytes(inputKey);
            RipeMD256Digest ripeMd256 = new RipeMD256Digest();

            byte[] key = new byte[ripeMd256.GetDigestSize()];
            ripeMd256.BlockUpdate(startKey, 0, startKey.Length);
            ripeMd256.DoFinal(key, 0);

            //BufferedBlockCipher engine = new CtsBlockCipher(new BlowfishEngine());
            IBlockCipher engine = new BlowfishEngine();
            //IBlockCipher engine = new CbcBlockCipher(new BlowfishEngine());
            //engine.Init(encode, new ParametersWithIV(new KeyParameter(key),new byte[engine.GetBlockSize()]));
            engine.Init(encode, new KeyParameter(key));


            if (encode)
            {
                byte[] message = Encoding.ASCII.GetBytes(inputMessage);
                
                int numberOfParts = 1;
                if (message.Length % engine.GetBlockSize() != 0)
                {
                    if (engine.GetBlockSize() > message.Length)
                        Array.Resize<byte>(ref message, engine.GetBlockSize());
                    else
                    {
                        numberOfParts = (message.Length / engine.GetBlockSize())+1;
                        Array.Resize<byte>(ref message,numberOfParts*engine.GetBlockSize());
                    }
                }
                else
                {
                    if (engine.GetBlockSize() > message.Length)
                        Array.Resize<byte>(ref message, engine.GetBlockSize());
                    else
                    {
                        numberOfParts = message.Length / engine.GetBlockSize();
                        Array.Resize<byte>(ref message, numberOfParts * engine.GetBlockSize());
                    }
                }
                

                byte[] encodedMessage = new byte[message.Length];
                for (int i = 0; i < numberOfParts; i++)
                {
                    engine.ProcessBlock(message, engine.GetBlockSize() * i, encodedMessage, engine.GetBlockSize() * i);
                }
                

                return Convert.ToBase64String(encodedMessage);
            }
            else //dekodowanie
            {
                byte[] encodedMessage = Convert.FromBase64String(inputMessage);
                
                int numberOfParts;

                if (encodedMessage.Length % engine.GetBlockSize() != 0)
                {
                    numberOfParts = (encodedMessage.Length / engine.GetBlockSize()) + 1;
                }
                else
                {
                    numberOfParts = (encodedMessage.Length / engine.GetBlockSize());
                }
                
                Array.Resize<byte>(ref encodedMessage, numberOfParts * engine.GetBlockSize());
                byte[] message = new byte[encodedMessage.Length];
                
                for (int i = 0; i < numberOfParts; i++)
                {
                    engine.ProcessBlock(encodedMessage, i * engine.GetBlockSize(), message, i * engine.GetBlockSize());
                }

                return Encoding.ASCII.GetString(message).Trim().Replace("\0",string.Empty); ;
            }
        }

        public static int DecodeX509String(string b64EncodedString, out string outputString)
        {
            return DecodeX509String(b64EncodedString, DEFAULT_CERT_STORE_NAME, out outputString);
        }


        public static int DecodeX509String(string b64EncodedString, string certificateStore, out string outputString)
        {
            outputString = string.Empty;
            if(string.IsNullOrEmpty(certificateStore))
            {
                certificateStore = DEFAULT_CERT_STORE_NAME;
            }

            try
            {
                byte[] encodedMsg = Convert.FromBase64String(b64EncodedString);
                
                
                IntPtr hSysStore = IntPtr.Zero;
                hSysStore = Win32.CertOpenSystemStore(IntPtr.Zero, certificateStore);

                CRYPT_DECRYPT_MESSAGE_PARA messpara = new CRYPT_DECRYPT_MESSAGE_PARA();
                messpara.cbSize = Marshal.SizeOf(messpara);
                messpara.dwMsgAndCertEncodingType = MY_ENCODING_TYPE;
                messpara.cCertStore = 1;
                messpara.rghCertStore = Marshal.AllocHGlobal(IntPtr.Size); //one cert store pointer
                Marshal.WriteIntPtr(messpara.rghCertStore, hSysStore);

                try
                {
                    int decodedSize = 0;

                    if (Win32.CryptDecryptMessage(ref messpara, encodedMsg, encodedMsg.Length, null, ref decodedSize, IntPtr.Zero))
                    {
                        byte[] decodedBytes = new byte[decodedSize];
                        if (Win32.CryptDecryptMessage(ref messpara, encodedMsg, encodedMsg.Length, decodedBytes, ref decodedSize, IntPtr.Zero))
                        {
                            Array.Resize<byte>(ref decodedBytes, decodedSize);
                            outputString = Encoding.ASCII.GetString(decodedBytes);

                            return 0;
                        }
                        else
                        {
                            outputString = new Win32Exception(Marshal.GetLastWin32Error()).Message;
                            return -1;
                        }
                    }
                    else
                    {
                        outputString = new Win32Exception(Marshal.GetLastWin32Error()).Message;
                        return -2;
                    }
                }
                catch (Exception ex)
                {
                    outputString = ex.Message;
                    return -3;
                }
                finally
                {
                    Marshal.FreeHGlobal(messpara.rghCertStore);
                    if (hSysStore != IntPtr.Zero)
                        Win32.CertCloseStore(hSysStore, 0);
                }
            }
            catch (Exception ex)
            {
                outputString = ex.Message;
                return -4;
            }
           
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
