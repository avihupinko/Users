using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace users_backend.Models
{
    public class UserLogicModel
    {
        [JsonProperty("id")]
        public Guid? Id { get; set; }

        [Required]
        [RegularExpression("[0-9]*", ErrorMessage = "User ID must be numeric")]
        [MaxLength(250)]
        [JsonProperty("userId")]
        public required string UserId { get; set; }

        [Required]
        [MaxLength(250)]
        [JsonProperty("userName")]
        public required string UserName { get; set; }

        [MaxLength(250)]
        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonProperty("gender")]
        public string? Gender { get; set; }

        [RegularExpression("[0-9]*", ErrorMessage = "Phone must be numeric")]
        [MaxLength(250)]
        [JsonProperty("phone")]
        public string? Phone { get; set; }

        [JsonProperty("created")]
        public DateTime? Created { get; set; }

        [JsonProperty("updated")]
        public DateTime? Updated { get; set; }
    }

    public class BasePageModel<T>
    {
        [JsonProperty("phone")]
        public int Total { get; set; }

        [JsonProperty("phone")]
        public IList<T> Data { get; set; }
    }
}
