using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKR_Visik.Classes
{
    public class MessageHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MH_id { get; set; }
        public int? MH_who { get; set; }
        [ForeignKey("MH_who ")]
        public Users? Users { get; set; }
        public int? MH_theme { get; set; }
        [ForeignKey("MH_theme")]
        public Themes? Themes { get; set; }
        public int? MH_placemant { get; set; }
        public int? MH_answer { get; set; }
        public DateTime? MH_data { get; set; }
        public string? MH_TheMessage { get; set; }
    }
}
