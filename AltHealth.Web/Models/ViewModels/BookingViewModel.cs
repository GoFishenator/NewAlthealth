using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltHealth.Application.Models;

namespace AltHealth.Web.Models.ViewModels
{
    public class BookingViewModel
    {
        public BookingDto Booking { get; set; }
        public List<BookingDto> Bookings { get; set; }
        public List<SelectListItem> Patients { get; set; }
        public List<SelectListItem> Practitioners { get; set; }
        public string Status { get; set; }
    }
}