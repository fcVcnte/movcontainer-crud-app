using movcontainer_crud_app.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace movcontainer_crud_app.Models
{
    public class Movement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public TypeMov TypeMov { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual Container? Container { get; set; }
        public int ContainerId { get; set; }
    }
}
