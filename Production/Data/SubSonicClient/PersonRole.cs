using System.Globalization;
using HP.HPFx.Diagnostics.Logging;
using SubSonic;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the Person_Role table.
	/// </summary>
	public partial class PersonRole
	{

		#region Deletion/Destruction Methods

		/// <summary>
		/// Delete all records by PersonId
		/// </summary>
		public static void DestroyByPersonId(int personId)
		{
			Query query = PersonRole.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(PersonIdColumn.ColumnName, personId);
			PersonRoleController.DestroyByQuery(query);
		}

		/// <summary>
		/// Deletes a specified <see cref="PersonRole"/> record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void Destroy(int personId, int roleId)
		{
			Query query = PersonRole.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(PersonIdColumn.ColumnName, personId);
			query = query.WHERE(RoleIdColumn.ColumnName, roleId);
			PersonRoleController.DestroyByQuery(query);
		}

		#endregion

	}
}
