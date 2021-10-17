namespace Xde.Code.Application
{
	/// <summary>
	/// TODO:Component settings
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks>
	/// Usually we were doing smth like god-like interface IAppSettings. It's a
	/// crap. SRPed interfaces like this one are much better. Just inject smth
	/// like
	/// <code>
	/// public SqlConnectionFactory(ISettings&lt;SqlConnectionSettings&gt; settings)
	/// {
	///		...
	/// }
	/// </code>
	/// </remarks>
	public interface ISettings<T>
	{

	}
}
