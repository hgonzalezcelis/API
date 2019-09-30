using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using API.Interface;
using System;

namespace API.Repository
{
    [ExcludeFromCodeCoverage]
    public class UserRepository : IUser
    {
        private readonly TestEntities _context;
        public UserRepository()
        {
            _context = new TestEntities();
        }
        public bool Delete(int id)
        {
            using (var ctx = new TestEntities())
            {
                User user = ctx.User.Find(id);
                ctx.User.Remove(user);

                int rowsAffected = ctx.SaveChanges();

                return rowsAffected > 0 ? true : false;
            }
        }
        public IList<User> Get()
        {
            IQueryable<User> user = _context.User.Where(a => a.Id > 0);
            return user.ToList();
        }
        public bool Add(User user)
        {
            if (user == null)
            {
                throw new System.ArgumentNullException("user");
            }

            User newRow = new User();

            try
            {
                newRow.Name = user.Name;
                newRow.LastName = user.LastName;
                newRow.CreateDate = DateTime.Now;
                newRow.Address = user.Address;
                _context.User.Add(newRow);
                int rowsAffected = _context.SaveChanges();

                return rowsAffected > 0 ? true : false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool Update(User u)
        {
            if (u == null)
            {
                throw new ArgumentNullException("user");
            }

            using (var ctx = new TestEntities())
            {
                var user = _context.User.Single(a => a.Id == u.Id);

                if (user != null)
                {
                    user.Name = u.Name;
                    user.LastName = u.LastName;
                    user.UpdateDate = DateTime.Now;
                    user.Address = u.Address;
                    int rowsAffected = _context.SaveChanges();

                    return rowsAffected > 0 ? true : false;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}