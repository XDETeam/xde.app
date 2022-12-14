namespace Xde.Forms.Flow.Parse
{
	/// <summary>
	/// TODO:Tests for arithmetic parsers
	/// </summary>
	/// 
	/// <remarks>
	/// Ideally arithmetic parsers should not do this from scratch for legion of languages
	/// that has its support.
	/// </remarks>
	public class ArithmeticSpecs
	{
		public class Item
		{
			public static Item operator |(Item left, Item right) => new Item();
		}

		#region -- Arithmetic sample -------------------------------------------------
		public static readonly Item ArithmeticPlus = new Item();
		public static readonly Item ArithmeticMinus = new Item();
		public static readonly Item ArithmeticMultiply = new Item();
		public static readonly Item ArithmeticDivide = new Item();
		public static readonly Item ArithmeticOperations
			= ArithmeticPlus
			| ArithmeticMinus
			| ArithmeticMultiply
			| ArithmeticDivide
		;
		#endregion -------------------------------------------------------------------
	}
}
