using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using RailWorkshop.Db.Data;
using RailWorkshop.Db.Utils;
using RailWorkshop.Services.Entity;
using RailWorkshop.Services.Interfaces;

namespace RailWorkshop.Tests.Db.Data.Tests
{
    public class HandbookRepositoryTests
    {
        private readonly HandbookRepository _repos;

        public HandbookRepositoryTests()
        {
            _repos = new HandbookRepository(new RailWorkshop.Db.PostgresContext(), NullLogger<HandbookRepository>.Instance);
        }

        [Fact]
        public async void HandbookRepository_GetById_ReturnDefectWithId_1()
        {
            const int id = 1;

            Defect result = await _repos.GetById<Defect>(id);

            if (result is null)
            {
                return;
            }

            result.Id.Should().Be(id);
        }

        [Fact]
        public async void HandbookRepository_GetById_ReturnNullIfNotFound()
        {
            const int id = 100_000;

            RailProfile result = await _repos.GetById<RailProfile>(id);

            result.Should().BeNull();
        }

        [Fact]
        public async void HandbookRepository_GetAll_ReturnCollectionOfSteelGrade()
        {
            IReadOnlyCollection<SteelGrade> result = await _repos.GetAll<SteelGrade>();

            result.Count.Should().NotBe(0);
        }

        [Fact]
        public async void HandbookRepository_Create_CreateAndInsertToDbNewDefect()
        {
            Defect defect = new() { Name = "Искревление" };

            IHandbookEntity result = await _repos.Create(defect);
            Defect selected = await _repos.GetById<Defect>(result.Id);

            result.Should().BeOfType<Defect>();
            result.Name.Should().BeEquivalentTo(defect.Name);
            result.Should().BeEquivalentTo(selected);
        }

        [Fact]
        public async void HandbookRepository_Update_UpdateSteelGradeName()
        {
            SteelGrade steel = new() { Id = 1, Name = "Сталь 3" };

            await _repos.Update(steel);
            SteelGrade selected = await _repos.GetById<SteelGrade>(steel.Id);

            selected.Name.Should().BeEquivalentTo(steel.Name);
        }

        [Fact]
        public async void HandbookRepository_Update_EntityNotFoundException()
        {
            SteelGrade steel = new() { Id = 100_000, Name = "Сталь 3" };

            EntityNotFoundException ex1 = await Assert.ThrowsAsync<EntityNotFoundException>(() => _repos.Update(steel));
            SteelGrade selected = await _repos.GetById<SteelGrade>(steel.Id);

            ex1.Should().NotBeNull();
            selected.Should().BeNull();
        }

        [Fact]
        public async void HandbookRepository_Delete_EntityNotFoundException()
        {
            const int id = 100_000;
            Defect defect = new() { Id = id };

            EntityNotFoundException ex = await Assert.ThrowsAsync<EntityNotFoundException>(() => _repos.Delete(defect));
            Defect selected = await _repos.GetById<Defect>(id);

            ex.Should().NotBeNull();
            selected.Should().BeNull();
        }

        [Fact]
        public async void HandbookRepository_Delete_DeleteExistingDefect()
        {
            const int id = 200_000;
            Defect defect = new() { Id = id, Name = "FakeName" };

            Defect createReturn = await _repos.Create(defect);
            Defect deletedEntity = await _repos.Delete(defect);
            Defect selected1 = await _repos.GetById<Defect>(id);

            selected1.Should().BeNull();
        }
    }
}