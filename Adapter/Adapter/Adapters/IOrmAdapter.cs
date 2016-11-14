using Adapter.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adapter.Adapters
{
    interface IOrmAdapter<TDbEntity> where TDbEntity : IDbEntity
    {
        IDbEntity this[int index]
        {
            get;
            set;
        }
        void Add(IDbEntity entity);
        void Delete(IDbEntity entity);
    }
}
