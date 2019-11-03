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
    public class PatientManager : IManager<PatientDto>
    {
        private readonly DataRepository<Patient> _db = new DataRepository<Patient>();
        private readonly DataRepository<Reference> _referenceDb = new DataRepository<Reference>();
        private readonly IMapper _mapper;
        public PatientManager()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            _mapper = mappingConfig.CreateMapper();
        }
        public List<PatientDto> GetAll()
        {
            var query = _db.GetAll().ToList();
            var patients = _mapper.Map<List<Patient>, List<PatientDto>>(query);
            return patients;
        }

        public List<ReferenceDto> GetAllReferences()
        {
            var query = _referenceDb.GetAll().ToList();
            var references = _mapper.Map<List<Reference>, List<ReferenceDto>>(query);
            return references;
        }

        public PatientDto FindBy(int id)
        {
            var patient = _db.GetAll().FirstOrDefault(x => x.Id == id);
            var result = _mapper.Map<PatientDto>(patient);
            return result;
        }

        public void Add(PatientDto entity)
        {
            var patient = _mapper.Map<Patient>(entity);
            _db.Add(patient);
            _db.Save();
        }

        public void Delete(PatientDto entity)
        {
            var patient = _mapper.Map<Patient>(entity);
            _db.Edit(patient);
            _db.Save();
        }

        public void Edit(PatientDto entity)
        {
            var patient = _mapper.Map<Patient>(entity);
            _db.Edit(patient);
            _db.Save();
        }
    }
}
