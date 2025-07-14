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
    public class ServiceService
    {
        private readonly ServiceRepository _serviceRepository;

        public ServiceService()
        {
            this._serviceRepository = new ServiceRepository();
        }

        public List<Service> GetServices()
        {
            return _serviceRepository.GetServices();
        }

        public Service GetService(int id)
        {
            return _serviceRepository.GetService(id);
        }

        public void CreateService(Service service)
        {
            _serviceRepository.CreateService(service);
        }

        public List<Service> SearchServices(string searchText)
        {
            return _serviceRepository.SearchServices(searchText);
        }

        public void UpdateService(Service service)
        {
            _serviceRepository.UpdateService(service);
        }

        public bool ExistByServiceName(string name)
        {
            return _serviceRepository.ExistByServiceName(name);
        }

        public bool ExistsByServiceNameExceptId(string serviceName, int id)
        {
            return _serviceRepository.ExistsByServiceNameExceptId(serviceName, id);
        }

    }
}
