using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddedProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string GetTopMostRatings = @"CREATE OR ALTER PROCEDURE spGetTopMostRatings
                                            AS
                                            BEGIN
                                                SELECT TOP 10 dbo.Media.Id as MediaId, Title, Count(Score) as NumberOfRatings, AVG(CAST(Score as FLOAT)) as AverageRating
                                                FROM dbo.Media
                                                INNER JOIN dbo.Ratings
                                                ON dbo.Media.Id = dbo.Ratings.MediaId
                                                GROUP BY dbo.Media.Id, Title
                                                HAVING Count(Score) >= (SELECT COUNT(Score)
                                                    FROM dbo.Ratings
                                                    INNER JOIN dbo.Media
                                                    ON dbo.Media.Id = dbo.Ratings.MediaId
                                                    GROUP BY dbo.Media.Id
                                                    ORDER BY COUNT(Score) DESC
                                                    OFFSET 10 ROWS
                                                    FETCH NEXT 1 ROWS ONLY)
                                                ORDER BY AVG(Score) DESC
                                            END";

            string GetTopWithMostScreenings = @"CREATE OR ALTER PROCEDURE spGetTopWithMostScreenings @StartDate DATETIME, @EndDate DATETIME
                                                AS
                                                BEGIN
                                                    SELECT TOP 10 dbo.Media.Id AS MediaId, dbo.Media.Title, COUNT(dbo.Screenings.Id) AS ScreeningCount
                                                    FROM dbo.Media
                                                    INNER JOIN dbo.Screenings
                                                    ON dbo.Media.Id = dbo.Screenings.MediaId
                                                    WHERE dbo.Screenings.ScreeningTime BETWEEN @StartDate and @EndDate
                                                    GROUP BY dbo.Media.Id, dbo.Media.Title
                                                    ORDER BY COUNT(dbo.Screenings.Id) DESC
                                                END";

            string GetTopWithMostSoldTicketsWithoutRating = @"CREATE OR ALTER PROCEDURE spGetTopWithMostSoldTicketsWithoutRating
                                                                AS
                                                                BEGIN
                                                                    SELECT dbo.Media.Id AS MediaId, dbo.Media.Title, dbo.Screenings.Id AS ScreeningId, COUNT(dbo.ScreeningUser.ScreeningsId) AS TicketsSold
                                                                    FROM dbo.Media
                                                                    LEFT JOIN dbo.Ratings
                                                                    ON dbo.Media.Id = dbo.Ratings.MediaId
                                                                    INNER JOIN dbo.Screenings
                                                                    ON dbo.Media.Id = dbo.Screenings.MediaId
                                                                    LEFT JOIN dbo.ScreeningUser
                                                                    ON dbo.Screenings.Id = dbo.ScreeningUser.ScreeningsId
                                                                    GROUP BY dbo.Media.Id, dbo.Media.Title, dbo.Screenings.Id
                                                                END";


            migrationBuilder.Sql(GetTopMostRatings);
            migrationBuilder.Sql(GetTopWithMostScreenings);
            migrationBuilder.Sql(GetTopWithMostSoldTicketsWithoutRating);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string GetTopMostRatings = @"DROP PROCEDURE spGetTopMostRatings";
            string GetTopWithMostScreenings = @"DROP PROCEDURE spGetTopWithMostScreenings";
            string GetTopWithMostSoldTicketsWithoutRating = @"DROP PROCEDURE spGetTopWithMostSoldTicketsWithoutRating";

            migrationBuilder.Sql(GetTopMostRatings);
            migrationBuilder.Sql(GetTopWithMostScreenings);
            migrationBuilder.Sql(GetTopWithMostSoldTicketsWithoutRating);
        }
    }
}
