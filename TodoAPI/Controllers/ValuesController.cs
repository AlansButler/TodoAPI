using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Controllers
{
	//You would derive from Controller not ControllerBase if building a web page.  Controller descends from Controllerbase and adds support for views.
	[Route("api/[controller]")]
	[ApiController]  //This attribute indicates that the controller responds to web API requests. 
	public class ValuesAlController : ControllerBase
	{
		// GET api/valuesAl
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/valuesAl/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		// POST api/valuesAl
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/valuesAl/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/valuesAl/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
