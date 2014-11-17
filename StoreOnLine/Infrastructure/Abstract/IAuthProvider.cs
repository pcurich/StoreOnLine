using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StoreOnLine.Infrastructure.Abstract
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}
