using System.Collections.Generic;

namespace Xde.App.Services.Spins
{
	public interface ISpinService
	{
		IEnumerable<string> Search(string query);
	}
}
