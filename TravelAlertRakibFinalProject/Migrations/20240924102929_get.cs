using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAlertRakibFinalProject.Migrations
{
    /// <inheritdoc />
    public partial class get : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE GetHotels
                            AS
                            BEGIN
                               SELECT 
                                    h.HotelId, 
                                    h.HotelName, 
                                    h.Description, 
                                    h.StarRating, 
                                    h.Address, 
                                    h.ContactInfo, 
                                    h.HotelCode, 
                                    h.AverageRoomRate, 
                                    h.LocationID
                                FROM 
                                    Hotels h

                                SELECT 
                                    hf.HotelFacilityId, 
                                    hf.HotelId, 
                                    hf.FacilityID, 
                                    hf.CreatedOn, 
                                    hf.UpdatedOn
                                FROM 
                                    HotelFacilities hf

                                SELECT 
                                    hi.HotelImageId, 
                                    hi.HotelId, 
                                    hi.ImageUrl, 
                                    hi.ImageResolution, 
                                    hi.Caption, 
                                    hi.IsThumbnail, 
                                    hi.CreatedOn, 
                                    hi.UpdatedOn
                                FROM 
                                    HotelImages hi
                            END");

            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE GetHotelById
                                @id int
                            AS
                            BEGIN
                                SELECT 
                                    h.HotelId, 
                                    h.HotelName, 
                                    h.Description, 
                                    h.StarRating, 
                                    h.Address, 
                                    h.ContactInfo, 
                                    h.HotelCode, 
                                    h.AverageRoomRate, 
                                    h.LocationID
                                FROM 
                                    Hotels h
                                WHERE 
                                    h.HotelId = @id

                                SELECT 
                                    hf.HotelFacilityId, 
                                    hf.HotelId, 
                                    hf.FacilityID, 
                                    hf.CreatedOn, 
                                    hf.UpdatedOn
                                FROM 
                                    HotelFacilities hf
                                WHERE 
                                    hf.HotelId = @id

                                SELECT 
                                    hi.HotelImageId, 
                                    hi.HotelId, 
                                    hi.ImageUrl, 
                                    hi.ImageResolution, 
                                    hi.Caption, 
                                    hi.IsThumbnail, 
                                    hi.CreatedOn, 
                                    hi.UpdatedOn
                                FROM 
                                    HotelImages hi
                                WHERE 
                                    hi.HotelId = @id
                            END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE GetHotels");
            migrationBuilder.Sql(@"DROP PROCEDURE GetHotelById");
        }
    }
}
