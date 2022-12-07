using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P132Pustok.Models
{
    public class Book:BaseEntity
    {
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public bool StockStatus { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercent { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsNew { get; set; }
        [NotMapped]
        public IFormFile?PosterFile { get; set; }
        [NotMapped]
        public IFormFile?HoverPosterFile { get; set; }

        [NotMapped]
        public List<IFormFile>?ImageFiles { get; set; }
        [NotMapped]
        public List<int>? BookImageIds { get; set; }


        public Author? Author { get; set; }
        public Genre? Genre { get; set; }
        public List<BookImage>? BookImages { get; set; }
    }
}
