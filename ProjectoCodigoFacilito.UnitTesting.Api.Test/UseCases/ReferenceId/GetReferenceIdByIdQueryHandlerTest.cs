using Microsoft.EntityFrameworkCore;
using ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;
using ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceIdById;
using ProjectoCodigoFacilito.Domain.Repository;
using ProjectoCodigoFacilito.Infraestructure.Data;
using ProjectoCodigoFacilito.Infraestructure.Repository;


namespace ProjectoCodigoFacilito.UnitTesting.Api.Test.UseCases.ReferenceId
{
    public class GetReferenceIdByIdQueryHandlerTest
    {
        [Fact]
        public async Task When_ExistReferencesIdById_Expected_SuccessfulResult()
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

            GetReferenceIdByIdQueryHandler getReferenceIdQueryHandler = new(referenceIdRepository);

            // Act
            var result = await getReferenceIdQueryHandler.Handle(new GetReferenceIdByIdQuery{ UserId = 2, CharacterId = 2}, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ReferenceIdDTO>(result);
            Assert.Equal(2, result.UserId);
            Assert.Equal(2, result.CharacterId);
        }

        [Fact]
        public async Task When_ExistReferencesIdById_Expected_BadResult()
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
                CharacterId = 3,
                IsVisible = true
            });
            projectDbContext.SaveChanges();

            IUnitOfWork unitOfWork = new UnitOfWork(projectDbContext);
            IReferenceIdRepository referenceIdRepository = new ReferenceIdRepository(projectDbContext, unitOfWork);

            GetReferenceIdByIdQueryHandler getReferenceIdQueryHandler = new(referenceIdRepository);

            // Act
            var result = await getReferenceIdQueryHandler.Handle(new GetReferenceIdByIdQuery { UserId = 2, CharacterId = 2 }, new System.Threading.CancellationToken());

            // Assert
            Assert.Null(result);
            Assert.True(result == null);
        }
    }
}
