using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class start : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "di_header",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_header", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "di_interval",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start = table.Column<decimal>(type: "numeric", nullable: true),
                    end = table.Column<decimal>(type: "numeric", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_interval", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dir_roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    alias = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dir_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "re_contractors",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<long>(type: "bigint", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_system = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_re_contractors", x => x.id);
                    table.ForeignKey(
                        name: "FK_re_contractors_re_contractors_parent_id",
                        column: x => x.parent_id,
                        principalTable: "re_contractors",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sys_data_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    alias = table.Column<string>(type: "text", nullable: false),
                    is_path = table.Column<char>(type: "character(1)", nullable: true),
                    path = table.Column<string>(type: "text", nullable: true),
                    is_system = table.Column<bool>(type: "boolean", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_data_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sys_field_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    alias = table.Column<string>(type: "text", nullable: false),
                    is_system = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_field_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sys_file_type",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<long>(type: "bigint", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    alias = table.Column<string>(type: "text", nullable: false),
                    is_system = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_file_type", x => x.id);
                    table.ForeignKey(
                        name: "FK_sys_file_type_sys_file_type_parent_id",
                        column: x => x.parent_id,
                        principalTable: "sys_file_type",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sys_formviews",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<long>(type: "bigint", nullable: true),
                    group_id = table.Column<int>(type: "integer", nullable: true),
                    platform_id = table.Column<string>(type: "text", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    alias = table.Column<string>(type: "text", nullable: false),
                    is_system = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_formviews", x => x.id);
                    table.ForeignKey(
                        name: "FK_sys_formviews_sys_formviews_parent_id",
                        column: x => x.parent_id,
                        principalTable: "sys_formviews",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "un_roles_formviews",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    formview_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_un_roles_formviews", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "re_phisycal_persons",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    patronymic = table.Column<string>(type: "text", nullable: true),
                    sex = table.Column<bool>(type: "boolean", nullable: true),
                    birth_day = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_system = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_re_phisycal_persons", x => x.id);
                    table.ForeignKey(
                        name: "FK_re_phisycal_persons_re_contractors_id",
                        column: x => x.id,
                        principalTable: "re_contractors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "re_users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    contractor_id = table.Column<long>(type: "bigint", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_system = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_re_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_re_users_re_contractors_contractor_id",
                        column: x => x.contractor_id,
                        principalTable: "re_contractors",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sys_params",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<string>(type: "text", nullable: false),
                    is_global = table.Column<bool>(type: "boolean", nullable: false),
                    can_edit = table.Column<bool>(type: "boolean", nullable: false),
                    field_type_id = table.Column<long>(type: "bigint", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    alias = table.Column<string>(type: "text", nullable: false),
                    is_system = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_params", x => x.id);
                    table.ForeignKey(
                        name: "FK_sys_params_sys_field_types_field_type_id",
                        column: x => x.field_type_id,
                        principalTable: "sys_field_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "re_files",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    data_type_id = table.Column<long>(type: "bigint", nullable: true),
                    file_type_id = table.Column<long>(type: "bigint", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    element_id = table.Column<string>(type: "text", nullable: false),
                    is_main = table.Column<bool>(type: "boolean", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_system = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_re_files", x => x.id);
                    table.ForeignKey(
                        name: "FK_re_files_sys_data_types_data_type_id",
                        column: x => x.data_type_id,
                        principalTable: "sys_data_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_re_files_sys_file_type_file_type_id",
                        column: x => x.file_type_id,
                        principalTable: "sys_file_type",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "BaseRoleBaseUser",
                columns: table => new
                {
                    RolesId = table.Column<long>(type: "bigint", nullable: false),
                    UsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseRoleBaseUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_BaseRoleBaseUser_dir_roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "dir_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseRoleBaseUser_re_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "re_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "un_users_roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_un_users_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_un_users_roles_dir_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "dir_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_un_users_roles_re_users_user_id",
                        column: x => x.user_id,
                        principalTable: "re_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "di_about",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleId = table.Column<long>(type: "bigint", nullable: true),
                    text = table.Column<string>(type: "text", nullable: true),
                    BackgroundId = table.Column<long>(type: "bigint", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_about", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "di_image",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    main = table.Column<string>(type: "text", nullable: true),
                    preview = table.Column<string>(type: "text", nullable: true),
                    listPlace = table.Column<int>(type: "integer", nullable: true),
                    AboutId = table.Column<long>(type: "bigint", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_image", x => x.id);
                    table.ForeignKey(
                        name: "FK_di_image_di_about_AboutId",
                        column: x => x.AboutId,
                        principalTable: "di_about",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "di_map",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BackgroundId = table.Column<long>(type: "bigint", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_map", x => x.id);
                    table.ForeignKey(
                        name: "FK_di_map_di_image_BackgroundId",
                        column: x => x.BackgroundId,
                        principalTable: "di_image",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "di_title",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImgId = table.Column<long>(type: "bigint", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    titleName = table.Column<string>(type: "text", nullable: true),
                    number = table.Column<string>(type: "text", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_title", x => x.id);
                    table.ForeignKey(
                        name: "FK_di_title_di_image_ImgId",
                        column: x => x.ImgId,
                        principalTable: "di_image",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "di_secondLevel",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HeaderId = table.Column<long>(type: "bigint", nullable: true),
                    BackgroundId = table.Column<long>(type: "bigint", nullable: true),
                    ThirdLevelBackgroundId = table.Column<long>(type: "bigint", nullable: true),
                    MapId = table.Column<long>(type: "bigint", nullable: true),
                    text = table.Column<string>(type: "text", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_secondLevel", x => x.id);
                    table.ForeignKey(
                        name: "FK_di_secondLevel_di_header_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "di_header",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_di_secondLevel_di_image_BackgroundId",
                        column: x => x.BackgroundId,
                        principalTable: "di_image",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_di_secondLevel_di_image_ThirdLevelBackgroundId",
                        column: x => x.ThirdLevelBackgroundId,
                        principalTable: "di_image",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_di_secondLevel_di_map_MapId",
                        column: x => x.MapId,
                        principalTable: "di_map",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "di_horisontal",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    x = table.Column<double>(type: "double precision", nullable: true),
                    y = table.Column<double>(type: "double precision", nullable: true),
                    ImgId = table.Column<long>(type: "bigint", nullable: true),
                    AboutId = table.Column<long>(type: "bigint", nullable: true),
                    text = table.Column<string>(type: "text", nullable: true),
                    SecondLevelId = table.Column<long>(type: "bigint", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_horisontal", x => x.id);
                    table.ForeignKey(
                        name: "FK_di_horisontal_di_about_AboutId",
                        column: x => x.AboutId,
                        principalTable: "di_about",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_di_horisontal_di_image_ImgId",
                        column: x => x.ImgId,
                        principalTable: "di_image",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_di_horisontal_di_secondLevel_SecondLevelId",
                        column: x => x.SecondLevelId,
                        principalTable: "di_secondLevel",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "di_vertical",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IntervalId = table.Column<long>(type: "bigint", nullable: true),
                    HeaderId = table.Column<long>(type: "bigint", nullable: true),
                    MapId = table.Column<long>(type: "bigint", nullable: true),
                    text = table.Column<string>(type: "text", nullable: true),
                    SecondLevelId = table.Column<long>(type: "bigint", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_vertical", x => x.id);
                    table.ForeignKey(
                        name: "FK_di_vertical_di_header_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "di_header",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_di_vertical_di_interval_IntervalId",
                        column: x => x.IntervalId,
                        principalTable: "di_interval",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_di_vertical_di_map_MapId",
                        column: x => x.MapId,
                        principalTable: "di_map",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_di_vertical_di_secondLevel_SecondLevelId",
                        column: x => x.SecondLevelId,
                        principalTable: "di_secondLevel",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseRoleBaseUser_UsersId",
                table: "BaseRoleBaseUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_di_about_BackgroundId",
                table: "di_about",
                column: "BackgroundId");

            migrationBuilder.CreateIndex(
                name: "IX_di_about_TitleId",
                table: "di_about",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_di_horisontal_AboutId",
                table: "di_horisontal",
                column: "AboutId");

            migrationBuilder.CreateIndex(
                name: "IX_di_horisontal_ImgId",
                table: "di_horisontal",
                column: "ImgId");

            migrationBuilder.CreateIndex(
                name: "IX_di_horisontal_SecondLevelId",
                table: "di_horisontal",
                column: "SecondLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_di_image_AboutId",
                table: "di_image",
                column: "AboutId");

            migrationBuilder.CreateIndex(
                name: "IX_di_map_BackgroundId",
                table: "di_map",
                column: "BackgroundId");

            migrationBuilder.CreateIndex(
                name: "IX_di_secondLevel_BackgroundId",
                table: "di_secondLevel",
                column: "BackgroundId");

            migrationBuilder.CreateIndex(
                name: "IX_di_secondLevel_HeaderId",
                table: "di_secondLevel",
                column: "HeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_di_secondLevel_MapId",
                table: "di_secondLevel",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_di_secondLevel_ThirdLevelBackgroundId",
                table: "di_secondLevel",
                column: "ThirdLevelBackgroundId");

            migrationBuilder.CreateIndex(
                name: "IX_di_title_ImgId",
                table: "di_title",
                column: "ImgId");

            migrationBuilder.CreateIndex(
                name: "IX_di_vertical_HeaderId",
                table: "di_vertical",
                column: "HeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_di_vertical_IntervalId",
                table: "di_vertical",
                column: "IntervalId");

            migrationBuilder.CreateIndex(
                name: "IX_di_vertical_MapId",
                table: "di_vertical",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_di_vertical_SecondLevelId",
                table: "di_vertical",
                column: "SecondLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_re_contractors_parent_id",
                table: "re_contractors",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_re_files_data_type_id",
                table: "re_files",
                column: "data_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_re_files_file_type_id",
                table: "re_files",
                column: "file_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_re_users_contractor_id",
                table: "re_users",
                column: "contractor_id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_file_type_parent_id",
                table: "sys_file_type",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_formviews_parent_id",
                table: "sys_formviews",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_params_field_type_id",
                table: "sys_params",
                column: "field_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_un_users_roles_role_id",
                table: "un_users_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_un_users_roles_user_id",
                table: "un_users_roles",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_di_about_di_image_BackgroundId",
                table: "di_about",
                column: "BackgroundId",
                principalTable: "di_image",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_di_about_di_title_TitleId",
                table: "di_about",
                column: "TitleId",
                principalTable: "di_title",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_di_about_di_image_BackgroundId",
                table: "di_about");

            migrationBuilder.DropForeignKey(
                name: "FK_di_title_di_image_ImgId",
                table: "di_title");

            migrationBuilder.DropTable(
                name: "BaseRoleBaseUser");

            migrationBuilder.DropTable(
                name: "di_horisontal");

            migrationBuilder.DropTable(
                name: "di_vertical");

            migrationBuilder.DropTable(
                name: "re_files");

            migrationBuilder.DropTable(
                name: "re_phisycal_persons");

            migrationBuilder.DropTable(
                name: "sys_formviews");

            migrationBuilder.DropTable(
                name: "sys_params");

            migrationBuilder.DropTable(
                name: "un_roles_formviews");

            migrationBuilder.DropTable(
                name: "un_users_roles");

            migrationBuilder.DropTable(
                name: "di_interval");

            migrationBuilder.DropTable(
                name: "di_secondLevel");

            migrationBuilder.DropTable(
                name: "sys_data_types");

            migrationBuilder.DropTable(
                name: "sys_file_type");

            migrationBuilder.DropTable(
                name: "sys_field_types");

            migrationBuilder.DropTable(
                name: "dir_roles");

            migrationBuilder.DropTable(
                name: "re_users");

            migrationBuilder.DropTable(
                name: "di_header");

            migrationBuilder.DropTable(
                name: "di_map");

            migrationBuilder.DropTable(
                name: "re_contractors");

            migrationBuilder.DropTable(
                name: "di_image");

            migrationBuilder.DropTable(
                name: "di_about");

            migrationBuilder.DropTable(
                name: "di_title");
        }
    }
}
