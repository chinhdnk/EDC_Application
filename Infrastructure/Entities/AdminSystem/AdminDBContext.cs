using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class AdminDBContext : DbContext
    {
        public AdminDBContext()
        {
        }

        public AdminDBContext(DbContextOptions<AdminDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAccessLog> TblAccessLogs { get; set; } = null!;
        public virtual DbSet<TblAccount> TblAccounts { get; set; } = null!;
        public virtual DbSet<TblContact> TblContacts { get; set; } = null!;
        public virtual DbSet<TblCountry> TblCountries { get; set; } = null!;
        public virtual DbSet<TblGroup> TblGroups { get; set; } = null!;
        public virtual DbSet<TblLanguage> TblLanguages { get; set; } = null!;
        public virtual DbSet<TblMailConfig> TblMailConfigs { get; set; } = null!;
        public virtual DbSet<TblMenu> TblMenus { get; set; } = null!;
        public virtual DbSet<TblPasswordHistory> TblPasswordHistories { get; set; } = null!;
        public virtual DbSet<TblPermission> TblPermissions { get; set; } = null!;
        public virtual DbSet<TblProject> TblProjects { get; set; } = null!;
        public virtual DbSet<TblProjectText> TblProjectTexts { get; set; } = null!;
        public virtual DbSet<TblProjectVersion> TblProjectVersions { get; set; } = null!;
        public virtual DbSet<TblSignInPage> TblSignInPages { get; set; } = null!;
        public virtual DbSet<TblSignUp> TblSignUps { get; set; } = null!;
        public virtual DbSet<TblSystemParam> TblSystemParams { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;
        public virtual DbSet<TblUserProject> TblUserProjects { get; set; } = null!;
        public virtual DbSet<TblWebConfig> TblWebConfigs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAccessLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("tblAccessLog");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.Action).HasColumnName("action");

                entity.Property(e => e.Ip)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ip");

                entity.Property(e => e.LogDate)
                    .HasColumnType("datetime")
                    .HasColumnName("log_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<TblAccount>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("tblAccount");

                entity.Property(e => e.Username)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.ExpiredDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expired_date");

                entity.Property(e => e.FullName)
                    .HasMaxLength(500)
                    .HasColumnName("full_name");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("datetime")
                    .HasColumnName("last_login");

                entity.Property(e => e.OnLogin).HasColumnName("on_login");

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PasswordDate)
                    .HasColumnType("datetime")
                    .HasColumnName("password_date");

                entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");

                entity.Property(e => e.RefreshTokenExpiryTime).HasColumnName("refresh_token_expiry_time");

                entity.Property(e => e.ResetPwDate)
                    .HasColumnType("datetime")
                    .HasColumnName("reset_pw_date");

                entity.Property(e => e.ResetPwKey)
                    .HasMaxLength(99)
                    .HasColumnName("reset_pw_key");

                entity.Property(e => e.Salt)
                    .HasMaxLength(50)
                    .HasColumnName("salt");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.WrongTime).HasColumnName("wrong_time");
            });

            modelBuilder.Entity<TblContact>(entity =>
            {
                entity.ToTable("tblContact");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Email)
                    .HasMaxLength(300)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(250)
                    .HasColumnName("full_name");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .HasColumnName("mobile");

                entity.Property(e => e.Office)
                    .HasMaxLength(20)
                    .HasColumnName("office");

                entity.Property(e => e.Title)
                    .HasMaxLength(300)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");
            });

            modelBuilder.Entity<TblCountry>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.ToTable("tblCountry");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(50)
                    .HasColumnName("country_code");

                entity.Property(e => e.IsoCodes)
                    .HasMaxLength(50)
                    .HasColumnName("iso_codes");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TblGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.ToTable("tblGroup");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.HasMany(d => d.Perms)
                    .WithMany(p => p.Groups)
                    .UsingEntity<Dictionary<string, object>>(
                        "TblGroupPermission",
                        l => l.HasOne<TblPermission>().WithMany().HasForeignKey("PermId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_tblGroup_Permission_tblPermission"),
                        r => r.HasOne<TblGroup>().WithMany().HasForeignKey("GroupId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_tblGroup_Permission_tblGroup"),
                        j =>
                        {
                            j.HasKey("GroupId", "PermId");

                            j.ToTable("tblGroup_Permission");

                            j.IndexerProperty<int>("GroupId").HasColumnName("group_id");

                            j.IndexerProperty<string>("PermId").HasMaxLength(250).IsUnicode(false).HasColumnName("perm_id");
                        });
            });

            modelBuilder.Entity<TblLanguage>(entity =>
            {
                entity.ToTable("tblLanguage");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Flag)
                    .HasMaxLength(50)
                    .HasColumnName("flag");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");
            });

            modelBuilder.Entity<TblMailConfig>(entity =>
            {
                entity.ToTable("tblMailConfig");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Port).HasColumnName("port");

                entity.Property(e => e.Server)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("server");

                entity.Property(e => e.Ssl).HasColumnName("ssl");

                entity.Property(e => e.Timeout).HasColumnName("timeout");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<TblMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.ToTable("tblMenu");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Icon)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("icon");

                entity.Property(e => e.Link)
                    .HasMaxLength(500)
                    .HasColumnName("link");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(250)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.Sort).HasColumnName("sort");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<TblPasswordHistory>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.CreatedDate });

                entity.ToTable("tblPasswordHistory");

                entity.Property(e => e.Username)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.TblPasswordHistories)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblPasswordHistory_tblUser");
            });

            modelBuilder.Entity<TblPermission>(entity =>
            {
                entity.HasKey(e => e.PermId);

                entity.ToTable("tblPermission");

                entity.Property(e => e.PermId)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("perm_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Menu).HasColumnName("menu");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(500)
                    .HasColumnName("title");

                entity.HasOne(d => d.MenuNavigation)
                    .WithMany(p => p.TblPermissions)
                    .HasForeignKey(d => d.Menu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblPermission_tblMenu");
            });

            modelBuilder.Entity<TblProject>(entity =>
            {
                entity.HasKey(e => e.ProjectCode);

                entity.ToTable("tblProject");

                entity.Property(e => e.ProjectCode)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("project_code");

                entity.Property(e => e.ActualEdate)
                    .HasColumnType("datetime")
                    .HasColumnName("actual_edate");

                entity.Property(e => e.ApprovedEdate)
                    .HasColumnType("datetime")
                    .HasColumnName("approved_edate");

                entity.Property(e => e.ApprovedSdate)
                    .HasColumnType("datetime")
                    .HasColumnName("approved_sdate");

                entity.Property(e => e.ChiefPi)
                    .HasMaxLength(500)
                    .HasColumnName("chief_pi");

                entity.Property(e => e.Collaborators)
                    .HasMaxLength(500)
                    .HasColumnName("collaborators");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CustomPage)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("custom_page");

                entity.Property(e => e.DefaultLang)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("default_lang");

                entity.Property(e => e.Funder)
                    .HasMaxLength(500)
                    .HasColumnName("funder");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.ProjectGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("project_group");

                entity.Property(e => e.ProjectType)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("project_type");

                entity.Property(e => e.RegDate)
                    .HasColumnType("datetime")
                    .HasColumnName("reg_date");

                entity.Property(e => e.RegNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("reg_number");

                entity.Property(e => e.Responsibility)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("responsibility");

                entity.Property(e => e.SiteSize).HasColumnName("site_size");

                entity.Property(e => e.Sponsor)
                    .HasMaxLength(500)
                    .HasColumnName("sponsor");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SupportedLang)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("supported_lang");

                entity.Property(e => e.SystemParameter)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("system_parameter");
            });

            modelBuilder.Entity<TblProjectText>(entity =>
            {
                entity.HasKey(e => new { e.ProjectCode, e.Lang });

                entity.ToTable("tblProject_Text");

                entity.Property(e => e.ProjectCode)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("project_code");

                entity.Property(e => e.Lang)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("lang");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Description)
                    .HasColumnType("ntext")
                    .HasColumnName("description");

                entity.Property(e => e.FullName)
                    .HasColumnType("ntext")
                    .HasColumnName("full_name");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.ShortName)
                    .HasColumnType("ntext")
                    .HasColumnName("short_name");

                entity.HasOne(d => d.ProjectCodeNavigation)
                    .WithMany(p => p.TblProjectTexts)
                    .HasForeignKey(d => d.ProjectCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblProject_Text_tblProject");
            });

            modelBuilder.Entity<TblProjectVersion>(entity =>
            {
                entity.HasKey(e => new { e.ProjectCode, e.VersionId });

                entity.ToTable("tblProject_Version");

                entity.Property(e => e.ProjectCode)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("project_code");

                entity.Property(e => e.VersionId).HasColumnName("version_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.VersionCode)
                    .HasMaxLength(50)
                    .HasColumnName("version_code");

                entity.HasOne(d => d.ProjectCodeNavigation)
                    .WithMany(p => p.TblProjectVersions)
                    .HasForeignKey(d => d.ProjectCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblProject_Version_tblProject");
            });

            modelBuilder.Entity<TblSignInPage>(entity =>
            {
                entity.ToTable("tblSignInPage");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.HtmlContent).HasColumnName("html_content");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");
            });

            modelBuilder.Entity<TblSignUp>(entity =>
            {
                entity.HasKey(e => e.Email);

                entity.ToTable("tblSignUp");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.City)
                    .HasMaxLength(250)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.FullName)
                    .HasMaxLength(500)
                    .HasColumnName("full_name");

                entity.Property(e => e.Institution)
                    .HasMaxLength(500)
                    .HasColumnName("institution");

                entity.Property(e => e.MPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("m_phone");

                entity.Property(e => e.OPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("o_phone");

                entity.Property(e => e.RegDate)
                    .HasColumnType("datetime")
                    .HasColumnName("reg_date");

                entity.Property(e => e.RegProject)
                    .HasMaxLength(500)
                    .HasColumnName("reg_project");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<TblSystemParam>(entity =>
            {
                entity.HasKey(e => e.SysParamId);

                entity.ToTable("tblSystem_Params");

                entity.Property(e => e.SysParamId).HasColumnName("sys_param_id");

                entity.Property(e => e.Category)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.ExpandedValue)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("expanded_value");

                entity.Property(e => e.Lang)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("lang");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.Property(e => e.Value)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("tblUser");

                entity.Property(e => e.Username)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.City)
                    .HasMaxLength(250)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.ESignature)
                    .IsUnicode(false)
                    .HasColumnName("e_signature");

                entity.Property(e => e.Institution)
                    .HasMaxLength(500)
                    .HasColumnName("institution");

                entity.Property(e => e.MPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("m_phone");

                entity.Property(e => e.OPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("o_phone");

                entity.Property(e => e.ProfileImage)
                    .IsUnicode(false)
                    .HasColumnName("profile_image");

                entity.HasMany(d => d.Groups)
                    .WithMany(p => p.Usernames)
                    .UsingEntity<Dictionary<string, object>>(
                        "TblUserGroup",
                        l => l.HasOne<TblGroup>().WithMany().HasForeignKey("GroupId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_tblUser_Group_tblGroup"),
                        r => r.HasOne<TblUser>().WithMany().HasForeignKey("Username").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_tblUser_Group_tblUser"),
                        j =>
                        {
                            j.HasKey("Username", "GroupId");

                            j.ToTable("tblUser_Group");

                            j.IndexerProperty<string>("Username").HasMaxLength(250).IsUnicode(false).HasColumnName("username");

                            j.IndexerProperty<int>("GroupId").HasColumnName("group_id");
                        });

                entity.HasMany(d => d.Perms)
                    .WithMany(p => p.Usernames)
                    .UsingEntity<Dictionary<string, object>>(
                        "TblUserPermission",
                        l => l.HasOne<TblPermission>().WithMany().HasForeignKey("PermId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_tblUser_Permission_tblPermission"),
                        r => r.HasOne<TblUser>().WithMany().HasForeignKey("Username").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_tblUser_Permission_tblUser_Permission"),
                        j =>
                        {
                            j.HasKey("Username", "PermId");

                            j.ToTable("tblUser_Permission");

                            j.IndexerProperty<string>("Username").HasMaxLength(250).IsUnicode(false).HasColumnName("username");

                            j.IndexerProperty<string>("PermId").HasMaxLength(250).IsUnicode(false).HasColumnName("perm_id");
                        });
            });

            modelBuilder.Entity<TblUserProject>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.ProjectCode });

                entity.ToTable("tblUser_Project");

                entity.Property(e => e.Username)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.ProjectCode)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("project_code");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("role");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.TblUserProjects)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUser_Project_tblAccount");
            });

            modelBuilder.Entity<TblWebConfig>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("tblWebConfig");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.Property(e => e.Value)
                    .HasMaxLength(250)
                    .HasColumnName("value");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
