namespace Xde.App
{
	/// <summary>
	/// TODO:
	/// </summary>
	/// 
	/// <remarks>
	/// RFC4918 5.2 Collection Resources
	/// 
	/// Collection resources differ from other resources in that they also act as
	/// containers.
	/// 
	/// A collection's state consists of at least a set of mappings between path
	/// segments and resources, and a set of properties on the collection itself.
	/// </remarks>
	public interface IWebDavCollection
		: IWebDavResource
	{

	}
}
