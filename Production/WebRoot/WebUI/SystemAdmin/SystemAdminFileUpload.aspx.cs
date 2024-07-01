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
using HP.HPFx.Extensions.Text.StringManipulation;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class SystemAdminFileUpload : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Global.RegisterAsFullPostBackTrigger(this.btnViewFileContent);
			Global.RegisterAsFullPostBackTrigger(this.btnUpload);
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

			this.txtOutput.Visible = true;

			//NOTE: verify that the FileUpload control contains a file.
			if (this.fuFile.HasFile)
			{
				HttpPostedFile postedFile = this.fuFile.PostedFile;

				this.lblStatus.Text = string.Format("You uploaded the file named '{0}'. The file's size is {1}. The file's content type is '{2}'.", fuFile.FileName, postedFile.ContentLength, postedFile.ContentType);
				using (StreamReader reader = new StreamReader(postedFile.InputStream))
				{
					this.txtOutput.Text = reader.ReadToEnd();
				}
			}
			else
			{
				// Notify the user that a file was not uploaded.
				this.lblStatus.Text = "You did not specify a file to upload.";
				this.txtOutput.Text = "";
			}
		}

		protected void btnUpload_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.txtOutput.Visible = true;

			//NOTE: verify that the FileUpload control contains a file.
			if (this.fuFile.HasFile)
			{
				try
				{
					HttpPostedFile postedFile = this.fuFile.PostedFile;

					//gather info about the uploaded file
					string fileName = this.fuFile.FileName;
					int contentLength = postedFile.ContentLength;
					string contentType = postedFile.ContentType;
					string destinationAbsolutePathName = this.GetDestinationAbsolutePath();

					//create the destination directory (if needed)
					string destinationDirectoryAbsolutePath = Path.GetDirectoryName(destinationAbsolutePathName);
					if (!Directory.Exists(destinationDirectoryAbsolutePath))
					{
						Directory.CreateDirectory(destinationDirectoryAbsolutePath);
					}

					//save the uploaded file
					postedFile.SaveAs(destinationAbsolutePathName);

					//log success
					string uploadSummary = string.Format("Successfully uploaded the file.\nThe file's original name was {0}.\nThe file was saved as {1}.\nThe file's size was {2}.\nThe file's content type was '{3}'.", fileName, destinationAbsolutePathName, contentLength, contentType);
					LogManager.Current.Log(Severity.Info, this, string.Format("System Admin: File Upload: {0}", uploadSummary));

					//report success
					this.lblStatus.Text = "File upload succeeded.";
					this.txtOutput.Text = uploadSummary;
				}
				catch (Exception ex)
				{
					//log failure
					LogManager.Current.Log(Severity.Info, this, "File upload failed.", ex);

					//report failure
					this.lblStatus.Text = "File upload failed.";
					this.txtOutput.Text = string.Format("The file upload failed.\nException:\n{0}", ex);

					//throw;
				}
			}
			else
			{
				// Notify the user that a file was not uploaded.
				this.lblStatus.Text = "You did not specify a file to upload.";
				this.txtOutput.Text = "";
			}
		}

		#region Path-related Methods

		private string GetDestinationAbsolutePath()
		{
			string originalFilename = this.fuFile.FileName;
			HttpServerUtility httpServerUtility = this.Server;
			string path = this.txtDestinationPath.Text;

			return GetDestinationAbsolutePath(path, httpServerUtility, originalFilename);
		}

		private static string GetDestinationAbsolutePath(string path, HttpServerUtility httpServerUtility, string originalFilename)
		{
			if (string.IsNullOrEmpty(path))
			{
				//construct a default path
				return Path.Combine(GetDefaultDirectory(httpServerUtility), GetDefaultFileName(originalFilename));
			}

			bool isAbsolutePath = !string.IsNullOrEmpty(Path.GetPathRoot(path));
			bool pathIncludesFilename = Path.HasExtension(path);

			if (isAbsolutePath && pathIncludesFilename)
			{
				return path;
			}

			string directoryPath = pathIncludesFilename ? Path.GetDirectoryName(path) : Path.GetFullPath(path);
			if (string.IsNullOrEmpty(directoryPath))
			{
				directoryPath = GetDefaultDirectory(httpServerUtility);
			}

			//convert the directoryPath to absolute
			directoryPath = Path.GetFullPath(directoryPath);
			isAbsolutePath = true;

			//get the filename (i.e. without the path info)
			string fileName = pathIncludesFilename ? Path.GetFileName(path) : GetDefaultFileName(originalFilename);

			//construct the absolute path/filename
			return Path.Combine(directoryPath, fileName);
		}

		private static string GetDefaultDirectory(HttpServerUtility httpServerUtility)
		{
			return httpServerUtility.MapPath("~/App_Data/");
		}

		private static string GetDefaultFileName(string originalFilename)
		{
			bool originalFilenameIsValid = (!string.IsNullOrEmpty(originalFilename)) && Path.HasExtension(originalFilename);
			return originalFilenameIsValid ? Path.GetFileName(originalFilename) : Path.GetRandomFileName();
		}

		#endregion

		protected void cvFileRequired_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = this.fuFile.HasFile;
		}

		protected void cvFileNotEmpty_ServerValidate(object source, ServerValidateEventArgs args)
		{
			if (this.fuFile.HasFile)
			{
				HttpPostedFile postedFile = this.fuFile.PostedFile;
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
