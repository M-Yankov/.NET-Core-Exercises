namespace CameraBazar.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Attributes;
    using Constants;
    using Enums;

    public class Camera
    {
        [Key]
        public int Id { get; set; }

        public CameraMake Make { get; set; }

        [Required]
        [TextRestrict]
        public string Model { get; set; }
        
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, DataModelConstants.MaxQuantity)]
        public int Quantity { get; set; }

        [Range(DataModelConstants.MinShutterSpeerdLowerBoundary, DataModelConstants.MinShutterSpeerdUpperBoundary)]
        public int MinShutterSpeed { get; set; }

        [Range(DataModelConstants.MaxShutterSpeerdLowerBoundary, DataModelConstants.MaxShutterSpeerdUpperBoundary)]
        public int MaxShutterSpeed { get; set; }

        [Range(DataModelConstants.MinISOSpeerdLowerBoundary, DataModelConstants.MinISOSpeerdUpperBoundary)]
        public int MinISO { get; set; }

        [DividableByAttribute(DividableValue = 100)]
        [Range(DataModelConstants.MaxISOSpeerdLowerBoundary, DataModelConstants.MaxISOSpeerdUpperBoundary)]
        public int MaxISO { get; set; }

        public bool IsFullFrame { get; set; }

        [MaxLength(DataModelConstants.ResolutionMaxLength)]
        public string VideoResolution { get; set; }

        public LightMetering LightMetering { get; set; }

        [MaxLength(DataModelConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [RegularExpression(DataModelConstants.ImageUrlRegExValue)]
        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
