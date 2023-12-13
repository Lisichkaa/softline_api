using System.ComponentModel.DataAnnotations;

namespace softlineEntities
{
    public class Status
    {
        [Key]
        public int Status_ID { get; set; }

        [Required]
        public string Status_name { get; set; }
    }
}