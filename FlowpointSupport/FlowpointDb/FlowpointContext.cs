using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlowpointSupport.FlowpointDb;

public partial class FlowpointContext : DbContext
{
    public FlowpointContext()
    {
    }

    public FlowpointContext(DbContextOptions<FlowpointContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FlowpointSupportCompany> FlowpointSupportCompanies { get; set; }

    public virtual DbSet<FlowpointSupportTicket> FlowpointSupportTickets { get; set; }

    public virtual DbSet<FlowpointSupportVendor> FlowpointSupportVendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=LITTERBOX\\DEV_SERVER;Initial Catalog=Flowpoint;Trusted_Connection=true;Integrated Security=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FlowpointSupportCompany>(entity =>
        {
            entity.HasKey(e => e.ICompanyId);

            entity.ToTable("Flowpoint_Support_Company");

            entity.Property(e => e.ICompanyId).HasColumnName("iCompanyID");
            entity.Property(e => e.BIsDeleted).HasColumnName("bIsDeleted");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dtCreated");
            entity.Property(e => e.VCity)
                .HasMaxLength(128)
                .HasColumnName("vCity");
            entity.Property(e => e.VCompanyName)
                .HasMaxLength(64)
                .HasColumnName("vCompanyName");
            entity.Property(e => e.VContact)
                .HasMaxLength(64)
                .HasColumnName("vContact");
            entity.Property(e => e.VCountry)
                .HasMaxLength(50)
                .HasColumnName("vCountry");
            entity.Property(e => e.VEmail)
                .HasMaxLength(50)
                .HasColumnName("vEmail");
            entity.Property(e => e.VFax)
                .HasMaxLength(50)
                .HasColumnName("vFax");
            entity.Property(e => e.VPhone)
                .HasMaxLength(50)
                .HasColumnName("vPhone");
            entity.Property(e => e.VPostalCode)
                .HasMaxLength(32)
                .HasColumnName("vPostalCode");
            entity.Property(e => e.VProvince)
                .HasMaxLength(50)
                .HasColumnName("vProvince");
            entity.Property(e => e.VStreet1)
                .HasMaxLength(128)
                .HasColumnName("vStreet1");
            entity.Property(e => e.VStreet2)
                .HasMaxLength(128)
                .HasColumnName("vStreet2");
        });

        modelBuilder.Entity<FlowpointSupportTicket>(entity =>
        {
            entity.HasKey(e => e.ITicketId);

            entity.ToTable("Flowpoint_Support_Ticket", tb => tb.HasTrigger("trg_UpdateTicketModifiedDate"));

            entity.Property(e => e.ITicketId).HasColumnName("iTicketID");
            entity.Property(e => e.BIsDeleted).HasColumnName("bIsDeleted");
            entity.Property(e => e.DtCreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dtCreatedDate");
            entity.Property(e => e.DtModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dtModifiedDate");
            entity.Property(e => e.ICreatedBy).HasColumnName("iCreatedBy");
            entity.Property(e => e.IModifiedBy).HasColumnName("iModifiedBy");
            entity.Property(e => e.IVendorId).HasColumnName("iVendorID");
            entity.Property(e => e.VTicketMessage)
                .HasMaxLength(3000)
                .HasColumnName("vTicketMessage");

            entity.HasOne(d => d.IVendor).WithMany(p => p.FlowpointSupportTickets)
                .HasForeignKey(d => d.IVendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flowpoint_Support_Ticket_Flowpoint_Support_Vendor");
        });

        modelBuilder.Entity<FlowpointSupportVendor>(entity =>
        {
            entity.HasKey(e => e.IVendorId);

            entity.ToTable("Flowpoint_Support_Vendor");

            entity.Property(e => e.IVendorId).HasColumnName("iVendorID");
            entity.Property(e => e.BIsDeleted).HasColumnName("bIsDeleted");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dtCreated");
            entity.Property(e => e.ICompanyId).HasColumnName("iCompanyID");
            entity.Property(e => e.IVendorName)
                .HasMaxLength(128)
                .HasColumnName("iVendorName");
            entity.Property(e => e.VCity)
                .HasMaxLength(128)
                .HasColumnName("vCity");
            entity.Property(e => e.VContact)
                .HasMaxLength(64)
                .HasColumnName("vContact");
            entity.Property(e => e.VCountry)
                .HasMaxLength(50)
                .HasColumnName("vCountry");
            entity.Property(e => e.VEmail)
                .HasMaxLength(50)
                .HasColumnName("vEmail");
            entity.Property(e => e.VFax)
                .HasMaxLength(50)
                .HasColumnName("vFax");
            entity.Property(e => e.VPhone)
                .HasMaxLength(50)
                .HasColumnName("vPhone");
            entity.Property(e => e.VPostalCode)
                .HasMaxLength(32)
                .HasColumnName("vPostalCode");
            entity.Property(e => e.VProvince)
                .HasMaxLength(50)
                .HasColumnName("vProvince");
            entity.Property(e => e.VStreet1)
                .HasMaxLength(128)
                .HasColumnName("vStreet1");
            entity.Property(e => e.VStreet2)
                .HasMaxLength(128)
                .HasColumnName("vStreet2");

            entity.HasOne(d => d.ICompany).WithMany(p => p.FlowpointSupportVendors)
                .HasForeignKey(d => d.ICompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flowpoint_Support_Vendor_Flowpoint_Support_Company");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
