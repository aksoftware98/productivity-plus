using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace AKSoftware.Components.Blazor
{
    public partial class Ring
    {
        #region Parameters 

        /// <summary>
        /// Set of attributes that will be applied for the parent div of the SVG
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? UserAttributes { get; set; }
        
        /// <summary>
        /// Set gradient color for the stroke of the ring, null for no gradient and will use SolidColor property 
        /// </summary>
        [Parameter]
        public GradientColor? RingGradient { get; set; }

        /// <summary>
        /// Set gradient color for the full track ring, null for no gradient and will use TrackRingSolidColor property 
        /// </summary>
        [Parameter]
        public GradientColor? TrackRingGradient { get; set; }

        /// <summary>
        /// Indicate if the full track ring is needed
        /// </summary>
        [Parameter]
        public bool WithTrackRing { get; set; }

        /// <summary>
        /// Represents the solid color of the progress ring 
        /// </summary>
        [Parameter]
        public string RingSolidColor { get; set; } = "#008AF6";
        
        /// <summary>
        /// Represents the solid color of the full track ring
        /// </summary>
        [Parameter]
        public string TrackRingSolidColor { get; set; } = "#e5e5e5aa";

        /// <summary>
        /// Represents the max value of the progress 
        /// </summary>
        [Parameter]
        public double MaxValue { get; set; } = 100;

        /// <summary>
        /// Represents the value of the progress 
        /// </summary>
        [Parameter]
        public double Value { get; set; } = 0;

        /// <summary>
        /// Represents the width of the ring container square
        /// Default value 100px
        /// </summary>
        [Parameter]
        public string Width { get; set; } = "100px";

        /// <summary>
        /// Represents the height of the ring container square
        /// Default value 100px
        /// </summary>
        [Parameter]
        public string Height { get; set; } = "100px";
        #endregion 

        private string? _ringStroke => RingGradient == null ? RingSolidColor : $"url(#{_guid}-progress-ring-gradient)";
        private string? _trackStroke => TrackRingGradient == null ? TrackRingSolidColor : $"url(#{_guid}-track-ring-gradient)";

        private string _guid = Guid.NewGuid().ToString();
        private double _dashesValue => Value * 100 / MaxValue;
        private double _dashesSpace => 100 - _dashesValue;

    }

    /// <summary>
    /// Represents a gradient color to be used for a ring 
    /// For version 0.1 it supports only linear gradient consists of two values (start and end)
    /// IMPORTANT! to be upgraded to support more complex gradients
    /// </summary>
    public struct GradientColor
    {
        public GradientColor(string? firstColor, string? secondColor)
        {
            if (string.IsNullOrWhiteSpace(firstColor))
                throw new ArgumentNullException(nameof(firstColor));

            if (string.IsNullOrWhiteSpace(secondColor))
                throw new ArgumentNullException(nameof(secondColor));
            FirstColor = firstColor;
            SecondColor = secondColor;
        }

        public string FirstColor { get; set; }
        public string SecondColor { get; set; }
    }
}