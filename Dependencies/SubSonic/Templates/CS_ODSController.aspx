<%@ Page Language="C#" %>
<%@ Import namespace="SubSonic.Utilities"%>
<%@ Import Namespace="SubSonic" %>
<%
	const string attributeGeneratedCode = "System.CodeDom.Compiler.GeneratedCodeAttribute(\"SubSonic\", \"2.1.0.0\")";
%>
<%
//The data we need
const string providerName = "#PROVIDER#";
const string tableName = "#TABLE#";
TableSchema.Table tbl = DataService.GetSchema(tableName, providerName, TableType.Table);
DataProvider provider = DataService.Providers[providerName];
ICodeLanguage lang = new CSharpCodeLanguage();

//The main vars we need
TableSchema.TableColumnCollection cols = tbl.Columns;
TableSchema.TableColumn[] keys = cols.GetPrimaryKeys();
const bool showGenerationInfo = false;
const bool enableGenerationOfODSDeleteMethods = true;
const bool enableGenerationOfODSInsertMethods = false;
const bool enableGenerationOfODSUpdateMethods = false;
  
%>
using HP.HPFx.Utility;

<% if (showGenerationInfo)
   { %>
 //Generated on <%=DateTime.Now.ToString()%> by <%=Environment.UserName%>
<% }  %>
<%
if (keys.Length > 0)
{
%>
namespace <%=provider.GeneratedNamespace%>
{
    /// <summary>
    /// Controller class for <%=tbl.Name%>
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class <%=tbl.ClassName%>Controller : BaseActiveRecordController<<%=tbl.ClassName%>, <%=tbl.ClassName%>Collection, <%=tbl.ClassName%>Controller>
    {

		[<%=attributeGeneratedCode%>]
		public override TableSchema.Table GetRecordSchema()
		{
			return <%=tbl.ClassName%>.Schema;
		}


		[<%=attributeGeneratedCode%>]
		static <%=tbl.ClassName%>Controller()
		{
			// Preload our schema..
			<%=tbl.ClassName%> thisSchemaLoad = new <%=tbl.ClassName%>(false);
		}

		#region ObjectDataSource support
        <%
string deleteArgs = String.Empty;
string whereArgs = String.Empty;

string deleteDelim = String.Empty;
string whereDelim = String.Empty;
bool useNullables = tbl.Provider.GenerateNullableProperties;
foreach (TableSchema.TableColumn key in keys)
{
	string propName = key.PropertyName;
   bool useNullType = useNullables ? key.IsNullable : false;
   string varType = Utility.GetVariableType(key.DataType, useNullType, lang);

   deleteArgs += deleteDelim + varType + " " + propName;
   deleteDelim = ",";

   whereArgs += whereDelim + "(\"" + propName + "\", " + propName + ")";
   whereDelim = ".AND";

}
%>
<% if (!enableGenerationOfODSDeleteMethods) { %>
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Delete method
<% } else if (keys.Length > 1) { 
// add this delete method if the table has multiple keys
%>

		[<%=attributeGeneratedCode%>]
		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public bool Delete(<%=deleteArgs%>)
		{
			Query qry = new Query(this.GetRecordSchema());
			qry.QueryType = QueryType.Delete;
			qry.AddWhere<%=whereArgs%>;
			qry.Execute();
			return (true);
		}
<% } else if (keys.Length == 1) { 
// add this delete method if the table has 1 key
%>
		[<%=attributeGeneratedCode%>]
		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static bool Delete(<%=deleteArgs%>)
		{
			return (ActiveRecord<<%=tbl.ClassName%>>.Delete(<%=keys[0].PropertyName%>) == 1);
		}
<% } %>

<%
if (keys.Length > 1)
{
        %>

		<%
}
string insertArgs = String.Empty;
string updateArgs = String.Empty;
const string seperator = ",";

foreach (TableSchema.TableColumn col in cols)
{
	string propName = col.PropertyName;
   bool useNullType = useNullables ? col.IsNullable : false;
   string varType = Utility.GetVariableType(col.DataType, useNullType, lang);

   updateArgs += varType + " " + propName + seperator;
   if (!col.AutoIncrement)
   {
	   insertArgs += varType + " " + propName + seperator;
   }
}
if (insertArgs.Length > 0)
	insertArgs = insertArgs.Remove(insertArgs.Length - seperator.Length, seperator.Length);
if (updateArgs.Length > 0)
	updateArgs = updateArgs.Remove(updateArgs.Length - seperator.Length, seperator.Length);
%>
    	
<% if (!enableGenerationOfODSInsertMethods) { %>
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Insert method
<% } else { %>
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		[<%=attributeGeneratedCode%>]
		[DataObjectMethod(DataObjectMethodType.Insert, true)]
		public void Insert(<%=insertArgs%>)
		{
			<%=tbl.ClassName%> item = new <%= tbl.ClassName%>();
			<% 
foreach (TableSchema.TableColumn col in cols)
{
	if (!col.AutoIncrement)
   { 
            %>
            item.<%=col.PropertyName%> = <%=col.PropertyName%>;
            <%
}
} 
            %>
	    
		    item.Save(UserName);
	    }
<% } %>

<% if (!enableGenerationOfODSUpdateMethods) { %>
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Update method
<% } else { %>

		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		[<%=attributeGeneratedCode%>]
		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public void Update(<%=updateArgs%>)
		{
			<%=tbl.ClassName%> item = new <%=tbl.ClassName%>();
			item.MarkOld();
			item.IsLoaded = true;
			<% 
foreach (TableSchema.TableColumn col in cols)
{
				%>
			item.<%=col.PropertyName%> = <%=col.PropertyName%>;
				<%
} 
            %>
			item.Save(UserName);
		}
<% } %>

		#endregion

	}

}
<%
}
else
{
%>
// The class <%= tbl.ClassName%>Controller was not generated because <%= tableName%> does not have a primary key.
<% } %>