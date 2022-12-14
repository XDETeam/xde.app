using System.Collections.Generic;
using System.Linq;

namespace Xde.App.Services.Spins
{
	public class SimpleSpinService
		: ISpinService
	{
		IEnumerable<string> ISpinService.Search(string query) => Enumerable
			.Range(1, 25)
			.Select(index => $"url-{index}-{query}")
		;
	}
}
