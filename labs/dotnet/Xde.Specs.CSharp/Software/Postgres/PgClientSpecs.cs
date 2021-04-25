using System;
using System.Collections.Generic;
using Docker.DotNet;
using Docker.DotNet.Models;
using Xunit;

namespace Xde.Software.Postgres
{
	public class PgClientSpecs
	{
		public const string ImageName = "postgres";
		public const string ImageTag = "13.3";
		public const int HostPort = 54320;

		//TODO:
		[Fact]
		public async void Temp()
		{
			using var client = new DockerClientConfiguration()
				.CreateClient()
			;

			await client.Images.CreateImageAsync(
				new ImagesCreateParameters
				{
					FromImage = ImageName,
					Tag = ImageTag
				},
				new AuthConfig(),
				new Progress<JSONMessage>()
			);

			var specs = new CreateContainerParameters()
			{
				Name = "pg_temp",
				Image = $"{ImageName}:{ImageTag}",
				Env = new List<string>()
				{
					"POSTGRES_PASSWORD=!qa2Ws3eD",
					"POSTGRES_USER=postgres"
				},
				ExposedPorts = new Dictionary<string, EmptyStruct>()
				{
					["5432"] = new EmptyStruct()
				},
				HostConfig = new HostConfig()
				{
					PortBindings = new Dictionary<string, IList<PortBinding>>()
					{
						["5432"] = new List<PortBinding>
						{
							new PortBinding
							{
								HostIP = "0.0.0.0",
								HostPort = $"{HostPort}"
							}
						}
					}
				}
			};

			var container = await client.Containers.CreateContainerAsync(specs);

			await client.Containers.RemoveContainerAsync(
				container.ID,
				new ContainerRemoveParameters
				{
					Force = true,
					RemoveVolumes = true
				}
			);
		}
	}
}
