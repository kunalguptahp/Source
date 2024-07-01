<%@ Page Language="C#"%>
<%@ Import namespace="SubSonic.Utilities"%>
<%@ Import Namespace="SubSonic" %>
<%
	const string attributeGeneratedCode = "System.CodeDom.Compiler.GeneratedCodeAttribute(\"SubSonic\", \"2.1.0.0\")";
%>
<%
    const string providerName = "#PROVIDER#";
    const string viewName = "#VIEW#";
    //The data we need
    TableSchema.Table view = DataService.GetSchema(viewName, providerName, TableType.View);
    ICodeLanguage language = new CSharpCodeLanguage();
    
    //The main vars we need
    TableSchema.TableColumnCollection cols = view.Columns;
    string className = view.ClassName;
    string nSpace = DataService.Providers[providerName].GeneratedNamespace;
	const bool enableGenerationOfCopyConstructor = true;
	const bool enableGenerationOfIdenticalToMethods = true;
%>
using HP.HPFx.Utility;
using HP.HPFx.Data.Utility;

namespace <%=nSpace %>{
	/// <summary>
	/// Strongly-typed collection for the <%=className%> class.
	/// </summary>
	[Serializable]
	public partial class <%=className%>Collection : ReadOnlyList[<]<%= className %>, <%=className%>Collection[>], IRecordCollection
	{
		[<%=attributeGeneratedCode%>]
		public <%=className%>Collection() {}

		[<%=attributeGeneratedCode%>]
		public Type GetRecordType()
		{
			return typeof(<%=className%>);
		}
	}

	/// <summary>
	/// Strongly-typed Controller class for the <%=className%> class.
	/// </summary>
	[Serializable]
	public partial class <%=className%>Controller : BaseReadOnlyRecordController[<]<%= className %>, <%= className %>Collection, <%= className %>Controller[>]
	{
		[<%=attributeGeneratedCode%>]
		public <%=className%>Controller() {}

		[<%=attributeGeneratedCode%>]
		public override TableSchema.Table GetRecordSchema()
		{
			return <%=className%>.Schema;
		}
	}

	/// <summary>
	/// This is  Read-only wrapper class for the <%=viewName%> view.
	/// </summary>
	[Serializable]
	public partial class <%=className%> : ReadOnlyRecord[<]<%= className %>[>], IReadOnlyRecord, IRecord
	{

		#region Schema Accessor

		[<%=attributeGeneratedCode%>]
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
				{
					SetSQLProps();
				}
				return BaseSchema;
			}
		}

		[<%=attributeGeneratedCode%>]
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("<%=viewName%>", TableType.View, DataService.GetInstance("<%=providerName%>"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"<%=view.SchemaName%>";

				//columns
				<%
				foreach(TableSchema.TableColumn col in cols)
				{
					string varName = "col" + col.ArgumentName;
				%>
				TableSchema.TableColumn <%=varName %> = new TableSchema.TableColumn(schema);
				<%=varName %>.ColumnName = "<%= col.ColumnName %>";
				<%=varName %>.DataType = DbType.<%=col.DataType %>;
				<%=varName %>.MaxLength = <%=col.MaxLength %>;
				<%=varName %>.AutoIncrement = false;
				<%=varName %>.IsNullable = <%=col.IsNullable.ToString().ToLower()%>;
				<%=varName %>.IsPrimaryKey = false;
				<%=varName %>.IsForeignKey = false;
				<%=varName %>.IsReadOnly = <%= col.IsReadOnly.ToString().ToLower() %>;
				<%
				if(col.IsForeignKey)
				{
				%>
				<%=varName %>.ForeignKeyTableName = "<%= col.ForeignKeyTableName %>";
				<% } %>
				schema.Columns.Add(<%=varName%>);

				<%
				}
				%>

				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["<%=providerName %>"].AddSchema("<%=viewName%>",schema);
			}
		}

		#endregion

		#region Query Accessor

		[<%=attributeGeneratedCode%>]
		public static Query CreateQuery()
		{
			return new Query(Schema);
		}

		#endregion

		#region Constructors and Default Settings

		[<%=attributeGeneratedCode%>]
		protected <%=className%>(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}

		[<%=attributeGeneratedCode%>]
		public <%=className%>() : this(false) {}

		[<%=attributeGeneratedCode%>]
		public <%=className%>(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}

		[<%=attributeGeneratedCode%>]
		public <%=className%>(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}

		[<%=attributeGeneratedCode%>]
		public <%=className%>(string columnName, object columnValue)
			: this(false, false)
		{
			LoadByParam(columnName, columnValue);
		}

		[<%=attributeGeneratedCode%>]
		protected static void SetSQLProps() { GetTableSchema(); }

		[<%=attributeGeneratedCode%>]
		private void InitSetDefaults() { SetDefaults(); }

		[<%=attributeGeneratedCode%>]
		private void ConstructorHelper(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if (useDatabaseDefaults)
			{
				this.ForceDefaults();
			}
			else
			{
				this.InitSetDefaults();
			}
		}

		#region Copy Constructor

<% if (!enableGenerationOfCopyConstructor) { %>
	//NOTE: Code Generation: Generation disabled: Copy Constructor
<% } else { %>

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="<%=className%>"/> instance to duplicate.</param>
		[<%=attributeGeneratedCode%>]
		private <%=className%>(<%=className%> original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}

		[<%=attributeGeneratedCode%>]
		private void CopyConstuctorHelper(<%=className%> original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			<% foreach(TableSchema.TableColumn col in cols){ %>
			this.<%=col.PropertyName%> = original.<%=col.PropertyName%>;
			<% } %>
		}

		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="<%=className%>"/> instance to duplicate.</param>
		[<%=attributeGeneratedCode%>]
		public static <%=className%> Copy(<%=className%> original)
		{
			return new <%=className%>(original);
		}

<% } %>

		#endregion

		#endregion

		#region Properties

		partial void OnPropertyChanging(string propertyName, object newValue);

		partial void OnPropertyChanged(string propertyName, object oldValue, object newValue);

		<%
		foreach(TableSchema.TableColumn col in cols)
		{
			string propName = col.PropertyName;
			string varType = Utility.GetVariableType(col.DataType, col.IsNullable, language);
		%>
		partial void On<%=propName%>Changing(<%=varType%> newValue);

		partial void On<%=propName%>Changed(<%=varType%> oldValue, <%=varType%> newValue);

		[<%=attributeGeneratedCode%>]
		[XmlAttribute("<%=propName%>")]
		[Bindable(true)]
		public <%=varType%> <%=propName%> 
		{
			get
			{
				return GetColumnValue[<]<%= varType %>[>]("<%= col.ColumnName %>");
			}
			set
			{
				this.On<%=propName%>Changing(value);
				this.OnPropertyChanging("<%=propName%>", value);
				<%=varType%> oldValue = this.<%=propName%>;
				SetColumnValue("<%=col.ColumnName%>", value);
				this.On<%=propName%>Changed(oldValue, value);
				this.OnPropertyChanged("<%=propName%>", oldValue, value);
			}
		}

<%
		}
%>
		#endregion

		#region Columns Struct

		[<%=attributeGeneratedCode%>]
		public struct Columns
		{

			<%
			foreach (TableSchema.TableColumn col in cols) {
				string propName = col.PropertyName;
			%>
			public static string <%=propName%> = @"<%=col.ColumnName%>";
			<%
			  }
			%>

		}

		#endregion

		#region IAbstractRecord Members

		[<%=attributeGeneratedCode%>]
		public new CT GetColumnValue[<]CT[>](string columnName) {
			return base.GetColumnValue[<]CT[>](columnName);
		}

		[<%=attributeGeneratedCode%>]
		public object GetColumnValue(string columnName) {
			return base.GetColumnValue[<]object[>](columnName);
		}

		#endregion

		#region Equality Methods

<% if (!enableGenerationOfIdenticalToMethods) { %>
	//NOTE: Code Generation: Generation disabled: IdenticalTo method
<% } else { %>
		
		// /// <summary>
		// /// Generates instance hashcodes based upon the instance's primary key value.
		// /// </summary>
		// /// <returns>A hash code for the current <see cref="<%=className%>"/>.</returns>
		// [<%=attributeGeneratedCode%>]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("<%=className%>#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }

		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="<%=className%>"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="<%=className%>"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="<%=className%>"/>; otherwise, <c>false</c>.</returns>
		[<%=attributeGeneratedCode%>]
		public bool IdenticalTo(object obj)
		{
			<%=className%> instance1 = this;
			<%=className%> instance2 = obj as <%=className%>;

			bool isEqual =
				(instance2 != null)
			<%foreach(TableSchema.TableColumn col in cols){%>
				&& (instance1.<%=col.PropertyName%> == instance2.<%=col.PropertyName%>)
			<%}%>
			;

			return isEqual; //return base.Equals(obj);
		}

/*
		/// <summary>
		/// Determines whether the specified <see cref="<%=className%>"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="<%=className%>"/> to compare.</param>
		/// <param name="instance2">The second <see cref="<%=className%>"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[<%=attributeGeneratedCode%>]
		public static bool Equals(<%=className%> instance1, <%=className%> instance2)
		{
			if (instance1 == null)
			{
				return (instance2 == null);
			}

			return instance1.Equals(instance2);
		}
*/

<% } %>

		#endregion

	}
}
