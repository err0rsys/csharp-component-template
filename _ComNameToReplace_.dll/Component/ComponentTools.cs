using DomConsult.GlobalShared.Utilities;
using System;

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
            data = null;
            int result = 0;

            try
            {
                BDW.LoadParams(inparams);
                //bool aaaaa = BDW.ParamExists("AAAAA") && BDW.Params["AAAAA"].AsInt() > 0;

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
