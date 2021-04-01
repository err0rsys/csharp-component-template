using System.Runtime.InteropServices;

namespace DomConsult.Platform.Interfaces
{
    /// <summary>
    /// Interface IDCPlatform used by application to manage transactions
    /// </summary>
    [ComVisible(false)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IDCPlatform
    {
        /// <summary>
        /// Assigns access code.
        /// </summary>
        /// <param name="AccessCode">Access code</param>
        /// <returns>System.Int32.</returns>
        int AssignAccessCode(object AccessCode);

        /// <summary>
        /// Checks the state of current transaction.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int CheckTransaction();

        /// <summary>
        /// Commits current transaction.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int Transaction_Commit();

        /// <summary>
        /// Rollback current transaction.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int Transaction_Rollback();
    }
}
