using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Xde.Forms.Flow
{
	/// <summary>
	/// TODO:
	/// </summary>
	/// 
	/// <remarks>
	/// - One node may produce many responses (e.g. log can be an additional producer)
	/// - One node may handler many requests (e.g. log different types of records). 
	/// Do we interpret this as any or all?
	/// - One node may have zero responses (CQRS or logging that produce nothing)
	/// 
	/// - IRequest/IResponse classes may collect automatically and meshed into the DAG.
	/// - IRequest/IResponse maybe smth like IConsume/IProduce, IAccept/IDeliver,
	/// IResource/IProduct, etc.
	/// </remarks>
	public static class RequestResponseIIdea
	{
		public interface IRequest<T>
		{
			T Request { set; }
		}

		public interface IResponse<T>
		{
			T Response { get; }
		}

		public interface IHandler
		{
			Task Handle();
		}

		//TODO:Tuples as generic args
		public interface IContract<TRequest, TResponse>
		{
			TRequest Request { get; set; }
			TResponse Response { get; set; }
		}

		public record SignInRequest(string Mail, string Password);

		public record SignInHashed(string Mail, byte[] Hash);

		public record SignInLog(DateTime Created, string Mail, bool success);

		public record SignInResponse(string Token);

		public class SignInHash
			: IHandler
			, IRequest<SignInRequest>
			, IResponse<SignInHashed>
		{
			private SignInRequest _request;
			SignInRequest IRequest<SignInRequest>.Request { set => _request = value; }

			private SignInHashed _response;
			SignInHashed IResponse<SignInHashed>.Response => _response;

			Task IHandler.Handle()
			{
				_response = new SignInHashed(
					_request.Mail,
					SHA256.HashData(Encoding.UTF8.GetBytes(_request.Password))
				);

				return Task.CompletedTask;
			}
		}

		public class SignInProcess
			: IHandler
			, IRequest<SignInHash>
			, IResponse<SignInResponse>
			, IResponse<SignInLog>
		{
			SignInLog IResponse<SignInLog>.Response => throw new NotImplementedException();

			SignInResponse IResponse<SignInResponse>.Response => throw new NotImplementedException();

			SignInHash IRequest<SignInHash>.Request { set => throw new NotImplementedException(); }

			Task IHandler.Handle()
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// API controller sample
		/// </summary>
		public class WebController
		{
			public SignInResponse SignIn(SignInRequest request)
			{
				throw new NotImplementedException(); //TODO:
			}
		}

		public static void Run()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<SignInHash>()
				.As<IRequest<SignInRequest>>()
				.As<IResponse<SignInHashed>>()
				.As<SignInHash>()
			;
			builder.RegisterType<SignInProcess>()
				.As<IRequest<SignInHash>>()
				.As<IResponse<SignInResponse>>()
				.As<SignInProcess>()
			;

			//builder
			//	.RegisterGeneric((context, types, parameters) =>
			//	{
			//		var requestType = typeof(IEnumerable<>).MakeGenericType(
			//			typeof(IRequest<>).MakeGenericType(types[0])
			//		);
			//		var requests =
			//			(context.Resolve(requestType) as IEnumerable<object>)
			//			.Select(instance => instance.GetType())
			//		;

			//		var responseType = typeof(IEnumerable<>).MakeGenericType(
			//			typeof(IResponse<>).MakeGenericType(types[1])
			//		);
			//		var responses = 
			//			(context.Resolve(responseType) as IEnumerable<object>)
			//			.Select(instance => instance.GetType())
			//		;

			//		var result = requests
			//			.Intersect(responses)
			//			.FirstOrDefault()
			//		;

			//		return context.Resolve(result);
			//	})
			//	.As(typeof(IHandler<,>))
			//;

			var container = builder.Build();

			var requests = container
				.Resolve<IEnumerable<IRequest<SignInRequest>>>()
				.ToArray()
			;
			var responses = requests
				.SelectMany(item => item
					.GetType()
					.GetInterfaces()
					.Where(contract => contract.IsGenericType)
					.Where(contract => contract.GetGenericTypeDefinition() == typeof(IResponse<>))
				)
				.ToArray()
			;
			//var handler = container.Resolve<IHandler<SignInRequest, SignInHashed>>();

			var signIn = new SignInRequest("stan", "!qa2Ws3eD");
		}
	}
}
