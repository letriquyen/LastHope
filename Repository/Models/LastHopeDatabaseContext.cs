using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Repository.Models
{
    public partial class LastHopeDatabaseContext : DbContext
    {
        public LastHopeDatabaseContext()
        {
        }

        public LastHopeDatabaseContext(DbContextOptions<LastHopeDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<BillItem> BillItems { get; set; } = null!;
        public virtual DbSet<Building> Buildings { get; set; } = null!;
        public virtual DbSet<Flat> Flats { get; set; } = null!;
        public virtual DbSet<FlatType> FlatTypes { get; set; } = null!;
        public virtual DbSet<RentContract> RentContracts { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<Term> Terms { get; set; } = null!;
        public virtual DbSet<UserAccount> UserAccounts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);Database=LastHopeDatabase;Trusted_Connection=True; TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("Bill");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Content).HasMaxLength(255);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Receiver).HasMaxLength(200);

                entity.Property(e => e.RentContractId).HasColumnName("RentContractID");

                entity.Property(e => e.Sender).HasMaxLength(200);

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.RentContract)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.RentContractId)
                    .HasConstraintName("FK__Bill__RentContra__34C8D9D1");
            });

            modelBuilder.Entity<BillItem>(entity =>
            {
                entity.HasKey(e => new { e.BillId, e.ServiceId })
                    .HasName("PK__BillItem__ADA34744E536F4A6");

                entity.ToTable("BillItem");

                entity.Property(e => e.BillId).HasColumnName("BillID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.BillItems)
                    .HasForeignKey(d => d.BillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BillItem__BillID__398D8EEE");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.BillItems)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BillItem__Servic__3A81B327");
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("Building");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Flat>(entity =>
            {
                entity.ToTable("Flat");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BuildingId).HasColumnName("BuildingID");

                entity.Property(e => e.Detail).HasMaxLength(255);

                entity.Property(e => e.FlatTypeId).HasColumnName("FlatTypeID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Flats)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("FK__Flat__BuildingID__2A4B4B5E");

                entity.HasOne(d => d.FlatType)
                    .WithMany(p => p.Flats)
                    .HasForeignKey(d => d.FlatTypeId)
                    .HasConstraintName("FK__Flat__FlatTypeID__2B3F6F97");
            });

            modelBuilder.Entity<FlatType>(entity =>
            {
                entity.ToTable("FlatType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<RentContract>(entity =>
            {
                entity.ToTable("RentContract");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Contract).HasMaxLength(100);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.ExpiryDate).HasColumnType("date");

                entity.Property(e => e.FlatId).HasColumnName("FlatID");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.RentContracts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__RentContr__Custo__2E1BDC42");

                entity.HasOne(d => d.Flat)
                    .WithMany(p => p.RentContracts)
                    .HasForeignKey(d => d.FlatId)
                    .HasConstraintName("FK__RentContr__FlatI__2F10007B");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Term>(entity =>
            {
                entity.ToTable("Term");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Content).HasMaxLength(255);

                entity.Property(e => e.RentContractId).HasColumnName("RentContractID");

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.HasOne(d => d.RentContract)
                    .WithMany(p => p.Terms)
                    .HasForeignKey(d => d.RentContractId)
                    .HasConstraintName("FK__Term__RentContra__31EC6D26");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.ToTable("UserAccount");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.CitizenId)
                    .HasMaxLength(50)
                    .HasColumnName("CitizenID");

                entity.Property(e => e.DateJoin).HasColumnType("date");

                entity.Property(e => e.DayOfBirth).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Fullname).HasMaxLength(200);

                entity.Property(e => e.Password).HasMaxLength(150);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
