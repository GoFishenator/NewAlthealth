//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AltHealth.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class PatientPayment
    {
        public int Id { get; set; }
        public int InvoiceBookingId { get; set; }
        public int PatientId { get; set; }
        public decimal AmountPaid { get; set; }
    
        public virtual InvoiceBooking InvoiceBooking { get; set; }
        public virtual Patient Patient { get; set; }
    }
}