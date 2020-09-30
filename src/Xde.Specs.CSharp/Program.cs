using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xde.Mesh;

Console.WriteLine($"XDE Specs {Assembly.GetEntryAssembly().GetName().Version}");

/*
 * - Есть раздел Security | Account
 * - Этот раздел должен быть доступен через протокол HTTPS. При попытке HTTP-доступа,
 * осуществлять 302 Redirect.
 *   - Может быть введена категория "безопасных разделов"
 * - В рамках Security есть операция SignIn
 *   - Вводится логин и пароль (реквизиты)
 *   - Проверяется, что пользователь с такими реквизитами присутсвует в базе данных.
 *   - Редиректим на ReferrerUrl, если он был предоставлен и не входит в число некоторых
 *   системных, в противном случае ведём на дефолтную страницу.
 *   - Опция remember (persistent cookie)
 *   
 */

Sample4.Main();

/// <summary>
/// Partial application.
/// </summary>
public static class Sample4
{
    public class SampleVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;
        private readonly ConstantExpression _value;

        public SampleVisitor(ParameterExpression parameter, ConstantExpression value)
        {
            _parameter = parameter;
            _value = value;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node == _parameter)
            {
                return _value;
            }

            return base.VisitParameter(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return Expression.Lambda(node.Body, node.Parameters.Skip(1));
        }
    }

    public static Expression<Func<T1, T2>> Apply<T1, T2, T3>(this Expression<Func<T1, T2, T3>> expression, T1 value)
    {
        var visitor = new SampleVisitor(expression.Parameters[0], Expression.Constant(value, typeof(T1)));

        var result = Expression.Lambda<Func<T1, T2>>(
            visitor.Visit(expression.Body),
            expression.Parameters.Skip(1)
        );

        return result;
    }

    public static void Main()
    {
        Expression<Func<int, int, int>> expr1 = (x, y) => x * y;
        var applied = expr1.Apply(2);

        var compiled = applied.Compile();

        Console.WriteLine("Sample v4");
        Console.WriteLine("Partially applied 2 => *2 = {0}", compiled(2));
    }
}

public class Sample3
{
    public class ReferralRedirect { }
    public class UserJwt { }

    public delegate (ReferralRedirect, UserJwt) SignedUser(string login, string password);

    // HttpsGuard -> SignInForm -> {UserInput} -> ApiEndPoint -> ....
}

public class Sample2
{
    //var RegisteredUser = (HtmlMessageSuccesfullyRegistered + Notification<Registration>) | Error<Registration>
    //var RegistrationError = UserAlreadyExists | SuspiciousRegistrationActivity
    //var HtmlMessageSuccesfullyRegistered = UserRecordInDb >> HtmlFragment<Registration>
    //var RegistrationNotificationMail = UserRecordInDb >> MailMessage.Template("Registration")
    //var UserRecordInDb = ValidRegistrationRequest...

    //var TlsProtectedSection = 

    //var InsertedRecord<T> = T >> DbInsertTransformer<T> >> DbQuery
    //var InserterUser = InsertedRecord<User>

    //var Log = Gene.Of<LogWrite>();
    //var DbQuery = Gene.Of<DbStatement>() & Log;
    //var InsertDbRecord = Gene.Of<SqlInsertGenerate>() & DbQuery;
    //var InsertUser = InsertDbRecord.For<User>();

    //var SignedUser = (AuthCookie + JsonResponse<UserTicket>) | (UserNotFound | ApiError)
    //var AuthCookie = GetUserByCreds & (user => User.Id) & WriteHttpCookie
    //var JsonResponse<UserTicket> = GetUserByCreds & JsonDecoder
    //var UserNotFound = GetUserByCreds & (user => user == null)
    //var SomeSecuredProduct = AuthorizedUser & GetProduct

    // TODO:Абстракции в случае конструкций вроде DbQuery. Может сделать так, что в принципе это могут
    // быть только абстрактные DSL-statements, которые потом маппяться на реальные команды, могут
    // пропускаться через "player".
    // TODO: Где аспекты?

    public class Gene
    {
        public static Gene operator +(Gene left, Gene right) => right;

        public static Gene operator |(Gene left, Gene right) => right;

        public static Gene operator &(Gene left, Gene right) => right;

        public static Gene operator <(Gene left, Gene right) => left;

        public static Gene operator >(Gene left, Gene right) => right;

        public static Gene Of<T>() => new Gene();

        public Gene For<T>() => this;
    }

    public class DbStatement
    {

    }

    public class LogWrite
    {

    }

    public class SqlInsertGenerate
    {

    }

    public class User
    {

    }

    public static void Main()
    {
        var gene1 = new Gene();
        var gene2 = new Gene();
        var gene3 = new Gene();
        var gene = gene1 > gene2 > gene3;
    }
}

public class Sample1 {
    public static void Play() {

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