using Microsoft.EntityFrameworkCore;
using ProjectoCodigoFacilito.Application.Users.Commands.DeleteUser;
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
    public class DeleteUserCommandHandlerTest
    {
        [Fact]
        public async Task When_DeleteUser_Expected_SuccessfulResult()
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
                Role = "Admin",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            });
            projectDbContext.SaveChanges();
            IUnitOfWork unitOfWork = new UnitOfWork(projectDbContext);
            IUserRepository userRepository = new UserRepository(projectDbContext, unitOfWork);

            DeleteUserCommandHandler deleteUserCommandHandler = new DeleteUserCommandHandler(userRepository);

            // Act
            var result = await deleteUserCommandHandler.Handle(new DeleteUserCommand { Id = 1 }, new System.Threading.CancellationToken());

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task When_DeleteUser_Expected_BadResult()
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
                Role = "Admin",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            });
            projectDbContext.SaveChanges();
            IUnitOfWork unitOfWork = new UnitOfWork(projectDbContext);
            IUserRepository userRepository = new UserRepository(projectDbContext, unitOfWork);

            DeleteUserCommandHandler deleteUserCommandHandler = new DeleteUserCommandHandler(userRepository);

            // Act
            var result = await deleteUserCommandHandler.Handle(new DeleteUserCommand { Id = 2 }, new System.Threading.CancellationToken());

            // Assert
            Assert.Equal(0, result);
        }
    }
}
