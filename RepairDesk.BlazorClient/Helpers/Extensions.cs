using System.Collections.Specialized;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace RepairDesk.BlazorClient.Helpers
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || source.Count() == 0;
        }

        public static bool HasRole(this ClaimsPrincipal principal, string role)
        {
            return principal.Claims
                .Where(c => c.Type == "role" && c.Value.Equals(role, StringComparison.InvariantCultureIgnoreCase))
                .Any();
        }

        public static bool IfKeyAndValueExist(this NameValueCollection collection, string keyName)
        {
            if (collection.AllKeys.Contains(keyName) && !string.IsNullOrEmpty(collection[keyName]))
            {
                return true;
            }

            return false;
        }

    }
}
