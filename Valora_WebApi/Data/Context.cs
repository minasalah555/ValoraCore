using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Emit;
using Valora.Models;

namespace Valora.Data
{
    public class Context:IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public Context(DbContextOptions<Context> options ):base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
         public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                 .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                 .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                 .EnableSensitiveDataLogging(true);

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<IdentityUserLogin<string>>().HasNoKey();
            //builder.Entity<IdentityUserToken<string>>().HasNoKey();

            //modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();

            builder.Entity<IdentityUserRole<string>>(b =>
            {
                b.HasKey(ur => new { ur.UserId, ur.RoleId });
            });

            var seededAt = new System.DateTime(2025, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);

            // Seed Categories
            builder.Entity<Category>().HasData(
                new Category { ID = 1, Name = "Oil Paintings", Description = "Rich, textured oil artworks.", CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Category { ID = 2, Name = "Acrylic Paintings", Description = "Vibrant acrylic compositions.", CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Category { ID = 3, Name = "Watercolor Paintings", Description = "Fluid, translucent watercolors.", CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Category { ID = 4, Name = "Sketches", Description = "Pencil and ink sketches.", CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Category { ID = 5, Name = "Digital Prints", Description = "High-quality digital art prints.", CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Category { ID = 6, Name = "Wood Sculptures", Description = "Hand-carved wooden sculptures.", CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Category { ID = 7, Name = "Stone Sculptures", Description = "Timeless stone masterpieces.", CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Category { ID = 8, Name = "Metal Sculptures", Description = "Modern metal forms and figures.", CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Category { ID = 9, Name = "Clay & Ceramic", Description = "Clay and ceramic creations.", CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Category { ID = 10, Name = "Miniatures", Description = "Small-scale detailed artworks.", CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false }
            );

            // Optional: Seed example Products (grayscale placeholders)
            builder.Entity<Product>().HasData(
                // Oil Paintings
                new Product { ID = 1, Name = "Sunset Oils", Description = "Textured sunset scene.", Price = 450, StockQuantity = 5, ImgUrl = "https://picsum.photos/seed/oil1/600/400?grayscale", CategoryId = 1, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 2, Name = "Still Life Oils", Description = "Classic still life.", Price = 520, StockQuantity = 3, ImgUrl = "https://picsum.photos/seed/oil2/600/400?grayscale", CategoryId = 1, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Acrylic Paintings
                new Product { ID = 3, Name = "Abstract Acrylic", Description = "Bold abstract forms.", Price = 380, StockQuantity = 8, ImgUrl = "https://picsum.photos/seed/acrylic1/600/400?grayscale", CategoryId = 2, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 4, Name = "Cityscape Acrylic", Description = "Urban skyline.", Price = 420, StockQuantity = 6, ImgUrl = "https://picsum.photos/seed/acrylic2/600/400?grayscale", CategoryId = 2, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Watercolor Paintings
                new Product { ID = 5, Name = "Coastal Watercolor", Description = "Soft coastal hues.", Price = 260, StockQuantity = 10, ImgUrl = "https://picsum.photos/seed/water1/600/400?grayscale", CategoryId = 3, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 6, Name = "Garden Watercolor", Description = "Botanical study.", Price = 240, StockQuantity = 12, ImgUrl = "https://picsum.photos/seed/water2/600/400?grayscale", CategoryId = 3, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Sketches
                new Product { ID = 7, Name = "Figure Sketch", Description = "Charcoal figure study.", Price = 120, StockQuantity = 20, ImgUrl = "https://picsum.photos/seed/sketch1/600/400?grayscale", CategoryId = 4, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 8, Name = "Architectural Sketch", Description = "Perspective facade.", Price = 140, StockQuantity = 15, ImgUrl = "https://picsum.photos/seed/sketch2/600/400?grayscale", CategoryId = 4, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Digital Prints
                new Product { ID = 9, Name = "Monochrome Print A", Description = "Minimalist digital print.", Price = 60, StockQuantity = 50, ImgUrl = "https://picsum.photos/seed/print1/600/400?grayscale", CategoryId = 5, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 10, Name = "Monochrome Print B", Description = "Geometric forms.", Price = 65, StockQuantity = 50, ImgUrl = "https://picsum.photos/seed/print2/600/400?grayscale", CategoryId = 5, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Wood Sculptures
                new Product { ID = 11, Name = "Carved Oak", Description = "Organic wooden form.", Price = 700, StockQuantity = 2, ImgUrl = "https://picsum.photos/seed/wood1/600/400?grayscale", CategoryId = 6, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 12, Name = "Maple Totem", Description = "Vertical wood totem.", Price = 820, StockQuantity = 1, ImgUrl = "https://picsum.photos/seed/wood2/600/400?grayscale", CategoryId = 6, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Stone Sculptures
                new Product { ID = 13, Name = "Marble Bust", Description = "Classical marble bust.", Price = 1500, StockQuantity = 1, ImgUrl = "https://picsum.photos/seed/stone1/600/400?grayscale", CategoryId = 7, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 14, Name = "Granite Form", Description = "Abstract granite form.", Price = 1100, StockQuantity = 1, ImgUrl = "https://picsum.photos/seed/stone2/600/400?grayscale", CategoryId = 7, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Metal Sculptures
                new Product { ID = 15, Name = "Steel Curve", Description = "Polished steel arc.", Price = 980, StockQuantity = 3, ImgUrl = "https://picsum.photos/seed/metal1/600/400?grayscale", CategoryId = 8, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 16, Name = "Welded Mesh", Description = "Interlaced metal.", Price = 890, StockQuantity = 2, ImgUrl = "https://picsum.photos/seed/metal2/600/400?grayscale", CategoryId = 8, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Clay & Ceramic
                new Product { ID = 17, Name = "Ceramic Vase", Description = "Matte ceramic vase.", Price = 210, StockQuantity = 7, ImgUrl = "https://picsum.photos/seed/ceramic1/600/400?grayscale", CategoryId = 9, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 18, Name = "Clay Figurine", Description = "Hand-molded figurine.", Price = 180, StockQuantity = 9, ImgUrl = "https://picsum.photos/seed/ceramic2/600/400?grayscale", CategoryId = 9, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Miniatures
                new Product { ID = 19, Name = "Mini Landscape", Description = "Tiny framed scene.", Price = 95, StockQuantity = 25, ImgUrl = "https://picsum.photos/seed/mini1/600/400?grayscale", CategoryId = 10, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 20, Name = "Mini Sculpture", Description = "Small metal miniature.", Price = 120, StockQuantity = 20, ImgUrl = "https://picsum.photos/seed/mini2/600/400?grayscale", CategoryId = 10, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false }
            );

            // Additional seeded products (ensure >8 per category)
            builder.Entity<Product>().HasData(
                // Oil Paintings (IDs 21-28)
                new Product { ID = 21, Name = "Monochrome Horizon", Description = "Muted horizon oil work.", Price = 460, StockQuantity = 4, ImgUrl = "https://picsum.photos/seed/oil3/600/400?grayscale", CategoryId = 1, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 22, Name = "Silent Forest", Description = "Grayscale forest scene.", Price = 480, StockQuantity = 6, ImgUrl = "https://picsum.photos/seed/oil4/600/400?grayscale", CategoryId = 1, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 23, Name = "Urban Twilight", Description = "City at dusk.", Price = 530, StockQuantity = 5, ImgUrl = "https://picsum.photos/seed/oil5/600/400?grayscale", CategoryId = 1, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 24, Name = "Ocean Study", Description = "Monochrome sea waves.", Price = 500, StockQuantity = 4, ImgUrl = "https://picsum.photos/seed/oil6/600/400?grayscale", CategoryId = 1, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 25, Name = "Portrait Shade", Description = "Oil portrait minimal.", Price = 650, StockQuantity = 2, ImgUrl = "https://picsum.photos/seed/oil7/600/400?grayscale", CategoryId = 1, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 26, Name = "Mountain Veil", Description = "Layered mountain forms.", Price = 560, StockQuantity = 3, ImgUrl = "https://picsum.photos/seed/oil8/600/400?grayscale", CategoryId = 1, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 27, Name = "Fog Harbor", Description = "Soft harbor study.", Price = 470, StockQuantity = 5, ImgUrl = "https://picsum.photos/seed/oil9/600/400?grayscale", CategoryId = 1, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 28, Name = "Rural Path", Description = "Minimal path landscape.", Price = 445, StockQuantity = 7, ImgUrl = "https://picsum.photos/seed/oil10/600/400?grayscale", CategoryId = 1, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Acrylic Paintings (IDs 29-36)
                new Product { ID = 29, Name = "Acrylic Geometry", Description = "Geometric grayscale acrylic.", Price = 400, StockQuantity = 9, ImgUrl = "https://picsum.photos/seed/acrylic3/600/400?grayscale", CategoryId = 2, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 30, Name = "Acrylic Motion", Description = "Dynamic brush strokes.", Price = 410, StockQuantity = 6, ImgUrl = "https://picsum.photos/seed/acrylic4/600/400?grayscale", CategoryId = 2, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 31, Name = "Acrylic Layers", Description = "Layered abstract study.", Price = 395, StockQuantity = 8, ImgUrl = "https://picsum.photos/seed/acrylic5/600/400?grayscale", CategoryId = 2, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 32, Name = "Acrylic City Grid", Description = "Urban grid concept.", Price = 430, StockQuantity = 5, ImgUrl = "https://picsum.photos/seed/acrylic6/600/400?grayscale", CategoryId = 2, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 33, Name = "Acrylic Flow", Description = "Fluid grayscale forms.", Price = 415, StockQuantity = 7, ImgUrl = "https://picsum.photos/seed/acrylic7/600/400?grayscale", CategoryId = 2, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 34, Name = "Acrylic Echo", Description = "Echoing shapes.", Price = 390, StockQuantity = 10, ImgUrl = "https://picsum.photos/seed/acrylic8/600/400?grayscale", CategoryId = 2, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 35, Name = "Acrylic Contrast", Description = "High contrast study.", Price = 420, StockQuantity = 4, ImgUrl = "https://picsum.photos/seed/acrylic9/600/400?grayscale", CategoryId = 2, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 36, Name = "Acrylic Field", Description = "Minimal field scene.", Price = 405, StockQuantity = 6, ImgUrl = "https://picsum.photos/seed/acrylic10/600/400?grayscale", CategoryId = 2, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Watercolor Paintings (IDs 37-44)
                new Product { ID = 37, Name = "Water Shade", Description = "Subtle gradient wash.", Price = 255, StockQuantity = 12, ImgUrl = "https://picsum.photos/seed/water3/600/400?grayscale", CategoryId = 3, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 38, Name = "Water Bloom", Description = "Soft bloom effect.", Price = 250, StockQuantity = 14, ImgUrl = "https://picsum.photos/seed/water4/600/400?grayscale", CategoryId = 3, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 39, Name = "Water Horizon", Description = "Fading distant line.", Price = 245, StockQuantity = 11, ImgUrl = "https://picsum.photos/seed/water5/600/400?grayscale", CategoryId = 3, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 40, Name = "Water Garden", Description = "Botanical impression.", Price = 235, StockQuantity = 13, ImgUrl = "https://picsum.photos/seed/water6/600/400?grayscale", CategoryId = 3, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 41, Name = "Water Mist", Description = "Misty atmosphere.", Price = 265, StockQuantity = 10, ImgUrl = "https://picsum.photos/seed/water7/600/400?grayscale", CategoryId = 3, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 42, Name = "Water Flow", Description = "Flowing water forms.", Price = 258, StockQuantity = 9, ImgUrl = "https://picsum.photos/seed/water8/600/400?grayscale", CategoryId = 3, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 43, Name = "Water Study", Description = "Technical wash study.", Price = 240, StockQuantity = 15, ImgUrl = "https://picsum.photos/seed/water9/600/400?grayscale", CategoryId = 3, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 44, Name = "Water Drift", Description = "Drifting shapes.", Price = 252, StockQuantity = 8, ImgUrl = "https://picsum.photos/seed/water10/600/400?grayscale", CategoryId = 3, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Sketches (IDs 45-52)
                new Product { ID = 45, Name = "Figure Study II", Description = "Gesture sketch series.", Price = 130, StockQuantity = 18, ImgUrl = "https://picsum.photos/seed/sketch3/600/400?grayscale", CategoryId = 4, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 46, Name = "Portrait Line", Description = "Minimal line portrait.", Price = 150, StockQuantity = 12, ImgUrl = "https://picsum.photos/seed/sketch4/600/400?grayscale", CategoryId = 4, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 47, Name = "Architecture Draft", Description = "Facade draft sketch.", Price = 145, StockQuantity = 13, ImgUrl = "https://picsum.photos/seed/sketch5/600/400?grayscale", CategoryId = 4, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 48, Name = "Hand Study", Description = "Detailed hand drawing.", Price = 125, StockQuantity = 20, ImgUrl = "https://picsum.photos/seed/sketch6/600/400?grayscale", CategoryId = 4, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 49, Name = "Urban Sketch", Description = "Street quick sketch.", Price = 135, StockQuantity = 16, ImgUrl = "https://picsum.photos/seed/sketch7/600/400?grayscale", CategoryId = 4, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 50, Name = "Still Life Draft", Description = "Objects rough outline.", Price = 115, StockQuantity = 22, ImgUrl = "https://picsum.photos/seed/sketch8/600/400?grayscale", CategoryId = 4, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 51, Name = "Animal Study", Description = "Wildlife motion sketch.", Price = 118, StockQuantity = 19, ImgUrl = "https://picsum.photos/seed/sketch9/600/400?grayscale", CategoryId = 4, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 52, Name = "Expression Series", Description = "Faces expression set.", Price = 155, StockQuantity = 14, ImgUrl = "https://picsum.photos/seed/sketch10/600/400?grayscale", CategoryId = 4, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Digital Prints (IDs 53-60)
                new Product { ID = 53, Name = "Print Grid", Description = "Grid-based composition.", Price = 70, StockQuantity = 60, ImgUrl = "https://picsum.photos/seed/print3/600/400?grayscale", CategoryId = 5, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 54, Name = "Print Wave", Description = "Waveform abstraction.", Price = 68, StockQuantity = 55, ImgUrl = "https://picsum.photos/seed/print4/600/400?grayscale", CategoryId = 5, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 55, Name = "Print Orbit", Description = "Orbital pattern.", Price = 72, StockQuantity = 58, ImgUrl = "https://picsum.photos/seed/print5/600/400?grayscale", CategoryId = 5, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 56, Name = "Print Mesh", Description = "Mesh lattice form.", Price = 66, StockQuantity = 65, ImgUrl = "https://picsum.photos/seed/print6/600/400?grayscale", CategoryId = 5, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 57, Name = "Print Pulse", Description = "Pulse line series.", Price = 69, StockQuantity = 62, ImgUrl = "https://picsum.photos/seed/print7/600/400?grayscale", CategoryId = 5, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 58, Name = "Print Minimal", Description = "Ultra-minimal design.", Price = 75, StockQuantity = 54, ImgUrl = "https://picsum.photos/seed/print8/600/400?grayscale", CategoryId = 5, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 59, Name = "Print Rings", Description = "Concentric ring set.", Price = 71, StockQuantity = 57, ImgUrl = "https://picsum.photos/seed/print9/600/400?grayscale", CategoryId = 5, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 60, Name = "Print Axis", Description = "Axis line concept.", Price = 67, StockQuantity = 59, ImgUrl = "https://picsum.photos/seed/print10/600/400?grayscale", CategoryId = 5, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Wood Sculptures (IDs 61-68)
                new Product { ID = 61, Name = "Oak Spiral", Description = "Spiral carved oak.", Price = 740, StockQuantity = 2, ImgUrl = "https://picsum.photos/seed/wood3/600/400?grayscale", CategoryId = 6, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 62, Name = "Pine Curve", Description = "Curved pine shape.", Price = 710, StockQuantity = 3, ImgUrl = "https://picsum.photos/seed/wood4/600/400?grayscale", CategoryId = 6, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 63, Name = "Walnut Form", Description = "Abstract walnut block.", Price = 760, StockQuantity = 2, ImgUrl = "https://picsum.photos/seed/wood5/600/400?grayscale", CategoryId = 6, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 64, Name = "Birch Totem", Description = "Stacked birch shapes.", Price = 790, StockQuantity = 1, ImgUrl = "https://picsum.photos/seed/wood6/600/400?grayscale", CategoryId = 6, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 65, Name = "Elm Slice", Description = "Polished elm slice.", Price = 705, StockQuantity = 3, ImgUrl = "https://picsum.photos/seed/wood7/600/400?grayscale", CategoryId = 6, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 66, Name = "Maple Study", Description = "Experimental maple form.", Price = 725, StockQuantity = 2, ImgUrl = "https://picsum.photos/seed/wood8/600/400?grayscale", CategoryId = 6, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 67, Name = "Cedar Twist", Description = "Twisted cedar piece.", Price = 750, StockQuantity = 2, ImgUrl = "https://picsum.photos/seed/wood9/600/400?grayscale", CategoryId = 6, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 68, Name = "Ash Pillar", Description = "Vertical ash pillar.", Price = 820, StockQuantity = 1, ImgUrl = "https://picsum.photos/seed/wood10/600/400?grayscale", CategoryId = 6, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Stone Sculptures (IDs 69-76)
                new Product { ID = 69, Name = "Granite Axis", Description = "Axis themed granite.", Price = 1150, StockQuantity = 1, ImgUrl = "https://picsum.photos/seed/stone3/600/400?grayscale", CategoryId = 7, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 70, Name = "Marble Fold", Description = "Folded marble form.", Price = 1520, StockQuantity = 1, ImgUrl = "https://picsum.photos/seed/stone4/600/400?grayscale", CategoryId = 7, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 71, Name = "Basalt Stack", Description = "Stacked basalt pieces.", Price = 1080, StockQuantity = 1, ImgUrl = "https://picsum.photos/seed/stone5/600/400?grayscale", CategoryId = 7, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 72, Name = "Limestone Arc", Description = "Arced limestone study.", Price = 1040, StockQuantity = 1, ImgUrl = "https://picsum.photos/seed/stone6/600/400?grayscale", CategoryId = 7, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 73, Name = "Marble Column", Description = "Minimal marble column.", Price = 1490, StockQuantity = 1, ImgUrl = "https://picsum.photos/seed/stone7/600/400?grayscale", CategoryId = 7, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 74, Name = "Slate Plane", Description = "Flat slate surface.", Price = 990, StockQuantity = 1, ImgUrl = "https://picsum.photos/seed/stone8/600/400?grayscale", CategoryId = 7, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 75, Name = "Onyx Curve", Description = "Curved onyx piece.", Price = 1300, StockQuantity = 1, ImgUrl = "https://picsum.photos/seed/stone9/600/400?grayscale", CategoryId = 7, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 76, Name = "Travertine Core", Description = "Core texture study.", Price = 1010, StockQuantity = 1, ImgUrl = "https://picsum.photos/seed/stone10/600/400?grayscale", CategoryId = 7, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Metal Sculptures (IDs 77-84)
                new Product { ID = 77, Name = "Steel Ring", Description = "Circular steel form.", Price = 920, StockQuantity = 2, ImgUrl = "https://picsum.photos/seed/metal3/600/400?grayscale", CategoryId = 8, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 78, Name = "Aluminum Fold", Description = "Folded aluminum plate.", Price = 870, StockQuantity = 2, ImgUrl = "https://picsum.photos/seed/metal4/600/400?grayscale", CategoryId = 8, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 79, Name = "Bronze Cast", Description = "Cast bronze form.", Price = 1050, StockQuantity = 1, ImgUrl = "https://picsum.photos/seed/metal5/600/400?grayscale", CategoryId = 8, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 80, Name = "Copper Curve", Description = "Curved copper piece.", Price = 910, StockQuantity = 2, ImgUrl = "https://picsum.photos/seed/metal6/600/400?grayscale", CategoryId = 8, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 81, Name = "Titanium Mesh", Description = "Mesh titanium structure.", Price = 1150, StockQuantity = 1, ImgUrl = "https://picsum.photos/seed/metal7/600/400?grayscale", CategoryId = 8, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 82, Name = "Iron Spine", Description = "Spine-like iron work.", Price = 890, StockQuantity = 2, ImgUrl = "https://picsum.photos/seed/metal8/600/400?grayscale", CategoryId = 8, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 83, Name = "Steel Weave", Description = "Woven steel rods.", Price = 960, StockQuantity = 2, ImgUrl = "https://picsum.photos/seed/metal9/600/400?grayscale", CategoryId = 8, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 84, Name = "Metal Axis", Description = "Axis themed metal.", Price = 980, StockQuantity = 2, ImgUrl = "https://picsum.photos/seed/metal10/600/400?grayscale", CategoryId = 8, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Clay & Ceramic (IDs 85-92)
                new Product { ID = 85, Name = "Ceramic Bowl", Description = "Matte shallow bowl.", Price = 190, StockQuantity = 6, ImgUrl = "https://picsum.photos/seed/ceramic3/600/400?grayscale", CategoryId = 9, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 86, Name = "Clay Relief", Description = "Textured wall relief.", Price = 230, StockQuantity = 5, ImgUrl = "https://picsum.photos/seed/ceramic4/600/400?grayscale", CategoryId = 9, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 87, Name = "Stoneware Plate", Description = "Minimal plate form.", Price = 175, StockQuantity = 9, ImgUrl = "https://picsum.photos/seed/ceramic5/600/400?grayscale", CategoryId = 9, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 88, Name = "Clay Vessel", Description = "Tapered vessel.", Price = 200, StockQuantity = 7, ImgUrl = "https://picsum.photos/seed/ceramic6/600/400?grayscale", CategoryId = 9, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 89, Name = "Porcelain Cup", Description = "Thin walled cup.", Price = 160, StockQuantity = 10, ImgUrl = "https://picsum.photos/seed/ceramic7/600/400?grayscale", CategoryId = 9, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 90, Name = "Clay Cylinder", Description = "Smooth cylinder form.", Price = 185, StockQuantity = 8, ImgUrl = "https://picsum.photos/seed/ceramic8/600/400?grayscale", CategoryId = 9, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 91, Name = "Earthen Jar", Description = "Rustic jar shape.", Price = 210, StockQuantity = 6, ImgUrl = "https://picsum.photos/seed/ceramic9/600/400?grayscale", CategoryId = 9, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 92, Name = "Ceramic Globe", Description = "Spherical matte form.", Price = 195, StockQuantity = 7, ImgUrl = "https://picsum.photos/seed/ceramic10/600/400?grayscale", CategoryId = 9, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                // Miniatures (IDs 93-100)
                new Product { ID = 93, Name = "Mini Abstract", Description = "Tiny abstract block.", Price = 105, StockQuantity = 30, ImgUrl = "https://picsum.photos/seed/mini3/600/400?grayscale", CategoryId = 10, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 94, Name = "Mini Portrait", Description = "Small portrait frame.", Price = 115, StockQuantity = 26, ImgUrl = "https://picsum.photos/seed/mini4/600/400?grayscale", CategoryId = 10, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 95, Name = "Mini Landscape II", Description = "Tiny grayscale landscape.", Price = 100, StockQuantity = 28, ImgUrl = "https://picsum.photos/seed/mini5/600/400?grayscale", CategoryId = 10, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 96, Name = "Mini Metal", Description = "Metal miniature form.", Price = 125, StockQuantity = 22, ImgUrl = "https://picsum.photos/seed/mini6/600/400?grayscale", CategoryId = 10, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 97, Name = "Mini Ceramic", Description = "Tiny ceramic piece.", Price = 98, StockQuantity = 24, ImgUrl = "https://picsum.photos/seed/mini7/600/400?grayscale", CategoryId = 10, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 98, Name = "Mini Wood", Description = "Small wood carving.", Price = 112, StockQuantity = 21, ImgUrl = "https://picsum.photos/seed/mini8/600/400?grayscale", CategoryId = 10, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 99, Name = "Mini Stone", Description = "Carved stone miniature.", Price = 118, StockQuantity = 19, ImgUrl = "https://picsum.photos/seed/mini9/600/400?grayscale", CategoryId = 10, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false },
                new Product { ID = 100, Name = "Mini Form", Description = "Minimal micro form.", Price = 102, StockQuantity = 27, ImgUrl = "https://picsum.photos/seed/mini10/600/400?grayscale", CategoryId = 10, CreatedAt = seededAt, UpdatedAt = seededAt, CreatedBy = 0, UpdatedBy = 0, IsDeleted = false }
            );
        }

    }
}
