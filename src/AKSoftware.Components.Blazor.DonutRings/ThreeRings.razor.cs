using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace AKSoftware.Components.Blazor
{
    public partial class ThreeRings
    {

        #region Parameters 
        /// <summary>
        /// The value of the big outside ring
        /// </summary>
        [Parameter]
        public int FirstValue { get; set; }

        /// <summary>
        /// The maximum value of the big outside ring 
        /// </summary>
        [Parameter]
        public int FirstMaxValue { get; set; }

        /// <summary>
        /// The value of the middle ring 
        /// </summary>
        [Parameter]
        public int SecondValue { get; set; }

        /// <summary>
        /// The maximum value of the middle ring 
        /// </summary>
        [Parameter]
        public int SecondMaxValue { get; set; }

        /// <summary>
        /// The value of the small inside ring 
        /// </summary>
        [Parameter]
        public int ThirdValue { get; set; }

        /// <summary>
        /// The maximum value of the small inside ring 
        /// </summary>
        [Parameter]
        public int ThirdMaxValue { get; set; }

        /// <summary>
        /// Gradient colors of the big outside ring 
        /// </summary>
        [Parameter]
        public GradientColor OutsideRingGradient { get; set; }

        /// <summary>
        /// Gradient colors of the middle ring 
        /// </summary>
        [Parameter]
        public GradientColor CenterRingGradient { get; set; }

        /// <summary>
        /// Gradient colors of the small inside ring 
        /// </summary>
        [Parameter]
        public GradientColor SmallRingGradient { get; set; }

        /// <summary>
        /// Set the width of the full component, the component has the square shape and by default the height will be equal to the width value 
        /// Default value is 100%, the value can be in px or rm (100px, 100rm..etc)
        /// </summary>
        [Parameter]
        public string? Width { get; set; } = "100%";
        #endregion 


    }
}