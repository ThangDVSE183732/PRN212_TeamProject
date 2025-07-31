using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapper
{
    public class PatientMapper
    {
        public static Patient ToPatient(string email, string password)
        {
            Patient newPatient = new Patient();
            newPatient.Email = email;
            newPatient.Password = password;
            newPatient.Name = "";
            newPatient.Phone = "";
            newPatient.BloodType = "";
            newPatient.EmergencyPhoneNumber = "";
            newPatient.Status = "Active";
            newPatient.Address = "";
            newPatient.DateOfBirth = DateOnly.FromDateTime(DateTime.Now);
            return newPatient;
        }
    }
}
