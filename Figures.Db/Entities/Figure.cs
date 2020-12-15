using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Figures.Db.Entities
{
    [Table("Figure")]
    public class Figure
    {
        [Key]
        public int FigureId { get; set; }

        [StringLength(32)]
        public string FigureType { get; set; }

        public string Object { get; set; }

        public double? Area { get; set; }
    }
}
