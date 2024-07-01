using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Data.Query;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using Microsoft.Security.Application;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class QueryParameterValueUpdatePanel : RecordDetailUserControl
	{
		#region Properties

		protected override Label ErrorLabel
		{
			get { return this.lblError; }
		}

		#endregion

		#region PageEvents

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				//this.UpdateChildListImmutableConditions();
			}
		}

		#endregion

		#region ControlEvents

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }

            this.OnInputSave(new EventArgs());

            this.DataSourceId = null;

            // need to keep the query parameter the same.
            int? queryParameterId = Convert.ToInt32(this.ddlParameter.SelectedValue);
            this.DefaultValuesSpecification = new QueryParameterValueQuerySpecification { QueryParameterId = (queryParameterId ?? -1), Wildcard = true };
            this.Response.Redirect(Global.GetQueryParameterValueUpdatePageUri(null, this.DefaultValuesSpecification));
        }

		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.OnInputSave(new EventArgs());
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			this.OnInputCancel(new EventArgs());
		}

		protected void cvNameUnique_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = false;
			VwMapQueryParameterValueCollection queryParameterValueColl = VwMapQueryParameterValueController.FetchByQueryParameterIdName(Convert.ToInt32(this.ddlParameter.SelectedValue), this.txtName.Text);
			switch (queryParameterValueColl.Count)
			{
				case 0:
					// unique name
					args.IsValid = true;
					break;
				case 1:
					// unique if updating same query parameter value
					args.IsValid = (queryParameterValueColl[0].Id == this.DataSourceId);
					break;
				default:
					// duplicates detected (query parameter names can't be the same)
					args.IsValid = false;
					break;
			}
		}

		#endregion

		#region Methods

		protected override void UnbindItem()
		{
			base.UnbindItem();

			this.PopulateListControls();
			this.ClearDataControls();
		}

		/// <summary>
		/// Re-populates the ListItems of all this control's ListControls.
		/// </summary>
		/// <remarks>
		/// This method should be called every time the control is Un-bound/Re-bound so that any changes made for a previous DataSource don't persist.
		/// </remarks>
		private void PopulateListControls()
		{
			Global.RebindQueryParameterListControl(this.ddlParameter, RowStatus.RowStatusId.Active);
			Global.BindRowStatusListControl(this.ddlStatus);
		}

		/// <summary>
		/// Clears/resets the value or selection of all of this control's DataBound controls that are associated with the "current" DataSource.
		/// </summary>
		private void ClearDataControls()
		{
			this.lblIdValue.Text = string.Empty;
			this.lblCreatedByValue.Text = string.Empty;
			this.lblCreatedOnValue.Text = string.Empty;
			this.lblModifiedByValue.Text = string.Empty;
			this.lblModifiedOnValue.Text = string.Empty;
			this.txtName.Text = string.Empty;
			this.txtDescription.Text = string.Empty;
			this.ddlStatus.ClearSelection();
			this.ddlParameter.ClearSelection();
		}
		/// <summary>
		/// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
		/// is appropriate (for a new, non-existing record) for each such control.
		/// </summary>
		private void ApplyDataControlDefaultValues()
		{
			QueryParameterValueQuerySpecification defaultValuesSpecification = this.ConvertToExpectedType(this.DefaultValuesSpecification);
			this.txtName.Text = defaultValuesSpecification.Name;
			if (defaultValuesSpecification.QueryParameterId != null) { this.ddlParameter.ForceSelectedValue(defaultValuesSpecification.QueryParameterId); }
			if (defaultValuesSpecification.RowStateId != null) { this.ddlStatus.ForceSelectedValue(defaultValuesSpecification.RowStateId); }
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public QueryParameterValueQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as QueryParameterValueQuerySpecification ?? new QueryParameterValueQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			if (this.IsNewRecord)
			{
				this.ApplyDataControlDefaultValues();
			    this.btnCreate.Enabled = false;
			}
			else
			{
				try
				{
					QueryParameterValue bindItem = QueryParameterValue.FetchByID(this.DataSourceId);
					if (bindItem == null)
					{
						this.Visible = false;
						return;
					}
					this.lblIdValue.Text = bindItem.Id.ToString();
					this.lblCreatedByValue.Text = bindItem.CreatedBy;
					this.lblCreatedOnValue.Text = bindItem.CreatedOn.ToString();
					this.lblModifiedByValue.Text = bindItem.ModifiedBy;
					this.lblModifiedOnValue.Text = bindItem.ModifiedOn.ToString();
					this.txtName.Text = bindItem.Name;
					this.ddlParameter.ForceSelectedValue(bindItem.QueryParameterId);
					this.txtDescription.Text = bindItem.Description;
					this.ddlStatus.ForceSelectedValue((RowStatus.RowStatusId)bindItem.RowStatusId);
                    this.btnCreate.Enabled = true;
                }
				catch (SqlException ex)
				{
					LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
					throw;
				}
			}
		}

		protected override void SaveInput()
		{
			QueryParameterValue saveItem;
			if (this.DataSourceId != null)
			{
				saveItem = QueryParameterValue.FetchByID(this.DataSourceId);
			}
			else
			{
				saveItem = new QueryParameterValue(true);
			}

			saveItem.RowStatusId = string.IsNullOrEmpty(this.ddlStatus.SelectedValue) ? (int)RowStatus.RowStatusId.Active : Convert.ToInt32(this.ddlStatus.SelectedValue);
			saveItem.Name = this.txtName.Text.TrimToNull();
			saveItem.QueryParameterId = Convert.ToInt32(this.ddlParameter.SelectedValue);
			saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
			saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

			//reload the control using the record's (possibly newly assigned) ID
			this.DataSourceId = saveItem.Id;
		}

		#endregion

    }
}