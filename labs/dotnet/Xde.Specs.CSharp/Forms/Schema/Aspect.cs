namespace Xde.Forms.Schema
{
	/// <summary>
	/// Aspect
	/// </summary>
	/// 
	/// <remarks>
	/// TODO:Temporary can interpret as a property of the <see cref="Form"/> (that itself
	/// is close to type).
	/// </remarks>
	public class Aspect
	{
		/// <summary>
		/// Aspect name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Aspect <see cref="Form">form</see>
		/// </summary>
		public Form Form { get; set; }
	}
}
