-- User and Role Management
-- Add a new user

CREATE PROCEDURE AddUser    
@RoleID INT,    
@Name VARCHAR(100),    
@ContactInfo VARCHAR(100),    
@Email VARCHAR(100),    
@PasswordHash VARCHAR(255)
AS BEGIN    
INSERT INTO Users (RoleID, Name, ContactInfo, Email, PasswordHash)    
VALUES (@RoleID, @Name, @ContactInfo, @Email, @PasswordHash);
END;


-- Update existing user details
CREATE PROCEDURE UpdateUser    
@UserID INT,    
@Name VARCHAR(100),    
@ContactInfo VARCHAR(100),    
@Email VARCHAR(100)
AS BEGIN    
UPDATE Users    
SET Name = @Name, ContactInfo = @ContactInfo, Email = @Email    
WHERE UserID = @UserID;
END;

-- Delete a user

CREATE PROCEDURE DeleteUser    
@UserID INT
AS BEGIN    
DELETE FROM Users 
WHERE UserID = @UserID;
END; 

-- Get users by role

Go
CREATE PROCEDURE GetUserByRole    
@RoleID INT
AS BEGIN    
SELECT * FROM Users 
WHERE RoleID = @RoleID;
END;
-- Get users by ID

Go
CREATE PROCEDURE GetUserById   
@UserID INT
AS BEGIN    
SELECT * FROM Users 
WHERE UserID = @UserID;
END;

-- Get users by Email

Go
CREATE PROCEDURE GetUserByEmail  
@Email NVarchar(100)
AS BEGIN    
SELECT * FROM Users 
WHERE Email = @Email;
END;

-- Add a new role 
Go
CREATE PROCEDURE AddRole    
@RoleName VARCHAR(50) 
AS BEGIN    
INSERT INTO Roles (RoleName) VALUES (@RoleName);
END;

-- Get all roles
CREATE PROCEDURE GetAllRoles
AS BEGIN    
SELECT * FROM Roles;
END;


-- Patient Management
-- Add a new patient

CREATE PROCEDURE AddPatient    
@UserID INT,    
@MedicalHistory TEXT 
AS BEGIN    
INSERT INTO Patients (UserID, MedicalHistory) 
VALUES (@UserID, @MedicalHistory);
END;

-- Update patient medical history
CREATE PROCEDURE UpdatePatientMedicalHistory    
@PatientID INT,    
@MedicalHistory TEXT 
AS BEGIN    
UPDATE Patients    
SET MedicalHistory = @MedicalHistory    
WHERE PatientID = @PatientID;
END;

-- Doctor Management
-- Add a new doctor
CREATE PROCEDURE AddDoctor    
@UserID INT,    
@Specialization VARCHAR(100),    
@Schedule TEXT 
AS BEGIN    
INSERT INTO Doctors (UserID, Specialization, Schedule) 
VALUES (@UserID, @Specialization, @Schedule);
END;

-- Update doctor schedule
CREATE PROCEDURE UpdateDoctorSchedule    
@DoctorID INT,    
@Schedule TEXT 
AS BEGIN    
UPDATE Doctors    SET Schedule = @Schedule    
WHERE DoctorID = @DoctorID;
END;

-- Appointment Management
-- Book a new appointment
CREATE PROCEDURE BookAppointment    
@DoctorID INT,    
@PatientID INT,    
@Date DATE,    
@Time TIME 
AS BEGIN    
INSERT INTO Appointments (DoctorID, PatientID, Date, Time, Status)    
VALUES (@DoctorID, @PatientID, @Date, @Time, 'Scheduled');
END;

-- Approve an appointment
CREATE PROCEDURE ApproveAppointment    
@AppointmentID INT
AS BEGIN    
UPDATE Appointments    SET Status = 'Approved'    
WHERE AppointmentID = @AppointmentID;
END;

-- Cancel an appointment 
CREATE PROCEDURE CancelAppointment    
@AppointmentID INT
AS BEGIN    
UPDATE Appointments    SET Status = 'Cancelled'    WHERE AppointmentID = @AppointmentID;
END;

-- Reschedule an appointment
CREATE PROCEDURE RescheduleAppointment    
@AppointmentID INT,    
@NewDate DATE,    
@NewTime TIME 
AS BEGIN    
UPDATE Appointments    SET Date = @NewDate, Time = @NewTime, Status = 'Rescheduled'    
WHERE AppointmentID = @AppointmentID;
END; 

-- Health Metrics Management

-- Add health metrics for a patient
CREATE PROCEDURE AddHealthMetrics    
@PatientID INT,    
@HeartRate INT,    
@BloodPressure VARCHAR(20),    
@Temperature DECIMAL(4,1) 
AS BEGIN    

INSERT INTO HealthMetrics (PatientID, HeartRate, BloodPressure, Temperature, RecordedDate)    
VALUES (@PatientID, @HeartRate, @BloodPressure, @Temperature, GETDATE());
END;

-- Get health metrics for a patient 
CREATE PROCEDURE GetHealthMetricsByPatient    
@PatientID INT
AS BEGIN    
SELECT * FROM HealthMetrics    
WHERE PatientID = @PatientID    
ORDER BY RecordedDate DESC;
END;

-- Prescription Management
-- Create a new prescription 
CREATE PROCEDURE CreatePrescription    
@DoctorID INT,    
@PatientID INT,    
@Medication VARCHAR(255),    
@Dosage VARCHAR(50),    
@Instructions TEXT
AS BEGIN    
INSERT INTO Prescriptions (DoctorID, PatientID, Medication, Dosage, Instructions, DateIssued)    
VALUES (@DoctorID, @PatientID, @Medication, @Dosage, @Instructions, GETDATE());
END;

-- Notification Management
-- Send a notification 
CREATE PROCEDURE SendNotification    
@UserID INT,    
@Message TEXT
AS BEGIN    
INSERT INTO Notifications (UserID, Message, NotificationDate, Status)
VALUES (@UserID, @Message, GETDATE(), 'Pending');
END;


-- Get notifications for a user
CREATE PROCEDURE GetNotificationsForUser    
@UserID INT
AS BEGIN    
SELECT * FROM Notifications    
WHERE UserID = @UserID    
ORDER BY NotificationDate DESC;
END;

-- Report Generation
-- Generate a report
CREATE PROCEDURE GenerateReport    
@ReportType VARCHAR(50),    
@CreatedBy INT,    
@Content TEXT
AS BEGIN    
INSERT INTO Reports (ReportType, CreatedBy, Content, GeneratedDate)
VALUES (@ReportType, @CreatedBy, @Content, GETDATE());
END;

-- Get reports by type
CREATE PROCEDURE GetReportsByType    
@ReportType VARCHAR(50)
AS BEGIN    
SELECT * FROM Reports    
WHERE ReportType = @ReportType    
ORDER BY GeneratedDate DESC;
END; 

-- Get All Appointments
CREATE PROCEDURE GetAllAppointments
AS
BEGIN
    SELECT * FROM Appointments;
END;

--Get All Patients
CREATE PROCEDURE GetAllPatients
AS
BEGIN
    SELECT * FROM Patients;
END;

-- GetPrescriptionsByPatientId
CREATE PROCEDURE GetPrescriptionsByPatientId
    @PatientID INT
AS
BEGIN
    SELECT * FROM Prescriptions WHERE PatientID = @PatientID;
END;

--Get All Doctors
CREATE PROCEDURE GetAllDoctors
AS
BEGIN
    SELECT * FROM Doctors;
END;


--GetHealthMetricsByPatientId
CREATE PROCEDURE GetHealthMetricsByPatientId
    @PatientID INT
AS
BEGIN
    SELECT * FROM HealthMetrics WHERE PatientID = @PatientID;
END;


----GetNotificationsForUser
--CREATE PROCEDURE GetNotificationsForUser
--    @UserID INT
--AS
--BEGIN
--    SELECT * FROM Notifications WHERE UserID = @UserID;
--END;

CREATE PROCEDURE GetAllRoles
AS
BEGIN
    SELECT * FROM Roles;
END;