namespace MultiFaceRec
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EntityModel : DbContext
    {
        public EntityModel()
            : base("name=connectionString")
        {
        }

        public virtual DbSet<face_test> face_test { get; set; }
        public virtual DbSet<face> faces { get; set; }
        public virtual DbSet<otp> OTP { get; set; }
        public virtual DbSet<user> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<face_test>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<otp>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.lastname)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.pin)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.faces)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.otps)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);
        }
    }
}
