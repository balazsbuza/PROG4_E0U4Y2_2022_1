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
    [Route("[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        IPatientTreatmentLogic logic;
        IHubContext<SignalRHub> hub;

        public PatientController(IPatientTreatmentLogic logic)
        {
            this.logic = logic;
            this.hub = hub;
        }

        // GET: api/<PatientController>
        [HttpGet]
        public IEnumerable<Patient> Get()
        {
            return this.logic.GetAllPatients();
        }

        // GET api/<PatientController>/5
        [HttpGet("{id}")]
        public Patient Get(int id)
        {
            return this.logic.GetOnePatient(id);
        }

        // POST api/<PatientController>
        [HttpPost]
        public void Post([FromBody] Patient value)
        {
            this.logic.AddOnePatient(value);
            this.hub.Clients.All.SendAsync("PatientCreated", value);
        }

        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            this.logic.ChangeOnePatientDisease(id, value);
            this.hub.Clients.All.SendAsync("PatientUpdated", value);
        }

        // DELETE api/<PatientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var PatientToDelete = this.logic.GetOnePatient(id);
            this.logic.RemoveOnePatient(id);
            this.hub.Clients.All.SendAsync("PatientDeleted", PatientToDelete);
        }
    }
}
