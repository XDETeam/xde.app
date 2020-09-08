using System;
using System.Reflection;
using Xde.Mesh;

Console.WriteLine($"XDE Specs {Assembly.GetEntryAssembly().GetName().Version}");

var entity1 = Entity
    .Create()
    .Set<Http>()
    .Set<Route>()
;

var entity2 = Entity.Create();

Console.WriteLine(entity1);
Console.WriteLine(entity2);
Console.WriteLine(entity1.Get<Http>().Method);

public class Http
{
    public string Method { get; set; }
}

public class Route
{
    public string Path { get; set; }
}
