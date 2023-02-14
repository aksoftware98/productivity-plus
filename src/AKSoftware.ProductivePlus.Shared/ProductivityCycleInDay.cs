namespace AKSoftware.ProductivePlus.Shared
{
    /// <summary>
    /// Represents a productivty cycle stats <see cref="ProductivityCycle"/> in a single day 
    /// </summary>
    public class ProductivityCycleInDay
    {
        /// <summary>
        /// Productivity cyle stats 
        /// </summary>
        public ProductivityCycle ProductivityCycle { get; set; }

        /// <summary>
        /// Date that the productivty cycle occured in
        /// </summary>
        public DateOnly Date { get; set; }
    }
}