﻿
-- USE master;
-- IF NOT EXISTS(SELECT name FROM sys.databases WHERE name = 'alrahma_care_db')
-- BEGIN
--     CREATE DATABASE alrahma_care_db COLLATE Arabic_100_CI_AS_KS_WS_SC_UTF8;
-- END
-- GO

--GO
-- CREATE TABLE Users(
-- 	[Id] [nvarchar](36) NOT NULL, --*
-- 	[FullName] [nvarchar](100) NOT NULL, --*
-- 	[PhoneNumber] [nvarchar](max) NULL,
-- 	[Email] [nvarchar](256) NULL,
-- 	[EmailConfirmed] [bit] NOT NULL DEFAULT 0,
-- 	[PhoneNumberConfirmed] [bit] NOT NULL DEFAULT 0,
--     [VerificationCode] VARCHAR(8), -- new
--     [ExpirationTime] DATETIMEOFFSET(7), -- new
--     [CreateOn] DATETIMEOFFSET(7) DEFAULT GETUTCDATE(),
-- 	[PasswordHash] [nvarchar](max) NULL,
	
--     -- [UserName] [nvarchar](256) NULL,
-- 	-- [SecurityStamp] [nvarchar](max) NULL,
-- 	-- [ConcurrencyStamp] [nvarchar](max) NULL,
-- 	-- [TwoFactorEnabled] [bit] NOT NULL DEFAULT 0,
-- 	-- [LockoutEnd] [DATETIMEOFFSET](7) NULL,
-- 	-- [LockoutEnabled] [bit] NOT NULL DEFAULT 0,
-- 	-- [AccessFailedCount] [int] NOT NULL DEFAULT 0,

-- 	CONSTRAINT PK_Users PRIMARY KEY (Id),
-- );
-- CREATE TABLE Users(
-- 	[Id] [nvarchar](450) NOT NULL, --*
-- 	[FullName] [nvarchar](100) NOT NULL, --*
-- 	[PhoneNumber] [nvarchar](max) NULL,
	
--     [Email] [nvarchar](256) NULL,
-- 	[EmailConfirmed] [bit] NOT NULL DEFAULT 0,
-- 	[PhoneNumberConfirmed] [bit] NOT NULL DEFAULT 0,
--     [VerificationCode] VARCHAR(8), -- new
--     [ExpirationTime] DATETIMEOFFSET(7), -- new
-- 	[PasswordHash] [nvarchar](max) NULL,
--     -- [UserName] [nvarchar](256) NULL,
-- 	-- [SecurityStamp] [nvarchar](max) NULL,
-- 	-- [ConcurrencyStamp] [nvarchar](max) NULL,
-- 	-- [TwoFactorEnabled] [bit] NOT NULL DEFAULT 0,
-- 	-- [LockoutEnd] [DATETIMEOFFSET](7) NULL,
-- 	-- [LockoutEnabled] [bit] NOT NULL DEFAULT 0,
-- 	-- [AccessFailedCount] [int] NOT NULL DEFAULT 0,
-- 	CONSTRAINT PK_Users PRIMARY KEY (Id),
-- );
--GO
-- CREATE TABLE Roles(
-- 	[Id] [nvarchar](450) NOT NULL,
-- 	[Name] [nvarchar](256) NULL,
-- 	[ConcurrencyStamp] [nvarchar](max) NULL,

-- 	CONSTRAINT PK_Roles PRIMARY KEY (Id),
-- );

-- GO
-- CREATE TABLE UserRoles(
-- 	[Id] [BIGINT] IDENTITY(1,1),
-- 	[UserId] [nvarchar](450) NOT NULL,
-- 	[RoleId] [nvarchar](450) NOT NULL,
-- 	[CreateOn] DATETIME DEFAULT GETDATE(),

-- 	CONSTRAINT PK_UserRoles PRIMARY KEY (Id),

-- 	CONSTRAINT FK_UserRoles_UserId
--     FOREIGN KEY (UserId)
--       REFERENCES Users(Id)
-- 		ON DELETE CASCADE,

--     CONSTRAINT FK_UserRoles_RoleId
--     FOREIGN KEY (RoleId)
--       REFERENCES Roles(Id)
-- 		ON DELETE CASCADE,
-- );
-- GO
----------------
CREATE TABLE EmployeeAccounts(
	[Id] [nvarchar](450) NOT NULL, --*
	[FullName] [nvarchar](200) NOT NULL, --*
	[UserName] [nvarchar](256) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
    [CreateOn] DATETIMEOFFSET(7) DEFAULT GETUTCDATE(),
	[PasswordHash] [nvarchar](max) NULL,
	CONSTRAINT PK_EmployeeAccounts PRIMARY KEY (Id),
);
-- GO
----------------
CREATE TABLE ConfirmationOptions(
	[Id] [varchar](36) NOT NULL,
	[OptionName] [nvarchar](36) NULL,
	[Chosen] [BIT] NOT NULL DEFAULT 0,

	CONSTRAINT PK_ConfirmationOptions PRIMARY KEY (Id),
);
GO
INSERT INTO ConfirmationOptions VALUES('sms','by phone number',0),('email','by email',0),('non','We do not need Confirmation',1);
GO
----------------
CREATE TABLE Languages
(
    [Code] VARCHAR(6),
    [LanguageName] NVARCHAR(40),
    [IsDefault] BIT NOT NULL DEFAULT 0,
	CONSTRAINT PK_Language PRIMARY KEY (Code)
);
GO
INSERT INTO Languages VALUES('ar','عربي',1),('en','English',0),('fr','française',0),('es','España',0);
GO
----------------
CREATE TABLE Countries
(
    Id SMALLINT IDENTITY(1,1),
    CallingCode VARCHAR(5) UNIQUE, -- +20
    NumberOfDigits TINYINT, -- 10
    CountryCode CHAR(2) UNIQUE, --EG
    CurrencyCode CHAR(3) NOT NULL ,-- EGP
    CurrencySymbol VARCHAR(2) NOT NULL, -- Le
    NationalFlag VARCHAR(52), -- photo
    Longitude DECIMAL(12, 9),
    Latitude DECIMAL(12, 9),

    CONSTRAINT PK_Currencies PRIMARY KEY (Id),
);
GO
----------------
CREATE TABLE CountriesTranslations --MM
(
    Id SMALLINT IDENTITY (1,1),
    CountryName NVARCHAR(150), --Egypt
    CurrencyName NVARCHAR(150), -- Egyptian pound
    CapitalName NVARCHAR(150), -- Cairo
    CountryId SMALLINT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_CountriesTranslations PRIMARY KEY (Id),
    CONSTRAINT FK_CountriesTranslations_LangCode_CountryId UNIQUE (CountryId, LangCode),

	CONSTRAINT FK_CountriesTranslations_CountryId
    FOREIGN KEY (CountryId)
      REFERENCES Countries(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_CountriesTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
    
  	INDEX IX_CountriesTranslations_FullName NONCLUSTERED (CountryName)
);
GO
----------------
CREATE TABLE Hospitals
(
    Id INT IDENTITY (1,1),
    Photo VARCHAR(55),
	CodeNumber VARCHAR(25),
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
	TelephoneNumber VARCHAR(25) ,
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
    Name NVARCHAR(200),
    Address NVARCHAR(200),
    Description NVARCHAR(MAX),
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
    SenderName NVARCHAR(200) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    Subject NVARCHAR(500) NOT NULL,
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
    Id INT IDENTITY(1, 1),
    Photo VARCHAR(255),
    CreateOn DATETIME DEFAULT GETDATE(),
    HospitalId INT,

    CONSTRAINT PK_HospitalFeatures PRIMARY KEY (Id),

    CONSTRAINT FK_HospitalFeatures_HospitalId
    FOREIGN KEY (HospitalId)
      REFERENCES Hospitals(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
);
CREATE TABLE HospitalFeatureTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(200),
    Description NVARCHAR(MAX),
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
	CodeNumber VARCHAR(25),
	Photo VARCHAR(55),
	CreateOn DATETIME DEFAULT GETDATE(),
	IsDeleted BIT NOT NULL DEFAULT 0,
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
    Name NVARCHAR(200),
	Description NVARCHAR(500),
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
	CodeNumber VARCHAR(25),
    Photo VARCHAR(55),
	CreateOn DATETIME DEFAULT GETDATE(),
	IsDeleted BIT NOT NULL DEFAULT 0,
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
    Name NVARCHAR(200),
	Description NVARCHAR(500),
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
	CodeNumber VARCHAR(25),
	CreateOn DATETIME DEFAULT GETDATE(),
	IsDeleted BIT NOT NULL DEFAULT 0,
	CONSTRAINT PK_RoomTypes PRIMARY KEY (Id),
);
GO
CREATE TABLE RoomTypeTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(200),
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
	CodeNumber VARCHAR(25),
    Kind VARCHAR(16), -- ==Type  --(Clinic - inpatient - operations - laboratory - x-rays - pharmacy - office)
    IsActive BIT NOT NULL DEFAULT 1,  -- (Active, inactive) -- update1
	IsDeleted BIT NOT NULL DEFAULT 0,
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
    Name NVARCHAR(200),
	Description NVARCHAR(500),
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
	CodeNumber VARCHAR(25),
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
    Name NVARCHAR(200),
	Description NVARCHAR(500),
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
-- CREATE TABLE MedicalSpecialtiesHospitals --MM
-- (
--     SpecialtyId INT,
--     HospitalId INT,

-- 	CONSTRAINT PK_MedicalSpecialtiesHospitals PRIMARY KEY (SpecialtyId,HospitalId),

-- 	CONSTRAINT FK_MedicalSpecialtiesHospitals_SPSpecialtyId
-- 	FOREIGN KEY (SpecialtyId)
-- 		REFERENCES MedicalSpecialties(Id)
-- 		ON DELETE NO ACtion ON UPDATE NO ACTION,

-- 	CONSTRAINT FK_MedicalSpecialtiesHospitals_HospitalId
-- 	FOREIGN KEY (HospitalId)
-- 		REFERENCES HospitalS(Id)
-- 		ON DELETE NO ACtion ON UPDATE NO ACTION,
    
--     INDEX IX_MedicalSpecialtiesHospitals_HospitalId NONCLUSTERED (HospitalId)
-- );
-- GO
----------------
CREATE TABLE Clinics
(
    Id INT IDENTITY (1,1),
    CodeNumber VARCHAR(25),
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
    Name NVARCHAR(200),
	Description NVARCHAR(500),
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
    CodeNumber VARCHAR(25),
    CONSTRAINT PK_SSNTypes PRIMARY KEY (Id),
);
GO
CREATE TABLE SSNTypesTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(200),
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
    CodeNumber VARCHAR(25), 
    StartTime TIME(0),
    EndTime TIME(0),
	CONSTRAINT PK_WorkingPeriod PRIMARY KEY (Id),
);
GO
CREATE TABLE WorkingPeriodTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(200),
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
    CodeNumber VARCHAR(25), 
    CreateOn DATETIME DEFAULT GETDATE(),
    Symbol VARCHAR (16),
	CONSTRAINT PK_PriceCategories PRIMARY KEY (Id),
);
GO
CREATE TABLE PriceCategoryTranslations --MM
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(200),
    PriceCategoryId INT,
	Description NVARCHAR(500),
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
	CodeNumber VARCHAR(25),
	CreateOn DATETIME DEFAULT GETDATE(),
	IsDeleted BIT NOT NULL DEFAULT 0,
	CONSTRAINT PK_TypesVisit PRIMARY KEY (Id),
)
CREATE TABLE TypesVisitTranslations --  1M
(
    Id INT IDENTITY (1,1),
    Name NVARCHAR(200),
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
    Name NVARCHAR(200),
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
    DegreeName NVARCHAR(150), --(general - specialist - consultant - university professor)
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
CREATE TABLE Religions (
    Id SMALLINT NOT NULL,
    CONSTRAINT PK_Religions PRIMARY KEY (Id),
);
GO
----------------
CREATE TABLE ReligionsTranslations --MM
(
    Id SMALLINT IDENTITY (1,1),
    Name NVARCHAR(200),
    ReligionId SMALLINT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_ReligionsTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_ReligionsTranslations_LangCode_ReligionId UNIQUE (ReligionId, LangCode),

	CONSTRAINT FK_ReligionsTranslations_ReligionId
    FOREIGN KEY (ReligionId)
      REFERENCES Religions(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_ReligionsTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION
);
GO
----------------
CREATE TABLE MaritalStatus (
    Id SMALLINT NOT NULL,
    CONSTRAINT PK_MaritalStatus PRIMARY KEY (Id),
);
GO
----------------
CREATE TABLE MaritalStatusTranslations --MM
(
    Id SMALLINT IDENTITY (1,1),
    Name NVARCHAR(200),
    MaritalId SMALLINT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_MaritalStatusTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_MaritalStatusTranslations_LangCode_MaritalId UNIQUE (MaritalId, LangCode),

	CONSTRAINT FK_MaritalStatusTranslations_MaritalId
    FOREIGN KEY (MaritalId)
      REFERENCES MaritalStatus(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_MaritalStatusTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION
);
GO
----------------
CREATE TABLE Genders
(
    Id SMALLINT NOT NULL,
    CONSTRAINT PK_Genders PRIMARY KEY (Id),
);
GO
----------------
CREATE TABLE GendersTranslations --MM
(
    Id SMALLINT IDENTITY (1,1),
    Name NVARCHAR(200),
    GenderId SMALLINT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_GendersTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_GendersTranslations_LangCode_GenderId UNIQUE (GenderId, LangCode),

	CONSTRAINT FK_GendersTranslations_GenderId
    FOREIGN KEY (GenderId)
      REFERENCES Genders(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_GendersTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION
);
GO
----------------
CREATE TABLE Weekdays (
    Id TINYINT NOT NULL,
    CONSTRAINT PK_Weekdays PRIMARY KEY (Id),
);
GO
----------------
CREATE TABLE WeekdaysTranslations --MM
(
    Id SMALLINT IDENTITY (1,1),
    Name NVARCHAR(200),
    WeekdayId TINYINT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_WeekdaysTranslations PRIMARY KEY (Id),
    CONSTRAINT FK_WeekdaysTranslations_LangCode_WeekdayId UNIQUE (WeekdayId, LangCode),

	CONSTRAINT FK_WeekdaysTranslations_WeekdayId
    FOREIGN KEY (WeekdayId)
      REFERENCES Weekdays(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_WeekdaysTranslations_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION
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
    StatusName NVARCHAR(100),
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
    StatusName NVARCHAR(100) NOT NULL,
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
INSERT INTO BookingStatuses (CreateOn)
VALUES (GETDATE()),(GETDATE()),(GETDATE()),(GETDATE()),(GETDATE());
INSERT INTO BookingStatusesTranslations (StatusName, BookingStatusId, LangCode)
VALUES
('Pending', 1, 'en'),('قيد الانتظار', 1, 'ar'),
('Confirmed', 2, 'en'),('تم التأكيد', 2, 'ar'),
('Cancelled', 3, 'en'),('تم الإلغاء', 3, 'ar'),
('Completed', 4, 'en'),('تم الانتهاء', 4, 'ar'),
('In Progress', 5, 'en'),('جاري التنفيذ', 5, 'ar');
GO
----------------
CREATE TABLE Promotions (
    Id INT IDENTITY(1,1),
    Photo VARCHAR(55),
    Position INT,
    Link VARCHAR(255)
    CONSTRAINT PK_Promotions PRIMARY KEY (Id),
);
GO
----------------
CREATE TABLE PromotionsTranslations --MM
(
    Id INT IDENTITY (1,1),
    Title VARCHAR(255),
    Description NVARCHAR(500),

    PromotionId INT,
    LangCode VARCHAR(6),

	CONSTRAINT PK_PromotionsTranslations PRIMARY KEY (Id),
    CONSTRAINT UK_Promotions_LangCode_PromotionId UNIQUE (PromotionId, LangCode),

	CONSTRAINT FK_Promotions_PromotionId
    FOREIGN KEY (PromotionId)
      REFERENCES Promotions(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_Promotions_LangCode
    FOREIGN KEY (LangCode)
      REFERENCES Languages(Code)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
);

-- ================================================================================= DOCTOR =================================================================================
-- ================================================================================= DOCTOR =================================================================================
-- ================================================================================= DOCTOR =================================================================================
-- ================================================================================= DOCTOR =================================================================================
-- ================================================================================= DOCTOR =================================================================================
GO
----------------
CREATE TABLE Doctors
(
    Id INT IDENTITY (1,1),
    CodeNumber VARCHAR(25), 
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
    FullName NVARCHAR(300),
    Headline NVARCHAR(300),
    About NVARCHAR(MAX),
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
    DoctorId INT Not NULL,
    MedicalSpecialtyId INT Not NULL,
    CreateOn DATETIME DEFAULT GETDATE(),

    CONSTRAINT PK_SpecialtiesDoctors PRIMARY KEY (DoctorId,MedicalSpecialtyId),

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
CREATE TABLE DoctorWorkPeriods --MM
(
    Id INT IDENTITY(1,1),
    HospitalId INT NOT NULL,
    SpecialtyId INT NOT NULL,
    ClinicId INT NOT NULL,
    DoctorId INT NOT NULL,
    WorkingPeriodId INT NOT NULL,
    DayId TINYINT NOT NULL,

    CONSTRAINT PK_DoctorWorkPeriods PRIMARY KEY (Id),
    CONSTRAINT UK_DoctorWorkPeriods_AllProperty UNIQUE (HospitalId,SpecialtyId,DoctorId,ClinicId,WorkingPeriodId,DayId),

    CONSTRAINT FK_DoctorWorkPeriods_HospitalId
    FOREIGN KEY (HospitalId)
      REFERENCES Hospitals(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_DoctorWorkPeriods_SpecialtyId
	FOREIGN KEY (SpecialtyId)
		REFERENCES MedicalSpecialties(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_DoctorWorkPeriods_DoctorId
    FOREIGN KEY (DoctorId)
      REFERENCES Doctors(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_DoctorWorkPeriods_ClinicId
    FOREIGN KEY (ClinicId)
      REFERENCES Clinics(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	CONSTRAINT FK_DoctorWorkPeriods_WorkingPeriodId
    FOREIGN KEY (WorkingPeriodId)
      REFERENCES WorkingPeriod(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    CONSTRAINT FK_DoctorWorkPeriods_DayId
    FOREIGN KEY (DayId)
      REFERENCES Weekdays(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,
    
    INDEX IX_DoctorWorkPeriods_Hospital_Clinic_DayId NONCLUSTERED (HospitalId,ClinicId,DayId),
    INDEX IX_DoctorWorkPeriods_DoctorId_DayId NONCLUSTERED (DoctorId,DayId),
    INDEX IX_DoctorWorkPeriods_DoctorId NONCLUSTERED (DoctorId)
);
GO
---------------- 
CREATE TABLE DoctorAttachments
(
    Id INT IDENTITY (1,1),
    AttachFileName VARCHAR(100),
    Title VARCHAR(200),
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
CREATE TABLE HosClients
(
    Id INT IDENTITY (1,1),
    CodeNumber VARCHAR(25), 
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
-- ================================================================================= Patients =================================================================================
-- ================================================================================= Patients =================================================================================
-- ================================================================================= Patients =================================================================================
-- ================================================================================= Patients =================================================================================
-- ================================================================================= Patients =================================================================================
GO
---------------- 
CREATE TABLE Patients
(
    Id INT IDENTITY (1,1),
    --MedicalFileNumber VARCHAR(16) NOT NULL,
    BirthDate DATE,
    NationalID TINYINT,
    SSN TINYINT, --Social Security number
    BloodType VARCHAR (10),
    PatientStatus TINYINT, -- active=0  inactive=1  attitude=2)
    Photo VARCHAR(55),
    SSNTypeId INT, --(ID card - passport - insurance card - job card - driver's license)
    IsDeleted BIT NOT NULL DEFAULT 0,
    ClientId INT NULL,
    ClientGroupId INT,
    NationalityId SMALLINT,

    GenderId SMALLINT,
    ReligionId SMALLINT,
    MaritalStatusId SMALLINT,

	CONSTRAINT PK_Patients PRIMARY KEY (Id),

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

	CONSTRAINT FK_Patients_CountryId
    FOREIGN KEY (NationalityId)
      REFERENCES Countries(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    CONSTRAINT FK_Patients_GenderId
    FOREIGN KEY (GenderId)
      REFERENCES Genders(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    CONSTRAINT FK_Patients_ReligionId
    FOREIGN KEY (ReligionId)
      REFERENCES Religions(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

    CONSTRAINT FK_Patients_MaritalStatus
    FOREIGN KEY (MaritalStatusId)
      REFERENCES MaritalStatus(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	INDEX IX_Patients_Client_ClientGroup NONCLUSTERED (ClientId,ClientGroupId),
);
GO
CREATE TABLE PatientTranslations --MM
(
    Id INT IDENTITY (1,1),
    FullName NVARCHAR(150),
    Occupation NVARCHAR(100),
    [Address] NVARCHAR(300),
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
CREATE TABLE UserAccount (
    Id INT IDENTITY (1,1),
    UserName [nvarchar](36) NULL, -- MedicalFileNumber
    PhoneNumber VARCHAR(25),
    CallingCode VARCHAR(5), -- +20
    Email NVARCHAR(255),
    EmailConfirmed BIT NOT NULL DEFAULT 0,
    PhoneNumberConfirmed BIT NOT NULL DEFAULT 0,
    VerificationCode VARCHAR(8),
    ExpirationTime DATETIMEOFFSET(7), -- new
    PasswordHash NVARCHAR(255) NOT NULL,
    UserType TINYINT NOT NULL, -- "Patient" or "Doctor"
    UserReferenceID INT NOT NULL, -- PatientID or DoctorID

    CONSTRAINT PK_UserAccount PRIMARY KEY (Id),
    CONSTRAINT UK_UserAccount_PhoneNumber UNIQUE (PhoneNumber),
    CONSTRAINT UK_UserAccount_Email UNIQUE (Email),
    CONSTRAINT UK_UserAccount_UserReferenceID_UserType UNIQUE (UserReferenceID,UserType),

	CONSTRAINT FK_UserAccount_UserReferenceID
    FOREIGN KEY (UserReferenceID)
      REFERENCES Patients(Id)
		ON DELETE NO ACtion ON UPDATE NO ACTION,

	INDEX IX_UserAccount_UserName NONCLUSTERED (UserName),
	INDEX IX_UserAccount_PhoneNumber NONCLUSTERED (PhoneNumber)
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
    CurrencyCode CHAR(3) ,-- EGP
    Price INT,
    DayNumber TINYINT,
	VisitingDate DATETIMEOFFSET(7),
    CreateOn DATETIMEOFFSET(7) DEFAULT GETUTCDATE(),
    BookingReason NVARCHAR(500),
	StatusReason NVARCHAR(300),

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