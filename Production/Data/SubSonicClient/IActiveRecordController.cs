using System.ComponentModel;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	public interface IActiveRecordController : IRecordController
	{
		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		bool Delete(object Id);

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
		bool Destroy(object Id);
	}
}