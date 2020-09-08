using System;
using System.Reflection;
using Xde.Mesh;

Console.WriteLine($"XDE Specs {Assembly.GetEntryAssembly().GetName().Version}");

var signIn = Entity
    .From<Http>(http => http.Path = "/security/sign-in")
;

// RULE: Отдельно взятый диспетчер не знает, кто "придёт за ним". Его задача вернуть
// сооответствующий тип, который выполнит роль абстракции, за которую будут цепляться
// остальные обработчики.

var securityRoute = Dispatcher
    .When<Http>(http => http.Path.Token("/security"))
    .Then<Route>((source, route) => route.Path = "/security") //TODO:Substring...
    //TODO:Redirect if not SSL
;

var signInRoute = Dispatcher
    .When<(Http http, Route route)>(aspects
        => aspects.route.Path.Token("/security") && aspects.route.Path.Next("/sign-in")
    )
;

Console.WriteLine(signIn);

public class Http
{
    public Parser Path { get; set; }
}

public class Route
{
    // Используя парсер мы можем загрузить хоть сразу весь Path, просто в рамках парсера
    // у нас будет много дополнительной информации. Например, какая часть уже прошла успешный
    // синтаксический разбор, а что осталось. Этот же парсер может быть использован для
    // JSON и т.п.
    public Parser Path { get; set; }
}
