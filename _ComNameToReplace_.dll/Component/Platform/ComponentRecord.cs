using DomConsult.GlobalShared.Utilities;

namespace DomConsult.Platform
{
    /// <summary>
    /// Class Record. This class cannot be inherited.
    /// </summary>
    public sealed class Record
    {
        private readonly string _defaultQuery = @"SELECT * FROM {0} WHERE {0}Id = {{0}}";
        private string _query = TUniConstants._STR_NULL;
        private string _viewQuery = TUniConstants._STR_NULL;
        private string _keyName = TUniConstants._STR_NULL;
        private string _viewName = TUniConstants._STR_NULL;

        /// <summary>
        /// The main identifier
        /// </summary>
        public int Id { get; set; } = TUniConstants._INT_NULL;

        /// <summary>
        /// The object identifier
        /// </summary>
        public string ObjectId { get; set; } = TUniConstants._STR_NULL;

        /// <summary>
        /// The table name for saving
        /// </summary>
        public string TableName { get; set; } = TUniConstants._STR_NULL;

        /// <summary>
        /// The view/table name for reading
        /// </summary>
        public string ViewName
        {
            get
            {
                if (_viewName.Length == 0)
                {
                    _viewName = TableName;
                }
                return _viewName;
            }
            set { _viewName = value; }
        }

        /// <summary>
        /// The primary key name
        /// </summary>
        public string KeyName {
            get
            {
                if (_keyName.Length == 0)
                {
                    _keyName = TableName + "Id";
                }
                return _keyName;
            }
            set { _keyName = value; }
        }

        /// <summary>
        /// The query eg: SELECT * FROM table WHERE tableId = {0}
        /// </summary>
        public string Query {
            get
            {
                if (_query.Length == 0)
                {
                    _query = string.Format(_defaultQuery, TableName);
                }
                return _query;
            }
            set { _query = value; }
        }

        /// <summary>
        /// The query for reading
        /// </summary>
        public string ViewQuery
        {
            get
            {
                if (_viewQuery.Length == 0)
                {
                    _viewQuery = Query;
                }
                return _viewQuery;
            }
            set { _viewQuery = value; }
        }

        /// <summary>
        /// The COM object
        /// </summary>
        #pragma warning disable S1104 // Fields should not have public accessibility
        public ComWrapper ComObj;
        #pragma warning restore S1104 // Fields should not have public accessibility

        /// <summary>
        /// QueryData
        /// </summary>
        #pragma warning disable S1104 // Fields should not have public accessibility
        public object[,] Data;
        #pragma warning restore S1104 // Fields should not have public accessibility

        /// <summary>
        /// Data array size
        /// </summary>
        public int DataSize { get; set; } = 0;
    }
}
