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
    public class BookingController : Controller
    {
        private readonly BookingManager _manager = new BookingManager();
        private readonly InvoiceManager _managerInvoice = new InvoiceManager();
        // GET: Booking
        public ActionResult Index(string status)
        {
            var model = new BookingViewModel()
            {
                Bookings = new List<BookingDto>(),
                Status = status
            };
            model.Bookings = _manager.GetAll();
            return View(model);
        }

        public ActionResult Add()
        {
            var patients = _manager.GetAllPatients();
            var practisioners = _manager.GetAllPractisioner();
            var model = new BookingViewModel()
            {
                Patients = patients.Select(x => new SelectListItem()
                {
                    Text = x.FirstName + " " + x.LastName,
                    Value = x.Id.ToString()
                }).ToList(),
                Practitioners = practisioners.Select(x => new SelectListItem()
                {
                    Text = x.FirstName + " " + x.LastName,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(BookingViewModel vm)
        {
            try
            {
                _manager.Add(vm.Booking);
                return RedirectToAction("Index", new { status = "success" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", new { status = "fail" });
            }

        }

        public ActionResult AddInvoice(int id)
        {
            try
            {
                _managerInvoice.CreateInvoice(id);
                return RedirectToAction("Manage", new { id , status = "invoiceSuccess" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Manage", new { id , status = "invoiceFail" });
            }

        }

        public ActionResult Manage(int id, string status)
        {
            var patients = _manager.GetAllPatients();
            var practisioners = _manager.GetAllPractisioner();
            var model = new BookingViewModel()
            {
                Booking = _manager.FindBy(id),
                Patients = patients.Select(x => new SelectListItem()
                {
                    Text = x.FirstName + " " + x.LastName,
                    Value = x.Id.ToString()
                }).ToList(),
                Practitioners = practisioners.Select(x => new SelectListItem()
                {
                    Text = x.FirstName + " " + x.LastName,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Manage(BookingViewModel vm)
        {
            try
            {
                _manager.Edit(vm.Booking);
                return RedirectToAction("Manage", new { id = vm.Booking.Id, status = "success" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Manage", new { id = vm.Booking.Id, status = "fail" });
            }

        }
    }
}