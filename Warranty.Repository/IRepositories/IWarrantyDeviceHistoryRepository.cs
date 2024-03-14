using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarrantyManagement.Entities;

namespace WarrantyRepository.IRepositories
{
    public interface IWarrantyDeviceHistoryRepository
    {
        bool Save();
        bool Add(WarrantyDeviceHistory warrantyDeviceHistory);
        List<WarrantyDeviceHistory> GetDeviceByWarrantyId(int warrantyId, int pageNumber);
        Task<int> Total(int warrantyId);
    }
}
