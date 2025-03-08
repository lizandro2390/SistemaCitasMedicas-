using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaCitasMedicas.Migrations
{
    /// <inheritdoc />
    public partial class AddCitaDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_AspNetUsers_PacienteId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Doctors_DoctorId",
                table: "Citas");

            migrationBuilder.AddColumn<string>(
                name: "Motivo",
                table: "Citas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notas",
                table: "Citas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_AspNetUsers_PacienteId",
                table: "Citas",
                column: "PacienteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Doctors_DoctorId",
                table: "Citas",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_AspNetUsers_PacienteId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Doctors_DoctorId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "Motivo",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "Notas",
                table: "Citas");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_AspNetUsers_PacienteId",
                table: "Citas",
                column: "PacienteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Doctors_DoctorId",
                table: "Citas",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
