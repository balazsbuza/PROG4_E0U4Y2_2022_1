using Hospital.Data.Tables;
using Hospital.Endpoint.Services;
using Hospital.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hospital.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IClinicDoctorLogic cdlogic;
        IPatientTreatmentLogic ptlogic;
        IHospitalLogicBase hlogic;

        public StatController(IClinicDoctorLogic cdlogic, IPatientTreatmentLogic ptlogic, IHospitalLogicBase hlogic)
        {
            this.cdlogic = cdlogic;
            this.ptlogic = ptlogic;
            this.hlogic = hlogic;
        }

        [HttpGet]
        public IList<DoctorWorkaddress> DoctorWorkAddress()
        {
            return this.cdlogic.GetDoctorWorkAddress();
        }

        [HttpGet]
        public IList<PatientLastTreatment> PatientLastTreatment()
        {
            return this.ptlogic.GetPatientLastTreatment();
        }

        [HttpGet]
        public ICollection<ClinicGender> ClinicGender()
        {
            return this.hlogic.GetClinicGender();
        }

        [HttpGet]
        public IList<DoctorOfficeHours> DoctorOfficeHours()
        {
            return this.cdlogic.GetDoctorOfficeHours();
        }

        [HttpGet]
        public IList<PatientTreatmentLastYear> PatientTreatmentLastYear()
        {
            return this.ptlogic.GetPatientTreatmentLastYear();
        }
    }
}
