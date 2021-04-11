using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Xde.Forms.Flow
{
	/// <summary>
	/// TODO:
	/// </summary>
	/// 
	/// <remarks>
	/// - One node may produce many responses (e.g. log can be an additional producer)
	/// - One node may handler many requests (e.g. log different types of records)
	/// - One node may have zero responses (CQRS or logging that produce nothing)
	/// 
	/// - IRequest/IResponse classes may collect automatically and meshed into the DAG.
	/// </remarks>
	public static class RequestResponseIIdea
	{
		public interface IRequest<T> { }

		public interface IResponse<T> { }

		public interface IHandler<TRequest, TResponse>
			: IRequest<TRequest>
			, IResponse<TResponse>
		{

		}

		public record SignInRequest(string Mail, string Password);

		public record SignInHashed(string Mail, byte[] Hash);

		public record SignInLog(DateTime Created, string Mail, bool success);

		public record SignInResponse(string Token);

		public class SignInHash
			: IRequest<SignInRequest>
			, IResponse<SignInHashed>
		{

		}

		public class SignInProcess
			: IRequest<SignInHash>
			, IResponse<SignInResponse>
			, IResponse<SignInLog>
		{

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

			builder
				.RegisterGeneric((context, types, parameters) =>
				{
					var requestType = typeof(IEnumerable<>).MakeGenericType(
						typeof(IRequest<>).MakeGenericType(types[0])
					);
					var requests =
						(context.Resolve(requestType) as IEnumerable<object>)
						.Select(instance => instance.GetType())
					;

					var responseType = typeof(IEnumerable<>).MakeGenericType(
						typeof(IResponse<>).MakeGenericType(types[1])
					);
					var responses = 
						(context.Resolve(responseType) as IEnumerable<object>)
						.Select(instance => instance.GetType())
					;

					var result = requests
						.Intersect(responses)
						.FirstOrDefault()
					;

					return context.Resolve(result);
				})
				.As(typeof(IHandler<,>))
			;

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
			var handler = container.Resolve<IHandler<SignInRequest, SignInHashed>>();

			var signIn = new SignInRequest("stan", "!qa2Ws3eD");
		}
	}
}
