using System;
using System.Globalization;
using HP.HPFx.Utility;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the Note table.
	/// </summary>
	public partial class Note
	{

		#region Properties

		/// <summary>
		/// Strongly-typed convenience wrapper for the <see cref="EntityTypeId"/> property.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if an invalid <see cref="EntityTypeId"/> value is passed to the property.
		/// </exception>
		public EntityTypeIdentifier EntityTypeIdentifierEnumValue
		{
			get
			{
				return (EntityTypeIdentifier)this.EntityTypeId;
			}
			set
			{
				ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "EntityTypeIdentifierEnumValue", typeof(EntityTypeIdentifier));
				this.EntityTypeId = (int)value;
			}
		}

		/// <summary>
		/// Strongly-typed convenience wrapper for the NoteTypeId property.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if an invalid <see cref="NoteTypeId"/> value is passed to the property.
		/// </exception>
		public NoteTypeIdentifier NoteTypeIdentifierEnumValue
		{
			get
			{
				return NoteTypeController.ConvertNoteTypeId(this.NoteTypeId);
			}
			set
			{
				ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "NoteTypeIdentifierEnumValue", typeof(NoteTypeIdentifier));
				this.NoteTypeId = NoteTypeController.ConvertNoteTypeId(value);
			}
		}

		#endregion

		#region ToString override

		public override string ToString()
		{
			return Format(this);
		}

		/// <summary>
		/// Returns a user-friendly string representation of a Note.
		/// </summary>
		/// <param name="instance">The <see cref="Note"/> to format.</param>
		/// <returns></returns>
		private static string Format(Note instance)
		{
			return string.Format(CultureInfo.CurrentCulture, "Note #{0} (EntityType={1},Entity={2}) ({3})", instance.Id, instance.EntityTypeIdentifierEnumValue, instance.EntityId, instance.Name);
		}

		#endregion

		#region Deletion/Destruction Methods

		/// <summary>
		/// Deletes all <see cref="Note"/> records with the specified <see cref="EntityTypeId"/> and <see cref="EntityId"/> (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void Destroy(EntityTypeIdentifier entityTypeId, int entityId)
		{
			Query query = Note.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(EntityTypeIdColumn.ColumnName, entityTypeId);
			query = query.WHERE(EntityIdColumn.ColumnName, entityId);
			NoteController.DestroyByQuery(query);
		}

		#endregion

	}
}
