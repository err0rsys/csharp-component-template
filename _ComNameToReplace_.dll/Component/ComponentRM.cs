using System;
using System.Runtime.InteropServices;
using System.EnterpriseServices;
using DomConsult.GlobalShared.Utilities;
using DomConsult.Components.Extensions;
using DomConsult.Components.Interfaces;
using DomConsult.Platform;
using DomConsult.Platform.Extensions;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using DomConsult.CIPHER;
using System.Diagnostics;

namespace DomConsult.Components
{
    // Place business logic here not connected directly with specific platform methods
    public partial class Manager
    {
        /// <summary>
        /// Description of the RunMethod RM_XXXXX
        /// </summary>
        /// <param name="inparams"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private int RM_XXXXX(object inparams, out object data)
        {
            bool initMethod()
            {
                bool res;
                try
                {
                    if (!BDW.OtherExists(TBDOthers.coConfirmationResult))
                    {
                        res = false;

                        // set up input parameters

                        /*
                         if (BDW.ParamExists("ParamId"))
                            ParamId = BDW.Params["ParamId"].AsInt();
                        else
                            Err.MessageRaise(ComponentDef.TXT_AXXXXX_ID, "ParamId");
                        */

                        // ask for a confirmation
                        if (Silent == 0)
                        {
                            Err.ErrCode = (int)TCSWMK.csWMK_JobConversation;
                            //Err.MessageRaise(ComponentDef.MSG_AXXXXX_ID, null);
                        }
                        else
                        {
                            res = true;
                        }
                    }
                    else
                    {
                        res = BDW.Others[TBDOthers.coConfirmationResult].AsInt() == (int)TConfirmResult.acrYes;
                        BDW.DeleteOther(TBDOthers.coConfirmationResult);
                    }
                    return res;
                }
                catch (MessageException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    Err.HandleException(ex, "initMethod");
                    return false;
                }
            }

            data = null;
            int result = 0;

            try
            {
                BDW.LoadMultiInOneParams(inparams);

                if (!TUniVar.VarIsArray(inparams))
                {
                    BDW.LoadParams(inparams);
                }

                if (!initMethod())
                {
                    return TWMK.CreateEmptyRunMethodMessage(out data);
                }

                TransactionStatus = TTransactionStatus.ctsRollbackRequired;
                /* do something e.g. run database script */
                TransactionStatus = TTransactionStatus.ctsCommitEnabled;
            }
            catch (MessageException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Err.HandleException(ex, "RM_XXXXX");
            }

            return result;
        }
    }
}