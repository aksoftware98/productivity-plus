namespace AKSoftware.ProductivityPlus.Server.Domain.Interfaces
{
    public interface IUserProfilesRepository
    {
        Task<UserProfile> CreateAsync(string userId, UserProfile userProfile);
        Task<UserProfile?> GetByEmailAsync(string email);
        Task<UserProfile?> GetByUserIdAsync(string userId);
        Task<UserProfile> UpdateAvatarAsync(string userId, UserProfile userProfile);
        Task<UserProfile> UpdateDailyGoalAsync(string userId, UserProfile userProfile);
        Task<UserProfile> UpdateDisplayNameAsync(string userId, UserProfile userProfile);
        Task<UserProfile> UpdateNameAsync(string userId, UserProfile userProfile);
        Task<UserProfile> UpdateWeeklyGoalAsync(string userId, UserProfile userProfile);
    }
}