-- TRUNCATE TABLE ;

-- booking
DROP TABLE Booking;

-- meny to meny
DROP TABLE DoctorWorkPeriods;
DROP TABLE DoctorVisitPrices;
DROP TABLE SpecialtiesDoctors;
DROP TABLE DoctorsWorkHospital;
DROP TABLE DoctorAttachments

--DROP TABLE MedicalSpecialtiesHospitals;
DROP TABLE HospitalPhoneNumbers;

-- users
DROP TABLE EmployeeAccounts;
DROP TABLE UserAccount;
DROP TABLE PatientTranslations;
DROP TABLE Patients;
DROP TABLE ClientGroups;
DROP TABLE HosClients;

-- doctor
DROP TABLE DoctorTranslations;
DROP TABLE Doctors;

-- HospitalBody
DROP TABLE ClinicTranslations;
DROP TABLE Clinics;
DROP TABLE MedicalSpecialtyTranslations;
DROP TABLE MedicalSpecialties;
DROP TABLE RoomTranslations;
DROP TABLE HosRooms;
DROP TABLE FloorTranslations;
DROP TABLE HosFloors;
DROP TABLE BuildingTranslations;
DROP TABLE HosBuildings;
DROP TABLE HospitalFeatureTranslations;
DROP TABLE HospitalFeatures;
DROP TABLE ContactForm;
DROP TABLE HospitalTranslations;
DROP TABLE Hospitals;

-- Settings
DROP TABLE DoctorsDegreesTranslations;
DROP TABLE DoctorsDegrees;
DROP TABLE WeekdaysTranslations;
DROP TABLE Weekdays;
DROP TABLE GendersTranslations;
DROP TABLE Genders;
DROP TABLE PromotionsTranslations;
DROP TABLE Promotions;
DROP TABLE ReligionsTranslations;
DROP TABLE Religions;
DROP TABLE MaritalStatusTranslations;
DROP TABLE MaritalStatus;
DROP TABLE EmployeesStatusTranslations;
DROP TABLE EmployeesStatus;
DROP TABLE SSNTypesTranslations;
DROP TABLE SSNTypes;
DROP TABLE BookingStatusesTranslations;
DROP TABLE BookingStatuses;
DROP TABLE PriceCategoryTranslations;
DROP TABLE PriceCategories;
DROP TABLE TypesVisitTranslations;
DROP TABLE TypesVisit;
DROP TABLE WorkingPeriodTranslations;
DROP TABLE WorkingPeriod;
DROP TABLE NationalitiesTranslations;
DROP TABLE Nationalities;
DROP TABLE RoomTypeTranslations;
DROP TABLE RoomTypes;
DROP TABLE CountriesTranslations;
DROP TABLE Countries;

--SYSTEM
DROP TABLE Languages;
DROP TABLE ConfirmationOptions;

SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE';