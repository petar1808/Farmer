using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.SqlLite
{
    public partial class MigrateFuelExpensesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            ////////
            // Fuel
            ////////
            // Step 1: Create TempExpenses and insert data into Expenses
            migrationBuilder.Sql(@"
                    CREATE TEMP TABLE TempExpenses AS
                    SELECT 
                        Date(t.Date) AS Date,
                        1 AS Type,
                        t.FuelPrice AS PricePerUnit,
                        t.AmountOfFuel AS Quantity,
                        NULL AS ArticleId,
                        s.WorkingSeasonId,
                        t.TenantId 
                    FROM Treatments t
                    JOIN Seedings s ON s.Id = t.SeedingId
                    UNION ALL
                    SELECT 
                        Date(pw.Date) AS Date,
                        1 AS Type,
                        pw.FuelPrice AS PricePerUnit,
                        pw.AmountOfFuel AS Quantity,
                        NULL AS ArticleId,
                        s.WorkingSeasonId,
                        pw.TenantId 
                    FROM PerformedWorks pw 
                    JOIN Seedings s ON s.Id = pw.SeedingId;

                    INSERT INTO Expenses (Date, Type, PricePerUnit, Quantity, Sum, ArticleId, WorkingSeasonId, TenantId)
                    SELECT 
                        t.Date,
                        t.Type,
                        SUM(t.PricePerUnit * t.Quantity) / SUM(t.Quantity) AS PricePerUnit,
                        SUM(t.Quantity) AS Quantity,
                        (SUM(t.PricePerUnit * t.Quantity) / SUM(t.Quantity)) * SUM(t.Quantity) AS Sum,
                        NULL AS ArticleId,
                        t.WorkingSeasonId,
                        t.TenantId
                    FROM TempExpenses t
                    GROUP BY t.Date, t.TenantId, t.WorkingSeasonId;
                ");

            // Step 2: Create TempExpensesByArableLand and insert data into ExpenseByArableLand
            migrationBuilder.Sql(@"
                    CREATE TEMP TABLE TempExpensesByArableLand AS
                    SELECT 
                        ar.Date, 
                        ar.TenantId, 
                        ar.WorkingSeasonId, 
                        ar.ArableLandId,
                        SUM(ar.FuelPrice * ar.AmountOfFuel) / SUM(ar.AmountOfFuel) AS FuelPrice,
                        SUM(ar.AmountOfFuel) AS AmountOfFuel
                    FROM 
                    (
                        SELECT 
                            Date(t.Date) AS Date,
                            t.TenantId,
                            s.WorkingSeasonId,
                            s.ArableLandId,
                            t.FuelPrice AS FuelPrice,
                            t.AmountOfFuel AS AmountOfFuel 
                        FROM Treatments t
                        JOIN Seedings s ON s.Id = t.SeedingId
                        UNION ALL
                        SELECT 
                            Date(pw.Date) AS Date,
                            pw.TenantId,
                            s.WorkingSeasonId,
                            s.ArableLandId,
                            pw.FuelPrice AS FuelPrice,
                            pw.AmountOfFuel AS AmountOfFuel 
                        FROM PerformedWorks pw 
                        JOIN Seedings s ON s.Id = pw.SeedingId
                    ) AS ar
                    GROUP BY ar.Date, ar.TenantId, ar.WorkingSeasonId, ar.ArableLandId;

                    INSERT INTO ExpenseByArableLand (ArableLandId, Sum, ExpenseId, TenantId)
                    SELECT 
                        als.ArableLandId,
                        als.FuelPrice * als.AmountOfFuel AS Sum,
                        e.Id AS ExpenseId,
                        e.TenantId
                    FROM Expenses e
                    JOIN TempExpensesByArableLand als 
                    ON DATE(e.Date) = DATE(als.Date)
                    AND e.TenantId = als.TenantId
                    AND e.WorkingSeasonId = als.WorkingSeasonId
                    AND e.""Type"" = 1
                ");

            // Step 3: Drop temp tables
            migrationBuilder.Sql(@"
                    DROP TABLE TempExpenses;
                    DROP TABLE TempExpensesByArableLand;
                ");

            //////////////////////////
            /// Fertilizers and Pesticides
            /////////////////////////

            migrationBuilder.Sql(@"
                    INSERT INTO Expenses (Date, Type, PricePerUnit, Quantity, Sum, ArticleId, WorkingSeasonId, TenantId)
                    SELECT 
                        Date(t.Date) AS Date,
                        CASE 
                            WHEN t.TreatmentType = 1 THEN 4
                            WHEN t.TreatmentType = 2 THEN 3
                        END as Type,
                        SUM(t.ArticlePrice * t.ArticleQuantity) / SUM(t.ArticleQuantity) AS PricePerUnit,
                        SUM(t.ArticleQuantity * al.SizeInDecar) AS Quantity,
                        (SUM(t.ArticlePrice * t.ArticleQuantity) / SUM(t.ArticleQuantity)) * SUM(t.ArticleQuantity * al.SizeInDecar) AS Sum,
                        t.ArticleId,
                        s.WorkingSeasonId,
                        t.TenantId
                    FROM Treatments t
                    JOIN Seedings s ON s.Id = t.SeedingId
                    JOIN ArableLands al ON al.Id = s.ArableLandId
                    GROUP BY Date(t.Date), t.TenantId, s.WorkingSeasonId, t.ArticleId, t.TreatmentType;
                ");

            // Step 2: Create temporary table and insert data into ExpenseByArableLand table
            migrationBuilder.Sql(@"
                    CREATE TEMP TABLE TempExpensesByArableLand AS
                    SELECT 
                        Date(t.Date) AS Date,
                        CASE 
                            WHEN t.TreatmentType = 1 THEN 4
                            WHEN t.TreatmentType = 2 THEN 3
                        END as Type,
                        SUM(t.ArticlePrice * t.ArticleQuantity) / SUM(t.ArticleQuantity) AS PricePerUnit,
                        SUM(t.ArticleQuantity) * MAX(al.SizeInDecar) AS Quantity,
                        t.ArticleId,
                        s.WorkingSeasonId,
                        t.TenantId,
                        s.ArableLandId
                    FROM Treatments t
                    JOIN Seedings s ON s.Id = t.SeedingId
                    JOIN ArableLands al ON al.Id = s.ArableLandId
                    GROUP BY Date(t.Date), t.TenantId, s.WorkingSeasonId, t.ArticleId, t.TreatmentType, s.ArableLandId;
                ");

            // Step 3: Insert into ExpenseByArableLand from the temporary table
            migrationBuilder.Sql(@"
                    INSERT INTO ExpenseByArableLand (ArableLandId, Sum, ExpenseId, TenantId)
                    SELECT 
                        als.ArableLandId,
                        als.PricePerUnit * als.Quantity AS Sum,
                        e.Id AS ExpenseId,
                        e.TenantId
                    FROM Expenses e
                    JOIN TempExpensesByArableLand als 
                    ON DATE(e.Date) = DATE(als.Date)
                    AND e.TenantId = als.TenantId
                    AND e.WorkingSeasonId = als.WorkingSeasonId
                    AND e.""Type"" = als.""Type""
                    AND e.ArticleId = als.ArticleId
                    AND e.""Type"" in (4, 3)
                ");

            // Step 4: Drop the temporary table
            migrationBuilder.Sql(@"
                DROP TABLE TempExpensesByArableLand;
            ");


            //////////////////////////
            /// Harvest
            /////////////////////////
            // Step 1: Insert data into Expenses table
            migrationBuilder.Sql(@"
                    INSERT INTO Expenses(Date, ""Type"", PricePerUnit, Quantity, Sum, ArticleId, WorkingSeasonId, TenantId)
                    SELECT 
                        DATE(ws.EndDate),
                        7 as Type,
                        SUM(s.ExpensesForHarvesting) as PricePerUnit,
                        1 as Quantity,
                        SUM(s.ExpensesForHarvesting) as Sum,
                        NULL as ArticleId,
                        s.WorkingSeasonId,
                        s.TenantId 
                    FROM Seedings s 
                    JOIN WorkingSeasons ws ON ws.Id = s.WorkingSeasonId
                    GROUP BY s.WorkingSeasonId, s.TenantId, DATE(ws.EndDate)
                    HAVING SUM(s.ExpensesForHarvesting) > 0;
                ");

            // Step 2: Insert data into ExpenseByArableLand table
            migrationBuilder.Sql(@"
                    INSERT INTO ExpenseByArableLand (ArableLandId, Sum, ExpenseId, TenantId)
                    SELECT 
                        sor.ArableLandId,
                        sor.Sum,
                        e.Id,
                        e.TenantId 
                    FROM
                    (
                        SELECT 
                            DATE(ws.EndDate) as Date,
                            s.ArableLandId,
                            SUM(s.ExpensesForHarvesting) as Sum,
                            s.TenantId,
                            s.WorkingSeasonId
                        FROM  Seedings s 
                        JOIN WorkingSeasons ws ON ws.Id = s.WorkingSeasonId
                        GROUP BY s.WorkingSeasonId, s.TenantId, DATE(ws.EndDate), s.ArableLandId 
                        HAVING SUM(s.ExpensesForHarvesting) > 0
                    ) as sor
                    JOIN Expenses e  
                    ON DATE(e.Date) = DATE(sor.Date)
                    AND e.TenantId = sor.TenantId
                    AND e.WorkingSeasonId = sor.WorkingSeasonId
                    AND e.""Type"" = 7
                ");

            //////////////////////////
            /// Seeds
            /////////////////////////

            // Step 1: Insert data into the Expenses table for seed expenses
            migrationBuilder.Sql(@"
                    INSERT INTO Expenses(Date, ""Type"", PricePerUnit, Quantity, Sum, ArticleId, WorkingSeasonId, TenantId)
                    SELECT 
                        DATE(ws.StartDate),
                        5 as ""Type"",
                        SUM(s.SeedsPricePerKilogram *(s.SeedsQuantityPerDecare * al.SizeInDecar)) / SUM(s.SeedsQuantityPerDecare * al.SizeInDecar)  as PricePerUnit,
                        SUM(s.SeedsQuantityPerDecare * al.SizeInDecar) as Quantity,
                        (SUM(s.SeedsPricePerKilogram *(s.SeedsQuantityPerDecare * al.SizeInDecar)) / SUM(s.SeedsQuantityPerDecare * al.SizeInDecar)) * (SUM(s.SeedsQuantityPerDecare * al.SizeInDecar)) as Sum,
                        s.ArticleId,
                        s.WorkingSeasonId,
                        s.TenantId 
                    FROM  Seedings s 
                    JOIN WorkingSeasons ws ON ws.Id = s.WorkingSeasonId
                    JOIN ArableLands al ON al.Id = s.ArableLandId 
                    GROUP BY s.WorkingSeasonId, s.TenantId, DATE(ws.StartDate), s.ArticleId  
                    HAVING SUM(s.SeedsPricePerKilogram) * SUM(s.SeedsQuantityPerDecare * al.SizeInDecar) > 0;
                ");

            // Step 2: Insert data into ExpenseByArableLand for the related seed expenses
            migrationBuilder.Sql(@"
                INSERT INTO ExpenseByArableLand (ArableLandId, Sum, ExpenseId, TenantId)
                SELECT 
                    sor.ArableLandId,
                    sor.Sum,
                    e.Id,
                    e.TenantId 
                FROM
                (
                    SELECT 
                        DATE(ws.StartDate) as Date,
                        s.ArableLandId,
                        SUM(s.SeedsPricePerKilogram) * SUM(s.SeedsQuantityPerDecare * al.SizeInDecar) as Sum,
                        s.TenantId,
                        s.WorkingSeasonId,
                        s.ArticleId
                    FROM  Seedings s 
                    JOIN WorkingSeasons ws ON ws.Id = s.WorkingSeasonId
                    JOIN ArableLands al ON al.Id = s.ArableLandId 
                    GROUP BY s.WorkingSeasonId, s.TenantId, DATE(ws.StartDate), s.ArticleId, s.ArableLandId  
                    HAVING SUM(s.SeedsPricePerKilogram) * SUM(s.SeedsQuantityPerDecare * al.SizeInDecar) > 0
                ) as sor
                JOIN Expenses e  
                ON DATE(e.Date) = DATE(sor.Date)
                AND e.TenantId = sor.TenantId
                AND e.WorkingSeasonId = sor.WorkingSeasonId
    	        AND e.ArticleId = sor.ArticleID
    	        AND e.""Type"" = 5;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
