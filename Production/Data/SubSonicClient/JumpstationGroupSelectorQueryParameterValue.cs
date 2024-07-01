using System.Globalization;
using HP.HPFx.Diagnostics.Logging;
using SubSonic;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the JumpstationGroupSelector_QueryParameterValue table.
	/// </summary>
	public partial class JumpstationGroupSelectorQueryParameterValue
	{

		#region Deletion/Destruction Methods

		/// <summary>
		/// Delete all records by JumpstationGroupSelectorId and QueryParameterId
		/// </summary>
		public static void DestroyByJumpstationGroupSelectorIdQueryParameterId(int jumpstationGroupSelectorId, int queryParameterId)
		{
			SubSonic.StoredProcedure sp = StoredProcedures.DeleteJumpstationGroupSelectorQueryParameterValueByJumpstationGroupSelectorIdQueryParameterId(jumpstationGroupSelectorId, queryParameterId);
			sp.ExecuteScalar();
		}

		/// <summary>
		/// Deletes a specified <see cref="JumpstationGroupSelectorQueryParameterValue"/> record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void DestroyByJumpstationGroupSelectorId(int jumpstationGroupSelectorId)
		{
			Query query = JumpstationGroupSelectorQueryParameterValue.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(JumpstationGroupSelectorIdColumn.ColumnName, jumpstationGroupSelectorId);
			JumpstationGroupSelectorQueryParameterValueController.DestroyByQuery(query);
		}

		#endregion

	}
}
