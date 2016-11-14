using Adapter.Models;
using Adapter.Models.Interfaces;
using Adapter.SecondOrmLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adapter.Adapters
{
    class SecondOrmAdapter<TDbEntity> : IOrmAdapter<TDbEntity> where TDbEntity : IDbEntity
    {
        private ISecondOrm _orm;

        public IDbEntity this[int index]
        {
            get
            {
                return _get(index);
            }
            set
            {
                var oldItem = _get(value.Id);
                if (value.GetType() == typeof(DbUserEntity))
                {
                    _orm.Context.Users.Remove((DbUserEntity)oldItem);
                    _orm.Context.Users.Add((DbUserEntity)value);
                }
                else if (value.GetType() == typeof(DbUserInfoEntity))
                {
                    _orm.Context.UserInfos.Remove((DbUserInfoEntity)oldItem);
                    _orm.Context.UserInfos.Add((DbUserInfoEntity)value);
                }
                else
                    throw new ArgumentException();
            }
        }

        public SecondOrmAdapter(ISecondOrm orm)
        {
            _orm = orm;
        }

        private IDbEntity _get(int index)
        {
            if (typeof(TDbEntity) == typeof(DbUserEntity))
                return _orm.Context.Users.Where(x => x.Id == index).FirstOrDefault();
            else if (typeof(TDbEntity) == typeof(DbUserInfoEntity))
                return _orm.Context.UserInfos.Where(x => x.Id == index).FirstOrDefault();
            else
                throw new ArgumentException();
        }

        public void Add(IDbEntity entity)
        {
            if (entity is DbUserEntity)
                _orm.Context.Users.Add((DbUserEntity)entity);
            else if (entity is DbUserInfoEntity)
                _orm.Context.UserInfos.Add((DbUserInfoEntity)entity);
            else
                throw new ArgumentException();
        }

        public void Delete(IDbEntity entity)
        {
            if (entity is DbUserEntity)
                _orm.Context.Users.Remove((DbUserEntity)entity);
            else if (entity is DbUserInfoEntity)
                _orm.Context.UserInfos.Remove((DbUserInfoEntity)entity);
            else
                throw new ArgumentException();
        }
    }
}
