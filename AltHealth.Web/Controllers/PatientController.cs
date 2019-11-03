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
    [Authorize]
    public class PatientController : Controller
    {
        PatientManager _manager = new PatientManager();
        // GET: Patients
        public ActionResult Index(string status)
        {
            var model = new PatientViewModel()
            {
                Patients = new List<PatientDto>(),
                Status = status
            };
            model.Patients = _manager.GetAll();
            return View(model);
        }
        public ActionResult Add()
        {
            var references = _manager.GetAllReferences();
            var model = new PatientViewModel()
            {
                References = references.Select(x => new SelectListItem()
                {
                    Text = x.Description,
                    Value = x.Description
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(PatientViewModel vm)
        {
            try
            {
                _manager.Add(vm.Patient);
                return RedirectToAction("Index", new { id = vm.Patient.Id, status = "success" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", new { id = vm.Patient.Id, status = "fail" });
            }

        }

        public ActionResult Manage(int id, string status)
        {
            var references = _manager.GetAllReferences();
            var model = new PatientViewModel()
            {
                Patient = _manager.FindBy(id),
                References = references.Select(x => new SelectListItem()
                {
                    Text = x.Description,
                    Value = x.Description
                }).ToList(),
                Status = status
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Manage(PatientViewModel vm)
        {
            try
            {
                _manager.Edit(vm.Patient);
                return RedirectToAction("Manage", new { id = vm.Patient.Id, status = "success" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Manage", new { id = vm.Patient.Id, status = "fail" });
            }
            
        }
    }
}