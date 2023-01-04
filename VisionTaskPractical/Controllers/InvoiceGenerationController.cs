using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisionTaskPractical.Models;

namespace VisionTaskPractical.Controllers
{
    public class InvoiceGenerationController : Controller
    {
        // GET: InvoiceGeneration
        public ActionResult Index()
        {
            var objInvoiceGenerationModel = new InvoiceGenerationModel();
            var objDataAccesLayer = new DataAccesLayer();
            List<InvoiceGenerationDetailModel> invoiceGenerationDetailModels = new List<InvoiceGenerationDetailModel>();
            var objInvoiceGenerationDetailModel = new InvoiceGenerationDetailModel();
            objInvoiceGenerationDetailModel.ProductId = 0;
            objInvoiceGenerationDetailModel.Qty = 0;
            objInvoiceGenerationDetailModel.Rate = 0;
            objInvoiceGenerationDetailModel.Amount = 0;
            invoiceGenerationDetailModels.Add(objInvoiceGenerationDetailModel);
            objInvoiceGenerationModel.lstInvoiceGenerationDetailModel = invoiceGenerationDetailModels;

            ViewBag.lstProduct = objDataAccesLayer.GetProductList();
            ViewBag.StateId = objDataAccesLayer.GetStateList();
            objInvoiceGenerationModel.lstinvoiceGenerationInvoiceLists = objDataAccesLayer.GetInvoiceList();
            return View(objInvoiceGenerationModel);
        }
        [HttpPost]
        public ActionResult AddNewRow(int id)
        {
            InvoiceGenerationDetailModel invoiceGenerationDetailModels = new InvoiceGenerationDetailModel();
            try
            {
                var objDataAccesLayer = new DataAccesLayer();
                ViewBag.lstProduct = objDataAccesLayer.GetProductList();
                ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("lstInvoiceGenerationDetailModel[{0}]", id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView("InvoiceGenerationPartialView", invoiceGenerationDetailModels);
        }
        [HttpPost]
        public JsonResult GetCiyList(int StateId)
        {
            var objDataAccesLayer = new DataAccesLayer();
            var objDropDownList = new List<DropDownList>();
            var List = objDataAccesLayer.GetCityList(StateId);
            foreach (var item in List)
            {
                var objDropDown = new DropDownList();
                objDropDown.ID = Convert.ToInt32(item.Value);
                objDropDown.Name = item.Text;
                objDropDownList.Add(objDropDown);
            }
            return Json(objDropDownList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SubmitData(InvoiceGenerationModel objInvoiceGenerationModel)
        {
            var objDataAccesLayer = new DataAccesLayer();
            var result = objDataAccesLayer.SaveData(objInvoiceGenerationModel);
            return RedirectToAction("Index");
        }
        public ActionResult GetEditBillDetails(int Invoiceno)
        {
            var obj = new InvoiceGenerationModel();
            var objDataAccesLayer = new DataAccesLayer();
            ViewBag.lstProduct = objDataAccesLayer.GetProductList();
            ViewBag.StateId = objDataAccesLayer.GetStateList();
            obj = objDataAccesLayer.GetInvoiceHeaderlist(Invoiceno);
            obj.lstInvoiceGenerationDetailModel = objDataAccesLayer.GetInvoiceDetailList(Invoiceno);
            obj.lstinvoiceGenerationInvoiceLists = objDataAccesLayer.GetInvoiceList();
            return View("Index", obj);
        }
    }


}