using System.ComponentModel.DataAnnotations;

namespace todoappjs.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
