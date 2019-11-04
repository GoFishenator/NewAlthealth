using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltHealth.Data;

namespace AltHealth.Application.Models
{
    public class InvoiceDisplayDto: InvoiceBookingDto
    {
        public string BookingNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
