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
using HP.ElementsCPS.Data.Utility;
using Microsoft.Security.Application;


namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class PersonRegistrationPanel : RecordDetailUserControl
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

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			this.OnInputCancel(new EventArgs());
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
		}

		/// <summary>
		/// Clears/resets the value or selection of all of this control's DataBound controls that are associated with the "current" DataSource.
		/// </summary>
		private void ClearDataControls()
		{
			this.lblNameText.Text = string.Empty;
			this.txtWindowsId.Text = string.Empty;
			this.txtFirstName.Text = string.Empty;
			this.txtMiddleName.Text = string.Empty;
			this.txtLastName.Text = string.Empty;
			this.txtEmail.Text = string.Empty;
		}
		/// <summary>
		/// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
		/// is appropriate (for a new, non-existing record) for each such control.
		/// </summary>
		private void ApplyDataControlDefaultValues()
		{
			PersonQuerySpecification defaultValuesSpecification = this.ConvertToExpectedType(this.DefaultValuesSpecification);

			//this.txtName.Text = defaultValuesSpecification.Name;

			if (SecurityManager.IsCurrentUserRegistered)
			{
				if (!string.IsNullOrEmpty(defaultValuesSpecification.WindowsId)) { this.txtWindowsId.Text = defaultValuesSpecification.WindowsId; }
			}
			else
			{
				//no matching Person record was found for the current user during the Application_AuthenticateRequest phase
				string userWindowsId = SecurityManager.CurrentUserIdentityName;

				if (!string.IsNullOrEmpty(userWindowsId))
				{

					// Search for user information by NTID
					PeopleFinder peopleFinder = new PeopleFinder();
					peopleFinder.Search(SearchType.ByNTID, userWindowsId);
					Profiler profiler = peopleFinder.GetProfiler();
					if (profiler == null)
					{
						//do nothing
						//throw new Exception(string.Format("PeopleFinder search failed. Profiler could not be obtained. WindowsID: {0}.", userWindowsId));
					}
					else
					{
						this.txtFirstName.Text = profiler.FirstName;
						this.txtLastName.Text = profiler.LastName;
						this.txtEmail.Text = profiler.Email;
					}
				}

				this.txtWindowsId.Text = userWindowsId;
				this.txtWindowsId.Enabled = string.IsNullOrEmpty(userWindowsId);
			}
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public PersonQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as PersonQuerySpecification ?? new PersonQuerySpecification(original);
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
					Person bindItem = Person.FetchByID(this.DataSourceId);
					if (bindItem == null)
					{
						this.Visible = false;
						return;
					}
					this.txtWindowsId.Text = bindItem.WindowsId;
					this.lblNameText.Text = bindItem.Name;
					this.txtFirstName.Text = bindItem.FirstName;
					this.txtMiddleName.Text = bindItem.MiddleName;
					this.txtLastName.Text = bindItem.LastName;
					this.txtEmail.Text = bindItem.Email;
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
			Person saveItem;
			if (this.DataSourceId != null)
			{
				saveItem = Person.FetchByID(this.DataSourceId);
			}
			else
			{
				saveItem = new Person(true);
			}

			saveItem.WindowsId = this.txtWindowsId.Text.TrimToNull();
			saveItem.RowStatusId = (int)RowStatus.RowStatusId.Active;
			saveItem.FirstName = this.txtFirstName.Text.TrimToNull();
			saveItem.MiddleName = this.txtMiddleName.Text.TrimToNull();
			saveItem.LastName = this.txtLastName.Text.TrimToNull();
			saveItem.Email = this.txtEmail.Text.TrimToNull();
			saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

			// save roles
			saveItem.SetRoles(new List<UserRoleId> {UserRoleId.Everyone});

			//TODO: Implement: Send "new self-registered user" notification
#warning Not Implemented: Send "new self-registered user" notification
			//throw new NotImplementedException("The invoked code path is not yet implemented.");

			//reload the control using the record's (possibly newly assigned) ID
			this.DataSourceId = saveItem.Id;
		}

		#endregion

	}
}