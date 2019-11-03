using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltHealth.Application.Models;

namespace AltHealth.Web.Models.ViewModels
{
    public class PatientViewModel
    {
        public PatientDto Patient { get; set; }
        public List<PatientDto> Patients { get; set; }
        public string Status { get; set; }
        public List<SelectListItem> References { get; set; }
    }
}