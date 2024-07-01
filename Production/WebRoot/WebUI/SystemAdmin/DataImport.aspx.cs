using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.ElementsCPS.Data.Utility.ImportUtility;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class DataImport : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Global.RegisterAsFullPostBackTrigger(this.btnViewFileContent);
			Global.RegisterAsFullPostBackTrigger(this.btnPreview);
			Global.RegisterAsFullPostBackTrigger(this.btnImport);
			if (!this.IsPostBack)
			{
				//this.ddlEntity.BindToEnum(typeof(EntityTypeIdentifier));
				this.gvFileData.DataBind();
			}
		}

		protected void btnViewFileContent_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.txtFileContent.Visible = true;
			this.gvFileData.Visible = false;

			//NOTE: verify that the FileUpload control contains a file.
			if (this.fuDataFile.HasFile)
			{
				HttpPostedFile postedFile = this.fuDataFile.PostedFile;

				this.lblUploadStatus.Text = string.Format("You uploaded the file named '{0}'. The file's size is {1}. The file's content type is '{2}'.", fuDataFile.FileName, postedFile.ContentLength, postedFile.ContentType);
				using (StreamReader reader = new StreamReader(postedFile.InputStream))
				{
					this.txtFileContent.Text = reader.ReadToEnd();
				}
			}
			else
			{
				// Notify the user that a file was not uploaded.
				this.lblUploadStatus.Text = "You did not specify a file to upload.";
				this.txtFileContent.Text = "";
			}
		}

		protected void btnPreview_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.txtFileContent.Visible = false;
			this.gvFileData.Visible = true;

			//NOTE: verify that the FileUpload control contains a file.
			if (this.fuDataFile.HasFile)
			{
				HttpPostedFile postedFile = this.fuDataFile.PostedFile;

				this.lblUploadStatus.Text = string.Format("You uploaded the file named '{0}'. The file's size is {1}. The file's content type is '{2}'.", fuDataFile.FileName, postedFile.ContentLength, postedFile.ContentType);
				DataSet ds = ElementsCPSDataUtility.ParseDelimitedText(postedFile.InputStream, true, false, true, "CVS");
				this.gvFileData.DataSource = ds.Tables[0];
				this.gvFileData.DataBind();
			}
			else
			{
				// Notify the user that a file was not uploaded.
				this.lblUploadStatus.Text = "You did not specify a file to preview.";
				this.gvFileData.DataSource = null;
				this.gvFileData.DataBind();
			}
		}

		protected void btnImport_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.txtFileContent.Visible = false;
			this.gvFileData.Visible = true;

			//NOTE: verify that the FileUpload control contains a file.
			if (this.fuDataFile.HasFile)
			{
				try
				{
					HttpPostedFile postedFile = this.fuDataFile.PostedFile;

					this.lblUploadStatus.Text = string.Format("Imported the file named '{0}'. The file's size was {1}. The file's content type was '{2}'.", this.fuDataFile.FileName, postedFile.ContentLength, postedFile.ContentType);
					DataSet ds = ElementsCPSDataUtility.ParseDelimitedText(postedFile.InputStream, true, false, true, "CVS");
					DataTable dataTable = ds.Tables[0];
					this.gvFileData.DataSource = dataTable;
					this.gvFileData.DataBind();

					//import the data
					string tableToImportTo = this.ddlEntity.SelectedValue;
					EntityTypeIdentifier? entityTypeId = tableToImportTo.TryParseEnum<EntityTypeIdentifier>(true);
					switch (entityTypeId)
					{
						case EntityTypeIdentifier.ConfigurationServiceApplication:
							new ImportUtility<ConfigurationServiceApplication>() { NewInstanceFactory = () => new ConfigurationServiceApplication(true) }.Import(dataTable.Rows);
							break;
						//case EntityTypeIdentifier.ConfigurationServiceGroup:
						//   new ImportUtility<ConfigurationServiceGroup>() { NewInstanceFactory = () => new ConfigurationServiceGroup(true) }.Import(dataTable.Rows);
						//   break;
						case EntityTypeIdentifier.ConfigurationServiceGroupSelector:
							new ImportUtility<ConfigurationServiceGroupSelector>() { NewInstanceFactory = () => new ConfigurationServiceGroupSelector(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.ConfigurationServiceGroupSelectorQueryParameterValue:
							new ImportUtility<ConfigurationServiceGroupSelectorQueryParameterValue>() { NewInstanceFactory = () => new ConfigurationServiceGroupSelectorQueryParameterValue(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.ConfigurationServiceGroupStatus:
							new ImportUtility<ConfigurationServiceGroupStatus>() { NewInstanceFactory = () => new ConfigurationServiceGroupStatus(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.ConfigurationServiceGroupTag:
							new ImportUtility<ConfigurationServiceGroupTag>() { NewInstanceFactory = () => new ConfigurationServiceGroupTag(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.ConfigurationServiceGroupType:
							new ImportUtility<ConfigurationServiceGroupType>() { NewInstanceFactory = () => new ConfigurationServiceGroupType(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.ConfigurationServiceItem:
							new ImportUtility<ConfigurationServiceItem>() { NewInstanceFactory = () => new ConfigurationServiceItem(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.ConfigurationServiceLabel:
							new ImportUtility<ConfigurationServiceLabel>() { NewInstanceFactory = () => new ConfigurationServiceLabel(true) }.Import(dataTable.Rows);
							break;
                        case EntityTypeIdentifier.JumpstationApplication:
                            new ImportUtility<JumpstationApplication>() { NewInstanceFactory = () => new JumpstationApplication(true) }.Import(dataTable.Rows);
                            break;
                        case EntityTypeIdentifier.JumpstationGroup:
                            new ImportUtility<JumpstationGroup>() { NewInstanceFactory = () => new JumpstationGroup(true) }.Import(dataTable.Rows);
                            break;
                        case EntityTypeIdentifier.JumpstationGroupSelector:
                            new ImportUtility<JumpstationGroupSelector>() { NewInstanceFactory = () => new JumpstationGroupSelector(true) }.Import(dataTable.Rows);
                            break;
                        case EntityTypeIdentifier.JumpstationGroupSelectorQueryParameterValue:
                            new ImportUtility<JumpstationGroupSelectorQueryParameterValue>() { NewInstanceFactory = () => new JumpstationGroupSelectorQueryParameterValue(true) }.Import(dataTable.Rows);
                            break;
                        case EntityTypeIdentifier.JumpstationGroupStatus:
                            new ImportUtility<JumpstationGroupStatus>() { NewInstanceFactory = () => new JumpstationGroupStatus(true) }.Import(dataTable.Rows);
                            break;
                        case EntityTypeIdentifier.JumpstationGroupTag:
                            new ImportUtility<JumpstationGroupTag>() { NewInstanceFactory = () => new JumpstationGroupTag(true) }.Import(dataTable.Rows);
                            break;
                        case EntityTypeIdentifier.JumpstationGroupType:
                            new ImportUtility<JumpstationGroupType>() { NewInstanceFactory = () => new JumpstationGroupType(true) }.Import(dataTable.Rows);
                            break;
                        case EntityTypeIdentifier.JumpstationMacro:
                            new ImportUtility<JumpstationMacro>() { NewInstanceFactory = () => new JumpstationMacro(true) }.Import(dataTable.Rows);
                            break;
						case EntityTypeIdentifier.EntityType:
							new ImportUtility<EntityType>() { NewInstanceFactory = () => new EntityType(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.Log:
							new ImportUtility<Log>() { NewInstanceFactory = () => new Log(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.Note:
							new ImportUtility<Note>() { NewInstanceFactory = () => new Note(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.NoteType:
							new ImportUtility<NoteType>() { NewInstanceFactory = () => new NoteType(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.Person:
							new ImportUtility<Person>() { NewInstanceFactory = () => new Person(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.PersonRole:
							new ImportUtility<PersonRole>() { NewInstanceFactory = () => new PersonRole(true) }.Import(dataTable.Rows);
							break;
						//case EntityTypeIdentifier.ProxyURL:
						//   new ImportUtility<ProxyURL>() { NewInstanceFactory = () => new ProxyURL(true) }.Import(dataTable.Rows);
						//   break;
						case EntityTypeIdentifier.ProxyURLDomain:
							new ImportUtility<ProxyURLDomain>() { NewInstanceFactory = () => new ProxyURLDomain(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.ProxyURLGroupType:
							new ImportUtility<ProxyURLGroupType>() { NewInstanceFactory = () => new ProxyURLGroupType(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.ProxyURLQueryParameterValue:
							new ImportUtility<ProxyURLQueryParameterValue>() { NewInstanceFactory = () => new ProxyURLQueryParameterValue(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.ProxyURLStatus:
							new ImportUtility<ProxyURLStatus>() { NewInstanceFactory = () => new ProxyURLStatus(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.ProxyURLTag:
							new ImportUtility<ProxyURLTag>() { NewInstanceFactory = () => new ProxyURLTag(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.ProxyURLType:
							new ImportUtility<ProxyURLType>() { NewInstanceFactory = () => new ProxyURLType(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.PublishTemp:
							new ImportUtility<PublishTemp>() { NewInstanceFactory = () => new PublishTemp(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.QueryParameter:
							new ImportUtility<QueryParameter>() { NewInstanceFactory = () => new QueryParameter(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.QueryParameterConfigurationServiceGroupType:
							new ImportUtility<QueryParameterConfigurationServiceGroupType>() { NewInstanceFactory = () => new QueryParameterConfigurationServiceGroupType(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.QueryParameterProxyURLType:
							new ImportUtility<QueryParameterProxyURLType>() { NewInstanceFactory = () => new QueryParameterProxyURLType(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.QueryParameterValue:
							new ImportUtility<QueryParameterValue>() { NewInstanceFactory = () => new QueryParameterValue(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.Role:
							new ImportUtility<Role>() { NewInstanceFactory = () => new Role(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.RowStatus:
							new ImportUtility<RowStatus>() { NewInstanceFactory = () => new RowStatus(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.Tag:
							new ImportUtility<Tag>() { NewInstanceFactory = () => new Tag(true) }.Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.ConfigurationServiceGroup:
							new ImportUtilityForStatefulEntity<ConfigurationServiceGroup>(ConfigurationServiceGroup.ConfigurationServiceGroupStatusIdColumn.ColumnName).Import(dataTable.Rows);
							break;
						case EntityTypeIdentifier.ProxyURL:
							new ImportUtilityForStatefulEntity<ProxyURL>(ProxyURL.ProxyURLStatusIdColumn.ColumnName).Import(dataTable.Rows);
							break;
						default:
							string message = string.Format("Data import is not yet supported for: {0}.", tableToImportTo);
							this.lblUploadStatus.Text = message;
							//throw new NotSupportedException(message);
							break;
					}
				}
				catch (Exception ex)
				{
					LogManager.Current.Log(Severity.Info, this, "Data import failed.", ex);

					this.lblUploadStatus.Text = string.Format("The data import failed. Exception: {0}. Message: {1}.", ex.GetType().FullName, ex.Message);
					this.gvFileData.DataSource = null;
					this.gvFileData.DataBind();

					//throw;
				}
			}
			else
			{
				// Notify the user that a file was not uploaded.
				this.lblUploadStatus.Text = "You did not specify a file to import.";
				this.gvFileData.DataSource = null;
				this.gvFileData.DataBind();
			}
		}

		protected void cvDataFileRequired_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = this.fuDataFile.HasFile;
		}

		protected void cvDataFileValid_ServerValidate(object source, ServerValidateEventArgs args)
		{
			if (this.fuDataFile.HasFile)
			{
				HttpPostedFile postedFile = this.fuDataFile.PostedFile;
				bool isEmptyFile = (postedFile.ContentLength <= 0);
				args.IsValid = !isEmptyFile;
			}
			else
			{
				//NOTE: Not uploading any file is considered valid by this validator, 
				//because that case is always handled separately by a RequiredFieldValidator (or some equivelent)
				args.IsValid = true;
			}
		}

	}
}
