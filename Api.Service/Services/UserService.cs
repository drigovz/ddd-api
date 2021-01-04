using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _repository;

        public UserService(IRepository<UserEntity> repository)
        {
            _repository = repository;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<UserEntity> GetAsync(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<UserEntity> PostAsync(UserEntity user)
        {
            return await _repository.AddAsync(user);
        }

        public async Task<UserEntity> PutAsync(UserEntity user)
        {
            return await _repository.UpdateAsync(user);
        }
    }
}