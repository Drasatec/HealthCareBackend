﻿using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts;

public class AppDbContext : DbContext
{

    public AppDbContext(){}

    public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}

    #region DbSets
    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BuildingTranslation> BuildingTranslations { get; set; }

    public virtual DbSet<ClientGroup> ClientGroups { get; set; }

    public virtual DbSet<ClientsSubscription> ClientsSubscriptions { get; set; }

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

    public virtual DbSet<MainService> MainServices { get; set; }

    public virtual DbSet<MainServiceTranslation> MainServiceTranslations { get; set; }

    public virtual DbSet<MedicalSpecialty> MedicalSpecialties { get; set; }

    public virtual DbSet<MedicalSpecialtyTranslation> MedicalSpecialtyTranslations { get; set; }

    public virtual DbSet<NationalitiesTranslation> NationalitiesTranslations { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientTranslation> PatientTranslations { get; set; }

    public virtual DbSet<PeriodWorkDoctorClinic> PeriodWorkDoctorClinics { get; set; }

    public virtual DbSet<PriceCategory> PriceCategories { get; set; }

    public virtual DbSet<PriceCategoryTranslation> PriceCategoryTranslations { get; set; }

    public virtual DbSet<PriceDoctorVisit> PriceDoctorVisits { get; set; }

    public virtual DbSet<RoomTranslation> RoomTranslations { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    public virtual DbSet<RoomTypeTranslation> RoomTypeTranslations { get; set; }

    public virtual DbSet<SecondaryService> SecondaryServices { get; set; }

    public virtual DbSet<SecondaryServiceTranslation> SecondaryServiceTranslations { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceTranslation> ServiceTranslations { get; set; }

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

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Booking");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VisitingDate).HasColumnType("date");

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
            entity.Property(e => e.Id).ValueGeneratedNever();
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

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorsWorkHospitals)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoctorsWorkHospital_DoctorId");

            entity.HasOne(d => d.Hospital).WithMany(p => p.DoctorsWorkHospitals)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoctorsWorkHospital_HospitalId");
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

        modelBuilder.Entity<MainService>(entity =>
        {
            entity.Property(e => e.Photo)
                .HasMaxLength(55)
                .IsUnicode(false);
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

            entity.HasIndex(e => e.MedicalFileNumber, "IX_Patients_MedicalFileNumber");

            entity.HasIndex(e => e.PhoneNumber, "IX_Patients_PhoneNumber");

            entity.HasIndex(e => e.MedicalFileNumber, "UQ__Patients__31851D959784584E").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.BloodType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.MedicalFileNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.NationalId).HasColumnName("NationalID");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(25)
                .IsUnicode(false);
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

            //entity.HasOne(d => d.Nationality).WithMany(p => p.Patients)
            //    .HasForeignKey(d => d.NationalityId)
            //    .HasConstraintName("FK_Patients_NationalityId");

            //entity.HasOne(d => d.Ssntype).WithMany(p => p.Patients)
            //    .HasForeignKey(d => d.SsntypeId)
            //    .HasConstraintName("FK_Patients_SSNTypeId");
        });

        modelBuilder.Entity<PeriodWorkDoctorClinic>(entity =>
        {
            entity.HasKey(e => new { e.DoctorId, e.ClinicId, e.WorkingPeriodId });

            entity.ToTable("PeriodWorkDoctorClinic");

            entity.HasIndex(e => e.DoctorId, "IX_PeriodWorkDoctorClinic_DoctorId");

            entity.HasIndex(e => new { e.DoctorId, e.OnDay }, "IX_PeriodWorkDoctorClinic_DoctorId_OnDay");

            entity.HasIndex(e => new { e.HospitalId, e.ClinicId, e.OnDay }, "IX_PeriodWorkDoctorClinic_Hospital_Clinic_OnDay");

            entity.HasOne(d => d.Build).WithMany(p => p.PeriodWorkDoctorClinics)
                .HasForeignKey(d => d.BuildId)
                .HasConstraintName("FK_PeriodWorkDoctorClinic_BuildId");

            entity.HasOne(d => d.Clinic).WithMany(p => p.PeriodWorkDoctorClinics)
                .HasForeignKey(d => d.ClinicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PeriodWorkDoctorClinic_ClinicId");

            entity.HasOne(d => d.Doctor).WithMany(p => p.PeriodWorkDoctorClinics)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PeriodWorkDoctorClinic_DoctorId");

            entity.HasOne(d => d.Floor).WithMany(p => p.PeriodWorkDoctorClinics)
                .HasForeignKey(d => d.FloorId)
                .HasConstraintName("FK_PeriodWorkDoctorClinic_FloorId");

            entity.HasOne(d => d.Hospital).WithMany(p => p.PeriodWorkDoctorClinics)
                .HasForeignKey(d => d.HospitalId)
                .HasConstraintName("FK_PeriodWorkDoctorClinic_HospitalId");

            entity.HasOne(d => d.Room).WithMany(p => p.PeriodWorkDoctorClinics)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK_PeriodWorkDoctorClinic_RoomId");

            //entity.HasOne(d => d.WorkingPeriod).WithMany(p => p.PeriodWorkDoctorClinics)
            //    .HasForeignKey(d => d.WorkingPeriodId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_PeriodWorkDoctorClinic_WorkingPeriodId");
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

        modelBuilder.Entity<PriceDoctorVisit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PriceDoctorVisit");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.PriceCurrency)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Doctor).WithMany()
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_PriceDoctorVisit_DoctorId");

            entity.HasOne(d => d.PriceCategory).WithMany()
                .HasForeignKey(d => d.PriceCategoryId)
                .HasConstraintName("FK_PriceDoctorVisit_PriceCategoryId");

            entity.HasOne(d => d.TypeVisit).WithMany()
                .HasForeignKey(d => d.TypeVisitId)
                .HasConstraintName("FK_PriceDoctorVisit_TypeVisitId");
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

        modelBuilder.Entity<SecondaryService>(entity =>
        {
            entity.Property(e => e.Photo)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.MainService).WithMany(p => p.SecondaryServices)
                .HasForeignKey(d => d.MainServiceId)
                .HasConstraintName("FK_SecondaryServices_MainServiceId");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.Property(e => e.Code).HasMaxLength(40);
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

            entity.HasOne(d => d.MainService).WithMany(p => p.Services)
                .HasForeignKey(d => d.MainServiceId)
                .HasConstraintName("FK_Services_MainServiceId");

            entity.HasOne(d => d.SecondaryService).WithMany(p => p.Services)
                .HasForeignKey(d => d.SecondaryServiceId)
                .HasConstraintName("FK_Services_SecondaryServiceId");
        });

        modelBuilder.Entity<ServicePrice>(entity =>
        {
            entity.HasKey(e => new { e.ServiceId, e.PriceCategoryId });

            entity.Property(e => e.Note).HasMaxLength(70);
            entity.Property(e => e.PriceCurrency)
                .HasMaxLength(10)
                .IsUnicode(false);

            //entity.HasOne(d => d.PriceCategory).WithMany(p => p.ServicePrices)
            //    .HasForeignKey(d => d.PriceCategoryId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_ServicePrices_PriceCategoryId");

            //entity.HasOne(d => d.Service).WithMany(p => p.ServicePrices)
            //    .HasForeignKey(d => d.ServiceId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_ServicePrices_ServiceId");
        });



        modelBuilder.Entity<SpecialtiesDoctor>(entity =>
        {
            entity.HasIndex(e => e.DoctorId, "IX_SpecialtiesDoctors_DoctorId");

            entity.HasIndex(e => e.MedicalSpecialtyId, "IX_SpecialtiesDoctors_MedicalSpecialtyId");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            //entity.HasOne(d => d.Doctor).WithMany(p => p.SpecialtiesDoctors)
            //    .HasForeignKey(d => d.DoctorId)
            //    .HasConstraintName("FK_MedicalSpecialtiesHospitals_DoctorId");

            entity.HasOne(d => d.MedicalSpecialty).WithMany(p => p.SpecialtiesDoctors)
                .HasForeignKey(d => d.MedicalSpecialtyId)
                .HasConstraintName("FK_MedicalSpecialtiesHospitals_MedicalSpecialtyId");
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

        modelBuilder.Entity<ClientsSubscription>(entity =>
        {
            entity.HasKey(e => new { e.ClientId, e.ClientGroupId });

            entity.ToTable("ClientsSubscription");

            entity.HasIndex(e => e.ClientId, "IX_ClientsSubscription_ClientId");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Duration).HasColumnType("date");
            entity.Property(e => e.Notes).HasMaxLength(100);
            entity.Property(e => e.PriceCurrency)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Specification).HasMaxLength(100);
            entity.Property(e => e.StartDate).HasPrecision(0);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.ClientGroup).WithMany(p => p.ClientsSubscriptions)
                .HasForeignKey(d => d.ClientGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientsSubscription_ClientGroupId");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientsSubscriptions)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientsSubscription_ClientId");

            //entity.HasOne(d => d.PriceCategory).WithMany(p => p.ClientsSubscriptions)
            //    .HasForeignKey(d => d.PriceCategoryId)
            //    .HasConstraintName("FK_ClientsSubscription_PriceCategoryId");
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

        modelBuilder.Entity<SecondaryServiceTranslation>(entity =>
        {
            entity.HasIndex(e => new { e.SecondaryServiceId, e.LangCode }, "UK_SecondaryServiceTranslations_LangCode_SecondaryServiceId").IsUnique();

            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.SecondaryServiceTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_SecondaryServiceTranslations_LangCode");

            //entity.HasOne(d => d.SecondaryService).WithMany(p => p.SecondaryServiceTranslations)
            //    .HasForeignKey(d => d.SecondaryServiceId)
            //    .HasConstraintName("FK_SecondaryServiceTranslations_SecondaryServiceId");
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

        modelBuilder.Entity<ServiceTranslation>(entity =>
        {
            entity.HasIndex(e => new { e.ServiceId, e.LangCode }, "UK_ServiceTranslations_LangCode_ServiceId").IsUnique();

            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.ServiceTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_ServiceTranslations_LangCode");

            //entity.HasOne(d => d.Service).WithMany(p => p.ServiceTranslations)
            //    .HasForeignKey(d => d.ServiceId)
            //    .HasConstraintName("FK_ServiceTranslations_ServiceId");
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

        modelBuilder.Entity<MainServiceTranslation>(entity =>
        {
            entity.HasIndex(e => new { e.MainServiceId, e.LangCode }, "UK_MainServiceTranslations_LangCode_MainServiceId").IsUnique();

            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(30);

            //entity.HasOne(d => d.LangCodeNavigation).WithMany(p => p.MainServiceTranslations)
            //    .HasForeignKey(d => d.LangCode)
            //    .HasConstraintName("FK_MainServiceTranslations_LangCode");

            //entity.HasOne(d => d.MainService).WithMany(p => p.MainServiceTranslations)
            //    .HasForeignKey(d => d.MainServiceId)
            //    .HasConstraintName("FK_MainServiceTranslations_MainServiceId");
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

            entity.Property(e => e.Employer).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(60);
            entity.Property(e => e.LangCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.Religion).HasMaxLength(50);

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

        //OnModelCreatingPartial(modelBuilder);
    }

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
