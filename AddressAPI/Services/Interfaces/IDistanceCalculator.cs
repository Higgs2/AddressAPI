namespace AddressAPI.Services.Interfaces
{
    /// <summary>
    /// Calculator to calculate distance between two points.
    /// </summary>
    public interface IDistanceCalculator
    {
        /// <summary>
        /// Calculates distance between two cities. If nothing could be calculated it returns 0.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public float CalculateDistance(string from, string to);
    }
}
