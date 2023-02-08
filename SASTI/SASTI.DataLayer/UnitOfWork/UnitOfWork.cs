using SASTI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.DataLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        DbContextTransaction transaction;
        private SASTIEntities _db;
        public SASTIEntities Db
        {
            get
            {
                if (_db == null)
                {
                    _db = new SASTIEntities();
                }
                return _db;
            }
        }

        public void StartTransaction()
        {
            transaction = Db.Database.BeginTransaction();
        }
        public void Commit()
        {
            if (transaction != null)
            {
                Db.SaveChanges();
                transaction.Commit();
            }
        }
        public void RollBack()
        {
            if (transaction != null)
            {
                transaction.Rollback();
            }
        }
    }
}
