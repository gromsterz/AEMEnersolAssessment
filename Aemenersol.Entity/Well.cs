using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aemenersol.Entity
{
    public class Well
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("id")]
        public long WellId { get; set; }

        [JsonProperty("platformId")]
        public long? PlatformId { get; set; }

        [Required]
        [JsonProperty("uniqueName")]
        public string UniqueName { get; set; }

        [Required]
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [Required]
        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [Required]
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Platform Platform { get; set; }
    }
}