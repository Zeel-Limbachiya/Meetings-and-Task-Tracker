using Microsoft.EntityFrameworkCore;

namespace MeetingAndTaskTracker.Models;

public partial class MeetTrackDbContext : DbContext
{
    public MeetTrackDbContext()
    {
    }

    public MeetTrackDbContext(DbContextOptions<MeetTrackDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActionItem> ActionItems { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Meeting> Meetings { get; set; }

    public virtual DbSet<MeetingParticipant> MeetingParticipants { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActionItem>(entity =>
        {
            entity.ToTable("ActionItem");

            entity.Property(e => e.AssignedTo).IsUnicode(false);
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Priority)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Department");
        });

        modelBuilder.Entity<Meeting>(entity =>
        {
            entity.ToTable("Meeting");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Title).IsUnicode(false);
        });

        modelBuilder.Entity<MeetingParticipant>(entity =>
        {
            entity.ToTable("MeetingParticipant");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
