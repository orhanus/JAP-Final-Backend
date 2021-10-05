using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Ratings")]
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int MediaId { get; set; }
        public Media Media { get; set; }
    }
}
