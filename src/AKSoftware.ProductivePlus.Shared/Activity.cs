using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKSoftware.ProductivePlus.Shared
{
    public class Activity
    {

        /// <summary>
        /// Number of units achieved in the activity (Minutes for working and learning, count for meditation)
        /// </summary>
        public int TotalUnits { get; set; }

        /// <summary>
        /// Starting time and date of the acitivty (Required only for the working and learning activities) 
        /// </summary>
        public DateTime? StartingDate { get; set; }

        /// <summary>
        /// Ending time and date of the activity (Required only for the working and learning activities)
        /// </summary>
        public DateTime? EndingDate { get; set; }

        /// <summary>
        /// Type of the activity <see cref="ActivityType"/> (Working, Learning, Meditation) 
        /// </summary>
        public ActivityType ActivityType { get; set; }

        /// <summary>
        /// Notes related to the achieved activity (Supports Markdown format) 
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Type of learning activity achieved (Required only for learning activity type) 
        /// </summary>
        public LearningActivityType? LearningActivityType { get; set; }

        /// <summary>
        /// Priority of the working activity achieved (Required only for working activity type) 
        /// </summary>
        public WorkingActivityPriority? WorkingActivityPriority { get; set; }

        /// <summary>
        /// Type of the meditiation activity achieved (Required only for meditation activity type) 
        /// </summary>
        public MeditationActivityType? MeditationActivityType { get; set; }

        /// <summary>
        /// Short description that describes the activity and what have been acheived through it 
        /// </summary>
        public string? ShortDescription { get; set; }

    }

}
