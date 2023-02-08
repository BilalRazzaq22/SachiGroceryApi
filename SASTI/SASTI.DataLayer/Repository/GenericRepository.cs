using SASTI.DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.DataLayer.Repository
{
    public class GenericRepository<T> where T : class
    {
        private BaseRepository<T> _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public GenericRepository(IUnitOfWork IUnitOfWork)
        {
            _unitOfWork = IUnitOfWork;
        }

        public BaseRepository<T> Repository
        {
            get
            {

                if (_Repository == null)
                {
                    _Repository = new BaseRepository<T>(_unitOfWork);
                }
                return _Repository;
            }
        }
    }
}
