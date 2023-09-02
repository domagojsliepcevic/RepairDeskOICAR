namespace RepairDesk.Api.Extensions
{
    public static class Extensions
    {
        //public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        //{
        //    return collection == null || collection.Count == 0;
        //}

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || source.Count() == 0;
        }

        public static bool IsNull<T>(this T source)
        {
            return source == null;
        }

        public static bool IsNotNull<T>(this T source)
        {
            return source != null;
        }
    }
}
