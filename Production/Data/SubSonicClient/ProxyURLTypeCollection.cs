using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the ProxyURLType Collection class.
	/// </summary>
	public partial class ProxyURLTypeCollection
	{
		public List<int> GetIds()
		{
			List<int> proxyURLTypes = new List<int>(this.Count);
			foreach (ProxyURLType proxyURLType in this)
			{
				proxyURLTypes.Add(proxyURLType.Id);
			}
			return proxyURLTypes;
		}
	}
}
