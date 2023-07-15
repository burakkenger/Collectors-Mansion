using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=./wwwroot/app_data/database/database.db");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartList> CartLists { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<FollowerList> FollowerLists { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(e => e.Carts)
                .WithMany(e => e.Products)
                .UsingEntity<CartList>(
                    l => l.HasOne<Cart>().WithMany().HasForeignKey(e => e.CartID).OnDelete(DeleteBehavior.ClientSetNull),
                    r => r.HasOne<Product>().WithMany().HasForeignKey(e => e.ProductID).OnDelete(DeleteBehavior.ClientSetNull));

            modelBuilder.Entity<User>()
                .HasOne(e => e.Cart)
                .WithOne(e => e.User)
                .HasForeignKey<Cart>(e => e.UserID); // Kullanıcının alışveriş sepeti ve ürünler arasındaki çoka çoka ilişki


            modelBuilder.Entity<FollowerList>().HasOne(l => l.Follower).WithMany(l => l.Followers).HasForeignKey(l => l.FollowerID).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<FollowerList>().HasOne(l => l.Followed).WithMany(l => l.Followings).HasForeignKey(l => l.FollowedID).OnDelete(DeleteBehavior.ClientSetNull); // Takipçi tablosu ilişkileri

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductLikes)
                .WithMany(e => e.FavProducts)
                .UsingEntity<Like>(
                    l => l.HasOne<User>().WithMany().HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.ClientSetNull),
                    r => r.HasOne<Product>().WithMany().HasForeignKey(e => e.ProductID).OnDelete(DeleteBehavior.ClientSetNull)); // Kullanıcı ve Ürünler ararsındaki beğeni ilişkisi

            modelBuilder.Entity<Comment>().HasOne(l => l.User).WithMany(l => l.Comments).HasForeignKey(l => l.UserID).OnDelete(DeleteBehavior.ClientSetNull); // Yorum kullanıcı ilişkisi
            modelBuilder.Entity<Comment>().HasOne(l => l.Product).WithMany(l => l.Comments).HasForeignKey(l => l.ProductID).OnDelete(DeleteBehavior.ClientSetNull); // Yorum ürün ilişkisi
            modelBuilder.Entity<Product>().HasOne(l => l.User).WithMany(l => l.Products).HasForeignKey(l => l.UserID).OnDelete(DeleteBehavior.ClientSetNull); // Ürün kullanıcı ilişkisi
            modelBuilder.Entity<Product>().HasOne(l => l.Category).WithMany(l => l.Products).HasForeignKey(l => l.CategoryID).OnDelete(DeleteBehavior.ClientSetNull); // Ürün Kategori İlişkisi
            modelBuilder.Entity<Product>().HasOne(l => l.Status).WithMany(l => l.Products).HasForeignKey(l => l.StatusID).OnDelete(DeleteBehavior.ClientSetNull); // Ürüm Durum İlişkisi
            modelBuilder.Entity<Product>().HasOne(l => l.Collection).WithMany(l => l.Products).HasForeignKey(l => l.CollectionID).OnDelete(DeleteBehavior.ClientSetNull); // Koleskiyon ürün ilişkisi
            modelBuilder.Entity<Image>().HasOne(l => l.Product).WithMany(l => l.Images).HasForeignKey(l => l.ProductID).OnDelete(DeleteBehavior.ClientSetNull); // görsel ürün ilişkisi


        }
    }
}
