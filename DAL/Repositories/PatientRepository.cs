using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PatientRepository
    {
        private readonly Prn212Context _context;
        public PatientRepository()
        {
            _context = new Prn212Context();
        }

        public Patient GetPatientProfile(string email, string password)
        {
            return _context.Patients.FirstOrDefault(x => x.Email == email && x.Password == password);

        }

        public bool CreatePatient(Patient patient)
        {
            bool isSuccess = false;
            Patient existPatient = GetPatientProfile(patient.Email, patient.Password);
            if(existPatient == null)
            {
            int maxId = _context.Patients.Any()
                   ? _context.Patients.Max(x => x.PatientId)
                   : 1;
            patient.PatientId = maxId + 1;
                isSuccess = true;
                _context.Patients.Add(patient);
            _context.SaveChanges();
            }
            return isSuccess;
        }

        public bool UpdatePatient(Patient patient)
        {
            bool isSuccess = false;
            var existedPatient = GetPatientProfile(patient.Email, patient.Password);
            if(existedPatient != null)
            {
                var tracker = _context.Attach(existedPatient);
                tracker.State = EntityState.Modified;
                _context.SaveChanges();
                _context.ChangeTracker.Clear();
                isSuccess = true;

            }

            return isSuccess;

        }

     

    }
}
