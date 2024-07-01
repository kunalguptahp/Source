using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Web.Utility;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class SystemAdminFileDownload : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Global.RegisterAsFullPostBackTrigger(this.btnViewFileContent);
			Global.RegisterAsFullPostBackTrigger(this.btnDownload);
			if (!this.IsPostBack)
			{
			}
		}

		protected void btnViewFileContent_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.litOutput.Visible = true;

			FileInfo file = this.GetTargetFileInfo();

			//NOTE: verify that the TargetFile exists
			if ((file == null) || (!file.Exists))
			{
				// Notify the user that a file was not downloaded.
				this.lblStatus.Text = "The file does not exist.";
				this.litOutput.Text = "";
				return;
				//throw new InvalidOperationException("The file does not exist.");
			}

			try
			{
				//display the file's content
				using (StreamReader reader = new StreamReader(file.OpenRead()))
				{
					this.litOutput.Text = HttpUtility.HtmlEncode(reader.ReadToEnd());
				}

				//log success
				string downloadSummary = string.Format("Viewed the file '{0}'. The file's size is {1}.", file.FullName, file.Length);
				this.lblStatus.Text = downloadSummary;
				LogManager.Current.Log(Severity.Info, this, string.Format("System Admin: File Download: {0}", downloadSummary));

				//report success
				this.lblStatus.Text = "File download succeeded.";
				//this.litOutput.Text = downloadSummary;
			}
			catch (Exception ex)
			{
				//log failure
				LogManager.Current.Log(Severity.Info, this, "File download failed.", ex);

				//report failure
				this.lblStatus.Text = "File download failed.";
				this.litOutput.Text = string.Format("The file download failed.\nException:\n{0}", ex);

				//throw;
			}
		}

		protected void btnDownload_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.litOutput.Visible = true;

			FileInfo file = this.GetTargetFileInfo();

			//NOTE: verify that the TargetFile exists
			if ((file == null) || (!file.Exists))
			{
				// Notify the user that a file was not downloaded.
				this.lblStatus.Text = "The file does not exist.";
				this.litOutput.Text = "";
				return;
				//throw new InvalidOperationException("The file does not exist.");
			}


			try
			{
				//attach the file to the Response
				//WebUtility.TransmitBinaryFile(this.Response, "application/octet-stream", file.FullName, File.ReadAllBytes(file.FullName));
				Global.TransmitBinaryFile(this.Response, "application/octet-stream", file, new byte[1024 * 1024]);

				//log success
				string downloadSummary = string.Format("Downloaded the file '{0}'. The file's size is {1}.", file.FullName, file.Length);
				LogManager.Current.Log(Severity.Info, this, string.Format("System Admin: File Download: {0}", downloadSummary));

				//NOTE: We must end the response here, or else any content subsequently written to the Response's stream will be appended to the bottom of the binary file sent to the client
				this.Response.End();

				//NOTE: Since we already ended the response, the following code wouldn't be executed anyway, and is therefore commented out
				////report success
				//this.lblStatus.Text = "File download succeeded.";
				//this.litOutput.Text = downloadSummary;
			}
			catch (Exception ex)
			{
				//log failure
				LogManager.Current.Log(Severity.Info, this, "File download failed.", ex);

				//report failure
				this.lblStatus.Text = "File download failed.";
				this.litOutput.Text = string.Format("The file download failed.\nException:\n{0}", ex);

				//throw;
			}
		}

		#region Path-related Methods

		private string GetTargetFilePath()
		{
			string text = this.txtTargetFilePath.Text;
			return (string.IsNullOrEmpty(text)) ? null : Path.GetFullPath(text);
		}

		private FileInfo GetTargetFileInfo()
		{
			return new FileInfo(this.GetTargetFilePath());
		}

		#endregion

		protected void cvFileExists_ServerValidate(object source, ServerValidateEventArgs args)
		{
			string value = this.GetTargetFilePath();
			if (!string.IsNullOrEmpty(value))
			{
				args.IsValid = File.Exists(value);
			}
			else
			{
				//NOTE: A blank value is considered valid by this validator, 
				//because that case is always handled separately by a RequiredFieldValidator (or some equivelent)
				args.IsValid = true;
			}
		}

	}
}
