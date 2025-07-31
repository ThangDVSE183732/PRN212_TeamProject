using DAL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PatientService
    {
        private PatientRepository _repo;
        public PatientService()
        {
            _repo = new PatientRepository();
        }

        public Patient GetPatientProfile(string email, string password)
        {
            return _repo.GetPatientProfile(email, password);

        }

        public bool UpdatePatient(Patient patient)
        {
            return _repo.UpdatePatient(patient);
        }

        public bool CreatePatient(Patient patient)
        {
            return _repo.CreatePatient(patient);
        }
    }
}
