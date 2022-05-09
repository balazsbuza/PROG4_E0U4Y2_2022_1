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
    public class TreatmentController : ControllerBase
    {
        IPatientTreatmentLogic logic;
        IHubContext<SignalRHub> hub;

        public TreatmentController(IPatientTreatmentLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        // GET: api/<TreatmentController>
        [HttpGet]
        public IEnumerable<Treatment> Get()
        {
            return this.logic.GetAllTreatments();
        }

        // GET api/<TreatmentController>/5
        [HttpGet("{id}")]
        public Treatment Get(int id)
        {
            return this.logic.GetOneTreatment(id);
        }

        // POST api/<TreatmentController>
        [HttpPost]
        public void Post([FromBody] Treatment value)
        {
            this.logic.AddOneTreatment(value);
            this.hub.Clients.All.SendAsync("TreatmentCreated", value);
        }

        // PUT api/<TreatmentController>/5
        [HttpPut]
        public void Put([FromBody] Treatment value)
        {
            this.logic.ChangeOneTreatmentDescription(value);
            this.hub.Clients.All.SendAsync("TreatmentUpdated", value);
        }

        // DELETE api/<TreatmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var TreatmentToDelete = this.logic.GetOneTreatment(id);
            this.logic.RemoveOneTreatment(id);
            this.hub.Clients.All.SendAsync("TreatmentDeleted", TreatmentToDelete);
        }
    }
}
