using System.Collections.Generic;

namespace Xde.Forms.Schema
{
	/// <summary>
	/// Basic form
	/// </summary>
	/// 
	/// <remarks>
	/// TODO:Some very basic objects schema to start steps from. This can be temporary
	/// interpreted as a type.
	/// </remarks>
	public class Form
	{
		/// <summary>
		/// Form <see cref="Fullname">name</see>
		/// </summary>
		public Fullname Fullname { get; set; }

		/// <summary>
		/// Form <see cref="Aspect">aspects</see>
		/// </summary>
		public IEnumerable<Aspect> Aspects { get; set; }
	}
}
