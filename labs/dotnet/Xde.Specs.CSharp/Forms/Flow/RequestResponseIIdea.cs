namespace Xde.Forms.Flow
{
	public static class RequestResponseIIdea
	{
		public interface IRequest<T> { }

		public interface IResponse<T> { }

		public record SignInRequest(string Mail, string Password);

		public record SignInHashed(string Mail, byte[] Hash);

		public class SignInCommand
			: IRequest<SignInRequest>
			, IResponse<SignInHashed>
		{

		}

		public static void Run()
		{

		}
	}
}
