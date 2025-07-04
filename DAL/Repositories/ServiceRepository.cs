using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ServiceRepository
    {
        private readonly Prn212Context _context;

        public ServiceRepository()
        {
            this._context = new Prn212Context();
        }

        public List<Service> GetServices()
        {
            return _context.Services.ToList();
        }

        public Service? GetService(int id)
        {
            return _context.Services.FirstOrDefault(x => x.ServiceId == id);
        }

        public void CreateService(Service service)
        {
            service.ServiceId = _context.Services.Max(x => x.ServiceId) + 1;
            _context.Services.Add(service);
            _context.SaveChanges();
        }

        public List<Service> SearchServices(string searchText)
        {
            return _context.Services
                .Where
                (x => x.Name.ToLower().Contains(searchText.ToLower()) ||
                 x.Description.ToLower().Contains(searchText.ToLower())).ToList();
        }

        public void UpdateService(Service service)
        {
            var serviceFromDB = _context.Services.Find(service.ServiceId);
            if (serviceFromDB != null)
            {
                serviceFromDB.Name = service.Name;
                serviceFromDB.Description = service.Description;
                serviceFromDB.Price = service.Price;
                serviceFromDB.Status = service.Status;
                _context.Update(serviceFromDB);
            }
            _context.SaveChanges();
        }


    }
}
