<%@ Page Language="C#"%>
<%@ Import Namespace="SubSonic" %>
<%@ Import Namespace="System.Data" %>
<%
	const string attributeGeneratedCode = "System.CodeDom.Compiler.GeneratedCodeAttribute(\"SubSonic\", \"2.1.0.0\")";
%>

<%foreach(DataProvider p in DataService.Providers)
 {
	 TableSchema.Table[] tables = DataService.GetTables(p.Name);
	 TableSchema.Table[] views = DataService.GetViews(p.Name);

%>
namespace <%=p.GeneratedNamespace%>
{

	#region Tables Struct

	public partial struct Tables
	{
		<%
	 foreach (TableSchema.Table t in tables)
	 {
		 if (CodeService.ShouldGenerate(t.Name, p.Name))
		 {
%>
		[<%=attributeGeneratedCode%>]
		public static string <%=t.ClassName%> = @"<%=t.Name%>";
		<%
		 }
	 }
%>
	}

	#endregion

	#region Schemas

	public partial class Schemas
	{

		<%
	 foreach (TableSchema.Table t in tables)
	 {
		 if (CodeService.ShouldGenerate(t.Name, p.Name))
		 {
%>
		[<%=attributeGeneratedCode%>]
		public static TableSchema.Table <%=t.ClassName%>{
			get { return DataService.GetSchema("<%=t.Name%>","<%=p.Name%>"); }
		}
		<%
		 }
	 }
%>

	}

	#endregion

	#region View Struct

	public partial struct Views
	{
		<%
	 foreach (TableSchema.Table v in views)
	 {
		 if (CodeService.ShouldGenerate(v.Name, p.Name))
		 {
%>
		[<%=attributeGeneratedCode%>]
		public static string <%=v.ClassName%> = @"<%=v.Name%>";
		<%
		 }
	 }
%>
	}

	#endregion

	#region Query Factories

	public static partial class DB
	{
		[<%=attributeGeneratedCode%>]
		public static DataProvider _provider = DataService.Providers["<%=p.Name%>"];
		[<%=attributeGeneratedCode%>]
		static ISubSonicRepository _repository;

		[<%=attributeGeneratedCode%>]
		public static ISubSonicRepository Repository {
			get
			{
				if (_repository == null)
					return new SubSonicRepository(_provider);

				return _repository; 
			}
			set { _repository = value; }
		}

		[<%=attributeGeneratedCode%>]
		public static Select SelectAllColumnsFrom<T>() where T : RecordBase<T>, new()
		{
			return Repository.SelectAllColumnsFrom<T>();
		}

		[<%=attributeGeneratedCode%>]
		public static Select Select()
		{
			return Repository.Select();
		}

		[<%=attributeGeneratedCode%>]
		public static Select Select(params string[] columns)
		{
			return Repository.Select(columns);
		}

		[<%=attributeGeneratedCode%>]
		public static Select Select(params Aggregate[] aggregates)
		{
			return Repository.Select(aggregates);
		}

		[<%=attributeGeneratedCode%>]
		public static Update Update<T>() where T : RecordBase<T>, new()
		{
			return Repository.Update<T>();
		}

		[<%=attributeGeneratedCode%>]
		public static Insert Insert()
		{
			return Repository.Insert();
		}

		[<%=attributeGeneratedCode%>]
		public static Delete Delete()
		{
			return Repository.Delete();
		}

		[<%=attributeGeneratedCode%>]
		public static InlineQuery Query()
		{
			return Repository.Query();
		}

		<%if (p.TableBaseClass=="RepositoryRecord"){%>
		#region Repository Compliance

		[<%=attributeGeneratedCode%>]
		public static T Get<T>(object primaryKeyValue) where T : RepositoryRecord<T>, new() 
		{
			return Repository.Get<T>(primaryKeyValue);
		}

		[<%=attributeGeneratedCode%>]
		public static T Get<T>(string columnName, object columnValue) where T : RepositoryRecord<T>, new()
		{
			return Repository.Get<T>(columnName,columnValue);
		}

		[<%=attributeGeneratedCode%>]
		public static void Delete<T>(string columnName, object columnValue) where T : RepositoryRecord<T>, new() 
		{
			Repository.Delete<T>(columnName, columnValue);
		}

		[<%=attributeGeneratedCode%>]
		public static void Delete<T>(RepositoryRecord<T> item) where T : RepositoryRecord<T>, new() 
		{
			Repository.Delete<T>(item);
		}

		[<%=attributeGeneratedCode%>]
		public static void Destroy<T>(RepositoryRecord<T> item) where T : RepositoryRecord<T>, new() 
		{
			Repository.Destroy<T>(item);
		}

		[<%=attributeGeneratedCode%>]
		public static void Destroy<T>(string columnName, object value) where T : RepositoryRecord<T>, new() 
		{
			Repository.Destroy<T>(columnName,value);
		}

		[<%=attributeGeneratedCode%>]
		public static int Save<T>(RepositoryRecord<T> item) where T : RepositoryRecord<T>, new() 
		{
			return Repository.Save<T>(item);
		}

		[<%=attributeGeneratedCode%>]
		public static int Save<T>(RepositoryRecord<T> item, string userName) where T : RepositoryRecord<T>, new()
		{
			return Repository.Save<T>(item,userName);
		}

		[<%=attributeGeneratedCode%>]
		public static int Update<T>(RepositoryRecord<T> item) where T : RepositoryRecord<T>, new() 
		{
			return Repository.Update<T>(item, "");
		}

		[<%=attributeGeneratedCode%>]
		public static int Update<T>(RepositoryRecord<T> item, string userName) where T : RepositoryRecord<T>, new() 
		{
			return Repository.Update<T>(item, userName);
		}

		[<%=attributeGeneratedCode%>]
		public static int Insert<T>(RepositoryRecord<T> item) where T : RepositoryRecord<T>, new() 
		{
			return Repository.Insert<T>(item);
		}

		[<%=attributeGeneratedCode%>]
		public static int Insert<T>(RepositoryRecord<T> item, string userName) where T : RepositoryRecord<T>, new() 
		{
			return Repository.Insert<T>(item,userName);
		}

		#endregion

		<%}%>
	}

	#endregion

}
<%} %>


namespace Generated
{

	#region Databases

	public partial struct Databases 
	{
		<%foreach (DataProvider p in DataService.Providers) { %>
		[<%=attributeGeneratedCode%>]
		public static string <%= p.Name %> = @"<%= p.Name%>";
		<%}%>
	}

	#endregion

}