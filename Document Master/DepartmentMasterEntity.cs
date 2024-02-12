using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GSYSAMSENTITY
{
    public class DepartmentMasterEntity
    {
        public string AUTOID { get; set; }

        public string USERCODE { get; set; }

        public string DEPARTMENTCODE { get; set; }

        public string DEPARTMENTNAME { get; set; }

        public string STATUS { get; set; }
    }

    public class RequestDepartmentMaster
    {
        public DepartmentMasterEntity reqdepartmentmaster { get; set; }
    }


    public class ResponseDepartmentMaster
    {
        public bool result { get; set; }

        public List<ErrorItem> ErrorContainer { get; set; }

        public DataTable JS_Departmentdetails { get; set; }
        public DataTable JS_Status { get; set; }
    }

}

