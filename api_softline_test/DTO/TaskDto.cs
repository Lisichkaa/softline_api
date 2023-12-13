using System.ComponentModel.DataAnnotations.Schema;

namespace api_softline_test.DTO
{
    public class TaskDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status_ID { get; set; }
        public string StatusName { get; set; }
    }
   
}
