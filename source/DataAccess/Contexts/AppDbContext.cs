﻿using DomainModel.Entities;
using DomainModel.Entities.DoctorEntities;
using DomainModel.Entities.HosInfo;
using DomainModel.Entities.HospitalBody;
using DomainModel.Entities.MedicalServices;
using DomainModel.Entities.Others;
using DomainModel.Entities.SettingsEntities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts;

public class AppDbContext : DbContext
{

    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    #region DbSets
    public virtual DbSet<UserAccount> UserAccounts { get; set; }
    public virtual DbSet<CountriesTranslation> CountriesTranslations { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<WeekdaysTranslation> WeekdaysTranslations { get; set; }
    public virtual DbSet<GendersTranslation> GendersTranslations { get; set; }
    public virtual DbSet<EmployeeAccount> EmployeeAccounts { get; set; }

    public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }

    public virtual DbSet<MaritalStatusTranslation> MaritalStatusTranslations { get; set; }

    public virtual DbSet<Religion> Religions { get; set; }

    public virtual DbSet<ReligionsTranslation> ReligionsTranslations { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<PromotionsTranslation> PromotionsTranslations { get; set; }

    public virtual DbSet<ConfirmationOption> ConfirmationOptions { get; set; }
    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }


    public virtual DbSet<ContactForm> ContactForms { get; set; }

    public virtual DbSet<HospitalFeature> HospitalFeatures { get; set; }

    public virtual DbSet<HospitalFeatureTranslation> HospitalFeatureTranslations { get; set; }

    //public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<EmployeesStatus> EmployeesStatuses { get; set; }

    public virtual DbSet<EmployeesStatusTranslation> EmployeesStatusTranslations { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }


    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingStatus> BookingStatuses { get; set; }

    public virtual DbSet<BookingStatusesTranslation> BookingStatusesTranslations { get; set; }

    public virtual DbSet<BuildingTranslation> BuildingTranslations { get; set; }

    public virtual DbSet<ClientGroup> ClientGroups { get; set; }


    public virtual DbSet<Clinic> Clinics { get; set; }

    public virtual DbSet<ClinicTranslation> ClinicTranslations { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorTranslation> DoctorTranslations { get; set; }

    public virtual DbSet<DoctorsDegree> DoctorsDegrees { get; set; }

    public virtual DbSet<DoctorsDegreesTranslation> DoctorsDegreesTranslations { get; set; }

    public virtual DbSet<DoctorsWorkHospital> DoctorsWorkHospitals { get; set; }

    public virtual DbSet<FloorTranslation> FloorTranslations { get; set; }

    public virtual DbSet<HosBuilding> HosBuildings { get; set; }

    public virtual DbSet<HosClient> HosClients { get; set; }

    public virtual DbSet<HosFloor> HosFloors { get; set; }

    public virtual DbSet<HosRoom> HosRooms { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }


    public virtual DbSet<HospitalPhoneNumber> HospitalPhoneNumbers { get; set; }

    public virtual DbSet<HospitalTranslation> HospitalTranslations { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Weekday> Weekdays { get; set; }



    public virtual DbSet<MedicalSpecialty> MedicalSpecialties { get; set; }

    public virtual DbSet<MedicalSpecialtyTranslation> MedicalSpecialtyTranslations { get; set; }

    public virtual DbSet<NationalitiesTranslation> NationalitiesTranslations { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientTranslation> PatientTranslations { get; set; }

    public virtual DbSet<DoctorWorkPeriod> DoctorWorkPeriods { get; set; }

    public virtual DbSet<PriceCategory> PriceCategories { get; set; }

    public virtual DbSet<PriceCategoryTranslation> PriceCategoryTranslations { get; set; }

    public virtual DbSet<DoctorVisitPrice> DoctorVisitPrices { get; set; }

    public virtual DbSet<DoctorAttachment> DoctorAttachments { get; set; }

    public virtual DbSet<RoomTranslation> RoomTranslations { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    public virtual DbSet<RoomTypeTranslation> RoomTypeTranslations { get; set; }

    public virtual DbSet<SpecialtiesDoctor> SpecialtiesDoctors { get; set; }

    public virtual DbSet<Ssntype> Ssntypes { get; set; }

    public virtual DbSet<SsntypesTranslation> SsntypesTranslations { get; set; }

    public virtual DbSet<TypesVisit> TypesVisits { get; set; }

    public virtual DbSet<TypesVisitTranslation> TypesVisitTranslations { get; set; }

    public virtual DbSet<WorkingPeriod> WorkingPeriods { get; set; }

    public virtual DbSet<WorkingPeriodTranslation> WorkingPeriodTranslations { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_100_CI_AS_KS_WS_SC_UTF8");

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.ToTable("UserAccount");

            entity.HasIndex(e => e.PhoneNumber, "IX_UserAccount_PhoneNumber");

            entity.HasIndex(e => e.UserName, "IX_UserAccount_UserName");

            entity.HasIndex(e => e.Email, "UK_UserAccount_Email").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UK_UserAccount_PhoneNumber").IsUnique();

            entity.Property(e => e.CallingCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName).HasMaxLength(36);
            entity.Property(e => e.UserReferenceId).HasColumnName("UserReferenceID");
            entity.Property(e => e.VerificationCode)
                .HasMaxLength(8)
                .IsUnicode(false);

            entity.HasOne(d => d.UserReference).WithOne(p => p.UserAccount)
                           .HasForeignKey<UserAccount>(d => d.UserReferenceId)
                           .OnDelete(DeleteBehavior.ClientSetNull)
                           .HasConstraintName("FK_UserAccount_UserReferenceID");
        });

        modelBuilder.Entity<EmployeeAccount>(entity =>
        {
            entity.Property(e => e.CreateOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<ConfirmationOption>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK_ConfirmationOptions");
            entity.Property(e => e.Code)
                .HasMaxLength(450)
                .IsUnicode(false)
                .HasColumnName("Id");
            entity.Property(e => e.OptionName).HasMaxLength(112);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Booking");

            entity.Property(e => e.CreateOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.VisitingDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.BookingReason).HasMaxLength(500);
            entity.Property(e => e.StatusReason).HasMaxLength(500);


            entity.HasOne(d => d.Clinic).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ClinicId)
                .HasConstraintName("FK_Booking_ClinicId");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_Booking_DoctorId");

            entity.HasOne(d => d.Hospital).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.HospitalId)
                .HasConstraintName("FK_Booking_HospitalId");

            entity.HasOne(d => d.Patient).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Booking_PatientId");

            entity.HasOne(d => d.TypeVisit).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TypeVisitId)
                .HasConstraintName("FK_Booking_TypeVisitId");

            //entity.HasOne(d => d.WorkingPeriod).WithMany(p => p.Bookings)
            //    .HasForeignKey(d => d.WorkingPeriodId)
            //    .HasConstraintName("FK_Booking_WorkingPeriodId");
        });

        modelBuilder.Entity<ClientGroup>(entity =>
        {
            entity.HasIndex(e => e.GroupCode, "UK_ClientGroups_GroupCode").IsUnique();

            entity.Property(e => e.GroupCode)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.GroupName).HasMaxLength(50);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientGroups)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_ClientGroups_HosClientId");
        });

        modelBuilder.Entity<Clinic>(entity =>
        {
            entity.HasIndex(e => e.HospitalId, "IX_Clinics_HospitalId");

            entity.HasIndex(e => e.SpecialtyId, "IX_Clinics_SpecialtyId");

            entity.Property(e => e.Appearance)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.CodeNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Photo)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.Reason).HasMaxLength(60);

            entity.HasOne(d => d.Build).WithMany(p => p.Clinics)
                .HasForeignKey(d => d.BuildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clinics_BuildId");

            entity.HasOne(d => d.Floor).WithMany(p => p.Clinics)
                .HasForeignKey(d => d.FloorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clinics_FloorId");

            entity.HasOne(d => d.Hospital).WithMany(p => p.Clinics)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clinics_HospitalId");

            entity.HasOne(d => d.Room).WithMany(p => p.Clinics)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clinics_RoomId");

            entity.HasOne(d => d.Specialty).WithMany(p => p.Clinics)
                .HasForeignKey(d => d.SpecialtyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clinics_SpecialtyId");
        });



        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasIndex(e => e.CurrencyName, "IX_Currencies_CurrencyName");

            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CurrencyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Latitude).HasColumnType("decimal(10, 8)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(10, 8)");
            entity.Property(e => e.Symbol)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmployeesStatus>(entity =>
        {
            entity.ToTable("EmployeesStatus");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<EmployeesStatusTranslation>(entity =>
        {
            entity.HasIndex(e => e.StatusName, "IX_EmployeesStatusTranslations_StatusName");

            entity.HasIndex(e => new { e.EmployeeStatusId, e.LangCode }, "UK_EmployeesStatusTranslations_LangCode_EmployeeStatusId").IsUnique();

            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.StatusName).HasMaxLength(20);

            entity.HasOne(d => d.EmployeeStatus).WithMany(p => p.EmployeesStatusTranslations)
                .HasForeignKey(d => d.EmployeeStatusId)
                .HasConstraintName("FK_EmployeesStatusTranslations_EmployeeStatusId");

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.EmployeesStatusTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_EmployeesStatusTranslations_LangCode");
        });


        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.Property(e => e.CodeNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.IsAppearanceOnSite)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumberAppearance)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Photo)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.Reason).HasMaxLength(60);
            entity.Property(e => e.VisitPriceAppearance)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.HasOne(d => d.DoctorsDegree).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.DoctorsDegreeId)
                .HasConstraintName("FK_Doctors_DoctorsDegreeId");

            //entity.HasOne(d => d.Nationality).WithMany(p => p.Doctors)
            //    .HasForeignKey(d => d.NationalityId)
            //    .HasConstraintName("FK_Doctors_NationalityId");
        });

        modelBuilder.Entity<DoctorTranslation>(entity =>
        {
            entity.HasIndex(e => e.FullName, "IX_DoctorTranslations_FullName");

            entity.HasIndex(e => new { e.DoctorId, e.LangCode }, "UK_DoctorTranslations_LangCode_DoctorId").IsUnique();

            entity.Property(e => e.About).HasMaxLength(300);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Headline).HasMaxLength(100);
            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorTranslations)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_DoctorTranslations_DoctorId");

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.DoctorTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_DoctorTranslations_LangCode");
        });

        modelBuilder.Entity<DoctorsDegree>(entity =>
        {
            entity.ToTable("DoctorsDegrees");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

        });

        modelBuilder.Entity<DoctorsDegreesTranslation>(entity =>
        {
            entity.HasIndex(e => e.DegreeName, "IX_DoctorsDegreesTranslations_DegreeName");

            entity.HasIndex(e => new { e.DoctorDegreeId, e.LangCode }, "UK_DoctorsDegreesTranslations_LangCode_DoctorDegreeId").IsUnique();

            entity.Property(e => e.DegreeName).HasMaxLength(20);
            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);

            entity.HasOne(d => d.DoctorDegree).WithMany(p => p.DoctorsDegreesTranslations)
                .HasForeignKey(d => d.DoctorDegreeId)
                .HasConstraintName("FK_DoctorsDegreesTranslations_DoctorDegreeId");

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.DoctorsDegreesTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_DoctorsDegreesTranslations_LangCode");
        });

        modelBuilder.Entity<DoctorsWorkHospital>(entity =>
        {
            entity.HasKey(e => new { e.DoctorId, e.HospitalId });

            entity.ToTable("DoctorsWorkHospital");

            entity.HasIndex(e => e.DoctorId, "IX_DoctorsWorkHospital_DoctorId");

            entity.HasIndex(e => e.HospitalId, "IX_DoctorsWorkHospital_HospitalId");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            //entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorsWorkHospitals)
            //    .HasForeignKey(d => d.DoctorId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_DoctorsWorkHospital_DoctorId");

            //entity.HasOne(d => d.Hospital).WithMany(p => p.DoctorsWorkHospitals)
            //    .HasForeignKey(d => d.HospitalId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_DoctorsWorkHospital_HospitalId");
        });

        modelBuilder.Entity<SpecialtiesDoctor>(entity =>
        {
            entity.HasKey(e => new { e.DoctorId, e.MedicalSpecialtyId });
            entity.HasIndex(e => e.DoctorId, "IX_SpecialtiesDoctors_DoctorId");

            entity.HasIndex(e => e.MedicalSpecialtyId, "IX_SpecialtiesDoctors_MedicalSpecialtyId");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            //entity.HasOne(d => d.Doctor).WithMany(p => p.SpecialtiesDoctors)
            //    .HasForeignKey(d => d.DoctorId)
            //    .HasConstraintName("FK_MedicalSpecialtiesHospitals_DoctorId");

            //entity.HasOne(d => d.MedicalSpecialty).WithMany(p => p.SpecialtiesDoctors)
            //    .HasForeignKey(d => d.MedicalSpecialtyId)
            //    .HasConstraintName("FK_MedicalSpecialtiesHospitals_MedicalSpecialtyId");
        });


        modelBuilder.Entity<DoctorVisitPrice>(entity =>
        {
            entity.ToTable("DoctorVisitPrices");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.PriceCurrency)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Doctor).WithMany()
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_DoctorVisitPrices_DoctorId");

            entity.HasOne(d => d.PriceCategory).WithMany()
                .HasForeignKey(d => d.PriceCategoryId)
                .HasConstraintName("FK_DoctorVisitPrices_PriceCategoryId");

            entity.HasOne(d => d.TypeVisit).WithMany()
                .HasForeignKey(d => d.TypeVisitId)
                .HasConstraintName("FK_DoctorVisitPrices_TypeVisitId");
        });

        modelBuilder.Entity<DoctorAttachment>(entity =>
        {
            entity.ToTable("DoctorAttachments");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.Property(e => e.DateProduced).HasColumnType("date");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            //entity.HasOne(d => d.Doctor).WithMany()
            //    .HasForeignKey(d => d.DoctorId)
            //    .HasConstraintName("FK_DoctorAttachments_DoctorId");
        });

        modelBuilder.Entity<HosBuilding>(entity =>
        {
            entity.HasIndex(e => e.HospitalId, "IX_HosBuildings_HospitalId");

            entity.Property(e => e.CodeNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Photo)
                .HasMaxLength(55)
                .IsUnicode(false);

            entity.HasOne(d => d.Hospital).WithMany(p => p.HosBuildings)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HosBuildings_HospitalId");
        });

        modelBuilder.Entity<HosClient>(entity =>
        {
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.CodeNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.NameEn).HasMaxLength(50);
            entity.Property(e => e.NameOriginalLang).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Photo)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.Reason).HasMaxLength(60);
            entity.Property(e => e.TelephoneNumber1)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.TelephoneNumber2)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.WhatsAppNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HosFloor>(entity =>
        {
            entity.HasIndex(e => new { e.HospitalId, e.BuildId }, "IX_HosFloors_Hospital_Build");

            entity.Property(e => e.CodeNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Photo)
                .HasMaxLength(55)
                .IsUnicode(false);

            entity.HasOne(d => d.Build).WithMany(p => p.HosFloors)
                .HasForeignKey(d => d.BuildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HosFloors_BuildId");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HosFloors)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HosFloors_HospitalId");
        });

        modelBuilder.Entity<HosRoom>(entity =>
        {
            entity.HasIndex(e => new { e.HospitalId, e.BuildId, e.FloorId }, "IX_HosRooms_Hospital_Build_Floor");

            entity.Property(e => e.CodeNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Kind)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Photo)
                .HasMaxLength(55)
                .IsUnicode(false);

            entity.HasOne(d => d.Build).WithMany(p => p.HosRooms)
                .HasForeignKey(d => d.BuildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HosRooms_BuildId");

            entity.HasOne(d => d.Floor).WithMany(p => p.HosRooms)
                .HasForeignKey(d => d.FloorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HosRooms_FloorId");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HosRooms)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HosRooms_HospitalId");

            entity.HasOne(d => d.RoomType).WithMany(p => p.HosRooms)
                .HasForeignKey(d => d.RoomTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HosRooms_RoomTypeId");
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Hospital");

            entity.Property(e => e.CodeNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Photo)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.WhatsAppNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ContactForm>(entity =>
        {
            entity.ToTable("ContactForm");

            entity.Property(e => e.ContactDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.SenderName).HasMaxLength(100);
            entity.Property(e => e.Subject).HasMaxLength(200);

            entity.HasOne(d => d.Hospital).WithMany(p => p.ContactForms)
                .HasForeignKey(d => d.HospitalId)
                .HasConstraintName("FK_ContactForm_HospitalId");
        });

        modelBuilder.Entity<HospitalFeature>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HospitalFeatures");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Hospital).WithMany(p => p.HospitalFeatures)
                .HasForeignKey(d => d.HospitalId)
                .HasConstraintName("FK_HospitalFeatures_HospitalId");
        });

        modelBuilder.Entity<HospitalFeatureTranslation>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_HospitalFeatureTranslations_Name");

            entity.HasIndex(e => new { e.FeatureId, e.LangCode }, "UK_HospitalFeatureTranslations_LangCode_FeatureId").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Feature).WithMany(p => p.HospitalFeatureTranslations)
                .HasForeignKey(d => d.FeatureId)
                .HasConstraintName("FK_HospitalFeatureTranslations_FeatureId");

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.HospitalFeatureTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_HospitalFeatureTranslations_LangCode");
        });

        modelBuilder.Entity<HospitalPhoneNumber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HospitalsPhoneNumbers");

            entity.HasIndex(e => e.HospitalId, "IX_HospitalsContactData_HospitalId");

            entity.Property(e => e.TelephoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);

            //entity.HasOne(d => d.Hospital).WithMany(p => p.HospitalPhoneNumbers)
            //    .HasForeignKey(d => d.HospitalId)
            //    .HasConstraintName("FK_HospitalsPhoneNumbers_HospitalId");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK_Language");

            entity.Property(e => e.Code)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.LanguageName).HasMaxLength(20);
        });


        modelBuilder.Entity<MedicalSpecialty>(entity =>
        {
            entity.Property(e => e.Appearance).HasDefaultValueSql("((1))");
            entity.Property(e => e.CodeNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Photo)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.Reason).HasMaxLength(60);

            entity.HasMany(d => d.Hospitals).WithMany(p => p.Specialties)
                .UsingEntity<Dictionary<string, object>>(
                    "MedicalSpecialtiesHospital",
                    r => r.HasOne<Hospital>().WithMany()
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MedicalSpecialtiesHospitals_HospitalId"),
                    l => l.HasOne<MedicalSpecialty>().WithMany()
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MedicalSpecialtiesHospitals_SPSpecialtyId"),
                    j =>
                    {
                        j.HasKey("SpecialtyId", "HospitalId");
                        j.ToTable("MedicalSpecialtiesHospitals");
                        j.HasIndex(new[] { "HospitalId" }, "IX_MedicalSpecialtiesHospitals_HospitalId");
                    });
        });



        modelBuilder.Entity<Nationality>(entity =>
        {
            entity.ToTable("Nationalities");
            entity.Property(e => e.Symbol)
                .HasMaxLength(16)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasIndex(e => new { e.ClientId, e.ClientGroupId }, "IX_Patients_Client_ClientGroup");

            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.BloodType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NationalId).HasColumnName("NationalID");
            entity.Property(e => e.Photo)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.Ssn).HasColumnName("SSN");
            entity.Property(e => e.SsntypeId).HasColumnName("SSNTypeId");

            entity.HasOne(d => d.ClientGroup).WithMany(p => p.Patients)
                .HasForeignKey(d => d.ClientGroupId)
                .HasConstraintName("FK_Patients_ClientGroupId");

            entity.HasOne(d => d.Client).WithMany(p => p.Patients)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_Patients_ClientId");

            entity.HasOne(d => d.Country).WithMany(p => p.Patients)
                .HasForeignKey(d => d.NationalityId)
                .HasConstraintName("FK_Patients_CountryId");

            //entity.HasOne(d => d.Gender).WithMany(p => p.Patients)
            //    .HasForeignKey(d => d.GenderId)
            //    .HasConstraintName("FK_Patients_GenderId");

            //entity.HasOne(d => d.MaritalStatus).WithMany(p => p.Patients)
            //    .HasForeignKey(d => d.MaritalStatusId)
            //    .HasConstraintName("FK_Patients_MaritalStatus");

            //entity.HasOne(d => d.Nationality).WithMany(p => p.Patients)
            //    .HasForeignKey(d => d.NationalityId)
            //    .HasConstraintName("FK_Patients_NationalityId");

            //entity.HasOne(d => d.Religion).WithMany(p => p.Patients)
            //    .HasForeignKey(d => d.ReligionId)
            //    .HasConstraintName("FK_Patients_ReligionId");

            //entity.HasOne(d => d.Ssntype).WithMany(p => p.Patients)
            //    .HasForeignKey(d => d.SsntypeId)
            //    .HasConstraintName("FK_Patients_SSNTypeId");
        });

        modelBuilder.Entity<DoctorWorkPeriod>(entity =>
        {
            entity.HasIndex(e => e.DoctorId, "IX_DoctorWorkPeriods_DoctorId");

            entity.HasIndex(e => new { e.DoctorId, e.DayId }, "IX_DoctorWorkPeriods_DoctorId_DayId");

            entity.HasIndex(e => new { e.HospitalId, e.ClinicId, e.DayId }, "IX_DoctorWorkPeriods_Hospital_Clinic_DayId");

            entity.HasIndex(e => new { e.HospitalId, e.SpecialtyId, e.DoctorId, e.ClinicId, e.WorkingPeriodId, e.DayId }, "UK_DoctorWorkPeriods_AllProperty").IsUnique();

            //entity.HasOne(d => d.Clinic).WithMany(p => p.DoctorWorkPeriods)
            //    .HasForeignKey(d => d.ClinicId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_DoctorWorkPeriods_ClinicId");

            //entity.HasOne(d => d.Day).WithMany(p => p.DoctorWorkPeriods)
            //    .HasForeignKey(d => d.DayId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_DoctorWorkPeriods_DayId");

            //entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorWorkPeriods)
            //    .HasForeignKey(d => d.DoctorId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_DoctorWorkPeriods_DoctorId");

            //entity.HasOne(d => d.Hospital).WithMany(p => p.DoctorWorkPeriods)
            //    .HasForeignKey(d => d.HospitalId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_DoctorWorkPeriods_HospitalId");

            //entity.HasOne(d => d.Specialty).WithMany(p => p.DoctorWorkPeriods)
            //    .HasForeignKey(d => d.SpecialtyId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_DoctorWorkPeriods_SpecialtyId");

            //entity.HasOne(d => d.WorkingPeriod).WithMany(p => p.DoctorWorkPeriods)
            //    .HasForeignKey(d => d.WorkingPeriodId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_DoctorWorkPeriods_WorkingPeriodId");
        });

        modelBuilder.Entity<PriceCategory>(entity =>
        {
            entity.Property(e => e.CodeNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Symbol)
                .HasMaxLength(16)
                .IsUnicode(false);
        });



        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.Property(e => e.CodeNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Ssntype>(entity =>
        {
            entity.ToTable("SSNTypes");

            entity.Property(e => e.CodeNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WorkingPeriod>(entity =>
        {
            entity.ToTable("WorkingPeriod");

            entity.Property(e => e.CodeNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.EndTime).HasPrecision(0);
            entity.Property(e => e.StartTime).HasPrecision(0);
        });

        modelBuilder.Entity<TypesVisit>(entity =>
        {
            entity.ToTable("TypesVisit");

            entity.Property(e => e.CodeNumber)
               .HasMaxLength(16)
               .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });


        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.Property(e => e.Photo)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("Photo");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MaritalStatus>(entity =>
        {
            entity.ToTable("MaritalStatus");
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Religion>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Weekday>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        // =========================== translations ===========================


        modelBuilder.Entity<HospitalTranslation>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_HospitalTranslations_Name");

            entity.HasIndex(e => new { e.HospitalId, e.LangCode }, "UK_HospitalTranslations_LangCode_HospitalId").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);

            //entity.HasOne(d => d.Hospital).WithMany(p => p.HospitalTranslations)
            //    .HasForeignKey(d => d.HospitalId)
            //    .HasConstraintName("FK_HospitalTranslations_HospitalId");

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.HospitalTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_HospitalTranslations_LangCode");
        });

        modelBuilder.Entity<BuildingTranslation>(entity =>
        {
            entity.HasIndex(e => e.BuildeingId, "IX_BuildeingTranslations_BuildeingId");

            entity.HasIndex(e => new { e.BuildeingId, e.LangCode }, "UK_BuildingTranslations_LangCode_BuildeingId").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.Buildeing).WithMany(p => p.BuildingTranslations)
            //    .HasForeignKey(d => d.BuildeingId)
            //    .HasConstraintName("FK_BuildeingTranslations_BuildeingId");

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.BuildingTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_BuildeingTranslations_LangCode");
        });

        modelBuilder.Entity<FloorTranslation>(entity =>
        {
            entity.HasIndex(e => new { e.FloorId, e.LangCode }, "UK_FloorTranslations_LangCode_FloorId").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.Floor).WithMany(p => p.FloorTranslations)
            //    .HasForeignKey(d => d.FloorId)
            //    .HasConstraintName("FK_FloorTranslations_FloorId");

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.FloorTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_FloorTranslations_LangCode");
        });

        modelBuilder.Entity<ClinicTranslation>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_ClinicTranslations_Name");

            entity.HasIndex(e => new { e.ClinicId, e.LangCode }, "UK_ClinicTranslations_LangCode_ClinicId").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.Clinic).WithMany(p => p.ClinicTranslations)
            //    .HasForeignKey(d => d.ClinicId)
            //    .HasConstraintName("FK_ClinicTranslations_ClinicId");

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.ClinicTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_ClinicTranslations_LangCode");
        });

        modelBuilder.Entity<RoomTypeTranslation>(entity =>
        {
            entity.HasIndex(e => new { e.RoomTypeId, e.LangCode }, "UK_RoomTypeTranslations_LangCode_RoomTypeId").IsUnique();

            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.RoomTypeTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_RoomTypeTranslations_LangCode");

            //entity.HasOne(d => d.RoomType).WithMany(p => p.RoomTypeTranslations)
            //    .HasForeignKey(d => d.RoomTypeId)
            //    .HasConstraintName("FK_RoomTypeTranslations_RoomTypeId");
        });


        modelBuilder.Entity<PriceCategoryTranslation>(entity =>
        {
            entity.HasIndex(e => new { e.PriceCategoryId, e.LangCode }, "UK_PriceCategoryTranslations_LangCode_PriceCategoryId").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.PriceCategoryTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_PriceCategoryTranslations_LangCode");

            //entity.HasOne(d => d.PriceCategory).WithMany(p => p.PriceCategoryTranslations)
            //    .HasForeignKey(d => d.PriceCategoryId)
            //    .HasConstraintName("FK_PriceCategoryTranslations_PriceCategoryId");
        });

        modelBuilder.Entity<RoomTranslation>(entity =>
        {
            entity.HasIndex(e => new { e.RoomId, e.LangCode }, "UK_RoomTranslations_LangCode_RoomId").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.RoomTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_RoomTranslations_LangCode");

            //entity.HasOne(d => d.Room).WithMany(p => p.RoomTranslations)
            //    .HasForeignKey(d => d.RoomId)
            //    .HasConstraintName("FK_RoomTranslations_RoomId");
        });


        modelBuilder.Entity<TypesVisitTranslation>(entity =>
        {
            entity.HasIndex(e => new { e.TypeVisitId, e.LangCode }, "UK_TypesVisitTranslations_LangCode_TypeVisitId").IsUnique();

            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.TypesVisitTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_TypesVisitTrans_LangCode");

            //entity.HasOne(d => d.TypeVisit).WithMany(p => p.TypesVisitTranslations)
            //    .HasForeignKey(d => d.TypeVisitId)
            //    .HasConstraintName("FK_TypesVisit_TypeVisitId");
        });

        modelBuilder.Entity<WorkingPeriodTranslation>(entity =>
        {
            entity.HasIndex(e => new { e.WorkingPeriodId, e.LangCode }, "UK_WorkingPeriodTranslations_LangCode_WorkingPeriodId").IsUnique();

            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.WorkingPeriodTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_WorkingPeriodTranslations_LangCode");

            //entity.HasOne(d => d.WorkingPeriod).WithMany(p => p.WorkingPeriodTranslations)
            //    .HasForeignKey(d => d.WorkingPeriodId)
            //    .HasConstraintName("FK_WorkingPeriodTranslations_WorkingPeriodId");
        });


        modelBuilder.Entity<MedicalSpecialtyTranslation>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_MedicalSpecialtyTranslations_Name");

            entity.HasIndex(e => new { e.MedicalSpecialtyId, e.LangCode }, "UK_MedicalSpecialtyTranslations_LangCode_MedicalSpecialtyId").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.MedicalSpecialtyTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_MedicalSpecialtyTranslations_LangCode");

            entity.HasOne(d => d.MedicalSpecialty).WithMany(p => p.MedicalSpecialtyTranslations)
                .HasForeignKey(d => d.MedicalSpecialtyId)
                .HasConstraintName("FK_MedicalSpecialtyTranslations_RoomTypeId");
        });

        modelBuilder.Entity<NationalitiesTranslation>(entity =>
        {
            entity.HasIndex(e => new { e.NationalityId, e.LangCode }, "UK_NationalitiesTranslations_LangCode_NationalityId").IsUnique();

            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.NationalitiesTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_NationalitiesTranslations_LangCode");

            //entity.HasOne(d => d.Nationality).WithMany(p => p.NationalitiesTranslations)
            //    .HasForeignKey(d => d.NationalityId)
            //    .HasConstraintName("FK_NationalitiesTranslations_NationalityId");
        });

        modelBuilder.Entity<PatientTranslation>(entity =>
        {
            entity.HasIndex(e => e.FullName, "IX_PatientTranslations_FullName");

            entity.HasIndex(e => new { e.PatientId, e.LangCode }, "UK_PatientTranslations_LangCode_PatientId").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(60);
            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Occupation).HasMaxLength(50);

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.PatientTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_PatientTranslations_LangCode");

            //entity.HasOne(d => d.Patient).WithMany(p => p.PatientTranslations)
            //    .HasForeignKey(d => d.PatientId)
            //    .HasConstraintName("FK_PatientTranslations_PatientId");
        });

        modelBuilder.Entity<SsntypesTranslation>(entity =>
        {
            entity.ToTable("SSNTypesTranslations");

            entity.HasIndex(e => new { e.SsntypeId, e.LangCode }, "UK_SSNTypesTranslations_LangCode_SSNTypeId").IsUnique();

            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.SsntypeId).HasColumnName("SSNTypeId");

            entity.HasOne(d => d.Ssntype).WithMany(p => p.SsntypesTranslations)
                .HasForeignKey(d => d.SsntypeId)
                .HasConstraintName("FK_SSNTypesTranslations_SSNTypeId");
        });


        modelBuilder.Entity<PromotionsTranslation>(entity =>
        {
            entity.HasIndex(e => new { e.PromotionId, e.LangCode }, "UK_Promotions_LangCode_PromotionId").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.PromotionsTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_Promotions_LangCode");

            //entity.HasOne(d => d.Promotion).WithMany(p => p.PromotionsTranslations)
            //    .HasForeignKey(d => d.PromotionId)
            //    .HasConstraintName("FK_Promotions_PromotionId");
        });

        modelBuilder.Entity<ReligionsTranslation>(entity =>
        {
            entity.HasIndex(e => new { e.ReligionId, e.LangCode }, "UK_ReligionsTranslations_LangCode_ReligionId").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.ReligionsTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_ReligionsTranslations_LangCode");

            //entity.HasOne(d => d.Religion).WithMany(p => p.ReligionsTranslations)
            //    .HasForeignKey(d => d.ReligionId)
            //    .HasConstraintName("FK_ReligionsTranslations_ReligionId");
        });


        modelBuilder.Entity<MaritalStatusTranslation>(entity =>
        {
            entity.HasIndex(e => new { e.MaritalId, e.LangCode }, "UK_MaritalStatusTranslations_LangCode_MaritalId").IsUnique();

            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.MaritalStatusTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_MaritalStatusTranslations_LangCode");

            //entity.HasOne(d => d.Marital).WithMany(p => p.MaritalStatusTranslations)
            //    .HasForeignKey(d => d.MaritalId)
            //    .HasConstraintName("FK_MaritalStatusTranslations_MaritalId");
        });

        modelBuilder.Entity<WeekdaysTranslation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WK_WeekdaysTranslations");

            entity.HasIndex(e => new { e.WeekdayId, e.LangCode }, "FK_WeekdaysTranslations_LangCode_WeekdayId").IsUnique();

            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.WeekdaysTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("WK_WeekdaysTranslations_LangCode");

            //entity.HasOne(d => d.Weekday).WithMany(p => p.WeekdaysTranslations)
            //    .HasForeignKey(d => d.WeekdayId)
            //    .HasConstraintName("WK_WeekdaysTranslations_WeekdayId");
        });

        modelBuilder.Entity<GendersTranslation>(entity =>
        {
            entity.HasIndex(e => new { e.GenderId, e.LangCode }, "UK_GendersTranslations_LangCode_GenderId").IsUnique();

            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.Gender).WithMany(p => p.GendersTranslations)
            //    .HasForeignKey(d => d.GenderId)
            //    .HasConstraintName("FK_GendersTranslations_GenderId");

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.GendersTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_GendersTranslations_LangCode");
        });

    }
}
