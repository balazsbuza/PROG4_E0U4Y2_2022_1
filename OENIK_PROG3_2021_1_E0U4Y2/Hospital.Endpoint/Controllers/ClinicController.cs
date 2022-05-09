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
    public class ClinicController : ControllerBase
    {
        IClinicDoctorLogic logic;
        IHubContext<SignalRHub> hub;

        public ClinicController(IClinicDoctorLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        // GET: api/<ClinicController>
        [HttpGet]
        public IEnumerable<Clinic> Get()
        {
            return this.logic.GetAllClinics();
        }

        // GET api/<ClinicController>/5
        [HttpGet("{id}")]
        public Clinic Get(int id)
        {
            return this.logic.GetOneClinic(id);
        }

        // POST api/<ClinicController>
        [HttpPost]
        public void Post([FromBody] Clinic value)
        {
            this.logic.AddOneClinic(value);
            this.hub.Clients.All.SendAsync("ClinicCreated", value);
        }

        // PUT api/<ClinicController>/5
        [HttpPut]
        public void Put([FromBody] Clinic value)
        {
            this.logic.ChangeOneClinicAddress(value);
            this.hub.Clients.All.SendAsync("ClinicUpdated", value);
        }

        // DELETE api/<ClinicController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var clinicToDelete = this.logic.GetOneClinic(id);
            this.logic.RemoveOneClinic(id);
            this.hub.Clients.All.SendAsync("ClinicDeleted", clinicToDelete);
        }
    }
}
