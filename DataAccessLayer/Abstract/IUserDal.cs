﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IUserDal : IGenericDal<User>
    {
        List<User> efRep_GetAllIncludeOthers();
        User efRep_GetIncludeChats(int ID);
    }
}
