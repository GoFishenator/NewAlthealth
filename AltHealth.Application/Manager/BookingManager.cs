using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltHealth.Application.MappingProfiles;
using AltHealth.Application.Models;
using AltHealth.Application.Respository;
using AltHealth.Data;
using AltHealth.Data.Repository;
using AutoMapper;

namespace AltHealth.Application.Manager
{
    public class BookingManager : IManager<BookingDto>
    {
        private readonly DataRepository<Booking> _db = new DataRepository<Booking>();
        private readonly DataRepository<Patient> _dbPatient = new DataRepository<Patient>();
        private readonly DataRepository<Practisioner> _dbPractisioner = new DataRepository<Practisioner>();
        private readonly IMapper _mapper;
        public BookingManager()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            _mapper = mappingConfig.CreateMapper();
        }
        public List<BookingDto> GetAll()
        {
            var query = _db.GetAll().ToList();
            
            var bookings = new List<BookingDto>();
            foreach (var item in query)
            {
                var patient = _dbPatient.GetAll().First(x => x.Id == item.PatientId);
                var practitioner = _dbPractisioner.GetAll().First(x => x.Id == item.PractisionerId);
                var booking = new BookingDto()
                {
                    Id = item.Id,
                    PatientId = item.PatientId,
                    PractisionerId = item.PractisionerId,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    BookingNumber = item.BookingNumber,
                    PractitionerName = practitioner.FirstName + " " + practitioner.LastName,
                    PatientName = patient.FirstName + " " + patient.LastName
                };
                bookings.Add(booking);
            }
            return bookings;
        }
        public List<PractitionerDto> GetAllPractisioner()
        {
            var query = _dbPractisioner.GetAll().ToList();
            var practitioners = _mapper.Map<List<Practisioner>, List<PractitionerDto>>(query);
            return practitioners;
        }

        public List<PatientDto> GetAllPatients()
        {
            var query = _dbPatient.GetAll().ToList();
            var patients = _mapper.Map<List<Patient>, List<PatientDto>>(query);
            return patients;
        }

        public BookingDto FindBy(int id)
        {
            var item = _db.GetAll().FirstOrDefault(x => x.Id == id);
            var booking = new BookingDto()
            {
                Id = item.Id = id,
                PatientId = item.PatientId,
                PractisionerId = item.PractisionerId,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                BookingNumber = item.BookingNumber
            };
            return booking;
        }

        public void Add(BookingDto entity)
        {
            var booking = new Booking()
            {
                PatientId = entity.PatientId,
                PractisionerId = entity.PractisionerId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                BookingNumber = entity.BookingNumber
            };

            _db.Add(booking);
            _db.Save();
        }

        public void Delete(BookingDto entity)
        {
            var booking = _mapper.Map<Booking>(entity);
            _db.Edit(booking);
            _db.Save();
        }

        public void Edit(BookingDto entity)
        {
            var booking = new Booking()
            {
                PatientId = entity.PatientId,
                PractisionerId = entity.PractisionerId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                BookingNumber = entity.BookingNumber
            };
            _db.Edit(booking);
            _db.Save();
        }
    }
}
