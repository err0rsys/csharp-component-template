using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomConsult.GlobalShared.Utilities;

namespace Component
{
    public sealed class Record
    {
        public int Id;
        public string ObjectId;
        public string KeyName;
        public string TableName;
        public string Query;
        public ComWrapper ComObj;
    }
}
