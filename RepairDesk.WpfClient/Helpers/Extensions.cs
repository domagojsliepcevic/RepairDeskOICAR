using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepairDesk.WpfClient.Helpers
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

		//public static bool HasRole(this ClaimsPrincipal principal, string role)
		//{
		//	return principal.Claims
		//		.Where(c => c.Type == "role" && c.Value.Equals(role, StringComparison.InvariantCultureIgnoreCase))
		//		.Any();
		//}
	}
}
