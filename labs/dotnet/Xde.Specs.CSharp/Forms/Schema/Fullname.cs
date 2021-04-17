namespace Xde.Forms.Schema
{
	/// <summary>
	/// Full name
	/// </summary>
	/// 
	/// <remarks>
	/// TODO:Simple basics that defines name that can be uniquely distinguished from the
	/// other one.
	/// </remarks>
	public class Fullname
	{
		/// <summary>
		/// Short name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Namespace
		/// </summary>
		public string Namespace { get; set; }

		/// <summary>
		/// Layer
		/// </summary>
		/// <remarks>
		/// TODO: Experiment with introducing layers as an additional axis of naming.
		/// For example, .NET class can have name (<see cref="Name"/>), namespace
		/// <see cref="Namespace"/> and assembly that is a very good candidate to be
		/// a layer.
		/// </remarks>
		public string Layer { get; set; }
	}
}
