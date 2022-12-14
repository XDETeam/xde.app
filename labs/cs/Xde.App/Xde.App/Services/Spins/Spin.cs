using System;

namespace Xde.App.Services.Spins
{
	public class Spin
	{
		/// <summary>
		/// User ID
		/// </summary>
		public string User {  get; set; }

		/// <summary>
		/// Visited spin URL
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Timestamp when user visited spin
		/// </summary>
		public DateTime Visited {  get; set; }
	}
}
