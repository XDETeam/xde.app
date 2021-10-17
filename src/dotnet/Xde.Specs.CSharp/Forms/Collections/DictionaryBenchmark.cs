using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Xunit;

namespace Xde.Forms.Collections
{
	public class DictionaryBenchmark
	{
		private const int N = 100;

		private KeyValuePair<Type, string>[] _types;
		private Dictionary<Type, string> _dictionary;
		private IImmutableDictionary<Type, string> _immutableDictionary;
		private Hashtable _hashtable;

		[GlobalSetup]
		public void Setup()
		{
			_types = typeof(Type)
				.Assembly
				.GetTypes()
				.Take(N)
				.Select(type => new KeyValuePair<Type, string>(type, type.FullName))
				.ToArray()
			;

			Assert.Equal(N, _types.Length);

			_dictionary = new Dictionary<Type, string>(_types);

			var builder = ImmutableDictionary.CreateBuilder<Type, string>();
			builder.AddRange(_types);
			_immutableDictionary = builder.ToImmutableDictionary();

			_hashtable = new Hashtable(_dictionary);
		}

		[Benchmark]
		public void LookupTypeUsingDictionary()
		{
			var ok = true;

			for (var index = 0; index < N; index++)
			{
				ok &= _dictionary
					.TryGetValue(_types[index].Key, out string _)
				;
			}
		}

		[Benchmark]
		public void LookupTypeUsingImmutableDictionary()
		{
			var ok = true;

			for (var index = 0; index < N; index++)
			{
				ok &= _immutableDictionary
					.TryGetValue(_types[index].Key, out string _)
				;
			}
		}

		[Benchmark]
		public void LookupTypeUsingHashtable()
		{
			var ok = true;

			for (var index = 0; index < N; index++)
			{
				ok &= _hashtable.ContainsKey(_types[index].Key);
			}
		}
	}
}
