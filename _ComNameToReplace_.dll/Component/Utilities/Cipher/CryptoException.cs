using System;

namespace DomConsult.Org.BouncyCastle.Crypto
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class CryptoException
        : Exception
    {
        public CryptoException()
        {
        }

		public CryptoException(
            string message)
			: base(message)
        {
        }

		public CryptoException(
            string		message,
            Exception	exception)
			: base(message, exception)
        {
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
