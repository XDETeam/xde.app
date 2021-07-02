using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Xde.Forms.Code
{
	/// TODO:Helper tool for reflection routines
	public class Reflect
	{
		private static readonly Reflect Current = new();

		public interface IFilter
			: IEnumerable<Type>
		{

		}

		private class Filter
			: IFilter
		{
			public Filter(Assembly assembly)
			{

			}

			IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			IEnumerator IEnumerable.GetEnumerator()
				=> (this as IEnumerable<Type>).GetEnumerator()
			;
		}

		public static IFilter Assembly<T>() => new Filter(typeof(T).Assembly);

		static Reflect()
		{

		}

		private Reflect()
		{

		}

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
