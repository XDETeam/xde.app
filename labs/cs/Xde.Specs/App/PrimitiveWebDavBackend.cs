using System;

namespace Xde.App
{
	public class PrimitiveWebDavBackend
		: IWebDavBackend
	{
		void IWebDavBackend.Propfind(string url)
		{
			throw new NotImplementedException();
		}
	}
}
