using System;
using System.ComponentModel;
using System.Globalization;
using HP.HPFx.Diagnostics.Logging;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the TagController class.
	/// </summary>
	public partial class TagController
	{
		/// <summary>
		/// Returns a collection of all <see cref="Tag"/> instances associated with a specified name.
		/// </summary>
		/// <param name="tagName"></param>
		/// <returns></returns>
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TagCollection FetchByName(string tagName)
		{
			if (!Tag.IsValidName(tagName))
			{
				return new TagCollection();
			}

			return new TagCollection().Where("Name", tagName).Load();
		}

		/// <summary>
		/// Returns the <see cref="Tag"/> instance with the specified name, or null if none exists.
		/// </summary>
		/// <param name="tagName">The Name of the Tag.</param>
		/// <returns>A <see cref="Tag"/> instance with the specified name, or null if none exists.</returns>
		public static Tag GetByName(string tagName)
		{
			TagCollection collection = TagController.FetchByName(tagName);
			if (collection.Count == 1)
			{
				return collection[0];
			}
			if (collection.Count == 0)
			{
				return null;
			}

			//The following condition should never occur
			string message = string.Format(CultureInfo.CurrentCulture, "The specified Name ({0}) matches more than one Tag record.", tagName);
			LogManager.Current.Log(Severity.Warn, typeof(TagController), message);
			throw new InvalidOperationException(message);
		}

		/// <summary>
		/// Returns a <see cref="Tag"/> instance with the specified name (creating one if none already exists).
		/// </summary>
		/// <param name="tagName">The Name of the Tag.</param>
		/// <returns>A <see cref="Tag"/> instance with the specified name.</returns>
		public static Tag GetOrCreateByName(string tagName)
		{
			Tag tag = TagController.GetByName(tagName);
			if (tag == null)
			{
				tag = Tag.Insert(tagName, null);
			}
			return tag;
		}

	}
}
