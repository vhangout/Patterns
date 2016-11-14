﻿using Adapter.Models.Interfaces;

namespace Adapter.Models
{
    public class DbUserEntity : IDbEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public int UserInfoId { get; set; }
    }
}
