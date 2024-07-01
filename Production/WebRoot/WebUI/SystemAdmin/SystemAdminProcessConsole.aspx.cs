using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Web.Utility;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class SystemAdminProcessConsole : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Global.RegisterAsFullPostBackTrigger(this.btnExecute);
			if (!this.IsPostBack)
			{
			}
		}

		protected void btnExecute_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			try
			{
				string processFileAbsolutePath = this.GetProcessFileAbsolutePath();

				//NOTE: verify that the Process file exists
				if ((processFileAbsolutePath == null) || (!File.Exists(processFileAbsolutePath)))
				{
					// Notify the user that the process was not run.
					this.lblStatus.Text = "The process file does not exist.";
					this.litOutput.Text = "";
					this.litErrorOutput.Text = "";
					return;
					//throw new InvalidOperationException("The process file does not exist.");
				}

				//Execute the process
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.UseShellExecute = false; //must be false to redirect the input, output, or error streams
				startInfo.CreateNoWindow = false;
				startInfo.WindowStyle = ProcessWindowStyle.Hidden;
				startInfo.ErrorDialog = false;

				startInfo.RedirectStandardError = true;
				//startInfo.RedirectStandardInput = true;
				startInfo.RedirectStandardOutput = true;
				//startInfo.StandardErrorEncoding = Encoding.Default;
				//startInfo.StandardOutputEncoding = Encoding.Default;

				//startInfo.UserName = "username";
				//startInfo.Password = "password";
				//startInfo.Domain = "domain";

				startInfo.FileName = processFileAbsolutePath; //Path.GetFileName(this.GetProcessFileAbsolutePath());
				//startInfo.WorkingDirectory = Path.GetFullPath(this.GetWorkingFolder()); //Environment.CurrentDirectory;
				string processArguments = (this.txtProcessArguments.Text ?? "").Trim();
				startInfo.Arguments = processArguments;

				using (Process process = Process.Start(startInfo))
				{
					//NOTE: We can't use synchronous "ReadToEnd" on both the StandardOutput and StandardError streams, because it could cause a deadlock (see the <see cref="ProcessStartInfo.RedirectStandardOutput"/> docs for details)
					//Therefore, we use an async read of the stdErr stream, followed by a synchronous read of the stdOut stream
					string stdOutData = null;
					string stdErrData = null;
					//process.OutputDataReceived += (DataReceivedEventHandler)((dataReceivedEventSender, dataReceivedEventArgs) => stdOutData += dataReceivedEventArgs.Data);
					process.ErrorDataReceived += (DataReceivedEventHandler)((dataReceivedEventSender, dataReceivedEventArgs) => stdErrData += dataReceivedEventArgs.Data);
					//process.BeginOutputReadLine();
					process.BeginErrorReadLine();
					stdOutData = process.StandardOutput.ReadToEnd();
					//stdErrOutput = process.StandardError.ReadToEnd();

					process.WaitForExit();

					int processExitCode = process.ExitCode;

					//handle process exit/completion

					//log success
					string processCommandLine = string.Format("{0} {1}", processFileAbsolutePath, processArguments).Trim();
					string processExecutionSummary = string.Format("Executed process '{0}'. The exit code was {1}.", processCommandLine, processExitCode);
					this.lblStatus.Text = processExecutionSummary;
					LogManager.Current.Log(Severity.Info, this, string.Format("System Admin: Process Console: {0}", processExecutionSummary));

					//report success
					this.lblStatus.Text = string.Format("Process exited with exit code {0}.", processExitCode);
					this.litOutput.Text = HttpUtility.HtmlEncode(stdOutData ?? "");
					this.litErrorOutput.Text = HttpUtility.HtmlEncode(stdErrData ?? "");
				}
			}
			catch (Exception ex)
			{
				//log failure
				string message = "Process execution failed.";
				LogManager.Current.Log(Severity.Info, this, message, ex);

				//report failure
				this.lblStatus.Text = message;
				this.litOutput.Text = "";
				this.litErrorOutput.Text = string.Format("Process execution failed.\nException:\n{0}", ex);

				//throw;
			}
		}

		#region Path-related Methods

		private string GetProcessFileAbsolutePath()
		{
			return GetProcessFileAbsolutePath(this.txtProcessFilePath.Text, this.Server);
		}

		private static string GetProcessFileAbsolutePath(string path, HttpServerUtility httpServerUtility)
		{
			if (string.IsNullOrEmpty(path))
			{
				//construct a default path
				return Path.GetFullPath(GetDefaultDirectory(httpServerUtility));
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

				//TODO: Implement: Relative paths: need to try to combine the original and default paths in case the original was a relative path
#warning Not Implemented: Relative paths: need to try to combine the original and default paths in case the original was a relative path
				//throw new NotImplementedException("The invoked code path is not yet implemented.");
			}

			//convert the directoryPath to absolute
			directoryPath = Path.GetFullPath(directoryPath);
			isAbsolutePath = true;

			//get the filename (i.e. without the path info)
			string fileName = pathIncludesFilename ? Path.GetFileName(path) : "";

			//construct the absolute path/filename
			return Path.Combine(directoryPath, fileName);
		}

		private static string GetDefaultDirectory(HttpServerUtility httpServerUtility)
		{
			return httpServerUtility.MapPath("~/");
		}

		//private string GetWorkingFolder()
		//{
		//   string text = this.txtWorkingFolder.Text;
		//   return (string.IsNullOrEmpty(text)) ? null : Path.GetFullPath(text);
		//}

		#endregion

		//protected void cvWorkingFolderExists_ServerValidate(object source, ServerValidateEventArgs args)
		//{
		//   string value = this.GetWorkingFolder();
		//   if (!string.IsNullOrEmpty(value))
		//   {
		//      args.IsValid = Directory.Exists(value);
		//   }
		//   else
		//   {
		//      //NOTE: A blank value is considered valid by this validator, 
		//      //because that case is always handled separately by a RequiredFieldValidator (or some equivelent)
		//      args.IsValid = true;
		//   }
		//}

		protected void cvProcessFileExists_ServerValidate(object source, ServerValidateEventArgs args)
		{
			string value = this.GetProcessFileAbsolutePath();
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
