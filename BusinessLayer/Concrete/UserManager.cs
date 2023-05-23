using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserManager : GenericManager<User>, IUserService
    {
        IUserDal userDal;
        public UserManager(IUserDal EfRepository) : base(EfRepository)
        {
            userDal = EfRepository;
        }

        public List<User> GetAllIncludeOthers()
        {
            return userDal.efRep_GetAllIncludeOthers();
        }

        public User GetUserData(string username)
        {
            return userDal.genericRep_Get(l => l.Username == username);
        }

        public User Login(User user)
        {
            return userDal.genericRep_Get(x => x.Username == user.Username && x.Password == user.Password);
        }
    }
}
