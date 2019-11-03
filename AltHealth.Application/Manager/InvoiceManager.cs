using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltHealth.Application.MappingProfiles;
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
        private readonly IMapper _mapper;

        public InvoiceManager()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            _mapper = mappingConfig.CreateMapper();
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
                        TotalAmount = 0
                    };
                    _dbInvoice.Add(invoice);

                }
                
            }
        }
    }
}
