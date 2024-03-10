using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectoCodigoFacilito.Application.Common.Mapping;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;
using ProjectoCodigoFacilito.Domain.Repository;
using ProjectoCodigoFacilito.Infraestructure.Data;
using ProjectoCodigoFacilito.Infraestructure.Repository;


namespace ProjectoCodigoFacilito.UnitTesting.Api.Test.UseCases.User
{
    public class GetUserQueryHandlerTest
    {

        [Fact]
        public async Task When_ExistUsers_Expected_SuccessfulResult()
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
            projectDbContext.Users.Add(new Domain.Entities.User
            {
                Id = 2,
                Name = "Test2",
                Email = "test2@gmail.com",
                Password = "12345632",
                listFavoriteCharacters = new List<Domain.Entities.Character>
            {
                new Domain.Entities.Character
                {
                    Id = 2,
                    Name = "Test2",
                    History = "Test2",
                    ImageUrl = "www.test.com",
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                    Role = "Admin",
                    Game = "TestGame2"
                }
            },
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsDeleted = false,
                Role = "User"

            });
            projectDbContext.SaveChanges();

            IUnitOfWork unitOfWork = new UnitOfWork(projectDbContext);
            IMapper mapper = new MapperConfiguration(c => c.AddProfile<MappingProfile>()).CreateMapper();
            IUserRepository userRepository = new UserRepository(projectDbContext, unitOfWork);
            GetUserQueryHandler getUserQueryHandler = new GetUserQueryHandler(userRepository, mapper);

            // Act
            var result = await getUserQueryHandler.Handle(new GetUserQuery(), new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<UserDTO>>(result);
        }
    }
}
