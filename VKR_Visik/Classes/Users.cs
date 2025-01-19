using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VKR_Visik.Classes
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int users_id { get; set; }
        public string? users_FIO { get; set; }
        [Required(ErrorMessage = "Wrong password")]
        [DataType(DataType.Password)]
        public string? password_u { get; set; }

        public string? ussers_role { get; set; }
    }
}
