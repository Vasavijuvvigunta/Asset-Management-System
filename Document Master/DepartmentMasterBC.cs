using GSYSAMSDAC;
using GSYSAMSENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSYSAMSBC
{
    public class DepartmentMasterBC
    {
        public ResponseDepartmentMaster DepartmentMasterPageloadBC(RequestDepartmentMaster request)
        {
            ResponseDepartmentMaster response = new ResponseDepartmentMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.DepartmentMasterPageLoadDAC(request);
            return response;
        }

        public ResponseDepartmentMaster InsertDepartmentMasterBC(RequestDepartmentMaster request)
        {
            ResponseDepartmentMaster response = new ResponseDepartmentMaster();
        //    response.ErrorContainer = Validate(request);
            WMSDAL DAL = new WMSDAL();
            response = DAL.InsertDepartmentMasterDAL(request);
            return response;

        }
        public ResponseDepartmentMaster EditDepartmentMasterBC(RequestDepartmentMaster request)
        {
            ResponseDepartmentMaster response = new ResponseDepartmentMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.EditDepartmentMasterDAL(request);
            return response;

        }


        public ResponseDepartmentMaster UpdateDepartmentMasterBC(RequestDepartmentMaster request)
        {
            ResponseDepartmentMaster response = new ResponseDepartmentMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.UpdateDepartmentMasterDAL(request);
            return response;

        }



        public List<ErrorItem> Validate(RequestDepartmentMaster request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.reqdepartmentmaster.DEPARTMENTCODE == "")
                err.Add(new ErrorItem { DataItem = "Department Code", ErrorNo = "SSB0009" });
            if (request.reqdepartmentmaster.DEPARTMENTNAME == "")
                err.Add(new ErrorItem { DataItem = "Department Name", ErrorNo = "SSB0009" });

            if (request.reqdepartmentmaster.STATUS == "")
                err.Add(new ErrorItem { DataItem = " Status", ErrorNo = "SSB0010" });

            return err;
        }

    }
}

