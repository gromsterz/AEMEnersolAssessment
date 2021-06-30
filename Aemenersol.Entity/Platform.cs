using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aemenersol.Entity
{
    public class Platform
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("id")]
        public long PlatformId { get; set; }

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

        [JsonProperty("well", NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<Well> Wells { get; set; } = new List<Well>();
    }
}