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
    public class DoctorController : ControllerBase
    {
        IClinicDoctorLogic logic;
        IHubContext<SignalRHub> hub;

        public DoctorController(IClinicDoctorLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        // GET: api/<DoctorController>
        [HttpGet]
        public IEnumerable<Doctor> Get()
        {
            return this.logic.GetAllDoctors();
        }

        // GET api/<DoctorController>/5
        [HttpGet("{id}")]
        public Doctor Get(int id)
        {
            return this.logic.GetOneDoctor(id);
        }

        // POST api/<DoctorController>
        [HttpPost]
        public void Post([FromBody] Doctor value)
        {
            this.logic.AddOneDoctor(value);
            this.hub.Clients.All.SendAsync("DoctorCreated", value);
        }

        // PUT api/<DoctorController>/5
        [HttpPut]
        public void Put([FromBody] Doctor value)
        {
            this.logic.ChangeOneDoctorDegree(value);
            this.hub.Clients.All.SendAsync("DoctorUpdated", value);
        }

        // DELETE api/<DoctorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var DoctorToDelete = this.logic.GetOneDoctor(id);
            this.logic.RemoveOneDoctor(id);
            this.hub.Clients.All.SendAsync("DoctorDeleted", DoctorToDelete);
        }
    }
}
