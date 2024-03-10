using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacterById;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;
using ProjectoCodigoFacilito.Application.Common.Mapping;
using ProjectoCodigoFacilito.Domain.Repository;
using ProjectoCodigoFacilito.Infraestructure.Data;
using ProjectoCodigoFacilito.Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.UnitTesting.Api.Test.UseCases.Character
{
    public class GetCharacterByIdHandlerTest
    {

        [Fact]
        public async Task GetCharacterByIdCommandHandler_Success()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            ProjectDbContext projectDbContext = new ProjectDbContext(options);

            projectDbContext.Characters.Add(new Domain.Entities.Character
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
            });
            projectDbContext.Characters.Add(new Domain.Entities.Character
            {
                Id = 2,
                Name = "Test2",
                History = "Test2",
                ImageUrl = "www.test2.com",
                CreatedById = 2,
                ModifiedById = 2,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsDeleted = true,
                Role = "Admin",
                Game = "TestGame2"
            });
            projectDbContext.SaveChanges();
            IUnitOfWork unitOfWork = new UnitOfWork(projectDbContext);
            IMapper mapper = new MapperConfiguration(c => c.AddProfile<MappingProfile>()).CreateMapper();
            ICharacterRepository characterRepository = new CharacterRepository(projectDbContext, unitOfWork);
            GetCharacterByIdQueryHandler getCharacterQueryHandler = new(characterRepository, mapper);

            // Act
            var result = await getCharacterQueryHandler.Handle(new GetCharacterByIdQuery { CharacterId = 2}, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CharacterDTO>(result);

        }
    }
}
