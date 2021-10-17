namespace Xde.Forms.Flow
{
	public class Sample3
	{
		public class ReferralRedirect { }
		public class UserJwt { }

		public delegate (ReferralRedirect, UserJwt) SignedUser(string login, string password);

		// HttpsGuard -> SignInForm -> {UserInput} -> ApiEndPoint -> ....
	}
}
