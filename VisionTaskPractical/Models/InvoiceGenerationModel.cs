using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace VisionTaskPractical.Models
{
    public class InvoiceGenerationModel
    {
        public InvoiceGenerationModel()
        {
            this.lstInvoiceGenerationDetailModel = new List<InvoiceGenerationDetailModel>();
            this.lstinvoiceGenerationInvoiceLists = new List<InvoiceGenerationInvoiceList>();
        }
        public int InvoiceNo { get; set; }
        public string BillingAddress { get; set; }
        public int BillingCityId { get; set; }
        public int BillingStateId { get; set; }
        public string BillingPhoneNo { get; set; }
        public string ShippingAddress { get; set; }
        public int ShippingCityId { get; set; }
        public int ShippingStateId { get; set; }
        public string ShippingPhoneNo { get; set; }
        public decimal TotalAmount { get; set; }
        public List<InvoiceGenerationDetailModel> lstInvoiceGenerationDetailModel { get; set; }
        public List<InvoiceGenerationInvoiceList> lstinvoiceGenerationInvoiceLists { get; set; }
    }
    public class InvoiceGenerationDetailModel
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
    public class DropDownList
    {
        public int ID { get; set; }
        public string Name { get; set; }

    }
    public class InvoiceGenerationInvoiceList
    {
        public int InvoiceNo { get; set; }
    }
}