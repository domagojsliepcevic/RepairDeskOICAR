using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairDesk.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "POSes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSpecial = table.Column<bool>(type: "bit", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepairStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    POSId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_POSes_POSId",
                        column: x => x.POSId,
                        principalTable: "POSes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepairNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    POSId = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repairs_POSes_POSId",
                        column: x => x.POSId,
                        principalTable: "POSes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Repairs_RepairStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "RepairStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Repairs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "AddedAt", "Description", "ModifiedAt", "Name" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MESSAGE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MESSAGE" });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "AddedAt", "Description", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORDER_RECEIVED", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORDER_RECEIVED" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IN_PROGRESS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IN_PROGRESS" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DONE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DONE" }
                });

            migrationBuilder.InsertData(
                table: "POSes",
                columns: new[] { "Id", "AddedAt", "Location", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "web", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Web Shop" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "shop1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trgovina A" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "AddedAt", "Description", "ImagePath", "IsSpecial", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smartphones - description", "smartphones.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smartphones" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Accessories - description", "accessories.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Accessories" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tablets - description", "tablets.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tablets" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New - description", "newarrival.webp", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Featured - description", "featured.png", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Featured" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laptops - description", "laptops.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laptops" },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Earphones - description", "earphones.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Earphones" },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smartwatches - description", "smartwatches.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smartwatches" }
                });

            migrationBuilder.InsertData(
                table: "RepairStatuses",
                columns: new[] { "Id", "AddedAt", "Description", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "REQUEST_RECEIVED", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "REQUEST_RECEIVED" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IN_PROGRESS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IN_PROGRESS" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DONE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DONE" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "AddedAt", "Description", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADMIN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADMIN" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EMPLOYEE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EMPLOYEE" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BUYER", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BUYER" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VISITOR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VISITOR" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedAt", "Brand", "CategoryId", "Description", "ImagePath", "LongDescription", "ModifiedAt", "Name", "Price", "Quantity", "Rating" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple", 1, "Latest iPhone with a 6.1-inch Super Retina XDR display and A15 Bionic chip.", "iphone13.jpg", "1. Display:\n	- Size: 6.1 inches (iPhone 13), 6.7 inches (iPhone 13 Pro/Pro Max), 5.4 inches (iPhone 13 Mini)\n	- Type: Super Retina XDR OLED\n	- Resolution: 2532 x 1170 pixels (iPhone 13/Pro), 2778 x 1284 pixels (iPhone 13 Pro Max), 2340 x 1080 pixels (iPhone 13 Mini)\n	- Refresh Rate: 120Hz (Pro models only)\n2. Processor:\n	- Apple A15 Bionic chip with 6-core CPU and 4-core GPU\n3. Storage Options:\n	- 128GB, 256GB, 512GB, and 1TB (Pro models)\n	- 128GB, 256GB, 512GB (iPhone 13/Mini)\n4. RAM:\n	- Not officially disclosed by Apple, but estimated to be 4GB (iPhone 13/Mini) and 6GB (Pro models)\n5. Rear Camera:\n	- Dual-camera system (iPhone 13/Mini):\n		- 12MP Wide camera with f/1.6 aperture\n		- 12MP Ultra Wide camera with f/2.4 aperture and 120-degree field of view\n	- Triple-camera system (iPhone 13 Pro/Pro Max):\n		- 12MP Wide camera with f/1.5 aperture\n		- 12MP Ultra Wide camera with f/1.8 aperture and 120-degree field of view\n		- 12MP Telephoto camera with f/2.8 aperture (Pro models only)\n6. Front Camera:\n	- 12MP TrueDepth camera with f/2.2 aperture\n7. Battery:\n	- Built-in rechargeable lithium-ion battery\n	- Specific battery capacity not disclosed by Apple\n	- iPhone 13 Mini: Up to 1.5 hours longer battery life than iPhone 12 Mini\n	- iPhone 13/Pro/Pro Max: Up to 2.5 hours longer battery life than iPhone 12/12 Pro/12 Pro Max\n8. Operating System:\n	- iOS 15 (latest version at the time of release)\n9. Connectivity:\n	- 5G capable\n	- Dual SIM (Nano-SIM and eSIM)\n	- Lightning connector\n10. Biometric Authentication:\n	- Face ID facial recognition\n11. Other Features:\n	- IP68 water and dust resistance\n	- Ceramic Shield front cover for improved durability\n	- MagSafe magnetic accessory support", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple iPhone 13", 799.99m, 4, 3 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung", 1, "Powerful Android smartphone with a 6.2-inch Dynamic AMOLED 2X display.", "samsungs21.jpg", "1. Display:\n	- Size: 6.2 inches\n	- Type: Dynamic AMOLED 2X\n	- Resolution: 3200 x 1440 pixels\n	- Refresh Rate: 120Hz\n2. Processor:\n	- Qualcomm Snapdragon 888 (USA/China) or Exynos 2100 (Global)\n3. Storage Options:\n	- 128GB, 256GB, 512GB\n4. RAM:\n	- 8GB\n5. Rear Camera:\n	- Triple-camera system:\n		- 12MP Wide camera with f/1.8 aperture\n		- 12MP Ultra Wide camera with f/2.2 aperture\n		- 64MP Telephoto camera with f/2.0 aperture\n6. Front Camera:\n	- 10MP selfie camera with f/2.2 aperture\n7. Battery:\n	- 4000mAh\n8. Operating System:\n	- Android 11 with Samsung One UI 3.1\n9. Connectivity:\n	- 5G capable\n	- Dual SIM (Nano-SIM and eSIM)\n	- USB Type-C\n10. Biometric Authentication:\n	- Ultrasonic Fingerprint Sensor\n	- Facial Recognition (2D)\n11. Other Features:\n	- IP68 water and dust resistance\n	- Wireless charging support\n	- Samsung DeX support\n	- NFC support", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung Galaxy S21", 749.99m, 7, 3 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Google", 1, "Sleek and advanced smartphone with a 6.4-inch AMOLED display and Google Tensor chip.", "pixel6.png", "1. Display:\n	- Size: 6.4 inches\n	- Type: OLED\n	- Resolution: 2400 x 1080 pixels\n	- Refresh Rate: 90Hz\n2. Processor:\n	- Google Tensor chip\n3. Storage Options:\n	- 128GB, 256GB\n4. RAM:\n	- 8GB\n5. Rear Camera:\n	- Dual-camera system:\n		- 50MP Wide camera with f/1.85 aperture\n		- 12MP Ultra Wide camera with f/2.2 aperture\n6. Front Camera:\n	- 8MP selfie camera with f/2.0 aperture\n7. Battery:\n	- 4614mAh\n8. Operating System:\n	- Android 12\n9. Connectivity:\n	- 5G capable\n	- Dual SIM (Nano-SIM and eSIM)\n	- USB Type-C\n10. Biometric Authentication:\n	- Fingerprint Sensor (under-display)\n11. Other Features:\n	- IP68 water and dust resistance\n	- Titan M2 security chip\n	- Google Assistant\n	- Pixel Neural Core\n	- Wireless charging support", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Google Pixel 6", 599.99m, 2, 3 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple", 8, "Smartwatch with a larger Always-On Retina display and new watch faces.", "applewatchs7.jpg", "1. Display:\n	- Size: 41mm and 45mm\n	- Type: LTPO OLED\n	- Resolution: 396 x 484 pixels (41mm), 428 x 520 pixels (45mm)\n	- Always-On Retina display\n2. Processor:\n	- Apple S7 chip\n3. Storage Capacity:\n	- Not officially disclosed by Apple\n4. Battery Life:\n	- Up to 18 hours\n5. Sensors:\n	- Electrical heart sensor (ECG)\n	- Optical heart sensor\n	- Blood oxygen sensor (SpO2)\n	- Accelerometer\n	- Gyroscope\n	- Compass\n	- Always-on altimeter\n6. Water Resistance:\n	- WR50 (50 meters water resistance)\n7. Operating System:\n	- watchOS 8\n8. Connectivity:\n	- Wi-Fi (802.11b/g/n/ac/ax)\n	- Bluetooth 5.0\n	- NFC\n9. Cellular Connectivity:\n	- Available in cellular and GPS only models\n	- Cellular models support LTE and UMTS\n10. Bands and Compatibility:\n	- Compatible with standard 18mm, 20mm, 22mm, or 24mm watch bands\n11. Other Features:\n	- Digital Crown with haptic feedback\n	- Built-in GPS\n	- Emergency SOS\n	- Fall detection\n	- Sleep tracking\n	- Fitness tracking\n	- Elevation tracking", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple Watch Series 7", 399.99m, 5, 3 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple", 7, "Noise-cancelling earbuds with immersive sound and transparency mode.", "appleairpodspro.jpg", "1. Design:\n	- In-ear\n	- Active Noise Cancellation\n	- Transparency mode\n2. Connectivity:\n	- Bluetooth 5.0\n	- Apple H1 chip\n3. Audio Technology:\n	- Adaptive EQ\n	- Custom high-excursion Apple driver\n	- Dual beamforming microphones\n	- Dual optical sensors\n	- Inward-facing microphone\n	- Spatial audio with dynamic head tracking\n4. Controls:\n	- Force sensor\n	- Press once to play, pause, or answer a phone call\n	- Press twice to skip forward\n	- Press three times to skip back\n	- Press and hold for switching between Active Noise Cancellation and Transparency mode\n5. Sensors:\n	- Dual beamforming microphones\n	- Dual optical sensors\n	- Inward-facing microphone\n	- Motion-detecting accelerometer\n	- Speech-detecting accelerometer\n6. Battery Life:\n	- Up to 4.5 hours of listening time (on one charge)\n	- Up to 5 hours of listening time (with Active Noise Cancellation and Transparency mode off)\n	- Up to 3.5 hours of talk time\n	- More than 24 hours of listening time (with multiple charges from the wireless charging case)\n7. Water and Sweat Resistance:\n	- IPX4\n8. Compatibility:\n	- iPhone, iPad, and iPod touch models with iOS 15 or later\n	- Apple Watch models with watchOS 8 or later\n	- Mac models with macOS Monterey or later\n	- Apple TV models with tvOS 15 or later", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple AirPods Pro", 249.99m, 8, 3 },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung", 7, "Wireless earbuds with immersive sound and active noise cancellation.", "samsunggalaxybudspro.jpg", "1. Design:\n	- In-ear\n	- Ergonomic design\n	- Adjustable ear tips\n	- Available in multiple colors\n2. Connectivity:\n	- Bluetooth 5.0\n	- Wireless charging case\n3. Audio Technology:\n	- Dual speakers (11mm woofer + 6.5mm tweeter)\n	- 360-degree surround sound\n	- Active Noise Cancellation\n	- Ambient Sound\n	- Voice Detect\n4. Controls:\n	- Touch controls on each earbud\n	- Tap to play, pause, answer calls, or skip tracks\n	- Long press to activate voice assistant\n5. Microphones:\n	- 3 microphones (2 outer, 1 inner)\n	- Voice Pickup Unit\n	- Wind Shield technology\n6. Battery Life:\n	- Up to 5 hours of playback time (ANC on)\n	- Up to 8 hours of playback time (ANC off)\n	- Up to 18 hours of total playback time with the charging case\n	- Quick charging: 1 hour of playback with 5 minutes of charging\n7. Water and Sweat Resistance:\n	- IPX7\n8. Compatibility:\n	- Android and iOS devices with Bluetooth connectivity\n	- Samsung Galaxy Wearable app\n	- One UI 3.1 or later for certain features\n9. Other Features:\n	- 360 Audio\n	- Game Mode\n	- Bixby voice wake-up\n	- Find My Earbuds\n	- Wireless PowerShare\n	- Auto Switch", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung Galaxy Buds Pro", 199.99m, 3, 3 },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mophie", 2, "Fast and convenient wireless charging pad for compatible devices.", "mophie.jpg", "1. Compatibility:\n	- Qi-enabled devices\n2. Charging Technology:\n	- Wireless charging\n3. Charging Speed:\n	- Up to 7.5W charging power\n4. Design:\n	- Sleek and compact design\n	- Non-slip charging surface\n	- LED indicator\n5. Charging Indicators:\n	- LED light to indicate charging status\n	- Solid white light when charging\n	- No light when not charging\n6. Charging Modes:\n	- Supports both standard charging and fast wireless charging\n7. Safety Features:\n	- Foreign object detection\n	- Over-temperature protection\n	- Over-voltage protection\n	- Over-current protection\n	- Short circuit protection\n8. Power Input:\n	- USB Type-A\n	- Input Voltage: 5V/2A or 9V/1.67A\n9. Dimensions:\n	- Width: 3.82 inches (97 mm)\n	- Height: 0.47 inches (12 mm)\n	- Length: 3.82 inches (97 mm)\n10. Weight:\n	- 4.23 ounces (120 grams)\n11. Package Contents:\n	- Mophie Wireless Charging Pad\n	- Quick Start Guide\n	- USB Type-A to micro-USB cable", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mophie Wireless Charging Pad", 59.99m, 6, 3 },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Belkin", 2, "High-capacity power bank with 20,000mAh battery for charging on the go.", "belkinpowerbank.jpg", "1. Capacity:\n	- Available in multiple capacities (e.g., 10,000mAh, 20,000mAh)\n2. Charging Technology:\n	- USB-C and USB-A ports\n	- Power Delivery (PD) technology\n	- Quick Charge 3.0\n3. Power Output:\n	- USB-C Output: Up to 30W\n	- USB-A Output: Up to 12W\n4. Power Input:\n	- USB-C Input: Up to 30W\n5. Compatibility:\n	- Supports a wide range of devices including smartphones, tablets, and other USB-powered devices\n6. Design:\n	- Compact and portable design\n	- Durable build quality\n	- LED indicators to display battery level\n7. Safety Features:\n	- Over-current protection\n	- Over-voltage protection\n	- Over-temperature protection\n	- Short circuit protection\n	- Surge protection\n8. Dimensions:\n	- Varies based on the specific model\n9. Weight:\n	- Varies based on the specific model\n10. Package Contents:\n	- Belkin BOOSTCHARGE Power Bank\n	- USB-C to USB-C cable (varies based on the model)\n	- User manual", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Belkin BOOSTCHARGE Power Bank", 79.99m, 9, 3 },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung", 3, "Premium Android tablet with a 11-inch display and S Pen included.", "samsungtabs7.jpeg", "1. Display:\n	- Size: 11 inches\n	- Resolution: 2560 x 1600 pixels\n	- Technology: TFT\n	- HDR10+ support\n2. Processor:\n	- Qualcomm Snapdragon 865+\n	- Octa-core (1x3.09 GHz Kryo 585 & 3x2.42 GHz Kryo 585 & 4x1.8 GHz Kryo 585)\n3. Memory:\n	- RAM: 6GB, 8GB (depending on the variant)\n	- Internal Storage: 128GB, 256GB, 512GB (expandable up to 1TB with microSD)\n4. Operating System:\n	- Android 10 with One UI 3.1\n5. Cameras:\n	- Rear Camera: 13 MP (wide), 5 MP (ultrawide)\n	- Front Camera: 8 MP (wide)\n	- Video Recording: Up to 4K @ 30fps\n6. Battery:\n	- Capacity: 8,000 mAh\n	- Fast Charging Support\n7. S Pen Support:\n	- Included in the package\n	- 9ms latency\n	- Bluetooth connectivity for remote control features\n8. Connectivity:\n	- Wi-Fi 802.11 a/b/g/n/ac/6\n	- Bluetooth 5.0\n	- USB Type-C 3.2\n	- GPS\n9. Dimensions:\n	- Height: 253.8 mm\n	- Width: 165.3 mm\n	- Depth: 6.3 mm\n	- Weight: 498 grams (Wi-Fi), 500 grams (LTE)\n10. Colors:\n	- Mystic Black, Mystic Bronze, Mystic Silver\n11. Other Features:\n	- AKG-tuned quad speakers\n	- Dolby Atmos support\n	- Fingerprint sensor (side-mounted)\n	- Face recognition", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung Galaxy Tab S7", 649.99m, 1, 3 },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dell", 6, "The Dell XPS 13 is a highly regarded laptop that combines sleek design, impressive performance, and a stunning display. With its thin bezels, compact form factor, powerful Intel processors, and vibrant screen options, the XPS 13 offers a premium computing experience for users who prioritize portability and productivity. Its premium build quality and attention to detail make it a popular choice among professionals and students alike.", "dellxps.jpg", "1. Display:\n	- Size: 13.4 inches\n	- Type: InfinityEdge display\n	- Resolution: Full HD+ or 4K Ultra HD+\n	- Brightness: 500 nits (Full HD+) or 400 nits (4K Ultra HD+)\n	- Color Gamut: 100% sRGB (Full HD+) or 90% DCI-P3 (4K Ultra HD+)\n2. Processor:\n	- 11th Generation Intel Core i3, i5, or i7\n3. Memory:\n	- RAM: 8GB or 16GB\n	- Storage: 256GB or 512GB or 1TB or 2TB\n4. Operating System:\n	- Windows 10 Home or Windows 10 Pro\n5. Graphics:\n	- Intel UHD Graphics or Intel Iris Xe Graphics\n6. Battery:\n	- Up to 14 hours of battery life\n7. Connectivity:\n	- Killer Wi-Fi 6 AX1650 (2x2) or Intel Wi-Fi 6 AX201 (2x2)\n	- Bluetooth 5.1\n8. Ports:\n	- 2 x Thunderbolt 4 (USB Type-C) with Power Delivery and DisplayPort\n	- 1 x USB-C 3.2 with Power Delivery and DisplayPort\n	- 1 x MicroSD card reader\n	- 1 x 3.5mm headphone/microphone combo jack\n9. Audio:\n	- Stereo speakers with Waves MaxxAudio Pro\n	- 3.5mm headphone/microphone combo jack\n10. Dimensions:\n	- Height: 14.8 mm (0.58 inches)\n	- Width: 295.7 mm (11.64 inches)\n	- Depth: 198.7 mm (7.82 inches)\n11. Weight:\n	- Starting at 1.2 kg (2.64 lbs)\n12. Color Options:\n	- Platinum Silver with Black Carbon Fiber Palm Rest\n	- Frost with Arctic White Woven Glass Fiber Palm Rest\n13. Package Contents:\n	- Dell XPS 13 Laptop\n	- Power Adapter\n	- Quick Start Guide", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dell XPS 13", 999.99m, 10, 4 },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anker", 2, "Stylish and adjustable wireless charger stand for compatible devices.", "ankerchargerstand.jpg", "1. Compatibility:\n	- Qi-enabled devices\n2. Charging Technology:\n	- Wireless charging\n3. Charging Speed:\n	- Up to 10W charging power\n4. Design:\n	- Stand design for easy viewing while charging\n	- Adjustable viewing angles\n	- Non-slip charging surface\n	- LED indicator\n5. Charging Indicators:\n	- LED light to indicate charging status\n	- Solid blue light when charging\n	- No light when not charging\n6. Charging Modes:\n	- Supports both standard charging and fast wireless charging\n7. Safety Features:\n	- Over-current protection\n	- Over-voltage protection\n	- Over-temperature protection\n	- Short circuit protection\n	- Foreign object detection\n8. Power Input:\n	- USB Type-C\n	- Input Voltage: 5V/2A or 9V/2A\n9. Dimensions:\n	- Height: 4.4 inches (112 mm)\n	- Width: 2.7 inches (70 mm)\n	- Depth: 3.5 inches (90 mm)\n10. Weight:\n	- 4.3 ounces (122 grams)\n11. Package Contents:\n	- Anker Wireless Charger Stand\n	- USB Type-C cable\n	- User manual", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anker Wireless Charger Stand", 29.99m, 4, 3 },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Otterbox", 2, "Rugged and protective case for the latest smartphones.", "otterbox.jpg", "1. Compatibility:\n	- Designed for specific device models (varies based on the model)\n2. Protection Levels:\n	- Certified drop protection\n	- Multi-layer defense\n	- Shock-absorbing materials\n	- Dust protection\n	- Scratch protection\n	- Port covers to keep out dust and debris\n3. Design:\n	- Rugged and durable design\n	- Belt-clip holster (optional, varies based on the model)\n	- Precise cutouts for ports, buttons, and features\n	- Raised edges for added screen and camera protection\n4. Materials:\n	- Polycarbonate shell\n	- Synthetic rubber slipcover\n	- Polycarbonate holster (optional, varies based on the model)\n5. Colors:\n	- Available in a variety of color options (varies based on the model)\n6. Dimensions:\n	- Varies based on the specific device model\n7. Weight:\n	- Varies based on the specific device model\n8. Package Contents:\n	- OtterBox Defender Series Case\n	- Belt-clip holster (optional, varies based on the model)\n	- Installation instructions", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OtterBox Defender Series Case", 49.99m, 7, 3 },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung", 8, "Premium smartwatch with a rotating bezel and advanced health monitoring.", "samsungwatch4.jpg", "1. Compatibility:\n	- Designed for specific device models (varies based on the model)\n2. Protection Levels:\n	- Certified drop protection\n	- Multi-layer defense\n	- Shock-absorbing materials\n	- Dust protection\n	- Scratch protection\n	- Port covers to keep out dust and debris\n3. Design:\n	- Rugged and durable design\n	- Belt-clip holster (optional, varies based on the model)\n	- Precise cutouts for ports, buttons, and features\n	- Raised edges for added screen and camera protection\n4. Materials:\n	- Polycarbonate shell\n	- Synthetic rubber slipcover\n	- Polycarbonate holster (optional, varies based on the model)\n5. Colors:\n	- Available in a variety of color options (varies based on the model)\n6. Dimensions:\n	- Varies based on the specific device model\n7. Weight:\n	- Varies based on the specific device model\n8. Package Contents:\n	- OtterBox Defender Series Case\n	- Belt-clip holster (optional, varies based on the model)\n	- Installation instructions", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung Galaxy Watch 4", 349.99m, 2, 3 },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple", 2, "Magnetic wireless charger for the latest iPhone models.", "applemagsafe.jpg", "1. Compatibility:\n	- iPhone 12 series and later\n2. Charging Technology:\n	- MagSafe wireless charging\n3. Charging Speed:\n	- Up to 15W charging power (varies based on the device and power adapter)\n4. Design:\n	- Slim and compact design\n	- Circular shape with magnets for easy alignment\n	- Non-slip charging surface\n	- LED indicator\n5. Charging Indicators:\n	- LED light to indicate charging status\n	- Orange light when not properly aligned\n	- Green light when charging\n6. Safety Features:\n	- Foreign object detection\n	- Over-temperature protection\n	- Over-voltage protection\n	- Over-current protection\n	- FOD (Foreign Object Detection)\n7. Power Input:\n	- USB Type-C\n	- Requires a compatible USB-C power adapter (sold separately)\n8. Dimensions:\n	- Diameter: 74.1 mm\n	- Height: 8.2 mm\n9. Weight:\n	- 38 grams\n10. Package Contents:\n	- Apple MagSafe Charger Lite", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple MagSafe Charger Lite", 39.99m, 5, 3 },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jabra", 7, "Advanced earbuds with adjustable noise cancellation and comfortable fit.", "jabraelite.png", "1. Design:\n	- True wireless earbuds with in-ear design\n	- Compact and ergonomic fit\n	- Available in multiple color options\n2. Active Noise Cancellation (ANC):\n	- Advanced ANC technology for immersive sound\n	- Adjustable ANC levels\n3. Sound Quality:\n	- High-definition sound with customizable equalizer\n	- Powerful speakers\n	- 6-microphone call technology for clear voice quality\n4. Battery Life:\n	- Up to 5.5 hours of battery life on a single charge (with ANC enabled)\n	- Up to 25 hours of total battery life with the charging case\n	- Fast charging support (15 minutes charge provides up to 1 hour of battery life)\n5. Connectivity:\n	- Bluetooth 5.1\n	- Dual device connectivity\n6. Voice Assistant Support:\n	- Works with Alexa, Google Assistant, and Siri\n7. Durability:\n	- IPX4-rated water and dust resistance\n8. Controls:\n	- Touch controls for music, calls, and ANC\n	- MySound personalization for personalized sound settings\n9. Fit and Comfort:\n	- Multiple sizes of EarGels for a secure and comfortable fit\n	- Semi-open design for pressure relief\n10. Dimensions:\n	- Earbuds: Varies based on the specific model\n	- Charging Case: Varies based on the specific model\n11. Weight:\n	- Earbuds: Varies based on the specific model\n	- Charging Case: Varies based on the specific model\n12. Package Contents:\n	- Jabra Elite 85t True Wireless Earbuds\n	- Charging case\n	- USB-C cable\n	- EarGels (multiple sizes)\n	- User manual", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jabra Elite 85t", 229.99m, 8, 3 },
                    { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple", 2, "Precise and responsive stylus for compatible iPad models.", "applepencil.jpg", "1. Compatibility:\n	- iPad Pro 11-inch (3rd generation) and later\n	- iPad Pro 12.9-inch (3rd generation) and later\n	- iPad Air (4th generation)\n2. Precision and Sensitivity:\n	- Pixel-perfect precision\n	- Low latency\n	- Tilt and pressure sensitivity\n3. Design:\n	- Seamless and sleek design\n	- Matte finish for better grip\n	- Magnetic attachment to iPad Pro\n	- Magnetic charging\n	- No Lightning connector (charges wirelessly)\n4. Controls:\n	- Double-tap gesture for switching tools (customizable)\n	- Tap gesture for erasing\n5. Battery Life:\n	- Up to 12 hours of usage\n	- Fast charging (15 seconds charge provides up to 30 minutes of use)\n6. Charging Method:\n	- Wirelessly charges when attached to iPad Pro\n7. Dimensions:\n	- Length: 166 mm\n	- Diameter: 8.9 mm\n8. Weight:\n	- 20.7 grams\n9. Package Contents:\n	- Apple Pencil (2nd Generation)\n	- Documentation", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple Pencil (2nd Generation)", 129.99m, 3, 3 },
                    { 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung", 5, "Convertible laptop with a 15.6-inch AMOLED display and 11th Gen Intel Core processor.", "samsungbookpro.jpg", "1. Display:\n	- Size: Varies based on the specific model\n	- Type: Super AMOLED\n	- Resolution: Varies based on the specific model\n	- Touchscreen: Yes\n	- S Pen support: Yes\n2. Processor:\n	- Varies based on the specific model\n3. Memory:\n	- RAM: Varies based on the specific model\n	- Storage: Varies based on the specific model\n4. Operating System:\n	- Windows 10 Home or Windows 10 Pro (varies based on the model)\n5. Graphics:\n	- Varies based on the specific model\n6. Connectivity:\n	- Wi-Fi 6E (802.11ax)\n	- Bluetooth 5.1\n7. Ports:\n	- USB-C ports: Varies based on the specific model\n	- USB-A ports: Varies based on the specific model\n	- HDMI: Varies based on the specific model\n	- MicroSD card slot: Varies based on the specific model\n	- SIM card slot (LTE models): Varies based on the specific model\n8. Battery:\n	- Varies based on the specific model\n9. Dimensions:\n	- Varies based on the specific model\n10. Weight:\n	- Varies based on the specific model\n11. Color Options:\n	- Varies based on the specific model\n12. Special Features:\n	- 360-degree hinge for convertible form factor\n	- S Pen included (varies based on the model)\n13. Package Contents:\n	- Samsung Galaxy Book Pro 360\n	- S Pen (varies based on the model)\n	- Power adapter\n	- USB-C cable\n	- Documentation", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung Book Pro 360", 1299.99m, 6, 3 },
                    { 18, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple", 5, "Lightweight and powerful laptop with Apple M1 chip and Retina display.", "macaircat.jpg", "1. Display:\n	- Retina display\n	- Size: 13.3 inches\n	- Resolution: 2560 x 1600 pixels\n	- True Tone technology\n	- Wide color (P3)\n2. Processor:\n	- Apple M1 chip with 8-core CPU and 8-core GPU\n	- Neural Engine\n3. Memory:\n	- 8GB or 16GB unified memory\n4. Storage:\n	- 256GB, 512GB, 1TB, or 2TB SSD\n5. Operating System:\n	- macOS Big Sur\n6. Graphics:\n	- 8-core GPU\n7. Battery Life:\n	- Up to 15 hours of web browsing\n	- Up to 18 hours of video playback\n8. Connectivity:\n	- Two Thunderbolt/USB 4 ports\n	- 3.5mm headphone jack\n	- SDXC card slot (M1 model)\n9. Wireless:\n	- Wi-Fi 6 (802.11ax)\n	- Bluetooth 5.0\n10. Camera:\n	- 720p FaceTime HD camera\n11. Audio:\n	- Stereo speakers\n	- Wide stereo sound\n	- Support for Dolby Atmos playback\n12. Keyboard and Trackpad:\n	- Magic Keyboard with backlight\n	- Force Touch trackpad\n13. Dimensions:\n	- Height: 0.16-0.63 inch (0.41-1.61 cm)\n	- Width: 11.97 inches (30.41 cm)\n	- Depth: 8.36 inches (21.24 cm)\n14. Weight:\n	- 2.8 pounds (1.29 kg)\n15. Color Options:\n	- Gold, Silver, Space Gray\n16. Package Contents:\n	- MacBook Air\n	- 30W USB-C Power Adapter\n	- USB-C Charge Cable (2m)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple MacBook Air M1", 999.99m, 9, 3 },
                    { 19, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Logitech", 5, "Compact and comfortable keyboard for typing on multiple devices.", "k380keyboard.jpg", "1. Keyboard Type:\n	- Wireless keyboard\n2. Connectivity:\n	- Bluetooth Smart\n	- Wireless range: Up to 10 meters (33 feet)\n3. Multi-Device Support:\n	- Connects and switches between up to 3 devices\n	- Compatible with Windows, Mac, Chrome OS, Android, and iOS\n4. Key Layout:\n	- Compact key layout with full-size keys\n	- Scissor switches for comfortable and quiet typing\n5. Battery Life:\n	- 2 AAA alkaline batteries (pre-installed)\n	- Battery life: Up to 2 years\n6. Special Keys:\n	- Easy-Switch keys for device switching\n	- OS-adaptive keys (automatically map to supported functions on different devices)\n	- Home, Back, App-Switch, Contextual Menu keys (for mobile devices)\n7. Design:\n	- Compact and lightweight design\n	- Available in various colors\n8. Dimensions:\n	- Height: 124 mm\n	- Width: 279 mm\n	- Depth: 16 mm\n9. Weight:\n	- 423 g (including batteries)\n10. Package Contents:\n	- Logitech K380 Multi-Device Keyboard\n	- 2 AAA alkaline batteries (pre-installed)\n	- User documentation", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Logitech K380 Keyboard", 39.99m, 1, 3 },
                    { 20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Microsoft", 5, "Unique dual-screen smartphone with foldable design and 5G connectivity.", "surfacepro.png", "1. Display:\n	- Dual PixelSense Fusion AMOLED displays\n	- Size: 8.3 inches (each display)\n	- Resolution: 2700 x 1800 pixels (each display)\n	- 90Hz refresh rate\n2. Processor:\n	- Qualcomm Snapdragon 888\n3. Memory:\n	- RAM: 8GB\n	- Storage: 128GB, 256GB, or 512GB\n4. Operating System:\n	- Android 11\n5. Camera:\n	- Rear Camera:\n		- 12 MP, f/1.7, 27mm (wide), 1.4µm, dual pixel PDAF, OIS\n		- 12 MP, f/2.4, 51mm (telephoto), 1.0µm, PDAF, 2x optical zoom\n	- Front Camera:\n		- 12 MP, f/2.0, 27mm (wide), 1.4µm\n6. Battery:\n	- Dual battery design (Total capacity: 4442mAh)\n	- Fast charging support\n7. Connectivity:\n	- 5G support\n	- Wi-Fi 6 (802.11ax)\n	- Bluetooth 5.1\n8. Ports:\n	- USB-C 3.2 Gen 2 port\n9. Dimensions (Closed):\n	- Height: 145.2 mm\n	- Width: 93.3 mm\n	- Depth: 11 mm\n10. Dimensions (Open):\n	- Height: 145.2 mm\n	- Width: 186.9 mm\n	- Depth: 5.8 mm (single screen) or 11 mm (spanned screen)\n11. Weight:\n	- 284 grams\n12. Color Options:\n	- Glacier\n	- Obsidian\n	- Ember\n13. Package Contents:\n	- Microsoft Surface Duo 2\n	- Surface Slim Pen (sold separately)\n	- USB-C cable\n	- Documentation", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Microsoft Surface Duo 2", 1499.99m, 10, 3 },
                    { 21, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple", 1, "The iPhone X is a premium smartphone that features a sleek design, a stunning OLED display, advanced facial recognition technology, and powerful performance, providing users with a cutting-edge mobile experience.", "iphonex.png", "1. Display:\n	- Size: 5.8 inches\n	- Type: Super Retina OLED\n	- Resolution: 2436 x 1125 pixels\n	- Pixel Density: 458 ppi\n2. Processor:\n	- Apple A11 Bionic chip\n	- Neural Engine\n3. Memory:\n	- RAM: 3GB\n	- Storage: 64GB or 256GB\n4. Operating System:\n	- iOS (upgradable to the latest version)\n5. Camera:\n	- Rear Camera:\n		- 12 MP wide-angle lens, f/1.8 aperture\n		- 12 MP telephoto lens, f/2.4 aperture\n		- Optical zoom\n		- Portrait mode\n		- Optical image stabilization\n	- Front Camera:\n		- 7 MP TrueDepth camera, f/2.2 aperture\n		- Face ID support\n6. Battery:\n	- Built-in rechargeable lithium-ion battery\n	- Wireless charging support\n7. Connectivity:\n	- 4G LTE\n	- Wi-Fi 802.11ac\n	- Bluetooth 5.0\n	- NFC (Apple Pay)\n	- Lightning connector\n8. Dimensions:\n	- Height: 143.6 mm\n	- Width: 70.9 mm\n	- Depth: 7.7 mm\n9. Weight:\n	- 174 grams\n10. Color Options:\n	- Silver\n	- Space Gray\n11. Package Contents:\n	- iPhone X\n	- EarPods with Lightning Connector\n	- Lightning to USB Cable\n	- USB Power Adapter\n	- Documentation", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple iPhone X", 999.99m, 10, 5 },
                    { 22, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xiamoi", 1, "The Xiaomi Redmi Note 10 is a feature-packed smartphone that offers a high-quality display, impressive camera capabilities, long-lasting battery life, and excellent performance, making it a reliable and affordable choice for tech-savvy users.", "xiamoiredminote.jpeg", "1. Display:\n	- Size: 6.43 inches\n	- Type: Super AMOLED\n	- Resolution: 2400 x 1080 pixels\n	- Refresh Rate: 60Hz\n	- Corning Gorilla Glass 3\n2. Processor:\n	- Qualcomm Snapdragon 678\n3. Memory:\n	- RAM: 4GB or 6GB\n	- Storage: 64GB or 128GB\n	- Expandable Storage: MicroSD card slot (up to 512GB)\n4. Operating System:\n	- MIUI 12 based on Android 11\n5. Camera:\n	- Rear Camera:\n		- 48 MP, f/1.79, wide-angle lens\n		- 8 MP, f/2.2, ultra-wide-angle lens\n		- 2 MP, f/2.4, macro lens\n		- 2 MP, f/2.4, depth sensor\n	- Front Camera:\n		- 13 MP, f/2.45, wide-angle lens\n6. Battery:\n	- Capacity: 5,000mAh\n	- 33W Fast Charging support\n7. Connectivity:\n	- 4G LTE\n	- Wi-Fi 802.11 a/b/g/n/ac\n	- Bluetooth 5.0\n	- IR blaster\n	- USB Type-C port\n8. Audio:\n	- Dual stereo speakers\n	- 3.5mm headphone jack\n9. Dimensions:\n	- Height: 160.46 mm\n	- Width: 74.5 mm\n	- Depth: 8.29 mm\n10. Weight:\n	- 178.8 grams\n11. Color Options:\n	- Frost White\n	- Shadow Black\n	- Aqua Green\n	- Pebble White\n12. Package Contents:\n	- Xiaomi Redmi Note 10\n	- Power Adapter\n	- USB Type-C Cable\n	- SIM Eject Tool\n	- Soft Case\n	- User Guide", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xiaomi Redmi Note 10", 799.99m, 10, 4 },
                    { 23, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung", 1, "The Samsung Galaxy A32 5G is a versatile smartphone that combines 5G connectivity with a large and vibrant display, a multi-lens camera system, reliable performance, and a long-lasting battery, delivering a seamless and enjoyable mobile experience at an accessible price point.", "samsunga32g.jpeg", "1. Display:\n	- Size: 6.5 inches\n	- Type: TFT LCD\n	- Resolution: 1600 x 720 pixels\n	- Refresh Rate: 60Hz\n2. Processor:\n	- MediaTek Dimensity 720\n3. Memory:\n	- RAM: 4GB or 8GB\n	- Storage: 64GB or 128GB\n	- Expandable Storage: MicroSD card slot (up to 1TB)\n4. Operating System:\n	- Android 11 with One UI 3.1\n5. Camera:\n	- Rear Camera:\n		- 48 MP, f/1.8, wide-angle lens\n		- 8 MP, f/2.2, ultra-wide-angle lens\n		- 5 MP, f/2.4, macro lens\n		- 2 MP, f/2.4, depth sensor\n	- Front Camera:\n		- 13 MP, f/2.2, wide-angle lens\n6. Battery:\n	- Capacity: 5,000mAh\n	- 15W Adaptive Fast Charging support\n7. Connectivity:\n	- 5G\n	- Wi-Fi 802.11 a/b/g/n/ac\n	- Bluetooth 5.0\n	- USB Type-C port\n	- 3.5mm headphone jack\n8. Dimensions:\n	- Height: 164.2 mm\n	- Width: 76.1 mm\n	- Depth: 9.1 mm\n9. Weight:\n	- 205 grams\n10. Color Options:\n	- Awesome Black\n	- Awesome White\n	- Awesome Blue\n	- Awesome Violet\n11. Package Contents:\n	- Samsung Galaxy A32 5G\n	- Travel Adapter\n	- USB Type-C Cable\n	- Ejection Pin\n	- Quick Start Guide", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung Galaxy A32 5G", 399.99m, 10, 3 },
                    { 24, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Microsoft", 3, "The Microsoft Surface Pro 7 is a powerful 2-in-1 device that combines the portability of a tablet with the functionality of a laptop. With its versatile design, high-resolution display, robust performance, and compatibility with various accessories, the Surface Pro 7 offers users a flexible and productive computing experience.", "surfacepro.png", "1. Display:\n	- Size: 12.3 inches\n	- Type: PixelSense Display\n	- Resolution: 2736 x 1824 pixels\n	- Aspect Ratio: 3:2\n2. Processor:\n	- Intel Core i3, i5, or i7 (10th Gen)\n3. Memory:\n	- RAM: 4GB, 8GB, or 16GB\n	- Storage: 128GB, 256GB, 512GB, or 1TB\n4. Operating System:\n	- Windows 10 Home\n	- Windows 10 Pro\n5. Graphics:\n	- Intel UHD Graphics (i3)\n	- Intel Iris Plus Graphics (i5, i7)\n6. Battery:\n	- Up to 10.5 hours of typical device usage\n	- Fast Charging support\n7. Connectivity:\n	- Wi-Fi 6 (802.11ax)\n	- Bluetooth 5.0\n	- USB Type-C\n	- USB-A\n	- Surface Connect\n	- MicroSDXC card reader\n8. Ports:\n	- 1 x USB-C\n	- 1 x USB-A\n	- 1 x Surface Connect\n	- 1 x 3.5mm headphone jack\n	- 1 x MicroSDXC card reader\n	- 1 x Surface Type Cover port\n9. Dimensions:\n	- Height: 292 mm\n	- Width: 201 mm\n	- Depth: 8.5 mm\n10. Weight:\n	- i3, i5: Starting from 1.70 lbs (775 g)\n	- i7: Starting from 1.74 lbs (790 g)\n11. Color Options:\n	- Platinum\n	- Matte Black\n12. Package Contents:\n	- Microsoft Surface Pro 7\n	- Power Supply\n	- Quick Start Guide\n	- Safety and Warranty Documents", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Microsoft Surface Pro 7", 650.99m, 10, 4 },
                    { 25, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung", 3, "The Samsung Galaxy Tab S6 Lite is a lightweight and portable tablet that offers a premium multimedia experience. With its vibrant display, S Pen support, long battery life, and solid performance, the Galaxy Tab S6 Lite is perfect for users seeking an affordable and versatile tablet for entertainment, note-taking, and productivity on the go.", "samsungtab6.jpg", "1. Display:\n	- Size: 10.4 inches\n	- Type: TFT LCD\n	- Resolution: 2000 x 1200 pixels\n2. Processor:\n	- Samsung Exynos 9610\n3. Memory:\n	- RAM: 4GB\n	- Storage: 64GB or 128GB\n	- Expandable Storage: MicroSD card slot (up to 1TB)\n4. Operating System:\n	- Android 10 with One UI 2.5\n5. Camera:\n	- Rear Camera:\n		- 8 MP, f/1.9, wide-angle lens\n	- Front Camera:\n		- 5 MP, f/2.0, wide-angle lens\n6. Battery:\n	- Capacity: 7,040mAh\n	- Fast Charging support\n7. Connectivity:\n	- Wi-Fi 802.11 a/b/g/n/ac\n	- Bluetooth 5.0\n	- USB Type-C port\n	- 3.5mm headphone jack\n8. S Pen Support:\n	- Yes\n	- Included in the box\n9. Dimensions:\n	- Height: 244.5 mm\n	- Width: 154.3 mm\n	- Depth: 7 mm\n10. Weight:\n	- 467 grams\n11. Color Options:\n	- Oxford Gray\n	- Angora Blue\n	- Chiffon Pink\n12. Package Contents:\n	- Samsung Galaxy Tab S6 Lite\n	- S Pen\n	- USB Type-C Cable\n	- Power Adapter\n	- Quick Start Guide", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung Galaxy Tab S6 Lite", 250.99m, 10, 2 },
                    { 26, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Amazon", 3, "The Amazon Fire HD 8 is a budget-friendly tablet that provides a convenient and accessible way to enjoy entertainment, browse the web, and access a wide range of apps. With its compact size, high-definition display, long battery life, and integration with Amazon services, the Fire HD 8 offers a value-packed tablet experience for casual users.", "amazonfire.jpg", "1. Display:\n	- Size: 8 inches\n	- Type: IPS LCD\n	- Resolution: 1280 x 800 pixels\n2. Processor:\n	- Quad-core 2.0 GHz\n3. Memory:\n	- RAM: 2GB\n	- Storage: 32GB or 64GB\n	- Expandable Storage: MicroSD card slot (up to 1TB)\n4. Operating System:\n	- Fire OS 7\n5. Camera:\n	- Rear Camera:\n		- 2 MP\n	- Front Camera:\n		- 2 MP\n6. Battery:\n	- Capacity: Up to 12 hours of mixed-use battery life\n	- USB-C (2.0) charging\n7. Connectivity:\n	- Wi-Fi 802.11 a/b/g/n/ac\n	- Bluetooth 5.0\n	- USB Type-C port\n8. Audio:\n	- Built-in stereo speakers\n	- Dolby Atmos support\n	- 3.5mm headphone jack\n9. Dimensions:\n	- Height: 202 mm\n	- Width: 137 mm\n	- Depth: 9.7 mm\n10. Weight:\n	- 355 grams\n11. Color Options:\n	- Black\n	- White\n	- Twilight Blue\n	- Plum\n12. Package Contents:\n	- Amazon Fire HD 8\n	- USB Type-C Cable\n	- Power Adapter\n	- Quick Start Guide", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Amazon Fire HD 8", 89.99m, 10, 2 },
                    { 27, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple", 3, "The Apple 10.2-inch iPad is a popular tablet that combines a sleek design, powerful performance, and a user-friendly interface. With its vibrant Retina display, support for the Apple Pencil, and a wide selection of apps, the 10.2-inch iPad is a versatile device suitable for entertainment, productivity, and creative pursuits, making it a go-to choice for many users.", "amazonipad9.jpg", "1. Display:\n	- Size: 10.2 inches\n	- Type: Retina display\n	- Resolution: 2160 x 1620 pixels\n	- PPI: 264\n2. Chip:\n	- A13 Bionic chip with 64-bit architecture\n3. Storage Options:\n	- 64GB\n	- 256GB\n4. Operating System:\n	- iPadOS\n5. Camera:\n	- Rear Camera:\n		- 8 MP\n	- Front Camera:\n		- 1.2 MP\n6. Battery:\n	- Built-in 32.4-watt-hour rechargeable lithium-polymer battery\n	- Up to 10 hours of surfing the web on Wi-Fi, watching video, or listening to music\n7. Connectivity:\n	- Wi-Fi 802.11a/b/g/n/ac\n	- Bluetooth 4.2\n8. Ports:\n	- Lightning connector\n	- 3.5mm headphone jack\n9. Dimensions:\n	- Height: 250.6 mm\n	- Width: 174.1 mm\n	- Depth: 7.5 mm\n10. Weight:\n	- Wi-Fi model: 483 grams\n	- Wi-Fi + Cellular model: 493 grams\n11. Color Options:\n	- Space Gray\n	- Silver\n	- Gold\n12. Package Contents:\n	- 10.2-inch iPad\n	- Lightning to USB Cable\n	- USB Power Adapter", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple Apple 10.2-inch iPad", 399.99m, 10, 4 },
                    { 28, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple", 3, "The Apple iPad Pro 12.9-inch is a flagship tablet that pushes the boundaries of performance and creativity. With its stunning Liquid Retina XDR display, powerful A-series chip, support for the Apple Pencil and Magic Keyboard, and advanced camera system, the iPad Pro 12.9-inch is designed to cater to professionals and demanding users who require exceptional power, versatility, and precision for tasks such as multimedia editing, graphic design, and productivity on the go.", "appleipadpro.jpeg", "1. Display:\n	- Size: 12.9 inches\n	- Type: Liquid Retina XDR display\n	- Resolution: 2732 x 2048 pixels\n	- ProMotion technology with 120Hz refresh rate\n	- True Tone display\n	- Wide color display (P3)\n	- Anti-reflective coating\n2. Chip:\n	- M1 chip with 8-core CPU and 8-core GPU\n3. Storage Options:\n	- 128GB\n	- 256GB\n	- 512GB\n	- 1TB\n	- 2TB\n4. Operating System:\n	- iPadOS\n5. Camera:\n	- Rear Camera:\n		- 12 MP Wide camera, f/1.8 aperture\n		- 10 MP Ultra Wide camera, f/2.4 aperture\n		- LiDAR Scanner\n	- Front Camera:\n		- 12 MP Ultra Wide camera, f/2.4 aperture\n6. Battery:\n	- Built-in 40.88-watt-hour rechargeable lithium-polymer battery\n	- Up to 10 hours of surfing the web on Wi-Fi, watching video, or listening to music\n7. Connectivity:\n	- 5G or Wi-Fi models available\n	- Wi-Fi 6 (802.11ax)\n	- Bluetooth 5.0\n8. Ports:\n	- Thunderbolt / USB 4 port\n	- USB-C port\n	- Smart Connector\n	- Nano-SIM tray (cellular models)\n9. Dimensions:\n	- Height: 280.6 mm\n	- Width: 214.9 mm\n	- Depth: 6.4 mm\n10. Weight:\n	- Wi-Fi model: 682 grams\n	- Wi-Fi + Cellular model: 685 grams\n11. Color Options:\n	- Silver\n	- Space Gray\n12. Package Contents:\n	- 12.9-inch iPad Pro\n	- USB-C Charge Cable\n	- 20W USB-C Power Adapter", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple iPad Pro 12.9-inch", 1349.99m, 10, 5 },
                    { 29, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alienware", 6, "The Alienware X17 is a high-performance gaming laptop that offers a powerful combination of cutting-edge hardware and immersive features. With its advanced graphics, overclockable processors, high-refresh-rate display, and customizable RGB lighting, the X17 delivers an exceptional gaming experience. Designed with gaming enthusiasts in mind, the Alienware X17 is known for its sleek design, robust build quality, and extensive gaming-specific features, making it a top choice for those seeking uncompromising performance in their gaming laptop.", "alienwarex17.jpg", "1. Display:\n	- Size: 17.3 inches\n	- Type: FHD or QHD or UHD\n	- Refresh Rate: 360Hz or 165Hz or 120Hz\n	- Technology: IPS or QHD IGZO\n	- Color Gamut: 100% sRGB\n2. Processor:\n	- 11th Generation Intel Core i7 or i9\n3. Graphics:\n	- NVIDIA GeForce RTX 3060 or RTX 3070 or RTX 3080\n	- AMD Radeon RX 6600M or RX 6700M or RX 6800M\n4. Memory:\n	- RAM: 16GB or 32GB or 64GB or 128GB\n	- Storage: 1TB or 2TB or 4TB\n5. Operating System:\n	- Windows 10 Home or Windows 10 Pro\n6. Battery:\n	- 6-Cell 86Whr integrated battery\n7. Connectivity:\n	- Killer Wi-Fi 6 AX1650i (2x2)\n	- Bluetooth 5.2\n8. Ports:\n	- 3 x USB 3.2 Type-A\n	- 1 x HDMI 2.1\n	- 1 x USB-C with Thunderbolt 4\n	- 1 x Alienware Graphics Amplifier Port\n	- 1 x RJ-45 Killer Ethernet\n	- 1 x 3.5mm headphone/microphone combo jack\n9. Audio:\n	- Alienware Sound Center and Audio Recon\n	- Dual speakers with Nahimic 3D Audio\n	- 3.5mm headphone/microphone combo jack\n10. Dimensions:\n	- Height: 22.9 mm (0.9 inches)\n	- Width: 399.8 mm (15.74 inches)\n	- Depth: 295.6 mm (11.64 inches)\n11. Weight:\n	- Starting at 3.06 kg (6.75 lbs)\n12. Color Options:\n	- Dark Side of the Moon\n	- Lunar Light\n13. Package Contents:\n	- Alienware X17 Laptop\n	- Power Adapter\n	- Quick Start Guide", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alienware X17", 3000.00m, 10, 5 },
                    { 30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Asus", 6, "The Asus ROG Zephyrus M16 is a premium gaming laptop that strikes a balance between power and portability. With its sleek design, high-refresh-rate display, powerful Intel processors, and NVIDIA graphics, the Zephyrus M16 delivers an immersive gaming experience. It also offers professional-grade features, such as color accuracy and productivity-enhancing capabilities, making it suitable for content creators and professionals who require both gaming performance and creative capabilities in a single device.", "asusrog.jpg", "1. Display:\n	- Size: 16 inches\n	- Type: WQXGA IPS-level display\n	- Resolution: 2560 x 1600 pixels\n	- Refresh Rate: 165Hz\n	- Technology: Anti-glare, Pantone Validated\n	- Color Gamut: 100% DCI-P3\n2. Processor:\n	- 11th Generation Intel Core i9 or i7\n3. Graphics:\n	- NVIDIA GeForce RTX 3070 or RTX 3060\n4. Memory:\n	- RAM: 16GB or 32GB or 64GB\n	- Storage: 1TB or 2TB\n5. Operating System:\n	- Windows 10 Home or Windows 10 Pro\n6. Battery:\n	- 90Whrs battery\n	- Up to 10 hours of battery life\n7. Connectivity:\n	- Intel Wi-Fi 6E (802.11ax)\n	- Bluetooth 5.2\n8. Ports:\n	- 3 x USB 3.2 Gen 2 Type-A\n	- 1 x USB 3.2 Gen 2 Type-C with DisplayPort 1.4 and Power Delivery\n	- 1 x HDMI 2.0b\n	- 1 x 3.5mm headphone and microphone combo jack\n	- 1 x RJ-45 Ethernet port\n9. Audio:\n	- Dual speakers with Dolby Atmos\n	- Array microphone\n10. Dimensions:\n	- Height: 19.9 mm (0.78 inches)\n	- Width: 355 mm (13.98 inches)\n	- Depth: 243 mm (9.57 inches)\n11. Weight:\n	- Starting at 1.9 kg (4.19 lbs)\n12. Color Options:\n	- Eclipse Gray\n13. Package Contents:\n	- Asus ROG Zephyrus M16 Laptop\n	- Power Adapter\n	- Quick Start Guide", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Asus ROG Zephyrus M16", 1499.99m, 10, 4 },
                    { 31, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HP", 6, "The HP ZBook Studio G8 is a high-performance mobile workstation designed for professionals in creative fields. With its sleek and stylish design, powerful Intel processors, professional-grade graphics options, and expansive display options, the ZBook Studio G8 delivers excellent performance and visual fidelity for demanding tasks such as 3D modeling, video editing, and graphic design. Its durable build, advanced security features, and extensive connectivity options make it a reliable and versatile choice for professionals seeking a workstation that can handle their intensive workloads.", "hpzbook.jpg", "1. Display:\n	- Size: 15.6 inches\n	- Type: UHD or FHD or OLED display\n	- Resolution: Up to 3840 x 2160 pixels\n	- Technology: IPS or AMOLED\n	- Color Gamut: 100% Adobe RGB or 100% DCI-P3\n2. Processor:\n	- 11th Generation Intel Core i9 or i7 or Xeon\n3. Graphics:\n	- NVIDIA GeForce RTX 3080 or RTX A5000 or RTX A4000 or Quadro\n4. Memory:\n	- RAM: 16GB or 32GB or 64GB or 128GB\n	- Storage: Up to 4TB PCIe NVMe SSD\n5. Operating System:\n	- Windows 10 Pro or Ubuntu Linux\n6. Battery:\n	- 6-cell Li-ion battery\n	- Up to 10 hours of battery life\n7. Connectivity:\n	- Intel Wi-Fi 6E AX210\n	- Bluetooth 5.2\n8. Ports:\n	- 2 x USB 3.2 Gen 1 Type-A\n	- 2 x Thunderbolt 4 with USB4 Type-C\n	- 1 x HDMI 2.1\n	- 1 x RJ-45 Ethernet port\n	- 1 x 3.5mm headphone and microphone combo jack\n9. Audio:\n	- Bang & Olufsen dual speakers\n	- HP Audio Boost\n10. Dimensions:\n	- Height: 18.4 mm (0.72 inches)\n	- Width: 354 mm (13.94 inches)\n	- Depth: 234 mm (9.21 inches)\n11. Weight:\n	- Starting at 1.86 kg (4.10 lbs)\n12. Color Options:\n	- Silver\n13. Package Contents:\n	- HP ZBook Studio G8 Laptop\n	- Power Adapter\n	- Quick Start Guide", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HP ZBook Studio G8", 1999.99m, 10, 5 },
                    { 32, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Acer", 6, "The Acer Predator Triton 300 SE is a compact and powerful gaming laptop that packs a punch. With its sleek design, high-performance hardware, including Intel processors and NVIDIA graphics, and a fast refresh rate display, the Triton 300 SE delivers an immersive gaming experience. Its portable form factor makes it easy to take on the go, while the impressive cooling system ensures optimal performance during intense gaming sessions. The Triton 300 SE is an ideal choice for gamers who prioritize both power and portability in their gaming laptop.", "acerpredatortriton.jpg", "1. Display:\n	- Size: 14 inches\n	- Type: Full HD IPS display\n	- Refresh Rate: 144Hz\n	- Response Time: 3ms\n	- Technology: Acer ComfyView, LED-backlit\n	- Color Gamut: 100% sRGB\n2. Processor:\n	- 11th Generation Intel Core i7\n3. Graphics:\n	- NVIDIA GeForce RTX 3060\n4. Memory:\n	- RAM: 16GB or 32GB\n	- Storage: 512GB or 1TB\n5. Operating System:\n	- Windows 10 Home\n6. Battery:\n	- 4-cell Li-ion battery\n	- Up to 10 hours of battery life\n7. Connectivity:\n	- Intel Wireless Wi-Fi 6 AX201\n	- Bluetooth 5.0\n8. Ports:\n	- 1 x HDMI 2.0\n	- 1 x USB 3.2 Gen 2 Type-C with Thunderbolt 4\n	- 2 x USB 3.2 Gen 1\n	- 1 x USB 2.0\n	- 1 x RJ-45 Ethernet port\n	- 1 x 3.5mm headphone and microphone combo jack\n9. Audio:\n	- DTS:X Ultra audio\n	- Dual speakers\n10. Dimensions:\n	- Height: 21.95 mm (0.86 inches)\n	- Width: 323 mm (12.71 inches)\n	- Depth: 228 mm (8.97 inches)\n11. Weight:\n	- Starting at 1.7 kg (3.75 lbs)\n12. Color Options:\n	- Abyss Black\n13. Package Contents:\n	- Acer Predator Triton 300 SE Laptop\n	- Power Adapter\n	- Quick Start Guide", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Acer Predator Triton", 1399.99m, 10, 4 },
                    { 33, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Asus", 6, "The Asus ZenBook 13 OLED (UX325) is a premium ultrabook that combines style, portability, and visual excellence. With its sleek and lightweight design, vibrant OLED display, and powerful hardware, the ZenBook 13 OLED offers an immersive and color-rich viewing experience. It features the latest Intel processors, ample storage options, and a long-lasting battery, making it a reliable companion for productivity on the go. With its combination of elegance, performance, and a stunning OLED screen, the ZenBook 13 OLED is a compelling choice for users who value both aesthetics and functionality in a compact laptop.", "asuszenbook.jpg", "1. Display:\n	- Size: 13.3 inches\n	- Type: OLED display\n	- Resolution: 1920 x 1080 pixels\n	- Technology: Full HD NanoEdge display\n	- Color Gamut: 100% DCI-P3\n2. Processor:\n	- 11th Generation Intel Core i7 or i5\n3. Graphics:\n	- Intel Iris Xe Graphics\n4. Memory:\n	- RAM: 8GB or 16GB\n	- Storage: 512GB or 1TB PCIe NVMe SSD\n5. Operating System:\n	- Windows 10 Home or Windows 10 Pro\n6. Battery:\n	- 4-cell Li-ion battery\n	- Up to 13 hours of battery life\n7. Connectivity:\n	- Wi-Fi 6 (802.11ax)\n	- Bluetooth 5.0\n8. Ports:\n	- 1 x USB 3.2 Gen 1 Type-A\n	- 2 x Thunderbolt 4 with USB4 Type-C\n	- 1 x HDMI\n	- 1 x MicroSD card reader\n	- 1 x 3.5mm headphone and microphone combo jack\n9. Audio:\n	- Harman Kardon-certified audio\n	- ASUS SonicMaster stereo audio system\n10. Dimensions:\n	- Height: 13.9 mm (0.54 inches)\n	- Width: 304 mm (11.97 inches)\n	- Depth: 203 mm (7.99 inches)\n11. Weight:\n	- Starting at 1.11 kg (2.45 lbs)\n12. Color Options:\n	- Pine Grey\n	- Lilac Mist\n13. Package Contents:\n	- Asus ZenBook 13 OLED (UX325) Laptop\n	- Power Adapter\n	- Quick Start Guide", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Asus ZenBook 13 OLED", 1199.99m, 10, 4 },
                    { 34, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fossil", 8, "The Fossil Generation 6 smartwatch is a stylish and feature-packed wearable device that blends traditional watch design with modern technology. With its sleek and versatile design, vibrant touchscreen display, and a range of fitness and health tracking features, the Generation 6 offers users the convenience of smartwatch functionality while maintaining a classic timepiece aesthetic. It also provides smartphone notifications, music control, and customizable watch faces, making it a fashionable and functional accessory for tech-savvy individuals seeking a seamless blend of style and smart features on their wrist.", "fossilgeneration.jpg", "1. Display:\n	- Size: 1.28 inches\n	- Type: AMOLED display\n	- Resolution: 416 x 416 pixels\n2. Processor:\n	- Qualcomm Snapdragon Wear 4100+ platform\n3. Memory:\n	- RAM: 1GB\n	- Storage: 8GB\n4. Operating System:\n	- Wear OS by Google\n5. Battery:\n	- 24-hour plus multi-day battery life\n6. Connectivity:\n	- Bluetooth 5.0\n	- Wi-Fi\n	- NFC\n7. Sensors:\n	- Accelerometer\n	- Altimeter\n	- Ambient Light Sensor\n	- Gyroscope\n	- Heart Rate Sensor\n	- SpO2 (Blood Oxygen) Sensor\n	- GPS\n8. Water Resistance:\n	- 3ATM (30 meters)\n9. Compatibility:\n	- Android 6.0+ (excluding Go edition)\n	- iOS 12.0+\n10. Dimensions:\n	- Case Size: 44mm\n	- Thickness: 12mm\n11. Strap Options:\n	- Various interchangeable straps\n12. Package Contents:\n	- Fossil Generation 6 Smartwatch\n	- Magnetic USB Charger\n	- Quick Start Guide", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fossil Generation 6", 299.99m, 10, 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AddedAt", "Address", "City", "ClosedAt", "Country", "Email", "FirstName", "IsActive", "LastName", "ModifiedAt", "Password", "PhoneNumber", "UserTypeId", "ZipCode" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "admin@example.com", "Admin", true, "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEDgSFsr9tJMx9tKbzUb3QWKKR8EpCPxG1CXVyJlao5KUgbSp5r4RKZy34Db7EvjO8A==", null, 1, null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "employee@example.com", "Employee", true, "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEObKOXWmFgvvS1Eo/U1EAMON13YRrXtLfBPgnvzSrMF17D/w0p4MRgt/rO/bRdwrrw==", null, 2, null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "buyer@example.com", "Buyer", true, "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEMnSzQhW2aJ2cYXQEa+HNKSScT39aHSp9vprrnJQ2iQ9IjaG4ujlNQ1/kZHiM0dfGA==", null, 3, null },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "visitor@example.com", "Visitor", true, "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAECEaKFxSakvfB2nh2VNiQEJLlmWe87Rw8CD9T9j4trl4/OgP9axAwZXZFy2zWi6Kwg==", null, 4, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TypeId",
                table: "Notifications",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_POSId",
                table: "Orders",
                column: "POSId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_POSId",
                table: "Repairs",
                column: "POSId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_StatusId",
                table: "Repairs",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_UserId",
                table: "Repairs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                table: "Users",
                column: "UserTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "RepairStatuses");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "POSes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "UserTypes");
        }
    }
}
