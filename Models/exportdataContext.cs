using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication5.Models
{
    public partial class exportdataContext : DbContext
    {
       

        public exportdataContext(DbContextOptions<exportdataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tbladmin> Tbladmin { get; set; }
        public virtual DbSet<Tblappointment> Tblappointment { get; set; }
        public virtual DbSet<Tblappointmentslot> Tblappointmentslot { get; set; }
        public virtual DbSet<Tblappointmentstatus> Tblappointmentstatus { get; set; }
        public virtual DbSet<Tblcategory> Tblcategory { get; set; }
        public virtual DbSet<Tblcity> Tblcity { get; set; }
        public virtual DbSet<Tblcustomer> Tblcustomer { get; set; }
        public virtual DbSet<Tblpackage> Tblpackage { get; set; }
        public virtual DbSet<Tblphoto> Tblphoto { get; set; }
        public virtual DbSet<Tblphotocomment> Tblphotocomment { get; set; }
        public virtual DbSet<Tblphotographer> Tblphotographer { get; set; }
        public virtual DbSet<Tblphotographerreview> Tblphotographerreview { get; set; }
        public virtual DbSet<Tblstate> Tblstate { get; set; }
        public virtual DbSet<Tbltag> Tbltag { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=exportdata;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tbladmin>(entity =>
            {
                entity.HasKey(e => e.Adminid)
                    .HasName("PK__tbladmin__AD040D7EECF3B151");

                entity.ToTable("tbladmin");

                entity.Property(e => e.Adminid)
                    .HasColumnName("adminid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Adminname)
                    .HasColumnName("adminname")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Adpassword)
                    .HasColumnName("adpassword")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Adprofile)
                    .HasColumnName("adprofile")
                    .IsUnicode(false);

                entity.Property(e => e.Adusername)
                    .HasColumnName("adusername")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Contact)
                    .HasColumnName("contact")
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tblappointment>(entity =>
            {
                entity.HasKey(e => e.Appointmentid)
                    .HasName("PK__tblappoi__D0666126F5BCE306");

                entity.ToTable("tblappointment");

                entity.Property(e => e.Appointmentid).HasColumnName("appointmentid");

                entity.Property(e => e.Appointmentdesc)
                    .HasColumnName("appointmentdesc")
                    .IsUnicode(false);

                entity.Property(e => e.Customerid).HasColumnName("customerid");

                entity.Property(e => e.Photographerid).HasColumnName("photographerid");

                entity.Property(e => e.Slotid).HasColumnName("slotid");

                entity.Property(e => e.Statusid).HasColumnName("statusid");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Tblappointment)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("FK__tblappoin__custo__3E52440B");

                entity.HasOne(d => d.Photographer)
                    .WithMany(p => p.Tblappointment)
                    .HasForeignKey(d => d.Photographerid)
                    .HasConstraintName("FK__tblappoin__photo__3D5E1FD2");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.Tblappointment)
                    .HasForeignKey(d => d.Slotid)
                    .HasConstraintName("FK__tblappoin__sloti__3C69FB99");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Tblappointment)
                    .HasForeignKey(d => d.Statusid)
                    .HasConstraintName("FK__tblappoin__statu__3F466844");
            });

            modelBuilder.Entity<Tblappointmentslot>(entity =>
            {
                entity.HasKey(e => e.Slotid)
                    .HasName("PK__tblappoi__9C4B6B2B1FE9AF31");

                entity.ToTable("tblappointmentslot");

                entity.Property(e => e.Slotid).HasColumnName("slotid");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Photographerid).HasColumnName("photographerid");

                entity.HasOne(d => d.Photographer)
                    .WithMany(p => p.Tblappointmentslot)
                    .HasForeignKey(d => d.Photographerid)
                    .HasConstraintName("FK__tblappoin__photo__403A8C7D");
            });

            modelBuilder.Entity<Tblappointmentstatus>(entity =>
            {
                entity.HasKey(e => e.Statusid)
                    .HasName("PK__tblappoi__36247E303691C0D7");

                entity.ToTable("tblappointmentstatus");

                entity.Property(e => e.Statusid).HasColumnName("statusid");

                entity.Property(e => e.Appstatus)
                    .HasColumnName("appstatus")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tblcategory>(entity =>
            {
                entity.HasKey(e => e.Categoryid)
                    .HasName("PK__tblcateg__23CDE590D0F86412");

                entity.ToTable("tblcategory");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Categoryname)
                    .HasColumnName("categoryname")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tblcity>(entity =>
            {
                entity.HasKey(e => e.Cityid)
                    .HasName("PK__tblcity__B4BDBD2658D830D8");

                entity.ToTable("tblcity");

                entity.Property(e => e.Cityid).HasColumnName("cityid");

                entity.Property(e => e.Cityname)
                    .HasColumnName("cityname")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Stateid).HasColumnName("stateid");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Tblcity)
                    .HasForeignKey(d => d.Stateid)
                    .HasConstraintName("FK__tblcity__stateid__412EB0B6");
            });

            modelBuilder.Entity<Tblcustomer>(entity =>
            {
                entity.HasKey(e => e.Customerid)
                    .HasName("PK__tblcusto__B61ED7F5A34DD701");

                entity.ToTable("tblcustomer");

                entity.Property(e => e.Customerid).HasColumnName("customerid");

                entity.Property(e => e.Contact)
                    .HasColumnName("contact")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Cumail)
                    .HasColumnName("cumail")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Cupassword)
                    .HasColumnName("cupassword")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Cuprofile)
                    .HasColumnName("cuprofile")
                    .IsUnicode(false);

                entity.Property(e => e.Customername)
                    .HasColumnName("customername")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Cuusername)
                    .HasColumnName("cuusername")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tblpackage>(entity =>
            {
                entity.HasKey(e => e.Packageid)
                    .HasName("PK__tblpacka__AA8A5400DB6B8521");

                entity.ToTable("tblpackage");

                entity.Property(e => e.Packageid).HasColumnName("packageid");

                entity.Property(e => e.Clicks).HasColumnName("clicks");

                entity.Property(e => e.Phototgrapherid).HasColumnName("phototgrapherid");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.Phototgrapher)
                    .WithMany(p => p.Tblpackage)
                    .HasForeignKey(d => d.Phototgrapherid)
                    .HasConstraintName("FK__tblpackag__photo__4222D4EF");
            });

            modelBuilder.Entity<Tblphoto>(entity =>
            {
                entity.HasKey(e => e.Photoid)
                    .HasName("PK__tblphoto__547D3E059DF5B94A");

                entity.ToTable("tblphoto");

                entity.Property(e => e.Photoid).HasColumnName("photoid");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Customerid).HasColumnName("customerid");

                entity.Property(e => e.Photographerid).HasColumnName("photographerid");

                entity.Property(e => e.Photourl)
                    .HasColumnName("photourl")
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Tblphoto)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("FK__tblphoto__catego__44FF419A");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Tblphoto)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("FK__tblphoto__custom__440B1D61");

                entity.HasOne(d => d.Photographer)
                    .WithMany(p => p.Tblphoto)
                    .HasForeignKey(d => d.Photographerid)
                    .HasConstraintName("FK__tblphoto__photog__4316F928");
            });

            modelBuilder.Entity<Tblphotocomment>(entity =>
            {
                entity.HasKey(e => e.Commentid)
                    .HasName("PK__tblphoto__CDA84BC5DD5DF99F");

                entity.ToTable("tblphotocomment");

                entity.Property(e => e.Commentid).HasColumnName("commentid");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .IsUnicode(false);

                entity.Property(e => e.Customerid).HasColumnName("customerid");

                entity.Property(e => e.Photoid).HasColumnName("photoid");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Tblphotocomment)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("FK__tblphotoc__custo__46E78A0C");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Tblphotocomment)
                    .HasForeignKey(d => d.Photoid)
                    .HasConstraintName("FK__tblphotoc__photo__45F365D3");
            });

            modelBuilder.Entity<Tblphotographer>(entity =>
            {
                entity.HasKey(e => e.Photographerid)
                    .HasName("PK__tblphoto__476BB07BC0BAE09B");

                entity.ToTable("tblphotographer");

                entity.Property(e => e.Photographerid).HasColumnName("photographerid");

                entity.Property(e => e.Cityid).HasColumnName("cityid");

                entity.Property(e => e.Contact)
                    .HasColumnName("contact")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Lan)
                    .HasColumnName("lan")
                    .IsUnicode(false);

                entity.Property(e => e.Lat)
                    .HasColumnName("lat")
                    .IsUnicode(false);

                entity.Property(e => e.Phmail)
                    .HasColumnName("phmail")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Photographername)
                    .HasColumnName("photographername")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Phpassword)
                    .HasColumnName("phpassword")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Phprofile)
                    .HasColumnName("phprofile")
                    .IsUnicode(false);

                entity.Property(e => e.Phusername)
                    .HasColumnName("phusername")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Tblphotographer)
                    .HasForeignKey(d => d.Cityid)
                    .HasConstraintName("FK__tblphotog__cityi__47DBAE45");
            });

            modelBuilder.Entity<Tblphotographerreview>(entity =>
            {
                entity.HasKey(e => e.Reviewid)
                    .HasName("PK__tblphoto__2ECE522CC0E12D1F");

                entity.ToTable("tblphotographerreview");

                entity.Property(e => e.Reviewid).HasColumnName("reviewid");

                entity.Property(e => e.Customerid).HasColumnName("customerid");

                entity.Property(e => e.Photographerid).HasColumnName("photographerid");

                entity.Property(e => e.Review).HasColumnName("review");

                entity.Property(e => e.Reviewdate)
                    .HasColumnName("reviewdate")
                    .HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Tblphotographerreview)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("FK__tblphotog__custo__49C3F6B7");

                entity.HasOne(d => d.Photographer)
                    .WithMany(p => p.Tblphotographerreview)
                    .HasForeignKey(d => d.Photographerid)
                    .HasConstraintName("FK__tblphotog__photo__48CFD27E");
            });

            modelBuilder.Entity<Tblstate>(entity =>
            {
                entity.HasKey(e => e.Stateid)
                    .HasName("PK__tblstate__A666BDB95D99464F");

                entity.ToTable("tblstate");

                entity.Property(e => e.Stateid).HasColumnName("stateid");

                entity.Property(e => e.Statename)
                    .HasColumnName("statename")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tbltag>(entity =>
            {
                entity.HasKey(e => e.Tagid)
                    .HasName("PK__tbltag__50FB05CF87AAC0D9");

                entity.ToTable("tbltag");

                entity.Property(e => e.Tagid).HasColumnName("tagid");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Tagname)
                    .HasColumnName("tagname")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Tbltag)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("FK__tbltag__category__4AB81AF0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
