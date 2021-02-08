using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPlusSupport.API.Classes
{
    public class QueryParameters
    {
        const int _maxSize = 100;
        private int _size = 50;
        public int Page { get; set; }
        public int Size
        {
            get{ return _size;}
            set{ _size = Math.Min(_maxSize, value);}
        }
        //id is the default value
        public string SortBy { get; set; } = "Id";

        //value for the sortorder; asc is the default
        public string _sortOrder = "asc";

        //public property
        public string SortOrder {
            get { return _sortOrder; }
            set { if (value == "asc" || value == "desc") _sortOrder = value; }
        }
    }
}
