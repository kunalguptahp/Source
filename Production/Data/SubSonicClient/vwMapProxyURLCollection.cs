using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapProxyURLCollection class.
	/// </summary>
	public partial class VwMapProxyURLCollection
	{

		/// <summary>
		/// Gets the <see cref="ProxyURL"/>s corresponding to the items in this <see cref="VwMapProxyURLCollection"/>.
		/// </summary>
		/// <returns></returns>
		public ProxyURLCollection GetProxyURLs()
		{
			return ProxyURLController.FetchByIDs(this.GetProxyURLIds());
		}

		/// <summary>
		/// Gets a list of the IDs of the <see cref="ProxyURL"/>s corresponding to the items in this <see cref="VwMapProxyURLCollection"/>.
		/// </summary>
		/// <returns></returns>
		public List<int> GetProxyURLIds()
		{
			List<int> proxyURLIds = new List<int>(this.Count);
			foreach (VwMapProxyURL vwMapProxyURL in this)
			{
				proxyURLIds.Add(vwMapProxyURL.Id);
			}
			return proxyURLIds;
		}

	}
}
