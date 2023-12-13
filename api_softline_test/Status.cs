using System.ComponentModel.DataAnnotations;

namespace api_softline_test
{
    public class Status
    {
        [Key]
        public int Status_ID { get; set; }

        [Required]
        public string Status_name { get; set; }

        public List<TaskDb> TasksList { get; set; }
    }
}