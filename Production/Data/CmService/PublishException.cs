using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace HP.ElementsCPS.Data.CmService
{
	public class PublishException : Exception
	{
        public const string PUBLISH_ERROR_RELEASE_DATE = "800";
        public const string PUBLISH_ERROR_DUPLICATE_MATCH_CRITERIA = "801";

		public class Error
		{
			public string ErrorCode { get; set; }
			public string ErrorDesc { get; set; }
			public string ErrorDetail { get; set; }
		}

		private List<Error> _errors;
		public Error[] Errors
		{
			get { return _errors.ToArray(); }
		}

		public PublishException(string msg, Exception inner)
			: base(msg, inner)
		{
			_errors = new List<Error>();
		}

		public PublishException(string msg, XmlNode detail, Exception inner)
			: base(msg, inner)
		{
			_errors = new List<Error>();

			try
			{
				XmlNamespaceManager nsmgr = new XmlNamespaceManager(detail.OwnerDocument.NameTable);
				nsmgr.AddNamespace(@"ns5", @"http://dps.rssx.hp.com/dps/error");
				var errors = detail.SelectNodes("/ns5:DPSGenericFault/error", nsmgr);
				foreach (XmlNode err in errors)
				{
					Error e = new Error();
					e.ErrorCode = err.SelectSingleNode("errorCode").InnerXml;
					e.ErrorDesc = err.SelectSingleNode("errorDesc").InnerXml;
					var d = err.SelectSingleNode("errorDetail");
					if (d != null)
						e.ErrorDetail = d.InnerXml;
					_errors.Add(e);
				}
			}
			catch
			{
				Error e = new Error();
				e.ErrorDesc = "Can not get error detail from server.";
				_errors.Add(e);
			}
		}

		public override string ToString()
		{
			StringBuilder str = new StringBuilder("Error from server:->");
			foreach (Error e in this._errors)
			{
				str.AppendFormat("[Code]:{0}\t[Desc]:{1}\t[Detail]:{2}", e.ErrorCode, e.ErrorDesc, e.ErrorDetail);
				str.AppendLine();
			}

			return str.ToString() + base.ToString();
		}
	}
}
