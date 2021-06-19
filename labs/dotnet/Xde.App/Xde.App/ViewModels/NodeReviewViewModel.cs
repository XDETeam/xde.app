using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xde.App.Services;

namespace Xde.App.ViewModels
{
	public class NodeReviewViewModel : BaseViewModel
    {
		public ICommand NodeReviewCommand { get; }

		private string _content = DefaultContent;
		private string _url = null;
		private string _selectedOperator = "@";

		public string Content
		{
			get => _content;
			set => SetProperty(ref _content, value);
		}

		public string Url
		{
			get => _url;
			set => SetProperty(ref _url, value);
		}

		public const string DefaultContent = "Press the button to review some node :)";


		public string Filter { get; set; } = DefaultFilter;

		public const string DefaultFilter = null;
		private readonly IDbConnectionFactory _db;

		public IList<string> Operators { get; } = new List<string>() { "@", "~" };
		public string SelectedOperator
		{
			get => _selectedOperator;
			set => SetProperty(ref _selectedOperator, value);
		}


		public NodeReviewViewModel(IDbConnectionFactory db)
        {
			_db = db;

            Title = "Node Review";
			NodeReviewCommand = new Command(NodeReviewHandler);
		}

		private async void NodeReviewHandler()
		{
			using (IDbConnection conn = _db.CreateConnection())
			{
				var res = conn.Query<Result>("mess.node_review", new { _filter = Filter, _operator = SelectedOperator }, commandType: CommandType.StoredProcedure);
				Content = res.SingleOrDefault()?.content;
				Url = res.SingleOrDefault()?.url;
			}
		}

		class Result
		{
			public string url { get; set; }
			public string content { get; set; }
		}
	}


}
