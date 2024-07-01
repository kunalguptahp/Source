using System;
using System.Configuration;
using HP.HPFx.Extensions.Text.StringManipulation;

namespace HP.ElementsCPS.Core.Utility
{
	/// <summary>
	/// Contains misc. utility methods.
	/// </summary>
	public static class ElementsCPSCoreUtility
	{

		#region AppSettings Properties

		public static TimeSpan AppCacheAbsoluteExpirationDefault
		{
			get { return ConfigurationManager.AppSettings["HP.ElementsCPS.Cache.AbsoluteExpiration.Default"].TryParseTimeSpan() ?? new TimeSpan(0, 5, 0); }
		}

		public static TimeSpan AppCacheAbsoluteExpirationVeryShort
		{
			get { return ConfigurationManager.AppSettings["HP.ElementsCPS.Cache.AbsoluteExpiration.VeryShort"].TryParseTimeSpan() ?? new TimeSpan(0, 0, 1); }
		}

		public static TimeSpan AppCacheAbsoluteExpirationShort
		{
			get { return ConfigurationManager.AppSettings["HP.ElementsCPS.Cache.AbsoluteExpiration.Short"].TryParseTimeSpan() ?? new TimeSpan(0, 0, 5); }
		}

		public static TimeSpan AppCacheAbsoluteExpirationLong
		{
			get { return ConfigurationManager.AppSettings["HP.ElementsCPS.Cache.AbsoluteExpiration.Long"].TryParseTimeSpan() ?? new TimeSpan(0, 15, 0); }
		}

		#endregion

	}
}