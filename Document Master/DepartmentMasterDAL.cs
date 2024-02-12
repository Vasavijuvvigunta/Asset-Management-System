using GSYSAMSENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace GSYSAMSDAC
{
    public partial class WMSDAL

    {
        public ResponseDepartmentMaster DepartmentMasterPageLoadDAC(RequestDepartmentMaster request)
        {
            ResponseDepartmentMaster response = new ResponseDepartmentMaster();
            response.ErrorContainer = new List<ErrorItem>();

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[DEPARTMENTMASTER_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Status = ds.Tables[0];
                            response.JS_Departmentdetails = ds.Tables[1];

                            response.result = true;
                        }
                        scope.Complete();
                    }
                }

            }

            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("FetchDepartmentMasterPageLoadDAC: " + "Method Name FetchDepartmentMasterPageLoadDAC" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }

            return response;
        }

        public ResponseDepartmentMaster InsertDepartmentMasterDAL(RequestDepartmentMaster request)
        {
            ResponseDepartmentMaster response = new ResponseDepartmentMaster();
            response.ErrorContainer = new List<ErrorItem>();

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[DEPARTMENTMASTER_INSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@DEPARTMENTCODE", request.reqdepartmentmaster.DEPARTMENTCODE));
                        cmd.Parameters.Add(new SqlParameter("@DEPARTMENTNAME", request.reqdepartmentmaster.DEPARTMENTNAME));
                        cmd.Parameters.Add(new SqlParameter("@STATUS", request.reqdepartmentmaster.STATUS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.reqdepartmentmaster.USERCODE));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
                                response.result = true;
                                response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.reqdepartmentmaster.DEPARTMENTNAME });
                            }
                            else
                            {
                                response.result = false;

                                //response.JS_ASSETCATEGORYDETAILS = ds.Tables[1];
                                response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.reqdepartmentmaster.DEPARTMENTCODE });
                            }
                        }
                    }
                    scope.Complete();
                }


            }


            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("InsertDepartmentMasterDAL: " + "Method Name InsertDepartMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }


            return response;
        }

        public ResponseDepartmentMaster EditDepartmentMasterDAL(RequestDepartmentMaster request)
        {

            ResponseDepartmentMaster response = new ResponseDepartmentMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[DEPARTMENTMASTER_EDIT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.reqdepartmentmaster.AUTOID));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Departmentdetails = ds.Tables[0];
                            //    response.JS_ItemDetails = ds.Tables[1];

                            response.result = true;
                        }

                    }
                    scope.Complete();

                }


            }

            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("EditDepartmentMasterDAL: " + "Method Name EditDepartmentMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }


            return response;
        }

            public ResponseDepartmentMaster UpdateDepartmentMasterDAL(RequestDepartmentMaster request)
            {


                ResponseDepartmentMaster response = new ResponseDepartmentMaster();
                response.ErrorContainer = new List<ErrorItem>();
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        using (SqlConnection con = new SqlConnection(connectionstring))
                        {
                            SqlCommand cmd = new SqlCommand("[MASTERS].[DEPARTMENTMASTER_UPDATE]", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@AUTOID", request.reqdepartmentmaster.AUTOID));
                            cmd.Parameters.Add(new SqlParameter("@DEPARTMENTCODE", request.reqdepartmentmaster.DEPARTMENTCODE));
                            cmd.Parameters.Add(new SqlParameter("@DEPARTMENTNAME", request.reqdepartmentmaster.DEPARTMENTNAME));
                            cmd.Parameters.Add(new SqlParameter("@STATUS", request.reqdepartmentmaster.STATUS));
                            cmd.Parameters.Add(new SqlParameter("@USERCODE", request.reqdepartmentmaster.USERCODE));
                            con.Open();
                            SqlDataAdapter oda = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
                                response.result = true;
                                response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.reqdepartmentmaster.DEPARTMENTNAME });
                            }
                            else
                            {
                                response.result = false;

                                //response.JS_ASSETCATEGORYDETAILS = ds.Tables[1];
                                response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.reqdepartmentmaster.DEPARTMENTCODE });
                            }
                        }
                    }

                    scope.Complete();
                    }
                }
                catch (Exception ex)
                {

                    string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                    string responselog = createlog("UpdateDepartmentDetailsDAL: " + "Method Name UpdateDepartmentDetailsDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                    response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                    response.result = false;

                }



                return response;

            }
        }
    }
    

