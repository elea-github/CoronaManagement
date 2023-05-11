Go
Drop table if exists patient 
GO
Create table patients (
ID varchar(100) primary key,
FirstName nvarchar(20),
LastName nvarchar(20),
IdentityCard nvarchar(10),
City  nvarchar(20),
Street nvarchar(20),
Number nvarchar(20),
DteOfBirth smalldatetime,
Telephone  varchar(10),
AlternateTelephone varchar(10)
)
Go
drop table if exists PatientVaccineInfo 
Go
create table PatientVaccineInfo (
PatientID varchar(100),
VaccinatedDate smalldatetime,
VaccineManufacturer nvarchar(20)
 FOREIGN KEY (PatientID) REFERENCES patients(ID)
)
Go
drop table if exists PatientInfectedInfo 
Go

create table PatientInfectedInfo (
PatientID varchar(100),
InfectedDate smalldatetime,
RecoveredDate smalldatetime
FOREIGN KEY (PatientID) REFERENCES patients(ID)
)

 


