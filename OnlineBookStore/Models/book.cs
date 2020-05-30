namespace BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mydb.books")]
    public partial class book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public book()
        {
            order_items = new HashSet<order_item>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [StringLength(255)]
        public string book_title { get; set; }

        public long no_of_books { get; set; }

        public decimal book_cost { get; set; }

       // [StringLength(45)]
        public string book_description { get; set; }

     //   public string book_test { get; set; }

       // [StringLength(45)]
        public string book_image { get; set; }

        public int book_weight { get; set; }

        public int? book_height { get; set; }

        public int? book_width { get; set; }
        [ForeignKey("user")]
        public string userId { get; set; }

        public int categoryId { get; set; }

        public virtual category category { get; set; }

        public virtual ApplicationUser user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_item> order_items { get; set; }
    }
}
