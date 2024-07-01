using System;
using System.Transactions;
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
	public partial class JumpstationMacroValueUpdatePanel : RecordDetailUserControl
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
            int? jumpstationMacroId = Convert.ToInt32(this.ddlJumpstationMacro.SelectedValue);
            this.DefaultValuesSpecification = new JumpstationMacroValueQuerySpecification { JumpstationMacroId = (jumpstationMacroId ?? -1) };
            this.Response.Redirect(Global.GetJumpstationMacroValueUpdatePageUri(null, this.DefaultValuesSpecification));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }
            this.Delete();

            lblError.Text = string.Format("Macro value deleted.");
            pnlEditArea.Enabled = false;
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
            VwMapJumpstationMacroValueCollection jumpstationMacroValueColl = VwMapJumpstationMacroValueController.FetchByJumpstationMacroIdName(Convert.ToInt32(this.ddlJumpstationMacro.SelectedValue), this.txtMatchName.Text);
            switch (jumpstationMacroValueColl.Count)
            {
                case 0:
                    // unique name
                    args.IsValid = true;
                    break;
                case 1:
                    // unique if updating same macro value
                    args.IsValid = (jumpstationMacroValueColl[0].Id == this.DataSourceId);
                    break;
                default:
                    // duplicates detected (macro names can't be the same)
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
            Global.RebindJumpstationMacroListControl(this.ddlJumpstationMacro, RowStatus.RowStatusId.Active);
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
			this.txtMatchName.Text = string.Empty;
		    this.txtResultValue.Text = string.Empty;
            this.ddlJumpstationMacro.ClearSelection();
			this.ddlStatus.ClearSelection();
		}
		/// <summary>
		/// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
		/// is appropriate (for a new, non-existing record) for each such control.
		/// </summary>
		private void ApplyDataControlDefaultValues()
		{            
            JumpstationMacroValueQuerySpecification defaultValuesSpecification = this.ConvertToExpectedType(this.DefaultValuesSpecification);

            if (defaultValuesSpecification.JumpstationMacroId != null) { this.ddlJumpstationMacro.ForceSelectedValue(defaultValuesSpecification.JumpstationMacroId); }
            if (defaultValuesSpecification.RowStateId != null) { this.ddlStatus.ForceSelectedValue(defaultValuesSpecification.RowStateId); }
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
        public JumpstationMacroValueQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

            return original as JumpstationMacroValueQuerySpecification ?? new JumpstationMacroValueQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

            JumpstationMacroValue bindItem = this.GetDataSource();

            if (this.IsNewRecord)
			{
				this.ApplyDataControlDefaultValues();
                this.btnCreate.Enabled = false;
			    this.btnDelete.Enabled = false;
			}
			else
			{
				try
				{
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
					this.txtMatchName.Text = bindItem.MatchName;
					this.txtResultValue.Text = bindItem.ResultValue;
                    this.ddlJumpstationMacro.ForceSelectedValue(bindItem.JumpstationMacroId);
					this.ddlStatus.ForceSelectedValue((RowStatus.RowStatusId)bindItem.RowStatusId);
                    this.btnCreate.Enabled = true;
				    this.btnDelete.Enabled = true;
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
            JumpstationMacroValue saveItem = this.GetDataSource();
            saveItem.RowStatusId = string.IsNullOrEmpty(this.ddlStatus.SelectedValue) ? (int)RowStatus.RowStatusId.Active : Convert.ToInt32(this.ddlStatus.SelectedValue);
			saveItem.MatchName = this.txtMatchName.Text.TrimToNull();
            saveItem.ResultValue = this.txtResultValue.Text.TrimToNull();
            saveItem.JumpstationMacroId = Convert.ToInt32(this.ddlJumpstationMacro.SelectedValue);
            saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

			//reload the control using the record's (possibly newly assigned) ID
			this.DataSourceId = saveItem.Id;
		}


        /// <summary>
        /// Delete the macro value being edited.
        /// </summary>
        private void Delete()
        {
            using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
            {
                JumpstationMacroValue jumpstationMacroValue = this.GetDataSource();
                jumpstationMacroValue.Delete();
                scope.Complete(); // transaction complete
            }
        }

        protected JumpstationMacroValue GetDataSource()
        {
            JumpstationMacroValue saveItem;
            if (!this.IsNewRecord)
            {
                saveItem = JumpstationMacroValue.FetchByID(this.DataSourceId);
            }
            else
            {
                saveItem = new JumpstationMacroValue(true);
            }
            return saveItem;
        }

		#endregion

	}
}