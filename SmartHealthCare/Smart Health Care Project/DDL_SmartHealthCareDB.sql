Go
CREATE DATABASE SmartHealthcareDB;
USE SmartHealthcareDB;



CREATE TABLE Roles (
RoleID INT PRIMARY KEY IDENTITY(1,1),    
RoleName VARCHAR(50) UNIQUE NOT NULL
);
--Select Column_Name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='Users' and COLUMN_NAME='RoleID'
--ALTER TABLE Users
--ADD CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleID) REFERENCES Roles(RoleID);
-- Create the Users table
CREATE TABLE Users (    
UserID INT PRIMARY KEY IDENTITY(1,1),    
RoleID INT,    
Name VARCHAR(100) NOT NULL,    
ContactInfo VARCHAR(100),    
Email VARCHAR(100) UNIQUE NOT NULL,    
PasswordHash VARCHAR(255) NOT NULL,    
CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

-- Create the Patients table
CREATE TABLE Patients (    
PatientID INT PRIMARY KEY IDENTITY(1,1),    
UserID INT UNIQUE,    
MedicalHistory TEXT,    
CONSTRAINT FK_Patients_Users FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Create the Doctors table
CREATE TABLE Doctors (    
DoctorID INT PRIMARY KEY IDENTITY(1,1),    
UserID INT UNIQUE,    
Specialization VARCHAR(100),    
Schedule TEXT,    
CONSTRAINT FK_Doctors_Users FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Create the Appointments table
CREATE TABLE Appointments (    
AppointmentID INT PRIMARY KEY IDENTITY(1,1),    
DoctorID INT,    
PatientID INT,    
Date DATE NOT NULL,    
Time TIME NOT NULL,    
Status VARCHAR(20) DEFAULT 'Scheduled',    
CONSTRAINT FK_Appointments_Doctors FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID),
CONSTRAINT FK_Appointments_Patients FOREIGN KEY (PatientID) REFERENCES Patients(PatientID)
);

-- Create the HealthMetrics table
CREATE TABLE HealthMetrics (    
MetricID INT PRIMARY KEY IDENTITY(1,1),    
PatientID INT,    
HeartRate INT,    
BloodPressure VARCHAR(20),    
Temperature DECIMAL(4,1),    
RecordedDate DATE DEFAULT GETDATE(),    
CONSTRAINT FK_HealthMetrics_Patients FOREIGN KEY (PatientID) REFERENCES Patients(PatientID)
);

-- Create the Prescriptions table
CREATE TABLE Prescriptions (    
PrescriptionID INT PRIMARY KEY IDENTITY(1,1),    
DoctorID INT,    
PatientID INT,    
Medication VARCHAR(255),    
Dosage VARCHAR(50),    
Instructions TEXT,    
DateIssued DATE DEFAULT GETDATE(),    
CONSTRAINT FK_Prescriptions_Doctors FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID),
CONSTRAINT FK_Prescriptions_Patients FOREIGN KEY (PatientID) REFERENCES Patients(PatientID)
);

-- Create the Notifications table
CREATE TABLE Notifications (    
NotificationID INT PRIMARY KEY IDENTITY(1,1),    
UserID INT,    Message TEXT,    
NotificationDate DATETIME DEFAULT GETDATE(),
Status VARCHAR(20) DEFAULT 'Pending',
CONSTRAINT FK_Notifications_Users FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Create the Reports table
CREATE TABLE Reports (    
ReportID INT PRIMARY KEY IDENTITY(1,1),    
ReportType VARCHAR(50),    
CreatedBy INT,    
Content TEXT,    
GeneratedDate DATETIME DEFAULT GETDATE(),    
CONSTRAINT FK_Reports_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
); 