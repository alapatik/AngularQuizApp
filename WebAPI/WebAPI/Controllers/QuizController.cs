using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class QuizController : ApiController
    {
        [HttpGet]
        [Route("api/GetQuestions")]
        public HttpResponseMessage Get()
        {
            using (DBModel model = new DBModel())
            {
                var questions = model.Questions
                        .Select(q => new { QnID = q.QnID, Qn = q.Qn, Image = q.Image, q.Option1, q.Option2, q.Option3, q.Option4 })
                        .OrderBy(y => Guid.NewGuid())
                        .Take(10)
                        .ToArray();

                var updated = questions.AsEnumerable()
                        .Select(x => 
                        new {
                            QnID = x.QnID,
                            Qn = x.Qn,
                            Image = x.Image,
                            Options = new string[] {x.Option1, x.Option2, x.Option3, x.Option4}
                        }).ToList();
                return this.Request.CreateResponse(HttpStatusCode.OK, updated);
            }
        }

        [HttpPost]
        [Route("api/GetAnswers")]
        public HttpResponseMessage GetAnswers(int[] QnIds)
        {
            using (DBModel model = new DBModel())
            {
                var qnAnswers = model.Questions.AsEnumerable()
                        .Where(q => QnIds.Contains(q.QnID))
                        .OrderBy(x => { return Array.IndexOf(QnIds, x.QnID); })
                        .Select(z => z.Answer)
                        .ToArray();
                return Request.CreateResponse(HttpStatusCode.OK, qnAnswers);
            }
        }


    }
}
