
using Bike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebAuthentication.Repository
{
    public interface IJWTManagerRepository
    {
        TokenModels Authenticate(User users);
    }

}