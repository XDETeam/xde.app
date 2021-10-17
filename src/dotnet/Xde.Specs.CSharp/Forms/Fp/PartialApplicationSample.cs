using System;
using System.Linq;
using System.Linq.Expressions;

namespace Xde.Forms.Fp
{
	/// <summary>
	/// Partial application.
	/// </summary>
	public static class PartialApplicationSample
	{
		public class SampleVisitor : ExpressionVisitor
		{
			private readonly ParameterExpression _parameter;
			private readonly ConstantExpression _value;

			public SampleVisitor(ParameterExpression parameter, ConstantExpression value)
			{
				_parameter = parameter;
				_value = value;
			}

			protected override Expression VisitParameter(ParameterExpression node)
			{
				if (node == _parameter)
				{
					return _value;
				}

				return base.VisitParameter(node);
			}

			protected override Expression VisitLambda<T>(Expression<T> node)
			{
				return Expression.Lambda(node.Body, node.Parameters.Skip(1));
			}
		}

		public static Expression<Func<T1, T2>> Apply<T1, T2, T3>(this Expression<Func<T1, T2, T3>> expression, T1 value)
		{
			var visitor = new SampleVisitor(expression.Parameters[0], Expression.Constant(value, typeof(T1)));

			var result = Expression.Lambda<Func<T1, T2>>(
				visitor.Visit(expression.Body),
				expression.Parameters.Skip(1)
			);

			return result;
		}

		public static void Main()
		{
			Expression<Func<int, int, int>> expr1 = (x, y) => x * y;
			var applied = expr1.Apply(2);

			var compiled = applied.Compile();

			Console.WriteLine("Sample v4");
			Console.WriteLine("Partially applied 2 => *2 = {0}", compiled(2));
		}
	}
}
