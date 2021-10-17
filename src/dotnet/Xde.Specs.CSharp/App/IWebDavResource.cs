using System;

namespace Xde.App
{
	public interface IWebDavResource
	{
		string DisplayName { get; set; }

		DateTimeOffset CreationDate { get; set; }
	}
}
