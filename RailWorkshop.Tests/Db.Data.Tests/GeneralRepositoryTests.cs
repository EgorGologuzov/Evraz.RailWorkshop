using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using RailWorkshop.Db;
using RailWorkshop.Db.Data;
using RailWorkshop.Services.Contracts;
using RailWorkshop.Services.Entity;

namespace RailWorkshop.Tests.Db.Data.Tests
{
    public class GeneralRepositoryTests
    {
        private readonly PostgresContext _context;
        private readonly HandbookRepository _handbookRepository;
        private readonly GeneralRepository<Product> _productRepos;
        private readonly GeneralRepository<Statement> _statementRepos;
        private readonly GeneralRepository<Employee> _employeeRepos;

        public GeneralRepositoryTests()
        {
            _context = new PostgresContext();
            _handbookRepository = new HandbookRepository(_context, NullLogger<HandbookRepository>.Instance);
            _productRepos = new GeneralRepository<Product>(_context, NullLogger<HandbookRepository>.Instance);
            _statementRepos = new GeneralRepository<Statement>(_context, NullLogger<HandbookRepository>.Instance);
            _employeeRepos = new GeneralRepository<Employee>(_context, NullLogger<HandbookRepository>.Instance);
        }

        public async void GeneralRepository_GetById_ReturnEmployeeById()
        {
            const string employeeGuid = "bc45539b-585a-454f-824c-3be7eabc6f94";

            Employee result = await _employeeRepos.GetById(employeeGuid);

            result.Should().NotBeNull();
        }

        [Fact]
        public async void GeneralRepository_Create_CreateProduct()
        {
            Product entity = new()
            {
                Name = "75849Е893",
                Profile = (await _handbookRepository.GetAll<RailProfile>()).FirstOrDefault(),
                Steel = (await _handbookRepository.GetAll<SteelGrade>()).FirstOrDefault(),
                Properties = new Dictionary<string, string>
                {
                    { "FakeName1", "FakeValue1" },
                    { "FakeName2", "FakeValue2" },
                    { "FakeName3", "FakeValue3" },
                }
            };

            Product result = await _productRepos.Create(entity);

            result.Should().BeEquivalentTo(entity);
        }

        [Fact]
        public async void GeneralRepository_Create_CreateEmployee()
        {
            Employee entity = new()
            {
                Name = "FakeName",
                Segment = (await _handbookRepository.GetAll<WorkshopSegment>()).FirstOrDefault()
            };

            Employee result = await _employeeRepos.Create(entity);

            result.Should().BeEquivalentTo(entity);
        }

        [Fact]
        public async void GeneralRepository_Update_UpdateEmployee()
        {
            Guid employeeGuid = new("bc45539b-585a-454f-824c-3be7eabc6f94");

            Employee entity = await _employeeRepos.GetById(employeeGuid);

            if (entity is null)
            {
                return;
            }

            entity.Name = "Updated FakeName";
            Employee result = await _employeeRepos.Update(entity);

            result.Name.Should().BeEquivalentTo(entity.Name);
        }

        [Fact]
        public async void GeneralRepository_Delete_DeleteEmployee()
        {
            Employee entity = new()
            {
                Name = "FakeName",
                Segment = (await _handbookRepository.GetAll<WorkshopSegment>()).FirstOrDefault()
            };

            await _employeeRepos.Create(entity);
            await _employeeRepos.Delete(entity);
            Employee result = await _employeeRepos.GetById(entity.Id);

            entity.Id.Should().NotBeEmpty();
            result.Should().BeNull();
        }
    }
}