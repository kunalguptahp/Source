using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ElementsCPS.Core.Utility
{
	[Serializable]
	public class JumpstationLog
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string Message { get; set; }
		public string Status { get; set; }

		public JumpstationLog() {}

		public JumpstationLog(int id, string message, string description, string status)
		{
			this.Id = id;
			this.Message = message;
			this.Description = description;
			this.Status = status;
		}
	}
}
