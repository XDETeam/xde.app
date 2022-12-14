using System;
using System.Collections.Generic;
using Xde.Forms.Flow.Parse;

namespace Xde.Forms.Schema
{
	/// <summary>
	/// TODO:Schema registry
	/// </summary>
	/// 
	/// <remarks>
	/// Many forms (types) are reusable, so when
	/// <see cref="IParser{TInput, TOutput}">parsing</see> we should not create
	/// forms again and again when came across the same input type/class/etc.
	/// 
	/// What is a good interface for this class? IDictionary looks like an overkill.
	/// Similar Get/Set methods are also questionable, because Set can potentially
	/// assign Fullname and Form from different instances.
	/// </remarks>
	public class SchemaRegistry
	{
		private Dictionary<string, Form> _forms = new();

		/// <summary>
		/// TODO:Get form
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Form Get(Fullname name)
			=> _forms.TryGetValue(name.Hash, out var form) ? form : null
		;

		/// <summary>
		/// TODO:Add form
		/// </summary>
		/// <param name="form"></param>
		public void Add(Form form)
		{
			_ = form ?? throw new ArgumentNullException(nameof(form));

			var hash = form.Fullname.Hash;

			if (_forms.ContainsKey(hash))
			{
				// TODO:Implement AlreadyExistsException or something like this
				throw new InvalidOperationException("Already exists");
			}

			_forms.Add(hash, form);
		}
	}
}
