using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Xde.App
{
	//TODO:http://www.webdav.org/specs/rfc4918.pdf
	public class WebDavMiddleware
	{
		private readonly RequestDelegate _next;

		public WebDavMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			var request = context.Request;
			request.Headers.TryGetValue("Depth", out var depth);

			var response = context.Response;
			response.StatusCode = 200;
			response.ContentType = "application/xml; charset=\"utf-8\"";

			var ns = XNamespace.Get("DAV:");

			//var xml = new XDocument(
			//	new XDeclaration(version: "1.0", encoding: "utf-8", standalone: "no"),
			//	new XElement(ns.GetName("propfind"))
			//);

			var xml = new XDocument(
				new XDeclaration(version: "1.0", encoding: "utf-8", standalone: "no"),
				new XElement(
					ns.GetName("multistatus"),
					new XElement(
						ns.GetName("response"),
						new XElement(
							ns.GetName("href"),
							//"http://localhost:5000/test1.html"
							"/folder1/"
						),
						new XElement(
							ns.GetName("propstat"),
							new XElement(
								ns.GetName("prop"),
								new XElement(
									ns.GetName("displayname"),
									"Example collection"
								),
								new XElement(
									ns.GetName("resourcetype"),
									new XElement(ns.GetName("collection"))
								),
								new XElement(
									ns.GetName("creationdate"),
									"1997-12-01T17:42:21-08:00"
								),
								new XElement(
									ns.GetName("getcontentlength"),
									4568
								),
								new XElement(
									ns.GetName("getcontenttype"),
									"text/html"
								),
								new XElement(
									ns.GetName("getetag"),
									"tag1,tag2"
								),
								new XElement(
									ns.GetName("getlastmodified"),
									"Mon, 12 Jan 1998 09:25:56 GMT"
								)
							)
						),
						new XElement(
							ns.GetName("status"),
							"HTTP/1.1 200 OK"
						)
					),

					new XElement(
						ns.GetName("response"),
						new XElement(
							ns.GetName("href"),
							"/file1.txt"
						),
						new XElement(
							ns.GetName("propstat"),
							new XElement(
								ns.GetName("prop"),
								new XElement(
									ns.GetName("displayname"),
									"Example file"
								),
								new XElement(
									ns.GetName("resourcetype")
									//new XElement(ns.GetName("collection"))
								),
								new XElement(
									ns.GetName("creationdate"),
									"1997-12-01T17:42:21-08:00"
								),
								new XElement(
									ns.GetName("getcontentlength"),
									4568
								),
								new XElement(
									ns.GetName("getcontenttype"),
									"text/html"
								),
								new XElement(
									ns.GetName("getetag"),
									"tag1,tag2"
								),
								new XElement(
									ns.GetName("getlastmodified"),
									"Mon, 12 Jan 1998 09:25:56 GMT"
								)
							)
						),
						new XElement(
							ns.GetName("status"),
							"HTTP/1.1 200 OK"
						)
					)
				)
			);;

			await response.WriteAsync(xml.ToString());

			//await _next.Invoke(context);
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
			//app.UseEndpoints(routes => routes
			//	.MapMethods(
			//);

			app.UseMiddleware<WebDavMiddleware>();
		}
	}
}
