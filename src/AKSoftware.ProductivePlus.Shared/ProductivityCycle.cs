namespace AKSoftware.ProductivePlus.Shared
{
    /// <summary>
    /// Represents the progress of the working time, learning time, and the number of the meditation sessions achieved in addition to their targets 
    /// </summary>
    public struct ProductivityCycle
    {

        public ProductivityCycle()
        {
            WorkingTime = 0;
            TargetWorkingTime = 360;

            LearningTime = 0;
            TargetLearningTime = 90;

			MeditationSessionsCount = 0;
            TargetMeditationSessionsCount = 5;
        }

        /// <summary>
        /// Working time measured in minutes 
        /// </summary>
        public int WorkingTime { get; set; }
        
        /// <summary>
        /// Total of targeted working time
        /// </summary>
        public int TargetWorkingTime { get; set; }

        /// <summary>
        /// Learning time measured in minutes 
        /// </summary>
        public int LearningTime { get; set; } 

        /// <summary>
        /// Total of targeted learning time
        /// </summary>
        public int TargetLearningTime { get; set; }

        /// <summary>
        /// Number of meditation session a day (Pray, self-talk, walk ..etc)
        /// </summary>
        public int MeditationSessionsCount { get; set; }

        /// <summary>
        /// Total number of meditation session targeted a day
        /// </summary>
        public int TargetMeditationSessionsCount { get; set; }

    }
}