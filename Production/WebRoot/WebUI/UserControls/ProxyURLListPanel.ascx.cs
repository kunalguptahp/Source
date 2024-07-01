using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using System.Web;
using System.Web.UI.WebControls;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.WebControlEnhancements;
using HP.HPFx.Web.Utility;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class ProxyURLListPanel : BaseListViewUserControl
	{
		const string NOT_SELECT_MESSAGE = "There is no row selected.";
		const string NOT_SAME_TYPE_MESSAGE = "When using multiple edit, all redirector should have same type.";
		const string ONLY_PUBLISHED_MESSAGE = "To replace redirector URLs, all selected redirectors must be published.";

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

		protected override void BindScreen(IQuerySpecification querySpecification)
		{
			ProxyURLQuerySpecification query = this.ConvertToExpectedType(querySpecification);
			ProxyURLQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);
			GridView gv = this.Grid;

			//Configure the ObjectDataSource using the current QuerySpecification
			Parameter querySpecificationParameter = this.odsDataSource.SelectParameters[0];
			querySpecificationParameter.DefaultValue = query.ToString();

			//read the query's Conditions info
			this.txtIdList.DisableIf(!string.IsNullOrEmpty(immutableConditions.IdListAsString));
			this.txtIdList.Text = query.IdListAsString;
			this.txtDescription.DisableIf(!string.IsNullOrEmpty(immutableConditions.Description));
			this.txtDescription.Text = query.Description;
			this.txtProxyURL.DisableIf(!string.IsNullOrEmpty(immutableConditions.ProxyURL));
			this.txtProxyURL.Text = query.ProxyURL;
			this.txtValidationId.DisableIf(!(immutableConditions.ValidationId == null));
			this.txtValidationId.Text = query.ValidationId.ToString();
			this.txtPublicationId.DisableIf(!(immutableConditions.ProductionId == null));
			this.txtPublicationId.Text = query.ProductionId.ToString();
			this.txtTagsFilter.DisableIf(!string.IsNullOrEmpty(immutableConditions.Tags));
			this.txtTagsFilter.Text = query.Tags;

			this.ddlProxyURLType.DisableIf(immutableConditions.ProxyURLTypeId != null);
			if (query.ProxyURLTypeId == null)
			{
				this.ddlProxyURLType.ClearSelection();
			}
			else
			{
				this.ddlProxyURLType.ForceSelectedValue(query.ProxyURLTypeId);
			}

			this.ddlProxyURLStatus.DisableIf(immutableConditions.ProxyURLStatusId != null);
			if (query.ProxyURLStatusId == null)
			{
				this.ddlProxyURLStatus.ClearSelection();
			}
			else
			{
				this.ddlProxyURLStatus.ForceSelectedValue(query.ProxyURLStatusId);
			}

			this.ddlOwner.DisableIf(immutableConditions.OwnerId != null);
			if (query.OwnerId == null)
			{
				this.ddlOwner.ClearSelection();
			}
			else
			{
				this.ddlOwner.ForceSelectedValue(query.OwnerId);
			}

			this.ddlTouchpoint.DisableIf(immutableConditions.TouchpointParameterValueQueryParameterValueId != null);
			if (query.TouchpointParameterValueQueryParameterValueId == null)
			{
				this.ddlTouchpoint.ClearSelection();
			}
			else
			{
				this.ddlTouchpoint.ForceSelectedValue(query.TouchpointParameterValueQueryParameterValueId);
			}

			this.ddlBrand.DisableIf(immutableConditions.BrandParameterValueQueryParameterValueId != null);
			if (query.BrandParameterValueQueryParameterValueId == null)
			{
				this.ddlBrand.ClearSelection();
			}
			else
			{
				this.ddlBrand.ForceSelectedValue(query.BrandParameterValueQueryParameterValueId);
			}

			this.ddlLocale.DisableIf(immutableConditions.LocaleParameterValueQueryParameterValueId != null);
			if (query.LocaleParameterValueQueryParameterValueId == null)
			{
				this.ddlLocale.ClearSelection();
			}
			else
			{
				this.ddlLocale.ForceSelectedValue(query.LocaleParameterValueQueryParameterValueId);
			}

			this.ddlCycle.DisableIf(immutableConditions.CycleParameterValueQueryParameterValueId != null);
			if (query.CycleParameterValueQueryParameterValueId == null)
			{
				this.ddlCycle.ClearSelection();
			}
			else
			{
				this.ddlCycle.ForceSelectedValue(query.CycleParameterValueQueryParameterValueId);
			}

			this.ddlPlatform.DisableIf(immutableConditions.PlatformParameterValueQueryParameterValueId != null);
			if (query.PlatformParameterValueQueryParameterValueId == null)
			{
				this.ddlPlatform.ClearSelection();
			}
			else
			{
				this.ddlPlatform.ForceSelectedValue(query.PlatformParameterValueQueryParameterValueId);
			}

			this.ddlPartnerCategory.DisableIf(immutableConditions.PartnerCategoryParameterValueQueryParameterValueId != null);
			if (query.PartnerCategoryParameterValueQueryParameterValueId == null)
			{
				this.ddlPartnerCategory.ClearSelection();
			}
			else
			{
				this.ddlPartnerCategory.ForceSelectedValue(query.PartnerCategoryParameterValueQueryParameterValueId);
			}

			this.txtCreatedAfter.DisableIf(immutableConditions.CreatedAfter != null);
			Global.InitializeFilter_DateOnly(this.txtCreatedAfter, query.CreatedAfter);

			this.txtCreatedBefore.DisableIf(immutableConditions.CreatedBefore != null);
			Global.InitializeFilter_DateOnly(this.txtCreatedBefore, query.CreatedBefore);

			this.txtCreatedBy.DisableIf(immutableConditions.CreatedBy != null);
			Global.InitializeFilter_CreatedByFilter(this.txtCreatedBy, query.CreatedBy);

			this.txtModifiedAfter.DisableIf(immutableConditions.ModifiedAfter != null);
			Global.InitializeFilter_DateOnly(this.txtModifiedAfter, query.ModifiedAfter);

			this.txtModifiedBefore.DisableIf(immutableConditions.ModifiedBefore != null);
			Global.InitializeFilter_DateOnly(this.txtModifiedBefore, query.ModifiedBefore);

			this.txtModifiedBy.DisableIf(immutableConditions.ModifiedBy != null);
			Global.InitializeFilter_ModifiedByFilter(this.txtModifiedBy, query.ModifiedBy);

			if (query.Paging.PageSize != null)
			{
				this.ddlItemsPerPage.ForceSelectedValue(query.Paging.PageSize);
			}

			//read the query's Paging.PageSize info
			gv.PageSize = query.Paging.PageSize ?? Convert.ToInt32(this.ddlItemsPerPage.SelectedValue);

			//read the query's Paging.PageIndex info last (since sorting resets the PageIndex to 0)
			int pageIndex = query.Paging.PageIndex ?? 0;
			if (gv.PageIndex != pageIndex)
			{
				gv.PageIndex = pageIndex;
			}
		}

		protected override IQuerySpecification CreateQueryFromScreen()
		{
			ProxyURLQuerySpecification query = new ProxyURLQuerySpecification();
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
			query.Description = this.txtDescription.Text.TrimToNull();
			query.ProxyURL = this.txtProxyURL.Text.TrimToNull();
			query.ValidationId = this.txtValidationId.Text.TryParseInt32();
			query.ProductionId = this.txtPublicationId.Text.TryParseInt32();
			query.Tags = this.txtTagsFilter.Text.TrimToNull();
			query.ProxyURLTypeId = this.ddlProxyURLType.SelectedValue.TryParseInt32();
			query.ProxyURLStatusId = this.ddlProxyURLStatus.SelectedValue.TryParseInt32();
			query.OwnerId = this.ddlOwner.SelectedValue.TryParseInt32();
			query.TouchpointParameterValueQueryParameterValueId = this.ddlTouchpoint.SelectedValue.TryParseInt32();
			query.BrandParameterValueQueryParameterValueId = this.ddlBrand.SelectedValue.TryParseInt32();
			query.LocaleParameterValueQueryParameterValueId = this.ddlLocale.SelectedValue.TryParseInt32();
			query.CycleParameterValueQueryParameterValueId = this.ddlCycle.SelectedValue.TryParseInt32();
			query.PlatformParameterValueQueryParameterValueId = this.ddlPlatform.SelectedValue.TryParseInt32();
			query.PartnerCategoryParameterValueQueryParameterValueId = this.ddlPartnerCategory.SelectedValue.TryParseInt32();

			query.CreatedAfter = Global.ReadFilterValue_DateOnly(this.txtCreatedAfter);
			query.CreatedBefore = Global.ReadFilterValue_DateOnly(this.txtCreatedBefore);
			query.CreatedBy = Global.ReadFilterValue_WindowsIdFromCreatedByFilter(this.txtCreatedBy, true);
			query.ModifiedAfter = Global.ReadFilterValue_DateOnly(this.txtModifiedAfter);
			query.ModifiedBefore = Global.ReadFilterValue_DateOnly(this.txtModifiedBefore);
			query.ModifiedBy = Global.ReadFilterValue_WindowsIdFromModifiedByFilter(this.txtModifiedBy, true);

			query.Paging.PageSize = Convert.ToInt32(this.ddlItemsPerPage.SelectedValue);
			return query;
		}

		protected override void EditItem(int index)
		{
			this.EditProxyURL(this.GetRowIdInt32(index));
		}

		protected override void PopulateListControls()
		{
			Global.BindProxyURLTypeListControl(this.ddlProxyURLType, RowStatus.RowStatusId.Active);
			this.ddlProxyURLType.InsertItem(0, "", Global.GetAllListText());

			Global.BindProxyURLStatusListControl(this.ddlProxyURLStatus, RowStatus.RowStatusId.Active);
			this.ddlProxyURLStatus.InsertItem(0, "", Global.GetAllListText());

			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			this.ddlOwner.InsertItem(0, "", Global.GetAllListText());

			Global.BindQueryParameterValueListControl(this.ddlTouchpoint, (int)QueryParameter.QueryParameterId.Touchpoint, RowStatus.RowStatusId.Active );
			this.ddlTouchpoint.InsertItem(0, "", Global.GetAllListText());

			Global.BindQueryParameterValueListControl(this.ddlBrand, (int)QueryParameter.QueryParameterId.Brand, RowStatus.RowStatusId.Active);
			this.ddlBrand.InsertItem(0, "", Global.GetAllListText());

			Global.BindQueryParameterValueListControl(this.ddlLocale, (int)QueryParameter.QueryParameterId.Locale, RowStatus.RowStatusId.Active);
			this.ddlLocale.InsertItem(0, "", Global.GetAllListText());

			Global.BindQueryParameterValueListControl(this.ddlCycle, (int)QueryParameter.QueryParameterId.Cycle, RowStatus.RowStatusId.Active);
			this.ddlCycle.InsertItem(0, "", Global.GetAllListText());

			Global.BindQueryParameterValueListControl(this.ddlPlatform, (int)QueryParameter.QueryParameterId.Platform, RowStatus.RowStatusId.Active);
			this.ddlPlatform.InsertItem(0, "", Global.GetAllListText());

			Global.BindQueryParameterValueListControl(this.ddlPartnerCategory, (int)QueryParameter.QueryParameterId.PartnerCategory, RowStatus.RowStatusId.Active);
			this.ddlPartnerCategory.InsertItem(0, "", Global.GetAllListText());
		}

		protected override void ClearDataControls()
		{
			this.txtIdList.Text = string.Empty;
			this.txtDescription.Text = string.Empty;
			this.txtProxyURL.Text = string.Empty;
			this.txtTagsFilter.Text = string.Empty;
			this.txtValidationId.Text = string.Empty;
			this.txtPublicationId.Text = string.Empty;
			this.ddlProxyURLType.ClearSelection();
			this.ddlProxyURLStatus.ClearSelection();
			this.ddlOwner.ClearSelection();
			this.ddlTouchpoint.ClearSelection();
			this.ddlBrand.ClearSelection();
			this.ddlLocale.ClearSelection();
			this.ddlCycle.ClearSelection();
			this.ddlPartnerCategory.ClearSelection();
			this.ddlPlatform.ClearSelection();

			this.txtCreatedAfter.Text = string.Empty;
			this.txtCreatedBefore.Text = string.Empty;
			this.txtCreatedBy.Text = string.Empty;
			this.txtModifiedAfter.Text = string.Empty;
			this.txtModifiedBefore.Text = string.Empty;
			this.txtModifiedBy.Text = string.Empty;
		}

		protected override void ExportData(IQuerySpecification querySpecification, string exportFormat, string filename)
		{
			//NOTE: DO NOT invoke the base class' implementation
			//this.ExportData(querySpecification, exportFormat, filename, Global.YieldDataTablesForDataExport<VwMapProxyURL, VwMapProxyURLCollection, ProxyURLQuerySpecification>(querySpecification, VwMapProxyURLController.Fetch));
            ProxyURLQuerySpecification query = this.ConvertToExpectedType(querySpecification);
            int startRowIndex = query.Paging.PagingStartRowIndex ?? 0;
            int totalRowCount = Math.Min(Global.ExportFileMaxRowCount, query.Paging.PageSize ?? int.MaxValue);
            //merge the Export paging settings/threshholds into the Paging settings of (a copy of) the QuerySpecification
            ProxyURLQuerySpecification qs = QuerySpecificationWrapper.CopyAs<ProxyURLQuerySpecification>(query);
            qs.Paging.SetPageIndexByStartRowIndex(totalRowCount, startRowIndex);


            DataTable newDataTable = VwMapProxyURLController.FetchToDataTable(qs);
            
            string fullFileName = filename.EndsWith(".xls") ? filename : (filename + ".xls");
            string strHeaderText = "Redirector List ";
            List<int> num0 = new List<int> { 0 };
            NPOIHelper.ExportByWeb(newDataTable, strHeaderText, fullFileName, num0);

		}

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
		public ProxyURLQuerySpecification ConvertedQuerySpecification
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
            btnExport.Attributes.Add("onclick", "return window.alert('Processing...?')"); 
			if (!SecurityManager.IsCurrentUserInRole(UserRoleId.Editor))
			{
				this.btnCreate.Visible = false;
				this.btnEdit.Visible = false;
				this.btnCopy.Visible = false;
				this.btnMultiCreate.Visible = false;
				this.btnMultiEdit.Visible = false;
				this.btnMultiReplace.Visible = false;

				ButtonField colEditButton = this.gvList.Columns[0] as ButtonField;
				colEditButton.Text = "View...";
			}
		}

		#endregion

		#region ControlEvents

		protected void btnCreate_Click(object sender, EventArgs e)
		{
			this.EditProxyURL(null);
		}

		protected void btnEdit_Click(object sender, EventArgs e)
		{
			List<int> selectedProxyURLIds = this.GetSelectedProxyURLIds();
			if (selectedProxyURLIds.Count == 0)
			{
				WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
			}
			else
			{
				ProxyURLQuerySpecification proxyURLQuerySpecification = new ProxyURLQuerySpecification() { IdList = selectedProxyURLIds };
				var proxyUrls = VwMapProxyURLController.Fetch(proxyURLQuerySpecification);
				int typeId = proxyUrls[0].ProxyURLTypeId;
				foreach (var proxyUrl in proxyUrls)
				{
					if (typeId != proxyUrl.ProxyURLTypeId)
					{
						WebUtility.ShowAlertBox(this, NOT_SAME_TYPE_MESSAGE);
						return;
					}
				}

				this.EditProxyURLs(proxyURLQuerySpecification);
			}
		}

		protected void btnCopy_Click(object sender, EventArgs e)
		{
			List<int> selectedProxyURLIds = this.GetSelectedProxyURLIds();
			if (selectedProxyURLIds.Count == 0)
			{
				WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
			}
			else
			{
				ProxyURLQuerySpecification proxyURLQuerySpecification = new ProxyURLQuerySpecification() { IdList = selectedProxyURLIds };
				this.CopyProxyURLs(proxyURLQuerySpecification);
			}
		}

		protected void btnMultiCreate_Click(object sender, EventArgs e)
		{
			// Id = -1 means create new redirectors
			ProxyURLQuerySpecification proxyURLQuerySpecification = new ProxyURLQuerySpecification() { Id = -1 };
			this.EditMultiEditProxyURLs(proxyURLQuerySpecification);
		}

		protected void btnMultiEdit_Click(object sender, EventArgs e)
		{
			List<int> selectedProxyURLIds = this.GetSelectedProxyURLIds();
			if (selectedProxyURLIds.Count == 0)
			{
				WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
			}
			else
			{
				ProxyURLQuerySpecification proxyURLQuerySpecification = new ProxyURLQuerySpecification() { IdList = selectedProxyURLIds };
				var proxyUrls = VwMapProxyURLController.Fetch(proxyURLQuerySpecification);
				int typeId = proxyUrls[0].ProxyURLTypeId;
				foreach (var proxyUrl in proxyUrls)
				{
					if (typeId != proxyUrl.ProxyURLTypeId)
					{
						WebUtility.ShowAlertBox(this, NOT_SAME_TYPE_MESSAGE);
						return;
					}
				}

				this.EditMultiEditProxyURLs(proxyURLQuerySpecification);
			}
		}

		protected void btnMultiReplace_Click(object sender, EventArgs e)
		{
			List<int> selectedProxyURLIds = this.GetSelectedProxyURLIds();
			if (selectedProxyURLIds.Count == 0)
			{
				WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
			}
			else
			{
				ProxyURLQuerySpecification proxyURLQuerySpecification = new ProxyURLQuerySpecification() { IdList = selectedProxyURLIds };
				var proxyUrls = VwMapProxyURLController.Fetch(proxyURLQuerySpecification);
				foreach (var proxyUrl in proxyUrls)
				{
					if ((int)ProxyURLStateId.Published != proxyUrl.ProxyURLStatusId)
					{
						WebUtility.ShowAlertBox(this, ONLY_PUBLISHED_MESSAGE);
						return;
					}
				}

				this.EditMultiReplaceProxyURLs(proxyURLQuerySpecification);
			}
		}

		protected void btnExport_Click(object sender, EventArgs e)
		{
			const string entityName = "EntityTypes";
			string filename = Global.GenerateStandardExportFilename(entityName, this.CombinedQuerySpecification);
			this.ExportData("xls", filename, true);
		}

		protected void btnFilter_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.Grid.PageIndex = 0;
			this.ApplyScreenInput();
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
				DataKey keys = gvList.DataKeys[e.Row.RowIndex];
				string keyVid = (keys["ValidationId"] != null) ? keys["ValidationId"].ToString() : string.Empty;
				string keyPid = (keys["ProductionId"] != null) ? keys["ProductionId"].ToString() : string.Empty;
				string keyVDomain = (keys["ValidationDomain"] != null) ? keys["ValidationDomain"].ToString() : string.Empty;
				string keyPDomain = (keys["ProductionDomain"] != null) ? keys["ProductionDomain"].ToString() : string.Empty;
				string keyQueryString = (keys["QueryString"] != null) ? keys["QueryString"].ToString() : string.Empty;
				string keyElementsKey = (keys["ProxyURLTypeElementsKey"] != null) ? keys["ProxyURLTypeElementsKey"].ToString() : string.Empty;

				if (!string.IsNullOrEmpty(keyVid))
				{
					HyperLink validationHyperlink = e.Row.Cells[16].FindControl("hlValidationId") as HyperLink;
					validationHyperlink.Text = keyVid;
					validationHyperlink.NavigateUrl = string.Format("{0}{1}{2}?{3}{4}", "http://", keyVDomain, "/svs/rdr", HttpUtility.HtmlDecode(keyQueryString), ((keyQueryString.Length > 0) ? "&" : string.Empty) + "TYPE=" + keyElementsKey);
				}


				if (!string.IsNullOrEmpty(keyPid))
				{
					HyperLink publicationHyperlink = e.Row.Cells[17].FindControl("hlPublicationId") as HyperLink;
					publicationHyperlink.Text = keyPid;
					publicationHyperlink.NavigateUrl = string.Format("{0}{1}{2}?{3}{4}", "http://", keyPDomain, "/svs/rdr", HttpUtility.HtmlDecode(keyQueryString), ((keyQueryString.Length > 0) ? "&" : string.Empty) + "TYPE=" + keyElementsKey);

					HyperLink publicationSourceURLHyperlink = e.Row.Cells[2].FindControl("hlPublicationSourceURL") as HyperLink;
					publicationSourceURLHyperlink.Text = publicationHyperlink.NavigateUrl.ToString();
					publicationSourceURLHyperlink.NavigateUrl = publicationHyperlink.NavigateUrl;
				}
			}
		}

		#endregion

		#region Methods

		#region Helper Methods

		private void EditProxyURL(int? proxyURLId)
		{
			this.Response.Redirect(Global.GetProxyURLUpdatePageUri(proxyURLId, this.QuerySpecification));
		}

		private void EditProxyURLs(ProxyURLQuerySpecification proxyURLQuerySpecification)
		{
			this.Response.Redirect(Global.GetProxyURLEditUpdatePageUri(proxyURLQuerySpecification));
		}

		private void EditMultiEditProxyURLs(ProxyURLQuerySpecification proxyURLQuerySpecification)
		{
			this.Response.Redirect(Global.GetProxyURLMultiEditUpdatePageUri(proxyURLQuerySpecification));
		}

		private void EditMultiReplaceProxyURLs(ProxyURLQuerySpecification proxyURLQuerySpecification)
		{
			this.Response.Redirect(Global.GetProxyURLMultiReplaceUpdatePageUri(proxyURLQuerySpecification));
		}

		private void CopyProxyURLs(ProxyURLQuerySpecification proxyURLQuerySpecification)
		{
			this.Response.Redirect(Global.GetProxyURLCopyPageUri(proxyURLQuerySpecification));
		}
		
		private List<int> GetSelectedProxyURLIds()
		{
			return this.gvList.GetSelectedDataKeyValues<int>();
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public ProxyURLQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as ProxyURLQuerySpecification ?? new ProxyURLQuerySpecification(original);
		}

		#endregion

		#endregion

	}
}