using Microsoft.EntityFrameworkCore;
using ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;
using ProjectoCodigoFacilito.Domain.Repository;
using ProjectoCodigoFacilito.Infraestructure.Data;
using ProjectoCodigoFacilito.Infraestructure.Repository;


namespace ProjectoCodigoFacilito.UnitTesting.Api.Test.UseCases.ReferenceId
{
    public class GetAllReferenceIdQueryHandlerTest
    {

        [Fact]
        public async Task When_NotExistReferencesId_Expected_SuccessfulResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            ProjectDbContext projectDbContext = new ProjectDbContext(options);

            IUnitOfWork unitOfWork = new UnitOfWork(projectDbContext);
            IReferenceIdRepository referenceIdRepository = new ReferenceIdRepository(projectDbContext, unitOfWork);

            GetAllReferenceIdQueryHandler getReferenceIdQueryHandler = new (referenceIdRepository);

            // Act
            var result = await getReferenceIdQueryHandler.Handle(new GetAllReferenceIdQuery(), new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ReferenceIdDTO>>(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public async Task When_ExistReferencesId_Expected_BadResult()
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
                IsVisible = true
            });
            projectDbContext.SaveChanges();

            IUnitOfWork unitOfWork = new UnitOfWork(projectDbContext);
            IReferenceIdRepository referenceIdRepository = new ReferenceIdRepository(projectDbContext, unitOfWork);

            GetAllReferenceIdQueryHandler getReferenceIdQueryHandler = new(referenceIdRepository);

            // Act
            var result = await getReferenceIdQueryHandler.Handle(new GetAllReferenceIdQuery(), new System.Threading.CancellationToken());

            // Assert
            Assert.NotEmpty(result);
        }
    }
}
