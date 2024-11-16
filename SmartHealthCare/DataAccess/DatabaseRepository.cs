using Dapper;
using Microsoft.Data.SqlClient;
using SmartHealthCare.Models;
using System.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using SmartHealthCare.Models;

namespace SmartHealthCare.DataAccess
{
    public class DatabaseRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DatabaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SmartHealthcareDB");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        // User and Role Management

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<Role>("GetAllRoles", commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<int> AddUserAsync(User user)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@RoleID", user.RoleID);
                parameters.Add("@Name", user.Name);
                parameters.Add("@ContactInfo", user.ContactInfo);
                parameters.Add("@Email", user.Email);
                parameters.Add("@PasswordHash", user.PasswordHash);

                return await connection.ExecuteAsync("AddUser", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", email);

                return await connection.QuerySingleOrDefaultAsync<User>("GetUserByEmail", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", userId);

                return await connection.QuerySingleOrDefaultAsync<User>("GetUserById", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<int> UpdateUserAsync(User user)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", user.UserID);
                parameters.Add("@Name", user.Name);
                parameters.Add("@ContactInfo", user.ContactInfo);
                parameters.Add("@Email", user.Email);

                return await connection.ExecuteAsync("UpdateUser", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> DeleteUserAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", userId);

                return await connection.ExecuteAsync("DeleteUser", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@RoleID", roleId);

                return await connection.QuerySingleOrDefaultAsync<Role>("GetRoleById", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Patient Management

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<Patient>("GetAllPatients", commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<int> AddPatientAsync(Patient patient)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", patient.UserID);
                parameters.Add("@MedicalHistory", patient.MedicalHistory);

                return await connection.ExecuteAsync("AddPatient", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> UpdatePatientMedicalHistoryAsync(int patientId, string medicalHistory)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PatientID", patientId);
                parameters.Add("@MedicalHistory", medicalHistory);

                return await connection.ExecuteAsync("UpdatePatientMedicalHistory", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Doctor Management

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<Doctor>("GetAllDoctors", commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<int> AddDoctorAsync(Doctor doctor)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", doctor.UserID);
                parameters.Add("@Specialization", doctor.Specialization);
                parameters.Add("@Schedule", doctor.Schedule);

                return await connection.ExecuteAsync("AddDoctor", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> UpdateDoctorScheduleAsync(int doctorId, string schedule)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DoctorID", doctorId);
                parameters.Add("@Schedule", schedule);

                return await connection.ExecuteAsync("UpdateDoctorSchedule", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Appointment Management

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<Appointment>("GetAllAppointments", commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<int> BookAppointmentAsync(Appointment appointment)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DoctorID", appointment.DoctorID);
                parameters.Add("@PatientID", appointment.PatientID);
                parameters.Add("@Date", appointment.Date);
                parameters.Add("@Time", appointment.Time);

                return await connection.ExecuteAsync("BookAppointment", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> ApproveAppointmentAsync(int appointmentId)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AppointmentID", appointmentId);

                return await connection.ExecuteAsync("ApproveAppointment", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> CancelAppointmentAsync(int appointmentId)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AppointmentID", appointmentId);

                return await connection.ExecuteAsync("CancelAppointment", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Health Metrics Management

        public async Task<IEnumerable<HealthMetrics>> GetHealthMetricsByPatientIdAsync(int patientId)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PatientID", patientId);

                return await connection.QueryAsync<HealthMetrics>("GetHealthMetricsByPatientId", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<int> AddHealthMetricsAsync(HealthMetrics metrics)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PatientID", metrics.PatientID);
                parameters.Add("@HeartRate", metrics.HeartRate);
                parameters.Add("@BloodPressure", metrics.BloodPressure);
                parameters.Add("@Temperature", metrics.Temperature);

                return await connection.ExecuteAsync("AddHealthMetrics", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Prescription Management

        public async Task<IEnumerable<Prescription>> GetPrescriptionsByPatientIdAsync(int patientId)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PatientID", patientId);

                return await connection.QueryAsync<Prescription>("GetPrescriptionsByPatientId", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<int> CreatePrescriptionAsync(Prescription prescription)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DoctorID", prescription.DoctorID);
                parameters.Add("@PatientID", prescription.PatientID);
                parameters.Add("@Medication", prescription.Medication);
                parameters.Add("@Dosage", prescription.Dosage);
                parameters.Add("@Instructions", prescription.Instructions);

                return await connection.ExecuteAsync("CreatePrescription", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Notification Management

        public async Task<IEnumerable<Notification>> GetNotificationsForUserAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", userId);

                return await connection.QueryAsync<Notification>("GetNotificationsForUser", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<int> SendNotificationAsync(Notification notification)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", notification.UserID);
                parameters.Add("@Message", notification.Message);

                return await connection.ExecuteAsync("SendNotification", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Report Generation

        public async Task<int> GenerateReportAsync(Report report)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ReportType", report.ReportType);
                parameters.Add("@CreatedBy", report.CreatedBy);
                parameters.Add("@Content", report.Content);

                return await connection.ExecuteAsync("GenerateReport", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}