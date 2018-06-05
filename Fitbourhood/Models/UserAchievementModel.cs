namespace Fitbourhood.Repositories
{
    public class UserAchievementModel
    {
        public int DDiscipline { get; set; }
        public string Achievement { get; set; }
        public string Description { get; set; }
        public int NumberOfSportEventsParticipated { get; set; }
        public int NumberOfSportEventsNeeded { get; set; }
        public bool IsAchieved { get; set; }
    }
}