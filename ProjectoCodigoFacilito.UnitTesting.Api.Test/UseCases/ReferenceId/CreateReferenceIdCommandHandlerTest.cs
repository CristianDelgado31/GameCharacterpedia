using Microsoft.EntityFrameworkCore;
using ProjectoCodigoFacilito.Application.ReferenceId.Commands.CreateReferenceId;
using ProjectoCodigoFacilito.Domain.Repository;
using ProjectoCodigoFacilito.Infraestructure.Data;
using ProjectoCodigoFacilito.Infraestructure.Repository;


namespace ProjectoCodigoFacilito.UnitTesting.Api.Test.UseCases.ReferenceId
{
    public class CreateReferenceIdCommandHandlerTest
    {

        [Fact]
        public async Task CreateReferenceIdCommandHandler_WhenCalled_ReturnReferenceIdDTO()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            ProjectDbContext projectDbContext = new ProjectDbContext(options);

            IUnitOfWork unitOfWork = new UnitOfWork(projectDbContext);
            IReferenceIdRepository referenceIdRepository = new ReferenceIdRepository(projectDbContext, unitOfWork);

            CreateReferenceIdCommandHandler createReferenceIdCommandHandler = new (referenceIdRepository);

            CreateReferenceIdCommand command = new CreateReferenceIdCommand
            {
                UserId = 1,
                CharacterId = 1
            };

            // Act
            var result = await createReferenceIdCommandHandler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            Assert.True(result.Id > 0);
            
        }
    }
}
