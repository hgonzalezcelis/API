using API.Interface;
using API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class UsersController : ApiController
    {

        #region GlobalVariables
        private IUser _management;
        private readonly TestEntities _context;
        #endregion

        #region Constructor
        public UsersController()
        {
            _context = new TestEntities();
            _management = new UserRepository();
        }
        public UsersController(IUser management)
        {
            _context = new TestEntities();

            if (management == null)
            {
                _management = new UserRepository();
            }
            else
            {
                _management = management;
            }
        }
        #endregion

        #region API Methods
        // GET api/<controller>
        [Route("api/User/GetUsers")]
        public IList<User> GetUsers()
        {
            return _management.Get();
        }
        [Route("api/User/AddUser")]
        [HttpPost]
 
        public bool AddUser([FromBody]User user)
        {
            return _management.Add(user);
        }
        [Route("api/User/UpdateUser")]
        [HttpPost]
        //POST:api/User/UpdateUser
        public bool UpdateUser([FromBody]User u)
        {
            return _management.Update(u);
        }
        // DELETE api/<controller>/5
        public bool Delete(int id)
        {
            return _management.Delete(id);
        }
        #endregion
    }
}
