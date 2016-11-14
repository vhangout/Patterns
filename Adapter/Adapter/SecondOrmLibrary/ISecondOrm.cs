using System.Collections.Generic;
using Adapter.Models;


namespace Adapter.SecondOrmLibrary
{
    public interface ISecondOrm
    {
        ISecondOrmContext Context { get; }
    }

    public interface ISecondOrmContext
    {
        HashSet<DbUserEntity> Users { get; }
        HashSet<DbUserInfoEntity> UserInfos { get; }
    }
}