using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltHealth.Application.Models;

namespace AltHealth.Web.Models.ViewModels
{
    public class InvoiceViewModel
    {
        public InvoiceDisplayDto Invoice { get; set; }
        public List<InvoiceDisplayDto> Invoices { get; set; }
        public List<SelectListItem> Bookings { get; set; }
    }
}