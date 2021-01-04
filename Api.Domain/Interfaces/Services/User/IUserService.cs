using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<UserEntity> GetAsync(Guid id);
        Task<IEnumerable<UserEntity>> GetAllAsync();
        Task<UserEntity> PostAsync(UserEntity user);
        Task<UserEntity> PutAsync(UserEntity user);
        Task<bool> DeleteAsync(Guid id);
    }
}