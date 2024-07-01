using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HP.ElementsCPS.Core.Security;
namespace HP.ElementsCPS.Data.SubSonicClient
{
    /// <summary>
    /// The non-generated portion of the Application class.
    /// </summary>
    public partial class Application
    {
        public RoleApplicationId RoleApplicationId
        {
            get
            {
                return (RoleApplicationId)this.Id;
            }
        }

        #region ToString override

        public override string ToString()
        {
            return Format(this);
        }

        /// <summary>
        /// Returns a user-friendly string representation of a Application.
        /// </summary>
        /// <param name="instance">The <see cref="Application"/> to format.</param>
        /// <returns></returns>
        private static string Format(Application instance)
        {
            return string.Format(CultureInfo.CurrentCulture, "Application #{0} ({1})", instance.Id, instance.Name);
        }

       

        public static string Format(IEnumerable<RoleApplicationId> applications)
        {
            const string separator = ", ";
            return Format(applications, separator);
        }
        public static IEnumerable<string> GetApplicationNames(IEnumerable<RoleApplicationId> Applications)
        {
            //TODO: Review Needed: Review implementation for correctness
#warning Review Needed: Review implementation for correctness
            return Applications.Select(r => string.Format("{0:G}", (object)r));
        }
        #endregion

        #region ApplicationId-related Utility Methods

        /// <summary>
        /// Converts a specified set of Applications to a set of Application names.
        /// </summary>
        /// <returns></returns>
       

        /// <summary>
        /// Returns a delimited string representing a specified set of Applications.
        /// </summary>
        /// <returns></returns>
        public static string Format(IEnumerable<RoleApplicationId> Applications, string separator)
        {
            return string.Join(separator, Application.GetApplicationNames(Applications).ToArray());
        }

       

        #endregion

    }
}
