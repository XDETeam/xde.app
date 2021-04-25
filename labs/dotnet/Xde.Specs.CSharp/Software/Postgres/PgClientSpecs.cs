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
		public const string PgUser = "postgres";
		public const string PgPassword = "!qa2Ws3eD";

		//TODO:
		[Fact]
		public async void Temp()
		{
			using var docker = new DockerClientConfiguration()
				.CreateClient()
			;

			await docker.Images.CreateImageAsync(
				new ImagesCreateParameters
				{
					FromImage = ImageName,
					Tag = ImageTag
				},
				new AuthConfig(),
				new Progress<JSONMessage>()
			);

			var container = await docker.Containers.CreateContainerAsync(new CreateContainerParameters()
			{
				Name = "pg_temp",
				Image = $"{ImageName}:{ImageTag}",
				Env = new List<string>()
				{
					$"POSTGRES_USER={PgUser}",
					$"POSTGRES_PASSWORD={PgPassword}"
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
			});

			try
			{
				await docker.Containers.StartContainerAsync(
					container.ID,
					new ContainerStartParameters()
				);

				var client = new PgClient("127.0.0.1", HostPort, PgUser, PgPassword);
				client.Connect();
			}
			finally
			{
				await docker.Containers.RemoveContainerAsync(
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
}
