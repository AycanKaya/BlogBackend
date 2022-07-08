using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Model;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseModel> RegisterUser(RegisterModel model);
        Task<ResponseModel> LoginUser(LoginModel model);

    }
}
