using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HackerNews.Service;

namespace HackerNew.WebAPI.Controllers
{   [RoutePrefix("api/Story")]
    public class StoryController : ApiController
    {
        IHackerNewsRestService _service;

        public StoryController(IHackerNewsRestService service)
        {
            _service = service;
        }

        [Route("beststories")]
        public IHttpActionResult getBestStories()
        {
            return Ok(_service.getBestStories());
        }
      

    }
}
