using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAlertRakibFinalProject.Migrations
{
    /// <inheritdoc />
    public partial class Room : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE GetRooms
                    AS
                    BEGIN
                        SELECT 
                            r.RoomId, 
                            r.RoomType, 
                            r.PricePerNight, 
                            r.BedInRoom, 
                            r.RoomNumber, 
                            r.FloorNumber, 
                            r.MaxOccupancy, 
                            r.RoomStatus, 
                            r.HotelId
                        FROM 
                            Rooms r

                        SELECT 
                            rf.RoomFacilityId, 
                            rf.RoomId, 
                            rf.FacilityID, 
                            rf.CreatedOn, 
                            rf.UpdatedOn
                        FROM 
                            RoomFacilities rf

                        SELECT 
                            ri.RoomImageId, 
                            ri.RoomId, 
                            ri.ImageUrl, 
                            ri.ImageResolution, 
                            ri.Caption, 
                            ri.IsThumbnail, 
                            ri.CreatedOn, 
                            ri.UpdatedOn
                        FROM 
                            RoomImages ri
                    END");
            migrationBuilder.Sql(@"CREATE PROCEDURE GetRoomById
                            @id int
                        AS
                        BEGIN
                            SELECT 
                                r.RoomId, 
                                r.RoomType, 
                                r.PricePerNight, 
                                r.BedInRoom, 
                                r.RoomNumber, 
                                r.FloorNumber, 
                                r.MaxOccupancy, 
                                r.RoomStatus, 
                                r.HotelId
                            FROM 
                                Rooms r
                            WHERE 
                                r.RoomId = @id

                            SELECT 
                                rf.RoomFacilityId, 
                                rf.RoomId, 
                                rf.FacilityID, 
                                rf.CreatedOn, 
                                rf.UpdatedOn
                            FROM 
                                RoomFacilities rf
                            WHERE 
                                rf.RoomId = @id

                            SELECT 
                                ri.RoomImageId, 
                                ri.RoomId, 
                                ri.ImageUrl, 
                                ri.ImageResolution, 
                                ri.Caption, 
                                ri.IsThumbnail, 
                                ri.CreatedOn, 
                                ri.UpdatedOn
                            FROM 
                                RoomImages ri
                            WHERE 
                                ri.RoomId = @id
                        END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE GetRooms");
            migrationBuilder.Sql(@"DROP PROCEDURE GetRoomById");
        }
    }
}
