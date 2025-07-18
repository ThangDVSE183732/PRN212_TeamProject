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

        public DoctorService(Prn212Context context)
        {
            _doctorRepository = new DoctorRepository(context);
        }

        public List<Doctor> GetAllDoctors()
        {
            return _doctorRepository.GetAllDoctors();
        }

        public Doctor GetDoctorById(int doctorId)
        {
            return _doctorRepository.GetDoctorById(doctorId);
        }

        public bool CreateDoctor(Doctor doctor)
        {
            if (string.IsNullOrWhiteSpace(doctor.DoctorName)
                || string.IsNullOrWhiteSpace(doctor.Phone)
                || string.IsNullOrWhiteSpace(doctor.Specialization))
                return false;

            if (!ValidatePhone(doctor.Phone)) return false;
            if (!ValidateLicenseNumber(doctor.LicenseNumber)) return false;
            if (!ValidateExperience(doctor.Experience)) return false;

            if (_doctorRepository.PhoneExists(doctor.Phone)) return false;

            if (!string.IsNullOrEmpty(doctor.LicenseNumber) &&
                _doctorRepository.LicenseNumberExists(doctor.LicenseNumber)) return false;

            doctor.Status = "ACTIVE";

            return _doctorRepository.CreateDoctor(doctor);
        }


        public bool UpdateDoctor(Doctor doctor)
        {
            try
            {
                if (string.IsNullOrEmpty(doctor.DoctorName) ||
                    string.IsNullOrEmpty(doctor.Phone) ||
                    string.IsNullOrEmpty(doctor.Specialization))
                {
                    return false;
                }

                return _doctorRepository.UpdateDoctor(doctor);
            }
            catch (Exception)
            {
                return false;
            }
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

        public List<string> GetUniqueSpecializations()
        {
            var doctors = _doctorRepository.GetAllDoctors();
            return doctors.Select(d => d.Specialization).Distinct().OrderBy(s => s).ToList();
        }
    }
}
