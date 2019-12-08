using Kontest.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kontest.Service.Interfaces
{
    public interface IUserService
    {
        ApplicationUser FindUserByOtac(string otac);
    }
}
