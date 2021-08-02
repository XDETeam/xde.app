using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Xde.Forms.Collections
{
	public class RecurseGeneratorBenchmark
	{
		public class Node
		{
			public IEnumerable<Node> Children { get; set; }
		}

		public static Node BuildTree(int levels = 5, int nodesPerLevel = 5) => new()
		{
			Children = levels > 1
				? Enumerable
					.Range(1, nodesPerLevel)
					.Select(index => BuildTree(levels - 1, nodesPerLevel))
				: null
		};

		public static IEnumerable<Node> DoRecurseUsingYield(Node node)
		{
			yield return node;

			if (node.Children != null)
			{
				foreach (Node child in node.Children)
				{
					foreach (var recursed in DoRecurseUsingYield(child))
					{
						yield return recursed;
					}
				}
			}
		}

		[Benchmark]
		public void RecurseUsingYield()
		{
			var tree = BuildTree(6,6);
			var result = DoRecurseUsingYield(tree);
			Assert.Equal(9331, result.Count());
		}

		public static IEnumerable<Node> DoRecurseUsingConcat(Node node)
		{
			return new[] { node }
				.Concat(
					node.Children != null
					? node.Children.SelectMany(child => DoRecurseUsingConcat(child))
					: Enumerable.Empty<Node>()
				)
			;
		}

		[Benchmark]
		public void RecurseUsingConcat()
		{
			var tree = BuildTree(6, 6);
			var result = DoRecurseUsingConcat(tree);
			Assert.Equal(9331, result.Count());
		}

		public static void DoRecurseUsingList(Node node, List<Node> nodes)
		{
			nodes.Add(node);

			if (node.Children != null)
			{
				foreach (var child in node.Children)
				{
					DoRecurseUsingList(child, nodes);
				}
			}
		}

		[Benchmark]
		public void RecurseUsingList()
		{
			var tree = BuildTree(6, 6);
			var list = new List<Node>();
			DoRecurseUsingList(tree, list);
			Assert.Equal(9331, list.Count);
		}

		[Benchmark]
		public void RecurseUsingListWithCapacity()
		{
			var tree = BuildTree(6, 6);
			var list = new List<Node>(10000);
			DoRecurseUsingList(tree, list);
			Assert.Equal(9331, list.Count);
		}

		[Benchmark]
		public void ReuseListUsingNew()
		{
			for (var index = 0; index < 100; index++)
			{
				var list = new List<int>(16);
				for (var i = 0; i < 16; i++)
				{
					list.Add(i);
				}
			}
		}

		[Benchmark]
		public void ReuseListUsingClear()
		{
			var list = new List<int>(16);

			for (var index = 0; index < 100; index++)
			{
				for (var i = 0; i < 16; i++)
				{
					list.Add(i);
				}

				list.Clear();
			}
		}
	}
}
