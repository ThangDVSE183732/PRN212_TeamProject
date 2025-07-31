using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class DoctorRepository
    {
        private  Prn212Context _context;

        public DoctorRepository()
        {
            _context = new Prn212Context();
        }

        public bool ExistsByEmail(string email)
        {
            return _context.Doctors.Any(d => d.Email == email);
        }

        public bool ExistsByPhone(string phone)
        {
            return _context.Doctors.Any(d => d.Phone == phone);
        }

        public List<Doctor> GetAllDoctors()
        {
            return _context.Doctors.AsNoTracking().ToList();

        }

        public Doctor GetDoctorById(int doctorId)
        {
            return _context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
        }

        public void CreateDoctor(Doctor doctor)
        {
            int maxId = _context.Doctors.Any()
                   ? _context.Doctors.Max(x => x.DoctorId)
                   : 1;
            doctor.DoctorId = maxId + 1;
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public void DeleteDoctor(Doctor doctor)
        {
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
        }

        public void UpdateDoctorNoReturn(Doctor doctor)
        {
            Doctor existingDoctor = _context.Doctors.Find(doctor.DoctorId); // Tìm đúng theo PK
            if (existingDoctor != null)
            {
                // Chỉ gán giá trị từng field, không tạo mới instance
                existingDoctor.DoctorName = doctor.DoctorName;
                existingDoctor.Specialization = doctor.Specialization;
                existingDoctor.Phone = doctor.Phone;
                _context.SaveChanges();
            }
        }

        public bool UpdateDoctor(Doctor doctor)
        {
            var existingDoctor = _context.Doctors.FirstOrDefault(d => d.DoctorId == doctor.DoctorId);
            if (existingDoctor == null) return false;

            existingDoctor.DoctorName = doctor.DoctorName;
            existingDoctor.Specialization = doctor.Specialization;
            existingDoctor.Phone = doctor.Phone;
            //existingDoctor.LicenseNumber = doctor.LicenseNumber;
            //existingDoctor.Experience = doctor.Experience;

            return _context.SaveChanges() > 0;
        }

        public bool BanDoctor(int doctorId)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
            if (doctor == null) return false;

            doctor.Status = "BAN";
            return _context.SaveChanges() > 0;
        }

        public bool UnbanDoctor(int doctorId)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
            if (doctor == null) return false;

            doctor.Status = "ACTIVE";
            return _context.SaveChanges() > 0;
        }

        public int GetNextDoctorId()
        {
            return (_context.Doctors.Max(d => (int?)d.DoctorId) ?? 0) + 1;
        }

        public bool PhoneExists(string phone)
        {
            return _context.Doctors.Any(d => d.Phone == phone);
        }

        //public bool LicenseNumberExists(string licenseNumber)
        //{
        //    return _context.Doctors.Any(d => d.LicenseNumber == licenseNumber);
        //}

        public List<Doctor> GetActiveDoctors()
        {
            return _context.Doctors.Where(d => d.Status == "ACTIVE").ToList();
        }

        public List<Doctor> GetDoctorsBySpecialization(string specialization)
        {
            return _context.Doctors
                .Where(d => d.Specialization == specialization && d.Status == "ACTIVE")
                .ToList();
        }
    }
}
