﻿using BrainStew.Filter;
using BrainStew.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BrainStew.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //return Redirect("/Home/Login");
            //return View();
            return Redirect("~/BrainStew/index.html");
        }
        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }
        public ActionResult LoginAction(Home obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Home Modal = new Home();
                DataSet ds = obj.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        if ((ds.Tables[0].Rows[0]["UserType"].ToString() == "Associate"))
                        {

                            string pass = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                            if (obj.Password == Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
                            {
                                Session["BenefitsLevel"] = ds.Tables[0].Rows[0]["BenefitsLevel"].ToString();
                                Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                                Session["Pk_userId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
                                Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                                Session["FullName"] = ds.Tables[0].Rows[0]["FullName"].ToString();
                                Session["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();
                                Session["TransPassword"] = ds.Tables[0].Rows[0]["TransPassword"].ToString();
                                Session["Profile"] = ds.Tables[0].Rows[0]["Profile"].ToString();
                                Session["Gender"] = ds.Tables[0].Rows[0]["Sex"].ToString();
                                Session["Branch"] = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                                Session["Bank"] = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                                Session["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
                                Session["Encrytid"] = Crypto.Encrypt(ds.Tables[0].Rows[0]["Pk_userId"].ToString());
                                if (ds.Tables[0].Rows[0]["TeamPermanent"].ToString() == "O" || ds.Tables[0].Rows[0]["TeamPermanent"].ToString() == "P")
                                {
                                    Session["IdActivated"] = true;
                                    FormName = "UserDashBoard";
                                    Controller = "User";
                                }
                                else
                                {
                                    Session["IdActivated"] = true;
                                    FormName = "UserDashBoard";
                                    Controller = "User";
                                    //FormName = "CompleteRegistration";
                                    //Controller = "Home";
                                }

                            }
                            else
                            {
                                TempData["Login"] = "Incorrect Password";
                                FormName = "Login";
                                Controller = "Home";

                            }
                        }
                        else if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                        {
                            Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                            Session["UsertypeName"] = ds.Tables[0].Rows[0]["UsertypeName"].ToString();
                            Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();

                            if (ds.Tables[0].Rows[0]["isFranchiseAdmin"].ToString() == "True")
                            {
                                Session["FranchiseAdminID"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                                FormName = "Registration";
                                Controller = "FranchiseAdmin";
                            }
                            else
                            {
                                FormName = "AdminDashBoard";
                                Controller = "Admin";
                            }
                        }
                        else
                        {
                            TempData["Login"] = "Incorrect LoginId Or Password";
                            FormName = "Login";
                            Controller = "Home";
                        }
                    }
                    else
                    {
                        TempData["Login"] = "Incorrect LoginId Or Password";
                        FormName = "Login";
                        Controller = "Home";

                    }

                }

                else
                {
                    TempData["Login"] = "Incorrect LoginId Or Password";
                    FormName = "Login";
                    Controller = "Home";
                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                FormName = "Login";
                Controller = "Home";
            }

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult Registration(string PId, Home obj)
        {
            //Home obj = new Home();
            List<SelectListItem> Gender = Common.BindGender();
            // obj.SponsorId = Crypto.Decrypt(PId);
            ViewBag.Gender = Gender;
            if (!string.IsNullOrEmpty(PId))
            {
                obj.Fk_UserId = Crypto.Decrypt(PId); ;
                // var d = Crypto.Decrypt(PId);
                DataSet ds = obj.GetMemberNameWithUserId();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.SponsorId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                }
                // ViewBag.SponsorId = d.Split('|')[0];
                //ViewBag.Leg = d.Split('|')[1];
            }
            return View(obj);
        }

        public ActionResult RegistrationAction(string SponsorId, string FirstName, string LastName, string MobileNo, string PinCode, string Leg, string Password, string Email, string Gender, string State, string City)
        {
            Home obj = new Home();
            try
            {
                obj.SponsorId = SponsorId;
                obj.FirstName = FirstName;
                obj.LastName = LastName;
                obj.MobileNo = MobileNo;
                obj.RegistrationBy = "Web";
                obj.PinCode = PinCode;
                obj.Leg = null;
                obj.Password = Crypto.Encrypt(Password);
                obj.Email = Email;
                obj.Gender = Gender;
                obj.State = State;
                obj.City = City;
                DataSet ds = obj.Registration();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        Session["Pk_userId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["DisplayName"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["PassWord"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        Session["Transpassword"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        Session["MobileNo"] = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        Session["Profile"] = "";
                        obj.Result = "1";
                        if (obj.Email != "" && obj.Email != null)
                        {
                            //try
                            //{
                            //    BLMail.SendRegistrationMail(Session["DisplayName"].ToString(), Session["LoginId"].ToString(), Session["PassWord"].ToString(), "Registration Successful", obj.Email);
                            //}
                            //catch (Exception ex)
                            //{
                            //    obj.Result = ex.Message;
                            //}
                        }
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ConfirmRegistration()
        {
            return View();
        }
        public ActionResult CompleteRegistration(Home model)
        {
            if (Session["LoginId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            #region Product Bind
            Common objcomm = new Common();

            objcomm.Fk_UserId = Session["Pk_userId"].ToString();
            DataSet dsbalance = objcomm.GetWalletBalance();
            if (dsbalance != null && dsbalance.Tables.Count > 0 && dsbalance.Tables[0].Rows.Count > 0)
            {
                model.WalletBalance = dsbalance.Tables[0].Rows[0]["amount"].ToString();
            }
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProductForJoiningForUser();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    //if (count == 0)
                    //{
                    //    ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    //}
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }
            ViewBag.ddlProduct = ddlProduct;
            #endregion
            return View(model);
        }
        public ActionResult FillAmount(string ProductId)
        {
            Admin obj = new Admin();
            obj.Package = ProductId;
            DataSet ds = obj.BindPriceByProduct();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.Amount = Convert.ToInt32(ds.Tables[0].Rows[0]["FinalAmount"]).ToString();
            }
            else { }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult emailtemplate()
        {
            return View();
        }
        public ActionResult GetSponserDetails(string ReferBy)
        {
            Common obj = new Common();
            obj.ReferBy = ReferBy;
            DataSet ds = obj.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();
                obj.Result = "Yes";


            }
            else { obj.Result = "Invalid SponsorId"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStateCity(string PinCode)
        {
            Common obj = new Common();
            obj.PinCode = PinCode;
            DataSet ds = obj.GetStateCity();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                obj.Result = "1";
            }
            else
            {
                obj.Result = "Invalid PinCode";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #region Menu
        public virtual ActionResult Menu()
        {
            Home Menu = null;

            if (Session["_Menu"] != null)
            {
                Menu = (Home)Session["_Menu"];
            }
            else
            {

                Menu = Home.GetMenus(Session["Pk_AdminId"].ToString(), Session["UserTypeName"].ToString()); // pass employee id here
                Session["_Menu"] = Menu;
            }
            return PartialView("_Menu", Menu);
        }
        #endregion
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ForgetPassword")]
        [OnAction(ButtonName = "forgetpassword")]
        public ActionResult ForgetPassword(Home model)
        {

            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();

            try
            {
                DataSet ds = model.ForgetPassword();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                        model.Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());

                        string signature = " &nbsp;&nbsp;&nbsp; Dear  " + model.Name + ",<br/>&nbsp;&nbsp;&nbsp; Your Password Is : " + model.Password;

                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress("email@gmail.com");
                            mail.To.Add(model.Email);
                            mail.Subject = "Forget Password";
                            mail.Body = signature;
                            mail.IsBodyHtml = true;
                            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                            {

                                //smtp.Credentials = new NetworkCredential("developer2.afluex@gmail.com", "devel@#123");
                                smtp.Credentials = new NetworkCredential("coustomer.BrainStew@gmail.com", "BrainStew@2022");
                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                            }
                        }
                        TempData["Login"] = "password sent your email-id successfully.";
                    }

                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        //TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        TempData["Login"] = "Please contact to admin";
                    }
                }
                else
                {
                    TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
            }
            return RedirectToAction("Login", "Home");
        }
        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult Maintenance()
        {
            return View();
        }


        public ActionResult CalculateLevelIncomeTr2()
        {
            Home model = new Home();
            DataSet ds = model.CalculateLevelIncomeTr2();
            return View();
        }
        public ActionResult CalculateROI()
        {
            Home model = new Home();
            DataSet ds = model.CalculateROI();
            return View();
        }
        public ActionResult ActivateUser(string Amount)
        {
            Home model = new Home();
            try
            {
                model.Fk_UserId = Session["Pk_UserId"].ToString();
                model.Amount = Amount;
                DataSet ds = model.ActivateUser();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        model.Result = "1";
                        return View("UserDashboard", "User");
                    }
                    else
                    {
                        return RedirectToAction("CompleteRegistration", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("CompleteRegistration", "Home");
                }
                // Return on PaymentPage with Order data
            }
            catch (Exception ex)
            {
                model.Result = "0";
                TempData["error"] = ex.Message;
                return RedirectToAction("CompleteRegistration", "Home");
            }
        }
        public ActionResult CalculateIncome()
        {
            Home model = new Home();
            DataSet ds = model.CalculateLevelIncome();
            DataSet ds1 = model.TransferPlacementUpgradeIncome();
            return View();
        }
        public ActionResult CharityDonation(Home model)
        {
            #region Payment Mode
            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
            DataSet ds1 = model.PaymentList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                }
            }
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            return View(model);
        }

        public ActionResult ChildrenDonation(Home model, string Id)
        {
            List<Home> lst = new List<Home>();
            if (Id != null)
            {
                model.DonationId = Id;
                DataSet ds = model.GetChildrenDonationDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.Pk_DonationId = ds.Tables[0].Rows[0]["Pk_DonationId"].ToString();
                    ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    ViewBag.ChildName = ds.Tables[0].Rows[0]["ChildName"].ToString();
                    ViewBag.TotalDaughter = ds.Tables[0].Rows[0]["TotalDaughter"].ToString();
                    ViewBag.TotalSon = ds.Tables[0].Rows[0]["TotalSon"].ToString();
                    ViewBag.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                    ViewBag.Need = ds.Tables[0].Rows[0]["Need"].ToString();
                    ViewBag.NeedAmount = ds.Tables[0].Rows[0]["NeedAmount"].ToString();
                    ViewBag.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                    ViewBag.Image = ds.Tables[0].Rows[0]["ChildImage"].ToString();
                    ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    ViewBag.ApprovedAmount = ds.Tables[0].Rows[0]["ApprovedAmount"].ToString();
                    ViewBag.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                    ViewBag.TotalDonation = ds.Tables[0].Rows[0]["TotalDonation"].ToString();
                }
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("ChildrenDonation")]
        [OnAction(ButtonName = "SaveBilling")]
        public ActionResult SaveChildrenDonation(Home model)
        {
           
            DataSet ds = model.SaveBillingDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["SaveBilling"] = "Donated amount succesfully !!";
                   // model.Result = "yes";
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["SaveBilling"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                TempData["SaveBilling"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
            return RedirectToAction("ChildrenDonationList", "Home");
        }

        [HttpPost]
        public JsonResult SaveDonationDetails(Home model, HttpPostedFileBase Image)
        {
            var profile = Request.Files;
            bool status = false;
            var sisterdatavalue = Request["SistersdataValue"];
            var SisCount = Request["Siscount"];
            var BroCount = Request["BroCount"];
            if (Convert.ToInt32(SisCount) > 0)
            {
                var jssss = new JavaScriptSerializer();
                var jdvvv = jssss.Deserialize<dynamic>(Request["SistersdataValue"]);
                DataTable dtSistersDetails = new DataTable();
                dtSistersDetails = JsonConvert.DeserializeObject<DataTable>(jdvvv["SistersAddData"]);
                model.dtSistersDetails = dtSistersDetails;
            }
            var brotherdatavalue = Request["BrothersdataValue"];
            if (Convert.ToInt32(BroCount) > 0)
            {
                var brojssc = new JavaScriptSerializer();
                var brojdv = brojssc.Deserialize<dynamic>(Request["BrothersdataValue"]);
                DataTable dtBrothersDetails = new DataTable();
                dtBrothersDetails = JsonConvert.DeserializeObject<DataTable>(brojdv["BrothersAddData"]);
                model.dtBrothersDetails = dtBrothersDetails;
            }
            if (Image != null)
            {
                model.Image = "/ChildImageUpload/" + Guid.NewGuid() + Path.GetExtension(Image.FileName);
                Image.SaveAs(Path.Combine(Server.MapPath(model.Image)));
            }

            #region Payment Mode
            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
            DataSet ds1 = model.PaymentList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                }
            }
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion

            DataSet ds = new DataSet();
            ds = model.SaveDonationDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["Donation"] = "Donated successfully";
                    model.Result = "1";
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    model.Result = "0";
                    TempData["Donation"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "0";

                TempData["Donation"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
            return new JsonResult { Data = new { status = status } };
            //return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChildrenDonationList(Home model)
        {
            List<Home> lst = new List<Home>();
            DataSet ds = model.GetChildrenDonationList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Home obj = new Home();
                    //obj.Fk_UserId = dr["loginId"].ToString();
                    obj.DonationId = dr["Pk_DonationId"].ToString();
                    obj.ChildName = dr["ChildName"].ToString();
                    obj.DOB = dr["DOB"].ToString();
                    obj.Need = dr["Need"].ToString();
                    obj.NeedAmount = dr["NeedAmount"].ToString();
                    obj.Description = dr["Description"].ToString();
                    obj.Image = dr["ChildImage"].ToString();
                    obj.Address = dr["Address"].ToString();
                    obj.ApprovedAmount = dr["ApprovedAmount"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }

        //[HttpPost]
        //public ActionResult SaveBillingDetails(Home model, string FK_DonationId, string FirstName, string LastName,
        //    string Email, string Mobile,string PinCode, string PanNumber, string Address, string TotalDonation)
        //{
        //    DataSet ds = new DataSet();
        //    model.FK_DonationId = FK_DonationId;
        //    model.FirstName = FirstName;
        //    model.LastName = LastName;
        //    model.Email = Email;
        //    model.MobileNo = Mobile;
        //    model.PinCode = PinCode;
        //    model.PanNo = PanNumber;
        //    model.TotalDonation = TotalDonation;
        //    ds = model.SaveBillingDetails();
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //        {
        //            //TempData["SaveBilling"] = "Donated amount succesfully !!";
        //            model.Result = "yes";
        //        }
        //        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
        //        {
        //            model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        TempData["SaveBilling"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //    }
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}
        
        [HttpPost]
        public ActionResult GetStateCity(Home model, string PinCode)
        {
            DataSet ds = new DataSet();
            model.PinCode = PinCode;
            ds = model.GetStateCity();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                    model.Result = "yes";
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
              
            }
            else
            {
                model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        

    }
}