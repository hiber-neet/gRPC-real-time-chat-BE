using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Membership",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    room_id = table.Column<int>(type: "int", nullable: false),
                    role = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true, defaultValue: "member"),
                    joined_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    left_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Membersh__3213E83F2BA1B2A7", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    password_hash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    display_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    avatar_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    is_online = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    last_seen = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__3213E83F81EBD47D", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    room_type = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true, defaultValue: "public"),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    max_members = table.Column<int>(type: "int", nullable: true, defaultValue: 100),
                    is_active = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Room__3213E83F22C05E08", x => x.id);
                    table.ForeignKey(
                        name: "FK__Room__created_by__44FF419A",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    room_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    message_type = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true, defaultValue: "text"),
                    reply_to_id = table.Column<int>(type: "int", nullable: true),
                    is_edited = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Message__3213E83F3BED2D1B", x => x.id);
                    table.ForeignKey(
                        name: "FK__Message__reply_t__5441852A",
                        column: x => x.reply_to_id,
                        principalTable: "Message",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Message__room_id__52593CB8",
                        column: x => x.room_id,
                        principalTable: "Room",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Message__user_id__534D60F1",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "FileUpload",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message_id = table.Column<int>(type: "int", nullable: false),
                    original_filename = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    stored_filename = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    file_path = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    file_size = table.Column<long>(type: "bigint", nullable: false),
                    mime_type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    file_extension = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    upload_status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true, defaultValue: "uploading"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FileUplo__3213E83F81F332DC", x => x.id);
                    table.ForeignKey(
                        name: "FK__FileUploa__messa__59FA5E80",
                        column: x => x.message_id,
                        principalTable: "Message",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileUpload_message_id",
                table: "FileUpload",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "IX_Message_reply_to_id",
                table: "Message",
                column: "reply_to_id");

            migrationBuilder.CreateIndex(
                name: "IX_Message_room_id",
                table: "Message",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_Message_user_id",
                table: "Message",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Room_created_by",
                table: "Room",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__AB6E616441CDE46A",
                table: "Users",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__F3DBC5720871AE2B",
                table: "Users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileUpload");

            migrationBuilder.DropTable(
                name: "Membership");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
