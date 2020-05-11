namespace BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mydb.cities")]
    public partial class city
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public city()
        {
            users = new HashSet<ApplicationUser>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "City")]

        public string city_name { get; set; }
        [Display(Name = "Gov")]
        [ForeignKey("gov")]
        public int gov_id { get; set; }

        public virtual gov gov { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationUser> users { get; set; }
        public virtual ICollection<hub_travel_cost> FromCosts { get; set; }
        public virtual ICollection<hub_travel_cost> ToCosts { get; set; }
    }
}
