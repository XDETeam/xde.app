using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Xde.Forms.Code
{
	/// TODO:Helper tool for reflection routines
	public class Reflect
	{
		private static readonly Reflect Current = new();

		/// TODO:Temporary class for extended type information. Maybe temporary...
		public class TypeInfo
		{
			public Type Type { get; set; }
			public string Namespace { get; set;}
			public bool CanNew { get; set;}
		}

		public interface IFilter
			: IEnumerable<TypeInfo>
		{

		}

		private class Filter
			: IFilter
		{
			private readonly System.Collections.Generic.GrowableArray _assembly;

			public Filter(Assembly assembly)
			{
				_assembly = assembly;
			}

			IEnumerator<TypeInfo> IEnumerable<TypeInfo>.GetEnumerator()
			{
				return _assembly
					.GetTypes()
					.Select(type => new TypeInfo
					{
						Type = type,
						CanNew = CanNew(type),
						Namespace = type.Namespace
					})
					.GetEnumerator()
				;
			}

			IEnumerator IEnumerable.GetEnumerator()
				=> (this as IEnumerable<TypeInfo>).GetEnumerator()
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
