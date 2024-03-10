using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectoCodigoFacilito.Application.Common.Mapping;
using ProjectoCodigoFacilito.Application.Users.Commands.CreateUser;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;
using ProjectoCodigoFacilito.Domain.Repository;
using ProjectoCodigoFacilito.Infraestructure.Data;
using ProjectoCodigoFacilito.Infraestructure.Repository;

namespace ProjectoCodigoFacilito.UnitTesting.Api.Test.UseCases.User
{
    public class CreateUserCommandHandlerTest
    {
        [Fact]
        public async Task When_CreateUser_Expected_SuccessfulResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            ProjectDbContext projectDbContext = new ProjectDbContext(options);

            IMapper mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper();

            IUnitOfWork unitOfWork = new UnitOfWork(projectDbContext);
            IUserRepository userRepository = new UserRepository(projectDbContext, unitOfWork);

            CreateUserCommandHandler createCharacterCommandHandler = new CreateUserCommandHandler(userRepository, mapper);
            CreateUserCommand command = new CreateUserCommand
            {
                Name = "Test",
                Email = "test@gmail.com",
                Password = "123456",
                Role = "Admin",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };
            // Act
            var result = await createCharacterCommandHandler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserDTO>(result);
        }

        [Fact]
        public async Task When_CreateUser_Expected_EmailBadResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            ProjectDbContext projectDbContext = new ProjectDbContext(options);

            IMapper mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper();

            IUnitOfWork unitOfWork = new UnitOfWork(projectDbContext);
            IUserRepository userRepository = new UserRepository(projectDbContext, unitOfWork);

            CreateUserCommandHandler createCharacterCommandHandler = new CreateUserCommandHandler(userRepository, mapper);
            CreateUserCommand command = new CreateUserCommand
            {
                Name = "Test",
                Email = "test@gmail.com",
                Password = "123456",
                Role = "Admin",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };
            // Act
            var result = await createCharacterCommandHandler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserDTO>(result);
        }
    }
}
