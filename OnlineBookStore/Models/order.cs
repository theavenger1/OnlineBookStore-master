namespace BookStore.Models
{
    using Antlr.Runtime.Tree;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Table("mydb.orders")]
    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            order_items = new HashSet<order_item>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "User")]
        public string  userId { get; set; }
        [Required]
        public bool order_state { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime OrderCreated { get; set; }

        //public DateTime? OrderDate;
        // [Required]

        // public DateTime orderDate
        //{    


        //get
        //    {

        //        if (OrderDate == null )
        //        {
        //            OrderDate = DateTime.Now;

        //        }
        //        return OrderDate.Value;
        //    }
        //    private set { OrderDate = value;   }
        //}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_item> order_items { get; set; }

        public virtual ApplicationUser user { get; set; }
    }
}
