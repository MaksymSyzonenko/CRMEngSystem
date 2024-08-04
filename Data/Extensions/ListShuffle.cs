namespace CRMEngSystem.Data.Extensions
{
    public static class ListShuffle
    {
        private readonly static Random rng = new();

        public static IList<TEntity> Shuffle<TEntity>(this IList<TEntity> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
            return list;
        }
    }
}
