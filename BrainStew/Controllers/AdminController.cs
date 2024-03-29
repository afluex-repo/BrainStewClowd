﻿using BrainStew.Filter;
using BrainStew.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
namespace BrainStew.Controllers
{
    public class AdminController : AdminBaseController
    {
        // GET: Admin
        public ActionResult AdminDashboard()
        {
            Dashboard newdata = new Dashboard();
            DataSet Ds = newdata.GetDashBoardDetails();
            ViewBag.TotalUsers = Ds.Tables[1].Rows[0]["TotalUsers"].ToString();
            ViewBag.BlockedUsers = Ds.Tables[1].Rows[0]["BlockedUsers"].ToString();
            ViewBag.InactiveUsers = Ds.Tables[1].Rows[0]["InactiveUsers"].ToString();
            ViewBag.ActiveUsers = Ds.Tables[1].Rows[0]["ActiveUsers"].ToString();
            if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[2].Rows.Count > 0)
            {
                ViewBag.Tr1Business = Ds.Tables[2].Rows[0]["Tr1Business"].ToString();
                ViewBag.Tr2Business = Ds.Tables[2].Rows[0]["Tr2Business"].ToString();
            }
            return View(newdata);
        }
        #region GeneratePin
        public ActionResult Generate_EPin()
        {
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion
            #region PaymentMode
            Common com = new Common();
            List<SelectListItem> ddlPayment = new List<SelectListItem>();
            DataSet ds = com.PaymentList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int paycount = 0;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (paycount == 0)
                    {
                        ddlPayment.Add(new SelectListItem { Text = "Select Payment", Value = "0" });
                    }
                    ddlPayment.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    paycount++;
                }
            }

            ViewBag.ddlPayment = ddlPayment;

            #endregion
            return View();
        }
        [HttpPost]
        [ActionName("Generate_EPin")]
        [OnAction(ButtonName = "btn_Pin")]
        public ActionResult CreatePinAction(Admin obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.CreatePin();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["createpin"] = "Pin Created Successfully";
                    }
                    else
                    {
                        TempData["createpin"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else { }

            }
            catch (Exception ex)
            {
                TempData["createpin"] = ex.Message;
            }
            return RedirectToAction("Generate_EPin", "Admin");
        }
        public ActionResult UnUsedPin(Admin obj)
        {

            List<Admin> lst = new List<Admin>();
            obj.Package = obj.Package == "0" ? null : obj.Package;
            DataSet ds = obj.GetUsedUnUsedPins();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin Objload = new Admin();
                    Objload.ePinNo = dr["ePinNo"].ToString();
                    Objload.Package = dr["Package"].ToString();
                    Objload.DisplayName = dr["PinUser"].ToString();
                    Objload.AddedOn = dr["CreatedDate"].ToString();
                    Objload.RegisteredTo = dr["RegisteredTo"].ToString();
                    Objload.Status = dr["PinStaus"].ToString();
                    lst.Add(Objload);
                }
                obj.lstunusedpins = lst;
            }
            #region Product Bind

            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }
            ViewBag.ddlProduct = ddlProduct;
            #endregion
            return View(obj);
        }
        public ActionResult FillAmount(string ProductId)
        {
            Admin obj = new Admin();
            obj.Package = ProductId;
            DataSet ds = obj.BindPriceByProduct();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.Amount = ds.Tables[0].Rows[0]["ProductPrice"].ToString();
            }
            else { }
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        #endregion
        public ActionResult GetMemberName(string LoginId)
        {
            Common obj = new Common();
            obj.ReferBy = LoginId;
            DataSet ds = obj.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();
                obj.Result = "Yes";
            }
            else { obj.Result = "Invalid LoginId"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ChangePassword")]
        public ActionResult ChangePassword(Admin model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ChangePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Password Changed  Successfully";
                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("ChangePassword", "Admin");
        }
        public ActionResult PinTransferReportForAdmin()
        {
            AdminReports model = new AdminReports();
            List<AdminReports> lst = new List<AdminReports>();
            DataSet ds = model.GetTransferPinReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ePinNo = r["EpinNo"].ToString();
                    obj.FromId = r["FromId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToId = r["ToId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.TransferDate = r["TransferDate"].ToString();
                    lst.Add(obj);
                }
                model.lstPinTransfer = lst;
            }
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "GetDetails")]
        [ActionName("PinTransferReportForAdmin")]
        public ActionResult PinTransferReportForAdmin(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            model.ePinNo = model.ePinNo == "0" ? null : model.ePinNo;
            model.LoginId = model.LoginId == "0" ? null : model.LoginId;
            model.ToLoginID = model.ToLoginID == "0" ? null : model.ToLoginID;
            DataSet ds = model.GetTransferPinReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ePinNo = r["EpinNo"].ToString();
                    obj.FromId = r["FromId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToId = r["ToId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.TransferDate = r["TransferDate"].ToString();
                    lst.Add(obj);
                }
                model.lstPinTransfer = lst;
            }
            return View(model);
        }
        public ActionResult WalletList()
        {
            Admin model = new Admin();
            List<Admin> lst = new List<Admin>();
            DataSet ds = model.GetEwalletRequestDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.RequestID = r["PK_RequestID"].ToString();
                    obj.UserId = r["FK_UserId"].ToString();
                    obj.RequestCode = r["RequestCode"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.BankName = r["BankName"].ToString();
                    obj.TransactionDate = r["RequestedDate"].ToString();
                    obj.BankBranch = r["BankBranch"].ToString();
                    obj.ChequeDDNo = r["ChequeDDNo"].ToString();
                    obj.ChequeDDDate = r["ChequeDDDate"].ToString();
                    obj.WalletId = r["WalletId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.DisplayName = r["Name"].ToString();
                    obj.PostedFile = r["Document"].ToString();
                    lst.Add(obj);
                }
                model.lstWallet = lst;
            }
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "GetDetails")]
        [ActionName("WalletList")]
        public ActionResult WalletList(Admin model)
        {
            List<Admin> lst = new List<Admin>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetEwalletRequestDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.RequestID = r["PK_RequestID"].ToString();
                    obj.UserId = r["FK_UserId"].ToString();
                    obj.RequestCode = r["RequestCode"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.BankName = r["BankName"].ToString();
                    obj.BankBranch = r["BankBranch"].ToString();
                    obj.ChequeDDNo = r["ChequeDDNo"].ToString();
                    obj.ChequeDDDate = r["ChequeDDDate"].ToString();
                    obj.WalletId = r["WalletId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.DisplayName = r["Name"].ToString();
                    obj.PostedFile = r["Document"].ToString();
                    lst.Add(obj);
                }
                model.lstWallet = lst;
            }
            return View(model);
        }
        public ActionResult Approve(string id)
        {
            try
            {
                Admin model = new Admin();
                model.RequestID = id;
                model.Status = (model.Status = "Approved");
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ApproveDeclineEwalletRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Ewallet Request Approved Successfully";
                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("WalletList", "Admin");
        }
        public ActionResult DeClined(string id)
        {
            try
            {
                Admin model = new Admin();
                model.RequestID = id;
                model.Status = (model.Status = "Declined");
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ApproveDeclineEwalletRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Ewallet Request Declined Successfully";
                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("WalletList", "Admin");
        }
        public ActionResult PaymentTypeMaster()
        {

            #region ddlSites
            Admin obj = new Admin();
            int count = 0;
            List<Admin> lst = new List<Admin>();
            List<SelectListItem> ddlPaymentRype = new List<SelectListItem>();
            DataSet ds1 = obj.GetPaymentType();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlPaymentRype.Add(new SelectListItem { Text = "-Select-", Value = "" });
                    }
                    ddlPaymentRype.Add(new SelectListItem { Text = r["PaymentType"].ToString(), Value = r["PK_PaymentTypeId"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlPaymentRype = ddlPaymentRype;
            #endregion
            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                Admin model = new Admin();
                model.PaymentType = dr["PaymentType"].ToString();
                if (dr["IsActive"].ToString() == "True")
                {
                    model.Status = "Active";
                }
                else
                {
                    model.Status = "Inactive";
                }
                lst.Add(model);
            }
            obj.lstWallet = lst;
            return View(obj);
        }
        [HttpPost]
        [OnAction(ButtonName = "Update")]
        [ActionName("PaymentTypeMaster")]
        public ActionResult PaymentTypeMaster(Admin model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                //model.PaymentTypeId = model.PaymentTypeId;
                DataSet ds = model.UpdatePaymentType();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Payment type update successfully";
                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("PaymentTypeMaster", "Admin");
        }
        public ActionResult EPinRequestList()
        {
            Admin model = new Admin();
            List<Admin> list = new List<Admin>();
            DataSet dss = model.GetEPinRequestDetails();
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dss.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.PK_RequestID = r["PK_RequestID"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Fk_Paymentid = r["PaymentMode"].ToString();
                    obj.BankName = r["BankName"].ToString();
                    obj.BankBranch = r["BankBranch"].ToString();
                    obj.TransactionNo = r["ChequeDDNo"].ToString();
                    obj.TransactionDate = r["ChequeDDDate"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.NoofPins = r["NoOfPins"].ToString();

                    list.Add(obj);
                }
                model.lstEpinRequest = list;
            }
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "GetDetails")]
        [ActionName("EPinRequestList")]
        public ActionResult EPinRequestList(Admin model)
        {
            List<Admin> list = new List<Admin>();
            model.Name = model.Name == "0" ? null : model.Name;
            model.LoginId = model.LoginId == "0" ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet dss = model.GetEPinRequestDetails();
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dss.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.PK_RequestID = r["PK_RequestID"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Fk_Paymentid = r["PaymentMode"].ToString();
                    obj.BankName = r["BankName"].ToString();
                    obj.BankBranch = r["BankBranch"].ToString();
                    obj.TransactionNo = r["ChequeDDNo"].ToString();
                    obj.TransactionDate = r["ChequeDDDate"].ToString();
                    obj.Status = r["Status"].ToString();
                    list.Add(obj);
                }
                model.lstEpinRequest = list;
            }
            return View(model);
        }
        public ActionResult AcceptedEPinRequest(string id)
        {
            try
            {
                Admin model = new Admin();
                model.RequestID = id;
                model.Status = (model.Status = "Accepted");
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.AcceptRejectEPinRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "EPin Request Accepted Successfully";
                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("EPinRequestList", "Admin");
        }
        public ActionResult RejectedEPinRequest(string id)
        {
            try
            {
                Admin model = new Admin();
                model.RequestID = id;
                model.Status = (model.Status = "Rejected");
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.AcceptRejectEPinRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "EPin Request Rejected Successfully";
                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("EPinRequestList", "Admin");
        }
        public ActionResult ROIWalletForAdmin()
        {
            Admin model = new Admin();
            List<Admin> lst = new List<Admin>();
            DataSet ds = model.GetROIWalletDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.RoiWalletId = r["Pk_ROIWalletId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lstTps = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("ROIWalletForAdmin")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ROIWalletForAdmin(Admin model)
        {
            List<Admin> lst = new List<Admin>();
            model.LoginId = model.LoginId == "0" ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetROIWalletDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.RoiWalletId = r["Pk_ROIWalletId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lstTps = lst;
            }
            return View(model);

        }
        public ActionResult ROIIncomeReportsForAdmin()
        {
            Admin model = new Admin();
            List<Admin> lst = new List<Admin>();
            DataSet ds = model.GetROIIncomeReportsDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.ROIId = r["Pk_ROIId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.TopUpAmount = r["TopUpAmount"].ToString();
                    obj.Date = r["TopUpDate"].ToString();
                    lst.Add(obj);
                }
                model.lstROIIncome = lst;
            }
            return View(model);
        }
        public ActionResult ViewROIForAdmin(string Id)
        {
            Admin model = new Admin();
            List<Admin> lst = new List<Admin>();
            DataSet ds = model.GetROIDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.ROI = r["Pk_ROIId"].ToString();
                    obj.ROI = r["ROI"].ToString();
                    obj.Date = r["ROIDate"].ToString();
                    lst.Add(obj);
                }
                model.lstROI = lst;
            }
            return View(model);
        }
        public ActionResult PayoutWalletLedgerForAdmin(string loginid)
        {
            List<Admin> lst = new List<Admin>();
            Admin model = new Admin();
            model.LoginId = loginid;
            DataSet ds = model.PayoutWalletLedger();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    Admin obj = new Admin();
                    obj.PK_PayoutWalletId = r["PK_PayoutWalletId"].ToString();
                    obj.Fk_UserId = r["FK_UserId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.Balance = r["Balance"].ToString();
                    obj.TransactionBy = r["TransactionBy"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult PayoutWalletLedgerForAdmin(Admin model, string loginId)
        {
            List<Admin> lst = new List<Admin>();
            model.LoginId = loginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.PayoutWalletLedger();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.PK_PayoutWalletId = r["PK_PayoutWalletId"].ToString();
                    obj.Fk_UserId = r["FK_UserId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult LevelIncomeTr1ForAdmin()
        {
            List<Admin> lst = new List<Admin>();
            Admin model = new Admin();
            DataSet ds = model.LevelIncomeTr1();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lstlevelIncome = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("LevelIncomeTr1ForAdmin")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult LevelIncomeTr1ForAdmin(Admin model)
        {
            List<Admin> lst = new List<Admin>();
            model.LoginId = model.LoginId == "0" ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.LevelIncomeTr1();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lstlevelIncome = lst;
            }
            return View(model);
        }
        public ActionResult LevelIncomeTr2ForAdmin()
        {
            List<Admin> lst = new List<Admin>();
            Admin model = new Admin();
            DataSet ds = model.LevelIncomeTr2();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lstlevel = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("LevelIncomeTr2ForAdmin")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult LevelIncomeTr2ForAdmin(Admin model)
        {
            List<Admin> lst = new List<Admin>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.LevelIncomeTr2();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lstlevel = lst;
            }
            return View(model);
        }
        public ActionResult PayoutDetailForAdmin()
        {
            List<Admin> lst = new List<Admin>();
            Admin model = new Admin();
            DataSet ds = model.PayoutDetail();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.Fk_UserId = r["Fk_Userid"].ToString();
                    obj.LevelIncomeTR1 = r["LevelIncomeTR1"].ToString();
                    obj.LevelIncomeTR2 = r["LevelIncomeTR2"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.ClosingDate = r["ClosingDate"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.ProcessingFee = r["AdminFee"].ToString();
                    obj.TDSAmount = r["TDSAmount"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    lst.Add(obj);
                }
                model.lstPayout = lst;
                ViewBag.LevelIncomeTR1 = double.Parse(ds.Tables[0].Compute("sum(LevelIncomeTR1)", "").ToString()).ToString("n2");
                ViewBag.LevelIncomeTR2 = double.Parse(ds.Tables[0].Compute("sum(LevelIncomeTR2)", "").ToString()).ToString("n2");
                ViewBag.GrossAmount = double.Parse(ds.Tables[0].Compute("sum(GrossAmount)", "").ToString()).ToString("n2");
                ViewBag.AdminFee = double.Parse(ds.Tables[0].Compute("sum(AdminFee)", "").ToString()).ToString("n2");
                ViewBag.TDSAmount = double.Parse(ds.Tables[0].Compute("sum(TDSAmount)", "").ToString()).ToString("n2");
                ViewBag.NetAmount = double.Parse(ds.Tables[0].Compute("sum(NetAmount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("PayoutDetailForAdmin")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult PayoutDetailForAdmin(Admin model)
        {
            List<Admin> lst = new List<Admin>();
            model.LoginId = model.LoginId == "" ? null : model.LoginId;
            model.PayoutNo = model.PayoutNo == "" ? null : model.PayoutNo;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.PayoutDetail();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.Fk_UserId = r["Fk_Userid"].ToString();
                    obj.LevelIncomeTR1 = r["LevelIncomeTR1"].ToString();
                    obj.LevelIncomeTR2 = r["LevelIncomeTR2"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.ClosingDate = r["ClosingDate"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.ProcessingFee = r["AdminFee"].ToString();
                    obj.TDSAmount = r["TDSAmount"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    lst.Add(obj);
                }
                model.lstPayout = lst;
                ViewBag.LevelIncomeTR1 = double.Parse(ds.Tables[0].Compute("sum(LevelIncomeTR1)", "").ToString()).ToString("n2");
                ViewBag.LevelIncomeTR2 = double.Parse(ds.Tables[0].Compute("sum(LevelIncomeTR2)", "").ToString()).ToString("n2");
                ViewBag.GrossAmount = double.Parse(ds.Tables[0].Compute("sum(GrossAmount)", "").ToString()).ToString("n2");
                ViewBag.AdminFee = double.Parse(ds.Tables[0].Compute("sum(AdminFee)", "").ToString()).ToString("n2");
                ViewBag.TDSAmount = double.Parse(ds.Tables[0].Compute("sum(TDSAmount)", "").ToString()).ToString("n2");
                ViewBag.NetAmount = double.Parse(ds.Tables[0].Compute("sum(NetAmount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        public ActionResult DistributePayment()
        {
            List<Admin> lst = new List<Admin>();
            Admin model = new Admin();
            //DataSet ds = model.DistributePayment();
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds.Tables[0].Rows)
            //    {
            //        Admin obj = new Admin();
            //        obj.Pk_UserId = r["Pk_UserId"].ToString();
            //        obj.LoginId = r["LoginId"].ToString();
            //        obj.Name = r["FirstName"].ToString();
            //        obj.TPSLevelIncome = r["TPSLevelIncome"].ToString();
            //        obj.TPPLevelIncome = r["TPPLevelIncome"].ToString();
            //        obj.GrossAmount = r["GrossIncome"].ToString();
            //        obj.ProcessingFee = r["Processing"].ToString();
            //        obj.TDSAmount = r["TDS"].ToString();
            //        obj.NetAmount = r["NetIncome"].ToString();
            //        lst.Add(obj);
            //    }
            //    model.lstDistributePayment = lst;
            //    ViewBag.TPSLevelIncome = double.Parse(ds.Tables[0].Compute("sum(TPSLevelIncome)", "").ToString()).ToString("n2");
            //    ViewBag.TPPLevelIncome = double.Parse(ds.Tables[0].Compute("sum(TPPLevelIncome)", "").ToString()).ToString("n2");
            //    ViewBag.GrossIncome = double.Parse(ds.Tables[0].Compute("sum(GrossIncome)", "").ToString()).ToString("n2");
            //    ViewBag.Processing = double.Parse(ds.Tables[0].Compute("sum(Processing)", "").ToString()).ToString("n2");
            //    ViewBag.TDS = double.Parse(ds.Tables[0].Compute("sum(TDS)", "").ToString()).ToString("n2");
            //    ViewBag.NetIncome = double.Parse(ds.Tables[0].Compute("sum(NetIncome)", "").ToString()).ToString("n2");
            //}
            //model.LastClosingDate = ds.Tables[1].Rows[0]["ClosingDate"].ToString();
            //model.PayoutNo = ds.Tables[1].Rows[0]["PayoutNo"].ToString();
            return View(model);
        }
        [HttpPost]
        [ActionName("DistributePayment")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult DistributePayment(Admin model)
        {
            List<Admin> lst = new List<Admin>();
            //Admin model = new Admin();
            //model.ClosingDate = string.IsNullOrEmpty(model.ClosingDate) ? null : Common.ConvertToSystemDate(model.ClosingDate, "dd/MM/yyyy");
            DataSet ds = model.DistributePayment();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["FirstName"].ToString();
                    obj.TPSLevelIncome = r["TPSLevelIncome"].ToString();
                    obj.TPPLevelIncome = r["TPPLevelIncome"].ToString();
                    obj.GrossAmount = r["GrossIncome"].ToString();
                    obj.ProcessingFee = r["Processing"].ToString();
                    obj.TDSAmount = r["TDS"].ToString();
                    obj.NetAmount = r["NetIncome"].ToString();
                    lst.Add(obj);
                }
                model.lstDistributePayment = lst;
                ViewBag.TPSLevelIncome = double.Parse(ds.Tables[0].Compute("sum(TPSLevelIncome)", "").ToString()).ToString("n2");
                ViewBag.TPPLevelIncome = double.Parse(ds.Tables[0].Compute("sum(TPPLevelIncome)", "").ToString()).ToString("n2");
                ViewBag.GrossIncome = double.Parse(ds.Tables[0].Compute("sum(GrossIncome)", "").ToString()).ToString("n2");
                ViewBag.Processing = double.Parse(ds.Tables[0].Compute("sum(Processing)", "").ToString()).ToString("n2");
                ViewBag.TDS = double.Parse(ds.Tables[0].Compute("sum(TDS)", "").ToString()).ToString("n2");
                ViewBag.NetIncome = double.Parse(ds.Tables[0].Compute("sum(NetIncome)", "").ToString()).ToString("n2");
            }
            model.LastClosingDate = ds.Tables[1].Rows[0]["ClosingDate"].ToString();
            model.PayoutNo = ds.Tables[1].Rows[0]["PayoutNo"].ToString();
            return View(model);
        }
        [HttpPost]
        [ActionName("DistributePayment")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult DistributePaymentSave(Admin model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.SaveDistributePayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Distribute payment successfully";
                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("DistributePayment", "Admin");
        }
        public ActionResult DistributePaymentTPS()
        {
            Admin model = new Admin();
            List<Admin> lst = new List<Admin>();
            DataSet ds = model.DistributePaymentTPS();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["FirstName"].ToString();
                    obj.PPP = r["PPP"].ToString();
                    obj.GrossAmount = r["GrossIncome"].ToString();
                    obj.ProcessingFee = r["Processing"].ToString();
                    obj.TDSAmount = r["TDS"].ToString();
                    obj.NetAmount = r["NetIncome"].ToString();
                    lst.Add(obj);
                }
                model.lstDistributePaymentTPP = lst;
                ViewBag.PPP = double.Parse(ds.Tables[0].Compute("sum(PPP)", "").ToString()).ToString("n2");
                ViewBag.GrossIncome = double.Parse(ds.Tables[0].Compute("sum(GrossIncome)", "").ToString()).ToString("n2");
                ViewBag.Processing = double.Parse(ds.Tables[0].Compute("sum(Processing)", "").ToString()).ToString("n2");
                ViewBag.TDS = double.Parse(ds.Tables[0].Compute("sum(TDS)", "").ToString()).ToString("n2");
                ViewBag.NetIncome = double.Parse(ds.Tables[0].Compute("sum(NetIncome)", "").ToString()).ToString("n2");
            }
            model.LastClosingDate = ds.Tables[1].Rows[0]["ClosingDate"].ToString();
            model.PayoutNo = ds.Tables[1].Rows[0]["PayoutNo"].ToString();
            return View(model);
        }
        [HttpPost]
        [ActionName("DistributePaymentTPS")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult DistributePaymentTPS(Admin model)
        {
            //Admin model = new Admin();
            List<Admin> lst = new List<Admin>();
            //model.ClosingDate = string.IsNullOrEmpty(model.ClosingDate) ? null : Common.ConvertToSystemDate(model.ClosingDate, "dd/MM/yyyy");
            DataSet ds = model.DistributePaymentTPS();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["FirstName"].ToString();
                    obj.PPP = r["PPP"].ToString();
                    obj.GrossAmount = r["GrossIncome"].ToString();
                    obj.ProcessingFee = r["Processing"].ToString();
                    obj.TDSAmount = r["TDS"].ToString();
                    obj.NetAmount = r["NetIncome"].ToString();
                    lst.Add(obj);
                }
                model.lstDistributePaymentTPP = lst;
                ViewBag.PPP = double.Parse(ds.Tables[0].Compute("sum(PPP)", "").ToString()).ToString("n2");
                ViewBag.GrossIncome = double.Parse(ds.Tables[0].Compute("sum(GrossIncome)", "").ToString()).ToString("n2");
                ViewBag.Processing = double.Parse(ds.Tables[0].Compute("sum(Processing)", "").ToString()).ToString("n2");
                ViewBag.TDS = double.Parse(ds.Tables[0].Compute("sum(TDS)", "").ToString()).ToString("n2");
                ViewBag.NetIncome = double.Parse(ds.Tables[0].Compute("sum(NetIncome)", "").ToString()).ToString("n2");
            }
            model.LastClosingDate = ds.Tables[1].Rows[0]["ClosingDate"].ToString();
            model.PayoutNo = ds.Tables[1].Rows[0]["PayoutNo"].ToString();
            return View(model);
        }
        [HttpPost]
        public ActionResult DistributePaymentTPSSave(Admin model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveDistributePaymentTPS();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Distribute payment PPP successfully";
                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("DistributePaymentTPS", "Admin");
        }
        public ActionResult BusinessReports()
        {
            Admin model = new Admin();
            model.LoginId = model.LoginId == "0" ? null : model.LoginId;
            if (model.IsDownline == "on")
            {
                model.IsDownline = "1";
            }
            else
            {
                model.IsDownline = "0";
            }
            List<Admin> lst = new List<Admin>();
            DataSet ds = model.GetBusinessReports();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["FirstName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.BV = r["BV"].ToString();
                    obj.Date = r["Date"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.PackageType = r["PackageType"].ToString();
                    lst.Add(obj);
                }
                model.lstBReports = lst;

                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
                ViewBag.BV = double.Parse(ds.Tables[0].Compute("sum(BV)", "").ToString()).ToString("n2");
            }

            #region ddlPlotSize
            int count = 0;
            List<SelectListItem> ddlProductName = new List<SelectListItem>();
            DataSet dss = model.GetProductName();
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dss.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProductName.Add(new SelectListItem { Text = "-Select-", Value = "" });
                    }
                    ddlProductName.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlProductName = ddlProductName;
            #endregion
            return View(model);
        }
        [HttpPost]
        [ActionName("BusinessReports")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult BusinessReports(Admin model)
        {
            List<Admin> lst = new List<Admin>();
            model.LoginId = model.LoginId == "0" ? null : model.LoginId;
            if (model.IsDownline == "on")
            {
                model.IsDownline = "1";
            }
            else
            {
                model.IsDownline = "0";
            }
            model.PK_ProductID = model.PK_ProductID == "0" ? null : model.PK_ProductID;
            model.Level = model.Level == "0" ? null : model.Level;
            model.IsDownline = model.IsDownline == "0" ? null : model.IsDownline;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetBusinessReports();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["FirstName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.BV = r["BV"].ToString();
                    obj.Date = r["Date"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.PackageType = r["PackageType"].ToString();
                    lst.Add(obj);
                }
                model.lstBReports = lst;
                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
                ViewBag.BV = double.Parse(ds.Tables[0].Compute("sum(BV)", "").ToString()).ToString("n2");
            }

            #region ddlPlotSize
            int count = 0;
            List<SelectListItem> ddlProductName = new List<SelectListItem>();
            DataSet dss = model.GetProductName();
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dss.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProductName.Add(new SelectListItem { Text = "-Select-", Value = "" });
                    }
                    ddlProductName.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlProductName = ddlProductName;
            #endregion
            return View(model);
        }
        public ActionResult GetTPPLevelIncome(string LoginId, string ClosingDate)
        {
            Reports model = new Reports();
            List<Reports> lst = new List<Reports>();
            model.LoginId = LoginId;
            model.ClosingDate = ClosingDate;
            DataSet dspayout = model.GetTPPAmountById();
            if (dspayout != null && dspayout.Tables[0].Rows.Count > 0)
            {
                //if (dspayout.Tables[0].Rows[0]["MSG"].ToString() == "1")
                //{
                model.Result = "yes";
                if (dspayout != null && dspayout.Tables.Count > 0 && dspayout.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dspayout.Tables[0].Rows)
                    {
                        Reports obj = new Reports();
                        obj.LoginId = r["LoginId"].ToString();
                        obj.Name = r["Name"].ToString();
                        obj.Amount = r["Amount"].ToString();
                        obj.Level = r["Lvl"].ToString();
                        obj.CurrentDate = r["CurrentDate"].ToString();
                        obj.UsedFor = r["UsedFor"].ToString();
                        lst.Add(obj);
                    }
                    model.lsttopupreport = lst;
                    //}
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTPSLevelIncome(string LoginId, string ClosingDate)
        {
            Reports model = new Reports();
            List<Reports> lst = new List<Reports>();
            model.LoginId = LoginId;
            model.ClosingDate = ClosingDate;
            DataSet dspayout = model.GetTPSAmountById();
            if (dspayout != null && dspayout.Tables[0].Rows.Count > 0)
            {
                //if (dspayout.Tables[0].Rows[0]["MSG"].ToString() == "1")
                //{
                model.Result = "yes";
                if (dspayout != null && dspayout.Tables.Count > 0 && dspayout.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dspayout.Tables[0].Rows)
                    {
                        Reports obj = new Reports();
                        obj.LoginId = r["LoginId"].ToString();
                        obj.Name = r["Name"].ToString();
                        obj.Amount = r["Amount"].ToString();
                        obj.Level = r["Lvl"].ToString();
                        obj.CurrentDate = r["CurrentDate"].ToString();
                        obj.UsedFor = r["UsedFor"].ToString();
                        lst.Add(obj);
                    }
                    model.lsttopupreporttps = lst;
                    //}
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPayoutReportforAmount(string Fk_UserId, string PayoutNo)
        {
            Reports model = new Reports();
            List<Reports> lst = new List<Reports>();
            model.Fk_UserId = Fk_UserId;
            model.PayoutNo = PayoutNo;
            DataSet dspayout = model.GetPaidPayoutDetailsByAmount();
            if (dspayout != null && dspayout.Tables[0].Rows.Count > 0)
            {
                //if (dspayout.Tables[0].Rows[0]["MSG"].ToString() == "1")
                //{
                model.Result = "yes";
                if (dspayout != null && dspayout.Tables.Count > 0 && dspayout.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dspayout.Tables[0].Rows)
                    {
                        Reports obj = new Reports();
                        obj.PayoutNo = r["PayoutNo"].ToString();
                        obj.CurrentDate = r["CurrentDate"].ToString();
                        obj.LoginId = r["FromId"].ToString();
                        obj.Name = r["FromName"].ToString();
                        obj.Amount = r["Income"].ToString();
                        obj.Level = r["Lvl"].ToString();
                        obj.Fk_Paymentid = r["Paymenttype"].ToString();
                        lst.Add(obj);
                    }
                    model.lsttopupreport = lst;
                    //}
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PayoutRequestList()
        {
            Admin model = new Admin();
            List<Admin> lst = new List<Admin>();
            DataSet ds = model.GetPayoutRequest();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.PK_RequestID = r["Pk_RequestId"].ToString();
                    obj.Amount = r["AMount"].ToString();
                    obj.Date = r["RequestedDate"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.MemberAccNo = r["MemberAccNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.TransactionNo = r["TransactionNo"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("PayoutRequestList")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult PayoutRequestListBy(Admin model)
        {
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Admin> lst = new List<Admin>();
            DataSet ds = model.GetPayoutRequest();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.PK_RequestID = r["Pk_RequestId"].ToString();
                    obj.Amount = r["AMount"].ToString();
                    obj.Date = r["RequestedDate"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.MemberAccNo = r["MemberAccNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.TransactionNo = r["TransactionNo"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }

        public ActionResult ApprovePayout(string TransactionNo, string TransactionDate, string requestid)
        {
            Admin model = new Admin();
            try
            {
                model.PK_RequestID = requestid;
                model.TransactionDate = string.IsNullOrEmpty(TransactionDate) ? null : Common.ConvertToSystemDate(TransactionDate, "dd/MM/yyyy");
                model.TransactionNo = TransactionNo;
                model.Status = (model.Status = "Approved");
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ApprovePayoutRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        //TempData["msg"] = "Your request has been approved Successfully !!";
                        // Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        // Session["Email"] = ds.Tables[0].Rows[0]["Email"].ToString();
                        //model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        // Session["TransactionNo"] = ds.Tables[0].Rows[0]["TransactionNo"].ToString();
                        // Session["TransactionDate"] = ds.Tables[0].Rows[0]["TransactionDate"].ToString();

                        //if (model.Email != "" && model.Email != null)
                        //{
                        //    try
                        //    {
                        //        BLMail.SendApprovePayoutMail(Session["Name"].ToString(), Session["TransactionNo"].ToString(), Session["TransactionDate"].ToString(), "Your request has been approved Successfully", Session["Email"].ToString());
                        //    }
                        //    catch (Exception ex)
                        //    {

                        //    }
                        //}

                        model.Result = "1";
                    }
                    else
                    {
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("PayoutRequestList", "Admin");
        }
        //public ActionResult ApprovePayout(string id)
        //{
        //    try
        //    {
        //        Admin model = new Admin();
        //        model.PK_RequestID = id;
        //        model.Status = (model.Status = "Approved");
        //        model.UpdatedBy = Session["Pk_AdminId"].ToString();
        //        DataSet ds = model.ApprovePayoutRequest();
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //            {
        //                TempData["msg"] = "Transfer to account approved Successfully";
        //            }
        //            else
        //            {
        //                TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["msg"] = ex.Message;
        //    }
        //    return RedirectToAction("PayoutRequestList", "Admin");
        //}
        public ActionResult DeclinePayout(string id)
        {
            try
            {
                Admin model = new Admin();
                model.PK_RequestID = id;
                model.Status = (model.Status = "Declined");
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ApprovePayoutRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Transfer to account Declined Successfully";
                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("PayoutRequestList", "Admin");
        }
        [HttpPost]
        public ActionResult GetNameDetails(string LoginId)
        {
            Admin model = new Admin();
            model.LoginId = LoginId;
            DataSet ds = model.GetNameDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Result = "yes";
                model.Fk_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
            }
            else
            {
                model.Result = "no";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TransferWallet()
        {
            return View();
        }
        [HttpPost]
        [ActionName("TransferWallet")]
        [OnAction(ButtonName = "Transfer")]
        public ActionResult TransferWallet(Admin model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveTransferWallet();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["TransferWallet"] = "Transferred  successfully";
                    }
                    else
                    {
                        TempData["TransferWallet"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["TransferWallet"] = ex.Message;
            }
            return RedirectToAction("TransferWallet", "Admin");
        }
        public ActionResult AdvanceDeduction()
        {
            return View();
        }
        [HttpPost]
        [ActionName("AdvanceDeduction")]
        [OnAction(ButtonName = "Advance")]
        public ActionResult AdvanceDeduction(Admin model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveDeduction();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["deduction"] = "Advance/Deduction done successfully";
                    }
                    else
                    {
                        TempData["deduction"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["deduction"] = ex.Message;
            }
            return RedirectToAction("AdvanceDeduction", "Admin");
        }
        public ActionResult CommissionReport()
        {
            List<Admin> lst = new List<Admin>();
            Admin model = new Admin();
            DataSet ds = model.GetCommissionReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    //obj.PK_CommissionId = r["PK_CommissionId"].ToString();
                    obj.Name = r["userDetails"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
                ViewBag.TotalAmount = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult CommissionReport(Admin model)
        {
            List<Admin> lst = new List<Admin>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.CommissionReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    //obj.PK_CommissionId = r["PK_CommissionId"].ToString();
                    obj.Name = r["userDetails"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
                ViewBag.TotalAmount = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        public ActionResult TopUpWalletLedgerForAdmin(string loginid)
        {
            List<Admin> lst = new List<Admin>();
            Admin model = new Admin();
            model.LoginId = loginid;
            DataSet ds = model.TopUpWalletLedger();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    Admin obj = new Admin();
                    obj.WalletId = r["Pk_EwalletId"].ToString();
                    obj.Fk_UserId = r["FK_UserId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.Balance = r["Balance"].ToString();
                    obj.TransactionBy = r["TransactionBy"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult TopUpWalletLedgerForAdmin(Admin model, string loginId)
        {
            List<Admin> lst = new List<Admin>();
            model.LoginId = loginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.TopUpWalletLedger();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.WalletId = r["Pk_EwalletId"].ToString();
                    obj.Fk_UserId = r["FK_UserId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult PushupPayment()
        {
            #region DonationPlan
            List<SelectListItem> ddldonationPlan = Common.BindDonationPlan();
            ViewBag.ddldonationplanType = ddldonationPlan;
            List<SelectListItem> ddlLevel = new List<SelectListItem>();
            ddlLevel.Add(new SelectListItem { Text = "Select Level", Value = "0" });
            ViewBag.ddlLevel = ddlLevel;
            #endregion
            return View();

        }
        public ActionResult getLevel(string DonationId, string loginId)
        {
            List<SelectListItem> lstDonation = new List<SelectListItem>();
            AdminReports model = new AdminReports();
            model.DonationPlanTypeId = DonationId;
            model.LoginId = loginId;
            DataSet ds = model.GetDonationPlanList();

            #region ddlLevelDonation
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Result = "Yes";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstDonation.Add(new SelectListItem { Text = dr["Donation"].ToString(), Value = dr["DonationId"].ToString() });
                }

            }

            model.lstLevelDonation = lstDonation;
            #endregion

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ActionName("PushupPayment")]
        [OnAction(ButtonName = "btn_Save")]
        public ActionResult SavePushPlanDetails(AdminReports model)
        {
            try
            {
                #region DonationPlan
                List<SelectListItem> ddldonationPlan = Common.BindDonationPlan();
                ViewBag.ddldonationplanType = ddldonationPlan;
                List<SelectListItem> ddlLevel = new List<SelectListItem>();
                ddlLevel.Add(new SelectListItem { Text = "Select Level", Value = "0" });
                ViewBag.ddlLevel = ddlLevel;
                #endregion
                //model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/MM/yyyy");
                DataSet ds = model.SavePushupPayment();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["msg"] = "Payment Saved Successfully !!";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("PushupPayment", "Admin");
        }
        public ActionResult CheckLoginDetails(string Loginid)
        {
            Admin model = new Admin();
            model.LoginId = Loginid;
            DataSet ds = model.CheckActivateLogin();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    model.Result = "1";
                    model.LoginId = ds.Tables[0].Rows[0]["Loginid"].ToString();
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteDonation(Admin model, string Id)
        {
            try
            {
                model.DonationId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteDonation();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        model.Result = "1";
                        TempData["Donation"] = "This record deleted successfully !!";
                    }
                    else
                    {
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}