namespace BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mydb.hub_travel_cost")]
    public partial class hub_travel_cost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hub_travel_cost()
        {
            order_items = new HashSet<order_item>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

       
        public int FromId { get; set; }

       
    
        public int ToId { get; set; }


        public virtual city From { get; set; }

        public virtual city To { get; set; }

        [Display(Name = "Cost")]

        public int cost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_item> order_items { get; set; }
    }
}
