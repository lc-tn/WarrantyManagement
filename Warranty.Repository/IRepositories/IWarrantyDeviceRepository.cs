using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarrantyManagement.Entities;

namespace WarrantyRepository.IRepositories
{
    public interface IWarrantyDeviceRepository
    {
        bool Save();
        bool Add(WarrantyDevice warrantyDevice);
        List<WarrantyDevice> GetDeviceByWarrantyId(int warrantyId);
        bool Edit(WarrantyDevice warrantyDevice);
    }
}
