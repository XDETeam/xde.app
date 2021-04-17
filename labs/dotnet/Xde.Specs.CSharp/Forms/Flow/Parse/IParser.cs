namespace Xde.Forms.Flow.Parse
{
	/// <summary>
	/// TODO:Parser contract
	/// </summary>
	/// <typeparam name="TInput"></typeparam>
	/// <typeparam name="TOutput"></typeparam>
	/// 
	/// <remarks>
	/// TODO: For investigation purposes we'll try to collect the whole flow "vocabulary"
	/// in a code form.
	/// </remarks>
	public interface IParser<TInput, TOutput>
	{
		/// <summary>
		/// TODO: Parse method
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		TOutput Parse(TInput input);
	}
}
