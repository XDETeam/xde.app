using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Xde.App
{
	public class WebDavMiddleware
	{
		private readonly RequestDelegate _next;

		public WebDavMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			await _next.Invoke(context);
		}
	}

	public class WebDavStartup
	{
		public WebDavStartup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			//TODO:
			//app.UseHttpsRedirection();
			//app.UseRouting();
			//app.UseAuthorization();
			//app.UseEndpoints(endpoints =>
			//{
			//	endpoints.MapControllers();
			//});

			//TODO:
			//app.UseEndpoints(routes => routes.Map())

			app.UseMiddleware<WebDavMiddleware>();
		}
	}
}
