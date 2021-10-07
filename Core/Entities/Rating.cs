using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Ratings")]
    public class Rating : BaseEntity
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int MediaId { get; set; }
        public Media Media { get; set; }
    }
}
