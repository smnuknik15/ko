using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineCourseWeb.Models
{
    public partial class OnlinecourseContext : DbContext
    {
        public OnlinecourseContext()
        {
        }

        public OnlinecourseContext(DbContextOptions<OnlinecourseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Coures> Coures { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Oders> Oders { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-QT0O3QL\\SQLEXPRESS;Initial Catalog=OnlineCourse;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Coures>(entity =>
            {
                entity.HasKey(e => e.CourseId);

                entity.Property(e => e.CourseId)
                    .HasColumnName("Course_ID")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CourseDate)
                    .HasColumnName("Course_Date")
                    .HasColumnType("date");

                entity.Property(e => e.CourseDetail)
                    .HasColumnName("Course_Detail")
                    .HasColumnType("text");

                entity.Property(e => e.CourseHourNum)
                    .HasColumnName("Course_HourNum")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CourseName)
                    .HasColumnName("Course_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CoursePrice)
                    .HasColumnName("Course_Price")
                    .HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CtmIdcard);

                entity.Property(e => e.CtmIdcard)
                    .HasColumnName("Ctm_IDcard")
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CtmEmail)
                    .HasColumnName("Ctm_Email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CtmLasname)
                    .HasColumnName("Ctm_Lasname")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CtmName)
                    .HasColumnName("Ctm_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CtmTel)
                    .HasColumnName("Ctm_Tel")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Oders>(entity =>
            {
                entity.HasKey(e => e.OderId);

                entity.Property(e => e.OderId)
                    .HasColumnName("Oder_ID")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.OderCustomerId)
                    .HasColumnName("Oder_CustomerID")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.OderDate)
                    .HasColumnName("Oder_Date")
                    .HasColumnType("date");

                entity.Property(e => e.OdersCouresId)
                    .HasColumnName("Oders_CouresID")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.OderCustomer)
                    .WithMany(p => p.Oders)
                    .HasForeignKey(d => d.OderCustomerId)
                    .HasConstraintName("FK_Oders_Customers");

                entity.HasOne(d => d.OdersCoures)
                    .WithMany(p => p.Oders)
                    .HasForeignKey(d => d.OdersCouresId)
                    .HasConstraintName("FK_Oders_Coures");
            });

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.HasKey(e => e.PmtId);

                entity.Property(e => e.PmtId)
                    .HasColumnName("Pmt_ID")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.PmtDate)
                    .HasColumnName("Pmt_Date")
                    .HasColumnType("date");

                entity.Property(e => e.PmtOderId)
                    .HasColumnName("Pmt_OderID")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.PmtPicture)
                    .HasColumnName("Pmt_Picture")
                    .HasColumnType("image");

                entity.HasOne(d => d.PmtOder)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PmtOderId)
                    .HasConstraintName("FK_Payments_Oders");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
