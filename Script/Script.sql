
-- USE master;
-- IF NOT EXISTS(SELECT name FROM sys.databases WHERE name = 'alrahma_care_db')
-- BEGIN
--     CREATE DATABASE alrahma_care_db COLLATE Arabic_100_CI_AS_KS_WS_SC_UTF8;
-- END
-- GO
-- use alrahma_care_db;
--GO
CREATE TABLE Languages
(
    Code VARCHAR(6),
    LanguageName NVARCHAR(20),
	CONSTRAINT PK_Language PRIMARY KEY (Code)
);
GO
INSERT INTO Languages VALUES('ar','عربي'),('en','English'),('fr','française'),('es','España');
GO
----------------
CREATE TABLE Weekdays
(
    Id INT IDENTITY(1,1),
    DayNumber TINYINT NOT NULL,
    WeekdayName NVARCHAR(20) NOT NULL,
    LangCode VARCHAR(6) NOT NULL,

    CONSTRAINT PK_Weekdays PRIMARY KEY (Id),
    CONSTRAINT UK_Weekdays_DayNumber_LangCode UNIQUE (DayNumber, LangCode),
    CONSTRAINT CHECK_Weekdays_DayNumber CHECK(DayNumber between 1 and 7),

    CONSTRAINT FK_Weekdays_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
	INDEX IX_Weekdays_DayNumber NONCLUSTERED (DayNumber)
);
GO
INSERT INTO Weekdays VALUES(1,N'سبت','ar'),(1,'Saturday','en'),(1,'Samedi','fr'),(1,'saa','es');
INSERT INTO Weekdays VALUES(2,N'الأحد','ar'),(2,'Sunday','en'),(2,'Dimanche','fr'),(2,'suu','es');
INSERT INTO Weekdays VALUES(3,N'الإثنين','ar'),(3,'Monday','en'),(3,'Lundi','fr'),(3,'moo','es');
INSERT INTO Weekdays VALUES(4,N'الثلاثاء','ar'),(5,N'الأربعاء','ar'),(6,N'الخميس','ar'),(7,N'الجمعة','ar')
GO
----------------
CREATE TABLE Genders
(
    Id INT IDENTITY(1,1),
    GenderNumber TINYINT NOT NULL,
    GenderName NVARCHAR(20) NOT NULL,
    LangCode VARCHAR(6) NOT NULL,

    CONSTRAINT PK_Genders PRIMARY KEY (Id),
    CONSTRAINT UK_Genders_GenderNumber_LangCode UNIQUE (GenderNumber, LangCode),
    CONSTRAINT CHECK_Genders_GenderNumber CHECK(GenderNumber between 1 and 3),

    CONSTRAINT FK_Genders_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
	INDEX IX_Genders_GenderNumber NONCLUSTERED (GenderNumber)
);
GO
----------------
CREATE TABLE Currencies
(
    Id INT IDENTITY(1,1),
    CurrencyCode VARCHAR(3) NOT NULL ,-- USA
    CurrencyName VARCHAR(50) NOT NULL, --US Dollar
    Symbol VARCHAR(10) NOT NULL, -- $
    country VARCHAR(50) NOT NULL, -- United States
    Longitude DECIMAL(12, 9),
    Latitude DECIMAL(12, 9),

    CONSTRAINT PK_Currencies PRIMARY KEY (Id),
    CONSTRAINT UK_Currencies_CurrencyCode UNIQUE (CurrencyCode),
	INDEX IX_Currencies_CurrencyName NONCLUSTERED (CurrencyName)
);
GO
----------------
CREATE TABLE Hospitals
(
    Id INT IDENTITY (1,1),
    Photo VARCHAR(55),
	CodeNumber VARCHAR(16),
    Email VARCHAR(40),
    WhatsAppNumber VARCHAR(15) ,
	CreateOn DATETIME DEFAULT GETDATE(),
	IsDeleted BIT NOT NULL DEFAULT 0,
    Longitude DECIMAL(12, 9),
    Latitude DECIMAL(12, 9),
	CONSTRAINT PK_Hospital PRIMARY KEY (Id)
);
GO
CREATE TABLE HospitalPhoneNumbers
(
	Id INT IDENTITY (1,1),
	TelephoneNumber VARCHAR(15) ,
	HospitalId INT,
	CONSTRAINT PK_HospitalsPhoneNumbers PRIMARY KEY (Id),
	CONSTRAINT FK_HospitalsPhoneNumbers_HospitalId
    FOREIGN KEY (HospitalId)
      REFERENCES Hospitals(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    INDEX IX_HospitalsContactData_HospitalId NONCLUSTERED (HospitalId)    
);
GO
CREATE TABLE HospitalTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(50),
    Address NVARCHAR(50),
    Description NVARCHAR(300),
    HospitalId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_HospitalTranslations PRIMARY KEY (Id),
	CONSTRAINT UK_HospitalTranslations_LangCode_HospitalId UNIQUE (HospitalId, LangCode),

	CONSTRAINT FK_HospitalTranslations_HospitalId
    FOREIGN KEY (HospitalId)
      REFERENCES Hospitals(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_HospitalTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	INDEX IX_HospitalTranslations_Name NONCLUSTERED (Name)
);
GO
CREATE TABLE ContactForm (
    Id INT IDENTITY (1, 1),
    SenderName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Subject NVARCHAR(200) NOT NULL,
    Message NVARCHAR(MAX) NOT NULL,
    ContactDate DATETIME DEFAULT GETDATE(),
    HospitalId INT,

	CONSTRAINT PK_ContactForm PRIMARY KEY (Id),

    CONSTRAINT FK_ContactForm_HospitalId
    FOREIGN KEY (HospitalId)
      REFERENCES Hospitals(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
);
GO
CREATE TABLE HospitalFeatures
(
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    Photo VARCHAR(255),
    CreateOn DATETIME DEFAULT GETDATE(),
    HospitalId INT,
    CONSTRAINT FK_HospitalFeatures_HospitalId
    FOREIGN KEY (HospitalId)
      REFERENCES Hospitals(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
);
CREATE TABLE HospitalFeatureTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(100),
    Description NVARCHAR(500),
    FeatureId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_HospitalFeatureTranslations PRIMARY KEY (Id),
	CONSTRAINT UK_HospitalFeatureTranslations_LangCode_FeatureId UNIQUE (FeatureId, LangCode),

	CONSTRAINT FK_HospitalFeatureTranslations_FeatureId
    FOREIGN KEY (FeatureId)
      REFERENCES HospitalFeatures(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_HospitalFeatureTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	INDEX IX_HospitalFeatureTranslations_Name NONCLUSTERED (Name)
);

-- CREATE TABLE HospitalCoordinates
-- (
--     Id INT IDENTITY(1,1),
--     HospitalName NVARCHAR(100),
--     Address NVARCHAR(200),
--     PhoneNumber VARCHAR(25),
--     Longitude DECIMAL(12, 9),
--     Latitude DECIMAL(12, 9),

--     CONSTRAINT PK_HospitalArrivalData PRIMARY KEY (Id),
--     --CONSTRAINT UNQ_HospitalArrivalData_HospitalName_Address UNIQUE (HospitalName, Address)

--     CONSTRAINT FK_HospitalTranslations_HospitalId
--     FOREIGN KEY (HospitalId)
--       REFERENCES Hospitals(Id)
-- 		ON DELETE NO ACtion ON UPDATE NO ACTION,
-- );

GO
----------------
CREATE TABLE HosBuildings
(
    Id INT IDENTITY (1,1),
	CodeNumber VARCHAR(16),
	Photo VARCHAR(55),
	CreateOn DATETIME DEFAULT GETDATE(),
	IsDeleted BIT,
    HospitalId INT NOT NULL,

	CONSTRAINT PK_HosBuildings PRIMARY KEY (Id),
	CONSTRAINT FK_HosBuildings_HospitalId
    FOREIGN KEY (HospitalId)
      REFERENCES Hospitals(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	INDEX IX_HosBuildings_HospitalId NONCLUSTERED (HospitalId)
);
GO
CREATE TABLE BuildingTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(30),
	Description NVARCHAR(100),
    BuildeingId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_BuildingTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_BuildingTranslations_LangCode_BuildeingId UNIQUE (BuildeingId, LangCode),

	CONSTRAINT FK_BuildeingTranslations_BuildeingId
    FOREIGN KEY (BuildeingId)
      REFERENCES HosBuildings(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_BuildeingTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	INDEX IX_BuildeingTranslations_BuildeingId NONCLUSTERED (BuildeingId)
);
GO
----------------
CREATE TABLE HosFloors
(
    Id INT IDENTITY (1,1),
	CodeNumber VARCHAR(16),
    Photo VARCHAR(55),
	CreateOn DATETIME DEFAULT GETDATE(),
	IsDeleted BIT,
    HospitalId INT NOT NULL,
    BuildId INT NOT NULL,

	CONSTRAINT PK_HosFloors PRIMARY KEY (Id),

    CONSTRAINT FK_HosFloors_HospitalId
    FOREIGN KEY (HospitalId)
      REFERENCES Hospitals(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_HosFloors_BuildId
    FOREIGN KEY (BuildId)
      REFERENCES HosBuildings(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
	
    INDEX IX_HosFloors_Hospital_Build (HospitalId,BuildId)
);
GO
CREATE TABLE FloorTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(30),
	Description NVARCHAR(100),
    FloorId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_FloorTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_FloorTranslations_LangCode_FloorId UNIQUE (FloorId, LangCode),

	CONSTRAINT FK_FloorTranslations_FloorId
    FOREIGN KEY (FloorId)
      REFERENCES HosFloors(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_FloorTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION
);
GO
----------------
CREATE TABLE RoomTypes
(
    Id INT IDENTITY (1,1),
	CodeNumber VARCHAR(16),
	CreateOn DATETIME DEFAULT GETDATE(),
	IsDeleted BIT,
	CONSTRAINT PK_RoomTypes PRIMARY KEY (Id),
);
GO
CREATE TABLE RoomTypeTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(30),
    RoomTypeId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_RoomTypeTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_RoomTypeTranslations_LangCode_RoomTypeId UNIQUE (RoomTypeId, LangCode),

	CONSTRAINT FK_RoomTypeTranslations_RoomTypeId
    FOREIGN KEY (RoomTypeId)
      REFERENCES RoomTypes(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_RoomTypeTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION
);
GO
----------------
CREATE TABLE HosRooms
(
    Id INT IDENTITY (1,1),  
    Photo VARCHAR(55),
	CodeNumber VARCHAR(16),
    Kind VARCHAR(16), -- ==Type  --(Clinic - inpatient - operations - laboratory - x-rays - pharmacy - office)
    IsActive BIT NOT NULL DEFAULT 1,  -- (Active, inactive) -- update1
	IsDeleted BIT,
    HospitalId INT NOT NULL,
    BuildId INT NOT NULL,
    FloorId INT NOT NULL,
    RoomTypeId INT NOT NULL,

	CONSTRAINT PK_HosRooms PRIMARY KEY (Id),

    CONSTRAINT FK_HosRooms_HospitalId
    FOREIGN KEY (HospitalId)
      REFERENCES Hospitals(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_HosRooms_BuildId
    FOREIGN KEY (BuildId)
      REFERENCES HosBuildings(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_HosRooms_FloorId
    FOREIGN KEY (FloorId)
      REFERENCES HosFloors(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_HosRooms_RoomTypeId
    FOREIGN KEY (RoomTypeId)
      REFERENCES RoomTypes(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
    
    INDEX IX_HosRooms_Hospital_Build_Floor NONCLUSTERED (HospitalId,BuildId,FloorId)
);
GO
CREATE TABLE RoomTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(30),
	Description NVARCHAR(100),
    RoomId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_RoomTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_RoomTranslations_LangCode_RoomId UNIQUE (RoomId, LangCode),

	CONSTRAINT FK_RoomTranslations_RoomId
    FOREIGN KEY (RoomId)
      REFERENCES HosRooms(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_RoomTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE CASCADE
);
GO
----------------
CREATE TABLE MedicalSpecialties
(
    Id INT IDENTITY (1,1),
	CodeNumber VARCHAR(16),
    IsActive BIT NOT NULL DEFAULT 1,  -- (Active, inactive) -- update1
    IsDeleted BIT NOT NULL DEFAULT 0,
	Reason NVARCHAR(60),
    Appearance BIT DEFAULT 1,
    Photo VARCHAR(55),

	CONSTRAINT PK_MedicalSpecialties PRIMARY KEY (Id),

);
GO
CREATE TABLE MedicalSpecialtyTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(30),
	Description NVARCHAR(100),
    MedicalSpecialtyId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_MedicalSpecialtyTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_MedicalSpecialtyTranslations_LangCode_MedicalSpecialtyId UNIQUE (MedicalSpecialtyId, LangCode),

	CONSTRAINT FK_MedicalSpecialtyTranslations_RoomTypeId
    FOREIGN KEY (MedicalSpecialtyId)
      REFERENCES MedicalSpecialties(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_MedicalSpecialtyTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
    
    INDEX IX_MedicalSpecialtyTranslations_Name NONCLUSTERED (Name)

);
GO
----------------
CREATE TABLE MedicalSpecialtiesHospitals --MM
(
    SpecialtyId INT,
    HospitalId INT,

	CONSTRAINT PK_MedicalSpecialtiesHospitals PRIMARY KEY (SpecialtyId,HospitalId),

	CONSTRAINT FK_MedicalSpecialtiesHospitals_SPSpecialtyId
	FOREIGN KEY (SpecialtyId)
		REFERENCES MedicalSpecialties(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_MedicalSpecialtiesHospitals_HospitalId
	FOREIGN KEY (HospitalId)
		REFERENCES HospitalS(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
    
    INDEX IX_MedicalSpecialtiesHospitals_HospitalId NONCLUSTERED (HospitalId)
);
GO
----------------
CREATE TABLE Clinics
(
    Id INT IDENTITY (1,1),
    CodeNumber VARCHAR(16),
    Photo VARCHAR(55),
    IsActive BIT NOT NULL DEFAULT 1,  -- (Active, inactive) -- update1
    IsDeleted BIT NOT NULL DEFAULT 0,
    Reason NVARCHAR(60), 
    Phone VARCHAR(15) ,
    WorkingHours SMALLINT,
    Appearance  BIT NOT NULL DEFAULT 1,
    
    HospitalId INT NOT NULL,
    BuildId INT NOT NULL,
    FloorId INT NOT NULL,
    RoomId INT NOT NULL,
    SpecialtyId INT NOT NULL,
    
	CONSTRAINT PK_Clinics PRIMARY KEY (Id),

        CONSTRAINT FK_Clinics_HospitalId
    FOREIGN KEY (HospitalId)
      REFERENCES Hospitals(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_Clinics_BuildId
    FOREIGN KEY (BuildId)
      REFERENCES HosBuildings(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_Clinics_FloorId
    FOREIGN KEY (FloorId)
      REFERENCES HosFloors(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
    
	CONSTRAINT FK_Clinics_RoomId
    FOREIGN KEY (RoomId)
      REFERENCES HosRooms(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_Clinics_SpecialtyId
    FOREIGN KEY (SpecialtyId)
      REFERENCES MedicalSpecialties(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    INDEX IX_Clinics_SpecialtyId NONCLUSTERED (SpecialtyId),
    INDEX IX_Clinics_HospitalId NONCLUSTERED (HospitalId)
);
GO
CREATE TABLE ClinicTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(30),
	Description NVARCHAR(100),
    ClinicId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_ClinicTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_ClinicTranslations_LangCode_ClinicId UNIQUE (ClinicId, LangCode),

	CONSTRAINT FK_ClinicTranslations_ClinicId
    FOREIGN KEY (ClinicId)
      REFERENCES Clinics(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_ClinicTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
    
    INDEX IX_ClinicTranslations_Name NONCLUSTERED (Name)

);
GO
----------------
CREATE TABLE SSNTypes
(
    Id INT IDENTITY (1,1),
    CodeNumber VARCHAR(16),
    CONSTRAINT PK_SSNTypes PRIMARY KEY (Id),
);
GO
CREATE TABLE SSNTypesTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(30),
    SSNTypeId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_SSNTypesTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_SSNTypesTranslations_LangCode_SSNTypeId UNIQUE (SSNTypeId, LangCode),

	CONSTRAINT FK_SSNTypesTranslations_SSNTypeId
    FOREIGN KEY (SSNTypeId)
      REFERENCES SSNTypes(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_SSNTypesTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION
);
GO
----------------
CREATE TABLE WorkingPeriod
(
    Id INT IDENTITY (1,1),
    CodeNumber VARCHAR(16), 
    StartTime TIME(0),
    EndTime TIME(0),
	CONSTRAINT PK_WorkingPeriod PRIMARY KEY (Id),
);
GO
CREATE TABLE WorkingPeriodTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(30),
    WorkingPeriodId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_WorkingPeriodTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_WorkingPeriodTranslations_LangCode_WorkingPeriodId UNIQUE (WorkingPeriodId, LangCode),

	CONSTRAINT FK_WorkingPeriodTranslations_WorkingPeriodId
    FOREIGN KEY (WorkingPeriodId)
      REFERENCES WorkingPeriod(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_WorkingPeriodTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION
);
GO
---------------- 
CREATE TABLE PriceCategories
(
    Id INT IDENTITY (1,1),
    CodeNumber VARCHAR(16), 
    CreateOn DATETIME DEFAULT GETDATE(),
    Symbol VARCHAR (16),
	CONSTRAINT PK_PriceCategories PRIMARY KEY (Id),
);
GO
CREATE TABLE PriceCategoryTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(30),
    PriceCategoryId INT,
	Description NVARCHAR(100),
    LangCode VARCHAR(6),

	CONSTRAINT PK_PriceCategoryTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_PriceCategoryTranslations_LangCode_PriceCategoryId UNIQUE (PriceCategoryId, LangCode),

	CONSTRAINT FK_PriceCategoryTranslations_PriceCategoryId
    FOREIGN KEY (PriceCategoryId)
      REFERENCES PriceCategories(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_PriceCategoryTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION
);
GO
----------------

CREATE TABLE TypesVisit 
(
    Id INT IDENTITY (1,1),
	CodeNumber VARCHAR(16),
	CreateOn DATETIME DEFAULT GETDATE(),
	IsDeleted BIT,
	CONSTRAINT PK_TypesVisit PRIMARY KEY (Id),
)
CREATE TABLE TypesVisitTranslations --  1M
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(30),
	TypeVisitId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_TypesVisitTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_TypesVisitTranslations_LangCode_TypeVisitId UNIQUE (TypeVisitId, LangCode),

	CONSTRAINT FK_TypesVisit_TypeVisitId
    FOREIGN KEY (TypeVisitId)
      REFERENCES TypesVisit(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_TypesVisitTrans_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION
);
GO
----------------
CREATE TABLE Nationalities
(
    Id INT  IDENTITY (1,1),
    Symbol VARCHAR (16),
    CONSTRAINT PK_Nationalities PRIMARY KEY (Id),
);
GO
---------------- 
CREATE TABLE NationalitiesTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(30),
    NationalityId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_NationalitiesTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_NationalitiesTranslations_LangCode_NationalityId UNIQUE (NationalityId, LangCode),

	CONSTRAINT FK_NationalitiesTranslations_NationalityId
    FOREIGN KEY (NationalityId)
      REFERENCES Nationalities(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_NationalitiesTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION
);
GO
----------------
CREATE TABLE DoctorsDegrees
(
    Id SMALLINT IDENTITY (1,1),
	CreateOn DATETIME DEFAULT GETDATE(),
    CONSTRAINT PK_DoctorsDegrees PRIMARY KEY (Id),
);
GO
----------------
CREATE TABLE DoctorsDegreesTranslations --MM
(
    Id INT IDENTITY (1,1),
    DegreeName NVARCHAR(20), --(general - specialist - consultant - university professor)
    DoctorDegreeId SMALLINT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_DoctorsDegreesTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_DoctorsDegreesTranslations_LangCode_DoctorDegreeId UNIQUE (DoctorDegreeId, LangCode),

	CONSTRAINT FK_DoctorsDegreesTranslations_DoctorDegreeId
    FOREIGN KEY (DoctorDegreeId)
      REFERENCES DoctorsDegrees(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_DoctorsDegreesTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    INDEX IX_DoctorsDegreesTranslations_DegreeName NONCLUSTERED (DegreeName)
);
GO
----------------
CREATE TABLE EmployeesStatus
(
    Id SMALLINT IDENTITY (1,1),
	CreateOn DATETIME DEFAULT GETDATE(),
    CONSTRAINT PK_EmployeesStatus PRIMARY KEY (Id),
);
GO
----------------
CREATE TABLE EmployeesStatusTranslations --MM
(
    Id INT IDENTITY (1,1),
    StatusName NVARCHAR(20),
    EmployeeStatusId SMALLINT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_EmployeesStatusTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_EmployeesStatusTranslations_LangCode_EmployeeStatusId UNIQUE (EmployeeStatusId, LangCode),

	CONSTRAINT FK_EmployeesStatusTranslations_EmployeeStatusId
    FOREIGN KEY (EmployeeStatusId)
      REFERENCES EmployeesStatus(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_EmployeesStatusTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    INDEX IX_EmployeesStatusTranslations_StatusName NONCLUSTERED (StatusName)
);
GO
----------------
CREATE TABLE Doctors
(
    Id INT IDENTITY (1,1),
    CodeNumber VARCHAR(16), 
	gender TINYINT,
    Photo VARCHAR(55),
    WorkingHours TINYINT,
    IsDeleted BIT NOT NULL DEFAULT 0,
    DocStatus TINYINT, -- active=0  inactive=1  standing=2
    Reason NVARCHAR(60),
    PhoneNumber VARCHAR(25),
    IsAppearanceOnSite BIT NOT NULL DEFAULT 1, --(yes/no)
    PhoneNumberAppearance BIT NOT NULL DEFAULT 1,
    VisitPriceAppearance BIT NOT NULL DEFAULT 1,
    DoctorsDegreeId SMALLINT,  
    NationalityId INT,

    CONSTRAINT CHECK_DoctorSex CHECK(gender between 1 and 2), -- mal = 1
	CONSTRAINT PK_Doctors PRIMARY KEY (Id),

    CONSTRAINT FK_Doctors_DoctorsDegreeId
    FOREIGN KEY (DoctorsDegreeId)
      REFERENCES DoctorsDegrees(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
    
    CONSTRAINT FK_Doctors_NationalityId
    FOREIGN KEY (NationalityId)
      REFERENCES Nationalities(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

);
GO
CREATE TABLE DoctorTranslations --MM
(
    Id INT IDENTITY (1,1),
    FullName NVARCHAR(100),
    Headline NVARCHAR(100),
    About NVARCHAR(300),
    DoctorId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_DoctorTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_DoctorTranslations_LangCode_DoctorId UNIQUE (DoctorId, LangCode),

	CONSTRAINT FK_DoctorTranslations_DoctorId
    FOREIGN KEY (DoctorId)
      REFERENCES Doctors(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_DoctorTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    INDEX IX_DoctorTranslations_FullName NONCLUSTERED (FullName)
);
GO
---------------- 
CREATE TABLE DoctorVisitPrices --MM
(
    Id INT IDENTITY(1,1),
    Price INT,
    PriceCurrency VARCHAR(10),

    DoctorId INT,
    PriceCategoryId INT,
    TypeVisitId INT,

    CONSTRAINT PK_DoctorVisitPrices PRIMARY KEY (Id),
    CONSTRAINT UK_DoctorVisitPrices_DOCid_PCid_TVid UNIQUE (DoctorId, PriceCategoryId,TypeVisitId),

    CONSTRAINT FK_DoctorVisitPrices_DoctorId
    FOREIGN KEY (DoctorId)
      REFERENCES Doctors(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,


    CONSTRAINT FK_DoctorVisitPrices_PriceCategoryId
    FOREIGN KEY (PriceCategoryId)
      REFERENCES PriceCategories(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    CONSTRAINT FK_DoctorVisitPrices_TypeVisitId
    FOREIGN KEY (TypeVisitId)
      REFERENCES TypesVisit(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

);
GO
----------------
CREATE TABLE DoctorsWorkHospital--MM
(
    DoctorId INT,
    HospitalId INT,
    CreateOn DATETIME DEFAULT GETDATE(),

    CONSTRAINT PK_DoctorsWorkHospital PRIMARY KEY (DoctorId,HospitalId),

	CONSTRAINT FK_DoctorsWorkHospital_DoctorId
	FOREIGN KEY (DoctorId)
		REFERENCES Doctors(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_DoctorsWorkHospital_HospitalId
	FOREIGN KEY (HospitalId)
		REFERENCES HospitalS(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
    
    INDEX IX_DoctorsWorkHospital_DoctorId NONCLUSTERED (DoctorId),
    INDEX IX_DoctorsWorkHospital_HospitalId NONCLUSTERED (HospitalId),
);
GO
---------------- 
CREATE TABLE SpecialtiesDoctors --MM
(
    Id INT IDENTITY(1,1),
    DoctorId INT,
    MedicalSpecialtyId INT,
    CreateOn DATETIME DEFAULT GETDATE(),

    CONSTRAINT PK_SpecialtiesDoctors PRIMARY KEY (Id),

	CONSTRAINT FK_MedicalSpecialtiesHospitals_DoctorId
	FOREIGN KEY (DoctorId)
		REFERENCES Doctors(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_MedicalSpecialtiesHospitals_MedicalSpecialtyId
	FOREIGN KEY (MedicalSpecialtyId)
		REFERENCES MedicalSpecialties(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    INDEX IX_SpecialtiesDoctors_DoctorId NONCLUSTERED (DoctorId),
    INDEX IX_SpecialtiesDoctors_MedicalSpecialtyId NONCLUSTERED (MedicalSpecialtyId),

);
GO
----------------
CREATE TABLE PeriodWorkDoctorClinic --MM
(
    Id INT IDENTITY(1,1),
    HospitalId INT,
    ClinicId INT,
    DoctorId INT,
    WorkingPeriodId INT,
    OnDay TINYINT,

    CONSTRAINT PK_PeriodWorkDoctorClinic PRIMARY KEY (Id),
    CONSTRAINT UK_PeriodWorkDoctorClinic_AllProperty UNIQUE (HospitalId,DoctorId,ClinicId,WorkingPeriodId,OnDay),
    CONSTRAINT CHECK_PeriodWorkDoctorClinic_OnDay CHECK(OnDay between 1 and 7),

    CONSTRAINT FK_PeriodWorkDoctorClinic_HospitalId
    FOREIGN KEY (HospitalId)
      REFERENCES Hospitals(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_PeriodWorkDoctorClinic_DoctorId
    FOREIGN KEY (DoctorId)
      REFERENCES Doctors(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_PeriodWorkDoctorClinic_ClinicId
    FOREIGN KEY (ClinicId)
      REFERENCES Clinics(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_PeriodWorkDoctorClinic_WorkingPeriodId
    FOREIGN KEY (WorkingPeriodId)
      REFERENCES WorkingPeriod(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
    
    INDEX IX_PeriodWorkDoctorClinic_Hospital_Clinic_OnDay NONCLUSTERED (HospitalId,ClinicId,OnDay),
    INDEX IX_PeriodWorkDoctorClinic_DoctorId_OnDay NONCLUSTERED (DoctorId,OnDay),
    INDEX IX_PeriodWorkDoctorClinic_DoctorId NONCLUSTERED (DoctorId)
);
GO
---------------- 
CREATE TABLE DoctorAttachments
(
    Id INT IDENTITY (1,1),
    AttachFileName VARCHAR(60),
    Title VARCHAR(100),
    DateProduced DATE,
    CreateOn DATETIME DEFAULT GETDATE(),

    DoctorId INT NOT NULL,

	CONSTRAINT PK_DoctorAttachments PRIMARY KEY (Id),

    CONSTRAINT FK_DoctorAttachments_DoctorId
    FOREIGN KEY (DoctorId)
      REFERENCES Doctors(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

);
GO
---------------- 
CREATE TABLE MainServices
(
    Id INT IDENTITY (1,1),
    Photo VARCHAR(55),
    CONSTRAINT PK_MainServices PRIMARY KEY (Id)
);
GO
CREATE TABLE MainServiceTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(30),
    MainServiceId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_MainServiceTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_MainServiceTranslations_LangCode_MainServiceId UNIQUE (MainServiceId, LangCode),

	CONSTRAINT FK_MainServiceTranslations_MainServiceId
    FOREIGN KEY (MainServiceId)
      REFERENCES MainServices(Id) 
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_MainServiceTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION


);
GO
----------------
CREATE TABLE SecondaryServices
(
    Id INT IDENTITY (1,1),  
    Photo VARCHAR(40),
    MainServiceId INT,

	CONSTRAINT PK_SecondaryServices PRIMARY KEY (Id),
    
    CONSTRAINT FK_SecondaryServices_MainServiceId
    FOREIGN KEY (MainServiceId)
      REFERENCES MainServices(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION

);
CREATE TABLE SecondaryServiceTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(30),
    SecondaryServiceId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_SecondaryServiceTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_SecondaryServiceTranslations_LangCode_SecondaryServiceId UNIQUE (SecondaryServiceId, LangCode),

	CONSTRAINT FK_SecondaryServiceTranslations_SecondaryServiceId
    FOREIGN KEY (SecondaryServiceId)
      REFERENCES SecondaryServices(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_SecondaryServiceTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION
);
GO
----------------
CREATE TABLE Services
(
    Id INT IDENTITY (1,1),
    CodeNumber VARCHAR(16),
    Code NVARCHAR(40),
    Photo VARCHAR(55),
    IsActive BIT NOT NULL DEFAULT 1,
    Reason NVARCHAR(60),
    SecondaryServiceId INT,
    MainServiceId INT,

	CONSTRAINT PK_Services PRIMARY KEY (Id),

    CONSTRAINT FK_Services_SecondaryServiceId
        FOREIGN KEY (SecondaryServiceId)
        REFERENCES SecondaryServices(Id)
            ON DELETE NO ACtion ON UPDATE NO ACTION,

    CONSTRAINT FK_Services_MainServiceId
    FOREIGN KEY (MainServiceId)
      REFERENCES MainServices(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
);
GO
CREATE TABLE ServiceTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(30),
    ServiceId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_ServiceTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_ServiceTranslations_LangCode_ServiceId UNIQUE (ServiceId, LangCode),

	CONSTRAINT FK_ServiceTranslations_ServiceId
    FOREIGN KEY (ServiceId)
      REFERENCES Services(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_ServiceTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION
);
GO
-- lowest price
-- The higher price
----------------
CREATE TABLE ServicePrices --MM
(
    ServiceId INT,
    PriceCategoryId INT,
    Price SMALLINT,
    PriceCurrency VARCHAR(10),
    Note NVARCHAR(70),
	CONSTRAINT PK_ServicePrices PRIMARY KEY (ServiceId,PriceCategoryId),

    CONSTRAINT FK_ServicePrices_ServiceId
    FOREIGN KEY (ServiceId)
      REFERENCES Services(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
    
    CONSTRAINT FK_ServicePrices_PriceCategoryId
    FOREIGN KEY (PriceCategoryId)
      REFERENCES PriceCategories(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
);
GO
----------------
CREATE TABLE HosClients
(
    Id INT IDENTITY (1,1),
    CodeNumber VARCHAR(16), 
    NameEn NVARCHAR(50),
    NameOriginalLang NVARCHAR(50),
    Address NVARCHAR(50),  
    Photo VARCHAR(55),
    TelephoneNumber1 VARCHAR(25) ,
	TelephoneNumber2 VARCHAR(25) ,
    Email VARCHAR(40),
    WhatsAppNumber VARCHAR(15) ,
    Username VARCHAR(32),
    Password VARCHAR(40),
    ClientStatus TINYINT, -- active=0  inactive=1  standing=2 
    Reason NVARCHAR(60),
    
	CONSTRAINT PK_HosClients PRIMARY KEY (Id),
);
GO
---------------- 
CREATE TABLE ClientGroups 
(
    Id INT IDENTITY (1,1),
    GroupCode VARCHAR(32),   
    GroupName NVARCHAR(50),
    ClientId INT,
   
	CONSTRAINT PK_ClientGroups PRIMARY KEY (Id),
    CONSTRAINT UK_ClientGroups_GroupCode UNIQUE (GroupCode),

	CONSTRAINT FK_ClientGroups_HosClientId
    FOREIGN KEY (ClientId)
      REFERENCES HosClients(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
);
GO
---------------- 
CREATE TABLE Patients
(
    Id INT IDENTITY (1,1),
    MedicalFileNumber VARCHAR(16) NOT NULL,
    PhoneNumber VARCHAR(25),
    gender TINYINT,
    BirthDate DATE,
    MaritalStatus TINYINT, --(single - married - divorced - widower)
    SSN TINYINT, --Social Security number
    NationalID TINYINT,
    BloodType VARCHAR (10),
    PatientStatus TINYINT, -- active=0  inactive=1  attitude=2)
    Photo VARCHAR(55),
    Religion TINYINT,
    SSNTypeId INT, --(ID card - passport - insurance card - job card - driver's license)
    IsDeleted BIT NOT NULL DEFAULT 0,
    ClientId INT,
    ClientGroupId INT,
    NationalityId INT,

	CONSTRAINT PK_Patients PRIMARY KEY (Id),
    CONSTRAINT UK_Patients_MedicalFileNumber UNIQUE (MedicalFileNumber),
    CONSTRAINT CHECK_PatientSex CHECK(gender between 1 and 4),
    CONSTRAINT CHECK_PatientsMaritalStatus CHECK(MaritalStatus between 1 and 4),

    CONSTRAINT FK_Patients_SSNTypeId
        FOREIGN KEY (SSNTypeId)
        REFERENCES SSNTypes(Id)
            ON DELETE NO ACtion ON UPDATE NO ACTION,

    CONSTRAINT FK_Patients_ClientId
    FOREIGN KEY (ClientId)
      REFERENCES HosClients(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
    
    CONSTRAINT FK_Patients_ClientGroupId
    FOREIGN KEY (ClientGroupId)
      REFERENCES ClientGroups(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_Patients_NationalityId
    FOREIGN KEY (NationalityId)
      REFERENCES Nationalities(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	INDEX IX_Patients_MedicalFileNumber NONCLUSTERED (MedicalFileNumber),
	INDEX IX_Patients_PhoneNumber NONCLUSTERED (PhoneNumber),
	INDEX IX_Patients_Client_ClientGroup NONCLUSTERED (ClientId,ClientGroupId),
);
GO
CREATE TABLE PatientTranslations --MM
(
    Id INT IDENTITY (1,1),
    FullName NVARCHAR(60),
    Address NVARCHAR(50),
    Occupation NVARCHAR(50),
    Employer NVARCHAR(50),
    RelationshipClient TINYINT, --(father / mother / husband / wife / son / daughter / brother / sister / other) 
    
    PatientId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_PatientTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_PatientTranslations_LangCode_PatientId UNIQUE (PatientId, LangCode),

	CONSTRAINT FK_PatientTranslations_PatientId
    FOREIGN KEY (PatientId)
      REFERENCES Patients(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_PatientTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
	
	INDEX IX_PatientTranslations_FullName NONCLUSTERED (FullName)
);
GO
----------------
CREATE TABLE ClientsSubscription --MM
(
    Specification NVARCHAR(100),
    NumberOfSubscribers SMALLINT,
    Duration DATE,
    StartDate TIME(0),
    PriceOfSubscription SMALLINT,
    PriceCurrency VARCHAR(10),
    Status TINYINT NOT NULL DEFAULT 1,
    Notes NVARCHAR(100),
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreateOn DATETIME DEFAULT GETDATE(),

    PriceCategoryId INT,
    ClientId INT,
    ClientGroupId INT,

	CONSTRAINT PK_ClientsSubscription PRIMARY KEY (ClientId,ClientGroupId),

    CONSTRAINT FK_ClientsSubscription_PriceCategoryId
    FOREIGN KEY (PriceCategoryId)
      REFERENCES PriceCategories(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    CONSTRAINT FK_ClientsSubscription_ClientId
    FOREIGN KEY (ClientId)
      REFERENCES HosClients(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    CONSTRAINT FK_ClientsSubscription_ClientGroupId
    FOREIGN KEY (ClientGroupId)
      REFERENCES ClientGroups(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	INDEX IX_ClientsSubscription_ClientId NONCLUSTERED(ClientId)
);
GO
----------------
CREATE TABLE BookingStatuses
(
    Id SMALLINT IDENTITY (1,1),
	CreateOn DATETIME DEFAULT GETDATE(),
    CONSTRAINT PK_BookingStatuses PRIMARY KEY (Id),
);
GO
----------------
CREATE TABLE BookingStatusesTranslations --MM
(
    Id INT IDENTITY (1,1),
    StatusName NVARCHAR(50) NOT NULL,
    BookingStatusId SMALLINT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_BookingStatusesTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_BookingStatusesTranslations_LangCode_BookingStatusId UNIQUE (BookingStatusId, LangCode),

	CONSTRAINT FK_BookingStatusesTranslations_BookingStatusId
    FOREIGN KEY (BookingStatusId)
      REFERENCES BookingStatuses(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_BookingStatusesTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    INDEX IX_BookingStatusesTranslations_StatusName NONCLUSTERED (StatusName)
);
GO
----------------
CREATE TABLE Booking
(
	Id BIGINT IDENTITY(1,1),
    BookingNumber NVARCHAR(25) NOT NULL,
    PatientId INT NOT NULL,
    HospitalId INT NOT NULL,
    SpecialtyId INT NOT NULL,
    DoctorId INT NOT NULL,
    WorkingPeriodId INT NOT NULL ,
	TypeVisitId INT NOT NULL,
	ClinicId INT NOT NULL, 
    BookingStatusId SMALLINT NOT NULL DEFAULT 1,
    PriceCategoryId INT,
    CurrencyId INT,
    Price INT,
    DayNumber TINYINT,
	VisitingDate DATE,
    CreateOn DATETIME DEFAULT GETDATE(),
	
	CONSTRAINT PK_Booking PRIMARY KEY (Id),
    CONSTRAINT UK_Booking_BookingNumber UNIQUE (BookingNumber),
    
	CONSTRAINT FK_Booking_PatientId
    FOREIGN KEY (PatientId)
      REFERENCES Patients(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_Booking_HospitalId
    FOREIGN KEY (HospitalId)
      REFERENCES HospitalS(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_Booking_SpecialtyId
    FOREIGN KEY (SpecialtyId)
      REFERENCES MedicalSpecialties(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
    
    CONSTRAINT FK_Booking_DoctorId
    FOREIGN KEY (DoctorId)
      REFERENCES Doctors(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_Booking_WorkingPeriodId
    FOREIGN KEY (WorkingPeriodId)
      REFERENCES WorkingPeriod(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_Booking_TypeVisitId
    FOREIGN KEY (TypeVisitId)
      REFERENCES TypesVisit(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_Booking_ClinicId
    FOREIGN KEY (ClinicId)
      REFERENCES Clinics(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
    
    CONSTRAINT FK_Booking_PriceCategoryId
    FOREIGN KEY (PriceCategoryId)
      REFERENCES PriceCategories(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    CONSTRAINT FK_Booking_CurrencyId
    FOREIGN KEY (CurrencyId)
      REFERENCES Currencies(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    CONSTRAINT FK_Booking_BookingStatusId
    FOREIGN KEY (BookingStatusId)
      REFERENCES BookingStatuses(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION
);
GO
----------------

-- CREATE TABLE Visits
-- (
--     Id INT IDENTITY (1,1),  
--     PatientId
--     DoctorId
--     WorkingPeriodId
--     ClinicId
--     TypeVisitId
--     PriceVisit
--     IsInHouse
--     DateTime
-- 	CONSTRAINT PK_Visits PRIMARY KEY (Id),
-- );
-- ----------------
-- CREATE TABLE PatientServicesVisit
-- (
--     PatientId
--     VisitId
--     ServiceId
--     CreateAt
-- );
-- ----------------
-- CREATE TABLE ServicesResults --MM
-- (
--     Id INT IDENTITY (1,1),  
--     PatientId INT,
--     ServiceId INt,
-- 	CONSTRAINT PK_ServicesResults PRIMARY KEY (Id),

-- );
----------------