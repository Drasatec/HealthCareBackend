-- Insert into Genders table
INSERT INTO Genders (Id)
VALUES (1), (2), (3);

-- Insert into GendersTranslations table
INSERT INTO GendersTranslations (Name, GenderId, LangCode)
VALUES
    ('Male', 1, 'en'),
    ('ذكر', 1, 'ar'),
    ('Female', 2, 'en'),
    ('أنثى', 2, 'ar'),
    ('Other', 3, 'en'),
    ('آخر', 3, 'ar');

-- Insert into Weekdays table
INSERT INTO Weekdays (Id)
VALUES (0), (1), (2), (3), (4), (5), (6);

-- Insert into WeekdaysTranslations table
INSERT INTO WeekdaysTranslations (Name, WeekdayId, LangCode)
VALUES
    ('Sunday', 0, 'en'),
	('Monday', 1 , 'en'),
    ('Tuesday', 2, 'en'),
    ('Wednesday', 3, 'en'),
    ('Thursday', 4, 'en'),
    ('Friday', 5, 'en'),
    ('Saturday', 6, 'en'),
    ('الأحد', 0, 'ar'),
    ('الإثنين', 1, 'ar'),
    ('الثلاثاء', 2, 'ar'),
    ('الأربعاء', 3, 'ar'),
    ('الخميس', 4, 'ar'),
    ('الجمعة', 5, 'ar'),
    ('السبت', 6, 'ar');

-- INSERT INTO Users
-- (Id,FullName,Email,PhoneNumber,EmailConfirmed,PhoneNumberConfirmed, PasswordHash)
--      VALUES
-- 	 ('19322255-1692-4eeb-aaa0-c1a3e7dff0b9','mai 1',N'mai1@gmail.com','+201234567891',1,0,'ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f'),
--      ('3e2fe885-5041-47ee-93cd-d4ff264450b3','mai 2',N'mai2@gmail.com','+201234567892',0,1,'ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f'),
--      ('3f430285-c46e-4af0-8b34-7f2d6af64d59','mai 3',N'mai3@gmail.com','+201234567893',1,1,'ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f'),
--      ('528b2b93-53c7-4f39-a2a1-056b92abad9d','mai 4',N'mai4@gmail.com','+201234567894',0,0,'ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f'),
--      ('5a44d741-312a-4604-a8c4-f6a903de44d5','mai 5',N'mai5@gmail.com','+201234567895',1,1,'ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f');
-- GO
--  Currencies
-- INSERT INTO Currencies (CurrencyCode, Symbol, country, Longitude, Latitude, CurrencyName) 
-- VALUES
--     ('AMD', '¥', 'Armenia', 44.6917527, 40.2367283, 'Dram'),
--     ('ARS', '£', 'Argentina', -60.61667, -26.73333, 'Peso'),
--     ('BOB', '€', 'Bolivia', -65.267799, -19.156561, 'Boliviano'),
--     ('BRL', '£', 'Brazil', -35.4551534, -7.8794782, 'Real'),
--     ('CAD', '¥', 'Canada', -63.0886099, 46.215926, 'Dollar'),
--     ('CNY', '€', 'China', 117.002933, 35.302488, 'Yuan Renminbi'),
--     ('COP', '$', 'Colombia', -77.3439283, 1.2296124, 'Peso'),
--     ('CUP', '€', 'Cuba', -75.9869609, 20.2218201, 'Peso'),
--     ('CZK', '£', 'Czech Republic', 15.4833007, 49.2908708, 'Koruna'),
--     ('DOP', '$', 'Dominican Republic', -71.0790048, 19.5403533, 'Peso'),
--     ('EGP', '£', 'Egypt', 30.734995, 28.4233602, 'Pound'),
--     ('EUR', '£', 'France', 5.8978018, 43.4945737, 'Euro'),
--     ('GMD', '£', 'Gambia', -14.769285, 13.449097, 'Dalasi'),
--     ('GTQ', '$', 'Guatemala', -91.4656296, 14.5802198, 'Quetzal'),
--     ('HRK', '¥', 'Croatia', 15.5854843, 45.1150317, 'Kuna'),
--     ('IDR', '$', 'Indonesia', 110.8171082, -6.4712737, 'Rupiah'),
--     ('JPY', '¥', 'Japan', 139.707963, 36.1219748, 'Yen'),
--     ('MGA', '£', 'Madagascar', 47.8402601, -22.3539973, 'Ariary'),
--     ('MKD', '$', 'Macedonia', 21.3421041, 41.9950451, 'Denar'),
--     ('MNT', '£', 'Mongolia', 106.9211984, 47.9195813, 'Tugrik'),
--     ('MXN', '£', 'Mexico', -100.9185477, 28.4943597, 'Peso'),
--     ('NAD', '¥', 'Namibia', 15.9523622, -21.4206681, 'Dollar'),
--     ('NGN', '€', 'Nigeria', 5.3102505, 9.2394244, 'Naira'),
--     ('NOK', '¥', 'Norway', 11.5610075, 60.8818611, 'Krone'),
--     ('NZD', '¥', 'New Zealand', 175.8087485, -38.9898711, 'Dollar'),
--     ('PHP', '¥', 'Philippines', 123.4675853, 9.8318931, 'Peso'),
--     ('PKR', '£', 'Pakistan', 72.4275483, 31.8917453, 'Rupee'),
--     ('PLN', '$', 'Poland', 18.3991666, 53.8514527, 'Zloty'),
--     ('RUB', '¥', 'Russia', 101.7805781, 56.3041802, 'Ruble'),
--     ('SEK', '$', 'Sweden', 17.9509504, 59.3245933, 'Krona'),
--     ('SOS', '£', 'Somalia', 49.8739267, 11.4720137, 'Shilling'),
--     ('THB', '$', 'Thailand', 100.3477801, 17.7116584, 'Baht'),
--     ('UAH', '$', 'Ukraine', 23.6611306, 49.7821352, 'Hryvnia'),
--     ('UGX', '¥', 'Uganda', 33.9303991, 1.1016277, 'Shilling'),
--     ('USD', '£', 'United States', -104.7099999, 40.42, 'Dollar'),
--     ('XAF', '$', 'Cameroon', 10.4284178, 5.4807517, 'Franc'),
--     ('XOF', '$', 'Burkina Faso', -4.3051542, 11.1649219, 'Franc'),
--     ('XPF', '¥', 'New Caledonia', 164.991636, -20.692675, 'Franc'),
--     ('ZAR', '$', 'South Africa', 30.18109, -29.56225, 'Rand');






--select * from currencies
GO
-- ===========================================================================================================================================================
-- Inserting data into the Hospitals table
INSERT INTO Hospitals (Photo, CodeNumber, Email, WhatsAppNumber,Longitude,Latitude)
VALUES ('hospital1.jpeg', 'CODE001', 'hospital1@example.com', '+123456789', -60.61667, -26.73333),
       ('hospital2.jpeg', 'CODE002', 'hospital2@example.com', '+987654321', 44.6917527, 40.2367283);

-- Inserting data into the HospitalPhoneNumbers table
INSERT INTO HospitalPhoneNumbers (TelephoneNumber, HospitalId)
VALUES ('+123456789', 1),
       ('+987654321', 1),
       ('+555555551', 2),
       ('+455555555', 2);

-- Inserting data into the HospitalTranslations table
INSERT INTO HospitalTranslations (Name, Address, Description, HospitalId, LangCode)
VALUES ('Hospital One', 'Address One', 'Description of Hospital One', 1, 'en'),
       ('مستشفى واحد', 'العنوان الأول', 'وصف المستشفى الأول', 1, 'ar'),
       ('Hospital Two', 'Address Two', 'Description of Hospital Two', 2, 'en'),
       ('مستشفى اثنين', 'العنوان الثاني', 'وصف المستشفى الثاني', 2, 'ar');
	   
--select * from Hospitals

GO
-- ===========================================================================================================================================================
-- Inserting data into the HosBuildings table
INSERT INTO HosBuildings (CodeNumber, Photo, IsDeleted, HospitalId)
VALUES ('B001', 'building1.jpeg', 0, 1),
       ('B002', 'building2.jpeg', 0, 1),
       ('B003', 'building3.jpeg', 0, 1),
       ('B004', 'building4.jpeg', 0, 2),
       ('B005', 'building5.jpeg', 0, 2),
       ('B006', 'building6.jpeg', 0, 2);

-- Inserting data into the BuildingTranslations table
INSERT INTO BuildingTranslations (Name, Description, BuildeingId, LangCode)
VALUES ('Building One', 'Description of Building One', 1, 'en'),
       ('مبنى واحد', 'وصف المبنى الأول', 1, 'ar'),
       ('Building Two', 'Description of Building Two', 2, 'en'),
       ('مبنى اثنين', 'وصف المبنى الثاني', 2, 'ar'),
       ('Building Three', 'Description of Building Three', 3, 'en'),
       ('مبنى ثلاثة', 'وصف المبنى الثالث', 3, 'ar'),
       ('Building Four', 'Description of Building Four', 4, 'en'),
       ('مبنى أربعة', 'وصف المبنى الرابع', 4, 'ar'),
       ('Building Five', 'Description of Building Five', 5, 'en'),
       ('مبنى خمسة', 'وصف المبنى الخامس', 5, 'ar'),
       ('Building Six', 'Description of Building Six', 6, 'en'),
       ('مبنى ستة', 'وصف المبنى السادس', 6, 'ar');

GO
-- ===========================================================================================================================================================
--select * from HosBuildings
-- Inserting data into the HosFloors table
INSERT INTO HosFloors (CodeNumber, Photo, IsDeleted, HospitalId, BuildId)
VALUES 
    ('F001', 'floor1.jpeg', 0, 1, 1),
    ('F002', 'floor2.jpeg', 0, 1, 1),
    ('F003', 'floor3.jpeg', 0, 1, 1),

    ('F004', 'floor4.jpeg', 0, 1, 2),
    ('F005', 'floor5.jpeg', 0, 1, 2),
    ('F006', 'floor5.jpeg', 0, 1, 2),


	('F008', 'floor1.jpeg', 0, 2, 4),
    ('F009', 'floor2.jpeg', 0, 2, 4),
    ('F010', 'floor3.jpeg', 0, 2, 4),

    ('F011', 'floor4.jpeg', 0, 2, 5),
    ('F012', 'floor5.jpeg', 0, 2, 5),
    ('F013', 'floor5.jpeg', 0, 2, 5),

    ('F013', 'floor5.jpeg', 0, 1, 3),
    ('F013', 'floor5.jpeg', 0, 2, 6);

go
-- Inserting data into the FloorTranslations table
INSERT INTO FloorTranslations (Name, Description, FloorId, LangCode)
VALUES 
    ('Floor One', 'Description of Floor One', 1, 'en'),
    ('طابق واحد', 'وصف الطابق الأول', 1, 'ar'),
    ('Floor Two', 'Description of Floor Two', 2, 'en'),
    ('طابق اثنين', 'وصف الطابق الثاني', 2, 'ar'),
    ('Floor Three', 'Description of Floor Three', 3, 'en'),
    ('طابق ثلاثة', 'وصف الطابق الثالث', 3, 'ar'),

    ('Floor One', 'Description of Floor One', 4, 'en'),
    ('طابق واحد', 'وصف الطابق الأول', 4, 'ar'),
    ('Floor Two', 'Description of Floor Two', 5, 'en'),
    ('طابق اثنين', 'وصف الطابق الثاني', 5, 'ar'),
    ('Floor Three', 'Description of Floor Three', 6, 'en'),
    ('طابق ثلاثة', 'وصف الطابق الثالث', 6, 'ar'),

    ('Floor One', 'Description of Floor One', 7, 'en'),
    ('طابق واحد', 'وصف الطابق الأول', 7, 'ar'),
    ('Floor Two', 'Description of Floor Two', 8, 'en'),
    ('طابق اثنين', 'وصف الطابق الثاني', 8, 'ar'),
    ('Floor Three', 'Description of Floor Three', 9, 'en'),
    ('طابق ثلاثة', 'وصف الطابق الثالث', 9, 'ar'),

    ('Floor One', 'Description of Floor One', 10, 'en'),
    ('طابق واحد', 'وصف الطابق الأول', 10, 'ar'),
    ('Floor Two', 'Description of Floor Two', 11, 'en'),
    ('طابق اثنين', 'وصف الطابق الثاني', 11, 'ar'),
    ('Floor Three', 'Description of Floor Three', 12, 'en'),
    ('طابق ثلاثة', 'وصف الطابق الثالث', 12, 'ar'),

	('Floor One', 'Description of Floor One', 13, 'en'),
    ('طابق واحد', 'وصف الطابق الأول', 13, 'ar'),
	('Floor One', 'Description of Floor One', 14, 'en'),
    ('طابق واحد', 'وصف الطابق الأول', 14, 'ar');

--select * from HosFloors
--select * from FloorTranslations
GO
-- ===========================================================================================================================================================
-- Inserting data into the RoomTypes table
INSERT INTO RoomTypes (CodeNumber, IsDeleted)
VALUES ('RT001', 0),
       ('RT002', 0),
       ('RT003', 0),
       ('RT004', 0),
       ('RT005', 0);

-- Inserting data into the RoomTypeTranslations table
INSERT INTO RoomTypeTranslations (Name, RoomTypeId, LangCode)
VALUES ('Room Type One', 1, 'en'),
       ('نوع الغرفة واحد', 1, 'ar'),
       ('Room Type Two', 2, 'en'),
       ('نوع الغرفة اثنين', 2, 'ar'),
       ('Room Type Three', 3, 'en'),
       ('نوع الغرفة ثلاثة', 3, 'ar'),
       ('Room Type Four', 4, 'en'),
       ('نوع الغرفة أربعة', 4, 'ar'),
       ('Room Type Five', 5, 'en'),
       ('نوع الغرفة خمسة', 5, 'ar');

GO
-- ===========================================================================================================================================================

--- ======== hos 1 build 1 ========
-- Inserting data into the HosRooms table

DECLARE @HospitalId INT;
DECLARE @BuildId INT;
DECLARE @FloorId INT;

SET @HospitalId = 1;
SET @BuildId = 1;
SET @FloorId = 1;

INSERT INTO HosRooms (Photo, CodeNumber, IsActive, IsDeleted, HospitalId, BuildId, FloorId, RoomTypeId)
VALUES
    ('room1.jpeg', 'R001', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room2.jpeg', 'R002', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room3.jpeg', 'R003', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room4.jpeg', 'R004', 1, 0,  @HospitalId, @BuildId, @FloorId,2),
    ('room5.jpeg', 'R005', 1, 0,  @HospitalId, @BuildId, @FloorId,3);

-- Inserting data into the RoomTranslations table
INSERT INTO RoomTranslations (Name, Description, RoomId, LangCode)
VALUES
    ('Room One', 'Description of Room One', 1, 'en'),
    ('غرفة واحدة', 'وصف الغرفة الأولى', 1, 'ar'),
    ('Room Two', 'Description of Room Two', 2, 'en'),
    ('غرفة اثنين', 'وصف الغرفة الثانية', 2, 'ar'),
    ('Room Three', 'Description of Room Three', 3, 'en'),
    ('غرفة ثلاثة', 'وصف الغرفة الثالثة', 3, 'ar'),
    ('Room Four', 'Description of Room Four', 4, 'en'),
    ('غرفة أربعة', 'وصف الغرفة الرابعة', 4, 'ar'),
    ('Room Five', 'Description of Room Five', 5, 'en'),
    ('غرفة خمسة', 'وصف الغرفة الخامسة', 5, 'ar');
GO
-- -----------------------------------------------------------------------------------------------------------------------
DECLARE @HospitalId INT;
DECLARE @BuildId INT;
DECLARE @FloorId INT;

SET @HospitalId = 1;
SET @BuildId = 1;
SET @FloorId = 2;
INSERT INTO HosRooms (Photo, CodeNumber, IsActive, IsDeleted, HospitalId, BuildId, FloorId, RoomTypeId)
VALUES
    ('room1.jpeg', 'R006', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room2.jpeg', 'R007', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room3.jpeg', 'R008', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room4.jpeg', 'R009', 1, 0,  @HospitalId, @BuildId, @FloorId,2),
    ('room5.jpeg', 'R010', 1, 0,  @HospitalId, @BuildId, @FloorId,3);

-- Inserting data into the RoomTranslations table
INSERT INTO RoomTranslations (Name, Description, RoomId, LangCode)
VALUES
    ('Room One', 'Description of Room One', 6, 'en'),
    ('غرفة واحدة', 'وصف الغرفة الأولى', 6, 'ar'),
    ('Room Two', 'Description of Room Two',7, 'en'),
    ('غرفة اثنين', 'وصف الغرفة الثانية', 7, 'ar'),
    ('Room Three', 'Description of Room Three', 8, 'en'),
    ('غرفة ثلاثة', 'وصف الغرفة الثالثة', 8, 'ar'),
    ('Room Four', 'Description of Room Four', 9, 'en'),
    ('غرفة أربعة', 'وصف الغرفة الرابعة', 9, 'ar'),
    ('Room Five', 'Description of Room Five', 10, 'en'),
    ('غرفة خمسة', 'وصف الغرفة الخامسة', 10, 'ar');
go

DECLARE @HospitalId INT;
DECLARE @BuildId INT;
DECLARE @FloorId INT;

SET @HospitalId = 1;
SET @BuildId = 1;
SET @FloorId = 3;
INSERT INTO HosRooms (Photo, CodeNumber, IsActive, IsDeleted, HospitalId, BuildId, FloorId, RoomTypeId)
VALUES
    ('room1.jpeg', 'R011', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room2.jpeg', 'R012', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room3.jpeg', 'R013', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room4.jpeg', 'R014', 1, 0,  @HospitalId, @BuildId, @FloorId,2),
    ('room5.jpeg', 'R015', 1, 0,  @HospitalId, @BuildId, @FloorId,3);

-- Inserting data into the RoomTranslations table
INSERT INTO RoomTranslations (Name, Description, RoomId, LangCode)
VALUES
    ('Room One', 'Description of Room One', 11, 'en'),
    ('غرفة واحدة', 'وصف الغرفة الأولى',11, 'ar'),
    ('Room Two', 'Description of Room Two',12, 'en'),
    ('غرفة اثنين', 'وصف الغرفة الثانية', 12, 'ar'),
    ('Room Three', 'Description of Room Three', 13, 'en'),
    ('غرفة ثلاثة', 'وصف الغرفة الثالثة', 13, 'ar'),
    ('Room Four', 'Description of Room Four', 14, 'en'),
    ('غرفة أربعة', 'وصف الغرفة الرابعة', 14, 'ar'),
    ('Room Five', 'Description of Room Five', 15, 'en'),
    ('غرفة خمسة', 'وصف الغرفة الخامسة', 15, 'ar');


GO
--- ======== hos 1 build 2 ========
-- Inserting data into the HosRooms table

DECLARE @HospitalId INT;
DECLARE @BuildId INT;
DECLARE @FloorId INT;

SET @HospitalId = 1;
SET @BuildId = 2;
SET @FloorId = 4;

INSERT INTO HosRooms (Photo, CodeNumber, IsActive, IsDeleted, HospitalId, BuildId, FloorId, RoomTypeId)
VALUES
    ('room1.jpeg', 'R016', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room2.jpeg', 'R017', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room3.jpeg', 'R018', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room4.jpeg', 'R019', 1, 0,  @HospitalId, @BuildId, @FloorId,2),
    ('room5.jpeg', 'R020', 1, 0,  @HospitalId, @BuildId, @FloorId,3);

-- Inserting data into the RoomTranslations table
INSERT INTO RoomTranslations (Name, Description, RoomId, LangCode)
VALUES
    ('Room One', 'Description of Room One', 16, 'en'),
    ('غرفة واحدة', 'وصف الغرفة الأولى', 16, 'ar'),
    ('Room Two', 'Description of Room Two', 17, 'en'),
    ('غرفة اثنين', 'وصف الغرفة الثانية', 17, 'ar'),
    ('Room Three', 'Description of Room Three', 18, 'en'),
    ('غرفة ثلاثة', 'وصف الغرفة الثالثة', 18, 'ar'),
    ('Room Four', 'Description of Room Four', 19, 'en'),
    ('غرفة أربعة', 'وصف الغرفة الرابعة', 19, 'ar'),
    ('Room Five', 'Description of Room Five', 20, 'en'),
    ('غرفة خمسة', 'وصف الغرفة الخامسة', 20, 'ar');
GO
-- -----------------------------------------------------------------------------------------------------------------------
DECLARE @HospitalId INT;
DECLARE @BuildId INT;
DECLARE @FloorId INT;

SET @HospitalId = 1;
SET @BuildId = 2;
SET @FloorId = 5;
INSERT INTO HosRooms (Photo, CodeNumber, IsActive, IsDeleted, HospitalId, BuildId, FloorId, RoomTypeId)
VALUES
    ('room1.jpeg', 'R021', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room2.jpeg', 'R022', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room3.jpeg', 'R023', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room4.jpeg', 'R024', 1, 0,  @HospitalId, @BuildId, @FloorId,2),
    ('room5.jpeg', 'R025', 1, 0,  @HospitalId, @BuildId, @FloorId,3);

-- Inserting data into the RoomTranslations table
INSERT INTO RoomTranslations (Name, Description, RoomId, LangCode)
VALUES
    ('Room One', 'Description of Room One', 21, 'en'),
    ('غرفة واحدة', 'وصف الغرفة الأولى', 21, 'ar'),
    ('Room Two', 'Description of Room Two',22, 'en'),
    ('غرفة اثنين', 'وصف الغرفة الثانية', 22, 'ar'),
    ('Room Three', 'Description of Room Three', 23, 'en'),
    ('غرفة ثلاثة', 'وصف الغرفة الثالثة', 23, 'ar'),
    ('Room Four', 'Description of Room Four', 24, 'en'),
    ('غرفة أربعة', 'وصف الغرفة الرابعة', 24, 'ar'),
    ('Room Five', 'Description of Room Five', 25, 'en'),
    ('غرفة خمسة', 'وصف الغرفة الخامسة', 25, 'ar');
go

DECLARE @HospitalId INT;
DECLARE @BuildId INT;
DECLARE @FloorId INT;

SET @HospitalId = 1;
SET @BuildId = 2;
SET @FloorId = 6;
INSERT INTO HosRooms (Photo, CodeNumber, IsActive, IsDeleted, HospitalId, BuildId, FloorId, RoomTypeId)
VALUES
    ('room1.jpeg', 'R026', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room2.jpeg', 'R027', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room3.jpeg', 'R028', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room4.jpeg', 'R029', 1, 0,  @HospitalId, @BuildId, @FloorId,2),
    ('room5.jpeg', 'R030', 1, 0,  @HospitalId, @BuildId, @FloorId,3);

-- Inserting data into the RoomTranslations table
INSERT INTO RoomTranslations (Name, Description, RoomId, LangCode)
VALUES
    ('Room One', 'Description of Room One', 26, 'en'),
    ('غرفة واحدة', 'وصف الغرفة الأولى', 26, 'ar'),
    ('Room Two', 'Description of Room Two',27, 'en'),
    ('غرفة اثنين', 'وصف الغرفة الثانية', 27, 'ar'),
    ('Room Three', 'Description of Room Three', 28, 'en'),
    ('غرفة ثلاثة', 'وصف الغرفة الثالثة', 28, 'ar'),
    ('Room Four', 'Description of Room Four', 29, 'en'),
    ('غرفة أربعة', 'وصف الغرفة الرابعة', 29, 'ar'),
    ('Room Five', 'Description of Room Five', 30, 'en'),
    ('غرفة خمسة', 'وصف الغرفة الخامسة', 30, 'ar');



--- ======== hos 1 build 3 ========
-- Inserting data into the HosRooms table
GO
DECLARE @HospitalId INT;
DECLARE @BuildId INT;
DECLARE @FloorId INT;

SET @HospitalId = 2;
SET @BuildId = 1;
SET @FloorId = 7;

INSERT INTO HosRooms (Photo, CodeNumber, IsActive, IsDeleted, HospitalId, BuildId, FloorId, RoomTypeId)
VALUES
    ('room1.jpeg', 'R031', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room2.jpeg', 'R032', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room3.jpeg', 'R033', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room4.jpeg', 'R034', 1, 0,  @HospitalId, @BuildId, @FloorId,2),
    ('room5.jpeg', 'R035', 1, 0,  @HospitalId, @BuildId, @FloorId,3);

-- Inserting data into the RoomTranslations table
INSERT INTO RoomTranslations (Name, Description, RoomId, LangCode)
VALUES
    ('Room One', 'Description of Room One', 31, 'en'),
    ('غرفة واحدة', 'وصف الغرفة الأولى', 31, 'ar'),
    ('Room Two', 'Description of Room Two', 32, 'en'),
    ('غرفة اثنين', 'وصف الغرفة الثانية', 32, 'ar'),
    ('Room Three', 'Description of Room Three', 33, 'en'),
    ('غرفة ثلاثة', 'وصف الغرفة الثالثة', 33, 'ar'),
    ('Room Four', 'Description of Room Four', 34, 'en'),
    ('غرفة أربعة', 'وصف الغرفة الرابعة', 34, 'ar'),
    ('Room Five', 'Description of Room Five', 35, 'en'),
    ('غرفة خمسة', 'وصف الغرفة الخامسة', 35, 'ar');
GO
-- -----------------------------------------------------------------------------------------------------------------------
DECLARE @HospitalId INT;
DECLARE @BuildId INT;
DECLARE @FloorId INT;

SET @HospitalId = 2;
SET @BuildId = 1;
SET @FloorId = 8;
INSERT INTO HosRooms (Photo, CodeNumber, IsActive, IsDeleted, HospitalId, BuildId, FloorId, RoomTypeId)
VALUES
    ('room1.jpeg', 'R036', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room2.jpeg', 'R037', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room3.jpeg', 'R038', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room4.jpeg', 'R039', 1, 0,  @HospitalId, @BuildId, @FloorId,2),
    ('room5.jpeg', 'R040', 1, 0,  @HospitalId, @BuildId, @FloorId,3);

-- Inserting data into the RoomTranslations table
INSERT INTO RoomTranslations (Name, Description, RoomId, LangCode)
VALUES
    ('Room One', 'Description of Room One', 36, 'en'),
    ('غرفة واحدة', 'وصف الغرفة الأولى', 36, 'ar'),
    ('Room Two', 'Description of Room Two',37, 'en'),
    ('غرفة اثنين', 'وصف الغرفة الثانية', 37, 'ar'),
    ('Room Three', 'Description of Room Three', 38, 'en'),
    ('غرفة ثلاثة', 'وصف الغرفة الثالثة', 38, 'ar'),
    ('Room Four', 'Description of Room Four', 39, 'en'),
    ('غرفة أربعة', 'وصف الغرفة الرابعة', 39, 'ar'),
    ('Room Five', 'Description of Room Five', 40, 'en'),
    ('غرفة خمسة', 'وصف الغرفة الخامسة', 40, 'ar');
go

DECLARE @HospitalId INT;
DECLARE @BuildId INT;
DECLARE @FloorId INT;

SET @HospitalId = 2;
SET @BuildId = 1;
SET @FloorId = 9;
INSERT INTO HosRooms (Photo, CodeNumber, IsActive, IsDeleted, HospitalId, BuildId, FloorId, RoomTypeId)
VALUES
    ('room1.jpeg', 'R041', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room2.jpeg', 'R042', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room3.jpeg', 'R043', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room4.jpeg', 'R044', 1, 0,  @HospitalId, @BuildId, @FloorId,2),
    ('room5.jpeg', 'R045', 1, 0,  @HospitalId, @BuildId, @FloorId,3);

-- Inserting data into the RoomTranslations table
INSERT INTO RoomTranslations (Name, Description, RoomId, LangCode)
VALUES
    ('Room One', 'Description of Room One', 41, 'en'),
    ('غرفة واحدة', 'وصف الغرفة الأولى', 41, 'ar'),
    ('Room Two', 'Description of Room Two', 42, 'en'),
    ('غرفة اثنين', 'وصف الغرفة الثانية', 42, 'ar'),
    ('Room Three', 'Description of Room Three', 43, 'en'),
    ('غرفة ثلاثة', 'وصف الغرفة الثالثة', 43, 'ar'),
    ('Room Four', 'Description of Room Four', 44, 'en'),
    ('غرفة أربعة', 'وصف الغرفة الرابعة', 44, 'ar'),
    ('Room Five', 'Description of Room Five', 45, 'en'),
    ('غرفة خمسة', 'وصف الغرفة الخامسة', 45, 'ar');




	
--- ======== hos 2 build 1 ========
-- Inserting data into the HosRooms table
GO
DECLARE @HospitalId INT;
DECLARE @BuildId INT;
DECLARE @FloorId INT;

SET @HospitalId = 2;
SET @BuildId = 2;
SET @FloorId = 10;

INSERT INTO HosRooms (Photo, CodeNumber, IsActive, IsDeleted, HospitalId, BuildId, FloorId, RoomTypeId)
VALUES
    ('room1.jpeg', 'R046', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room2.jpeg', 'R047', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room3.jpeg', 'R048', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room4.jpeg', 'R049', 1, 0,  @HospitalId, @BuildId, @FloorId,2),
    ('room5.jpeg', 'R050', 1, 0,  @HospitalId, @BuildId, @FloorId,3);

-- Inserting data into the RoomTranslations table
INSERT INTO RoomTranslations (Name, Description, RoomId, LangCode)
VALUES
    ('Room One', 'Description of Room One', 46, 'en'),
    ('غرفة واحدة', 'وصف الغرفة الأولى', 46, 'ar'),
    ('Room Two', 'Description of Room Two', 47, 'en'),
    ('غرفة اثنين', 'وصف الغرفة الثانية', 47, 'ar'),
    ('Room Three', 'Description of Room Three', 48, 'en'),
    ('غرفة ثلاثة', 'وصف الغرفة الثالثة', 48, 'ar'),
    ('Room Four', 'Description of Room Four', 49, 'en'),
    ('غرفة أربعة', 'وصف الغرفة الرابعة', 49, 'ar'),
    ('Room Five', 'Description of Room Five', 50, 'en'),
    ('غرفة خمسة', 'وصف الغرفة الخامسة', 50, 'ar');
GO
-- -----------------------------------------------------------------------------------------------------------------------
DECLARE @HospitalId INT;
DECLARE @BuildId INT;
DECLARE @FloorId INT;

SET @HospitalId = 2;
SET @BuildId = 2;
SET @FloorId = 11;
INSERT INTO HosRooms (Photo, CodeNumber, IsActive, IsDeleted, HospitalId, BuildId, FloorId, RoomTypeId)
VALUES
    ('room1.jpeg', 'R051', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room2.jpeg', 'R052', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room3.jpeg', 'R053', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room4.jpeg', 'R054', 1, 0,  @HospitalId, @BuildId, @FloorId,2),
    ('room5.jpeg', 'R055', 1, 0,  @HospitalId, @BuildId, @FloorId,3);
go
-- Inserting data into the RoomTranslations table
INSERT INTO RoomTranslations (Name, Description, RoomId, LangCode)
VALUES
    ('Room One', 'Description of Room One', 51, 'en'),
    ('غرفة واحدة', 'وصف الغرفة الأولى', 51, 'ar'),
    ('Room Two', 'Description of Room Two',52, 'en'),
    ('غرفة اثنين', 'وصف الغرفة الثانية', 52, 'ar'),
    ('Room Three', 'Description of Room Three', 53, 'en'),
    ('غرفة ثلاثة', 'وصف الغرفة الثالثة', 53, 'ar'),
    ('Room Four', 'Description of Room Four', 54, 'en'),
    ('غرفة أربعة', 'وصف الغرفة الرابعة', 54, 'ar'),
    ('Room Five', 'Description of Room Five', 55, 'en'),
    ('غرفة خمسة', 'وصف الغرفة الخامسة', 55, 'ar');
go

DECLARE @HospitalId INT;
DECLARE @BuildId INT;
DECLARE @FloorId INT;

SET @HospitalId = 2;
SET @BuildId = 2;
SET @FloorId = 12;
INSERT INTO HosRooms (Photo, CodeNumber, IsActive, IsDeleted, HospitalId, BuildId, FloorId, RoomTypeId)
VALUES
    ('room1.jpeg', 'R056', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room2.jpeg', 'R057', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room3.jpeg', 'R058', 1, 0,  @HospitalId, @BuildId, @FloorId,1),
    ('room4.jpeg', 'R059', 1, 0,  @HospitalId, @BuildId, @FloorId,2),
    ('room5.jpeg', 'R060', 1, 0,  @HospitalId, @BuildId, @FloorId,3);

-- Inserting data into the RoomTranslations table
INSERT INTO RoomTranslations (Name, Description, RoomId, LangCode)
VALUES
    ('Room One', 'Description of Room One', 56, 'en'),
    ('غرفة واحدة', 'وصف الغرفة الأولى', 56, 'ar'),
    ('Room Two', 'Description of Room Two',57, 'en'),
    ('غرفة اثنين', 'وصف الغرفة الثانية', 57, 'ar'),
    ('Room Three', 'Description of Room Three', 58, 'en'),
    ('غرفة ثلاثة', 'وصف الغرفة الثالثة', 58, 'ar'),
    ('Room Four', 'Description of Room Four', 59, 'en'),
    ('غرفة أربعة', 'وصف الغرفة الرابعة', 59, 'ar'),
    ('Room Five', 'Description of Room Five', 60, 'en'),
    ('غرفة خمسة', 'وصف الغرفة الخامسة', 60, 'ar');


GO
-- ===========================================================================================================================================================

-- Inserting data into the MedicalSpecialties table
INSERT INTO MedicalSpecialties (CodeNumber, IsActive, IsDeleted, Reason, Appearance, Photo)
VALUES
    ('Specialty001', 1, 0, 'Reason for Specialty 001', 1, 'specialty001.jpg'),
    ('Specialty002', 1, 0, 'Reason for Specialty 002', 1, 'specialty002.jpg'),
    ('Specialty003', 1, 0, 'Reason for Specialty 003', 1, 'specialty003.jpg'),
    ('Specialty004', 1, 0, 'Reason for Specialty 004', 1, 'specialty004.jpg'),
    ('Specialty005', 1, 0, 'Reason for Specialty 005', 1, 'specialty005.jpg'),
    ('Specialty006', 1, 0, 'Reason for Specialty 006', 1, 'specialty006.jpg'),
    ('Specialty007', 1, 0, 'Reason for Specialty 007', 1, 'specialty007.jpg'),
    ('Specialty008', 1, 0, 'Reason for Specialty 008', 1, 'specialty008.jpg'),
    ('Specialty009', 1, 0, 'Reason for Specialty 009', 1, 'specialty009.jpg'),
    ('Specialty010', 1, 0, 'Reason for Specialty 010', 1, 'specialty010.jpg');

-- Inserting data into the MedicalSpecialtyTranslations table
INSERT INTO MedicalSpecialtyTranslations (Name, Description, MedicalSpecialtyId, LangCode)
VALUES
    ('Specialty One', 'Description of Specialty One', 1, 'en'),
    ('تخصص واحد', 'وصف التخصص الأول', 1, 'ar'),
    ('Specialty Two', 'Description of Specialty Two', 2, 'en'),
    ('تخصص اثنين', 'وصف التخصص الثاني', 2, 'ar'),
    ('Specialty Three', 'Description of Specialty Three', 3, 'en'),
    ('تخصص ثلاثة', 'وصف التخصص الثالث', 3, 'ar'),
    ('Specialty Four', 'Description of Specialty Four', 4, 'en'),
    ('تخصص أربعة', 'وصف التخصص الرابع', 4, 'ar'),
    ('Specialty Five', 'Description of Specialty Five', 5, 'en'),
    ('تخصص خمسة', 'وصف التخصص الخامس', 5, 'ar'),
    ('Specialty Six', 'Description of Specialty Six', 6, 'en'),
    ('تخصص ستة', 'وصف التخصص السادس', 6, 'ar'),
    ('Specialty Seven', 'Description of Specialty Seven', 7, 'en'),
    ('تخصص سبعة', 'وصف التخصص السابع', 7, 'ar'),
    ('Specialty Eight', 'Description of Specialty Eight', 8, 'en'),
    ('تخصص ثمانية', 'وصف التخصص الثامن', 8, 'ar'),
    ('Specialty Nine', 'Description of Specialty Nine', 9, 'en'),
    ('تخصص تسعة', 'وصف التخصص التاسع', 9, 'ar'),
    ('Specialty Ten', 'Description of Specialty Ten', 10, 'en'),
    ('تخصص عشرة', 'وصف التخصص العاشر', 10, 'ar');

GO
-- ===========================================================================================================================================================

-- Inserting data into the Clinics table
INSERT INTO Clinics (CodeNumber, Photo, IsActive, IsDeleted, Reason, Phone, WorkingHours, Appearance, HospitalId, BuildId, FloorId, RoomId, SpecialtyId)
VALUES
    ('C001', 'clinic1.jpg', 1, 0, 'Reason 1', '123456789', 8, 1,  1, 1,  1, 1, 1),
    ('C002', 'clinic2.jpg', 1, 0, 'Reason 2', '987654321', 8, 1,  1, 1,  2, 2, 2),
    ('C003', 'clinic3.jpg', 1, 0, 'Reason 3', '555555555', 8, 1,  1, 2,  3, 3, 3),
    ('C004', 'clinic4.jpg', 1, 0, 'Reason 4', '111111111', 8, 1,  1, 2,  4, 4, 4),
    ('C005', 'clinic5.jpg', 1, 0, 'Reason 5', '999999999', 8, 1,  1, 3,  5, 5, 5),
    ('C006', 'clinic6.jpg', 1, 0, 'Reason 6', '777777777', 8, 1,  2, 4,  7, 6, 6),
    ('C007', 'clinic7.jpg', 1, 0, 'Reason 7', '333333333', 8, 1,  2, 4,  8, 7, 7),
    ('C008', 'clinic8.jpg', 1, 0, 'Reason 8', '444444444', 8, 1,  2, 5,  9, 8, 8),
    ('C009', 'clinic9.jpg', 1, 0, 'Reason 9', '222222222', 8, 1,  2, 5, 10, 9, 9),
    ('C010','clinic10.jpg', 1, 0, 'Reason10', '888888888', 8, 1,  2, 6, 11, 10, 10);

-- Inserting data into the ClinicTranslations table
INSERT INTO ClinicTranslations (Name, Description, ClinicId, LangCode)
VALUES
    ('Clinic One', 'Description of Clinic One', 1, 'en'),
    ('عيادة واحدة', 'وصف العيادة الأولى', 1, 'ar'),
    ('Clinic Two', 'Description of Clinic Two', 2, 'en'),
    ('عيادة اثنين', 'وصف العيادة الثانية', 2, 'ar'),
    ('Clinic Three', 'Description of Clinic Three', 3, 'en'),
    ('عيادة ثلاثة', 'وصف العيادة الثالثة', 3, 'ar'),
    ('Clinic Four', 'Description of Clinic Four', 4, 'en'),
    ('عيادة أربعة', 'وصف العيادة الرابعة', 4, 'ar'),
    ('Clinic Five', 'Description of Clinic Five', 5, 'en'),
    ('عيادة خمسة', 'وصف العيادة الخامسة', 5, 'ar'),
    ('Clinic Six', 'Description of Clinic Six', 6, 'en'),
    ('عيادة ستة', 'وصف العيادة السادسة', 6, 'ar'),
    ('Clinic Seven', 'Description of Clinic Seven', 7, 'en'),
    ('عيادة سبعة', 'وصف العيادة السابعة', 7, 'ar'),
    ('Clinic Eight', 'Description of Clinic Eight', 8, 'en'),
    ('عيادة ثمانية', 'وصف العيادة الثامنة', 8, 'ar'),
    ('Clinic Nine', 'Description of Clinic Nine', 9, 'en'),
    ('عيادة تسعة', 'وصف العيادة التاسعة', 9, 'ar'),
    ('Clinic Ten', 'Description of Clinic Ten', 10, 'en'),
    ('عيادة عشرة', 'وصف العيادة العاشرة', 10, 'ar');

GO
-- ===========================================================================================================================================================
-- Inserting data into SSNTypes
INSERT INTO SSNTypes (CodeNumber)
VALUES ('SSN1'),
       ('SSN2'),
       ('SSN3'),
       ('SSN4'),
       ('SSN5');

-- Inserting translation data for the SSN types (English and Arabic translations)
-- Assuming SSNTypeId starts from 1 for the first row in SSNTypes table
INSERT INTO SSNTypesTranslations (Name, SSNTypeId, LangCode)
VALUES
('Social Security Number 1', 1, 'en'),
('رقم الضمان الاجتماعي 1', 1, 'ar'),
('Social Security Number 2', 2, 'en'),
('رقم الضمان الاجتماعي 2', 2, 'ar'),
('Social Security Number 3', 3, 'en'),
('رقم الضمان الاجتماعي 3', 3, 'ar'),
-- Continue inserting translations for other SSN types and languages.
('Social Security Number 4', 4, 'en'),
('رقم الضمان الاجتماعي 4', 4, 'ar'),
('Social Security Number 5', 5, 'en'),
('رقم الضمان الاجتماعي 5', 5, 'ar');

GO
-- ===========================================================================================================================================================
-- Inserting data into WorkingPeriod
INSERT INTO WorkingPeriod (CodeNumber, StartTime, EndTime)
VALUES ('WP1', '08:00', '12:00'),
       ('WP2', '13:00', '17:00'),
       ('WP3', '09:00', '13:00'),
       ('WP4', '14:00', '18:00'),
       ('WP5', '10:00', '14:00');

-- Inserting translation data for the working periods (English and Arabic translations)
-- Assuming WorkingPeriodId starts from 1 for the first row in WorkingPeriod table
INSERT INTO WorkingPeriodTranslations (Name, WorkingPeriodId, LangCode)
VALUES
('Morning Shift', 1, 'en'),
('فترة الصباح', 1, 'ar'),
('Afternoon Shift', 2, 'en'),
('فترة الظهر', 2, 'ar'),
('Day Shift', 3, 'en'),
('فترة النهار', 3, 'ar'),
-- Continue inserting translations for other working periods and languages.
('Evening Shift', 4, 'en'),
('فترة المساء', 4, 'ar'),
('Late Shift', 5, 'en'),
('فترة الليل', 5, 'ar');

GO
-- ===========================================================================================================================================================
-- Inserting data into PriceCategories
INSERT INTO PriceCategories (CodeNumber, Symbol)
VALUES ('PC1', '$'),
       ('PC2', '€'),
       ('PC3', '£'),
       ('PC4', '¥'),
       ('PC5', '₹');

-- Inserting translation data for the price categories (English and Arabic translations)
-- Assuming PriceCategoryId starts from 1 for the first row in PriceCategories table
INSERT INTO PriceCategoryTranslations (Name, PriceCategoryId, Description, LangCode)
VALUES
('General', 1, 'Gold Currency', 'en'),
('الذهبية', 1, 'العملة الذهبية', 'ar'),
('Silver', 2, 'Silver Currency', 'en'),
('الفضية', 2, 'العملة الفضية', 'ar'),
('Bronze', 3, 'Bronze Currency', 'en'),
('البرونزية', 3, 'العملة البرونزية', 'ar'),
-- Continue inserting translations for other price categories and languages.
('Yen', 4, 'Yen Currency', 'en'),
('الين', 4, 'العملة الين', 'ar'),
('Rupees', 5, 'Rupees Currency', 'en'),
('الروبية', 5, 'العملة الروبية', 'ar');
GO
-- ===========================================================================================================================================================
-- Inserting data into TypesVisit
INSERT INTO TypesVisit (CodeNumber) VALUES ('TV1'), ('TV2'), ('TV3'), ('TV4'), ('TV5');

-- Inserting translation data for TypesVisit (English and Arabic translations)
-- Assuming TypeVisitId starts from 1 for the first row in TypesVisit table
INSERT INTO TypesVisitTranslations (Name, TypeVisitId, LangCode)
VALUES
('Visit Type 1', 1, 'en'),
('زيارة نوع 1', 1, 'ar'),
('Visit Type 2', 2, 'en'),
('زيارة نوع 2', 2, 'ar'),
('Visit Type 3', 3, 'en'),
('زيارة نوع 3', 3, 'ar'),
('Visit Type 4', 4, 'en'),
('زيارة نوع 4', 4, 'ar'),
('Visit Type 5', 5, 'en'),
('زيارة نوع 5', 5, 'ar');
GO
-- ===========================================================================================================================================================
-- Inserting data into Nationalities
INSERT INTO Nationalities (Symbol) VALUES ('NT1'), ('NT2'), ('NT3'), ('NT4'), ('NT5');

-- Inserting translation data for Nationalities (English and Arabic translations)
-- Assuming NationalityId starts from 1 for the first row in Nationalities table
INSERT INTO NationalitiesTranslations (Name, NationalityId, LangCode)
VALUES
('Nationality 1', 1, 'en'),
('الجنسية 1', 1, 'ar'),
('Nationality 2', 2, 'en'),
('الجنسية 2', 2, 'ar'),
('Nationality 3', 3, 'en'),
('الجنسية 3', 3, 'ar'),
('Nationality 4', 4, 'en'),
('الجنسية 4', 4, 'ar'),
('Nationality 5', 5, 'en'),
('الجنسية 5', 5, 'ar');
GO
-- ===========================================================================================================================================================
-- Inserting data into DoctorsDegrees
INSERT INTO DoctorsDegrees (CreateOn) VALUES (GETDATE()), (GETDATE()), (GETDATE()), (GETDATE());

-- Inserting translation data for DoctorsDegrees (English and Arabic translations)
-- Assuming DoctorDegreeId starts from 1 for the first row in DoctorsDegrees table
INSERT INTO DoctorsDegreesTranslations (DegreeName, DoctorDegreeId, LangCode)
VALUES
('General', 1, 'en'),
('عام', 1, 'ar'),
('Specialist', 2, 'en'),
('اخصائي', 2, 'ar'),
('Consultant', 3, 'en'),
('استشاري', 3, 'ar'),
('University Professor', 4, 'en'),
('أستاذ جامعي', 4, 'ar');

GO
-- ===========================================================================================================================================================
-- Inserting data into EmployeesStatus
INSERT INTO EmployeesStatus (CreateOn) VALUES (GETDATE()), (GETDATE()), (GETDATE()), (GETDATE()), (GETDATE());

-- Inserting translation data for EmployeesStatus (English and Arabic translations)
-- Assuming EmployeeStatusId starts from 1 for the first row in EmployeesStatus table
INSERT INTO EmployeesStatusTranslations (StatusName, EmployeeStatusId, LangCode)
VALUES
('On Leave', 1, 'en'),
('في إجازة', 1, 'ar'),
('Sick', 2, 'en'),
('مريض', 2, 'ar'),
('Terminated', 3, 'en'),
('مفصول', 3, 'ar'),
('No Longer Employed', 4, 'en'),
('لم يعد يعمل معنا', 4, 'ar');

GO
-- ===========================================================================================================================================================
-- Inserting data into Doctors
INSERT INTO Doctors (CodeNumber, gender, Photo, WorkingHours, DocStatus, Reason, PhoneNumber, DoctorsDegreeId, NationalityId)
VALUES
('DOC001', 1, 'photo1.jpg', 8, 0, NULL, '+123456789', 1, 1),
('DOC002', 2, 'photo2.jpg', 9, 0, NULL, '+987654321', 2, 2),
('DOC003', 1, 'photo3.jpg', 7, 2, 'Standing reason', '+111111111', 3, 3),
('DOC004', 1, 'photo4.jpg', 10, 1, 'Inactive reason', '+222222222', 1, 2),
('DOC005', 2, 'photo5.jpg', 8, 0, NULL, '+333333333', 4, 1);

-- Inserting translation data for Doctors (English and Arabic translations)
-- Assuming DoctorId starts from 1 for the first row in Doctors table
INSERT INTO DoctorTranslations (FullName, Headline, About, DoctorId, LangCode)
VALUES
('Dr. John Doe', 'General Practitioner', 'Experienced doctor specialized in general medicine.', 1, 'en'),
('د. أحمد علي', 'اختصاصي باطنية', 'طبيب ذو خبرة في الطب العام والأمراض الداخلية.', 1, 'ar'),
('Dr. Sarah Smith', 'Pediatrician', 'Pediatric specialist with expertise in child health.', 2, 'en'),
('د. محمد خالد', 'أخصائي أطفال', 'طبيب مختص في صحة الأطفال.', 2, 'ar'),
('Dr. Michael Johnson', 'Orthopedic Surgeon', 'Highly skilled orthopedic surgeon.', 3, 'en'),
('د. أحمد مصطفى', 'جراح عظام', 'جراح عظام ذو خبرة عالية.', 3, 'ar'),
('Dr. Emily Williams', 'Obstetrician-Gynecologist', 'Specialized in women s health and reproductive system.', 4, 'en'),
('د. نورة عبدالله', 'نسائية وتوليد', 'خبيرة في صحة المرأة والجهاز التناسلي.', 4, 'ar'),
('Dr. David Lee', 'Dermatologist', 'Dermatology specialist with expertise in skin conditions.', 5, 'en'),
('د. عبدالرحمن السعيد', 'أخصائي أمراض جلدية', 'مختص في أمراض الجلد والأمراض المتعلقة بالجلد.', 5, 'ar');
GO
-- ===========================================================================================================================================================
INSERT INTO DoctorsWorkHospital(DoctorId,HospitalId)
VALUES
    (1,1),
    (2,2),
    (3,1),
    (4,1),
    (5,2)
GO
-- ===========================================================================================================================================================
INSERT INTO DoctorVisitPrices (Price, PriceCurrency, DoctorId, PriceCategoryId, TypeVisitId)
VALUES
(100, 'USD', 1, 1, 1), -- DoctorId = 1, PriceCategoryId = 1, TypeVisitId = 1
(80, 'USD', 1, 2, 2),  -- DoctorId = 1, PriceCategoryId = 2, TypeVisitId = 2
(120, 'USD', 2, 1, 1), -- DoctorId = 2, PriceCategoryId = 1, TypeVisitId = 1
(90, 'USD', 2, 2, 2),  -- DoctorId = 2, PriceCategoryId = 2, TypeVisitId = 2
(150, 'USD', 3, 1, 1); -- DoctorId = 3, PriceCategoryId = 1, TypeVisitId = 1

GO
-- ===========================================================================================================================================================
INSERT INTO SpecialtiesDoctors (DoctorId, MedicalSpecialtyId, CreateOn)
VALUES 
    (1, 1, GETDATE()),
    (1, 2, GETDATE()),
    (2, 1, GETDATE()),
    (3, 2, GETDATE()),
    (4, 4, GETDATE());

GO
-- ===========================================================================================================================================================
INSERT INTO DoctorWorkPeriods (HospitalId, SpecialtyId, ClinicId, DoctorId, WorkingPeriodId, DayId)
VALUES 
    (1,1, 1, 1, 1, 1),
    (2,1, 2, 2, 2, 2),
    (1,3, 3, 3, 3, 3),
    (2,2, 4, 4, 4, 4),
    (1,2, 5, 5, 5, 5);


-- Insert data into Countries table
INSERT INTO Countries (CallingCode, NumberOfDigits, CountryCode, CurrencyCode, CurrencySymbol, NationalFlag, Longitude, Latitude)
VALUES
    ('+20', 10, 'EG', 'EGP', 'Le', 'egypt_flag.jpg', 31.23, 30.04)


-- Insert data into MaritalStatus table
INSERT INTO MaritalStatus (Id)
VALUES
    (1), -- Single
    (2), -- Married
    (3), -- Divorced
    (4); -- Widower

-- Insert data into MaritalStatusTranslations table (English and Arabic translations)
INSERT INTO MaritalStatusTranslations (Name, MaritalId, LangCode)
VALUES
    (N'Single', 1, 'en'),
    (N'أعذب', 1, 'ar'),

    (N'Married', 2, 'en'),
    (N'متزوج', 2, 'ar'),

    (N'Divorced', 3, 'en'),
    (N'مطلق', 3, 'ar'),

    (N'Widower', 4, 'en'),
    (N'أرمل', 4, 'ar');

















    
GO
-- ===========================================================================================================================================================
-- Insert data into the Patients table
INSERT INTO Patients (MedicalFileNumber, PhoneNumber, gender, BirthDate, MaritalStatus, SSN, NationalID, BloodType, PatientStatus, Photo, Religion, SSNTypeId, IsDeleted, NationalityId)
VALUES
    ('MFN12345', '123456789', 1, '1990-05-15', 2, 1, 1, 'A+', 0, 'photo1.jpg', 1, 1, 0,   1),
    ('MFN67890', '987654321', 2, '1985-09-22', 1, 2, 2, 'B-', 0, 'photo2.jpg', 2, 2, 0,  2),
    ('MFN98765', '555555555', 1, '1978-12-10', 3, 3, 3, 'O+', 1, 'photo3.jpg', 3, 3, 0, 3),
    ('MFN54321', '111111111', 2, '2000-03-25', 1, 4, 4, 'AB+', 0, 'photo4.jpg', 4, 4, 0,   4),
    ('MFN11111', '999999999', 1, '1995-08-05', 2, 5, 5, 'A-', 0, 'photo5.jpg', 5, 5, 0,  5);

-- Insert data into the PatientTranslations table with Arabic and English names
INSERT INTO PatientTranslations (FullName, Address, Occupation, Employer, RelationshipClient, PatientId, LangCode)
VALUES
    ('جون دو', '١٢٣ شارع الرئيسي', 'مهندس', 'شركة أي بي سي', 1,1, 'ar'),
    ('جين سميث', '٤٥٦ شارع الدردار', 'طبيب', 'مستشفى أي بي سي',1, 2, 'ar'),
    ('أحمد محمد', '٧٨٩ شارع البلوط', 'مدرس', 'مدرسة أي بي سي',1, 3, 'ar'),
    ('فاطمة علي', '١١١ شارع الصنوبر', 'ممرضة', 'مستشفى أي بي سي',1, 4, 'ar'),
    ('ميشيل جيسون', '222 Maple St', 'محامي', 'مكتب محاماة أي بي سي',1, 5, 'ar'),
    ('John Doe', '123 Main St', 'Engineer', 'ABC Company',1, 1, 'en'),
    ('Jane Smith', '456 Oak St', 'Doctor', 'ABC Hospital',1, 2, 'en'),
    ('Ahmed Mohamed', '789 Pine St', 'Teacher', 'ABC School',1, 3, 'en'),
    ('Fatima Ali', '111 Spruce St', 'Nurse', 'ABC Hospital',1, 4, 'en'),
    ('Michael Johnson', '222 Maple St', 'Lawyer', 'ABC Law Firm',1, 5, 'en');
GO
-- ===========================================================================================================================================================
-- Inserting data into BookingStatuses
-- INSERT INTO BookingStatuses (CreateOn)
-- VALUES (GETDATE()), -- Assuming current date and time for simplicity
--        (GETDATE()),
--        (GETDATE()),
--        (GETDATE()),
--        (GETDATE());

-- -- Inserting translation data for the booking statuses (English and Arabic translations)
-- -- Assuming BookingStatusId starts from 1 for the first row in BookingStatuses table
-- INSERT INTO BookingStatusesTranslations (StatusName, BookingStatusId, LangCode)
-- VALUES
-- ('Pending', 1, 'en'),
-- ('قيد الانتظار', 1, 'ar'),
-- ('Confirmed', 2, 'en'),
-- ('تم التأكيد', 2, 'ar'),
-- ('Cancelled', 3, 'en'),
-- ('تم الإلغاء', 3, 'ar'),
-- -- Continue inserting translations for other booking statuses and languages.
-- ('Completed', 4, 'en'),
-- ('تم الانتهاء', 4, 'ar'),
-- ('In Progress', 5, 'en'),
-- ('جاري التنفيذ', 5, 'ar');
-- GO
GO
-- ===========================================================================================================================================================
INSERT INTO Booking (BookingNumber,PatientId, HospitalId, SpecialtyId, DoctorId, WorkingPeriodId, TypeVisitId, ClinicId, PriceCategoryId, CurrencyId, BookingStatusId, Price, VisitingDate,DayNumber,StatusReason,BookingReason)
VALUES 
  ('123456789121',1, 1, 3, 4, 5, 1, 2, 3, 4, 4, 100,  '2020-01-01T00:00:00+00:00',1,'',''),
  ('133456789121',3, 1, 5, 1, 2, 2, 3, 4, 5, 4, 200,  '2020-02-01T00:00:00+00:00',2,'',''),
  ('124456789121',2, 1, 4, 5, 1, 3, 4, 5, 1, 4, 300,  '2020-03-01T00:00:00+00:00',2,'',''),
  ('123556789121',4, 1, 1, 2, 2, 4, 5, 1, 2, 4, 400,  '2020-04-01T00:00:00+00:00',5,'',''),
  ('123566789121',5, 1, 2, 3, 3, 5, 1, 2, 3, 4, 500,  '2020-05-01T00:00:00+00:00',1,'',''),
  ('123567789121',1, 1, 3, 4, 4, 1, 2, 3, 4, 4, 600,  '2020-06-01T00:00:00+00:00',7,'',''),
  ('123567889121',3, 1, 5, 1, 5, 2, 3, 4, 5, 4, 700,  '2020-07-01T00:00:00+00:00',4,'',''),
  ('123567899121',2, 1, 4, 5, 1, 3, 4, 5, 1, 4, 800,  '2020-08-01T00:00:00+00:00',6,'',''),
  ('123567890121',4, 1, 1, 2, 2, 4, 5, 1, 2, 4, 900,  '2020-09-01T00:00:00+00:00',1,'',''),
  ('123567890221',5, 1, 2, 3, 3, 5, 1, 2, 3, 4, 1000, '2020-10-01T00:00:00+00:00',1,'',''),
  ('123567890231',1, 1, 3, 4, 4, 1, 2, 3, 4, 4, 1100, '2020-11-01T00:00:00+00:00',2,'',''),
  ('223567890231',3, 1, 5, 1, 5, 2, 3, 4, 5, 4, 1200, '2020-12-01T00:00:00+00:00',2,'','')
;

INSERT INTO Booking (BookingNumber,PatientId, HospitalId, SpecialtyId, DoctorId, WorkingPeriodId, TypeVisitId, ClinicId, PriceCategoryId, CurrencyId, BookingStatusId, Price, VisitingDate,DayNumber,StatusReason,BookingReason)
VALUES 
  ('123456789122',1, 1, 3, 4, 5, 1, 2, 3, 4, 4, 100,  '2021-01-01T00:00:00+00:00',1,'',''),
  ('133456789122',3, 1, 5, 1, 2, 2, 3, 4, 5, 4, 200,  '2021-02-01T00:00:00+00:00',2,'',''),
  ('124456789122',2, 1, 4, 5, 1, 3, 4, 5, 1, 4, 300,  '2021-03-01T00:00:00+00:00',2,'',''),
  ('123556789122',4, 1, 1, 2, 2, 4, 5, 1, 2, 4, 400,  '2021-04-01T00:00:00+00:00',5,'',''),
  ('123566789122',5, 1, 2, 3, 3, 5, 1, 2, 3, 4, 500,  '2021-05-01T00:00:00+00:00',1,'',''),
  ('123567789122',1, 1, 3, 4, 4, 1, 2, 3, 4, 4, 600,  '2021-06-01T00:00:00+00:00',7,'',''),
  ('123567889122',3, 1, 5, 1, 5, 2, 3, 4, 5, 4, 700,  '2021-07-01T00:00:00+00:00',4,'',''),
  ('123567899122',2, 1, 4, 5, 1, 3, 4, 5, 1, 4, 800,  '2021-08-01T00:00:00+00:00',6,'',''),
  ('123567890122',4, 1, 1, 2, 2, 4, 5, 1, 2, 4, 900,  '2021-09-01T00:00:00+00:00',1,'',''),
  ('123567890222',5, 1, 2, 3, 3, 5, 1, 2, 3, 4, 1000, '2021-10-01T00:00:00+00:00',1,'',''),
  ('123567890232',1, 1, 3, 4, 4, 1, 2, 3, 4, 4, 1100, '2021-11-01T00:00:00+00:00',2,'',''),
  ('223567890232',3, 1, 5, 1, 5, 2, 3, 4, 5, 4, 1200, '2021-12-01T00:00:00+00:00',2,'','')
;

INSERT INTO Booking (BookingNumber,PatientId, HospitalId, SpecialtyId, DoctorId, WorkingPeriodId, TypeVisitId, ClinicId, PriceCategoryId, CurrencyId, BookingStatusId, Price, VisitingDate,DayNumber,StatusReason,BookingReason)
VALUES 
  ('123456789124',1, 1, 3, 4, 5, 1, 2, 3, 4, 4, 100,  '2022-01-01T00:00:00+00:00',1,'',''),
  ('133456789124',3, 1, 5, 1, 2, 2, 3, 4, 5, 4, 200,  '2022-02-01T00:00:00+00:00',2,'',''),
  ('124456789124',2, 1, 4, 5, 1, 3, 4, 5, 1, 4, 300,  '2022-03-01T00:00:00+00:00',2,'',''),
  ('123556789124',4, 1, 1, 2, 2, 4, 5, 1, 2, 4, 400,  '2022-04-01T00:00:00+00:00',5,'',''),
  ('123566789124',5, 1, 2, 3, 3, 5, 1, 2, 3, 4, 500,  '2022-05-01T00:00:00+00:00',1,'',''),
  ('123567789124',1, 1, 3, 4, 4, 1, 2, 3, 4, 4, 600,  '2022-06-01T00:00:00+00:00',7,'',''),
  ('123567889124',3, 1, 5, 1, 5, 2, 3, 4, 5, 4, 700,  '2022-07-01T00:00:00+00:00',4,'',''),
  ('123567899124',2, 1, 4, 5, 1, 3, 4, 5, 1, 4, 800,  '2022-08-01T00:00:00+00:00',6,'',''),
  ('123567890124',4, 1, 1, 2, 2, 4, 5, 1, 2, 4, 900,  '2022-09-01T00:00:00+00:00',1,'',''),
  ('123567890224',5, 1, 2, 3, 3, 5, 1, 2, 3, 4, 1000, '2022-10-01T00:00:00+00:00',1,'',''),
  ('123567890234',1, 1, 3, 4, 4, 1, 2, 3, 4, 4, 1100, '2022-11-01T00:00:00+00:00',2,'',''),
  ('223567890234',3, 1, 5, 1, 5, 2, 3, 4, 5, 4, 1200, '2022-12-01T00:00:00+00:00',2,'','')
;

INSERT INTO Booking (BookingNumber,PatientId, HospitalId, SpecialtyId, DoctorId, WorkingPeriodId, TypeVisitId, ClinicId, PriceCategoryId, CurrencyId, BookingStatusId, Price, VisitingDate,DayNumber,StatusReason,BookingReason)
VALUES 
  ('123456789125',1, 1, 3, 4, 5, 1, 2, 3, 4, 4, 100,  '2023-01-01T00:00:00+00:00',1,'',''),
  ('133456789125',3, 1, 5, 1, 2, 2, 3, 4, 5, 4, 200,  '2023-02-01T00:00:00+00:00',2,'',''),
  ('124456789125',2, 1, 4, 5, 1, 3, 4, 5, 1, 4, 300,  '2023-03-01T00:00:00+00:00',2,'',''),
  ('123556789125',4, 1, 1, 2, 2, 4, 5, 1, 2, 4, 400,  '2023-04-01T00:00:00+00:00',5,'',''),
  ('123566789125',5, 1, 2, 3, 3, 5, 1, 2, 3, 4, 500,  '2023-05-01T00:00:00+00:00',1,'',''),
  ('123567789125',1, 1, 3, 4, 4, 1, 2, 3, 4, 4, 600,  '2023-06-01T00:00:00+00:00',7,'',''),
  ('123567889125',3, 1, 5, 1, 5, 2, 3, 4, 5, 4, 700,  '2023-07-01T00:00:00+00:00',4,'',''),
  ('123567899125',2, 1, 4, 5, 1, 3, 4, 5, 1, 4, 800,  '2023-08-01T00:00:00+00:00',6,'','')
;

GO
-- ===========================================================================================================================================================

GO
-- ===========================================================================================================================================================

GO
-- ===========================================================================================================================================================

GO
-- ===========================================================================================================================================================

GO
-- ===========================================================================================================================================================

GO
-- ===========================================================================================================================================================

GO
-- ===========================================================================================================================================================
