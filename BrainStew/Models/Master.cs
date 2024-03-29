﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BrainStew.Models
{
    public class Master : Common
    {
        public List<Master> lstpackage { get; set; }

        public string BinaryPercent { get; set; }
        public string BV { get; set; }
        public string CGST { get; set; }
        public string DirectPercent { get; set; }
        public string IGST { get; set; }
        public string Packageid { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string ROIPercent { get; set; }
        public string SGST { get; set; }
        public string PackageTypeId { get; set; }
        public string ToAmount { get; set; }
        public string FromAmount { get; set; }
        public string PackageTypeName { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public List<Master> lstReward { get; set; }
        public string PK_RewardId { get; set; }
        public string NewsID { get; set; }
        public string NewsHeading { get; set; }
        public string NewsBody { get; set; }
        public string NewsDate { get; set; }
        public List<Master> lstNews { get; set; }

        #region ProductMaster

        public DataSet SaveProduct()
        {
            SqlParameter[] para = { new SqlParameter("@ProductName", ProductName),
                                  new SqlParameter("@ProductPrice", ProductPrice),
                                  new SqlParameter("@IGST", IGST),
                                  new SqlParameter("@CGST", CGST),
                                  new SqlParameter("@SGST", SGST),
                                  new SqlParameter("@BinaryPercent", BinaryPercent),
                                  new SqlParameter("@DirectPercent", DirectPercent),
                                  new SqlParameter("@ROIPercent", ROIPercent),
                                  new SqlParameter("@BV",BV),
                                  new SqlParameter("@AddedBy", AddedBy),
                                  new SqlParameter("@PackageTypeId", PackageTypeId),
                                   new SqlParameter("@FromAmount", FromAmount),
                                    new SqlParameter("@ToAmount", ToAmount)
            };

            DataSet ds = DBHelper.ExecuteQuery("AddProduct", para);
            return ds;
        }
        public DataSet ProductList()
        {
            SqlParameter[] para = { new SqlParameter("@ProductID", Packageid) };
            DataSet ds = DBHelper.ExecuteQuery("ProductList", para);
            return ds;
        }
        public DataSet DeleteProduct()
        {
            SqlParameter[] para = { new SqlParameter("@ProductID", Packageid),
                                  new SqlParameter("@DeletedBy", AddedBy),};

            DataSet ds = DBHelper.ExecuteQuery("DeleteProduct", para);
            return ds;
        }

        public DataSet UpdateProduct()
        {
            SqlParameter[] para = { new SqlParameter("@ProductID", Packageid),
                                  new SqlParameter("@ProductName", ProductName),
                                  new SqlParameter("@ProductPrice", ProductPrice),
                                  new SqlParameter("@IGST", IGST),
                                  new SqlParameter("@CGST", CGST),
                                  new SqlParameter("@SGST", SGST),
                                  new SqlParameter("@BinaryPercent", BinaryPercent),
                                  new SqlParameter("@DirectPercent", DirectPercent),
                                  new SqlParameter("@ROIPercent", ROIPercent),
                                  new SqlParameter("@BV", BV),
                                  new SqlParameter("@UpdatedBy", UpdatedBy),
                                     new SqlParameter("@PackageTypeId", PackageTypeId),
                                   new SqlParameter("@FromAmount", FromAmount),
                                    new SqlParameter("@ToAmount", ToAmount)
            };

            DataSet ds = DBHelper.ExecuteQuery("UpdateProduct", para);
            return ds;
        }


        public DataSet Upload()
        {
            SqlParameter[] para = { new SqlParameter("@Title", Title),
                                  new SqlParameter("@postedFile", Image),
                                  new SqlParameter("@AddedBy", AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("Upload", para);
            return ds;
        }
        public DataSet UploadFile()
        {
            SqlParameter[] para = { new SqlParameter("@Title", Title),
                                  new SqlParameter("@postedFile", Image),
                                  new SqlParameter("@AddedBy", AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("UploadFile", para);
            return ds;
        }
        public DataSet GetRewarDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Title",Title)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetRewarDetails", para);
            return ds;
        }
        public DataSet GetFilesDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Title",Title)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetFilesDetails", para);
            return ds;
        }
        public DataSet DeleteReward()
        {
            SqlParameter[] para = {
                new SqlParameter("@PK_RewardId",PK_RewardId),
                new SqlParameter("@DeletedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteReward", para);
            return ds;
        }
        public DataSet DeleteFile()
        {
            SqlParameter[] para = {
                new SqlParameter("@PK_RewardId",PK_RewardId),
                new SqlParameter("@DeletedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteFile", para);
            return ds;
        }
        #endregion
        #region NewsMaster

        public DataSet SaveNews()
        {
            SqlParameter[] para = { new SqlParameter("@NewsHeading", NewsHeading),
                                  new SqlParameter("@NewsBody", NewsBody),
                                  new SqlParameter("@AddedBy", AddedBy)};

            DataSet ds = DBHelper.ExecuteQuery("AddNews", para);
            return ds;
        }

        public DataSet NewsList()
        {
            SqlParameter[] para = { new SqlParameter("@NewsID", NewsID) };
            DataSet ds = DBHelper.ExecuteQuery("NewsList", para);
            return ds;
        }

        public DataSet UpdateNews()
        {
            SqlParameter[] para = { new SqlParameter("@NewsID", NewsID),
                                  new SqlParameter("@NewsHeading", NewsHeading),
                                  new SqlParameter("@NewsBody", NewsBody),
                                  new SqlParameter("@UpdatedBy", UpdatedBy) };

            DataSet ds = DBHelper.ExecuteQuery("UpdateNews", para);
            return ds;
        }

        public DataSet DeleteNews()
        {
            SqlParameter[] para = { new SqlParameter("@NewsID", NewsID),
                                  new SqlParameter("@DeletedBy", AddedBy),};

            DataSet ds = DBHelper.ExecuteQuery("DeleteNews", para);
            return ds;
        }

        #endregion
    }
}