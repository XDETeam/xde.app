using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;

namespace Xde.Forms.Code
{
	public class ReflectionCacheUsingBitArray
	{
		private List<Type> _types = new();
		private ImmutableDictionary<Type, BitArray> _contracts;

		public ReflectionCacheUsingBitArray()
		{

		}

		public ReflectionCacheUsingBitArray AddTypes(Assembly assembly)
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
			if (!_contracts.TryGetValue(contract, out var bitmap))
			{
				yield break;
			}

			for (var index = 0; index < bitmap.Length; index++)
			{
				if (bitmap[index])
				{
					yield return _types[index];
				}
			}
		}

		public ReflectionCacheUsingBitArray Prepare()
		{
			var builder = ImmutableDictionary.CreateBuilder<Type, BitArray>();
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
						if (!builder.TryGetValue(contract, out var typesBitmap))
						{
							typesBitmap = new BitArray(_types.Count);
							builder.Add(contract, typesBitmap);
						}

						typesBitmap.Set(index, true);
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
