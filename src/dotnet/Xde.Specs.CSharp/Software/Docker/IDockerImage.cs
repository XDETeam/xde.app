namespace Xde.Software.Docker
{
	/// <summary>
	/// Docker image
	/// </summary>
	/// 
	/// <remarks>
	/// TODO: https://docs.docker.com/get-started/overview/
	/// An image is a read-only template with instructions for creating a Docker container.
	/// Often, an image is based on another image, with some additional customization.
	/// </remarks>
	public interface IDockerImage
	{
		/// <summary>
		/// Image ID
		/// </summary>
		///
		/// <remarks>
		/// TODO:Probably we can reuse some binary DockerId structure here.
		/// </remarks>
		string Id { get; set; }

		string Tag { get; set; }

		string Repository { get; set; }

		//TODO: string[] Layers { get; set; }
	}
}
