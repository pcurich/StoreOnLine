﻿using System.Collections.Generic;
using System.Web.Mvc;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.DataBase.Abstract
{
    public interface ISecurityRepository
    {
        IEnumerable<Role> Roles { get; }
        IEnumerable<User> Users { get; }
        SelectList GetRoles(string selected);
        SelectList GetUser(string selected);
    }
}
