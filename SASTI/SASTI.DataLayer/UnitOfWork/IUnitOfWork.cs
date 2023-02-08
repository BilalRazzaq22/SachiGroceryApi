using SASTI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.DataLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        SASTIEntities Db { get; }
        /// <summary>
        /// Starts a transaction on this unit of work
        /// </summary>
        void StartTransaction();

        /// <summary>
        /// Call this to commit the unit of work
        /// </summary>
        void Commit();

        /// <summary>
        /// Call this to Rollback the unit of work
        /// </summary>
        void RollBack();
    }
}
