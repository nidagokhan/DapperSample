using Dapper.Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Sample.Interface
{
    public interface IShipperDal
    {
        List<Shipper> GetAll();
        Shipper GetById(int id);
        Shipper Add(Shipper shipper);
        void Update(int id,Shipper shipper);
        void DeleteById(int id);
    }
}
