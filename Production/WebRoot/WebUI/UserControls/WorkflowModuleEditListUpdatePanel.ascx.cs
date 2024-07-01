using System;
using System.Data;
using System.Web.UI.WebControls;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using System.Configuration;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.WebControlEnhancements;
using HP.HPFx.Web.Utility;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using HP.HPFx.Diagnostics.Logging;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class WorkflowModuleEditListUpdatePanel : BaseListViewUserControl
	{

        #region Constants

        private const string ViewStateKey_AvailableList = "AvailableList";
        private const string ViewStateKey_SelectionList = "SelectionList";

        #endregion

        #region Properties

        /// <summary>
		///  Available workflow modules
		/// </summary>
		private List<VwMapWorkflowModule> AvailableList
		{
			get
			{
				return this.ViewState[ViewStateKey_AvailableList] as List<VwMapWorkflowModule>;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove(ViewStateKey_AvailableList);
				}
				else
				{
					this.ViewState[ViewStateKey_AvailableList] = value;
				}
			}
		}

		/// <summary>
		///  Selected workflow modules
		/// </summary>
		private List<VwMapWorkflowModule> SelectionList
		{
			get
			{
				return this.ViewState[ViewStateKey_SelectionList] as List<VwMapWorkflowModule>;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove(ViewStateKey_SelectionList);
				}
				else
				{
					this.ViewState[ViewStateKey_SelectionList] = value;
				}
			}
		}

		/// <summary>
		///  visibility of persistence control (Default = false)
		/// </summary>
		public bool PersistenceControlsVisible { get; set; }

		/// <summary>
		///  minimum selection (Default = 1)
		/// </summary>
		public int? MinimumSelection { get ; set; }
        
        #endregion

		#region Overrides

		#region Property Overrides

		protected override Label ErrorLabel
		{
			get { return this.lblError; }
		}

		protected override GridView Grid
		{
			get { return this.gvList; }
		}

		#endregion

		#region Method Overrides

        protected override void EditItem(int index)
        {
            // Not Implmented
        }

		protected override void BindScreen(IQuerySpecification querySpecification)
		{
			WorkflowModuleQuerySpecification query = this.ConvertToExpectedType(querySpecification);
            WorkflowModuleQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);
            GridView gv = this.Grid;

			//Configure the ObjectDataSource using the current QuerySpecification
			Parameter querySpecificationParameter = this.odsDataSource.SelectParameters[0];
			querySpecificationParameter.DefaultValue = query.ToString();

			//read the query's Conditions info
			this.txtIdList.DisableIf(!string.IsNullOrEmpty(immutableConditions.IdListAsString));
			this.txtIdList.Text = query.IdListAsString;
			this.txtName.DisableIf(!string.IsNullOrEmpty(immutableConditions.Name));
			this.txtName.Text = query.Name;
			this.txtVersionMajor.DisableIf(!(immutableConditions.VersionMajor == null));
			this.txtVersionMajor.Text = query.VersionMajor.ToString();
			this.txtVersionMinor.DisableIf(!(immutableConditions.VersionMinor == null));
			this.txtVersionMinor.Text = query.VersionMinor.ToString();
            this.txtFilename.DisableIf(!string.IsNullOrEmpty(immutableConditions.Filename));
            this.txtFilename.Text = query.Filename;

			this.ddlModuleCategory.DisableIf(immutableConditions.ModuleCategoryId != null);
			if (query.ModuleCategoryId == null)
			{
				this.ddlModuleCategory.ClearSelection();
			}
			else
			{
				this.ddlModuleCategory.ForceSelectedValue(query.ModuleCategoryId);
			}

			this.ddlModuleSubCategory.DisableIf(immutableConditions.ModuleSubCategoryId != null);
			if (query.ModuleSubCategoryId == null)
			{
				this.ddlModuleSubCategory.ClearSelection();
			}
			else
			{
				this.ddlModuleSubCategory.ForceSelectedValue(query.ModuleSubCategoryId);
			}

			//read the query's Paging.PageSize info
			gv.PageSize = query.Paging.PageSize ?? QueryPaginationSpecification.DefaultPageSize;

			//read the query's Paging.PageIndex info last (since sorting resets the PageIndex to 0)
			int pageIndex = query.Paging.PageIndex ?? 0;
			if (gv.PageIndex != pageIndex)
			{
				gv.PageIndex = pageIndex;
			}
		}

		protected override IQuerySpecification CreateQueryFromScreen()
		{
			WorkflowModuleQuerySpecification query = new WorkflowModuleQuerySpecification();
			GridView gv = this.Grid;

			//NOTE: DO NOT update the sorting and paging here _from the GridView_, because that is done in the GridView's RowCommand handler
			//NOTE: However, DO preserve the existing sort order (if any) _from the previous query_ to preserve the sort order when the filter conditions are changed
			IQuerySpecification previousQuery = this.QuerySpecification;
			if ((previousQuery != null) && (previousQuery.SortBy != null) && (previousQuery.SortBy.Count > 0))
			{
				query.SortBy.ReplaceAllItems(previousQuery.SortBy); //copy the SortBy info from the previousQuery
			}

			//set the query's Conditions info
			query.IdListAsString = this.txtIdList.Text.TrimToNull();
			query.Name = this.txtName.Text.TrimToNull();
			query.VersionMajor = this.txtVersionMajor.Text.TryParseInt32();
			query.VersionMinor = this.txtVersionMinor.Text.TryParseInt32();
			query.ModuleCategoryId = this.ddlModuleCategory.SelectedValue.TryParseInt32();
			query.ModuleSubCategoryId = this.ddlModuleSubCategory.SelectedValue.TryParseInt32();
			query.Filename = this.txtFilename.Text.TrimToNull();
			return query;
		}

		protected override void PopulateListControls()
		{
			Global.BindWorkflowModuleCategoryListControl(this.ddlModuleCategory, RowStatus.RowStatusId.Active);
			this.ddlModuleCategory.InsertItem(0, "", Global.GetAllListText());

			Global.BindWorkflowModuleSubCategoryListControl(this.ddlModuleSubCategory, RowStatus.RowStatusId.Active);
			this.ddlModuleSubCategory.InsertItem(0, "", Global.GetAllListText());
		}

		protected override void ClearDataControls()
		{
			this.txtIdList.Text = string.Empty;
			this.txtName.Text = string.Empty;
			this.txtVersionMajor.Text = string.Empty;
			this.txtVersionMinor.Text = string.Empty;
			this.txtFilename.Text = string.Empty;

            this.ddlModuleCategory.ClearSelection();
			this.ddlModuleSubCategory.ClearSelection();
		}

        //protected override void ExportData(IQuerySpecification querySpecification, string exportFormat, string filename)
        //{
        //    //NOTE: DO NOT invoke the base class' implementation
        //    this.ExportData(querySpecification, exportFormat, filename, Global.YieldDataTablesForDataExport<VwMapWorkflowModule, VwMapWorkflowModuleCollection, WorkflowModuleQuerySpecification>(querySpecification, VwMapWorkflowModuleController.Fetch));
        //}

        //protected override void ExportData(System.Collections.Generic.IEnumerable<DataTable> dataTables, HP.HPFx.Extensions.Data.Export.ExportDataExtensions.ExportOptions exportOptions, string mimeType, string filename)
        //{
        //    exportOptions.ExcludedColumns.AddRange(new[] { "rowstatusid", "rowstatusname" });
        //    base.ExportData(dataTables, exportOptions, mimeType, filename);
        //}

		#endregion

		#endregion

		#region Properties

		#region Convenience Properties

		/// <summary>
		/// Returns the result of invoking <see cref="ConvertToExpectedType"/> on this instance's <see cref="QuerySpecification"/>.
		/// </summary>
		public WorkflowModuleQuerySpecification ConvertedQuerySpecification
		{
			get
			{
				return this.ConvertToExpectedType(this.QuerySpecification);
			}
		}

		#endregion

		#endregion

		#region PageEvents
        
        protected void Page_Load(object sender, EventArgs e)
		{
            this.Page_LoadHelper();

            //this.pnlPersistanceControls.Visible = this.PersistenceControlsVisible;
            if (!IsPostBack)
            {
                // get the selection list
                InitializeSelectionList();
            }

            // bind the selection list
            gvSelectionList.DataSource = SelectionList;
            gvSelectionList.DataBind();

            // Update available list
            gvList.DataBind();
        }

		#endregion

		#region ControlEvents

		protected void btnFilter_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.Grid.PageIndex = 0;
			this.ApplyScreenInput();
		}

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            List<VwMapWorkflowModule> selectionList = (SelectionList ?? new List<VwMapWorkflowModule>());

            if (e.CommandName == "Insert")
            {
                // get the row index stored in the CommandArgument property
                int index = Convert.ToInt32(e.CommandArgument);

                // get the GridViewRow where the command was raised.
                GridViewRow selectedRow = ((GridView)e.CommandSource).Rows[index];

                // for bound fields, values are stored in the Text property of the Cell [fieldIndex]
                int id = Convert.ToInt32(selectedRow.Cells[1].Text);
                string name = selectedRow.Cells[2].Text;
                string category = selectedRow.Cells[3].Text;
                string subCategory = selectedRow.Cells[4].Text;
                string verMajor = selectedRow.Cells[5].Text;
                string verMinor = selectedRow.Cells[6].Text;
                string filename = selectedRow.Cells[7].Text;
                string description = selectedRow.Cells[8].Text;

                // no duplidate filename allowed.
                bool fileNameSelected = false;
                foreach (VwMapWorkflowModule selectedItem in selectionList)
                {
                    if (selectedItem.Filename == filename)
                    {
                        fileNameSelected = true;
                        break;
                    }
                }

                if (fileNameSelected)
                {
                    this.lblError.Text = "You may not select a module that has a duplicate filename in the same workflow.";
                }
                else
                {
                    VwMapWorkflowModule workflowModule = new VwMapWorkflowModule();
                    workflowModule.Id = id;
                    workflowModule.Name = name;
                    workflowModule.ModuleCategoryName = category;
                    workflowModule.ModuleSubCategoryName = subCategory;
                    workflowModule.VersionMajor = Convert.ToInt32(verMajor);
                    workflowModule.VersionMinor = Convert.ToInt32(verMinor);
                    workflowModule.Filename = filename;
                    workflowModule.Description = description;
                    selectionList.Add(workflowModule);

                    this.lblError.Text = "";
                }

                // Update selection list
                SelectionList = selectionList;
                gvSelectionList.DataSource = selectionList;
                gvSelectionList.DataBind();

                // Update available list.
                gvList.DataBind();
            }
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Note - this event fires when commandname = "Edit"
            e.Cancel = true;
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<VwMapWorkflowModule> selectionList = (SelectionList ?? new List<VwMapWorkflowModule>());
                int moduleId = (int)this.gvList.DataKeys[e.Row.RowIndex].Values[0];

                foreach (VwMapWorkflowModule workflowModule in selectionList)
                {
                    if (workflowModule.Id == moduleId)
                    {
                        e.Row.Enabled = false;
                    }
                }
            }
        }

        protected void gvSelectionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            List<VwMapWorkflowModule> selectionList = (SelectionList ?? new List<VwMapWorkflowModule>());

            // get the row index stored in the CommandArgument property
            int index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "remove")
            {
                selectionList.RemoveAt(index);
            }
            else if (e.CommandName == "up")
            {
                // Don't order if already on first row.
                if (index > 0)
                {
                    VwMapWorkflowModule workflowModuleSave = FindWorkflow(selectionList, index);
                    selectionList.RemoveAt(index);
                    selectionList.Insert(index - 1, workflowModuleSave);
                }
            }
            else if (e.CommandName == "down")
            {
                // index starts from zero.  Don't order if already on last row.
                if ((index + 1) < selectionList.Count)
                {
                    VwMapWorkflowModule workflowModuleSave = FindWorkflow(selectionList, index);
                    selectionList.RemoveAt(index);
                    selectionList.Insert(index + 1, workflowModuleSave);
                }
            }

            // Update selection list
            SelectionList = selectionList;
            gvSelectionList.DataSource = selectionList;
            gvSelectionList.DataBind();

            // Update available list
            gvList.DataBind();
        }

		#endregion

		#region Methods

		#region Helper Methods

        /// <summary>
        ///  get all selected modules
        /// </summary>
        private void InitializeSelectionList()
        {
            int workflowId = this.GetWorkflowIdOfParent() ?? 0;
            if (workflowId == 0)
            {
                SelectionList = null;
                return;
            }

            List<VwMapWorkflowModule> selectionList = new List<VwMapWorkflowModule>();
            VwMapWorkflowWorkflowModuleCollection workflowModuleCollection =
                VwMapWorkflowWorkflowModuleController.FetchByWorkflowId(workflowId);
            workflowModuleCollection.Sort("SortOrder", true);
            foreach (VwMapWorkflowWorkflowModule workflowWorkflowModule in workflowModuleCollection)
            {
                VwMapWorkflowModule workflowModule = new VwMapWorkflowModule();
                workflowModule.Id = workflowWorkflowModule.Id;
                workflowModule.Name = workflowWorkflowModule.Name;
                workflowModule.ModuleCategoryName = workflowWorkflowModule.ModuleCategoryName;
                workflowModule.ModuleSubCategoryName = workflowWorkflowModule.ModuleSubCategoryName;
                workflowModule.VersionMajor = workflowWorkflowModule.VersionMajor;
                workflowModule.VersionMinor = workflowWorkflowModule.VersionMinor;
                workflowModule.Filename = workflowWorkflowModule.Filename;
                workflowModule.Description = workflowWorkflowModule.Description;
                selectionList.Add(workflowModule);
            }
            SelectionList = selectionList;
        }

        /// <summary>
        ///  enable/disable available choices whether selected
        /// </summary>
        protected void UpdateAvailableList()
        {
            List<VwMapWorkflowModule> selectionList = (SelectionList ?? new List<VwMapWorkflowModule>());
            for (int availableIndex = 0; availableIndex < this.gvList.Rows.Count; availableIndex++)
            {
                if (gvList.Rows[availableIndex].RowType == DataControlRowType.DataRow)
                {
                    bool moduleSelected = false;
                    foreach (VwMapWorkflowModule workflowModule in selectionList)
                    {
                        if (Convert.ToInt32(gvList.Rows[availableIndex].Cells[1].Text) == workflowModule.Id)
                        {
                            moduleSelected = true;
                        }
                    }
                    gvList.Rows[availableIndex].Enabled = moduleSelected;
                }
            }
        }

        /// <summary>
        ///  find the selection by gridview index in the selection list
        /// </summary>
        private static VwMapWorkflowModule FindWorkflow(List<VwMapWorkflowModule> selectionList, int index)
        {
            // find the workFlow module to move.
            int selectionIndex = 0;
            VwMapWorkflowModule workflowModuleSave = new VwMapWorkflowModule();
            foreach (VwMapWorkflowModule workflowModule in selectionList)
            {
                if (index == selectionIndex)
                {
                    workflowModuleSave.Id = workflowModule.Id;
                    workflowModuleSave.Name = workflowModule.Name;
                    workflowModuleSave.ModuleCategoryName = workflowModule.ModuleCategoryName;
                    workflowModuleSave.ModuleSubCategoryName = workflowModule.ModuleSubCategoryName;
                    workflowModuleSave.VersionMajor = workflowModule.VersionMajor;
                    workflowModuleSave.VersionMinor = workflowModule.VersionMinor;
                    workflowModuleSave.Filename = workflowModule.Filename;
                    workflowModuleSave.Description = workflowModuleSave.Description;
                }
                selectionIndex++;
            }
            return workflowModuleSave;
        }

        /// <summary>
        ///  save the selections
        /// </summary>
        public void SaveInput(int workflowId)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    // remove existing workflow modules
                    WorkflowWorkflowModule.DestroyByWorkflowId(workflowId);

                    // update selected workflow moduless
                    List<VwMapWorkflowModule> selectionList = (SelectionList ?? new List<VwMapWorkflowModule>());
                    for (int i = 0; i < selectionList.Count; i++)
                    {
                        WorkflowWorkflowModule saveItem = new WorkflowWorkflowModule(true);
                        saveItem.WorkflowId = workflowId;
                        saveItem.WorkflowModuleId = selectionList[i].Id;
                        saveItem.SortOrder = i;
                        saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
                    }

                    // transaction complete
                    scope.Complete();
                }
            }
            catch (SqlException ex)
            {
                LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
                throw;
            }
        }

        /// <summary>
        /// Check minimum number of selections
        /// </summary>
        /// <returns></returns>
        public bool IsMinimumValueSelected()
        {
            // must select the minimum selection
            return ((SelectionList == null ? 0 : SelectionList.Count) >= (this.MinimumSelection ?? 1));
        }

        public int SelectionCount()
        {
            return (SelectionList == null ? 0 : SelectionList.Count);
        }

        private int? GetWorkflowIdOfParent()
        {
            RecordDetailUserControl parentRecordDetailUserControl = Global.GetParentRecordDetailUserControl(this);
            return (parentRecordDetailUserControl == null) ? null : parentRecordDetailUserControl.DataSourceId;
        }

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public WorkflowModuleQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as WorkflowModuleQuerySpecification ?? new WorkflowModuleQuerySpecification(original);
		}

		#endregion

		#endregion

	}
}