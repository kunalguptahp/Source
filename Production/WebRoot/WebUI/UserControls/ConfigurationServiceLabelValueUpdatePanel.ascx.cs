using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Data.Query;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.ElementsCPS.Data.QuerySpecifications;
using Image=System.Web.UI.WebControls.Image;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class ConfigurationServiceLabelValueUpdatePanel : BaseEditDataUserControl
	{
		#region Constants

		private const string ViewStateKey_ConfigurationServiceGroupTypeId = "ConfigurationServiceGroupTypeId";
        private const string ViewStateKey_ConfigurationServiceApplicationId = "ConfigurationServiceApplicationId";

		#endregion

		#region Properties

        protected override Label ErrorLabel
        {
			get { return this.lblError; }
        }

		public int? ConfigurationServiceGroupTypeId
		{
			get
			{
				return this.ViewState[ViewStateKey_ConfigurationServiceGroupTypeId] as int?;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove(ViewStateKey_ConfigurationServiceGroupTypeId);
				}
				else
				{
					this.ViewState[ViewStateKey_ConfigurationServiceGroupTypeId] = value;
				}
			}
		}

        public int? ConfigurationServiceApplicationId
        {
            get
            {
                return this.ViewState[ViewStateKey_ConfigurationServiceApplicationId] as int?;
            }
            set
            {
                if (value == null)
                {
                    this.ViewState.Remove(ViewStateKey_ConfigurationServiceApplicationId);
                }
                else
                {
                    this.ViewState[ViewStateKey_ConfigurationServiceApplicationId] = value;
                }
            }
        }

		public bool IsNewRecord
		{
			get
			{
				return (this.GetGroupIdOfParent() == null);
			}
		}

        #endregion

        #region PageEvents

		protected void Page_Init(object sender, EventArgs e)
		{
			CreateLabelValueControls();
		}

		#endregion

        #region ControlEvents

        #endregion

        #region Methods

		/// <summary>
		/// Method to simulate a btnSave_Click event since this control has no save button.
		/// </summary>
		internal void SaveUpdates()
		{
			this.OnInputSave(new EventArgs());
		}

		/// <summary>
		/// Method to simulate a btnCancel_Click event since this control has no cancel button.
		/// </summary>
		internal void CancelUpdates()
		{
			this.OnInputSave(new EventArgs());
		}

        protected override void UnbindItem()
        {
            base.UnbindItem();
        }

        protected override void BindItem()
        {
			this.UnbindItem();

			if (!this.IsNewRecord)
			{
				try
				{
					int configurationServiceGroupId = this.GetGroupIdOfParent() ?? 0;
					ConfigurationServiceGroup groupItem = ConfigurationServiceGroup.FetchByID(configurationServiceGroupId);
					if (groupItem != null)
					{
						VwMapConfigurationServiceLabelValueCollection labelValueColl =
							VwMapConfigurationServiceLabelValueController.FetchByConfigurationServiceGroupId(groupItem.Id);
						foreach (VwMapConfigurationServiceLabelValue labelValueItem in labelValueColl)
						{
							switch (labelValueItem.ConfigurationServiceLabelTypeId)
							{
								case (int)ConfigurationServiceLabelTypeId.LabelTypeDropDownList:
									DropDownList ddlControl = this.FindControl(labelValueItem.ConfigurationServiceLabelName + groupItem.ConfigurationServiceGroupTypeId.ToString()) as DropDownList;
									if (ddlControl != null)
									{
										ddlControl.ForceSelectedValue(labelValueItem.ValueX); ;
									}									
									break;
								default:
									// default single and multiple textbox
									TextBox txtBoxControl = this.FindControl(labelValueItem.ConfigurationServiceLabelName + groupItem.ConfigurationServiceGroupTypeId.ToString()) as TextBox;
									if (txtBoxControl != null)
									{
										txtBoxControl.Text = labelValueItem.ValueX;
									}
									break;
							}			
						}
					}
				}
				catch (SqlException ex)
				{
					LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
					throw;
				}
			}
			this.LabelValueControlsToSetVisible();
		}

		private void LabelValueControlsToSetVisible()
		{
			try
			{
				//VwMapConfigurationServiceGroupTypeCollection groupTypeColl = VwMapConfigurationServiceGroupTypeController.FetchByConfigurationServiceApplicationId(ConfigurationServiceApplicationId == null ? 0 : (int)ConfigurationServiceApplicationId);
				VwMapConfigurationServiceGroupTypeCollection groupTypeColl = VwMapConfigurationServiceGroupTypeController.FetchAll();
				foreach (VwMapConfigurationServiceGroupType groupItem in groupTypeColl)
				{
					Table tb = this.FindControl("tbl" + groupItem.Id.ToString()) as Table;
					if (tb != null)
					{
						tb.Visible = (groupItem.Id == (this.ConfigurationServiceGroupTypeId ?? 0)) ? true : false;
					}
				}
			}
			catch(Exception ex)
            {
				string err = ex.Message;
            }
		}

		private void CreateLabelValueControls()
		{
            VwMapConfigurationServiceGroupTypeCollection groupTypeColl = VwMapConfigurationServiceGroupTypeController.FetchAll();
            //VwMapConfigurationServiceGroupTypeCollection groupTypeColl = VwMapConfigurationServiceGroupTypeController.FetchByConfigurationServiceApplicationId(ConfigurationServiceApplicationId == null ? 0 : (int)ConfigurationServiceApplicationId);
			foreach (VwMapConfigurationServiceGroupType groupItem in groupTypeColl)
			{
				Table tblLabelValue = new Table { ID = "tbl" + groupItem.Id.ToString() , BorderWidth = Unit.Pixel(1), Visible = false, BorderStyle = BorderStyle.Solid };
				AddConfigurationServiceLabelTableRowHeader(tblLabelValue);

				VwMapConfigurationServiceLabelConfigurationServiceGroupTypeCollection labelColl =
					VwMapConfigurationServiceLabelConfigurationServiceGroupTypeController.FetchByConfigurationServiceGroupTypeIdOrdered(groupItem.Id);
				if (labelColl != null)
				{
					//labelColl.Sort("ConfigurationServiceItemName", true);
					foreach (VwMapConfigurationServiceLabelConfigurationServiceGroupType labelItem in labelColl)
					{
						AddConfigurationServiceLabelTableRow(tblLabelValue, labelItem);
					}
					this.plcLabelValue.Controls.Add(tblLabelValue);
				}
			}
		}

		private static void AddConfigurationServiceLabelTableRowHeader(Table tbl)
		{
			TableRow tr = new TableRow{};

			Label lblItemName = new Label {Text = "Item"};
			lblItemName.Font.Bold = true;

			TableCell tcItemName = new TableCell {HorizontalAlign = HorizontalAlign.Center};
			tcItemName.Controls.Add(lblItemName);
			tr.Cells.Add(tcItemName);

			Label lblLabelName = new Label {Text = "Label"};
			lblLabelName.Font.Bold = true;

			TableCell tcLabelName = new TableCell {HorizontalAlign = HorizontalAlign.Center};
			tcLabelName.Controls.Add(lblLabelName);
			tr.Cells.Add(tcLabelName);

			Label lblLabelValue = new Label {Text = "Value"};
			lblLabelValue.Font.Bold = true;

			TableCell tcLabelValue = new TableCell {HorizontalAlign = HorizontalAlign.Center};
			tcLabelValue.Controls.Add(lblLabelValue);
			tr.Cells.Add(tcLabelValue);
	
			tbl.Rows.Add(tr);
		}

		private void AddConfigurationServiceLabelTableRow(Table tbl, VwMapConfigurationServiceLabelConfigurationServiceGroupType labelItem)
    	{
    		TableRow tr = new TableRow();

    		Label lblItemName = new Label {Text = labelItem.ConfigurationServiceItemName};
    		TableCell tcItemName = new TableCell();
    		tcItemName.Controls.Add(lblItemName);
    		tr.Cells.Add(tcItemName);

			string labelName = labelItem.Name + ":";
    		Label lblLabelName = new Label {Text = (labelItem.InputRequired ? labelName + " *" : labelName )};
    		TableCell tcLabelName = new TableCell();
    		tcLabelName.Controls.Add(lblLabelName);
    		tr.Cells.Add(tcLabelName);

			Control cntLabelValue;
			switch (labelItem.ConfigurationServiceLabelTypeId)
			{
				case (int)ConfigurationServiceLabelTypeId.LabelTypeTextMultiple:
					cntLabelValue = new TextBox { ID = (labelItem.Name + labelItem.ConfigurationServiceGroupTypeId.ToString()), Width = 500, TextMode = TextBoxMode.MultiLine, Rows = 5};
					break;
				case (int)ConfigurationServiceLabelTypeId.LabelTypeDropDownList:
					cntLabelValue = new DropDownList { ID = (labelItem.Name + labelItem.ConfigurationServiceGroupTypeId.ToString()) };
					DropDownList ddlLoadItems = (DropDownList)cntLabelValue;
					Global.BindCommaDelimitedListControl(ddlLoadItems, labelItem.ValueList);
					ddlLoadItems.InsertItem(0, "", Global.GetSelectListText());
					cntLabelValue = ddlLoadItems;
					break;
				default:
					// default single line textbox
					cntLabelValue = new TextBox { ID = (labelItem.Name + labelItem.ConfigurationServiceGroupTypeId.ToString()), Width = 500, MaxLength = 500 };
					break;
			}			
			TableCell tcLabelValue = new TableCell();
			tcLabelValue.Controls.Add(cntLabelValue);
			tr.Cells.Add(tcLabelValue);

			Image btnLabelHelp = new Image() { ImageUrl = "~//images/help-icon.png", ID = "lblLabelHelp" + labelItem.Name + labelItem.ConfigurationServiceGroupTypeId.ToString()};
			TableCell tcLabelHelp = new TableCell();
			tcLabelHelp.Controls.Add(btnLabelHelp);
			tr.Cells.Add(tcLabelHelp);

			PopupControlExtender popLabelHelp = new PopupControlExtender() 
				{ID = "ajaxLabelHelpPopupControl"  + labelItem.Name + labelItem.ConfigurationServiceGroupTypeId.ToString(), TargetControlID = "lblLabelHelp" + labelItem.Name  + labelItem.ConfigurationServiceGroupTypeId.ToString(), PopupControlID="pnlPopupArea", DynamicControlID="pnlPopupAreaDynamicContent", 
					DynamicContextKey=labelItem.Id.ToString(), DynamicServiceMethod="GetDynamicContentLabelHelp", DynamicServicePath="~/WebMethods.asmx"};
			TableCell tcLabelHelpPopup = new TableCell();
			tcLabelHelpPopup.Controls.Add(popLabelHelp);
			tr.Cells.Add(tcLabelHelpPopup);

			HiddenField hdnLabelId = new HiddenField { ID = ("hdn" + labelItem.Name + labelItem.ConfigurationServiceGroupTypeId.ToString()) };
    		hdnLabelId.Value = labelItem.Id.ToString();
			TableCell tcLabelId = new TableCell();
			tcLabelId.Controls.Add(hdnLabelId);
			tr.Cells.Add(tcLabelId);

			if ((bool)labelItem.InputRequired)
			{
				Label lblInputRequired = new Label { ID = ("lbl" + labelItem.Name + labelItem.ConfigurationServiceGroupTypeId.ToString()), Text = ("Please enter a value."), ForeColor=Color.Red, Visible=false };
				TableCell tcInputRequired = new TableCell();
				tcInputRequired.Controls.Add(lblInputRequired);
				tr.Cells.Add(tcInputRequired);
			}
    		tbl.Rows.Add(tr);
    	}

    	protected override void SaveInput()
        {
			// remove existing label values by configuration service group id
    		int configurationServiceGroupId = this.GetGroupIdOfParent() ?? 0;
			ConfigurationServiceLabelValue.DestroyByConfigurationServiceGroupId(configurationServiceGroupId);
            SaveItemLabelValues(configurationServiceGroupId, false);
		}

        /// <summary>
        /// Validate all required input
        /// </summary>
        /// <returns></returns>
        private void SaveItemLabelValues(int configurationServiceGroupId, bool ignoreEmptyValue)
        {
            ConfigurationServiceGroup groupItem = ConfigurationServiceGroup.FetchByID(configurationServiceGroupId);
            if (groupItem != null)
            {
                VwMapConfigurationServiceLabelConfigurationServiceGroupTypeCollection labelColl =
                    VwMapConfigurationServiceLabelConfigurationServiceGroupTypeController.FetchByConfigurationServiceGroupTypeId(groupItem.ConfigurationServiceGroupTypeId);
                foreach (VwMapConfigurationServiceLabelConfigurationServiceGroupType labelItem in labelColl)
                {
                    HiddenField hdnControl = this.FindControl("hdn" + labelItem.Name + labelItem.ConfigurationServiceGroupTypeId.ToString()) as HiddenField;
                    if (hdnControl != null)
                    {
                        ConfigurationServiceLabelValue saveItem = new ConfigurationServiceLabelValue(true);
                        saveItem.ConfigurationServiceGroupId = groupItem.Id;
                        saveItem.ConfigurationServiceLabelId = (int)hdnControl.Value.TryParseInt32();

                        switch (labelItem.ConfigurationServiceLabelTypeId)
                        {
                            case (int)ConfigurationServiceLabelTypeId.LabelTypeDropDownList:
                                DropDownList ddlControl = this.FindControl(labelItem.Name + labelItem.ConfigurationServiceGroupTypeId.ToString()) as DropDownList;
                                if (ddlControl != null)
                                {
                                    saveItem.ValueX = ddlControl.SelectedValue;
                                }
                                break;
                            default:
                                // default single and mutiple textbox
                                TextBox txtBoxControl = this.FindControl(labelItem.Name + labelItem.ConfigurationServiceGroupTypeId.ToString()) as TextBox;
                                if (txtBoxControl != null)
                                {
                                    saveItem.ValueX = txtBoxControl.Text;
                                }
                                break;
                        }

                        // only save empty or null values if not multi-edit.  
                        if (!(string.IsNullOrEmpty(saveItem.ValueX)) || (ignoreEmptyValue == false))
                        {
                            ConfigurationServiceLabelValue.DestroyByConfigurationServiceGroupIdLabelId(saveItem.ConfigurationServiceGroupId, saveItem.ConfigurationServiceLabelId);
                            saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Multiple Save Input so ignore empty values
        /// </summary>
        /// <returns></returns>
        public void MultipleSaveInput(int configurationServiceGroupId)
        {
            SaveItemLabelValues(configurationServiceGroupId, true);
        }

		private int? GetGroupIdOfParent()
		{
			RecordDetailUserControl parentRecordDetailUserControl = Global.GetParentRecordDetailUserControl(this);
			return (parentRecordDetailUserControl == null) ? null : parentRecordDetailUserControl.DataSourceId;
		}

		#region Helper Methods

		/// <summary>
		/// Validate all required input
		/// </summary>
		/// <returns></returns>
		public Boolean IsValid()
		{
			bool validInput = true;

			VwMapConfigurationServiceLabelConfigurationServiceGroupTypeCollection labelColl =
				VwMapConfigurationServiceLabelConfigurationServiceGroupTypeController.FetchByConfigurationServiceGroupTypeId(this.ConfigurationServiceGroupTypeId ?? 0);
			foreach (VwMapConfigurationServiceLabelConfigurationServiceGroupType labelItem in labelColl)
			{
				if ((bool)labelItem.InputRequired)
					switch (labelItem.ConfigurationServiceLabelTypeId)
					{
						case (int)ConfigurationServiceLabelTypeId.LabelTypeDropDownList:
							DropDownList ddlControl = this.FindControl(labelItem.Name + labelItem.ConfigurationServiceGroupTypeId.ToString()) as DropDownList;
							if (ddlControl != null)
							{
								Label lblInputRequired = this.FindControl("lbl" + labelItem.Name + labelItem.ConfigurationServiceGroupTypeId.ToString()) as Label;
								if (String.IsNullOrEmpty(ddlControl.SelectedValue))
								{
									lblInputRequired.Visible = lblInputRequired != null;
									validInput = false;
								}
								else
								{
									lblInputRequired.Visible = false;
								}
							}
							break;
						default:
							// default single and mutiple textbox
							TextBox txtBoxControl = this.FindControl(labelItem.Name + labelItem.ConfigurationServiceGroupTypeId.ToString()) as TextBox;
							if (txtBoxControl != null)
							{
								Label lblInputRequired = this.FindControl("lbl" + labelItem.Name + labelItem.ConfigurationServiceGroupTypeId.ToString()) as Label;
								if (String.IsNullOrEmpty(txtBoxControl.Text))
								{
									lblInputRequired.Visible = lblInputRequired != null;
									validInput = false;
								}
								else
								{
									lblInputRequired.Visible = false;
								}
							}
							break;
					}
			}
			return validInput;
		}	

		#endregion
			
		#endregion
    }
}