using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltHealth.Application.Manager;
using AltHealth.Application.Models;
using AltHealth.Web.Models.ViewModels;

namespace AltHealth.Web.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly InvoiceManager _manager = new InvoiceManager();
        private readonly BookingManager _bookingManager = new BookingManager();
        // GET: Invoice
        public ActionResult Index()
        {
            var bookings = _bookingManager.GetAll().Where(x => x.InvoiceBookings.Count == 0);
            var model = new InvoiceViewModel()
            {
                Invoices = new List<InvoiceDisplayDto>(),
                Bookings = bookings.Select(x => new SelectListItem()
                {
                    Text = x.BookingNumber,
                    Value = x.Id.ToString()
                }).ToList()
            };
            model.Invoices = _manager.GetAllInvoices();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(InvoiceViewModel vm)
        {
            _manager.CreateInvoice(vm.Invoice.BookingId);
            return RedirectToAction("Index");
        }
    }
}