using System;
using Xde.Forms.Mesh;

namespace Xde.Forms.Flow
{
	public class Sample1
	{
		public static void Play()
		{
			var signIn = Entity
				.From<Http>(http => http.Path = "/security/sign-in")
			;

			// RULE: Отдельно взятый диспетчер не знает, кто "придёт за ним". Его задача вернуть
			// сооответствующий тип, который выполнит роль абстракции, за которую будут цепляться
			// остальные обработчики.

			var securityRoute = When
				// TODO:Как эффективно диспетчеризовать без необходимость делать для каждого запроса
				// проверку по нескольким (десятков) потенциальных роутов.
				.Is<Http>(http => http.Path.Token("/security"))
				.Then<Route>((from, to) => to.Path = "/security")
			//.Then<Http, TlsGuard>(http => http.IsTls)
			;

			var checkTls = When
				.Is<(Http http, TlsGuard guard)>(aspects => !aspects.http.IsTls)
				//.Then<Route>((from, to) => to.Path = from.route.Path)
				.Then<Redirect>((from, to) => to.Path = $"https://{from.http.Path}")
			;

			var signInRoute = When
				.Is<(Http http, Route route)>(aspects
					=> aspects.route.Path.Token("/security") && aspects.route.Path.Next("/sign-in")
				)
			;

			Console.WriteLine(signIn);
		}

		public class Http
		{
			public Parser Path { get; set; }

			public bool IsTls { get; set; }
		}

		public class Route
		{
			// Используя парсер мы можем загрузить хоть сразу весь Path, просто в рамках парсера
			// у нас будет много дополнительной информации. Например, какая часть уже прошла успешный
			// синтаксический разбор, а что осталось. Этот же парсер может быть использован для
			// JSON и т.п.
			public Parser Path { get; set; }
		}

		public class TlsGuard
		{

		}

		public class Redirect
		{
			public string Path { get; set; }
		}
	}
}
