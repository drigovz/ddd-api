using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<UserDTO> GetAsync(Guid id);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserCreatedResultDTO> PostAsync(UserDTO user);
        Task<UserDTO> PutAsync(UserDTO user);
        Task<bool> DeleteAsync(Guid id);
    }
}