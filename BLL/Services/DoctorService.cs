using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DLL.Services
{
    public class DoctorService
    {
        private readonly DoctorRepository _doctorRepository;

        public DoctorService()
        {
            _doctorRepository = new DoctorRepository();
        }

        public List<Doctor> GetAllDoctors()
        {
            return _doctorRepository.GetAllDoctors();
        }

        public void DeleteDoctor(Doctor doctor)
        {
            _doctorRepository.DeleteDoctor(doctor);
        }

        public Doctor GetDoctorById(int doctorId)
        {
            return _doctorRepository.GetDoctorById(doctorId);
        }

        public void CreateDoctor(Doctor doctor)
        {
            _doctorRepository.CreateDoctor(doctor);
        }

        public void UpdateDoctorNoReturn(Doctor doctor)
        {
            _doctorRepository.UpdateDoctorNoReturn(doctor);
        }

        public bool BanDoctor(int doctorId)
        {
            try
            {
                return _doctorRepository.BanDoctor(doctorId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UnbanDoctor(int doctorId)
        {
            try
            {
                return _doctorRepository.UnbanDoctor(doctorId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Doctor> GetActiveDoctors()
        {
            return _doctorRepository.GetActiveDoctors();
        }

        public List<Doctor> GetDoctorsBySpecialization(string specialization)
        {
            return _doctorRepository.GetDoctorsBySpecialization(specialization);
        }

        public bool ValidatePhone(string phone)
        {
            return !string.IsNullOrEmpty(phone) && phone.Length >= 10 && phone.All(char.IsDigit);
        }

        public bool ValidateLicenseNumber(string licenseNumber)
        {
            return string.IsNullOrEmpty(licenseNumber) || licenseNumber.Length >= 5;
        }

        public bool ValidateExperience(int? experience)
        {
            return !experience.HasValue || experience >= 0;
        }

        public bool ExistsByPhone(string phone)
        {
            return _doctorRepository.ExistsByPhone(phone);
        }

        public bool ExistsByEmail(string email)
        {
            return _doctorRepository.ExistsByEmail(email);
        }

        public List<string> GetUniqueSpecializations()
        {
            var doctors = _doctorRepository.GetAllDoctors();
            return doctors.Select(d => d.Specialization).Distinct().OrderBy(s => s).ToList();
        }
    }
}
