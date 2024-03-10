using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectoCodigoFacilito.Application.Common.Mapping;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUserById;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;
using ProjectoCodigoFacilito.Domain.Repository;
using ProjectoCodigoFacilito.Infraestructure.Data;
using ProjectoCodigoFacilito.Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.UnitTesting.Api.Test.UseCases.User
{
    public class GetUserByIdQueryHandlerTest
    {
        [Fact]
        public async Task When_ExistUserById_Expected_SuccessfulResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            ProjectDbContext projectDbContext = new ProjectDbContext(options);

            projectDbContext.Users.Add(new Domain.Entities.User
            {
                Id = 1,
                Name = "Test",
                Email = "test@gmail.com",
                Password = "123456",
                listFavoriteCharacters = new List<Domain.Entities.Character>
                {
                    new Domain.Entities.Character
                    {
                        Id = 1,
                        Name = "Test",
                        History = "Test",
                        ImageUrl = "www.test.com",
                        CreatedById = 1,
                        ModifiedById = 1,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                        Role = "Admin",
                        Game = "TestGame"
                    }
                },
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsDeleted = false,
                Role = "Admin"

            });
            projectDbContext.SaveChanges();

            IUnitOfWork unitOfWork = new UnitOfWork(projectDbContext);
            IMapper mapper = new MapperConfiguration(c => c.AddProfile<MappingProfile>()).CreateMapper();
            IUserRepository userRepository = new UserRepository(projectDbContext, unitOfWork);
            GetUserByIdQueryHandler getUserQueryHandler = new GetUserByIdQueryHandler(userRepository, mapper);

            // Act
            var result = await getUserQueryHandler.Handle(new GetUserByIdQuery { UserId = 1}, new System.Threading.CancellationToken());

            // Assert
            Assert.IsType<UserDTO>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task When_ExistUserById_Expected_BadResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            ProjectDbContext projectDbContext = new ProjectDbContext(options);

            projectDbContext.Users.Add(new Domain.Entities.User
            {
                Id = 1,
                Name = "Test",
                Email = "test@gmail.com",
                Password = "123456",
                listFavoriteCharacters = new List<Domain.Entities.Character>
                {
                    new Domain.Entities.Character
                    {
                        Id = 1,
                        Name = "Test",
                        History = "Test",
                        ImageUrl = "www.test.com",
                        CreatedById = 1,
                        ModifiedById = 1,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                        Role = "Admin",
                        Game = "TestGame"
                    }
                },
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsDeleted = false,
                Role = "Admin"

            });
            projectDbContext.SaveChanges();

            IUnitOfWork unitOfWork = new UnitOfWork(projectDbContext);
            IMapper mapper = new MapperConfiguration(c => c.AddProfile<MappingProfile>()).CreateMapper();
            IUserRepository userRepository = new UserRepository(projectDbContext, unitOfWork);
            GetUserByIdQueryHandler getUserQueryHandler = new GetUserByIdQueryHandler(userRepository, mapper);

            // Act
            var result = await getUserQueryHandler.Handle(new GetUserByIdQuery { UserId = 2 }, new System.Threading.CancellationToken());

            // Asserts
            Assert.Null(result);
        }
    }
}
