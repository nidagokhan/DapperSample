using Dapper.Sample.Context;
using Dapper.Sample.Entities;
using Dapper.Sample.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Sample.Concrete
{

    #region MyNotes
    /// <summary>
    /// Dapper'da Query ve Execute metodları farklı türde işlemler için kullanılır:
    ///* Query: Bu metod, genellikle sorgu sonuçlarını almak için kullanılır.Sorgu sonuçlarını bir koleksiyon içinde döndürür.Bu koleksiyon, sorgu sonuçlarının tipine karşılık gelen bir liste veya özel bir nesne tipi olabilir.Örneğin Query<T> metodu, sorgu sonuçlarını T tipinde bir liste olarak döndürür.
    /// *Execute: Bu metod, sorguyu çalıştırmak ve sonucunda etkilenen satır sayısını almak için kullanılır. Sorgu sonucunda veriler döndürülmez, sadece etkilenen satır sayısı bilgisi elde edilir.Bu metot genellikle INSERT, UPDATE veya DELETE gibi sorgular için kullanılır.
    /// </summary> 
    #endregion
    public class ShipperDal:IShipperDal
    {
        private readonly IDbConnection db;

        public ShipperDal(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("conn"));
        }

        //todo db bi veri ekliyor ancak sonuç çoklu dönüyor şeklinde hata veriyor
        public Shipper Add(Shipper shipper)
        {
            var sql = "Insert Into Shippers (CompanyName,Phone,Aktifmi) Values (@CompanyName,@Phone,@Aktifmi);";
            var id = db.Query<int>(sql, shipper).Single();
            shipper.ShipperID = id;
            return shipper;
        }

        public void DeleteById(int id)
        {
            var sql = "Delete From Shippers where ShipperID=@id";
            db.Execute(sql, new { @id = id });
        }

        public List<Shipper> GetAll()
        {
            var sql = "Select * From Shippers";
            return db.Query<Shipper>(sql).ToList();
        }

        public Shipper GetById(int id)
        {
            var sql = "Select * From Shippers where ShipperID=@id";
            return db.Query<Shipper>(sql, new { @id = id }).SingleOrDefault();
        }

        public void Update(int id,Shipper shipper)
        {
            var sql = "Update Shippers set CompanyName=@CompanyName,Phone=@Phone,Aktifmi=@Aktifmi where ShipperID=@id";
            db.Execute(sql, new { id, shipper.CompanyName, shipper.Phone, shipper.Aktifmi });
        }
    }
}
