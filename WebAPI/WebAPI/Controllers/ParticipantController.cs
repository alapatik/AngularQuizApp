using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class ParticipantController : ApiController
    {
        [HttpPost]
        [Route("api/InsertParticipant")]
        public Participant Insert(Participant participant)
        {
            using (DBModel model = new DBModel())
            {
                model.Participants.Add(participant);
                model.SaveChanges();
            }
            return participant;
        }

        [HttpPut]
        [Route("api/UpdateParticipant")]
        public void UpdateParticipant(Participant participant)
        {
            using (DBModel model = new DBModel())
            {
                model.Entry(model).State = System.Data.Entity.EntityState.Modified;
                model.SaveChanges();
            }
        }
    }
}
