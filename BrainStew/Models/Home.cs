﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BrainStew.Models;
namespace BrainStew.Models
{
    public class Home : Common
    {
        public List<Home> lstMenu { get; set; }
        public List<Home> lst { get; set; }
        public List<Home> lstsubmenu { get; set; }
        public List<Home> lstChildrenDonation { get; set; }
        public string Amount { get; set; }
        public string WalletBalance { get; set; }
        public string BankBranch { get; set; }
        #region property
        public string SponsorId { get; set; }
        public string LoginId { get; set; }
        public string SponsorName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AdharNo { get; set; }
        public string PanNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string RegistrationBy { get; set; }
        public string Password { get; set; }
        public string Pk_AdminId { get; set; }
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string UserType { get; set; }
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }
        public string ProfilePic { get; set; }
        public HttpPostedFileBase postedFile { get; set; }
        public string Name { get; set; }
        public string ePinNo { get; set; }
        public string MemberNo { get; set; }
        public string ChildName { get; set; }
        public string DOB { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SisterName { get; set; }
        public string SisterAge { get; set; }
        public string BrotherName { get; set; }
        public string BrotherAge { get; set; }
        public string FamilyWork { get; set; }
        public string Need { get; set; }
        public string NeedAmount { get; set; }

        public DataTable dtSistersDetails { get; set; }
        public DataTable dtBrothersDetails { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ChildCharity { get; set; }
        public string ApprovedAmount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }
        public string Age { get; set; }
        public string DonationId { get; set; }
        public string SisName { get; set; }

        public string FK_DonationId { get; set; }
        public string ZipPostalCode { get; set; }
        public string TotalDonation { get; set; }

        #endregion
        #region Sponsor
        public DataSet GetMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", ReferBy),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberName", para);

            return ds;
        }
        #endregion
        #region Registration
        public DataSet Registration()
        {
            SqlParameter[] para = {

                                   new SqlParameter("@SponsorId",SponsorId),
                                   new SqlParameter("@MobileNo",MobileNo),
                                    new SqlParameter("@Email",Email),
                                     new SqlParameter("@Gender",Gender),
                                   new SqlParameter("@FirstName",FirstName),
                                   new SqlParameter("@LastName",LastName),
                                    new SqlParameter("@RegistrationBy",RegistrationBy),
                                     new SqlParameter("@PinCode",PinCode),
                                     new SqlParameter("@State",State),
                                      new SqlParameter("@City",City),
                                     new SqlParameter("@Leg",Leg),
                                     new SqlParameter("@Password",Password)

                                   };
            DataSet ds = DBHelper.ExecuteQuery("Registration", para);
            return ds;
        }
        #endregion
        #region Login
        public DataSet Login()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginId",LoginId),
                                new SqlParameter("@Password",Password)};
            DataSet ds = DBHelper.ExecuteQuery("Login", para);
            return ds;
        }
        public DataSet loadHeaderMenu()
        {
            SqlParameter[] para = {
                                new SqlParameter("@PK_AdminId", Pk_AdminId),
                                 new SqlParameter("@UserType", UserType)
            };

            DataSet ds = DBHelper.ExecuteQuery("GetMenuUserWise", para);
            return ds;
        }
        public static Home GetMenus(string Pk_AdminId, string UserType)
        {
            Home model = new Home();
            List<Home> lstmenu = new List<Home>();
            List<Home> lstsubmenu = new List<Home>();

            model.Pk_AdminId = Pk_AdminId;
            model.UserType = UserType;
            DataSet dsHeader = model.loadHeaderMenu();
            if (dsHeader != null && dsHeader.Tables.Count > 0)
            {
                if (dsHeader.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsHeader.Tables[0].Rows)
                    {
                        Home obj = new Home();

                        obj.MenuId = r["PK_FormTypeId"].ToString();
                        obj.MenuName = r["FormType"].ToString();
                        obj.Icon = r["Icon"].ToString();
                        obj.Url = r["Url"].ToString();
                        lstmenu.Add(obj);
                    }

                    model.lstMenu = lstmenu;
                    foreach (DataRow r in dsHeader.Tables[1].Rows)
                    {
                        Home obj = new Home();
                        obj.Url = r["Url"].ToString();
                        obj.MenuId = r["FK_FormTypeId"].ToString();
                        obj.SubMenuId = r["PK_FormId"].ToString();
                        obj.SubMenuName = r["FormName"].ToString();
                        lstsubmenu.Add(obj);
                    }

                    model.lstsubmenu = lstsubmenu;

                }


            }
            return model;

        }
        #endregion
        public DataSet GetMemberName()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetUserName", para);

            return ds;
        }

        public DataSet GetInActiveUser()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetInActiveUser", para);

            return ds;
        }
        public DataSet GetMemberNameWithUserId()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Pk_UserId", Fk_UserId),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberNameWithUserId", para);

            return ds;
        }


        public DataSet UserProfile()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserId", Fk_UserId),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UserProfile", para);

            return ds;
        }
        public DataSet UpdateProfile()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserId", Fk_UserId),
                                      new SqlParameter("@ProfilePic", ProfilePic),
                                      //new SqlParameter("@AadharNo", AdharNo),
                                      //new SqlParameter("@PanNo", PanNo),
                                      new SqlParameter("@Address", Address),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProfile", para);

            return ds;
        }


        public DataSet ForgetPassword()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Email",Email)
            };
            DataSet ds = DBHelper.ExecuteQuery("CheckLoginDetails", para);
            return ds;
        }

        public DataSet CalculateLevelIncomeTr2()
        {
            DataSet ds = DBHelper.ExecuteQuery("CalculateLevelIncomeTR2");
            return ds;
        }
        public DataSet CalculateLevelIncome()
        {
            DataSet ds = DBHelper.ExecuteQuery("CalculateLevelIncome");
            return ds;
        }
        public DataSet TransferPlacementUpgradeIncome()
        {
            DataSet ds = DBHelper.ExecuteQuery("TransferPlacementUpgradeIncome");
            return ds;
        }
        public DataSet CalculateROI()
        {
            DataSet ds = DBHelper.ExecuteQuery("CalculateROI");
            return ds;
        }
        public DataSet ActivateUser()
        {
            SqlParameter[] para =
            {
                     new SqlParameter("@Fk_UserId",Fk_UserId),
                     new SqlParameter("@Amount",Amount)
            };
            DataSet ds = DBHelper.ExecuteQuery("ActivateUserByWallet", para);
            return ds;
        }



       
        public DataSet SaveDonationDetails()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@MemberNo", MemberNo),
                                      new SqlParameter("@ChildName", ChildName),
                                     new SqlParameter("@Gender", Gender),
                                       new SqlParameter("@DOB",DOB) ,
                                      new SqlParameter("@FatherName", FatherName) ,
                                      new SqlParameter("@MotherName", MotherName),
                                      new SqlParameter("@FamilyWork", FamilyWork) ,
                                      new SqlParameter("@Need", Need),
                                        new SqlParameter("@NeedAmount", NeedAmount),
                                         new SqlParameter("@dtSistersDetails", dtSistersDetails),
                                          new SqlParameter("@dtBrothersDetails", dtBrothersDetails),
                                              new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@ChildImage", Image),
                                    new SqlParameter("@Description", Description),
                                     new SqlParameter("@MobileNo", MobileNo),
                                    new SqlParameter("@Address", Address)
    };
            DataSet ds = DBHelper.ExecuteQuery("SaveDonationDetails", para);
            return ds;
        }
        public DataSet GetChildrenDonationList()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetChildrenDonationList");
            return ds;
        }
        public DataSet GetChildrenDonationDetails()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@DonationId", DonationId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetChildrenDonationDetails", para);
            return ds;

        }
        public DataSet SaveBillingDetails()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@FK_DonationId", FK_DonationId),
                                      new SqlParameter("@FirstName", FirstName),
                                     new SqlParameter("@LastName", LastName),
                                       new SqlParameter("@Email",Email) ,
                                      new SqlParameter("@MobileNo", MobileNo) ,
                                      new SqlParameter("@PinCode", PinCode),
                                      //new SqlParameter("@State", State) ,
                                      //new SqlParameter("@City", City),
                                      //  new SqlParameter("@ZipPostalCode", ZipPostalCode),
                                         new SqlParameter("@PanNumber", PanNo),
                                          new SqlParameter("@Address", Address),
                                              new SqlParameter("@TotalDonation",TotalDonation),
                                    new SqlParameter("@PaymentMode",Fk_Paymentid),
                                        new SqlParameter("@BankName",BankName),
                                       new SqlParameter("@BranchName",BankBranch),
                                       new SqlParameter("@TransactionNo",TransactionNo),
                                       new SqlParameter("@TransactionDate",TransactionDate)
    };
            DataSet ds = DBHelper.ExecuteQuery("SaveBillingDetails", para);
            return ds;
        }
        public DataSet GetStateCity()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@PinCode", PinCode)
    };
            DataSet ds = DBHelper.ExecuteQuery("GetStateCity", para);
            return ds;
        }

       

    }
}