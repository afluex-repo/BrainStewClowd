using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BrainStew.Models
{
    public class API
    {

    }
    #region Registratio
    public class RegistrationAPI
    {

        public string Status { get; set; }
        public string Message { get; set; }
        //public string Leg { get; set; }
        public string PK_UserId { get; set; }
        public string Password { get; set; }
        public string RegistrationBy { get; set; }
        public string SponsorId { get; set; }
        public string MobileNo { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string LoginId { get; set; }
        public string TransPassword { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }
        public string PinCode { get; set; }
        public string Gender { get; set; }
        public string State { get; set; }
        public string City { get; set; }
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
                                     new SqlParameter("@Leg",""),
                                     new SqlParameter("@Password",Password)

                                   };
            DataSet ds = DBHelper.ExecuteQuery("Registration", para);
            return ds;
        }


    }
    #endregion
    #region Sponsor
    public class SponsorNameAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string sponsorId { get; set; }

        public DataSet GetMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", sponsorId),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberDetailsMobile", para);

            return ds;
        }
    }
    public class SponsorNameA
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string SponsorName { get; set; }

    }
    #endregion
    #region Sponsor
    public class Pincode
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string PinCode { get; set; }
        public DataSet GetStateCity()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PinCode", PinCode),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetStateCity", para);

            return ds;
        }
    }
    public class StateDetails
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
    #endregion
    #region Login
    public class LoginAPI
    {
        public string LoginId { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string UserType { get; set; }
        public string FullName { get; set; }
        public string Pk_adminId { get; set; }
        public string TeamPermanent { get; set; }
        public string FranchiseAdminID { get; set; }
        public string Profile { get; set; }
        public string Gender { get; set; }
        public DataSet Login()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginId",LoginId),
                                new SqlParameter("@Password",Password)};
            DataSet ds = DBHelper.ExecuteQuery("Login", para);
            return ds;
        }
    }
    #endregion
    #region EpinDetails
    public class EpinDetails
    {

        public string EPin { get; set; }
        public string Fk_UserId { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        //public DataSet ValidateEpin()
        //{
        //    SqlParameter[] para = {
        //                              new SqlParameter("@EPin", EPin),

        //                          };
        //    DataSet ds = DBHelper.ExecuteQuery("ValidatePin", para);

        //    return ds;
        //}
        public DataSet ActivateUser()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@EPinNo", EPin),
                                      new SqlParameter("@Fk_UserId",Fk_UserId)

                                  };
            DataSet ds = DBHelper.ExecuteQuery("ActivateUser", para);

            return ds;
        }
    }
    public class EpinDetails1
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string PinStatus { get; internal set; }
    }
    #endregion
    public class AssociateDashBoard
    {
        public string Fk_UserId { get; set; }
        public DataSet GetAssociateDashboard()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId), };
            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetailsForAssociate", para);
            return ds;
        }
    }
    public class DashboardResponse
    {
        public string Status1 { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string MyDonation { get; set; }
        public string DirectMember { get; set; }
        public string ReferralBenefits { get; set; }
        public string LevelBenefits { get; set; }
        public string PlacementBenefits { get; set; }
        public string UpgradeSponsorBenefits { get; set; }
        public string MatrixIncomeUpdateDate { get; set; }
        public string BRAINLEVELACTIVE { get; set; }
        public string BrainMatrixBenefits { get; set; }
        public string BrainMatrixLevelBenefits { get; set; }
        public string TotalBenefits { get; set; }
        public string TopupWallet { get; set; }
        public string MyWallet { get; set; }
        public string WithdrawlAmount { get; set; }
        public string StewMatrixLevelBenefits { get; set; }
        public string CurrentLevel { get; set; }
        public string STEWMATRIXACTIVE { get; set; }
        public string CopyReferralLink { get; set; }
        public string StewMatrixBenefits { get; set; }
    }
    public class UpdateProfile
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
    public class TreeAPI
    {
        public List<Tree1> GetGenelogy { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }
        public string Fk_headId { get; set; }
        public DataSet GetTree()
        {
            SqlParameter[] para = {   new SqlParameter("@LoginId", LoginId),
                 new SqlParameter("@Fk_headId", Fk_headId)
                                  };

            DataSet ds = DBHelper.ExecuteQuery("GetTree", para);
            return ds;
        }
    }
    public class Tree1
    {

        public string SponsorId { get; set; }
        public string Fk_ParentId { get; set; }
        public string TeamPermanent { get; set; }
        public string Fk_SponsorId { get; set; }
        public string MemberName { get; set; }
        public string MemberLevel { get; set; }
        public string Id { get; set; }
        public string ActivationDate { get; set; }
        public string ActiveLeft { get; set; }
        public string ActiveRight { get; set; }
        public string InactiveLeft { get; set; }
        public string InactiveRight { get; set; }
        public string BusinessLeft { get; set; }
        public string BusinessRight { get; set; }
        public string ImageURL { get; set; }
        public string Fk_UserId { get; set; }
        public string LoginId { get; set; }
        public string Leg { get; set; }
    }
    public class TopupByUser
    {
        public string LoginId { get; set; }
        public string PackageId { get; set; }
        public string Amount { get; set; }
        public string FK_UserId { get; set; }
        public DataSet TopUp()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                        new SqlParameter("@AddedBy", FK_UserId),
                                        new SqlParameter("@Fk_ProductId",PackageId),
                                        new SqlParameter("@Amount", Amount),
                                 };
            DataSet ds = DBHelper.ExecuteQuery("TopUp", para);
            return ds;
        }
    }
    public class TopupResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
    public class PaymentMode
    {
        public string PK_PaymentModeId { get; set; }
        public string PaymentModeName { get; set; }
    }
    public class PaymentModeResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<PaymentMode> lst { get; set; }
        public DataSet PaymentList()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetPaymentModeList");
            return ds;
        }
    }
    public class Package
    {
        public string PK_PackageId { get; set; }
        public string PackageName { get; set; }
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public string InMultipleOf { get; set; }
    }
    public class PackageResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<Package> lst { get; set; }
        public DataSet PackageList()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetProductListForTopUp");
            return ds;
        }
    }
    public class DirectRequest
    {
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FromActivationDate { get; set; }
        public string ToActivationDate { get; set; }
        public string Leg { get; set; }
        public string ActivationStatus { get; set; }
        public DataSet GetDirectList()
        {

            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@FromActivationDate", FromActivationDate),
                                    new SqlParameter("@ToActivationDate", ToActivationDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", ActivationStatus),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetDirectList", para);
            return ds;
        }
        public DataSet GetDownlineList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", ActivationStatus), };
            DataSet ds = DBHelper.ExecuteQuery("DownlineList", para);
            return ds;
        }
    }
    public class DirectReponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<DirectList> lst { get; set; }
    }
    public class DirectList
    {
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Leg { get; set; }
        public string Package { get; set; }
        public string JoiningDate { get; set; }
        public string PermanentDate { get; set; }
        public string Status { get; set; }
    }
    public class PinRequest
    {
        public string FK_UserId { get; set; }

        public DataSet GetPinList()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserId", FK_UserId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetPin", para);
            return ds;
        }
    }
    public class PinAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<PinAPIResponse> lst { get; set; }
    }
    public class PinAPIResponse
    {
        public string ePinNo { get; set; }
        public string PinAmount { get; set; }
        public string PinStatus { get; set; }
        public string RegisteredTo { get; set; }
        public string ProductName { get; set; }
    }
    //public class LevelTreeReq
    //{
    //    public string LoginId { get; set; }
    //    public string RootAgentCode { get; set; }

    //    public DataSet GetLevelTreeData()
    //    {
    //        SqlParameter[] para = {
    //                                  new SqlParameter("@AgentCode", LoginId),
    //                                  new SqlParameter("@RootAgentCode", RootAgentCode),

    //        };

    //        DataSet ds = DBHelper.ExecuteQuery("LevelTree", para);
    //        return ds;
    //    }
    //}
    //public class LevelTreeAPI
    //{
    //    public string Status { get; set; }
    //    public string Message { get; set; }
    //    public List<LevelTreeResponse> lst { get; set; }
    //}
    public class LevelTreeResponse
    {
        public string FK_ParentId { get; set; }
        public string PK_UserId { get; set; }
        public string FK_SponsorId { get; set; }
        public string LoginId { get; set; }
        public string MemberName { get; set; }
        public string AssociateMemberName { get; set; }
        public string ProfilePic { get; set; }
    }
    public class AssociateBookingRequest
    {
        public string FK_UserId { get; set; }
        public string LoginId { get; set; }
        public DataSet GetDownlineTree()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_UserId", FK_UserId),
                                          new SqlParameter("@LoginId", LoginId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetAssociateDownlineTree", para);
            return ds;
        }
    }
    public class AssociateBookingAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<AssciateBookingResponse> lst { get; set; }
    }
    public class AssciateBookingResponse
    {
        public string ActiveStatus { get; set; }
        public string FirstName { get; set; }
        public string Fk_SponsorId { get; set; }
        public string Fk_UserId { get; set; }
        public string LoginId { get; set; }
        public string Status { get; set; }
    }
    public class Wallet
    {
        public string PK_UserId { get; set; }
        public DataSet GetWalletBalance()
        {
            SqlParameter[] para = { new SqlParameter("@PK_USerID", PK_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetWalletBalance", para);
            return ds;
        }
    }
    public class WalletBalanceAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Balance { get; set; }
    }
    public class TransferPin
    {
        public string EPinNo { get; set; }
        public string LoginId { get; set; }
        public DataSet ePinTransfer()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Epino",EPinNo),
                new SqlParameter("@LoginId",LoginId)
            };
            DataSet ds = DBHelper.ExecuteQuery("ePinTransfer", para);
            return ds;
        }
    }
    public class Reponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
    public class PinReport
    {
        public string LoginId { get; set; }
        public string ToLoginId { get; set; }
        public string ePinNo { get; set; }
        public DataSet GetTransferPinReport()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FromLoginId",LoginId),
                new SqlParameter("@ToLoginId",ToLoginId),
                new SqlParameter("@EpinNo",ePinNo)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetTransferPinReport", para);
            return ds;
        }
    }
    public class PinResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<PinDetails> lst { get; set; }
    }
    public class PinDetails
    {
        public string ePinNo { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string ToId { get; set; }
        public string ToName { get; set; }
        public string TransferDate { get; set; }
    }
    public class Request
    {
        public string FK_UserId { get; set; }
        public DataSet UserProfile()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FK_UserId",FK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("UserProfile", para);
            return ds;
        }
    }
    public class ProfileAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string FK_UserId { get; set; }
        public string LoginId { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ProfilePic { get; set; }
        public string AadharNo { get; set; }
        public string PanNo { get; set; }
        public DataSet UpdateProfile()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserId", FK_UserId),
                                      new SqlParameter("@AadharNo", AadharNo),
                                      new SqlParameter("@PanNo", PanNo),
                                      new SqlParameter("@Address", Address),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProfile", para);
            return ds;
        }
    }
    //public class BankDetailsUpdateRequest
    //{
    //    public string FK_UserId { get; set; }
    //    public DataSet BankDetailsEdit()
    //    {
    //        SqlParameter[] para =
    //        {
    //            new SqlParameter("@FK_UserId",FK_UserId),
    //        };
    //        DataSet ds = DBHelper.ExecuteQuery("UserProfile", para);
    //        return ds;
    //    }

    //}


    //public class BankDetailsUpdateAPIResponse
    //{

    //    public string Status { get; set; }
    //    public string Message { get; set; }
    //    public string Fk_UserId { get; set; }
    //    public string PanNumber { get; set; }
    //    public string AdharNo { get; set; }
    //    public string BankName { get; set; }
    //    public string BranchName { get; set; }
    //    public string AccountNo { get; set; }
    //    public string IFSCCode { get; set; }
    //    public string NomineeName { get; set; }
    //    public string NomineeRelation { get; set; }
    //    public string NomineeAge { get; set; }

    //    public DataSet BankUpdate()
    //    {
    //        SqlParameter[] para = {
    //                             new SqlParameter("@FK_UserId", Fk_UserId),
    //                               new SqlParameter("@PanNo", PanNumber),
    //                               new SqlParameter("@AadharNo", AdharNo),
    //                               new SqlParameter("@BankName", BankName),
    //                                 new SqlParameter("@Branch", BranchName),
    //                               new SqlParameter("@AccountNo", AccountNo),
    //                                new SqlParameter("@IFSCCode", IFSCCode),
    //                                 new SqlParameter("@NomineeName", NomineeName),
    //                                new SqlParameter("@NomineeRelation", NomineeRelation),
    //                                 new SqlParameter("@NomineeAge", NomineeAge),
    //                                  new SqlParameter("@UpdatedBy", Fk_UserId)
    //                              };
    //        DataSet ds = DBHelper.ExecuteQuery("UpdateBankDetails", para);
    //        return ds;
    //    }
    //}
    public class Password
    {
        public string FK_UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public DataSet ChangePassword()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@OldPassword", OldPassword),
                                      new SqlParameter("@NewPassword", NewPassword),
                                       new SqlParameter("@UpdatedBy", FK_UserId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UserChangePassword", para);

            return ds;
        }
    }
    public class ActivateUser
    {
        public string ePinNo { get; set; }
        public string LoginId { get; set; }
        public DataSet ActivateUserByPin()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@EPinNo", ePinNo)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ActivateUserMobile", para);

            return ds;
        }
    }
    public class SponsorRequest
    {
        public string LoginId { get; set; }
        public DataSet GetSponsor()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberName", para);
            return ds;
        }
    }
    public class SponsorResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string SponsorName { get; set; }

    }
    public class NewsRequest
    {
        public string NewsID { get; set; }
        public DataSet GetNewsDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@NewsID",NewsID)
            };
            DataSet ds = DBHelper.ExecuteQuery("NewsList", para);
            return ds;
        }
    }
    public class NewsListResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<NewsResponse> lstnew { get; set; }
    }
    public class NewsResponse
    {
        public string NewsHeading { get; set; }
        public string NewsBody { get; set; }
    }
    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
    public class AddWalletRequest
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }
        public string PaymentMode { get; set; }
        public string Amount { get; set; }
        public string DDChequeNo { get; set; }
        public string DDChequeDate { get; set; }
        public string BankBranch { get; set; }
        public string BankName { get; set; }
        public string Remark { get; set; }
        public string PostedFile { get; set; }
        public string AddedBy { get; set; }
        public DataSet AddWallet()
        {
            SqlParameter[] para = {
                                 new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Amount", Amount),
                                    //new SqlParameter("@PaymentMode", 10) ,
                                      new SqlParameter("@DDChequeNo", DDChequeNo) ,
                                      new SqlParameter("@DDChequeDate", DDChequeDate) ,
                                      new SqlParameter("@BankBranch", BankBranch) ,
                                      new SqlParameter("@BankName", BankName),
                                      new SqlParameter("@Remarks", Remark),
                                      new SqlParameter("@Document",PostedFile),
                                      new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("EwalletRequest", para);
            return ds;
        }
    }
    public class QRandWalletRequest
    {
        public string FK_UserId { get; set; }
        public DataSet GetWalletBalance()
        {
            SqlParameter[] para = { new SqlParameter("@PK_USerID", FK_UserId)

            };
            DataSet ds = DBHelper.ExecuteQuery("GetWalletBalance", para);
            return ds;
        }
    }
    public class lstQRandWalletResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<QRandWalletResponse> lstwallet { get; set; }

    }
    public class QRandWalletResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string WalletBalance { get; set; }
        public string QRLink { get; set; }

    }
    public class EwalletRequestDetails
    {
        public string Fk_UserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public DataSet GetEwalletRequestDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_UserId",Fk_UserId),
                                   new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletRequestDetails", para);
            return ds;
        }
    }
    public class EwalletResponsDetails
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<EwalletRespons> lstWalletDetails { get; set; }
    }
    public class EwalletRespons
    {
        public string RequestID { get; set; }
        public string Amount { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionDate { get; set; }
        public string Status { get; set; }

    }
    public class DeleteWalletRequest
    {
        public string PK_RequestID { get; set; }
        public string AddedBy { get; set; }
        public DataSet DeleteWallet()
        {
            SqlParameter[] para = {
                new SqlParameter("@PK_RequestID", PK_RequestID),
                  new SqlParameter("@DeletedBy", AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteWalletRequest", para);
            return ds;
        }
    }
    public class EWalletDetailsRequest
    {
        public string FK_UserId { get; set; }
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public DataSet GetEWalletDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserId", FK_UserId),
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                     };

            DataSet ds = DBHelper.ExecuteQuery("GetEWalletDetails", para);
            return ds;
        }
    }
    public class EWalletDetailsResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string TotalCrAmount { get; set; }
        public string TotalDrAmount { get; set; }
        public string Available { get; set; }
        public List<EWalletDetailsResp> lstWalletLedger { get; set; }
    }
    public class EWalletDetailsResp
    {
        public string Pk_EwalletId { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }
        public string Narration { get; set; }
        public string TransactionDate { get; set; }
    }
    public class GetWalletBalanceRequest
    {
        public string Fk_UserId { get; set; }
        public DataSet GetWalletBalance()
        {
            SqlParameter[] para = { new SqlParameter("@PK_USerID", Fk_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetWalletBalance", para);
            return ds;
        }
    }
    public class GetWalletBalanceResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string BalanceAmount { get; set; }

    }
    public class TransferToOtherWalletRequest
    {
        public string Fk_UserId { get; set; }
        public string LoginId { get; set; }
        public string Amount { get; set; }
        public string AddedBy { get; set; }
        public DataSet TransfertoOther()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@LoginId",LoginId),
                                   new SqlParameter("@Amount",Amount),
                                    new SqlParameter("@AddedBy",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("PayoutRequestOther", para);
            return ds;
        }
    }
    public class GetDonationAmountRequest
    {
        public string Fk_UserId { get; set; }
        public DataSet GetDonationAmount()
        {
            SqlParameter[] para = { new SqlParameter("@PK_USerID", Fk_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetDonationAmount", para);
            return ds;
        }
        public DataSet GetWalletBalance()
        {
            SqlParameter[] para = { new SqlParameter("@PK_USerID", Fk_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetWalletBalance", para);
            return ds;
        }
    }
    public class GetDonationAmountResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string DonationAmount { get; set; }
        public string DonationPlanId { get; set; }
        public string UpdatedDonationPlanId { get; set; }
        public string WalletBalance { get; set; }
    }
    public class DonationByWalletRequest
    {
        public string Fk_UserId { get; set; }
        public string Amount { get; set; }
        public string DonationPlanId { get; set; }
        public DataSet DonationByWallet()
        {
            SqlParameter[] para =
            {
                     new SqlParameter("@Fk_UserId",Fk_UserId),
                     new SqlParameter("@Amount",Amount),
                     new SqlParameter("@DonationPlanId",DonationPlanId)
            };
            DataSet ds = DBHelper.ExecuteQuery("ActivateUserByWallet", para);
            return ds;
        }
    }
    public class BrainMatrixDonationRequest
    {
        public string Fk_UserId { get; set; }
        public DataSet GetBrainMatrixPlanAmount()
        {
            SqlParameter[] para = { new SqlParameter("@PK_USerID", Fk_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetBrainMatrixPlanAmount", para);
            return ds;
        }
        public DataSet GetWalletBalance()
        {
            SqlParameter[] para = { new SqlParameter("@PK_USerID", Fk_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetWalletBalance", para);
            return ds;
        }
    }
    public class BrainMatrixDonationResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string DonationAmount { get; set; }
        public string DonationPlanId { get; set; }
        public string UpdatedDonationPlanId { get; set; }
        public string WalletBalance { get; set; }
    }
    public class DonationBrainMatrixPlanRequest
    {
        public string Fk_UserId { get; set; }
        public string Amount { get; set; }
        public string DonationPlanId { get; set; }
        public DataSet DonationBrainMatrixPlan()
        {
            SqlParameter[] para =
            {
                     new SqlParameter("@Fk_UserId",Fk_UserId),
                     new SqlParameter("@Amount",Amount),
                     new SqlParameter("@BrainMatrixPlanId",DonationPlanId)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveBrainMatrixPlanDetails", para);
            return ds;
        }
    }
    public class GetStewMatrixPlanAmountRequest
    {
        public string Fk_UserId { get; set; }
        public DataSet GetStewMatrixPlanAmount()
        {
            SqlParameter[] para = { new SqlParameter("@PK_USerID", Fk_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetStewMatrixPlanAmount", para);
            return ds;
        }
        public DataSet GetWalletBalance()
        {
            SqlParameter[] para = { new SqlParameter("@PK_USerID", Fk_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetWalletBalance", para);
            return ds;
        }
    }
    public class GetStewMatrixPlanAmountResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string DonationAmount { get; set; }
        public string DonationPlanId { get; set; }
        public string UpdatedDonationPlanId { get; set; }
        public string WalletBalance { get; set; }
    }
    public class DonationStewMatrixPlanRequest
    {
        public string Fk_UserId { get; set; }
        public string Amount { get; set; }
        public string DonationPlanId { get; set; }
        public DataSet DonationStewMatrixPlan()
        {
            SqlParameter[] para =
            {
                     new SqlParameter("@Fk_UserId",Fk_UserId),
                     new SqlParameter("@Amount",Amount),
                     new SqlParameter("@StewMatrixPlanId",DonationPlanId)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveStewMatrixPlanDetails", para);
            return ds;
        }
    }
    public class TopUpListRequest
    {
        public string Fk_UserId { get; set; }
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public DataSet GetTopUpDetails()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@FK_UserId", Fk_UserId),
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                 };
            DataSet ds = DBHelper.ExecuteQuery("GetTopUpDetails", para);
            return ds;
        }
    }
    public class TopUpListResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<TopUpListResp> lstTopUp { get; set; }
    }
    public class TopUpListResp
    {
        public string Name { get; set; }
        public string PinAmount { get; set; }
        public string TopUpDate { get; set; }
    }
    public class BrainMatrixDonationListRequest
    {
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public DataSet GetBrainMatrixDonation()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBrainMatrixReport", para);
            return ds;
        }
    }
    public class BrainMatrixDonationListResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<BrainMatrixDonationListResp> lst { get; set; }
    }
    public class BrainMatrixDonationListResp
    {
        public string FromName { get; set; }
        public string Amount { get; set; }
        public string TransactionDate { get; set; }
    }
    public class StewMatrixDonationListRequest
    {
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public DataSet GetStewMatrixDonation()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetStewMatrixReport", para);
            return ds;
        }
    }
    public class StewMatrixDonationListResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<StewMatrixDonationListResp> lst { get; set; }
    }
    public class StewMatrixDonationListResp
    {
        public string FromName { get; set; }
        public string Amount { get; set; }
        public string TransactionDate { get; set; }
    }
    public class DirectListRequest
    {
        public string Ids { get; set; }
        public string Fk_UserId { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FromActivationDate { get; set; }
        public string ToActivationDate { get; set; }
        public string Leg { get; set; }
        public string Status { get; set; }
        public string DirectStatus { get; set; }
        public DataSet GetDirectList()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@PK_UserIds", Ids),
                                    new SqlParameter("@FK_UserId", Fk_UserId),
                                    new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@FromActivationDate", FromActivationDate),
                                    new SqlParameter("@ToActivationDate", ToActivationDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", Status),
                                       new SqlParameter("@DirectStatus", DirectStatus),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetDirectList", para);
            return ds;
        }
    }
    public class DirectListResponse
    {
        public string Ids { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<DirectListResp> lstassociate { get; set; }
    }
    public class DirectListResp
    {
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string SponsorId { get; set; }
        public string Mobile { get; set; }
        public string Package { get; set; }
        public string PermanentDate { get; set; }
        public string Level { get; set; }
        public string Status { get; set; }
    }
    public class TreeTTPRequest
    {
        public string PK_UserId { get; set; }
        public string Id { get; set; }
        public string LoginId { get; set; }
        public string RootAgentCode { get; set; }
        public DataSet GetLevelMembersCountTR1()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@AgentCode", LoginId),
                                      new SqlParameter("@RootAgentCode", RootAgentCode)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLevelMembersCountTR1", para);
            return ds;
        }
    }
    public class TreeTTPResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<TreeTTPResp> lst { get; set; }
        public List<TreeTTPResponselstMembers> lstMember { get; set; }
    }
    public class TreeTTPResp
    {
        public string LevelName { get; set; }
        public string TargetMember { get; set; }
        public string NumberOfMembers { get; set; }

    }
    public class TreeTTPResponselstMember
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<TreeTTPResponselstMembers> lst { get; set; }
    }
    public class TreeTTPResponselstMembers
    {
        public string LevelName { get; set; }
        public string TargetMember { get; set; }
        public string NumberOfMembers { get; set; }
    }
    public class LevelTreeReq
    {
        public string LoginId { get; set; }
        public string RootAgentCode { get; set; }
        public DataSet GetLevelMembersCountTR1()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@AgentCode", LoginId),
                                      new SqlParameter("@RootAgentCode", RootAgentCode),

            };

            DataSet ds = DBHelper.ExecuteQuery("GetLevelMembersCountTR1", para);
            return ds;
        }
        public DataSet GetLevelMembersCount()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@AgentCode", LoginId),
                                      new SqlParameter("@RootAgentCode", RootAgentCode),

            };
            DataSet ds = DBHelper.ExecuteQuery("GetLevelMembersCount", para);
            return ds;
        }
        public DataSet GetLevelMembers(string Level, string PK_UserId)
        {
            SqlParameter[] para = {
                                      new SqlParameter("@MemId", PK_UserId),
                                      new SqlParameter("@Level", Level),
            };

            DataSet ds = DBHelper.ExecuteQuery("GetLevelMembers", para);
            return ds;
        }
    }
    public class LevelTreeAPI
    {
        public string Status1 { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string DisplayName { get; set; }
        public string PK_UserId { get; set; }
        public string Level { get; set; }
        public string Color { get; set; }
        //public string ActiveStatus { get; set; }
        public string ProfilePic { get; set; }
        public string TotalDirect { get; set; }
        public string TotalActive { get; set; }
        public string TotalInactive { get; set; }
        public string TotalTeam { get; set; }
        public string TotalActiveTeam { get; set; }
        public string TotalInActiveTeam { get; set; }
        public string SponsorName { get; set; }
        //public string SelfBV { get; set; }
        //public string TeamBV { get; set; }
        public List<LevelTreeMembers> lst { get; set; }
        public List<LevelTreeMemberDetails> lstDetails { get; set; }
    }
    public class LevelTreeMembers
    {
        public string LevelName { get; set; }
        public string TargetMember { get; set; }
        public string NumberOfMembers { get; set; }
    }
    public class LevelMembers
    {
        public string Status1 { get; set; }
        public string Message { get; set; }
        public List<LevelTreeMemberDetails> lst { get; set; }
    }
    public class LevelTreeMemberDetails
    {
        public string PK_UserId { get; set; }
        public string LoginId { get; set; }
        public string MemberName { get; set; }
        public string SponsorName { get; set; }
        public string ProfilePic { get; set; }
        public string Level { get; set; }
        public string Status { get; set; }
        public string SelfBV { get; set; }
        public string TeamBV { get; set; }
        public string Color { get; set; }
    }
    public class AssociateTreeRequest
    {
        public string Fk_UserId { get; set; }
        public string LoginId { get; set; }
        public string FK_RootId { get; set; }
        public DataSet GetDownlineTree()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_UserId", Fk_UserId),
                                          new SqlParameter("@LoginId", LoginId),
                                            new SqlParameter("@Fk_RootId", FK_RootId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetAssociateDownlineTree", para);
            return ds;
        }
    }
    public class AssociateTreeResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<AssociateTreeRespo> lstPlot { get; set; }
    }
    public class AssociateTreeRespo
    {
        public string LoginId { get; set; }
        public string FirstName { get; set; }
        public string Mobile { get; set; }
        public string SponsorID { get; set; }
        public string Status { get; set; }
        public string ActivationDate { get; set; }
    }
    public class BusinessReportsRequest
    {
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string IsDownline { get; set; }
        public string Level { get; set; }
        public DataSet GetBusinessReports()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                 new SqlParameter("@IsDownline", IsDownline),
                       new SqlParameter("@Lvl", Level)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBusiness", para);
            return ds;
        }
    }
    public class BusinessReportsResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string TotalBV { get; set; }
        public string TotalAmount { get; set; }
        public List<BusinessReportsResp> lstBReports { get; set; }
    }
    public class BusinessReportsResp
    {
        public string LoginId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Level { get; set; }
        public string Date { get; set; }
        public string PackageType { get; set; }
    }
    public class LevelIncomeTr2Request
    {
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }
        public DataSet LevelIncomeTr2()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                   new SqlParameter("@Status", Status)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLevelIncomeTr2", para);
            return ds;
        }
    }
    public class LevelIncomeTr2Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<LevelIncomeTr2Resp> lst { get; set; }
    }
    public class LevelIncomeTr2Resp
    {
        public string FromName { get; set; }
        public string BusinessAmount { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string TransactionDate { get; set; }
    }
    public class LevelIncomeTr1Request
    {
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }
        public DataSet LevelIncomeTr1()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLevelIncomeTr1", para);
            return ds;
        }
    }
    public class LevelIncomeTr1Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<LevelIncomeTr1Resp> lst { get; set; }
    }
    public class LevelIncomeTr1Resp
    {
        public string FromName { get; set; }
        public string BusinessAmount { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string TransactionDate { get; set; }
    }
    public class PlacementBenefitsRequest
    {
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }
        public DataSet PlacementBenefits()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPlacementBenefitsReport", para);
            return ds;
        }
    }
    public class PlacementBenefitsResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<PlacementBenefitsResp> lst { get; set; }
    }
    public class PlacementBenefitsResp
    {
        public string FromName { get; set; }
        public string BusinessAmount { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string TransactionDate { get; set; }
    }
    public class UpgradeBenefitsRequest
    {
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }
        public string Fk_IncomeTypeId { get; set; }
        public DataSet GetbenefitsReport()
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
    public class UpgradeBenefitsResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<UpgradeBenefitsResp> lst { get; set; }
    }
    public class UpgradeBenefitsResp
    {
        public string FromName { get; set; }
        public string BusinessAmount { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public string TransactionDate { get; set; }
    }
    public class BrainMatrixBenefitsRequest
    {
        public string LoginId { get; set; }
        public string Fk_IncomeTypeId { get; set; }
        public DataSet GetbenefitReportNew()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                  new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetbrainmatrixReportNew", para);
            return ds;
        }
    }
    public class BrainMatrixBenefitsResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string TotalBenefits { get; set; }
        public List<BrainMatrixBenefitsResp> lst { get; set; }
    }
    public class BrainMatrixBenefitsResp
    {
        public string Amount { get; set; }
        public string Level { get; set; }
    }
    public class BrainLevelBenefitsRequest
    {
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }
        public string Fk_IncomeTypeId { get; set; }
        public DataSet GetBrainMatixLevelBenefits()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status),
                  new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBrainLevelBenefits", para);
            return ds;
        }
    }
    public class BrainLevelBenefitsResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<BrainLevelBenefitsResp> lst { get; set; }
    }
    public class BrainLevelBenefitsResp
    {
        public string FromName { get; set; }
        public string BusinessAmount { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public string TransactionDate { get; set; }
    }
    public class StewMatrixBenefitsRequest
    {
        public string LoginId { get; set; }
        public string Fk_IncomeTypeId { get; set; }
        public DataSet getStewbenefitsReport()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                  new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetStewMatrixReportNew", para);
            return ds;
        }
    }
    public class StewMatrixBenefitsResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string TotalBenefits { get; set; }
        public List<StewMatrixBenefitsResp> lst { get; set; }
    }
    public class StewMatrixBenefitsResp
    {
        public string Amount { get; set; }
        public string Level { get; set; }
    }
    public class StewMatrixLevelBenefitsRequest
    {
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }
        public string Fk_IncomeTypeId { get; set; }
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
    }
    public class StewMatrixLevelBenefitsResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<StewMatrixLevelBenefitsResp> lst { get; set; }
    }
    public class StewMatrixLevelBenefitsResp
    {
        public string FromName { get; set; }
        public string BusinessAmount { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public string TransactionDate { get; set; }
    }
    public class PayoutWalletLedgerRequest
    {
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public DataSet PayoutWalletLedger()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("PayoutWalletLedger", para);
            return ds;
        }
    }
    public class PayoutWalletLedgerResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<PayoutWalletLedgerResp> lst { get; set; }
    }
    public class PayoutWalletLedgerResp
    {
        public string Narration { get; set; }
        public string LoginId { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }
        public string TransactionDate { get; set; }
    }
    public class GetPayoutBalanceReq
    {
        public string Fk_UserId { get; set; }
        public DataSet GetPayoutBalance()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@Fk_UserId",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPayoutBalance", para);
            return ds;
        }
    }
    public class GetPayoutBalanceResp
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string PayoutBalance { get; set; }
    }
    public class GetPayoutRequestListRequ
    {
        public string LoginId { get; set; }
        public DataSet GetPayoutRequest()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@LoginId",LoginId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPayoutRequest", para);
            return ds;
        }
    }
    public class GetPayoutRequestListResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<GetPayoutRequestListRespon> lstPayoutRequest { get; set; }
    }
    public class GetPayoutRequestListRespon
    {
        public string TransactionNo { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public decimal WithdrawlAmount { get; set; }
        public string ProcessingFee { get; set; }
        public string GrossAmount { get; set; }
        public string Date { get; set; }
        public string IFSCCode { get; set; }
        public string AccountNo { get; set; }
        public string Status { get; set; }
    }
    public class PayoutRequestrequest
    {
        public string LoginId { get; set; }
        public string Amount { get; set; }
        public string AddedBy { get; set; }
        public string Naration { get; set; }
        public DataSet PayoutRequest()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@LoginId",LoginId),
                                   new SqlParameter("@Amount",Amount),
                                    new SqlParameter("@AddedBy",AddedBy),
                                    new SqlParameter("@Naration",Naration)
            };
            DataSet ds = DBHelper.ExecuteQuery("PayoutRequest", para);
            return ds;
        }
    }
    public class GetTransfertoPayoutWalletRequest
    {
        public string Fk_UserId { get; set; }
        public DataSet GetPayoutBalance()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@Fk_UserId",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPayoutBalance", para);
            return ds;
        }
    }
    public class GetTransfertoPayoutWalletResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Balance { get; set; }

    }
    public class TransfertoPayoutWalletRequest
    {
        public string LoginId { get; set; }
        public string Amount { get; set; }
        public string AddedBy { get; set; }
        public DataSet TransfertoTopupWallet()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@LoginId",LoginId),
                                   new SqlParameter("@Amount",Amount),
                                    new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("TransferToPayoutWallet", para);
            return ds;
        }
    }
    public class ChangePasswordRequest
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string UpdatedBy { get; set; }
        public DataSet ChangePassword()
        {
            SqlParameter[] para = {new SqlParameter("@OldPassword",Password),
                                   new SqlParameter("@NewPassword",NewPassword),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("UserChangePassword", para);
            return ds;
        }
    }
    public class ForgetPasswordRequest
    {
        public string Email { get; set; }
        public DataSet ForgetPassword()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Email",Email)
            };
            DataSet ds = DBHelper.ExecuteQuery("CheckLoginDetails", para);
            return ds;
        }
    }
    public class ForgetPasswordResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

    }
    public class BankDetailsUpdateRequest
    {
        public string FK_UserId { get; set; }
        public DataSet BankDetailsEdit()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FK_UserId",FK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("UserProfile", para);
            return ds;
        }
    }
    public class BankDetailsUpdateResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string IsVerified { get; set; }
        public string AdharNo { get; set; }
        public string PanNumber { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string BranchName { get; set; }
        public string IFSCCode { get; set; }
        public string NomineeName { get; set; }
        public string NomineeRelation { get; set; }
        public string NomineeAge { get; set; }
        public string Image { get; set; }
    }
    public class BankDetailsUpdateRequested
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Fk_UserId { get; set; }
        public string PanNumber { get; set; }
        public string AdharNo { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string NomineeName { get; set; }
        public string NomineeRelation { get; set; }
        public string NomineeAge { get; set; }
        public string Image { get; set; }
        public DataSet BankDetailsUpdate()
        {
            SqlParameter[] para = {
                                   new SqlParameter("@FK_UserId",Fk_UserId),
                                   new SqlParameter("@PanNo",PanNumber),
                                   new SqlParameter("@AadharNo",AdharNo),
                                   new SqlParameter("@BankName",BankName),
                                     new SqlParameter("@Branch",BranchName),
                                   new SqlParameter("@AccountNo",AccountNo),
                                    new SqlParameter("@IFSCCode",IFSCCode),
                                     new SqlParameter("@NomineeName",NomineeName),
                                    new SqlParameter("@NomineeRelation",NomineeRelation),
                                     new SqlParameter("@NomineeAge",NomineeAge),
                                         new SqlParameter("@PanImage",Image),
                                           //new SqlParameter("@UPIID",UPIID),
                                      new SqlParameter("@UpdatedBy",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateBankDetails", para);
            return ds;
        }
    }
    public class ViewProfileEditRequest
    {
        public string Fk_UserId { get; set; }
        public DataSet UserProfile()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserId", Fk_UserId),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UserProfile", para);
            return ds;
        }
    }
    public class ViewProfileEditResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string ProfilePic { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
    public class ViewProfileUpdateRequest
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Fk_UserId { get; set; }
        public string ProfilePic { get; set; }
        public string Address { get; set; }
        public DataSet UpdateProfile()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserId", Fk_UserId),
                                      new SqlParameter("@ProfilePic", ProfilePic),
                                      new SqlParameter("@Address", Address)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProfile", para);
            return ds;
        }
    }
    public class GetTreeMembersRequest
    {
        public string PK_UserId { get; set; }
        public string Level { get; set; }
        public DataSet GetLevelMembers()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@MemId", PK_UserId),
                                      new SqlParameter("@Level", Level),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLevelMembers", para);
            return ds;
        }
    }
    public class GetTreeMembersResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<GetTreeMembersResp> lstMember { get; set; }
    }
    public class GetTreeMembersResp
    {
        public string PK_UserId { get; set; }
        public string MemberName { get; set; }
        public string LoginId { get; set; }
        public string Level { get; set; }
        public string ProfilePic { get; set; }
        public string SelfBV { get; set; }
        public string TeamBV { get; set; }
        public string SelfBVDollar { get; set; }
        public string TeamBVDollar { get; set; }
        public string SponsorName { get; set; }
        public string Color { get; set; }
    }
    public class TreeTTPReques
    {
        public string LoginId { get; set; }
        public string RootAgentCode { get; set; }
        public DataSet GetLevelMembersCountTR1()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@AgentCode", LoginId),
                                      new SqlParameter("@RootAgentCode", RootAgentCode)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLevelMembersCountTR1", para);
            return ds;
        }
        public DataSet GetLevelMembers(string Level,string PK_UserId)
        {
            SqlParameter[] para = {
                                      new SqlParameter("@MemId", PK_UserId),
                                      new SqlParameter("@Level", Level),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLevelMembers", para);
            return ds;
        }
    }
    public class GetTreeMembersRespon
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public string StatusCheck { get; set; }
        public string Color { get; set; }
        public string DisplayName { get; set; }
        public string PK_UserId { get; set; }
        public string ProfilePic { get; set; }
        public string TotalDirect { get; set; }
        public string TotalActive { get; set; }
        public string TotalInactive { get; set; }
        public string TotalTeam { get; set; }
        public string TotalActiveTeam { get; set; }
        public string TotalInActiveTeam { get; set; }
        public string SponsorName { get; set; }
        public List<TreeMembersRes> lst { get; set; }
        public List<MemberDetailsRes> lstMember { get; set; }
    }
    public class TreeMembersRes
    {
        public string LevelName { get; set; }
        public string TargetMember { get; set; }
        public string NumberOfMembers { get; set; }
    }
    public class MemberDetailsRes
    {
        public string PK_UserId { get; set; }
        public string MemberName { get; set; }
        public string LoginId { get; set; }
        public string Level { get; set; }
        public string ProfilePic { get; set; }
        public string Status { get; set; }
        public string SelfBV { get; set; }
        public string TeamBV { get; set; }
        public string SponsorName { get; set; }
        public string Color { get; set; }
    }
}