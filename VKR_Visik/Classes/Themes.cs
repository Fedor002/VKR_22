using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace VKR_Visik.Classes
{
    public class Themes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int themes_id { get; set; }
        public string? themes_name { get; set; }
        public DateTime? themes_data { get; set; }
        public int? sectionsId { get; set; }

        [ForeignKey("sectionsId")]
        public Sections? Sections { get; set; }
    }
}
