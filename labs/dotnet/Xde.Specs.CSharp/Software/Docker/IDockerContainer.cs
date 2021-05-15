namespace Xde.Software.Docker
{
	/// <summary>
	/// Docker container
	/// </summary>
	/// 
	/// <remarks>
	/// TODO: https://docs.docker.com/get-started/overview/
	/// A container is a runnable instance of an image.
	/// </remarks>
	public interface IDockerImage
	{
		/// <summary>
		/// Container ID
		/// </summary>
		///
		/// <remarks>
		/// TODO:Probably we can reuse some binary DockerId structure here.
		/// </remarks>
		string Id { get; set; }

		// TODO: Can share reusable ImageId that can be implemented by <see cref="IDockerImage"/>
		string ImageId { get; set; }

		string[] Names { get; set; }

		string Command { get; set; }
	}
}
