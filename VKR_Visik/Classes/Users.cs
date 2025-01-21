using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VKR_Visik.Classes
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int users_Id { get; set; }
        public string? users_FIO { get; set; }
        [Required(ErrorMessage = "Wrong password")]
        [DataType(DataType.Password)]
        public string? users_Password { get; set; }

        public string? users_Role { get; set; }

        public int? Users_AccountActive { get; set; }
    }
}
