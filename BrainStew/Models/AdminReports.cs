﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BrainStew.Models
{
    public class AdminReports : Common
    {
        public List<AdminReports> lstStewMatrix { get; set; }
        public List<AdminReports> lstStew { get; set; }
        public List<AdminReports> lsttopupreport { get; set; }
        public List<AdminReports> lst { get; set; }

        public string isBlocked { get; set; }

        public string Email { get; set; }
        public string FromDate { get; set; }
        public bool IsDownline { get; set; }
        public string JoiningDate { get; set; }
        public string LoginId { get; set; }
        public List<AdminReports> lstassociate { get; set; }
        public List<AdminReports> lstPinTransfer { get; set; }
        public List<AdminReports> lstDirect { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string Status { get; set; }
        public string ToDate { get; set; }
        public string ToLoginID { get; set; }
        public string UpgradtionDate { get; set; }
        public string Amount { get; set; }
        public string TopupBy { get; set; }
        public string PrintingDate { get; set; }
        public string Description { get; set; }
        public string PaymentMode { get; set; }
        public string BusinessType { get; set; }
        public string ReceiptNo { get; set; }

        public string FromLoginID { get; set; }
        public string ePinNo { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string ToId { get; set; }
        public string ToName { get; set; }
        public string TransferDate { get; set; }
        public string FromActivationDate { get; set; }
        public string ToActivationDate { get; set; }
        public string PermanentDate { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }

        public string AdharNo { get; set; }
        public string PanNo { get; set; }


        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string NomineeName { get; set; }
        public string NomineeAge { get; set; }
        public string NomineeRelation { get; set; }
        public string Image { get; set; }


        //public string FromLoginId { get; set; }
        public string BusinessAmount { get; set; }
        public string Percentage { get; set; }
        public string PayoutNo { get; set; }
        public string Level { get; set; }
        public string Fk_IncomeTypeId { get; set; }
        //public string SponserName { get; set; }
        public string AvailableBalance { get; set; }
        public List<AdminReports> lstWalletLedger { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }
        #region associatelist
        public DataSet WalletLedger()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name",Name)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("PayoutWalletLedgerForAdmin", para);
            return ds;
        }
        public DataSet GetAssociateList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@SponsorID", SponsorId),
                                    new SqlParameter("@SponsorName", SponsorName),
                                    new SqlParameter("@Status", Status),
                                    new SqlParameter("@IsDownline", IsDownline),
                                    new SqlParameter("@Leg", Leg)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetAssociateList", para);
            return ds;
        }
        #endregion
        #region topupreport
        public DataSet GetTopupReport()
        {
            SqlParameter[] para = {   new SqlParameter("@LoginID", LoginId),
                                      new SqlParameter("@Name", Name),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@Package", Package),
                                      new SqlParameter("@ClaculationStatus", Status),
                                      new SqlParameter("@BusinesAmount", BusinessType)
                                  };

            DataSet ds = DBHelper.ExecuteQuery("GetTopupreport", para);
            return ds;
        }
        #endregion

        public DataSet GetTransferPinReport()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FromLoginId",LoginId),
                new SqlParameter("@ToLoginId",ToLoginID),
                new SqlParameter("@EpinNo",ePinNo)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetTransferPinReportForAdmin", para);
            return ds;
        }

        public DataSet GetDirectList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@FromActivationDate", FromActivationDate),
                                    new SqlParameter("@ToActivationDate", ToActivationDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", Status),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetDirectListForAdmin", para);
            return ds;
        }

        public DataSet GetAdminProfileDetails()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", Fk_UserId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetAdminProfileDetails", para);
            return ds;
        }
        
        public DataSet UpdateAdminProfile()
        {
            SqlParameter[] para = {
                   new SqlParameter("@PK_UserId",Fk_UserId),
                      new SqlParameter("@SponsorId",SponsorId),
                                   new SqlParameter("@SponserName",SponsorName),
                                   new SqlParameter("@FirstName",FirstName),
                                   new SqlParameter("@LastName",LastName),
                                     new SqlParameter("@Sex",Gender),
                                   new SqlParameter("@Mobile",MobileNo),
                                    new SqlParameter("@Email",Email),
                                     new SqlParameter("@PinCode",PinCode),
                                    new SqlParameter("@State",State),
                                     new SqlParameter("@City",City),
                                   new SqlParameter("@PanNo",PanNo),
                                    new SqlParameter("@PanImage",Image),
                                    new SqlParameter("@Address",Address),
                                   new SqlParameter("@AadharNo",AdharNo),
                                   new SqlParameter("@BankName",BankName),
                                     new SqlParameter("@Branch",BranchName),
                                   new SqlParameter("@AccountNo",AccountNo),
                                    new SqlParameter("@IFSCCode",IFSCCode),
                                     new SqlParameter("@NomineeName",NomineeName),
                                    new SqlParameter("@NomineeRelation",NomineeRelation),
                                     new SqlParameter("@NomineeAge",NomineeAge),
                                      new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateAdminProfile", para);
            return ds;
        }


        

        public DataSet DeleteUerDetails()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", Fk_UserId),
                                    new SqlParameter("@DeletedBy",UpdatedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("DeleteUerDetails", para);
            return ds;
        }

        public DataSet ViewProfileVeriFy()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", Fk_UserId),
                                    new SqlParameter("@UpdatedBy",UpdatedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ViewProfileVeriFy", para);
            return ds;
        }





        public DataSet GetDirectBenefitsList()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status),
                  //new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLevelIncomeTr2", para);
            return ds;
        }

        public DataSet GetLevelBenefitsList()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status),
                  //new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLevelIncomeTr1", para);
            return ds;
        }

        public DataSet GetUpgradeBenefitsList()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status),
                  new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBenefitsReports", para);
            return ds;
        }

        public DataSet GetPlacementBenefitsList()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status),
                  //new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPlacementBenefitsReport", para);
            return ds;
        }

        public DataSet GetBraintBenefitsList()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status),
                  new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBenefitsReports", para);
            return ds;
        }

        public DataSet GetBrainLevelBenefitsList()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status),
                  //new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBrainLevelBenefits", para);
            return ds;
        }

        public DataSet GetStewBenefitsList()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status),
                  //new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBrainLevelBenefits", para);
            return ds;
        }

        public DataSet GetStewLevelBenefitsList()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status),
                  //new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBrainLevelBenefits", para);
            return ds;
        }
        public DataSet getBenefitlist()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                  new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBenefitsReportsForUserWise", para);
            return ds;
        }


        public DataSet TopUpWallet()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name",Name)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("TopUpWalletLedgerForAdmin", para);
            return ds;
        }

        public DataSet GetStewMatrixLevelBenefits()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status),
                  new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetStewMatrixLevelBenefits", para);
            return ds;
        }
        

        public DataSet GetStewMatrixBenefitsList()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                  new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBenefitsReportsForUserWise", para);
            return ds;
        }


        public DataSet GetStewMatrixBenefitsLists()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status),
                  new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBenefitsReports", para);
            return ds;
        }


    }
}