namespace Xde.Mesh
{
	/// <summary>
	/// Preliminary idea of the mesh resource item.
	/// </summary>
	public class Node
	{
		/// <summary>
		/// Internal, system ID.
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Node URL (Unique Resource Locator).
		/// </summary>
		/// 
		/// <remarks>
		/// <para>
		///		Probably this should be an URL, not URI, because we will address mostly real
		///		resources, not abstractions like urn:isbn:0-486-27557-4. But who knows...
		/// </para>
		/// <para>
		///		Also the can be a relative path inside node defined as <see cref="Owner"/>.
		///		So absolute URL will be Owner.Url + Node.Url. This is another reason, why using
		///		URIs should be tricky.
		/// </para>
		/// </remarks>
		public string Url { get; set; }

		/// <summary>
		/// Node owner.
		/// </summary>
		public int Owner { get; }
	}
}
