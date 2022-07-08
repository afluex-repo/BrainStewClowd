using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrainStew.Models;
using System.Data;
using System.IO;
namespace BrainStew.Controllers
{
    public class WebApiController : Controller
    {
        // GET: API
        #region Registration
        public ActionResult Registration(RegistrationAPI model)
        {
            RegistrationAPI obj = new RegistrationAPI();
            if (model.SponsorId == "" || model.SponsorId == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Sponsor Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.FirstName == "" || model.FirstName == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter First Name";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.MobileNo == "" || model.MobileNo == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Mobile No";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            //if (model.Leg == "" || model.Leg == null)
            //{
            //    obj.Status = "1";
            //    obj.Message = "Please Select Leg";
            //    return Json(obj, JsonRequestBehavior.AllowGet);
            //}
            model.SponsorId = model.SponsorId;
            try
            {
                model.RegistrationBy = "Mobile";
                model.Password = Crypto.Encrypt(model.Password);
                DataSet ds = model.Registration();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.PK_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                        obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        obj.FullName = ds.Tables[0].Rows[0]["Name"].ToString();
                        obj.Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        obj.TransPassword = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        obj.MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        //obj.Leg = model.Leg;
                        obj.RegistrationBy = model.RegistrationBy;
                        obj.SponsorId = model.SponsorId;
                        obj.LastName = model.LastName;
                        obj.PinCode = model.PinCode;
                        obj.State = model.State;
                        obj.City = model.City;
                        obj.Email = model.Email;
                        obj.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                        obj.Status = "0";
                        obj.Gender = model.Gender;
                        obj.Message = "Registered Successfully";
                        if (obj.Email != "" && obj.Email != null)
                        {
                            //string Body = "Dear " + obj.FullName + ",\t\nThank you for your registration. Your Details are as Below: \t\nLogin ID: " + obj.LoginId + "\t\nPassword: " + obj.Password;
                            //BLMail.SendMail(obj.Email, "Registration Successful", Body, false);
                        }
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region SponsporName
        public ActionResult GetSponsorName(SponsorNameAPI sponsorname)
        {
            SponsorNameA obj = new SponsorNameA();
            DataSet ds = sponsorname.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.SponsorName = ds.Tables[0].Rows[0]["FullName"].ToString();
                obj.Status = "0";
                obj.Message = "Sponsor Name Fetched";
                return Json(obj, JsonRequestBehavior.AllowGet);


            }
            else
            {
                sponsorname.Status = "1";
                sponsorname.Message = "Invalid SponsorId"; return Json(sponsorname, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region getState
        public ActionResult GetState(Pincode Pindetails)
        {
            StateDetails obj = new StateDetails();
            DataSet ds = Pindetails.GetStateCity();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                obj.Status = "0";
                obj.Message = "Details Fetched";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Pindetails.Status = "1";
                Pindetails.Message = "Invalid PinCode"; return Json(Pindetails, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Login
        public ActionResult Login(LoginAPI model)
        {
            LoginAPI obj = new LoginAPI();
            Reponse res = new Reponse();
            if (model.LoginId == "" || model.LoginId == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Login Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.Password == "" || model.Password == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Password";
            }
            try
            {

                DataSet dsResult = model.Login();
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        if (model.Password == Crypto.Decrypt(dsResult.Tables[0].Rows[0]["Password"].ToString()))
                        {
                            if ((dsResult.Tables[0].Rows[0]["UserType"].ToString() == "Associate"))
                            {
                                obj.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                                obj.UserId = dsResult.Tables[0].Rows[0]["Pk_userId"].ToString();
                                obj.UserType = dsResult.Tables[0].Rows[0]["UserType"].ToString();
                                obj.FullName = dsResult.Tables[0].Rows[0]["FullName"].ToString();
                                obj.Password = Crypto.Decrypt(dsResult.Tables[0].Rows[0]["Password"].ToString());
                                obj.Profile = dsResult.Tables[0].Rows[0]["Profile"].ToString();
                                obj.Status = dsResult.Tables[0].Rows[0]["Status"].ToString();
                                obj.TeamPermanent = dsResult.Tables[0].Rows[0]["TeamPermanent"].ToString();
                                obj.Gender = dsResult.Tables[0].Rows[0]["Sex"].ToString();
                                obj.Status = "0";
                                obj.Message = "Successfully Logged in";
                                return Json(obj, JsonRequestBehavior.AllowGet);
                            }
                            else if (dsResult.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                            {
                                obj.Status = "0";
                                obj.Message = "Successfully Logged in";
                                obj.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                                obj.Pk_adminId = dsResult.Tables[0].Rows[0]["Pk_adminId"].ToString();
                                obj.UserType = dsResult.Tables[0].Rows[0]["UsertypeName"].ToString();
                                obj.FullName = dsResult.Tables[0].Rows[0]["Name"].ToString();

                                if (dsResult.Tables[0].Rows[0]["isFranchiseAdmin"].ToString() == "True")
                                {
                                    obj.FranchiseAdminID = dsResult.Tables[0].Rows[0]["Pk_adminId"].ToString();
                                }
                            }
                            else
                            {
                                res.Status = "1";
                                res.Message = "Incorrect LoginId Or Password";
                                return Json(res, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {

                            res.Status = "1";
                            res.Message = "Invalid LoginId or Password.";
                            return Json(res, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {

                        res.Status = "1";
                        res.Message = "Invalid LoginId or Password.";
                        return Json(res, JsonRequestBehavior.AllowGet);
                    }

                }


                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                res.Status = "1";
                res.Message = "Invalid LoginId or Password.";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
        #region ActivateUser

        public ActionResult ActivateUser(EpinDetails model)
        {
            EpinDetails obj = new EpinDetails();

            if (model.EPin == "" || model.EPin == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter EPin No";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            try
            {
                DataSet dsResult = model.ActivateUser();
                {
                    if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                    {
                        if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            string Email = dsResult.Tables[0].Rows[0]["Email"].ToString();
                            obj.Status = "0";
                            obj.Message = "User Activated Successfully";
                            if (Email != null && Email != "")
                            {
                                string Body = "Dear User,</br> Your Account have been activated.";
                                BLMail.SendMail(Email, "Activation Successful", Body, false);
                            }
                            return Json(obj, JsonRequestBehavior.AllowGet);

                        }
                        else
                        {

                            obj.Status = "1";
                            obj.Message = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            return Json(obj, JsonRequestBehavior.AllowGet);
                        }
                    }


                }


                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Dashboard
        //public ActionResult GetDashboard(AssociateDashBoard associate)
        //{
        //    DashboardResponse obj = new DashboardResponse();
        //    DataSet ds = associate.GetAssociateDashboard();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        obj.TotalDownline = ds.Tables[0].Rows[0]["TotalDownline"].ToString();
        //        obj.TotalBusiness = ds.Tables[0].Rows[0]["TotalBusiness"].ToString();
        //        obj.TeamBusiness = ds.Tables[0].Rows[0]["TeamBusiness"].ToString();
        //        obj.SelfBusiness = ds.Tables[0].Rows[0]["SelfBusiness"].ToString();
        //        obj.TotalDirect = ds.Tables[0].Rows[0]["TotalDirect"].ToString();
        //        obj.TotalActive = ds.Tables[0].Rows[0]["TotalActive"].ToString();
        //        obj.TotalInActive = ds.Tables[0].Rows[0]["TotalInActive"].ToString();
        //        obj.TotalTeam = ds.Tables[0].Rows[0]["TotalTeam"].ToString();
        //        obj.TotalTeamActive = ds.Tables[0].Rows[0]["TotalTeamActive"].ToString();
        //        obj.TotalTeamInActive = ds.Tables[0].Rows[0]["TotalTeamInActive"].ToString();
        //        obj.TotalIncome = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalLevelIncomeTTP"]) + Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalLevelIncomeTPS"]);
        //        obj.LevelIncomeTr1 = ds.Tables[0].Rows[0]["TotalLevelIncomeTTP"].ToString();
        //        obj.LevelIncomeTr2 = ds.Tables[0].Rows[0]["TotalLevelIncomeTPS"].ToString();
        //        obj.UsedPins = ds.Tables[0].Rows[0]["UsedPins"].ToString();
        //        obj.AvailablePins = ds.Tables[0].Rows[0]["AvailablePins"].ToString();
        //        obj.TotalPins = ds.Tables[0].Rows[0]["TotalPins"].ToString();
        //        obj.Status = ds.Tables[2].Rows[0]["Status"].ToString();
        //        obj.TotalPayoutWallet = ds.Tables[0].Rows[0]["TotalPayoutWalletAmount"].ToString();
        //        obj.TotalAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalPayoutWalletAmount"]) + 0;
        //        if (obj.ActiveStatus == "Active")
        //        {
        //            obj.ReferralLink = "http://BrainStew.co.in/Home/Registration?Pid=" + associate.Fk_UserId;
        //        }
        //        else
        //        {
        //            obj.ReferralLink = "";
        //        }
        //        obj.Status = "0";
        //        obj.Message = "Data Fetched";

        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        obj.Status = "1";
        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
        //    {
        //        obj.Tr1Business = ds.Tables[1].Rows[0]["Tr1Business"].ToString();
        //        obj.Tr2Business = ds.Tables[1].Rows[0]["Tr2Business"].ToString();
        //    }
        //    List<Dashboard> lst = new List<Dashboard>();
        //    DataSet ds1 = obj.GetRewardDetails();
        //    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow r in ds1.Tables[0].Rows)
        //        {
        //            Dashboard obj1 = new Dashboard();
        //            obj1.PK_RewardId = r["PK_RewardId"].ToString();
        //            obj1.Title = r["Title"].ToString();
        //            obj1.Image = "/UploadReward/" + r["postedFile"].ToString();
        //            lst.Add(obj1);
        //        }
        //        obj.lstReward = lst;
        //    }
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[3].Rows.Count > 0)
        //    {
        //        obj.TotalTPSAmountTobeReceived = double.Parse(ds.Tables[3].Compute("sum(TopUpAmount)", "").ToString()).ToString("n2");
        //    }
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[4].Rows.Count > 0)
        //    {
        //        obj.TotalTPSAmountReceived = double.Parse(ds.Tables[4].Compute("sum(TotalROI)", "").ToString()).ToString("n2");
        //        obj.TotalTPSBalanceAmount = Convert.ToDecimal(ViewBag.TotalTPSAmountTobeReceived) - Convert.ToDecimal(ViewBag.TotalTPSAmountReceived);
        //    }
        //}
        #endregion
        #region Tree
        public ActionResult Tree(TreeAPI model)
        {

            UpdateProfile sta = new UpdateProfile();
            TreeAPI obj = new TreeAPI();
            if (model.LoginId == "" || model.LoginId == null)
            {
                model.Status = "1";
                model.Message = "Please enter LoginId";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            if (model.Fk_headId == "" || model.Fk_headId == null)
            {
                model.Status = "1";
                model.Message = "Please enter headId";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            try
            {
                DataSet ds = model.GetTree();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["msg"].ToString() == "0")
                    {

                        List<Tree1> GetGenelogy = new List<Tree1>();
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            Tree1 obj1 = new Tree1();
                            obj1.Fk_UserId = r["Fk_UserId"].ToString();
                            obj1.Fk_ParentId = r["Fk_ParentId"].ToString();
                            obj1.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                            obj1.SponsorId = r["SponsorId"].ToString();
                            obj1.LoginId = r["LoginId"].ToString();
                            obj1.TeamPermanent = r["TeamPermanent"].ToString();
                            obj1.MemberName = r["MemberName"].ToString();
                            obj1.MemberLevel = r["MemberLevel"].ToString();
                            obj1.Leg = r["Leg"].ToString();
                            obj1.Id = r["Id"].ToString();

                            obj1.ActivationDate = r["ActivationDate"].ToString();
                            obj1.ActiveLeft = r["ActiveLeft"].ToString();
                            obj1.ActiveRight = r["ActiveRight"].ToString();
                            obj1.InactiveLeft = r["InactiveLeft"].ToString();
                            obj1.InactiveRight = r["InactiveRight"].ToString();
                            obj1.BusinessLeft = r["BusinessLeft"].ToString();
                            obj1.BusinessRight = r["BusinessRight"].ToString();
                            obj1.ImageURL = r["ImageURL"].ToString();
                            GetGenelogy.Add(obj1);
                        }
                        obj.GetGenelogy = GetGenelogy;
                        obj.Message = "Tree";
                        obj.Status = "0";
                        obj.LoginId = model.LoginId;
                        obj.Fk_headId = model.Fk_headId;

                    }
                    else
                    {
                        sta.Status = "1";
                        sta.Message = "No Data Found";
                        return Json(sta, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    sta.Status = "1";
                    sta.Message = "No Data Found";
                    return Json(sta, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                sta.Status = "1";
                sta.Message = ex.Message;
                return Json(sta, JsonRequestBehavior.AllowGet);
            }


            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ActivateUser

        public ActionResult Topup(TopupByUser model)
        {
            TopupResponse obj = new TopupResponse();
            //model.TopUpDate = string.IsNullOrEmpty(model.TopUpDate) ? null : Common.ConvertToSystemDate(model.TopUpDate, "dd/mm/yyyy");
            //model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/mm/yyyy");
            try
            {
                DataSet dsResult = model.TopUp();
                {
                    if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                    {
                        if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            obj.Status = "0";
                            obj.Message = "Top-Up Done successfully";
                            return Json(obj, JsonRequestBehavior.AllowGet);

                        }
                        else
                        {

                            obj.Status = "1";
                            obj.Message = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            return Json(obj, JsonRequestBehavior.AllowGet);
                        }
                    }


                }


                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region getPaymentMode
        public ActionResult GetPaymentMode()
        {
            List<PaymentMode> lst = new List<PaymentMode>();
            PaymentModeResponse obj = new PaymentModeResponse();
            DataSet ds = obj.PaymentList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    PaymentMode model = new PaymentMode();
                    model.PK_PaymentModeId = r["PK_paymentID"].ToString();
                    model.PaymentModeName = r["PaymentMode"].ToString();
                    lst.Add(model);
                }
                obj.lst = lst;
            }
            else
            {
                obj.Status = "1";
                obj.Message = "No Record Found";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult GetPackage()
        {
            List<Package> lst = new List<Package>();
            PackageResponse obj = new PackageResponse();
            DataSet ds = obj.PackageList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Package model = new Package();
                    model.PK_PackageId = r["Pk_ProductId"].ToString();
                    model.PackageName = r["ProductName"].ToString();
                    model.MinimumAmount = 1000;
                    model.MaximumAmount = 5000;
                    model.InMultipleOf = "1000";
                    lst.Add(model);
                }
                obj.lst = lst;
            }
            else
            {
                obj.Status = "1";
                obj.Message = "No Record Found";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DirectList(DirectRequest req)
        {
            DirectReponse model = new DirectReponse();
            List<DirectList> lst = new List<DirectList>();
            DataSet ds = req.GetDirectList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DirectList obj = new DirectList();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.LoginId = (r["LoginId"].ToString());
                    obj.Name = (r["Name"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DownlineList(DirectRequest req)
        {
            DirectReponse model = new DirectReponse();
            List<DirectList> lst = new List<DirectList>();
            DataSet ds = req.GetDownlineList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DirectList obj = new DirectList();
                    obj.Mobile = r["Mobile"].ToString();
                    //obj.Email = r["Email"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.LoginId = (r["LoginId"].ToString());
                    obj.Name = (r["Name"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult PinList(PinRequest req)
        {
            PinAPI model = new PinAPI();
            List<PinAPIResponse> lst = new List<PinAPIResponse>();
            DataSet ds = req.GetPinList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    PinAPIResponse obj = new PinAPIResponse();
                    obj.ePinNo = r["ePinNo"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.PinStatus = r["PinStatus"].ToString();
                    obj.RegisteredTo = r["RegisteredTo"].ToString();
                    //obj.IsRegistered = r["IsRegistered"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public ActionResult LevelTree(LevelTreeReq req)
        //{
        //    LevelTreeAPI model = new LevelTreeAPI();
        //    List<LevelTreeResponse> lst = new List<LevelTreeResponse>();
        //    DataSet ds = req.GetLevelTreeData();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        model.Status = "0";
        //        model.Message = "Record Found";
        //        foreach (DataRow r in ds.Tables[0].Rows)
        //        {
        //            LevelTreeResponse obj = new LevelTreeResponse();
        //            obj.FK_ParentId = r["Parentid"].ToString();
        //            obj.PK_UserId = r["PK_UserId"].ToString();
        //            obj.FK_SponsorId = r["FK_SponsorID"].ToString();
        //            obj.LoginId = r["LoginId"].ToString();
        //            obj.MemberName = r["MemberName"].ToString();
        //            obj.AssociateMemberName = r["AssociateMemberName"].ToString();
        //            obj.ProfilePic = r["ProfilePic"].ToString();
        //            lst.Add(obj);
        //        }
        //        model.lst = lst;
        //    }
        //    else
        //    {
        //        model.Status = "1";
        //        model.Message = "No Record Found";
        //    }
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public ActionResult AssociateTree(AssociateBookingRequest req)
        //{
        //    AssociateBookingAPI model = new AssociateBookingAPI();
        //    List<AssciateBookingResponse> lst = new List<AssciateBookingResponse>();
        //    DataSet ds = req.GetDownlineTree();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        model.Status = "0";
        //        model.Message = "Record Found";
        //        foreach (DataRow r in ds.Tables[0].Rows)
        //        {
        //            AssciateBookingResponse obj = new AssciateBookingResponse();
        //            obj.Fk_UserId = r["Pk_UserId"].ToString();
        //            obj.Fk_SponsorId = r["Fk_SponsorId"].ToString();
        //            obj.LoginId = r["LoginId"].ToString();
        //            obj.FirstName = r["FirstName"].ToString();
        //            obj.Status = r["Status"].ToString();
        //            obj.ActiveStatus = r["ActiveStatus"].ToString();
        //            lst.Add(obj);
        //        }
        //        model.lst = lst;
        //    }
        //    else
        //    {
        //        model.Status = "1";
        //        model.Message = "No Record Found";
        //    }
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public ActionResult WalletBalance(Wallet model)
        {
            WalletBalanceAPI obj = new WalletBalanceAPI();
            DataSet ds = model.GetWalletBalance();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.Message = "Record Found";
                obj.Balance = ds.Tables[0].Rows[0]["amount"].ToString();
            }
            else
            {
                obj.Status = "1";
                obj.Message = "No Record Found";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult TransferPin(TransferPin model)
        {
            Reponse obj = new Reponse();
            try
            {
                DataSet ds = model.ePinTransfer();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.Message = "Transfer Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PinTransferReport(PinReport req)
        {
            PinResponse model = new PinResponse();
            List<PinDetails> lst = new List<PinDetails>();
            DataSet ds = req.GetTransferPinReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    PinDetails obj = new PinDetails();
                    obj.ePinNo = r["EpinNo"].ToString();
                    obj.FromId = r["FromId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToId = r["ToId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.TransferDate = r["TransferDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UserProfile(Request model)
        {
            ProfileAPI obj = new ProfileAPI();
            DataSet ds = model.UserProfile();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.Message = "Record Found";
                obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                obj.FK_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                obj.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                obj.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                obj.Gender = ds.Tables[0].Rows[0]["Sex"].ToString();
                obj.MobileNo = ds.Tables[0].Rows[0]["Mobile"].ToString();
                obj.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                obj.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                obj.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.AadharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                obj.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
            }
            else
            {
                obj.Status = "1";
                obj.Message = "No Record Found";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult UpdateProfile(ProfileAPI model)
        {
            Reponse obj = new Reponse();
            try
            {
                DataSet ds = model.UpdateProfile();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.Message = "Profile Updated Successfully";
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ChangePassword(Password model)
        {
            Reponse obj = new Reponse();
            try
            {
                model.OldPassword = Crypto.Encrypt(model.OldPassword);
                model.NewPassword = Crypto.Encrypt(model.NewPassword);
                DataSet ds = model.ChangePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.Message = "Password Changed  Successfully";
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ActivateUserByPin(ActivateUser model)
        {
            Reponse obj = new Reponse();
            try
            {
                DataSet ds = model.ActivateUserByPin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.Message = "User Activated Successfully";
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetBankDetails(BankDetailsUpdateRequest model)
        {
            BankDetailsUpdateAPIResponse obj = new BankDetailsUpdateAPIResponse();
            DataSet ds = model.BankDetailsEdit();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.Message = "Record Found";
                obj.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.BankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                obj.AccountNo = ds.Tables[0].Rows[0]["MemberAccNo"].ToString();
                obj.BranchName = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                obj.IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                obj.NomineeName = ds.Tables[0].Rows[0]["NomineeName"].ToString();
                obj.NomineeRelation = ds.Tables[0].Rows[0]["NomineeRelation"].ToString();
                obj.NomineeAge = ds.Tables[0].Rows[0]["NomineeAge"].ToString();
            }
            else
            {
                obj.Status = "1";
                obj.Message = "No Record Found";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UpdateBankDetails(BankDetailsUpdateAPIResponse model)
        {
            Reponse obj = new Reponse();
            try
            {
                DataSet ds = model.BankUpdate();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.Message = "Bank Details Updated Successfully";
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        //[HttpPost]
        //public ActionResult AddWallet(AddWalletRequest model)
        //{
        //    Reponse obj = new Reponse();
        //    try
        //    {
        //        DataSet ds = model.AddWallet();
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //            {
        //                obj.Status = "0";
        //                obj.Message = "E-Wallet save successfully";
        //            }
        //            else
        //            {
        //                obj.Status = "1";
        //                obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj.Status = "1";
        //        obj.Message = ex.Message;
        //    }
        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //}


        public ActionResult GetUserDashBoard(AssociateDashBoard model)
        {
            DashboardResponse obj = new DashboardResponse();
            DataSet ds = model.GetAssociateDashboard();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.Message = "Record Found";
                obj.ActiveStatus = ds.Tables[2].Rows[0]["Status"].ToString();
                obj.TotalDonation = ds.Tables[0].Rows[0]["TotalDonation"].ToString();
                obj.TotalDirect = ds.Tables[0].Rows[0]["TotalDirect"].ToString();
                obj.ReferralIncentive = ds.Tables[6].Rows[0]["ReferralIncentive"].ToString();
                obj.LevelIncome = ds.Tables[6].Rows[0]["LevelIncome"].ToString();
                obj.LevelUpgradeIncome = ds.Tables[6].Rows[0]["LevelUpgradeIncome"].ToString();
                obj.ReferralSponsorIncome = ds.Tables[6].Rows[0]["ReferralSponsorIncome"].ToString();
                obj.MatrixIncomeUpdateDate = ds.Tables[6].Rows[0]["MatrixIncomeUpdateDate"].ToString();
                obj.MatrixIncomeLevel = ds.Tables[6].Rows[0]["MatrixIncomeLevel"].ToString();
                obj.ForeverMatrixIncome = ds.Tables[6].Rows[0]["ForeverMatrixIncome"].ToString();
                obj.ForeverLevelIncome = ds.Tables[6].Rows[0]["ForeverLevelIncome"].ToString();
                obj.TotalIncome = ds.Tables[6].Rows[0]["TotalIncome"].ToString();
                obj.TopupWallet = ds.Tables[6].Rows[0]["TopupWallet"].ToString();
                obj.TotalWalletAmount = ds.Tables[0].Rows[0]["TotalWalletAmount"].ToString();
                obj.WithdrawlAmount = ds.Tables[6].Rows[0]["WithdrawlAmount"].ToString();
                obj.MatrixWallet = ds.Tables[6].Rows[0]["MatrixWallet"].ToString();
                obj.CurrentLevel = ds.Tables[6].Rows[0]["CurrentLevel"].ToString();
                obj.UpgradeMatrix = ds.Tables[6].Rows[0]["UpgradeMatrix"].ToString();
                obj.Stewmatrixincome = ds.Tables[6].Rows[0]["Stewmatrixincome"].ToString();
                if (obj.ActiveStatus == "Active")
                {
                    obj.ReferralLink = "http://brainstewfoundation.com/Home/Registration?Pid=" + model.Fk_UserId;
                }
                else
                {
                    obj.ReferralLink = "";
                }
            }
            else
            {
                obj.Status = "0";
                obj.Message = "Record Not Found";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSponsorName(SponsorRequest model)
        {
            SponsorResponse obj = new SponsorResponse();
            DataSet ds = model.GetSponsor();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.Message = "Record Found";
                obj.SponsorName = ds.Tables[0].Rows[0]["FullName"].ToString();
            }
            else
            {
                obj.Status = "1";
                obj.Message = "Record Not Found";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult NewDetails(NewsRequest model)
        {
            NewsListResponse Response = new NewsListResponse();
            List<NewsResponse> lst = new List<NewsResponse>();
            DataSet ds = model.GetNewsDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Response.Status = "0";
                Response.Message = "Record Found";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    NewsResponse obj = new NewsResponse();
                    obj.NewsHeading = ds.Tables[0].Rows[0]["NewsHeading"].ToString();
                    obj.NewsBody = ds.Tables[0].Rows[0]["NewsBody"].ToString();
                    lst.Add(obj);
                }
                Response.lstnew = lst;
            }
            else
            {
                Response.Status = "1";
                Response.Message = "Record Not Found";
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult QRWalletDetails(QRandWalletRequest model)
        {
            lstQRandWalletResponse obj = new lstQRandWalletResponse();
            List<QRandWalletResponse> lst = new List<QRandWalletResponse>();
            DataSet ds = model.GetWalletBalance();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.Message = "Record Found";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    QRandWalletResponse obj1 = new QRandWalletResponse();

                    obj1.WalletBalance = Decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString()).ToString("0.00");
                    obj1.QRLink = "http://localhost:53404/assets/img/QR.jpeg, + http://localhost:53404/assets/img/QR1.jpeg, + http://localhost:53404/assets/img/QR2.jpeg";

                    lst.Add(obj1);
                }
                obj.lstwallet = lst;
            }
            else
            {
                obj.Status = "1";
                obj.Message = "Record Not Found";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddWallet(AddWalletRequest model, HttpPostedFileBase PostedFile)
        {
            Response response = new Response();
            try
            {
                if (PostedFile != null)
                {
                    model.PostedFile = "/Documents/" + Guid.NewGuid() + Path.GetExtension(PostedFile.FileName);
                    PostedFile.SaveAs(Path.Combine(Server.MapPath(model.PostedFile)));
                }

                model.DDChequeDate = string.IsNullOrEmpty(model.DDChequeDate) ? null : Common.ConvertToSystemDate(model.DDChequeDate, "dd/mm/yyyy");
                if (model.PaymentMode == "1")
                {
                    model.BankName = null;
                    model.BankBranch = null;
                }
                DataSet ds = model.AddWallet();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        response.Status = "0";
                        response.Message = "Requested successfully";
                    }
                    else
                    {
                        response.Status = "1";
                        response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult WalletList(EwalletRequestDetails model)
        {
            EwalletResponsDetails Response = new EwalletResponsDetails();
            List<EwalletRespons> lst = new List<EwalletRespons>();
            DataSet ds = model.GetEwalletRequestDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Response.Status = "0";
                Response.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    EwalletRespons obj = new EwalletRespons();
                    obj.RequestID = r["PK_RequestID"].ToString();
                    //obj.UserId = r["FK_UserId"].ToString();
                    //obj.RequestCode = r["RequestCode"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.Status = r["Status"].ToString();
                    //obj.BankName = r["BankName"].ToString();
                    obj.TransactionDate = r["RequestedDate"].ToString();
                    //obj.BankBranch = r["BankBranch"].ToString();
                    //obj.ChequeDDNo = r["ChequeDDNo"].ToString();
                    //obj.ChequeDDDate = r["ChequeDDDate"].ToString();
                    //obj.WalletId = r["WalletId"].ToString();
                    //obj.LoginId = r["LoginId"].ToString();
                    //obj.DisplayName = r["Name"].ToString();
                    //obj.Remark = r["Remark"].ToString();
                    lst.Add(obj);
                }
                Response.lstWalletDetails = lst;
            }
            else
            {
                Response.Status = "0";
                Response.Message = "Record Not Found";
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteWallet(DeleteWalletRequest model)
        {
            Response response = new Response();
            try
            {
                model.AddedBy = "1";
                DataSet ds = model.DeleteWallet();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        response.Status = "0";
                        response.Message = "Wallet requested deleted successfully";

                    }
                    else
                    {
                        response.Status = "1";
                        response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
                else
                {
                    response.Status = "1";
                    response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult WalletLedgerList(EWalletDetailsRequest model)
        {
            EWalletDetailsResponse Response = new EWalletDetailsResponse();
            List<EWalletDetailsResp> lst = new List<EWalletDetailsResp>();
            model.FK_UserId = model.FK_UserId == "" ? null : model.FK_UserId;
            model.LoginId = model.LoginId == "" ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetEWalletDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Response.Status = "0";
                Response.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    EWalletDetailsResp obj = new EWalletDetailsResp();
                    obj.Pk_EwalletId = r["Pk_EwalletId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    //obj.Balance = r["Balance"].ToString();
                    lst.Add(obj);
                }
                Response.lstWalletLedger = lst;
                Response.TotalCrAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
                Response.TotalDrAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
                Response.Available = double.Parse(ds.Tables[0].Compute("sum(CrAmount)-sum(DrAmount)", "").ToString()).ToString("n2");
            }
            else
            {
                Response.Status = "0";
                Response.Message = "Record Not Found";
            }

            //DataSet ds11 = model.GetWalletBalance();
            //if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            //{
            //    ViewBag.BalanceAmount = ds11.Tables[0].Rows[0]["amount"].ToString();
            //}
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetWalletBalanceForTransferToOtherWallet(GetWalletBalanceRequest model)
        {
            GetWalletBalanceResponse response = new GetWalletBalanceResponse();
            DataSet ds11 = model.GetWalletBalance();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                response.BalanceAmount = ds11.Tables[0].Rows[0]["amount"].ToString();
            }
            else
            {
                response.Status = "1";
                response.Message = "Record Not Found";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult TransferWallet(TransferToOtherWalletRequest model)
        {
            Response response = new Response();
            try
            {
                DataSet ds = model.TransfertoOther();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        response.Status = "0";
                        response.Message = "Transferred  successfully";
                    }
                    else
                    {
                        response.Status = "1";
                        response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DonationByWallet(GetDonationAmountRequest model)
        {
            GetDonationAmountResponse response = new GetDonationAmountResponse();
            #region GetDonationAmount
            DataSet ds1 = model.GetDonationAmount();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                response.DonationAmount = ds1.Tables[0].Rows[0]["Amount"].ToString();
                response.DonationPlanId = ds1.Tables[0].Rows[0]["Pk_DonationPlanId"].ToString();
            }
            else
            {

                response.Status = "1";
                response.Message = "Record Not Found";
            }
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
            {

                response.Status = "0";
                response.Message = "Record Found";
                response.UpdatedDonationPlanId = ds1.Tables[1].Rows[0]["UpdatedDonationId"].ToString();
            }
            else
            {

                response.Status = "1";
                response.Message = "Record Not Found";
            }
            #endregion
            #region Check Balance
            DataSet ds = model.GetWalletBalance();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                response.WalletBalance = Decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString()).ToString("0.00"); ;
            }
            else
            {
                response.Status = "1";
                response.Message = "Record Not Found";
            }
            #endregion
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Donation(DonationByWalletRequest model)
        {
            Response response = new Response();
            try
            {
                DataSet ds = model.DonationByWallet();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        response.Status = "0";
                        response.Message = "Donate Amount Successfully !!";
                    }
                    else
                    {

                        response.Status = "1";
                        response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    response.Status = "1";
                    response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                response.Status = "0";
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BrainMatrixDonation(BrainMatrixDonationRequest model)
        {

            BrainMatrixDonationResponse response = new BrainMatrixDonationResponse();
            #region GetDonationAmount
            DataSet ds1 = model.GetBrainMatrixPlanAmount();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                response.DonationAmount = ds1.Tables[0].Rows[0]["Amount"].ToString();
                response.DonationPlanId = ds1.Tables[0].Rows[0]["Pk_BrainMatrixPlanId"].ToString();
            }
            else
            {
                response.Status = "1";
                response.Message = "Record Not Found";
            }
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                response.UpdatedDonationPlanId = ds1.Tables[1].Rows[0]["UpdatedDonationId"].ToString();
            }
            else
            {
                response.Status = "1";
                response.Message = "Record Not Found";
            }
            #endregion
            #region Check Balance
            DataSet ds = model.GetWalletBalance();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                response.WalletBalance = Decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString()).ToString("0.00"); ;
            }
            else
            {
                response.Status = "1";
                response.Message = "Record Not Found";
            }
            #endregion
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult BrainDonation(DonationBrainMatrixPlanRequest model)
        {
            Response response = new Response();
            try
            {
                DataSet ds = model.DonationBrainMatrixPlan();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        response.Status = "0";
                        response.Message = "Donate Amount Successfully !!";
                    }
                    else
                    {
                        response.Status = "1";
                        response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {

                    response.Status = "1";
                    response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult StewMatrixDonation(GetStewMatrixPlanAmountRequest model)
        {
            GetStewMatrixPlanAmountResponse response = new GetStewMatrixPlanAmountResponse();
            #region GetDonationAmount
            DataSet ds1 = model.GetStewMatrixPlanAmount();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {

                response.Status = "0";
                response.Message = "Record Found";
                response.DonationAmount = ds1.Tables[0].Rows[0]["Amount"].ToString();
                response.DonationPlanId = ds1.Tables[0].Rows[0]["Pk_StewMatrixPlanId"].ToString();
            }
            else
            {

                response.Status = "1";
                response.Message = "Record Not Found";
            }
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
            {

                response.Status = "0";
                response.Message = "Record  Found";
                response.UpdatedDonationPlanId = ds1.Tables[1].Rows[0]["UpdatedDonationId"].ToString();
            }
            else
            {

                response.Status = "1";
                response.Message = "Record Not Found";
            }
            #endregion
            #region Check Balance
            DataSet ds = model.GetWalletBalance();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                response.WalletBalance = Decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString()).ToString("0.00"); ;
            }
            else
            {

                response.Status = "1";
                response.Message = "Record Not Found";
            }
            #endregion
            return Json(response, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult SaveStewMatrixDonation(DonationStewMatrixPlanRequest model)
        {
            Response response = new Response();
            try
            {
                DataSet ds = model.DonationStewMatrixPlan();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        response.Status = "0";
                        response.Message = "Donate Amount Successfully !!";
                    }
                    else
                    {
                        response.Status = "1";
                        response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {

                    response.Status = "1";
                    response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult TopUpList(TopUpListRequest model)
        {
            TopUpListResponse response = new TopUpListResponse();
            List<TopUpListResp> lst = new List<TopUpListResp>();
            model.Fk_UserId = model.Fk_UserId == "" ? null : model.Fk_UserId;
            model.LoginId = model.LoginId == "" ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds1 = model.GetTopUpDetails();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    TopUpListResp obj = new TopUpListResp();
                    //obj.InvestmentId = r["Pk_InvestmentId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    //obj.UsedFor = r["UsedFor"].ToString();
                    //obj.BV = r["BV"].ToString();
                    //obj.IsCalculated = r["IsCalculated"].ToString();
                    //obj.TransactionBy = r["TransactionBy"].ToString();
                    //obj.Status = r["Status"].ToString();
                    //obj.ROIPercentage = r["ROIPercentage"].ToString();
                    obj.TopUpDate = r["TopUpDate"].ToString();
                    //obj.ProductName = r["ProductName"].ToString();
                    lst.Add(obj);
                }
                response.lstTopUp = lst;
            }
            else
            {
                response.Status = "1";
                response.Message = "Record Not Found";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BrainMatrixDonationList(BrainMatrixDonationListRequest model)
        {
            BrainMatrixDonationListResponse response = new BrainMatrixDonationListResponse();
            List<BrainMatrixDonationListResp> lst = new List<BrainMatrixDonationListResp>();
            
            model.LoginId = model.LoginId == "" ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetBrainMatrixDonation();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    BrainMatrixDonationListResp obj = new BrainMatrixDonationListResp();
                    obj.FromName = r["Name"].ToString();
                    //obj.FromLoginId = r["LoginId"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    //obj.Level = r["BrainMatrixLevel"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                response.lst = lst;
            }
            else
            {
                response.Status = "1";
                response.Message = "Record Not Found";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult StewMatrixDonationList(StewMatrixDonationListRequest model)
        {
            StewMatrixDonationListResponse response = new StewMatrixDonationListResponse();
            List<StewMatrixDonationListResp> lst = new List<StewMatrixDonationListResp>();
            model.LoginId = model.LoginId == "" ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            
            DataSet ds = model.GetStewMatrixDonation();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    StewMatrixDonationListResp obj = new StewMatrixDonationListResp();
                    obj.FromName = r["Name"].ToString();
                    //obj.FromLoginId = r["LoginId"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    //obj.Level = r["stewMatrixLevel"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                response.lst = lst;
            }
            else
            {
                response.Status = "1";
                response.Message = "Record Not Found";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DirectListBy(DirectListRequest model)
        {

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DirectListResponse response = new DirectListResponse();
            List<DirectListResp> lst = new List<DirectListResp>();

            if (model.Ids == null || model.Ids == "")
            {
                model.Ids = model.Fk_UserId;

            }
            DataSet ds = model.GetDirectList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                string Ids = "";
                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    DirectListResp obj = new DirectListResp();
                    obj.Mobile = r["Mobile"].ToString();
                    //obj.Email = r["Email"].ToString();
                    obj.SponsorId = r["SponsorId"].ToString();
                    //obj.SponsorName = r["SponsorName"].ToString();
                    //obj.JoiningDate = r["JoiningDate"].ToString();
                    //obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.LoginId = (r["LoginId"].ToString());
                    obj.Name = (r["Name"].ToString());
                    obj.Level = (r["Lvl"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    Ids = Ids + r["PK_UserId"].ToString() + ",";
                    lst.Add(obj);
                }
                response.lstassociate = lst;
                model.Ids = Ids;
            }
            else
            {
                response.Status = "1";
                response.Message = "Record Not Found";
            }
            //List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            //ViewBag.ddlStatus = AssociateStatus;
            //List<SelectListItem> Leg = Common.LegType();
            //ViewBag.ddlleg = Leg;
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult GetTTPMembersCountLevelWise(LevelTreeReq req)
        {
            LevelTreeAPI res = new LevelTreeAPI();
            try
            {
                List<LevelTreeMembers> lst = new List<LevelTreeMembers>();
                List<LevelTreeMemberDetails> lstMember = new List<LevelTreeMemberDetails>();
                DataSet ds = req.GetLevelMembersCountTR1();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        res.Status1 = "1";
                        res.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                    else
                    {
                        res.Status1 = "0";
                        res.Message = "Record Found";
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            LevelTreeMembers obj = new LevelTreeMembers();
                            obj.LevelName = r["LevelNo"].ToString();
                            obj.TargetMember = r["TargetMember"].ToString();
                            obj.NumberOfMembers = r["TotalAssociate"].ToString();
                            lst.Add(obj);
                        }
                        res.lst = lst;
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                        {
                            if (ds.Tables[1].Rows[0]["Lvl"].ToString() == "10")
                            {

                            }
                            res.Level = ds.Tables[1].Rows[0]["Lvl"].ToString();
                            res.Status = ds.Tables[1].Rows[0]["Status"].ToString();
                            res.Color = ds.Tables[1].Rows[0]["Color"].ToString();
                            res.DisplayName = ds.Tables[1].Rows[0]["Name"].ToString();
                            res.PK_UserId = ds.Tables[1].Rows[0]["PK_UserId"].ToString();
                            res.ProfilePic = ds.Tables[1].Rows[0]["ProfilePic"].ToString();
                            res.TotalDirect = ds.Tables[1].Rows[0]["TotalDirect"].ToString();
                            res.TotalActive = ds.Tables[1].Rows[0]["TotalActive"].ToString();
                            res.TotalInactive = ds.Tables[1].Rows[0]["TotalInActive"].ToString();
                            res.TotalTeam = ds.Tables[1].Rows[0]["TotalTeam"].ToString();
                            res.TotalActiveTeam = ds.Tables[1].Rows[0]["TotalActiveTeam"].ToString();
                            res.TotalInActiveTeam = ds.Tables[1].Rows[0]["TotalInActiveTeam"].ToString();
                            res.SponsorName = ds.Tables[1].Rows[0]["SponsorName"].ToString();
                        }
                        DataSet ds1 = req.GetLevelMembers("1", res.PK_UserId);
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow r in ds1.Tables[0].Rows)
                            {
                                LevelTreeMemberDetails obj = new LevelTreeMemberDetails();
                                obj.PK_UserId = r["PK_UserId"].ToString();
                                obj.MemberName = r["MemberName"].ToString();
                                obj.LoginId = r["LoginId"].ToString();
                                obj.Level = r["Lvl"].ToString();
                                obj.ProfilePic = r["ProfilePic"].ToString();
                                obj.Status = r["Status"].ToString();
                                obj.SelfBV = r["SelfBV"].ToString();
                                obj.TeamBV = r["TeamBV"].ToString();
                                obj.SponsorName = r["SponsorName"].ToString();
                                obj.Color = r["Color"].ToString();
                                lstMember.Add(obj);
                            }
                            res.lstDetails = lstMember;
                        }
                        else
                        {
                            res.Status1 = "1";
                            res.Message = "No Record Found";
                            res.lstDetails = lstMember;
                        }
                    }
                }
                else
                {
                    res.Status = "1";
                    res.Message = "No Record Found";
                }

            }
            catch (Exception ex)
            {
                res.Status = "1";
                res.Message = ex.Message;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult AssociateTree(AssociateTreeRequest model)
        {
            AssociateTreeResponse response = new AssociateTreeResponse();
            List<AssociateTreeRespo> lst = new List<AssociateTreeRespo>();
            DataSet ds = model.GetDownlineTree();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateTreeRespo obj = new AssociateTreeRespo();
                    //obj.Fk_UserId = r["Pk_UserId"].ToString();
                    //obj.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.Status = r["Status"].ToString();
                    //obj.ActiveStatus = r["ActiveStatus"].ToString();
                    obj.SponsorID = r["SponsorId"].ToString();
                    //obj.SponsorName = r["SponsorName"].ToString();
                    obj.ActivationDate = r["PermanentDate"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    //obj.Lvl = r["Level"].ToString();
                    lst.Add(obj);
                }
                response.lstPlot = lst;
            }
            else
            {
                response.Status = "1";
                response.Message = "No Record Found";
            }

          return Json(response, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult BusinessReports(BusinessReportsRequest model)
        {
            BusinessReportsResponse response = new BusinessReportsResponse();
            List<BusinessReportsResp> lst = new List<BusinessReportsResp>();
            model.LoginId = model.LoginId == "" ? null : model.LoginId;
            if (model.IsDownline == "on")
            {
                model.IsDownline = "1";
            }
            else
            {
                model.IsDownline = "0";
            }
            model.Level = model.Level == "0" ? null : model.Level;
            model.IsDownline = model.IsDownline == "0" ? null : model.IsDownline;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");      
            DataSet ds = model.GetBusinessReports();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    BusinessReportsResp obj = new BusinessReportsResp();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["FirstName"].ToString();
                    obj.Amount = Convert.ToDecimal(r["Amount"].ToString());
                    obj.Date = r["Date"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.PackageType = r["PackageType"].ToString();
                    lst.Add(obj);
                }
                response.lstBReports = lst;
                response.TotalAmount = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
                response.TotalBV = double.Parse(ds.Tables[0].Compute("sum(BV)", "").ToString()).ToString("n2");
            }
            else
            {
                response.Status = "1";
                response.Message = "Record Not Found";
            }

            //#region ddlPlotSize
            //int count = 0;
            //List<SelectListItem> ddlProductName = new List<SelectListItem>();
            //DataSet dss = model.GetProductName();
            //if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in dss.Tables[0].Rows)
            //    {
            //        if (count == 0)
            //        {
            //            ddlProductName.Add(new SelectListItem { Text = "-Select-", Value = "" });
            //        }
            //        ddlProductName.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
            //        count = count + 1;
            //    }
            //}

            //ViewBag.ddlProductName = ddlProductName;
            //#endregion
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LevelIncomeTr2(LevelIncomeTr2Request model)
        {
            LevelIncomeTr2Response response = new LevelIncomeTr2Response();
            List<LevelIncomeTr2Resp> lst = new List<LevelIncomeTr2Resp>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");       
            DataSet ds = model.LevelIncomeTr2();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    LevelIncomeTr2Resp obj = new LevelIncomeTr2Resp();
                    obj.FromName = r["FromName"].ToString();
                    //obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    //obj.Percentage = r["CommissionPercentage"].ToString();
                    //obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    //obj.Amount = r["Amount"].ToString();
                    //obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                response.lst = lst;
            }
            else
            {
                response.Status = "1";
                response.Message = "Record Not Found";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }







        [HttpPost]
        public ActionResult LevelIncomeTr1(LevelIncomeTr1Request model)
        {
            LevelIncomeTr1Response response = new LevelIncomeTr1Response();
            List<LevelIncomeTr1Resp> lst = new List<LevelIncomeTr1Resp>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.LevelIncomeTr1();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    LevelIncomeTr1Resp obj = new LevelIncomeTr1Resp();
                    obj.FromName = r["FromName"].ToString();
                    //obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    //obj.Percentage = r["CommissionPercentage"].ToString();
                    //obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    //obj.Amount = r["Amount"].ToString();
                    //obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                response.lst = lst;
            }
            else
            {
                response.Status = "1";
                response.Message = "Record Not Found";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult PlacementBenefits(PlacementBenefitsRequest model)
        {
            PlacementBenefitsResponse response = new PlacementBenefitsResponse();
            List<PlacementBenefitsResp> lst = new List<PlacementBenefitsResp>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.PlacementBenefits();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    PlacementBenefitsResp obj = new PlacementBenefitsResp();
                    obj.FromName = r["FromName"].ToString();
                    //obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    //obj.Percentage = r["CommissionPercentage"].ToString();
                    //obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    //obj.Amount = r["Amount"].ToString();
                    //obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                response.lst = lst;
            }
            else
            {
                response.Status = "1";
                response.Message = "Record Not Found";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpgradeBenefits(UpgradeBenefitsRequest model)
        {
            UpgradeBenefitsResponse response = new UpgradeBenefitsResponse();
            List<UpgradeBenefitsResp> lst = new List<UpgradeBenefitsResp>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetbenefitsReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UpgradeBenefitsResp obj = new UpgradeBenefitsResp();
                    obj.FromName = r["FromName"].ToString();
                    //obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    //obj.Percentage = r["CommissionPercentage"].ToString();
                    //obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    //obj.Amount = r["Amount"].ToString();
                    //obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                response.lst = lst;
            }
            else
            {
                response.Status = "1";
                response.Message = "Record Not Found";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult BrainMatrixBenefits(BrainMatrixBenefitsRequest model)
        {
            BrainMatrixBenefitsResponse response = new BrainMatrixBenefitsResponse();
            List<BrainMatrixBenefitsResp> lst = new List<BrainMatrixBenefitsResp>();
            DataSet ds = model.GetbenefitReportNew();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                response.Status = "0";
                response.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    BrainMatrixBenefitsResp obj = new BrainMatrixBenefitsResp();
                    //obj.FromName = r["FromName"].ToString();
                    //obj.FromLoginId = r["LoginId"].ToString();
                    //obj.BusinessAmount = r["BusinessAmount"].ToString();
                    //obj.Percentage = r["CommissionPercentage"].ToString();
                    //obj.PayoutNo = r["PayoutNo"].ToString();
                    //obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    //obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                response.lst = lst;
                response.TotalBenefits = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
            }
            else
            {
                response.Status = "1";
                response.Message = "Record Not Found";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        


    }
}