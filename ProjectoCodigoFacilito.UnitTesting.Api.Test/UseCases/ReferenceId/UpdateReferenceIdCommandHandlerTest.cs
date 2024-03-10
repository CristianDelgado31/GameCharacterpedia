using Microsoft.EntityFrameworkCore;
using ProjectoCodigoFacilito.Application.ReferenceId.Commands.ModifyReferenceId;
using ProjectoCodigoFacilito.Domain.Repository;
using ProjectoCodigoFacilito.Infraestructure.Data;
using ProjectoCodigoFacilito.Infraestructure.Repository;


namespace ProjectoCodigoFacilito.UnitTesting.Api.Test.UseCases.ReferenceId
{
    public class UpdateReferenceIdCommandHandlerTest
    {

        [Fact]
        public async Task When_UpdateReferenceId_Expected_SuccessfulResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            ProjectDbContext projectDbContext = new ProjectDbContext(options);

            projectDbContext.ReferenceIds.Add(new Domain.Entities.ReferenceId
            {
                Id = 1,
                UserId = 2,
                CharacterId = 2,
                IsVisible = false
            });
            projectDbContext.SaveChanges();

            IUnitOfWork unitOfWork = new UnitOfWork(projectDbContext);
            IReferenceIdRepository referenceIdRepository = new ReferenceIdRepository(projectDbContext, unitOfWork);

            UpdateReferenceIdCommandHandler deleteReferenceIdHandler = new(referenceIdRepository);

            // Act
            var result = await deleteReferenceIdHandler.Handle(new UpdateReferenceIdCommand { UserId = 2, CharacterId = 2 }, new System.Threading.CancellationToken());


            // Assert
            Assert.IsType<int>(result);
            Assert.True(result == 1);
        }

        [Fact]
        public async Task When_UpdateReferenceId_Expected_BadResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            ProjectDbContext projectDbContext = new ProjectDbContext(options);

            projectDbContext.ReferenceIds.Add(new Domain.Entities.ReferenceId
            {
                Id = 1,
                UserId = 2,
                CharacterId = 2,
                IsVisible = false
            });
            projectDbContext.SaveChanges();

            IUnitOfWork unitOfWork = new UnitOfWork(projectDbContext);
            IReferenceIdRepository referenceIdRepository = new ReferenceIdRepository(projectDbContext, unitOfWork);

            UpdateReferenceIdCommandHandler deleteReferenceIdHandler = new(referenceIdRepository);

            // Act
            var result = await deleteReferenceIdHandler.Handle(new UpdateReferenceIdCommand { UserId = 2, CharacterId = 2 }, new System.Threading.CancellationToken());


            // Assert
            Assert.IsType<int>(result);
            Assert.True(result == 1);
        }
    }
}
