using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	public abstract class BaseReadOnlyRecordController<TRecord, TRecordCollection, TController> : BaseRecordController<TRecord, TRecordCollection, TController>
		where TRecord : ReadOnlyRecord<TRecord>, IReadOnlyRecord, new()
		where TRecordCollection : ReadOnlyList<TRecord, TRecordCollection>, new()
		where TController : BaseReadOnlyRecordController<TRecord, TRecordCollection, TController>, new()
	{
	}
}