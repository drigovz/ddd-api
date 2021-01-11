using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Interfaces.Repository;
using Api.Domain.DTOs;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;

        public LoginService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> FindByLogin(LoginDTO user)
        {
            if (user != null && !string.IsNullOrEmpty(user.Email))
                return await _repository.FindByEmail(user.Email);
            else
                return null;
        }
    }
}