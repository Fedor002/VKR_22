using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKR_Visik.Classes
{
    public class Sections
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int sections_Id { get; set; }

        public string? sections_Name { get; set; }

        public DateTime? sections_Data { get; set; }
    }
}
