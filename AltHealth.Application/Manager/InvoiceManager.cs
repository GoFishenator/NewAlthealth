using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltHealth.Application.MappingProfiles;
using AltHealth.Application.Models;
using AltHealth.Data;
using AltHealth.Data.Repository;
using AutoMapper;

namespace AltHealth.Application.Manager
{
    public class InvoiceManager
    {
        private readonly DataRepository<Booking> _dbBooking = new DataRepository<Booking>();
        private readonly DataRepository<Invoice> _dbInvoice = new DataRepository<Invoice>();
        private readonly DataRepository<InvoiceBooking> _dbInvoiceBooking = new DataRepository<InvoiceBooking>();
        private readonly DataRepository<InvoiceItem> _dbInvoiceItem = new DataRepository<InvoiceItem>();
        private readonly DataRepository<Patient> _dbPatient = new DataRepository<Patient>();

        public InvoiceManager()
        {
            
        }

        public void CreateInvoice(int bookingId)
        {
            var booking = _dbBooking.GetAll().FirstOrDefault(x => x.Id == bookingId);
            if (booking != null)
            {
                var patient = _dbPatient.GetAll().FirstOrDefault(x => x.Id == booking.PatientId);
                if (patient != null)
                {
                    var random = new Random();
                    var invoice = new Invoice()
                    {
                        InvoiceNumber = patient.Id + "0" + random.Next(10000) + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year,
                        TotalAmount = 0m
                    };
                    _dbInvoice.Add(invoice);
                    _dbInvoice.Save();
                    var newInvoice = _dbInvoice.GetAll().OrderByDescending(x => x.Id).First();
                    var bookingInvoice = new InvoiceBooking()
                    {
                        BookingId = bookingId,
                        InvoiceId = newInvoice.Id,
                        InvoiceDate = DateTime.Now,
                        IsPaid = false,
                        IsSent = false,
                        PayedUpDate = null,
                        OutStandingAmount = 0m
                    };
                    _dbInvoiceBooking.Add(bookingInvoice);
                    _dbInvoiceBooking.Save();
                }
                
            }
        }

        public List<InvoiceDisplayDto> GetAllInvoices()
        {
            var invoiceBookings = _dbInvoiceBooking.GetAll();

            var result = new List<InvoiceDisplayDto>();
            foreach (var item in invoiceBookings)
            {
                var booking = _dbBooking.GetAll().First(x => x.Id == item.BookingId);
                var invoice = _dbInvoice.GetAll().First(x => x.Id == item.InvoiceId);
                var invoiceDisplay = new InvoiceDisplayDto()
                {
                    BookingId = item.BookingId,
                    InvoiceId = item.InvoiceId,
                    InvoiceDate = item.InvoiceDate,
                    IsPaid = item.IsPaid,
                    IsSent = item.IsSent,
                    PayedUpDate = item.PayedUpDate,
                    OutStandingAmount = item.OutStandingAmount,
                    BookingNumber = booking.BookingNumber,
                    InvoiceNumber = invoice.InvoiceNumber,
                    TotalAmount = invoice.TotalAmount,
                    InvoiceItems = new List<InvoiceItem>()
                };
                invoiceDisplay.InvoiceItems = _dbInvoiceItem.GetAll().Where(x => x.InvoiceId == item.InvoiceId).ToList();
                result.Add(invoiceDisplay);
            }


            return result;
        }

        public InvoiceDisplayDto GetInvoice(int id)
        {
            var item = _dbInvoiceBooking.GetAll().First(x => x.Id == id);
            var booking = _dbBooking.GetAll().First(x => x.Id == item.BookingId);
            var invoice = _dbInvoice.GetAll().First(x => x.Id == item.InvoiceId);
            var invoiceDisplay = new InvoiceDisplayDto()
            {
                BookingId = item.BookingId,
                InvoiceId = item.InvoiceId,
                InvoiceDate = item.InvoiceDate,
                IsPaid = item.IsPaid,
                IsSent = item.IsSent,
                PayedUpDate = item.PayedUpDate,
                OutStandingAmount = item.OutStandingAmount,
                BookingNumber = booking.BookingNumber,
                InvoiceNumber = invoice.InvoiceNumber,
                TotalAmount = invoice.TotalAmount,
                InvoiceItems = new List<InvoiceItem>()
            };
            invoiceDisplay.InvoiceItems = _dbInvoiceItem.GetAll().Where(x => x.InvoiceId == item.InvoiceId).ToList();
            return invoiceDisplay;
        }

        public void AddInvoiceItem(int invoiceId, InvoiceItemDto item)
        {
            var invoiceItem = new InvoiceItem()
            {
                Name = item.Name,
                Description = item.Description,
                Amount = item.Amount,
                Quantity = item.Quantity,
                InvoiceId = invoiceId
            };
            _dbInvoiceItem.Add(invoiceItem);
            _dbInvoiceItem.Save();
        }

        public void UpdateInvoiceItem(InvoiceItemDto item)
        {
            var invoiceItem = _dbInvoiceItem.GetAll().First(x => x.Id == item.Id);
            invoiceItem.Id = item.Id;
            invoiceItem.Name = item.Name;
            invoiceItem.Description = item.Description;
            invoiceItem.Amount = item.Amount;
            invoiceItem.Quantity = item.Quantity;
            invoiceItem.InvoiceId = item.InvoiceId;
            _dbInvoiceItem.Edit(invoiceItem);
        }

        public void RemoveInvoiceItem(int id)
        {
            var invoiceItem = _dbInvoiceItem.GetAll().First(x => x.Id == id);
            _dbInvoiceItem.Delete(invoiceItem);
        }
    }
}
