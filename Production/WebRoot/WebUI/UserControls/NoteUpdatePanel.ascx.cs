using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Data.Query;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using Microsoft.Security.Application;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
    public partial class NoteUpdatePanel : RecordDetailUserControl
    {
        #region Properties

        protected override Label ErrorLabel
        {
            get { return this.lblError; }
        }

		/// <summary>
		/// A strongly-typed shadow of the underlying property.
		/// </summary>
		protected new VwMapNote DataItem
		{
			get
			{
				return base.DataItem as VwMapNote;
			}
		}

        #endregion

        #region PageEvents

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.UpdateChildListImmutableConditions();
            }
        }

        #endregion

        #region ControlEvents

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }

            this.OnInputSave(new EventArgs());
        }
		protected void btnSaveAndDone_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.OnInputSave(new EventArgs());

			this.Response.Redirect(Global.GetNoteDetailPageUri(this.DataItem.Id, null));
		}

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.OnInputCancel(new EventArgs());
        }

        #endregion

        #region Methods

		protected override object LoadDataItem()
		{
			VwMapNote dataItem = null;
			if (this.DataSourceId != null)
			{
				VwMapNoteCollection bindItemCollection = VwMapNoteController.FetchByID(this.DataSourceId);
				dataItem = (bindItemCollection.Count == 0) ? null : bindItemCollection[0];
			}
			return dataItem ?? new VwMapNote();
		}
		
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
            Global.BindRowStatusListControl(this.ddlStatus);

            Global.BindEntityTypeListControl(this.ddlEntityType);
            this.ddlEntityType.InsertItem(0, "", Global.GetSelectListText());

            Global.BindNoteTypeListControl(this.ddlNoteType);
            this.ddlNoteType.InsertItem(0, "", Global.GetNoneListText());

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
            this.ddlStatus.ClearSelection();
            this.txtComment.Text = string.Empty;
            this.ddlEntityType.ClearSelection();
            this.txtEntityId.Text = string.Empty;
            this.ddlNoteType.ClearSelection();

            this.lnkEntityType.NavigateUrl = string.Empty;
            this.lnkEntityType.Visible = false;
            this.lnkNoteType.NavigateUrl = string.Empty;
            this.lnkNoteType.Visible = false;
        }
        /// <summary>
        /// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
        /// is appropriate (for a new, non-existing record) for each such control.
        /// </summary>
        private void ApplyDataControlDefaultValues()
        {
            NoteQuerySpecification defaultValuesSpecification = this.ConvertToExpectedType(this.DefaultValuesSpecification);

            this.txtName.Text = defaultValuesSpecification.Name;

            if (defaultValuesSpecification.RowStateId != null) { this.ddlStatus.ForceSelectedValue(defaultValuesSpecification.RowStateId); }
            if (defaultValuesSpecification.EntityTypeId != null) { this.ddlEntityType.ForceSelectedValue(defaultValuesSpecification.EntityTypeId); }
            if (defaultValuesSpecification.EntityId != null) { this.txtEntityId.Text = defaultValuesSpecification.EntityId.ToString(); }
            if (defaultValuesSpecification.NoteTypeId != null) { this.ddlNoteType.ForceSelectedValue(defaultValuesSpecification.NoteTypeId); }

        }

        /// <summary>
        /// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public NoteQuerySpecification ConvertToExpectedType(IQuerySpecification original)
        {
            if (original == null)
            {
                return null;
            }

            return original as NoteQuerySpecification ?? new NoteQuerySpecification(original);
        }

        protected override void BindItem()
        {
            this.UnbindItem();

            if (this.IsNewRecord)
            { this.ApplyDataControlDefaultValues(); }
            else
            {
                try
                {
                    Note bindItem = Note.FetchByID(this.DataSourceId);
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
                    this.ddlStatus.ForceSelectedValue((RowStatus.RowStatusId)bindItem.RowStatusId);
                    this.txtComment.Text = bindItem.Comment;
                    this.ddlEntityType.ForceSelectedValue(bindItem.EntityTypeId);
                    this.txtEntityId.Text = bindItem.EntityId.ToString();
                    this.ddlNoteType.ForceSelectedValue(bindItem.NoteTypeId);

                    this.lnkEntityType.NavigateUrl = Global.GetEntityTypeUpdatePageUri(bindItem.EntityTypeId, null);
                    this.lnkEntityType.Visible = bindItem.EntityTypeId != null;
                    this.lnkNoteType.NavigateUrl = Global.GetNoteTypeUpdatePageUri(bindItem.NoteTypeId, null);
                    this.lnkNoteType.Visible = bindItem.NoteTypeId != null;

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
            Note saveItem;
            if (this.DataSourceId != null)
            {
                saveItem = Note.FetchByID(this.DataSourceId);
            }
            else
            {
                saveItem = new Note(true);
            }

            saveItem.Name = this.txtName.Text.TrimToNull();
            saveItem.RowStatusId = string.IsNullOrEmpty(this.ddlStatus.SelectedValue) ? (int)RowStatus.RowStatusId.Active : Convert.ToInt32(this.ddlStatus.SelectedValue);
            saveItem.Comment = this.txtComment.Text.TrimToNull();
            saveItem.EntityTypeId = this.ddlEntityType.SelectedValue.TryParseInt32() ?? -1;
            saveItem.EntityId = this.txtEntityId.Text.TryParseInt32() ?? -1;

            saveItem.NoteTypeId = this.ddlNoteType.SelectedValue.TryParseInt32();

            saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

            //reload the control using the record's (possibly newly assigned) ID
            this.DataSourceId = saveItem.Id;
        }

        protected override void OnDataSourceIdChange(EventArgs e)
        {
            base.OnDataSourceIdChange(e);
            this.UpdateChildListImmutableConditions();
        }

        private void UpdateChildListImmutableConditions()
        {
        }

        #endregion
    }
}