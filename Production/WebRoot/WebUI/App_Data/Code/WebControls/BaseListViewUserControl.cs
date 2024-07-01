using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Data.Export;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Utility;
using HP.HPFx.Web.Utility;

namespace HP.ElementsCPS.Apps.WebUI.WebControls
{
	public abstract class BaseListViewUserControl : UserControl
	{

		#region Events

		public event EventHandler QueryChanged;

		protected void RaiseQueryChanged(EventArgs e)
		{
			if (this.QueryChanged != null)
			{
				this.QueryChanged(this, e);
			}
		}

		public event EventHandler ImmutableQueryConditionsChanged;

		protected void RaiseImmutableQueryConditionsChanged(EventArgs e)
		{
			if (this.ImmutableQueryConditionsChanged != null)
			{
				this.ImmutableQueryConditionsChanged(this, e);
			}
		}

		public event EventHandler QueryInputSaved;

		protected void RaiseQueryInputSaved(EventArgs e)
		{
			if (this.QueryInputSaved != null)
			{
				this.QueryInputSaved(this, e);
			}
		}

		#endregion

		#region Constants

		private const string ViewStateKey_QuerySpecification = "QuerySpecification";
		private const string ViewStateKey_ImmutableQueryConditions = "ImmutableQueryConditions";

		#endregion

		#region Properties

		private IQuerySpecification _CachedQuerySpecification = null;

		/// <remarks>
		/// NOTE: Setting the value of this property automatically invokes the <see cref="SaveQuerySpecificationChanges"/> method.
		/// </remarks>
		public IQuerySpecification QuerySpecification
		{
			get
			{
				this.RefreshQuerySpecificationCache();
				return this._CachedQuerySpecification;
			}
			set
			{
				this.RefreshQuerySpecificationCache(); //this is needed in case the set method is called before the get method.
				this._CachedQuerySpecification = value; //update the backing field (i.e. the "cached" copy) immediately
				this.SaveQuerySpecificationChanges(); //this will raise the QueryChanged event (if a change occurred)
			}
		}

		/// <summary>
		/// Returns an <see cref="IQuerySpecification"/> that represents the result of combining the 
		/// <see cref="QuerySpecification"/> and <see cref="ImmutableQueryConditions"/> values.
		/// </summary>
		public IQuerySpecification CombinedQuerySpecification
		{
			get
			{
				return HPFx.Data.Query.QuerySpecification.Merge(this.QuerySpecification, this.ImmutableQueryConditions);
			}
		}

		private void RefreshQuerySpecificationCache()
		{
			if (this._CachedQuerySpecification == null)
			{
				this._CachedQuerySpecification = this.QuerySpecificationViewState;
				if (this._CachedQuerySpecification == null)
				{
					//NOTE: use ImmutableQueryConditions as the default value so that any filter-initialization code will properly reflect the ImmutableQueryConditions
					this._CachedQuerySpecification = new QuerySpecificationWrapper(this.ImmutableQueryConditions.InnerData.Copy());
				}
			}
		}

		/// <summary>
		/// Indicates whether any changes have been made to the <see cref="QuerySpecification"/> since 
		/// <see cref="SaveQuerySpecificationChanges"/> was last invoked.
		/// </summary>
		/// <returns></returns>
		public bool HasQuerySpecificationChanged()
		{
			return (this._CachedQuerySpecification != this.QuerySpecificationViewState);
		}

		/// <remarks>
		/// <para>
		/// Note that, unlike the <see cref="QuerySpecification"/> property, this property may return a <c>null</c> value.
		/// </para>
		/// <para>
		/// Note that, unlike the <see cref="QuerySpecification"/> property, any direct modifications of the value returned by this property WILL NOT be persisted.
		/// </para>
		/// </remarks>
		private IQuerySpecification QuerySpecificationViewState
		{
			get
			{
				//TODO: Review Needed: Review serialization implementation for correctness
#warning Review Needed: Review serialization implementation for correctness
				string xml = this.ViewState[ViewStateKey_QuerySpecification] as string;
				return (string.IsNullOrEmpty(xml)) ? null : QuerySpecificationWrapper.FromXml(xml);
				//return this.ViewState[ViewStateKey_QuerySpecification] as IQuerySpecification;
			}
			set
			{
				IQuerySpecification currentQuerySpecification = this.QuerySpecificationViewState;
				if (value == currentQuerySpecification)
				{
					return; //do nothing if the new value is the same as the current value
				}

				if (value == null)
				{
					this.ViewState.Remove(ViewStateKey_QuerySpecification);
				}
				else
				{
					this.ViewState[ViewStateKey_QuerySpecification] = value.ToString();
				}

				this.OnQueryChange(new EventArgs());
			}
		}

		/// <summary>
		/// Stores any query conditions that are fixed or immutable except via this property (e.g. shouldn't be modifiable by filter controls, etc.).
		/// </summary>
		/// <remarks>
		/// <para>
		/// Note that, unlike the <see cref="QuerySpecification"/> property, any direct modifications of the value returned by this property WILL NOT be persisted.
		/// </para>
		/// </remarks>
		public IQuerySpecification ImmutableQueryConditions
		{
			get
			{
				string xml = this.ViewState[ViewStateKey_ImmutableQueryConditions] as string;
				return (string.IsNullOrEmpty(xml)) ? new QuerySpecificationWrapper() : QuerySpecificationWrapper.FromXml(xml);
			}
			set
			{
				IQuerySpecification newValue = value;

				newValue = CleanUpImmutableQueryConditions(newValue);

				IQuerySpecification currentImmutableQueryConditions = this.ImmutableQueryConditions;
				if (newValue == currentImmutableQueryConditions)
				{
					return; //do nothing if the new value is the same as the current value
				}

				if (newValue == null)
				{
					this.ViewState.Remove(ViewStateKey_ImmutableQueryConditions);
				}
				else
				{
					this.ViewState[ViewStateKey_ImmutableQueryConditions] = newValue.ToString();
				}

				this.OnImmutableQueryConditionsChange(new EventArgs());
			}
		}

		/// <summary>
		/// Perform "clean up" conversion/standardization of an <see cref="ImmutableQueryConditions"/> value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private static IQuerySpecification CleanUpImmutableQueryConditions(IQuerySpecification value)
		{
			if (value != null)
			{
				if (value.Conditions.Count < 1)
				{
					//there are no conditions, so the value is equivelent to null
					value = null;
				}
				else
				{
					value = value.InnerData.Copy(); //copy the value before modifying it

					//NOTE: The following optimization's correctness is tightly coupled with the implemenation of the QuerySpecification.Merge() method
					//value.Paging.ResetPageIndex(); //value.Paging.Clear();
					////value.SortBy.Clear();
				}
			}
			return value;
		}

		#endregion

		#region Abstract Members

		protected abstract Label ErrorLabel { get; }

		protected abstract GridView Grid { get; }

		/// <summary>
		/// Updates/binds the state of the user interface controls (including GridView, filters, etc.) to match a specified <see cref="IQuerySpecification"/> instance.
		/// </summary>
		/// <param name="querySpecification"></param>
		/// <seealso cref="CreateQueryFromScreen"/>
		protected abstract void BindScreen(IQuerySpecification querySpecification);

		/// <summary>
		/// Creates a new <see cref="IQuerySpecification"/> instance using the curent input/state of the user interface controls.
		/// </summary>
		/// <returns></returns>
		protected abstract IQuerySpecification CreateQueryFromScreen();

		protected abstract void EditItem(int index);

		/// <summary>
		/// Re-populates the ListItems of all this control's ListControls.
		/// </summary>
		/// <remarks>
		/// This method should be called every time the control is Un-bound/Re-bound so that any changes made for a previous DataItem don't persist.
		/// </remarks>
		protected abstract void PopulateListControls();

		/// <summary>
		/// Clears/resets the value or selection of all of this control's DataBound controls that are associated with the "current" DataItem.
		/// </summary>
		protected abstract void ClearDataControls();

		/// <summary>
		/// Implementors should override this method if they want to implement data export support, 
		/// or if they intend to use any of the overloads of this method.
		/// </summary>
		protected virtual void ExportData(IQuerySpecification querySpecification, string exportFormat, string filename)
		{
			//VwMapTaskCollection taskCollection = VwMapTaskController.FetchBySearchCriteria(querySpecification);
			//DataTable dataTable = taskCollection.ToDataTable();

			throw new NotSupportedException("Data export has not been implemented for this control.");
		}

		#endregion

		#region Methods

		#region DataBinding, List Binding, QuerySpecification-related Methods

		/// <summary>
		/// Saves any changes that have been made (e.g. directly to the objects properties or sub-objects) 
		/// and conditionally raises the QueryChanged event (only if a change actually occurred).
		/// </summary>
		public void SaveQuerySpecificationChanges()
		{
			if (!this.HasQuerySpecificationChanged())
			{
				return; //do nothing so that the QueryChanged event DOES NOT get raised
			}

			IQuerySpecification oldValue = this.QuerySpecificationViewState;
			IQuerySpecification newValue = this._CachedQuerySpecification;

			//if either the conditions or the sort order have changed, then auto-reset the page index
			//because it doesn't make sense to go directly to a specific page (except the first) 
			//when the sort order and/or filtering conditions have changed
			if ((oldValue != null) && (!HPFx.Data.Query.QuerySpecification.AreEqualExceptForPaging(oldValue, newValue)))
			{
				newValue.Paging.ResetPageIndex();
			}

			//Save the new instance to the ViewState (i.e. the persistent copy)
			this.QuerySpecificationViewState = newValue;
			this._CachedQuerySpecification = newValue; //just to be safe

			//indicate to any listeners that changes have occurred
			this.OnQueryChange(new EventArgs());
		}

		protected virtual void UnbindList()
		{
			LogManager.Current.Log(Severity.Debug, this, this.ID + ": Entering: BaseListViewUserControl.UnbindList()");
			this.ClearErrorMessage();
			this.PopulateListControls();
			this.ClearDataControls();
		}

		protected override void OnDataBinding(EventArgs e)
		{
			LogManager.Current.Log(Severity.Debug, this, this.ID + ": Entering: BaseListViewUserControl.OnDataBinding(EventArgs e)");

			//NOTE: We must unbind the UI controls before we Re-DataBind
			//Otherwise, we may get exceptions like these in certain situations:
			//    "'ddlRole' has a SelectedValue which is invalid because it does not exist in the list of items. Parameter name: value "
			this.UnbindList();

			base.OnDataBinding(e);
			try
			{
				this.BindScreen(this.CombinedQuerySpecification);
			}
			catch (Exception ex)
			{
				//TODO: Review Needed: Review implementation for correctness
#warning Review Needed: Review implementation for correctness
				throw;

				//string message = "An unexpected error occurred while loading the list data.";
				//LogManager.Current.Log(Severity.Error, this, message, ex);
				////throw;
				//this.SetErrorMessage(message);
				//this.UnbindList();
			}
		}

		protected void OnQueryChange(EventArgs e)
		{
			this.RaiseQueryChanged(e);
		}

		protected void OnImmutableQueryConditionsChange(EventArgs e)
		{
			this.RaiseImmutableQueryConditionsChanged(e);
		}

		protected void OnQueryInputSave(EventArgs e)
		{
			this.QuerySpecification = this.CreateQueryFromScreen();
			this.RaiseQueryInputSaved(e);
		}

		/// <summary>
		/// Saves any screen input to <see cref="QuerySpecification"/>, then refreshes the screen using the results of (re)executing current query.
		/// </summary>
		/// <remarks>
		/// If no screen input has been changed, then invoking this method will refresh the list data using the same query as was previously used.
		/// </remarks>
		public void ApplyScreenInput()
		{
			this.OnQueryInputSave(new EventArgs());
			this.BindScreen(this.CombinedQuerySpecification);
		}

		/// <summary>
		/// Helper method for use by inheritors implementing the <see cref="BindScreen"/> method.
		/// </summary>
		/// <param name="query">The <see cref="IQuerySpecification"/> that was passed as a parameter to <see cref="BindScreen"/>.</param>
		protected void BindScreen_GridPaging(IQuerySpecification query)
		{
			GridView gridView = this.Grid;
			//get the query's Paging.PageSize info
			bool pagingEnabled = (query.Paging.HasValue && (query.Paging.PageSize != int.MaxValue)); //(query.Paging.Enabled && query.Paging.HasValue);
			//bool pagingEnabled = (query.Paging.PageSize != int.MaxValue); //(query.Paging.Enabled && query.Paging.HasValue);
			//gv.AllowPaging = pagingEnabled;
			gridView.PageSize = (!pagingEnabled) ? QueryPaginationSpecification.DefaultPageSize : query.Paging.PageSize ?? QueryPaginationSpecification.DefaultPageSize; //= query.Paging.PageSize ?? QueryPaginationSpecification.DefaultPageSize;

			//update the query's Paging.PageIndex info last (since sorting resets the PageIndex to 0)
			int pageIndex = query.Paging.PageIndex ?? 0;
			if (gridView.PageIndex != pageIndex)
			{
				gridView.PageIndex = pageIndex;
			}
		}

		#endregion

		#region ErrorMessage-related Methods

		/// <summary>
		/// Convenience method.
		/// </summary>
		protected void ClearErrorMessage()
		{
			this.SetErrorMessage(null);
		}

		/// <summary>
		/// Sets the error or warning message that should be displayed by the control to communicate a problem to the end user.
		/// </summary>
		/// <param name="message">The message for the user.</param>
		protected void SetErrorMessage(string message)
		{
			this.ErrorLabel.Text = message ?? "";

			if (!string.IsNullOrEmpty(message))
			{
				WebUtility.ShowAlertBox(this, message);
			}
		}

		/// <summary>
		/// Returns the current error or warning message that is (or will be) displayed by the control.
		/// </summary>
		/// <returns></returns>
		protected string GetErrorMessage()
		{
			return this.ErrorLabel.Text;
		}

		#endregion

		#region Page_LoadHelper Method

		/// <summary>
		/// A standard implementation for the event handler of the <see cref="Control.Load"/> event of the <see cref="BaseListViewUserControl"/>.
		/// </summary>
		/// <param name="e"></param>
		protected void Page_LoadHelper()
		{
			LogManager.Current.Log(Severity.Debug, this, this.ID + ": Entering: BaseListViewUserControl.Page_LoadHelper()");
			if (!this.Page.IsPostBack)
			{
				//this.Grid.PageSize = QueryPaginationSpecification.DefaultPageSize;
			}

			//Dynamically attach the RowCommand event handler
			this.Grid.RowCommand += this.GridView_RowCommand;

			////Dynamically attach the RowDeleting event handler
			//this.Grid.RowDeleting += this.GridView_RowDeleting;

			//Dynamically attach the RowDeleted event handler
			this.Grid.RowDeleted += this.GridView_RowDeleted;

			if (true)
			{
				//Dynamically attach the PageIndexChanging event handler
				this.Grid.PageIndexChanging += this.GridView_PageIndexChanging;
			}
			else
			{
				//Dynamically attach the PageIndexChanged event handler
				this.Grid.PageIndexChanged += this.GridView_PageIndexChanged;
			}

			this.RegisterExportControl("btnExport");
		}

		#endregion

		#region Paging, Sorting, RowCommand-related Methods

		/// <summary>
		/// A standard implementation for the event handler of the <see cref="GridView.RowCommand"/> event of the panel's <see cref="Grid"/>.
		/// </summary>
		/// <param name="e"></param>
		protected void Grid_RowCommandHelper(GridViewCommandEventArgs e, bool throwOnUnhandledCommandName)
		{
			string commandName = e.CommandName;
			switch (commandName)
			{
				case "Sort":
					string newSortExpression = e.CommandArgument.ToString();
					this.OnRowCommand_Sort(newSortExpression);
					this.ReBindScreenAfterSortingPagingChange(false);
					break;
				case "Page":
					string strPage = e.CommandArgument.ToString();
					int? newPageIndex = null;
					switch (strPage)
					{
						case "First":
							newPageIndex = 0;
							break;
						case "Last":
							//TODO: Implement: Real support for "Jump To Last Page" links/buttons
#warning Not Implemented: Real support for "Jump To Last Page" links/buttons
							//throw new NotImplementedException("The invoked code path is not yet implemented.");

							//NOTE: Last-page isn't implemented yet, so for now, increment pageIndex by 10 as a semi-implementation
							newPageIndex = this.Grid.PageIndex + 10;
							//newPageIndex = int.MaxValue;
							break;
						default:
							newPageIndex = strPage.TryParseInt32();
							if (newPageIndex != null)
							{
								newPageIndex--; //Decrement to make it 0-based instead of 1-based
							}
							break;
					}
					this.OnRowCommand_Page(newPageIndex);
					this.ReBindScreenAfterSortingPagingChange(false);
					break;
				case "Edit...":
					this.EditItem(Convert.ToInt32(e.CommandArgument));
					break;
				case "Cancel":
				case "Delete":
					break; //do nothing
				case "Edit":
				case "Insert":
				case "New":
				case "Select":
				case "Update":
					break; //do nothing
				default:
					if (throwOnUnhandledCommandName)
					{
						LogManager.Current.Log(Severity.Warn, this, string.Format("RowCommand '{0}' is not supported.", commandName));
						throw new NotSupportedException();
					}
					//do nothing
					break;
			}
		}

		/// <summary>
		/// Re-DataBinds the UI to the curent (presumably modified) QuerySpecification (so that the UI will be refreshed).
		/// </summary>
		/// <param name="applyScreenInputChangesFirst">
		/// If <c>true</c>, "unsaved" screen input changes (e.g. filter input) will be applied before the UI is re-DataBound; 
		/// else such input will be ignored/un-done.
		/// </param>
		private void ReBindScreenAfterSortingPagingChange(bool applyScreenInputChangesFirst)
		{
			if (applyScreenInputChangesFirst)
			{
				//this is equivelent to clicking the "Filter" button AND re-sorting ("unsaved" filter changes will not be ignored)
				this.ApplyScreenInput();
			}
			else
			{
				//this does re-sorting only ("unsaved" filter input/changes will be ignored)
				this.BindScreen(this.CombinedQuerySpecification);
			}
		}

		private void OnRowCommand_Page(int? newPageIndex)
		{
			if (newPageIndex != null)
			{
				IQuerySpecification combinedQuerySpecification = this.CombinedQuerySpecification;
				int pageSize = combinedQuerySpecification.Paging.PageSize ?? QueryPaginationSpecification.DefaultPageSize;

				IQuerySpecification qs = this.QuerySpecification;

				if (newPageIndex == int.MaxValue)
				{
					//go to the last valid page instead

					//TODO: Implement: Support for the "Last Page" paging link
#warning Not Implemented: Support for the "Last Page" paging link
					throw new NotImplementedException("The invoked code path is not yet implemented.");
				}

				//newPageIndex = newPageIndex == 0 ? null : newPageIndex; //change 0 to null
				qs.Paging.SetPageIndex(pageSize, newPageIndex);
				this.SaveQuerySpecificationChanges(); //this will raise the QueryChanged event (if a change occurred)
			}
		}

		private void OnRowCommand_Sort(string newSortExpression)
		{
			if (!string.IsNullOrEmpty(newSortExpression))
			{
				IQuerySpecification qs = this.QuerySpecification;
				qs.SortBy.PromoteToPrimary(newSortExpression);
				this.SaveQuerySpecificationChanges(); //this will raise the QueryChanged event (if a change occurred)
			}
		}

		#endregion

		#region Export-related methods

		/// <summary>
		/// Registers a specified control as an "Export" control, and configures the page appropriately.
		/// </summary>
		/// <remarks>
		/// It is necessary to call this method for export controls because data export will not work when initiated as part of a partial page PostBack.
		/// See here for details (this describes a related issue, but the cause and solution are the same):
		/// http://blog.encoresystems.net/articles/fileupload-in-an-ajax-updatepanel.aspx
		/// </remarks>
		protected void RegisterExportControl(Control exportControl)
		{
			Global.RegisterAsFullPostBackTrigger(exportControl);
		}

		/// <summary>
		/// Convenience overload.
		/// </summary>
		/// <param name="controlId"></param>
		/// <returns></returns>
		protected bool RegisterExportControl(string controlId)
		{
			Control exportControl = this.FindControl(controlId);
			if (exportControl != null)
			{
				this.RegisterExportControl(exportControl);
				return true;
			}
			return false;
		}

		/// <summary>
		/// A standard implementation of a handler for an "Export Data to File" button.
		/// </summary>
		protected void ExportButton_Click(object sender, EventArgs e)
		{
			this.ExportData("xls", "ExportedData", true);
		}

		/// <summary>
		/// Implementors may override this method if they wish to override any of the default export options 
		/// (e.g. <see cref="ExportDataExtensions.ExportOptions.ExcludedColumns"/>) before the data is actually exported.
		/// </summary>
		protected virtual void ExportData(IEnumerable<DataTable> dataTables, ExportDataExtensions.ExportOptions exportOptions, string mimeType, string filename)
		{
			//exportOptions.ExcludedColumns.AddRange(new[] { "rowstatusid", "rowstatusname" });

			TextWriter writer = WebUtility.TransmitTextFile(this.Response, mimeType, filename);
			writer.Write(dataTables, exportOptions, true, null, null);
			writer.Flush();
			this.Response.End();
		}

		/// <summary>
		/// Convenience method that centralizes the retrieval of the <see cref="IQuerySpecification"/> and the (conditional) overriding of its pagination settings.
		/// </summary>
		/// <param name="exportFormat">One of the predefined data export format values supported by the method.</param>
		/// <param name="filename">The name of the exported file (the correct file extension will be appended automatically).</param>
		/// <param name="withoutPagination">If <c>true</c>, then all matching items (i.e. from all pages) will be exported, regardless of the list's current pagination settings.</param>
		protected void ExportData(string exportFormat, string filename, bool withoutPagination)
		{
			IQuerySpecification querySpecification = this.CombinedQuerySpecification;
			if (withoutPagination)
			{
				querySpecification = querySpecification.InnerData.Copy();
				querySpecification.Paging.SetPageIndex(int.MaxValue, 0);
			}

			this.ExportData(querySpecification, exportFormat, filename);
		}

		/// <summary>
		/// Convenience overload.
		/// </summary>
		protected void ExportData(IQuerySpecification querySpecification, string exportFormat, string filename, DataTable data)
		{
			this.ExportData(querySpecification, exportFormat, filename, new[] { data });
		}

		/// <summary>
		/// Exports the specified <see cref="DataTable"/> using a specified data/file format and filename.
		/// </summary>
		/// <param name="querySpecification">The <see cref="IQuerySpecification"/> that was used to obtain the <paramref name="data"/> being exported.</param>
		/// <param name="exportFormat">One of the predefined data export format values supported by the method.</param>
		/// <param name="filename">The name of the exported file (the correct file extension will be appended automatically).</param>
		/// <param name="data">The data to be exported.</param>
		protected void ExportData(IQuerySpecification querySpecification, string exportFormat, string filename, IEnumerable<DataTable> data)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(filename, "filename");
			//ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(exportFormat, "exportFormat");

			ExportDataExtensions.ExportOptions exportOptions;
			string mimeType = "text/plain";
			string correctFileExtension;

			switch ((exportFormat ?? "").ToLowerInvariant())
			{
				case "xls":
				case "xlsx":
				case "excel":
					exportOptions = ExportDataExtensions.ExportOptions.Excel;
					correctFileExtension = ".csv";
					mimeType = "application/vnd.ms-excel"; //"application/excel";"application/x-excel";"application/x-msexcel";
					break;
				case "csv":
					exportOptions = ExportDataExtensions.ExportOptions.CSV;
					correctFileExtension = ".csv";
					break;
				case "tsv":
				case "tab":
					exportOptions = ExportDataExtensions.ExportOptions.TSV;
					correctFileExtension = ".tsv";
					break;
				case "txt":
				case "text":
				default:
					exportOptions = ExportDataExtensions.ExportOptions.TSV;
					correctFileExtension = ".txt";
					break;
			}

			string fullFileName = filename.EndsWith(correctFileExtension) ? filename : (filename + correctFileExtension);

			Func<string> messageBuilder = () => string.Format("Exporting Data: Filename='{0}'; Query:\n{1}", fullFileName, querySpecification);
			LogManager.Current.Log(Severity.Info, this, messageBuilder);

			this.ExportData(data, exportOptions, mimeType, fullFileName);
		}

		#endregion

		#region Standard Control Event Handlers

		/// <summary>
		/// An event handler that is dynamically added to the <see cref="Grid"/>'s <see cref="GridView.PageIndexChanging"/> event.
		/// </summary>
		/// <remarks>
		/// NOTE: You would typically use either <see cref="GridView_PageIndexChanging"/> or <see cref="GridView_PageIndexChanged"/>, but not both.
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <seealso cref="GridView_PageIndexChanged"/>
		protected virtual void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			int newPageIndex = e.NewPageIndex;
			this.OnRowCommand_Page(newPageIndex);
			this.ReBindScreenAfterSortingPagingChange(false);
		}

		/// <summary>
		/// An event handler that is dynamically added to the <see cref="Grid"/>'s <see cref="GridView.PageIndexChanged"/> event.
		/// </summary>
		/// <remarks>
		/// NOTE: You would typically use either <see cref="GridView_PageIndexChanging"/> or <see cref="GridView_PageIndexChanged"/>, but not both.
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <seealso cref="GridView_PageIndexChanging"/>
		protected void GridView_PageIndexChanged(object sender, EventArgs e)
		{
			int newPageIndex = this.Grid.PageIndex;
			this.OnRowCommand_Page(newPageIndex);
			this.ReBindScreenAfterSortingPagingChange(false);
		}

		/// <summary>
		/// An event handler that is dynamically added to the <see cref="Grid"/>'s <see cref="GridView.RowCommand"/> event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			this.Grid_RowCommandHelper(e, true);
		}

		/// <summary>
		/// An event handler that is dynamically added to the <see cref="Grid"/>'s <see cref="GridView.RowDeleting"/> event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			//do nothing
		}

		/// <summary>
		/// An event handler that is dynamically added to the <see cref="Grid"/>'s <see cref="GridView.RowDeleted"/> event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void GridView_RowDeleted(object sender, GridViewDeletedEventArgs e)
		{
			if (e.Exception == null)
			{
				//this.SetErrorMessage(string.Format("{0} rows were deleted.", e.AffectedRows));
				return;
			}

			//handle the Exception
			if (e.ExceptionHandled)
			{
				return; //the exception was already handled
			}
			else
			{
				Exception ex = e.Exception;

				if (ex is ThreadAbortException)
				{
					//NOTE: we intentionally bypass the normal exception handling logic for ThreadAbortExceptions, because these are almost always the result of invoking Response.Redirect(), and aren't really an error
					return;
				}

				//analyze the exception to determine whether it is "expected" and whether it can be handled or not
				string userFriendlyExceptionMessage = Global.GetUserFriendlyExceptionMessage(ex, true);
				if (string.IsNullOrEmpty(userFriendlyExceptionMessage))
				{
					//even if we can't get a user friendly error message for the exception itself, we'll still handle the error because we can say WHAT failed (even if not WHY).
					userFriendlyExceptionMessage = "";
				}
				string warningMessage = string.Format("Delete failed. {0}", userFriendlyExceptionMessage);
				bool isHandled = !string.IsNullOrEmpty(userFriendlyExceptionMessage);
				if (isHandled)
				{
					this.SetErrorMessage(warningMessage);
					e.ExceptionHandled = true;
				}
				//log the exception (with a low severity since it has already been handled)
				LogManager.Current.Log(isHandled ? Severity.Info : Severity.Warn, this, warningMessage, ex);
			}
		}

		/// <summary>
		/// A generic event handler for a <see cref="DataPager"/> control's <see cref="Control.Init"/> event.
		/// It is primarily intended to be used with <see cref="DataPager"/> controls that use the "StandardDataPager" <see cref="WebControl.SkinID"/>.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DataPager_Init(object sender, EventArgs e)
		{
			DataPager dataPager = sender as DataPager;
			BaseListViewUserControl.InitializeDataPager(dataPager);
		}

		internal static void InitializeDataPager(DataPager dataPager)
		{
			if (dataPager == null)
			{
				return; //do nothing
			}
			//(dataPager.Fields[2] as TemplatePagerField).PagerTemplate = new DataPagerPageStatisticsTemplate();
			dataPager.Fields.Add(new TemplatePagerField { PagerTemplate = new DataPagerPageStatisticsTemplate() });
		}

		#endregion

		#region Misc. Utility Methods

		/// <summary>
		/// Returns the <see cref="DataKey.Value"/> (converted to a Nullable{int}) of the row with the specified rowIndex.
		/// </summary>
		/// <param name="rowIndex"></param>
		/// <returns></returns>
		protected virtual int? GetRowIdInt32(int rowIndex)
		{
			DataKey key = this.Grid.DataKeys[rowIndex];
			return (key == null) ? (int?)null : Convert.ToInt32(key.Value);
		}

		#endregion

		#endregion

	}
}
