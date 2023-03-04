using auth.Authorization;
using auth.Data;
using auth.Model;
using AutoMapper;

namespace auth.Interfaces
{
    public interface IUserService 
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(RegisterRequest model);
    }


}
