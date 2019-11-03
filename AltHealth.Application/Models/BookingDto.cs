using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltHealth.Data;

namespace AltHealth.Application.Models
{
    public class BookingDto : Booking
    {
        public string PractitionerName { get; set; }
        public string PatientName { get; set; }
    }
}
