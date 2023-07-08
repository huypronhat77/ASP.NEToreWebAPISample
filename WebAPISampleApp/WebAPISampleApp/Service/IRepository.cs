using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPISampleApp.Data;

namespace WebAPISampleApp.Service
{
    public interface IRepository<T, M>
    {
        List<T> GetAll();
        T GetById(string id);
        bool Remove(string id);
        bool Update(string id, M model);
        T Add(M model);
    }
}
