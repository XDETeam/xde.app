using Xde.Software.Docker;

namespace Xde.Software.Kafka
{
	public class KafkaBrokerDocker
		: IDockerImage
	{
		/// <inheritdoc />
		string IDockerImage.Id { get; set; }

		/// <inheritdoc />
		string IDockerImage.Tag { get; set; }

		/// <inheritdoc />
		string IDockerImage.Repository { get; set; }
	}
}
