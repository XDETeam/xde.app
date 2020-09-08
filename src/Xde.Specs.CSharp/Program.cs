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

// Нам надо разобрать роут на две части: security -> sign-in. Это обеспечить гибкость при
// дальнейшем создании динамических роутов.
//var securityRoute = Dispatcher
//    .When<Http>(http => http.Path.StartsWith("/security"))
//    .Then<Route>((source, route) => route.Path = source.Path) //TODO:Substring...
//;

var securityRoute = Dispatcher
    .When<Http>(http => http.Path.StartsWith("/security"))
    .Then<Route>((source, route) => route.Path = "/security") //TODO:Substring...
    //TODO:Redirect if not SSL
;

var signInRoute = Dispatcher
    .When<(Http http, Route route)>(aspects
        => aspects.route.Path == "/security" && aspects.route.Remainder.StartsWith("/sign-in")
    )
;

Console.WriteLine(signIn);

public class Http
{
    public string Path { get; set; }
}

public class Route
{
    public string Path { get; set; }

    public string Remainder { get; set; }
}
