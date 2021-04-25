using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace Xde.Forms.Code
{
	/// TODO:Rename to Reflector?
	/// TODO:Keep only sources, may be initialize using array initializer
	/// TODO:Think about replacing BitArray with simple list of implementation types
	/// (indexes).
	public class ReflectionAssistant
	{
		private static readonly IEnumerable<Type> _empty = Enumerable.Empty<Type>();

		private List<Type> _types = new();
		private Tuple<string, int>[] _names;
		// TODO:There are some doubts between Immutable and straighforward Dictionary.
		// Look at DictionaryBenchmark
		private ImmutableDictionary<Type, List<Type>> _contracts;

		public ReflectionAssistant()
		{

		}

		public ReflectionAssistant AddTypes(Assembly assembly)
		{
			foreach (var type in assembly.GetTypes())
			{
				//TODO:Only public (configurable, any additional conditions)
				_types.Add(type);
			}

			return this;
		}

		public IEnumerable<Type> Lookup(Type contract)
		{
			if (_contracts.TryGetValue(contract, out var result))
			{
				return result;
			}

			return _empty;
		}

		public ReflectionAssistant Prepare()
		{
			var builder = ImmutableDictionary.CreateBuilder<Type, List<Type>>();
			var contracts = new List<Type>(16);

			for (var index = 0; index < _types.Count; index++)
			{
				var type = _types[index];

				// TODO:Limit contracts only by existing in provided assemblies? Or in the same
				// list of types.
				foreach (var contract in type.GetInterfaces())
				{
					contracts.Add(contract);
					if (contract.IsGenericType)
					{
						contracts.Add(contract.GetGenericTypeDefinition());
					}
				}

				if (contracts.Count > 0)
				{
					foreach (var contract in contracts)
					{
						if (!builder.TryGetValue(contract, out var types))
						{
							types = new List<Type>(_types.Count);
							builder.Add(contract, types);
						}

						types.Add(type);
					}

					// TODO: CollectionBenchmark.ReuseListUsingClear looks like being
					// 2x faster than recreating.
					contracts.Clear();
				}
			}

			_contracts = builder.ToImmutable();

			return this;
		}
	}
}
