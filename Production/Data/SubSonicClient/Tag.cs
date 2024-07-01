using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HP.ElementsCPS.Core.Security;
using HP.HPFx.Extensions.Text.StringAnalysis;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the Tag class.
	/// </summary>
	public partial class Tag
	{
		#region ToString override

		public override string ToString()
		{
			return Format(this);
		}

		/// <summary>
		/// Returns a user-friendly string representation of an Offer.
		/// </summary>
		/// <param name="tag">The Offer to format.</param>
		/// <returns></returns>
		private static string Format(Tag tag)
		{
			return string.Format(CultureInfo.CurrentCulture, "Tag #{0} ({1})", tag.Id, tag.Name);
		}

		#endregion

		/// <summary>
		/// Creates and returns a new <see cref="Tag"/> instance with the specified name.
		/// </summary>
		/// <param name="name">The Name of the Tag.</param>
		/// <param name="notes">The Notes for the Tag.</param>
		/// <returns>A <see cref="Tag"/> instance with the specified properties.</returns>
		public static Tag Insert(string name, string notes)
		{
			if (!Tag.IsValidName(name))
			{
				throw new ArgumentException("Invalid Tag name.", "name");
			}

			Tag tag = new Tag(true);
			tag.Name = name;
			tag.Notes = notes;
			tag.Save(SecurityManager.CurrentUserIdentityName);
			return tag;
		}

		/// <summary>
		/// Indicates whether a specified string is a valid value for the <see cref="Tag.Name"/> property.
		/// </summary>
		/// <param name="name">The Tag name to validate.</param>
		/// <returns><c>true</c> if <paramref name="name"/> is a valid name for a <see cref="Tag"/>; else <c>false</c>.</returns>
		public static bool IsValidName(string name)
		{
			//min length is 5
			if (string.IsNullOrEmpty(name) || (name.Length < 5))
			{
				return false;
			}

			//max length is 256
			if (name.Length > 256)
			{
				return false;
			}

			//must start with a letter
			if (!Char.IsLetter(name[0]))
			{
				return false;
			}

			//must contain only letters, numbers, and underscores
			if (name.RegExIsMatch(@"\W", RegexOptions.None))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Indicates whether a specified set if strings contains any values which are not a valid value for the <see cref="Tag.Name"/> property.
		/// </summary>
		/// <param name="names">The Tag names to validate.</param>
		/// <returns><c>true</c> if all of the values in <paramref name="names"/> are valid names for a <see cref="Tag"/>; else <c>false</c>.</returns>
		public static bool AreValidNames(IEnumerable<string> names)
		{
			int countOfInvalidNames = names.Count(name => !IsValidName(name));
			return countOfInvalidNames == 0;
		}

		/// <summary>
		/// Splits a delimited string containing Tag names into an enumerable set of individual Tag names.
		/// </summary>
		/// <param name="namesList">A delimited string containing Tag names.</param>
		/// <param name="validateTagNames">If <c>true</c>, the parsed tag names will be validated to ensure that they are valid.</param>
		/// <returns>An enumerable set of the individual Tag names in <paramref name="namesList"/>.</returns>
		/// <exception cref="ArgumentException">
		/// Thrown if <paramref name="validateTagNames"/> is <c>true</c> and <paramref name="namesList"/> contains any invalid Tag names.
		/// </exception>
		public static IEnumerable<string> ParseTagNameList(string namesList, bool validateTagNames)
		{
			if (namesList == null)
			{
				return new string[0];
			}

			string[] tagNames = namesList.Split(", \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			if (validateTagNames && !Tag.AreValidNames(tagNames))
			{
				throw new ArgumentException("The list contains invalid tag names.", "namesList");
			}
			return tagNames;
		}
	}
}
