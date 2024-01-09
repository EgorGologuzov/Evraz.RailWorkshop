using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using RailWorkshop.Services.Entity;

#nullable disable

namespace RailWorkshop.Db.Migrations
{
    public partial class Fill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ClearTables();
            FillTables();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }

        private static void ClearTables()
        {
            PostgresContext context = new();

            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.Defects)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.RailProfiles)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.SteelGrades)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.Products)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.ProductDefects)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.WorkshopSegments)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.Statements)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.Employees)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.SegmentAccounts)}\"");
        }

        private static void FillTables()
        {
            PostgresContext context = new();

            Defect defect1 = new() { Name = "Трещина" };
            context.Defects.Add(defect1);

            Defect defect2 = new() { Name = "Скол" };
            context.Defects.Add(defect2);

            RailProfile profile1 = new() { Name = "Т58" };
            context.RailProfiles.Add(profile1);

            RailProfile profile2 = new() { Name = "Р65" };
            context.RailProfiles.Add(profile2);

            SteelGrade steel1 = new() { Name= "Ст 3" };
            context.SteelGrades.Add(steel1);

            SteelGrade steel2 = new() { Name= "Ст 4" };
            context.SteelGrades.Add(steel2);

            WorkshopSegment segment1 = new() { Name = "Разгрузка поставок - Приемка"};
            context.WorkshopSegments.Add(segment1);

            WorkshopSegment segment2 = new() { Name = "Разгрузка поставок - Склад"};
            context.WorkshopSegments.Add(segment2);

            WorkshopSegment segment3 = new() { Name = "Печь - Приемка" };
            context.WorkshopSegments.Add(segment3);

            WorkshopSegment segment4 = new() { Name = "Печь - Склад"        };
            context.WorkshopSegments.Add(segment4);

            WorkshopSegment segment5 = new() { Name = "Прокатный стан - Приемка" };
            context.WorkshopSegments.Add(segment5);

            WorkshopSegment segment6 = new() { Name = "Холодильник - Приемка" };
            context.WorkshopSegments.Add(segment6);

            WorkshopSegment segment7 = new() { Name = "Холодильник - Склад" };
            context.WorkshopSegments.Add(segment7);

            WorkshopSegment segment8 = new() { Name = "Контроль качества - Приемка" };
            context.WorkshopSegments.Add(segment8);

            WorkshopSegment segment9 = new() { Name = "Контроль качества - Склад" };
            context.WorkshopSegments.Add(segment9);

            WorkshopSegment segment10 = new() { Name = "Погрузка продукции - Приемка" };
            context.WorkshopSegments.Add(segment10);

            WorkshopSegment segment11 = new() { Name = "Погрузка продукции - Склад" };
            context.WorkshopSegments.Add(segment11);

            SegmentAccount segmentAccount1 = new() { Segment = segment1, Products = new Dictionary<Guid, decimal>() };
            context.SegmentAccounts.Add(segmentAccount1);

            SegmentAccount segmentAccount2 = new() { Segment = segment2, Products = new Dictionary<Guid, decimal>() };
            context.SegmentAccounts.Add(segmentAccount2);

            SegmentAccount segmentAccount3 = new() { Segment = segment3, Products = new Dictionary<Guid, decimal>() };
            context.SegmentAccounts.Add(segmentAccount3);

            SegmentAccount segmentAccount4 = new() { Segment = segment4, Products = new Dictionary<Guid, decimal>() };
            context.SegmentAccounts.Add(segmentAccount4);

            SegmentAccount segmentAccount5 = new() { Segment = segment5, Products = new Dictionary<Guid, decimal>() };
            context.SegmentAccounts.Add(segmentAccount5);

            SegmentAccount segmentAccount6 = new() { Segment = segment6, Products = new Dictionary<Guid, decimal>() };
            context.SegmentAccounts.Add(segmentAccount6);

            SegmentAccount segmentAccount7 = new() { Segment = segment7, Products = new Dictionary<Guid, decimal>() };
            context.SegmentAccounts.Add(segmentAccount7);

            SegmentAccount segmentAccount8 = new() { Segment = segment8, Products = new Dictionary<Guid, decimal>() };
            context.SegmentAccounts.Add(segmentAccount8);

            SegmentAccount segmentAccount9 = new() { Segment = segment9, Products = new Dictionary<Guid, decimal>() };
            context.SegmentAccounts.Add(segmentAccount9);

            SegmentAccount segmentAccount10 = new() { Segment = segment10, Products = new Dictionary<Guid, decimal>() };
            context.SegmentAccounts.Add(segmentAccount10);

            SegmentAccount segmentAccount11 = new() { Segment = segment11, Products = new Dictionary<Guid, decimal>() };
            context.SegmentAccounts.Add(segmentAccount11);

            Employee employee1 = new() { Name = "Антонов Антон Антонович", Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", Segment = segment1 };
            context.Employees.Add(employee1);

            Employee employee2 = new() { Name = "Борисов Борис Борисович", Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", Segment = segment2 };
            context.Employees.Add(employee2);

            Employee employee3 = new() { Name = "Васильев Василий Васильевич", Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", Segment = segment3 };
            context.Employees.Add(employee3);

            Employee employee4 = new() { Name = "Григорьев Григорий Григорьевич", Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", Segment = segment4 };
            context.Employees.Add(employee4);

            Employee employee5 = new() { Name = "Дмитриев Дмитрий Дмитриевич", Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", Segment = segment5 };
            context.Employees.Add(employee5);

            Employee employee6 = new() { Name = "Егоров Егор Егорович", Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", Segment = segment6 };
            context.Employees.Add(employee6);

            Employee employee7 = new() { Name = "Жаннова Жанна Жановна", Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", Segment = segment7 };
            context.Employees.Add(employee7);

            Employee employee8 = new() { Name = "Игорев Игорь Игоревич", Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", Segment = segment8 };
            context.Employees.Add(employee8);

            Employee employee9 = new() { Name = "Константинов Константин Константинович", Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", Segment = segment9 };
            context.Employees.Add(employee9);

            Employee employee10 = new() { Name = "Леонидов Леонид Леонидович", Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", Segment = segment10 };
            context.Employees.Add(employee10);

            Employee employee11 = new() { Name = "Максимов Максим Максимович", Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", Segment = segment11 };
            context.Employees.Add(employee11);

            Product product1 = new()
            {
                Name = "37843А349",
                Profile = profile1,
                Steel = steel1,
                Properties = new Dictionary<string, string>
                {
                    { "ГОСТ", "ГОСТ 16210-77" },
                    { "Масса 1 м рельса, кг", "74,4" },
                    { "Высота рельса, мм", "192" },
                }
            };
            context.Products.Add(product1);

            Product product2 = new()
            {
                Name = "17543В349",
                Profile = profile2,
                Steel = steel2,
                Properties = new Dictionary<string, string>
                {
                    { "ГОСТ", "ГОСТ 8161-75" },
                    { "Масса 1 м рельса, кг", "64,72" },
                    { "Высота рельса, мм", "180" },
                }
            };
            context.Products.Add(product2);

            Product product3 = new()
            {
                Name = "90843Д301",
                Profile = profile1,
                Steel = steel2,
                Properties = new Dictionary<string, string>
                {
                    { "ГОСТ", "ГОСТ 7174-75" },
                    { "Масса 1 м рельса, кг", "51,67" },
                    { "Высота рельса, мм", "152" },
                }
            };
            context.Products.Add(product3);

            ProductDefect productDefect1 = new()
            {
                Product = product1,
                Defect = defect1,
                Quantity = 1m,
                Size = 1.5m
            };
            context.ProductDefects.Add(productDefect1);

            ProductDefect productDefect2 = new()
            {
                Product = product2,
                Defect = defect2,
                Quantity = 2m,
                Size = 3m
            };
            context.ProductDefects.Add(productDefect2);

            ProductDefect productDefect3 = new()
            {
                Product = product3,
                Defect = defect1,
                Quantity = 1.8904m,
                Size = 9.9034m
            };
            context.ProductDefects.Add(productDefect3);

            context.SaveChanges();

            List<Product> products = context.Products.ToList();
            product1 = products[0];
            product2 = products[1];
            product3 = products[2];

            Statement statement1 = new()
            {
                Type = StatementType.Debit,
                Date = DateTime.Now,
                Products = new Dictionary<Guid, decimal> { { product1.Id, 500.9m }, { product2.Id, 367.89m } },
                Responsible = employee1,
                Segment = segment1
            };
            context.Statements.Add(statement1);

            Statement statement2 = new()
            {
                Type = StatementType.Credit,
                Date = DateTime.Now,
                Products = new Dictionary<Guid, decimal> { { product2.Id, 1100m }, { product3.Id, 367.89m } },
                Responsible = employee2,
                Segment = segment2
            };
            context.Statements.Add(statement2);

            Statement statement3 = new()
            {
                Type = StatementType.Debit,
                Date = DateTime.Now,
                Products = new Dictionary<Guid, decimal>
                {
                    { product1.Id, 100.9m },
                    { product2.Id, 907.89m },
                    { product3.Id, 478.984543m}
                },
                Responsible = employee3,
                Segment = segment3
            };
            context.Statements.Add(statement3);

            context.SaveChanges();
        }
    }
}