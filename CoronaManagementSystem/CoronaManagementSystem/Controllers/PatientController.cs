using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CoronaManagementSystemHMO.Controllers
{
    [Route("api/[controller]")]
    public class PatientController : Controller
    {

        [HttpPost("Register")]
        public IActionResult RegisterPatient([FromBody] Patient patient)
        {
            try
            {
                string query = "insert into [dbo].[patients] (ID,FirstName,LastName,IdentityCard,City,Street,Number,DteOfBirth,Telephone,AlternateTelephone)" +
                    "values(@ID,@FirstName,@LastName,@IdentityCard,@City,@Street,@Number,@DteOfBirth,@Telephone,@AlternateTelephone)";

                List<SqlParameter> parameters = new List<SqlParameter>(){
                    new SqlParameter() { ParameterName = "ID", Value = Guid.NewGuid().ToString()},
                    new SqlParameter() { ParameterName = "FirstName", Value = patient.FirstName },
                    new SqlParameter() { ParameterName = "LastName", Value = patient.LastName },
                    new SqlParameter() { ParameterName = "IdentityCard", Value = patient.IdentityCard },
                    new SqlParameter() { ParameterName = "City", Value = patient.City },
                    new SqlParameter() { ParameterName = "Street", Value = patient.Street },
                    new SqlParameter() { ParameterName = "Number", Value = patient.Number },
                    new SqlParameter() { ParameterName = "DteOfBirth", Value = patient.DteOfBirth },
                    new SqlParameter() { ParameterName = "Telephone", Value = patient.Telephone },
                    new SqlParameter() { ParameterName = "AlternateTelephone", Value = patient.AlternateTelephone },
                };

                SqlEngine.ExecuteNonQuery(query, parameters);
                return Content("Success");
            }
            catch (Exception ex)
            {
                return Content("Failure in saving the patient info to DB");
            }
        }

        [HttpGet("Get")]
        public IActionResult GetPatientList([FromQuery] string patientId)
        {
            try
            {
                var query = "select ID,FirstName,LastName,IdentityCard,City,Street,Number,DteOfBirth,Telephone,AlternateTelephone" +
                    " from [dbo].[patients] where ID = @id or @id = ''";
                List<SqlParameter> parameters = new List<SqlParameter>(){
                    new SqlParameter() { ParameterName = "ID", Value =string.IsNullOrEmpty(patientId)?"":patientId},
                };

                var datatable = SqlEngine.GetDataTable(query, parameters);

                var patients = new List<Patient>();
                foreach (DataRow row in datatable.Rows)
                {
                    var patient = new Patient();
                    patient.ID = row["ID"].ToString();
                    patient.FirstName = row["FirstName"].ToString();
                    patient.LastName = row["LastName"].ToString();
                    patient.IdentityCard = row["IdentityCard"].ToString();
                    patient.City = row["City"].ToString();
                    patient.Street = row["Street"].ToString();
                    patient.Number = row["Number"].ToString();
                    patient.DteOfBirth = (DateTime)row["DteOfBirth"];
                    patient.Telephone = row["DteOfBirth"].ToString();
                    patient.AlternateTelephone = row["DteOfBirth"].ToString();
                    patients.Add(patient);
                }

                // here we write the code to save the patient info to the DB
                return Content(Newtonsoft.Json.JsonConvert.SerializeObject(patients));
            }
            catch (Exception ex)
            {
                return Content("Failure in returning the patient info");
            }
        }

        [HttpPost("VaccinationInfo")]
        public IActionResult RegisterPatient([FromBody] PatientVaccinationInfo patientVaccinationInfo)
        {
            try
            {
                string query = "insert into [dbo].[PatientVaccineInfo] (PatientID,VaccinatedDate,VaccineManufacturer)" +
                    "values(@PatientID,@VaccinatedDate,@VaccineManufacturer)";

                List<SqlParameter> parameters = new List<SqlParameter>(){
                    new SqlParameter() { ParameterName = "PatientID", Value = patientVaccinationInfo.PatientId},
                    new SqlParameter() { ParameterName = "VaccinatedDate", Value = patientVaccinationInfo.VaccinatedDate },
                    new SqlParameter() { ParameterName = "VaccineManufacturer", Value = patientVaccinationInfo.VaccineManufacturer },
                };

                var res = SqlEngine.ExecuteNonQuery(query, parameters);
                return Content("Success");
            }
            catch (Exception ex)
            {
                return Content("Failure in saving the patient info to DB");
            }
        }



        [HttpGet("VaccinationInfo")]
        public IActionResult GetVaccinationInfo([FromQuery] string patientId)
        {
            try
            {
                if (string.IsNullOrEmpty(patientId))
                    throw new Exception("send the patient id");


                var query = "select [PatientID],[VaccienatedDate],[VaccineManufacturer] " +
                    " from [dbo].[PatientVaccineInfo] where [PatientID]= @id ";
                List<SqlParameter> parameters = new List<SqlParameter>(){
                    new SqlParameter() { ParameterName = "ID", Value =string.IsNullOrEmpty(patientId)?"":patientId},
                };

                var datatable = SqlEngine.GetDataTable(query, parameters);

                var patientVaccinationInfolist = new List<PatientVaccinationInfo>();
                foreach (DataRow row in datatable.Rows)
                {
                    var patientVaccinationInfo = new PatientVaccinationInfo();
                    patientVaccinationInfo.PatientId = row["PatientID"].ToString();
                    patientVaccinationInfo.VaccinatedDate = (DateTime)row["VaccinatedDate"];
                    patientVaccinationInfo.VaccineManufacturer = row["VaccineManufacturer"].ToString();
                    patientVaccinationInfolist.Add(patientVaccinationInfo);
                }
                // here we write the code to save the patient info to the DB
                return Content(Newtonsoft.Json.JsonConvert.SerializeObject(patientVaccinationInfolist));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }


        [HttpPost("InfectedInfo")]
        public IActionResult PostPatientInfectedInfo([FromBody] PatientInfectedInfo patientInfectedInfo)
        {
            try
            {
                string query = "insert into [dbo].[PatientInfectedInfo] (PatientID,InfectedDate,RecoveredDate)" +
                    "values(@PatientID,@InfectedDate,@RecoveredDate)";

                List<SqlParameter> parameters = new List<SqlParameter>(){
                    new SqlParameter() { ParameterName = "PatientID", Value = patientInfectedInfo.PatientId},
                    new SqlParameter() { ParameterName = "InfectedDate", Value = patientInfectedInfo.InfectedDate },
                    new SqlParameter() { ParameterName = "RecoveredDate", Value = patientInfectedInfo.RecoveredDate},
                };

                var res = SqlEngine.ExecuteNonQuery(query, parameters);
                return Content("Success");
            }
            catch (Exception ex)
            {
                return Content("Failure in saving the patient info to DB");
            }
        }



        [HttpGet("InfectedInfo")]
        public IActionResult GetPatientInfectedInfo([FromQuery] string patientId)
        {
            try
            {
                if (string.IsNullOrEmpty(patientId))
                    throw new Exception("send the patient id");

                var query = "select [PatientID],[InfectedDate],[RecoveredDate] " +
                    " from [dbo].[PatientInfectedInfo] where [PatientID]= @id ";
                List<SqlParameter> parameters = new List<SqlParameter>(){
                    new SqlParameter() { ParameterName = "ID", Value =string.IsNullOrEmpty(patientId)?"":patientId},
                };

                var datatable = SqlEngine.GetDataTable(query, parameters);

                var patientInfectedInfoList = new List<PatientInfectedInfo>();
                foreach (DataRow row in datatable.Rows)
                {
                    var patientVaccinationInfo = new PatientInfectedInfo();
                    patientVaccinationInfo.PatientId = row["PatientID"].ToString();
                    patientVaccinationInfo.InfectedDate = (DateTime)row["InfectedDate"];
                    patientVaccinationInfo.RecoveredDate = (DateTime)row["RecoveredDate"];
                    patientInfectedInfoList.Add(patientVaccinationInfo);
                }
                // here we write the code to save the patient info to the DB
                return Content(Newtonsoft.Json.JsonConvert.SerializeObject(patientInfectedInfoList));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }


    public class Patient
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityCard { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public System.DateTime DteOfBirth { get; set; }
        public string Telephone { get; set; }
        public string AlternateTelephone { get; set; }
    }


    public class PatientVaccinationInfo
    {
        public string PatientId { get; set; }
        public DateTime VaccinatedDate { get; set; }
        public string VaccineManufacturer { get; set; }
    }

    public class PatientInfectedInfo
    {
        public string PatientId { get; set; }
        public DateTime InfectedDate { get; set; }
        public DateTime RecoveredDate { get; set; }
    }
}