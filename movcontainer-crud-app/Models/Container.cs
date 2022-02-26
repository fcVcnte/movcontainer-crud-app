using movcontainer_crud_app.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace movcontainer_crud_app.Models
{
    public class Container
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ClientName { get; set; }
        [RegularExpression(@"^[a-zA-Z]{4}[0-9]{7}$", ErrorMessage = "Utilize apenas 11 caracteres, sendo 4 letras de início mais 7 números. Exemplo: PPJQ7162534")]
        public string Number { get; set; }
        public TypeCont TypeCont { get; set; }
        public Status Status { get; set; }
        public Category Category { get; set; }
    }
}
