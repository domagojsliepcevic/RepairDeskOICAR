using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepairDesk.Api.Data;
using RepairDesk.Api.Helpers;
using RepairDesk.Api.Repositories;
using RepairDesk.Api.Repositories.interfaces;
using RepairDesk.Api.Services;
using RepairDesk.Api.Services.interfaces;
using System.Text;

namespace RepairDesk.Api
{
    public class Program
    {
        //private static readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var jwtSettings = builder.Configuration.GetSection("JwtSettings"); // JWT authentication configuration
            var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        Console.WriteLine($"Token received: {context.Token}");
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception}");
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
                c.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "decimal" })
            );

            // database
            //builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "RepairDeskDb"));
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // repositories
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            //builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IPOSRepository, POSRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IRepairRepository, RepairRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IStatsRepository, StatsRepository>();

            // services
            builder.Services.AddScoped<ICartService, CartService>();
            //builder.Services.AddScoped<IInventoryService, InventoryService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPOSService, POSService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IRepairService, RepairService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IStatsService, StatsService>();

            var app = builder.Build();

            //Configure(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // jwt!!!
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        //public static void Configure(IApplicationBuilder app)
        //{
        //    using (var scope = app.ApplicationServices.CreateScope())
        //    {
        //        SeedInMemoryDatabase(scope.ServiceProvider.GetRequiredService<AppDbContext>());
        //    }
        //}

        //private static void SeedInMemoryDatabase(AppDbContext context)
        //{
        //    // user types
        //    context.UserTypes.AddRange(
        //        Enum.GetValues<UserTypes>().Select(e => new UserType
        //        {
        //            Id = (int)e,
        //            Name = e.ToString(),
        //            Description = e.ToString()
        //        }).ToArray()
        //    );

        //    // notification types
        //    context.NotificationTypes.AddRange(
        //        Enum.GetValues<NotificationTypes>().Select(e => new NotificationType
        //        {
        //            Id = (int)e,
        //            Name = e.ToString(),
        //            Description = e.ToString()
        //        }
        //    ).ToArray());

        //    // order statuses
        //    context.OrderStatuses.AddRange(
        //        Enum.GetValues<OrderStatuses>().Select(e => new OrderStatus
        //        {
        //            Id = (int)e,
        //            Name = e.ToString(),
        //            Description = e.ToString()
        //        }).ToArray()
        //    );

        //    // repair statuses
        //    context.RepairStatuses.AddRange(
        //        Enum.GetValues<RepairStatuses>().Select(e => new RepairStatus
        //        {
        //            Id = (int)e,
        //            Name = e.ToString(),
        //            Description = e.ToString()
        //        }).ToArray()
        //    );

        //    // POSes
        //    context.POSes.AddRange(
        //        new POS { Id = 1, Location = "web", Name = "Web Shop" },
        //        new POS { Id = 2, Location = "shop1", Name = "Trgovina A" }
        //    );

        //    // seed user data
        //    context.Users.AddRange(
        //        new User { Id = 1, FirstName = "Admin", LastName = "User", Email = "admin@example.com", Password = _passwordHasher.HashPassword(null, "Password123"), UserTypeId = (int)Data.UserTypes.ADMIN },
        //        new User { Id = 2, FirstName = "Employee", LastName = "User", Email = "employee@example.com", Password = _passwordHasher.HashPassword(null, "Password123"), UserTypeId = (int)Data.UserTypes.EMPLOYEE },
        //        new User { Id = 3, FirstName = "Buyer", LastName = "User", Email = "buyer@example.com", Password = _passwordHasher.HashPassword(null, "Password123"), UserTypeId = (int)Data.UserTypes.BUYER },
        //        new User { Id = 4, FirstName = "Visitor", LastName = "User", Email = "visitor@example.com", Password = _passwordHasher.HashPassword(null, "Password123"), UserTypeId = (int)Data.UserTypes.VISITOR }
        //    );

        //    // product categories
        //    var productCategories = new List<string>
        //    {
        //        "Smartphones",
        //        "Accessories",
        //        "Tablets",
        //        "New",
        //        "Featured",
        //        "Laptops",
        //        "Earphones",
        //        "Smartwatches"
        //    };
        //    //var productCategoriesList = productCategories
        //    //    .Select((category, index) => new ProductCategory { Id = index + 1, Name = category, Description = category + " - description", IsSpecial = (category == "New" || category == "Featured") ? true : false, ImagePath = "pic2.png" })
        //    //    .ToArray();
        //    //context.ProductCategories.AddRange(productCategoriesList);

        //    context.ProductCategories.AddRange(
        //        new ProductCategory { Id = 1, Name = "Smartphones", Description = "Smartphones - description", IsSpecial = false, ImagePath = "smartphones.jpg" },
        //        new ProductCategory { Id = 2, Name = "Accessories", Description = "Accessories - description", IsSpecial = false, ImagePath = "accessories.jpg" },
        //        new ProductCategory { Id = 3, Name = "Tablets", Description = "Tablets - description", IsSpecial = false, ImagePath = "tablets.png" },
        //        new ProductCategory { Id = 4, Name = "New", Description = "New - description", IsSpecial = true, ImagePath = "newarrival.webp" },
        //        new ProductCategory { Id = 5, Name = "Featured", Description = "Featured - description", IsSpecial = true, ImagePath = "featured.png" },
        //        new ProductCategory { Id = 6, Name = "Laptops", Description = "Laptops - description", IsSpecial = false, ImagePath = "laptops.png" },
        //        new ProductCategory { Id = 7, Name = "Earphones", Description = "Earphones - description", IsSpecial = false, ImagePath = "earphones.jpg" },
        //        new ProductCategory { Id = 8, Name = "Smartwatches", Description = "Smartwatches - description", IsSpecial = false, ImagePath = "smartwatches.png" }

        //        );


        //    DescriptionLib description = new DescriptionLib();



        //    // products
        //    context.Products.AddRange(
        //        new Product { Id = 1, Name = "Apple iPhone 13", Brand = "Apple", Price = 799.99m, Description = "Latest iPhone with a 6.1-inch Super Retina XDR display and A15 Bionic chip.", CategoryId = 1, Quantity = 4, Rating = 3, LongDescription = description.IphoneDesc, ImagePath = "iphone13.jpg" },
        //        new Product { Id = 2, Name = "Samsung Galaxy S21", Brand = "Samsung", Price = 749.99m, Description = "Powerful Android smartphone with a 6.2-inch Dynamic AMOLED 2X display.", CategoryId = 1, Quantity = 7, Rating = 3, LongDescription = description.SamsungSTwentyOne, ImagePath = "samsungs21.jpg" },
        //        new Product { Id = 3, Name = "Google Pixel 6", Brand = "Google", Price = 599.99m, Description = "Sleek and advanced smartphone with a 6.4-inch AMOLED display and Google Tensor chip.", CategoryId = 1, Quantity = 2, Rating = 3, LongDescription = description.GooglePixel, ImagePath = "pixel6.png" },
        //        new Product { Id = 4, Name = "Apple Watch Series 7", Brand = "Apple", Price = 399.99m, Description = "Smartwatch with a larger Always-On Retina display and new watch faces.", CategoryId = 8, Quantity = 5, Rating = 3, LongDescription = description.AppleWatch, ImagePath = "applewatchs7.jpg" },
        //        new Product { Id = 5, Name = "Apple AirPods Pro", Brand = "Apple", Price = 249.99m, Description = "Noise-cancelling earbuds with immersive sound and transparency mode.", CategoryId = 7, Quantity = 8, Rating = 3, LongDescription = description.AppleAirpods, ImagePath = "appleairpodspro.jpg" },
        //        new Product { Id = 6, Name = "Samsung Galaxy Buds Pro", Brand = "Samsung", Price = 199.99m, Description = "Wireless earbuds with immersive sound and active noise cancellation.", CategoryId = 7, Quantity = 3, Rating = 3, LongDescription = description.SamsungBuds, ImagePath = "samsunggalaxybudspro.jpg" },
        //        new Product { Id = 7, Name = "Mophie Wireless Charging Pad", Brand = "Mophie", Price = 59.99m, Description = "Fast and convenient wireless charging pad for compatible devices.", CategoryId = 2, Quantity = 6, Rating = 3, LongDescription = description.MophiePowerBank, ImagePath = "mophie.jpg" },
        //        new Product { Id = 8, Name = "Belkin BOOSTCHARGE Power Bank", Brand = "Belkin", Price = 79.99m, Description = "High-capacity power bank with 20,000mAh battery for charging on the go.", CategoryId = 2, Quantity = 9, Rating = 3, LongDescription = description.BelkinPowerBank, ImagePath = "belkinpowerbank.jpg" },
        //        new Product { Id = 9, Name = "Samsung Galaxy Tab S7", Brand = "Samsung", Price = 649.99m, Description = "Premium Android tablet with a 11-inch display and S Pen included.", CategoryId = 3, Quantity = 1, Rating = 3, LongDescription = description.SamsungGalaxyTab, ImagePath = "samsungtabs7.jpeg" },
        //        new Product
        //        {
        //            Id = 10,
        //            Name = "Dell XPS 13",
        //            Brand = "Dell",
        //            Price = 999.99m,
        //            Description = "The Dell XPS 13 is a highly regarded laptop that combines sleek design, impressive performance, and a stunning display. With its thin bezels, compact form factor, powerful Intel processors, and vibrant screen options, the XPS 13 offers a premium computing experience for users who prioritize portability and productivity. Its premium build quality and attention to detail make it a popular choice among professionals and students alike.",
        //            CategoryId = 6,
        //            Quantity = 10,
        //            Rating = 4,
        //            LongDescription = description.DellXPS
        //        ,
        //            ImagePath = "dellxps.jpg"
        //        },
        //        new Product { Id = 11, Name = "Anker Wireless Charger Stand", Brand = "Anker", Price = 29.99m, Description = "Stylish and adjustable wireless charger stand for compatible devices.", CategoryId = 2, Quantity = 4, Rating = 3, LongDescription = description.AnkerStand, ImagePath = "ankerchargerstand.jpg" },
        //        new Product { Id = 12, Name = "OtterBox Defender Series Case", Brand = "Otterbox", Price = 49.99m, Description = "Rugged and protective case for the latest smartphones.", CategoryId = 2, Quantity = 7, Rating = 3, LongDescription = description.OtterBox, ImagePath = "otterbox.jpg" },
        //        new Product { Id = 13, Name = "Samsung Galaxy Watch 4", Brand = "Samsung", Price = 349.99m, Description = "Premium smartwatch with a rotating bezel and advanced health monitoring.", CategoryId = 8, Quantity = 2, Rating = 3, LongDescription = description.OtterBox, ImagePath = "samsungwatch4.jpg" },
        //        new Product { Id = 14, Name = "Apple MagSafe Charger Lite", Brand = "Apple", Price = 39.99m, Description = "Magnetic wireless charger for the latest iPhone models.", CategoryId = 2, Quantity = 5, Rating = 3, LongDescription = description.AppleCharger, ImagePath = "applemagsafe.jpg" },
        //        new Product { Id = 15, Name = "Jabra Elite 85t", Brand = "Jabra", Price = 229.99m, Description = "Advanced earbuds with adjustable noise cancellation and comfortable fit.", CategoryId = 7, Quantity = 8, Rating = 3, LongDescription = description.JabraEarphones, ImagePath = "jabraelite.png" },
        //        new Product { Id = 16, Name = "Apple Pencil (2nd Generation)", Brand = "Apple", Price = 129.99m, Description = "Precise and responsive stylus for compatible iPad models.", CategoryId = 2, Quantity = 3, Rating = 3, LongDescription = description.ApplePencil, ImagePath = "applepencil.jpg" },
        //        new Product { Id = 17, Name = "Samsung Book Pro 360", Brand = "Samsung", Price = 1299.99m, Description = "Convertible laptop with a 15.6-inch AMOLED display and 11th Gen Intel Core processor.", CategoryId = 5, Quantity = 6, Rating = 3, LongDescription = description.SamsungBookPro, ImagePath = "samsungbookpro.jpg" },
        //        new Product { Id = 18, Name = "Apple MacBook Air M1", Brand = "Apple", Price = 999.99m, Description = "Lightweight and powerful laptop with Apple M1 chip and Retina display.", CategoryId = 5, Quantity = 9, Rating = 3, LongDescription = description.AppleMacBookAir, ImagePath = "macaircat.jpg" },
        //        new Product { Id = 19, Name = "Logitech K380 Keyboard", Brand = "Logitech", Price = 39.99m, Description = "Compact and comfortable keyboard for typing on multiple devices.", CategoryId = 5, Quantity = 1, Rating = 3, LongDescription = description.LogitechKeyboard, ImagePath = "k380keyboard.jpg" },
        //        new Product { Id = 20, Name = "Microsoft Surface Duo 2", Brand = "Microsoft", Price = 1499.99m, Description = "Unique dual-screen smartphone with foldable design and 5G connectivity.", CategoryId = 5, Quantity = 10, Rating = 3, LongDescription = description.MicrosoftSurfaceDuo, ImagePath = "surfacepro.png" },
        //        new Product { Id = 21, Name = "Apple iPhone X", Brand = "Apple", Price = 999.99m, Description = "The iPhone X is a premium smartphone that features a sleek design, a stunning OLED display, advanced facial recognition technology, and powerful performance, providing users with a cutting-edge mobile experience.", CategoryId = 1, Quantity = 10, Rating = 5, LongDescription = description.IphoneX, ImagePath = "iphonex.png" },
        //        new Product { Id = 22, Name = "Xiaomi Redmi Note 10", Brand = "Xiamoi", Price = 799.99m, Description = "The Xiaomi Redmi Note 10 is a feature-packed smartphone that offers a high-quality display, impressive camera capabilities, long-lasting battery life, and excellent performance, making it a reliable and affordable choice for tech-savvy users.", CategoryId = 1, Quantity = 10, Rating = 4, LongDescription = description.XiamoiRedmiNoteTen, ImagePath = "xiamoiredminote.jpeg" },
        //        new Product { Id = 23, Name = "Samsung Galaxy A32 5G", Brand = "Samsung", Price = 399.99m, Description = "The Samsung Galaxy A32 5G is a versatile smartphone that combines 5G connectivity with a large and vibrant display, a multi-lens camera system, reliable performance, and a long-lasting battery, delivering a seamless and enjoyable mobile experience at an accessible price point.", CategoryId = 1, Quantity = 10, Rating = 3, LongDescription = description.SamsungA325G, ImagePath = "samsunga32g.jpeg" },
        //        new Product { Id = 24, Name = "Microsoft Surface Pro 7", Brand = "Microsoft", Price = 650.99m, Description = "The Microsoft Surface Pro 7 is a powerful 2-in-1 device that combines the portability of a tablet with the functionality of a laptop. With its versatile design, high-resolution display, robust performance, and compatibility with various accessories, the Surface Pro 7 offers users a flexible and productive computing experience.", CategoryId = 3, Quantity = 10, Rating = 4, LongDescription = description.SurfacePro, ImagePath = "surfacepro.png" },
        //        new Product { Id = 25, Name = "Samsung Galaxy Tab S6 Lite", Brand = "Samsung", Price = 250.99m, Description = "The Samsung Galaxy Tab S6 Lite is a lightweight and portable tablet that offers a premium multimedia experience. With its vibrant display, S Pen support, long battery life, and solid performance, the Galaxy Tab S6 Lite is perfect for users seeking an affordable and versatile tablet for entertainment, note-taking, and productivity on the go.", CategoryId = 3, Quantity = 10, Rating = 2, LongDescription = description.SamsungTabLite, ImagePath = "samsungtab6.jpg" },
        //        new Product { Id = 26, Name = "Amazon Fire HD 8", Brand = "Amazon", Price = 89.99m, Description = "The Amazon Fire HD 8 is a budget-friendly tablet that provides a convenient and accessible way to enjoy entertainment, browse the web, and access a wide range of apps. With its compact size, high-definition display, long battery life, and integration with Amazon services, the Fire HD 8 offers a value-packed tablet experience for casual users.", CategoryId = 3, Quantity = 10, Rating = 2, LongDescription = description.AmazonFire, ImagePath = "amazonfire.jpg" },
        //        new Product { Id = 27, Name = "Apple Apple 10.2-inch iPad", Brand = "Apple", Price = 399.99m, Description = "The Apple 10.2-inch iPad is a popular tablet that combines a sleek design, powerful performance, and a user-friendly interface. With its vibrant Retina display, support for the Apple Pencil, and a wide selection of apps, the 10.2-inch iPad is a versatile device suitable for entertainment, productivity, and creative pursuits, making it a go-to choice for many users.", CategoryId = 3, Quantity = 10, Rating = 4, LongDescription = description.AppleIpad, ImagePath = "amazonipad9.jpg" },
        //        new Product { Id = 28, Name = "Apple iPad Pro 12.9-inch", Brand = "Apple", Price = 1349.99m, Description = "The Apple iPad Pro 12.9-inch is a flagship tablet that pushes the boundaries of performance and creativity. With its stunning Liquid Retina XDR display, powerful A-series chip, support for the Apple Pencil and Magic Keyboard, and advanced camera system, the iPad Pro 12.9-inch is designed to cater to professionals and demanding users who require exceptional power, versatility, and precision for tasks such as multimedia editing, graphic design, and productivity on the go.", CategoryId = 3, Quantity = 10, Rating = 5, LongDescription = description.AppleIpadPro, ImagePath = "appleipadpro.jpeg" },
        //         new Product
        //         {
        //             Id = 29,
        //             Name = "Alienware X17",
        //             Brand = "Alienware",
        //             Price = 3000.00m,
        //             Description = "The Alienware X17 is a high-performance gaming laptop that offers a powerful combination of cutting-edge hardware and immersive features. With its advanced graphics, overclockable processors, high-refresh-rate display, and customizable RGB lighting, the X17 delivers an exceptional gaming experience. Designed with gaming enthusiasts in mind, the Alienware X17 is known for its sleek design, robust build quality, and extensive gaming-specific features, making it a top choice for those seeking uncompromising performance in their gaming laptop.",
        //             CategoryId = 6,
        //             Quantity = 10,
        //             Rating = 5,
        //             LongDescription = description.AlienwareX17,
        //             ImagePath = "alienwarex17.jpg"
        //         },
        //         new Product
        //         {
        //             Id = 30,
        //             Name = "Asus ROG Zephyrus M16",
        //             Brand = "Asus",
        //             Price = 1499.99m,
        //             Description = "The Asus ROG Zephyrus M16 is a premium gaming laptop that strikes a balance between power and portability. With its sleek design, high-refresh-rate display, powerful Intel processors, and NVIDIA graphics, the Zephyrus M16 delivers an immersive gaming experience. It also offers professional-grade features, such as color accuracy and productivity-enhancing capabilities, making it suitable for content creators and professionals who require both gaming performance and creative capabilities in a single device.",
        //             CategoryId = 6,
        //             Quantity = 10,
        //             Rating = 4,
        //             LongDescription = description.AsusRog,
        //             ImagePath = "asusrog.jpg"
        //         },
        //         new Product
        //         {
        //             Id = 31,
        //             Name = "HP ZBook Studio G8",
        //             Brand = "HP",
        //             Price = 1999.99m,
        //             Description = "The HP ZBook Studio G8 is a high-performance mobile workstation designed for professionals in creative fields. With its sleek and stylish design, powerful Intel processors, professional-grade graphics options, and expansive display options, the ZBook Studio G8 delivers excellent performance and visual fidelity for demanding tasks such as 3D modeling, video editing, and graphic design. Its durable build, advanced security features, and extensive connectivity options make it a reliable and versatile choice for professionals seeking a workstation that can handle their intensive workloads.",
        //             CategoryId = 6,
        //             Quantity = 10,
        //             Rating = 5,
        //             LongDescription = description.HPZBookStudio,
        //             ImagePath = "hpzbook.jpg"
        //         },
        //         new Product
        //         {
        //             Id = 32,
        //             Name = "Acer Predator Triton",
        //             Brand = "Acer",
        //             Price = 1399.99m,
        //             Description = "The Acer Predator Triton 300 SE is a compact and powerful gaming laptop that packs a punch. With its sleek design, high-performance hardware, including Intel processors and NVIDIA graphics, and a fast refresh rate display, the Triton 300 SE delivers an immersive gaming experience. Its portable form factor makes it easy to take on the go, while the impressive cooling system ensures optimal performance during intense gaming sessions. The Triton 300 SE is an ideal choice for gamers who prioritize both power and portability in their gaming laptop.",
        //             CategoryId = 6,
        //             Quantity = 10,
        //             Rating = 4,
        //             LongDescription = description.AcerPredatorTriton,
        //             ImagePath = "acerpredatortriton.jpg"
        //         },
        //         new Product
        //         {
        //             Id = 33,
        //             Name = "Asus ZenBook 13 OLED",
        //             Brand = "Asus",
        //             Price = 1199.99m,
        //             Description = "The Asus ZenBook 13 OLED (UX325) is a premium ultrabook that combines style, portability, and visual excellence. With its sleek and lightweight design, vibrant OLED display, and powerful hardware, the ZenBook 13 OLED offers an immersive and color-rich viewing experience. It features the latest Intel processors, ample storage options, and a long-lasting battery, making it a reliable companion for productivity on the go. With its combination of elegance, performance, and a stunning OLED screen, the ZenBook 13 OLED is a compelling choice for users who value both aesthetics and functionality in a compact laptop.",
        //             CategoryId = 6,
        //             Quantity = 10,
        //             Rating = 4,
        //             LongDescription = description.AsusZenBook,
        //             ImagePath = "asuszenbook.jpg"
        //         },
        //         new Product
        //         {
        //             Id = 34,
        //             Name = "Fossil Generation 6",
        //             Brand = "Fossil",
        //             Price = 299.99m,
        //             Description = "The Fossil Generation 6 smartwatch is a stylish and feature-packed wearable device that blends traditional watch design with modern technology. With its sleek and versatile design, vibrant touchscreen display, and a range of fitness and health tracking features, the Generation 6 offers users the convenience of smartwatch functionality while maintaining a classic timepiece aesthetic. It also provides smartphone notifications, music control, and customizable watch faces, making it a fashionable and functional accessory for tech-savvy individuals seeking a seamless blend of style and smart features on their wrist.",
        //             CategoryId = 8,
        //             Quantity = 10,
        //             Rating = 3,
        //             LongDescription = description.FossilGeneration,
        //             ImagePath = "fossilgeneration.jpg"
        //         }

        //    );


        //    // cart
        //    var cart = new Cart { UserId = 3, Total = 0, CartItems = new List<CartItem>() };
        //    // cart items
        //    for (var i = 1; i <= 3; i++)
        //    {
        //        var cartItem = new CartItem { UnitPrice = i * 10, Quantity = i, Price = i * 10 * i, ProductId = i, Cart = cart };
        //        cart.CartItems.Add(cartItem);

        //        cart.Quantity += cartItem.Quantity;
        //        cart.Total += cartItem.Price;
        //    }
        //    context.Carts.Add(cart);

        //    // inventory
        //    var quantities = new decimal[] { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100 };
        //    // POS 1
        //    for (int productId = 1; productId <= 20; productId++)
        //    {
        //        var inventory = new Inventory
        //        {
        //            ProductId = productId,
        //            Quantity = quantities[productId - 1],
        //            POSId = 1
        //        };
        //        context.Inventories.Add(inventory);
        //    }
        //    // POS 2
        //    for (int productId = 1; productId <= 20; productId++)
        //    {
        //        var inventory = new Inventory
        //        {
        //            ProductId = productId,
        //            Quantity = quantities[19 - (productId - 1)],
        //            POSId = 2
        //        };
        //        context.Inventories.Add(inventory);
        //    }

        //    // orders
        //    context.Orders.Add(new Order
        //    {
        //        OrderNumber = "123-4567-890",
        //        OrderDate = DateTime.Now,
        //        OrderStatusId = 1,
        //        TotalAmount = 2 * 799.99m + 2 * 749.99m,
        //        UserId = 3,
        //        POSId = 1,
        //        PaymentMethod = "COD",
        //        OrderItems = new List<OrderItem>
        //        {
        //            new OrderItem { UnitPrice = 799.99m, Quantity = 2, Price = 2 * 799.99m, ProductId = 1 },
        //            new OrderItem { UnitPrice = 749.99m, Quantity = 2, Price = 2 * 749.99m, ProductId = 2 }
        //        }
        //    });
        //    context.Orders.Add(new Order
        //    {
        //        OrderNumber = "231-4567-890",
        //        OrderDate = DateTime.Now,
        //        OrderStatusId = 2,
        //        TotalAmount = 2 * 599.99m + 2 * 399.99m,
        //        UserId = 3,
        //        POSId = 1,
        //        PaymentMethod = "COD",
        //        OrderItems = new List<OrderItem>
        //        {
        //            new OrderItem { UnitPrice = 599.99m, Quantity = 2, Price = 2 * 599.99m, ProductId = 3 },
        //            new OrderItem { UnitPrice = 399.99m, Quantity = 2, Price = 2 * 399.99m, ProductId = 4 }
        //        }
        //    });
        //    context.Orders.Add(new Order
        //    {
        //        OrderNumber = "321-4567-890",
        //        OrderDate = DateTime.Now,
        //        OrderStatusId = 3,
        //        TotalAmount = 2 * 249.99m + 2 * 199.99m,
        //        UserId = 3,
        //        POSId = 1,
        //        PaymentMethod = "COD",
        //        OrderItems = new List<OrderItem>
        //        {
        //            new OrderItem { UnitPrice = 249.99m, Quantity = 2, Price = 2 * 249.99m, ProductId = 5 },
        //            new OrderItem { UnitPrice = 199.99m, Quantity = 2, Price = 2 * 199.99m, ProductId = 6 }
        //        }
        //    });

        //    // repairs
        //    context.Repairs.AddRange(
        //        new Repair { RepairNumber = "123-4567-890", RequestDate = DateTime.Now, StatusId = 1, Description = "First repair description", FinishDate = null, UserId = 3, POSId = 1 },
        //        new Repair { RepairNumber = "231-4567-890", RequestDate = DateTime.Now, StatusId = 2, Description = "Second repair description", FinishDate = DateTime.Now.AddDays(10), UserId = 3, POSId = 1 },
        //        new Repair { RepairNumber = "321-4567-890", RequestDate = DateTime.Now, StatusId = 3, Description = "Third repair description", FinishDate = DateTime.Now.AddDays(15), UserId = 3, POSId = 1 }
        //    );

        //    context.SaveChanges();
        //}

    }
}