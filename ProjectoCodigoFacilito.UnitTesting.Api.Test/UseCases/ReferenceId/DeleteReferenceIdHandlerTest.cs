using Microsoft.EntityFrameworkCore;
using ProjectoCodigoFacilito.Application.ReferenceId.Commands.DeleteReferenceId;
using ProjectoCodigoFacilito.Domain.Repository;
using ProjectoCodigoFacilito.Infraestructure.Data;
using ProjectoCodigoFacilito.Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.UnitTesting.Api.Test.UseCases.ReferenceId
{
    public class DeleteReferenceIdHandlerTest
    {
        [Fact]
        public async Task When_DeleteReferenceId_Expected_ResultSuccess()
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

            DeleteReferenceIdCommandHandler deleteReferenceIdHandler = new(referenceIdRepository);

            // Act
            var result = await deleteReferenceIdHandler.Handle(new DeleteReferenceIdCommand { UserId = 2, CharacterId = 2 }, new System.Threading.CancellationToken());

            
            // Assert
            Assert.IsType<int>(result);
            Assert.True(result == 1);
       }

        [Fact]
        public async Task When_DeleteReferenceId_Expected_ResultBad()
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

            DeleteReferenceIdCommandHandler deleteReferenceIdHandler = new(referenceIdRepository);

            // Act
            var result = await deleteReferenceIdHandler.Handle(new DeleteReferenceIdCommand { UserId = 2, CharacterId = 3 }, new System.Threading.CancellationToken());


            // Assert
            Assert.IsType<int>(result);
            Assert.True(result == 0);
        }
    }
}
