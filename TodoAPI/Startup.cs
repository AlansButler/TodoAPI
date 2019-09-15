using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;


// https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-2.2&tabs=visual-studio#call-the-web-api-with-javascript-1


namespace TodoAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			//Next 2 lines: Configure the app to serve static files and enable default file mapping by updating Startup.cs with the following highlighted code:
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
