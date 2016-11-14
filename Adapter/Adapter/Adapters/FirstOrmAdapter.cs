using Adapter.Adapters;
using Adapter.FirstOrmLibrary;
using Adapter.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adapter.Adapters
{
    class FirstOrmAdapter<TDbEntity> : IOrmAdapter<TDbEntity> where TDbEntity : IDbEntity
    {
        private IFirstOrm<TDbEntity> _orm;

        public IDbEntity this[int index]
        {
            get
            {
                return _orm.Read(index);
            }
            set
            {
                _orm.Update((TDbEntity)value);
            }
        }

        public FirstOrmAdapter(IFirstOrm<TDbEntity> orm)
        {
            _orm = orm;
        }

        public void Add(IDbEntity entity)
        {
            if (entity is TDbEntity)
                _orm.Create((TDbEntity)entity);
            else
                throw new ArgumentException();
        }

        public void Delete(IDbEntity entity)
        {
            if (entity is TDbEntity)
                _orm.Delete((TDbEntity)entity);
            else
                throw new ArgumentException();
        }
    }
}
