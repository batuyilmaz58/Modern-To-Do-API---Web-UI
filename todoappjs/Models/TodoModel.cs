using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace todoappjs.Models
{
    public class TodoModel
    {
        [DisplayName("Başlık"),Required(ErrorMessage = "{0} boş olamaz")]
        public required string Title { get; set; }
    }
}
