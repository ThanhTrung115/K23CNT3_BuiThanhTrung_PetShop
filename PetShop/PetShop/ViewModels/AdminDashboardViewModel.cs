namespace PetShop.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int UserCount { get; set; }
        public int OrderCount { get; set; }
        public int PetCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}