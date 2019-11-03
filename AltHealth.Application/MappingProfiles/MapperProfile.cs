using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltHealth.Application.Models;
using AltHealth.Data;
using AutoMapper;

namespace AltHealth.Application.MappingProfiles
{
    
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Patient, PatientDto>();
                CreateMap<PatientDto, Patient>();

                CreateMap<Booking, BookingDto>().ForAllOtherMembers(x => x.Ignore());
                CreateMap<BookingDto, Booking>().ForAllOtherMembers(x => x.Ignore()); 

                CreateMap<Invoice, InvoiceDto>();
                CreateMap<InvoiceDto, Invoice>();

                CreateMap<Reference, ReferenceDto>();
                CreateMap<ReferenceDto, Reference>();

            CreateMap<Practisioner, PractitionerDto>();
                CreateMap<PractitionerDto, Practisioner>();
            }
        }
    
}
