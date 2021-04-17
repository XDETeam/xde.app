using System;
using System.Linq;
using Xde.Forms.Flow.Parse;

namespace Xde.Forms.Schema.DotNet
{
	/// <summary>
	/// Parser from <see cref="System.Type"/> into <see cref="Form"/>
	/// </summary>
	public class TypeParser
		: IParser<Type, Form>
	{
		/// <summary>
		/// TODO:Parsing function
		/// </summary>
		Form IParser<Type, Form>.Parse(Type type)
		{
			_ = type ?? throw new ArgumentNullException(nameof(type));

			var name = new Fullname()
			{
				Name = type.Name,
				Namespace = type.Namespace,
				Layer = type.AssemblyQualifiedName
			};

			var aspects = type
				.GetProperties()
				.Select(property => new Aspect()
				{
					Name = property.Name,
					//TODO:0 Registry
					Form = (this as IParser<Type, Form>).Parse(property.PropertyType)
				})
			;

			var result = new Form()
			{
				Fullname = name,
				Aspects = aspects
			};

			return result;
		}
	}
}
