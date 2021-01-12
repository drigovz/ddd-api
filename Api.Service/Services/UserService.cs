using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _repository;
        private readonly IMapper _mapper;

        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(entities);
        }

        public async Task<UserDTO> GetAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<UserDTO>(entity);
        }

        public async Task<UserCreatedResultDTO> PostAsync(UserDTO user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _repository.AddAsync(entity);

            return _mapper.Map<UserCreatedResultDTO>(result);
        }

        public async Task<UserDTO> PutAsync(UserDTO user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<UserDTO>(result);
        }
    }
}