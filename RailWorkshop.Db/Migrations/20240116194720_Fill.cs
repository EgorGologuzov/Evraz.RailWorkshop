using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using RailWorkshop.Services.Entity;

#nullable disable

namespace RailWorkshop.Db.Migrations
{
    public partial class Fill : Migration
    {
        private const string _fillDbConnectionString = "host=localhost;port=5432;database=Evraz_RailWorkshop;username=Evraz;password=12345678";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ClearTables();
            FillTables();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ClearTables();
        }

        private static void ClearTables()
        {
            PostgresContext context = new(_fillDbConnectionString);

            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.Defects)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.RailProfiles)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.SteelGrades)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.Products)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.ConsignmentDefects)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.WorkshopSegments)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.Statements)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.Employees)}\"");
            context.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(context.Consignments)}\"");
        }

        private static void FillTables()
        {
            PostgresContext context = new(_fillDbConnectionString);

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

            Statement statement1 = new()
            {
                Type = StatementType.Credit,
                Date = DateTime.Now,
                Responsible = employee1,
                Segment = segment1,
                Products = new List<Consignment>
                {
                    new() { Stamp = product1.Name, Product = product1, Quantity = 100 },
                    new() { Stamp = product2.Name, Product = product2, Quantity = 50 },
                }
            };

            context.Statements.Add(statement1);

            Statement statement2 = new()
            {
                Type = StatementType.Debit,
                Date = DateTime.Now,
                Responsible = employee2,
                Segment = segment2,
                Products = new List<Consignment>
                {
                    new()
                    {
                        Stamp = product3.Name,
                        Product = product1,
                        Quantity = 1,
                        Defects = new List<ConsignmentDefect>
                        {
                            new() { Defect = defect1, Size = 1.30m }
                        }
                    }
                }
            };

            context.Statements.Add(statement2);

            Statement statement3 = new()
            {
                Type = StatementType.Debit,
                Date = DateTime.Now,
                Responsible = employee1,
                Segment = segment1,
                Products = new List<Consignment>
                {
                    new() { Stamp = product1.Name, Product = product2, Quantity = 500 },
                    new() { Stamp = product2.Name, Product = product3, Quantity = 300 },
                }
            };

            context.Statements.Add(statement3);

            Statement statement4 = new()
            {
                Type = StatementType.Debit,
                Date = DateTime.Now,
                Responsible = employee2,
                Segment = segment2,
                Products = new List<Consignment>
                {
                    new()
                    {
                        Stamp = product3.Name,
                        Product = product1,
                        Quantity = 1,
                        Defects = new List<ConsignmentDefect>
                        {
                            new() { Defect = defect1, Size = 1.30m },
                            new() { Defect = defect2, Size = 3.30m },
                        }
                    }
                }
            };

            context.Statements.Add(statement4);

            context.SaveChanges();
        }
    }
}