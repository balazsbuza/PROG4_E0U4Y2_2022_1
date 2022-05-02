using Hospital.Data.Tables;
using Hospital.Logic;
using Microsoft.AspNetCore.Mvc;
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

        public ClinicController(IClinicDoctorLogic logic)
        {
            this.logic = logic;
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
            //this.hub.Clients.All.SendAsync("ActorCreated", value);
        }

        // PUT api/<ClinicController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            this.logic.ChangeOneClinicAddress(id, value);
        }

        // DELETE api/<ClinicController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.RemoveOneClinic(id);
        }
    }
}
