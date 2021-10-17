namespace Xde.Forms.Flow
{
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
}
