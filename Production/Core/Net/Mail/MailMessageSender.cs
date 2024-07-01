using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using HP.HPFx.Utility;

namespace HP.ElementsCPS.Core.Net.Mail
{
	/// <summary>
	/// Summary description for MailMessageSender.
	/// </summary>
	public static class MailMessageSender
	{
		/// <summary>
		/// Type initializer. 
		/// </summary>
		static MailMessageSender()
		{
			MailMessageSender.InitConfigSettings();
		}

		private static void InitConfigSettings()
		{
			//string s = ConfigurationManager.AppSettings["ElementsCPS.Core.MailMessageSender.Email_RedirectAllMessagesTo"];
			//if (!string.IsNullOrEmpty(s))
			//{
			//   RedirectAllMessagesTo = s;
			//}
		}

		#region Configuration Properties

		private static string _SmtpClientHost;

		/// <summary>
		/// The hostname of the SMTP server to use to send mail.
		/// </summary>
		/// <remarks>
		/// Since we've switched from using System.Web.Mail to System.Net.Mail, 
		/// this property was added as a replacement for the System.Web.Mail.SmtpMail.SmtpServer property.
		/// <para>
		/// Note that this property will return a default value (based on the system configuration settings) 
		/// when uninitialized (and when set to null).
		/// </para>
		/// </remarks>
		public static string SmtpClientHost
		{
			get
			{
				string s = MailMessageSender._SmtpClientHost;
				if (s == null)
				{
					//get the default server setting from the config file
					s = MailMessageSender.DefaultSmtpServer;
				}
				//if the value is invalid, return a null instead
				if (string.IsNullOrEmpty(s) || (s.Trim() == ""))
				{
					s = null;
				}
				return s;
			}
			set
			{
				if ((string.IsNullOrEmpty(value)) || (value.Trim() == ""))
				{
					value = null;
				}
				//else if (value == MailMessageSender.DefaultSmtpServer)
				//{
				//   value = null;
				//}

				if (MailMessageSender.SmtpClientHost != value)
				{
					MailMessageSender._SmtpClientHost = value;
				}
			}
		}

		public static bool IsSmtpClientHostConfigured
		{
			get
			{
				return !string.IsNullOrEmpty(SmtpClientHost);
			}
		}

		private static string _DefaultEmailFrom;

		private static MailAddress DefaultEmailFrom
		{
			get
			{
				const string CONFIGKEY = "ElementsCPS.Core.MailMessageSender.EmailFrom";
				if (_DefaultEmailFrom == null)
				{
					string s = ConfigurationManager.AppSettings[CONFIGKEY];
					if (string.IsNullOrEmpty(s))
					{
						throw new System.Configuration.ConfigurationErrorsException(
							string.Format("Missing or invalid AppSetting: {0}.", CONFIGKEY));
					}
					_DefaultEmailFrom = s;
				}
				return (string.IsNullOrEmpty(_DefaultEmailFrom)) ? null : new MailAddress(_DefaultEmailFrom);
			}
		}

		private static string _DefaultEmailReplyTo;

		private static MailAddress DefaultEmailReplyTo
		{
			get
			{
				const string CONFIGKEY = "ElementsCPS.Core.MailMessageSender.EmailReplyTo";
				if (_DefaultEmailReplyTo == null)
				{
					string s = ConfigurationManager.AppSettings[CONFIGKEY];
					if (string.IsNullOrEmpty(s))
					{
						throw new System.Configuration.ConfigurationErrorsException(
							string.Format("Missing or invalid AppSetting: {0}.", CONFIGKEY));
					}
					_DefaultEmailReplyTo = s;
				}
				return (string.IsNullOrEmpty(_DefaultEmailReplyTo)) ? null : new MailAddress(_DefaultEmailReplyTo);
			}
		}

		private static string _DefaultSmtpServer;

		private static string DefaultSmtpServer
		{
			get
			{
				const string CONFIGKEY = "ElementsCPS.Core.MailMessageSender.SmtpServer";
				if (_DefaultSmtpServer == null)
				{
					string s = ConfigurationManager.AppSettings[CONFIGKEY];
					if (string.IsNullOrEmpty(s))
					{
						//throw new System.Configuration.ConfigurationErrorsException(
						//   string.Format("Missing or invalid AppSetting: {0}.", CONFIGKEY));
						s = "";
					}
					_DefaultSmtpServer = s;
				}
				return _DefaultSmtpServer;
			}
		}

		private static string _RedirectAllMessagesTo;

		private static string RedirectAllMessagesTo
		{
			get
			{
				const string CONFIGKEY = "ElementsCPS.Core.MailMessageSender.Debug.RedirectAllMessagesTo";
				if (_RedirectAllMessagesTo == null)
				{
					string s = ConfigurationManager.AppSettings[CONFIGKEY];
					if (string.IsNullOrEmpty(s))
					{
						s = "";
						//throw new System.Configuration.ConfigurationErrorsException(
						//   string.Format("Missing or invalid AppSetting: {0}.", CONFIGKEY));
					}
					_RedirectAllMessagesTo = s;
				}
				return _RedirectAllMessagesTo;
			}
		}

		private static string _EmailArchiveBcc;

		private static string EmailArchiveBcc
		{
			get
			{
				const string CONFIGKEY = "ElementsCPS.Core.MailMessageSender.EmailArchiveBcc";
				if (_EmailArchiveBcc == null)
				{
					string s = ConfigurationManager.AppSettings[CONFIGKEY];
					if (string.IsNullOrEmpty(s))
					{
						s = "";
						//throw new System.Configuration.ConfigurationErrorsException(
						//   string.Format("Missing or invalid AppSetting: {0}.", CONFIGKEY));
					}
					_EmailArchiveBcc = s;
				}
				return _EmailArchiveBcc;
			}
		}

		#endregion

		/// <summary>
		/// Creates an instance of SmtpClient based on the application's current configuration settings.
		/// </summary>
		public static SmtpClient CreateSmtpClient()
		{
			string smtpServer = MailMessageSender.SmtpClientHost;
			if (string.IsNullOrEmpty(smtpServer))
			{
				throw new System.InvalidOperationException("MailMessageSender.SmtpServer is invalid.");
			}

			SmtpClient sc = new SmtpClient(smtpServer);
			return sc;
		}

		#region CreateMailMessage Method

		/// <summary>
		/// Convenience method for constructing a MailMessage instance, 
		/// with built-in support for application-specific business logic.
		/// </summary>
		/// <param name="from"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.From"/>.</param>
		/// <returns>A new <see cref="MailMessage"/> instance initialized as specified by the values of the method's parameters.</returns>
		public static System.Net.Mail.MailMessage CreateMailMessage(
			MailAddress from)
		{
			MailAddress replyTo = null; //initialized to null here so that the default value will be used below
			if ((from == null) || (string.IsNullOrEmpty(from.Address)))
			{
				//get the default setting from the config file
				from = MailMessageSender.DefaultEmailFrom;
			}
			if ((replyTo == null) || (string.IsNullOrEmpty(replyTo.Address)))
			{
				//get the default setting from the config file
				replyTo = MailMessageSender.DefaultEmailReplyTo;
			}
			MailMessage message = new MailMessage();
			if ((from != null) && (!string.IsNullOrEmpty(from.Address)))
			{
				message.From = from;
			}
			if ((replyTo != null) && (!string.IsNullOrEmpty(replyTo.Address)))
			{
				message.ReplyTo = replyTo;
			}
			return message;
		}

		/// <summary>
		/// Convenience method for constructing a MailMessage instance, 
		/// with built-in support for application-specific business logic.
		/// </summary>
		/// <param name="from"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.From"/>.</param>
		/// <param name="subject"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.Subject"/>.</param>
		/// <param name="body"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.Body"/>.</param>
		/// <param name="isBodyHtml">Indicates whether the specified <paramref name="body"/> content contains (or will contain if body was unspecified) HTML.</param>
		/// <returns>A new <see cref="MailMessage"/> instance initialized as specified by the values of the method's parameters.</returns>
		public static MailMessage CreateMailMessage(MailAddress from, string subject, string body, bool isBodyHtml)
		{
			MailMessage mail = MailMessageSender.CreateMailMessage(from);
			if (!string.IsNullOrEmpty(subject))
			{
				mail.Subject = subject;
			}
			if (!string.IsNullOrEmpty(body))
			{
				mail.Body = body;
				mail.IsBodyHtml = isBodyHtml;
			}
			return mail;
		}

		/// <summary>
		/// Convenience method for constructing a MailMessage instance, 
		/// with built-in support for application-specific business logic.
		/// </summary>
		/// <param name="from"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.From"/>.</param>
		/// <param name="subject"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.Subject"/>.</param>
		/// <param name="body"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.Body"/>.</param>
		/// <param name="isBodyHtml">Indicates whether the specified <paramref name="body"/> content contains (or will contain if body was unspecified) HTML.</param>
		/// <param name="to"><c>null</c>, or a comma-separated list of valid email addresses that will be used to initialize <see cref="MailMessage.To"/>.</param>
		/// <param name="cc"><c>null</c>, or a comma-separated list of valid email addresses that will be used to initialize <see cref="MailMessage.CC"/>.</param>
		/// <param name="bcc"><c>null</c>, or a comma-separated list of valid email addresses that will be used to initialize <see cref="MailMessage.Bcc"/>.</param>
		/// <returns>A new <see cref="MailMessage"/> instance initialized as specified by the values of the method's parameters.</returns>
		public static MailMessage CreateMailMessage(MailAddress from, string subject, string body, bool isBodyHtml, string to, string cc, string bcc)
		{
			MailMessage mail = MailMessageSender.CreateMailMessage(from, subject, body, isBodyHtml);
			if (!string.IsNullOrEmpty(to))
			{
				mail.To.Add(to);
			}
			if (!string.IsNullOrEmpty(cc))
			{
				mail.CC.Add(cc);
			}
			if (!string.IsNullOrEmpty(bcc))
			{
				mail.Bcc.Add(bcc);
			}
			return mail;
		}

		/// <summary>
		/// Convenience method for constructing a MailMessage instance, 
		/// with built-in support for application-specific business logic.
		/// </summary>
		/// <param name="from"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.From"/>.</param>
		/// <param name="subject"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.Subject"/>.</param>
		/// <param name="body"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.Body"/>.</param>
		/// <param name="isBodyHtml">Indicates whether the specified <paramref name="body"/> content contains (or will contain if body was unspecified) HTML.</param>
		/// <param name="to"><c>null</c>, or a set of valid email addresses that will be used to initialize <see cref="MailMessage.To"/>.</param>
		/// <param name="cc"><c>null</c>, or a set of valid email addresses that will be used to initialize <see cref="MailMessage.CC"/>.</param>
		/// <param name="bcc"><c>null</c>, or a set of valid email addresses that will be used to initialize <see cref="MailMessage.Bcc"/>.</param>
		/// <returns>A new <see cref="MailMessage"/> instance initialized as specified by the values of the method's parameters.</returns>
		public static MailMessage CreateMailMessage(
			MailAddress from,
			string subject,
			string body,
			bool isBodyHtml,
			IEnumerable<string> to,
			IEnumerable<string> cc,
			IEnumerable<string> bcc)
		{
			MailMessage mail = MailMessageSender.CreateMailMessage(
				from,
				subject,
				body,
				isBodyHtml,
				string.Join(",", to.ToArray()),
				string.Join(",", cc.ToArray()),
				string.Join(",", bcc.ToArray())
				);
			return mail;
		}

		/// <summary>
		/// Convenience method for constructing a MailMessage instance, 
		/// with built-in support for application-specific business logic.
		/// </summary>
		/// <param name="from"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.From"/>.</param>
		/// <param name="subject"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.Subject"/>.</param>
		/// <param name="body"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.Body"/>.</param>
		/// <param name="isBodyHtml">Indicates whether the specified <paramref name="body"/> content contains (or will contain if body was unspecified) HTML.</param>
		/// <param name="to"><c>null</c>, or a set of <see cref="MailAddress"/>es that will be used to initialize <see cref="MailMessage.To"/>.</param>
		/// <param name="cc"><c>null</c>, or a set of <see cref="MailAddress"/>es that will be used to initialize <see cref="MailMessage.CC"/>.</param>
		/// <param name="bcc"><c>null</c>, or a set of <see cref="MailAddress"/>es that will be used to initialize <see cref="MailMessage.Bcc"/>.</param>
		/// <returns>A new <see cref="MailMessage"/> instance initialized as specified by the values of the method's parameters.</returns>
		public static MailMessage CreateMailMessage(
			MailAddress from,
			string subject,
			string body,
			bool isBodyHtml,
			IEnumerable<MailAddress> to,
			IEnumerable<MailAddress> cc,
			IEnumerable<MailAddress> bcc)
		{
			MailMessage mail = MailMessageSender.CreateMailMessage(from, subject, body, isBodyHtml);
			if (to != null)
			{
				foreach (MailAddress emailAddress in to)
				{
					mail.To.Add(emailAddress);
				}
			}
			if (cc != null)
			{
				foreach (MailAddress emailAddress in cc)
				{
					mail.CC.Add(emailAddress);
				}
			}
			if (bcc != null)
			{
				foreach (MailAddress emailAddress in bcc)
				{
					mail.Bcc.Add(emailAddress);
				}
			}
			return mail;
		}

		/// <summary>
		/// Convenience method for constructing a MailMessage instance, 
		/// with built-in support for application-specific business logic.
		/// </summary>
		/// <param name="from"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.From"/>.</param>
		/// <param name="subject"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.Subject"/>.</param>
		/// <param name="body"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.Body"/>.</param>
		/// <param name="isBodyHtml">Indicates whether the specified <paramref name="body"/> content contains (or will contain if body was unspecified) HTML.</param>
		/// <param name="to"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.To"/>.</param>
		/// <param name="cc"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.CC"/>.</param>
		/// <param name="bcc"><c>null</c>, or the value that will be used to initialize <see cref="MailMessage.Bcc"/>.</param>
		/// <returns>A new <see cref="MailMessage"/> instance initialized as specified by the values of the method's parameters.</returns>
		[Obsolete("Not yet sure if this overload is useful enough to warrant the added overload complexity it creates.", false)]
		private static MailMessage CreateMailMessage(MailAddress from, string subject, string body, bool isBodyHtml, MailAddress to, MailAddress cc, MailAddress bcc)
		{
			MailMessage mail = MailMessageSender.CreateMailMessage(
				from,
				subject,
				body,
				false,
				((to == null) ? null : new[] { to }),
				((cc == null) ? null : new[] { cc }),
				((bcc == null) ? null : new[] { bcc })
				);
			return mail;
		}

		#endregion

		#region Send Method

		/// <summary>
		/// Sends a specified MailMessage using a specified SmtpClient, 
		/// with built-in support for application-specific business logic.
		/// </summary>
		/// <param name="sc"><c>null</c>, or a valid SmtpClient.</param>
		/// <param name="message">the message to be sent.</param>
		/// <param name="disposeMessage">
		/// If <c>true</c>, <paramref name="message"/> will be disposed of automatically after it is successfully sent.
		/// </param>
		public static void Send(
			System.Net.Mail.SmtpClient sc,
			System.Net.Mail.MailMessage message,
			bool disposeMessage)
		{
			if (message == null)
			{
				throw new System.ArgumentNullException("message");
			}

			PrepareToSend(message);

			if (sc == null)
			{
				sc = MailMessageSender.CreateSmtpClient();
			}

			sc.Send(message);

			if (disposeMessage)
			{
				message.Dispose();
			}
		}

		private static void PrepareToSend(MailMessage message)
		{
			if ((message.From == null) || (string.IsNullOrEmpty(message.From.Address)) || (string.IsNullOrEmpty(message.From.Address.Trim())))
			{
				//attempt to initialize the From field using the default
				MailAddress defaultEmailFrom = MailMessageSender.DefaultEmailFrom;
				if ((defaultEmailFrom != null) && (!string.IsNullOrEmpty(defaultEmailFrom.Address)))
				{
					try
					{
						message.From = defaultEmailFrom;
					}
					catch (System.ArgumentException)
					{
						//ignore
					}
				}

				if ((message.From == null) || (string.IsNullOrEmpty(message.From.Address)) || (string.IsNullOrEmpty(message.From.Address.Trim())))
				{
					throw new System.ArgumentException("Invalid message content: From field.");
				}
			}

			if (!MailMessageSender.HasRecipients(message))
			{
				//NOTE: technically, it would be OK to send an email with no TO if it had a CC and/or BCC
				throw new System.ArgumentException("Invalid message content: Message has no recipients in the To, CC, or BCC fields.");
			}

			string redirectAllMessagesTo = MailMessageSender.RedirectAllMessagesTo;
			if (!string.IsNullOrEmpty(redirectAllMessagesTo))
			{
				//override the recipient fields with the RedirectAllMessagesTo value
				if ((message.To != null) && (message.To.Count > 0))
				{
					message.To.Clear();
					message.To.Add(redirectAllMessagesTo);
				}
				if ((message.CC != null) && (message.CC.Count > 0))
				{
					message.CC.Clear();
					message.CC.Add(redirectAllMessagesTo);
				}
				if ((message.Bcc != null) && (message.Bcc.Count > 0))
				{
					message.Bcc.Clear();
					message.Bcc.Add(redirectAllMessagesTo);
				}
			}

			string emailArchiveBcc = MailMessageSender.EmailArchiveBcc;
			if (!string.IsNullOrEmpty(emailArchiveBcc))
			{
				//Automatically BCC using the EmailArchiveBcc value (in addition to any other recipients that were already included in the message's BCC collection)
				if (message.Bcc != null)
				{
					message.Bcc.Add(emailArchiveBcc);
				}
			}
		}

		#endregion

		#region SendAsync Method

		/// <summary>
		/// Asynchronously sends a specified MailMessage using a specified SmtpClient, 
		/// with built-in support for application-specific business logic.
		/// </summary>
		/// <param name="sc"><c>null</c>, or a valid SmtpClient.</param>
		/// <param name="message">the message to be sent.</param>
		/// <param name="disposeMessage">
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// If <c>true</c>, <paramref name="message"/> will be disposed of automatically after it is successfully sent.
		/// </param>
		[Obsolete("Broken. Doesn't work yet.", true)]
		public static void SendAsync(
			System.Net.Mail.SmtpClient sc,
			System.Net.Mail.MailMessage message,
			bool disposeMessage,
			object userToken)
		{
			//TODO: Implement: Primary code path: The implementation below does not work.
#warning Not Implemented: Primary code path: The implementation below does not work.
			throw new NotImplementedException("The invoked code path is not yet implemented.");

			//if (message == null)
			//{
			//	throw new System.ArgumentNullException("message");
			//}

			//PrepareToSend(message);

			//if (sc == null)
			//{
			//	sc = MailMessageSender.CreateSmtpClient();
			//}

			//sc.SendAsync(message, userToken);

			//if (disposeMessage)
			//{
			//	message.Dispose();
			//}
		}

		#endregion

		#region HasRecipients Methods

		public static bool HasRecipients(MailMessage message)
		{
			return MailMessageSender.HasToRecipients(message) || MailMessageSender.HasCCRecipients(message) || MailMessageSender.HasBccRecipients(message);
		}

		public static bool HasToRecipients(MailMessage message)
		{
			return !((message.To == null) || (message.To.Count == 0) || (string.IsNullOrEmpty(message.To.ToString().Trim())));
		}

		public static bool HasCCRecipients(MailMessage message)
		{
			return !((message.CC == null) || (message.CC.Count == 0) || (string.IsNullOrEmpty(message.CC.ToString().Trim())));
		}

		public static bool HasBccRecipients(MailMessage message)
		{
			return !((message.Bcc == null) || (message.Bcc.Count == 0) || (string.IsNullOrEmpty(message.Bcc.ToString().Trim())));
		}

		#endregion

		#region Other Methods

		/// <summary>
		/// Converts a <see cref="MailMessage"/> instance into an equivelent or representative string format.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public static string Format(MailMessage message)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(message, "message");
			return string.Format("TO:\n{0}\n\nCC:\n{1}\n\nBCC:\n{2}\n\nSUBJECT:\n{3}\n\nBODY:\n{4}",
				message.To.ToString(),
				message.CC.ToString(),
				message.Bcc.ToString(),
				message.Subject,
				message.Body);
		}

		/// <summary>
		/// Returns the number of recipients (whether TO, CC, and/or BCC) configured for a specified <see cref="MailMessage"/> instance.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public static int GetRecipientCount(MailMessage message)
		{
			//ExceptionUtility.ArgumentNullEx_ThrowIfNull(message, "message");
			if (message == null)
			{
				return 0;
			}
			return message.To.Count + message.CC.Count + message.Bcc.Count;
		}

		#endregion

	}
}