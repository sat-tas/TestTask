using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 5;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }

    public class DepartmentParameters : RequestParameters
    {
        public string SearchTerm { get; set; }
    }

    public class EmployeeParameters : RequestParameters
    {
        public string SearchTerm { get; set; }
    }

}
