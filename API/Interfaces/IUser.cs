using System;
using System.Collections.Generic;

namespace API.Interface
{
    using System.Linq;
    using System.Web;

    public interface  IUser
    {
        IList<User> Get();
        bool Add(User user);
        bool Update(User user);
        bool Delete(int id);
    }
}