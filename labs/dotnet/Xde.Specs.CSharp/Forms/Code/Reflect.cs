using System;

namespace Xde.Forms.Code
{
	/// TODO:Helper tool for reflection routines
	public class Reflect
	{
		/// <summary>
		/// Check if <paramref name="type"/> can be created
		/// </summary>
		/// 
		/// <remarks>
		/// Similar as "where T : new()" condition.
		/// </remarks>
		public static bool CanNew(Type type)
			=> !type.IsAbstract
			&& !type.IsGenericTypeDefinition
			&& type.GetConstructor(Type.EmptyTypes) != null
		;

		/// <summary>
		/// Check if type <typeparamref name="T"/> can be created
		/// </summary>
		/// 
		/// <remarks>
		/// Using <see cref="CanNew(Type)"/>.
		/// </remarks>
		public static bool CanNew<T>() => CanNew(typeof(T));
	}
}
