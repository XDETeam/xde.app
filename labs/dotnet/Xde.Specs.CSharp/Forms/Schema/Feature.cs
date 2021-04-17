namespace Xde.Forms.Schema
{
	/// <summary>
	/// Feature
	/// </summary>
	/// 
	/// <remarks>
	/// TODO:Temporary can interpret as a property of the <see cref="Form"/> that is
	/// close to type.
	/// </remarks>
	public class Feature
	{
		/// <summary>
		/// Feature name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Feature <see cref="Form">form</see>
		/// </summary>
		public Form Form { get; set; }
	}
}
