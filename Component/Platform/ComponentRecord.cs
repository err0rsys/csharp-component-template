using DomConsult.GlobalShared.Utilities;

namespace DomConsult.Platform
{
    /// <summary>
    /// Class Record. This class cannot be inherited.
    /// </summary>
    public sealed class Record
    {
        /// <summary>
        /// The main identifier
        /// </summary>
        public int Id;
        /// <summary>
        /// The object identifier
        /// </summary>
        public string ObjectId;
        /// <summary>
        /// The primary key name
        /// </summary>
        public string KeyName;
        /// <summary>
        /// The table name
        /// </summary>
        public string TableName;
        /// <summary>
        /// The query
        /// </summary>
        public string Query;
        /// <summary>
        /// The COM object
        /// </summary>
        public ComWrapper ComObj;
        /// <summary>
        /// QueryData
        /// </summary>
        public object[,] Data;
        /// <summary>
        /// Data array size
        /// </summary>
        public int DataSize;
    }
}
