using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DIY_API.Models;

public partial class DIYDbContext : DbContext
{
    public DIYDbContext()
    {
    }

    public DIYDbContext(DbContextOptions<DIYDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Challenge> Challenges { get; set; }

    public virtual DbSet<ChallengeResult> ChallengeResults { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserChallenge> UserChallenges { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BE59C9997");

            entity.HasIndex(e => e.CategoryNameEn, "UQ__Categori__41280FCE0A26AA6B").IsUnique();

            entity.HasIndex(e => e.CategoryNameAr, "UQ__Categori__4129A8CDF9061F14").IsUnique();

            entity.Property(e => e.CategoryNameAr)
                .HasMaxLength(255)
                .HasColumnName("CategoryName_ar");
            entity.Property(e => e.CategoryNameEn)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CategoryName_en");
            entity.Property(e => e.CreatedBy).IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).IsUnicode(false);
        });

        modelBuilder.Entity<Challenge>(entity =>
        {
            entity.HasKey(e => e.ChallengesId).HasName("PK__Challeng__E1BE31221BAA88A9");

            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Level).HasMaxLength(50);
            entity.Property(e => e.MediaUrl)
                .HasMaxLength(255)
                .HasColumnName("MediaURL");
            entity.Property(e => e.Title).HasMaxLength(150);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Challenges)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Challenge__Categ__60A75C0F");
        });

        modelBuilder.Entity<ChallengeResult>(entity =>
        {
            entity.HasKey(e => e.ChallengeResultId).HasName("PK__Challeng__6D3E935D8D7AD799");

            entity.HasIndex(e => e.UserChallengeId, "UQ__Challeng__22D4F91C6980558D").IsUnique();

            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileUrl).HasColumnName("FileURL");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.UserChallenge).WithOne(p => p.ChallengeResult)
                .HasForeignKey<ChallengeResult>(d => d.UserChallengeId)
                .HasConstraintName("FK__Challenge__UserC__5FB337D6");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Ratings__FCCDF87C31E4BEF2");

            entity.HasIndex(e => new { e.UserId, e.ChallengeId }, "UQ_UserRating").IsUnique();

            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.RatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);

            entity.HasOne(d => d.Challenge).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ChallengeId)
                .HasConstraintName("FK__Ratings__Challen__628FA481");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Ratings__UserId__619B8048");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC55099590");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E426F70853").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ__Users__85FB4E38C4F49632").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053476F3630D").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedBy).IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ExpireOtp).HasColumnName("ExpireOTP");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.IsVerified)
                .HasDefaultValue(true)
                .HasColumnName("isVerified");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Otp).HasColumnName("OTP");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ProfileImage).HasColumnName("Profile_Image");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__656C112C");
        });

        modelBuilder.Entity<UserChallenge>(entity =>
        {
            entity.HasKey(e => e.UserChallengeId).HasName("PK__UserChal__22D4F91D9C045F84");

            entity.HasIndex(e => new { e.UserId, e.ChallengeId, e.Status }, "UQ_UserChallenge").IsUnique();

            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);

            entity.HasOne(d => d.Challenge).WithMany(p => p.UserChallenges)
                .HasForeignKey(d => d.ChallengeId)
                .HasConstraintName("FK__UserChall__Chall__6477ECF3");

            entity.HasOne(d => d.User).WithMany(p => p.UserChallenges)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserChall__UserI__6383C8BA");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A55F9732D0B");

            entity.ToTable("UserRole");

            entity.HasIndex(e => e.RoleName, "UQ__UserRole__8A2B6160319657D1").IsUnique();

            entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            entity.Property(e => e.CreatedBy).IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
