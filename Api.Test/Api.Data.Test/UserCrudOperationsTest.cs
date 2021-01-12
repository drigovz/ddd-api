using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class UserCrudOperationsTest : DbTest, IClassFixture<DbTest>
    {
        private readonly ServiceProvider _serviceProvider;

        public UserCrudOperationsTest(DbTest bdTeste)
        {
            _serviceProvider = bdTeste.ServiceProvider;
        }

        [Fact]
        [Trait("CRUD", "UserEntity")]
        public async Task Its_Possible_Create_New_User()
        {
            using (var context = _serviceProvider.GetService<AppDbContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                UserEntity _entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };

                var _registerCreated = await _repository.AddAsync(_entity);

                Assert.NotNull(_registerCreated);
                Assert.Equal(_entity.Email, _registerCreated.Email);
                Assert.Equal(_entity.Name, _registerCreated.Name);
            }
        }

        [Fact]
        [Trait("CRUD", "UserEntity")]
        public async Task Its_Possible_Update_User()
        {
            using (var context = _serviceProvider.GetService<AppDbContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                UserEntity _entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };

                var userCreated = await _repository.AddAsync(_entity);

                _entity.Name = Faker.Name.First();
                var userUpdated = await _repository.UpdateAsync(userCreated);

                Assert.NotNull(userUpdated);
                Assert.Equal(_entity.Email, userUpdated.Email);
                Assert.Equal(_entity.Name, userUpdated.Name);
            }
        }

        [Fact]
        [Trait("CRUD", "UserEntity")]
        public async Task Its_Possible_List_Users()
        {
            using (var context = _serviceProvider.GetService<AppDbContext>())
            {
                UserImplementation _repository = new UserImplementation(context);

                var entities = new List<UserEntity>();

                for (int i = 0; i < 5; i++)
                {
                    UserEntity _entity = new UserEntity
                    {
                        Email = Faker.Internet.Email(),
                        Name = Faker.Name.FullName()
                    };

                    entities.Add(_entity);
                    await _repository.AddAsync(_entity);
                }

                var listEntities = await _repository.GetAsync();

                Assert.True(listEntities.Count() > 0);
            }
        }

        [Fact]
        [Trait("CRUD", "UserEntity")]
        public async Task Its_Possible_Delete_Users()
        {
            using (var context = _serviceProvider.GetService<AppDbContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                UserEntity _entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };

                var userCreated = await _repository.AddAsync(_entity);

                var result = await _repository.DeleteAsync(userCreated.Id);
                Assert.True(result);
            }
        }

        [Fact]
        [Trait("CRUD", "UserEntity")]
        public async Task Its_Possible_Get_User_Details()
        {
            using (var context = _serviceProvider.GetService<AppDbContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                UserEntity _entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };

                var userCreated = await _repository.AddAsync(_entity);
                var user = await _repository.GetById(userCreated.Id);

                Assert.NotNull(user);
                Assert.Equal(_entity.Email, user.Email);
                Assert.Equal(_entity.Name, user.Name);
            }
        }
    }
}