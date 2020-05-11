using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("city")]
        public int cityId { get; set; }

        [StringLength(45)]
        public string user_address { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<book> books { get; set; }

        public virtual city city { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<book> books { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<city> cities { get; set; }
        public virtual DbSet<gov> govs { get; set; }
        public virtual DbSet<hub_travel_cost> hub_travel_cost { get; set; }
        public virtual DbSet<hub> hubs { get; set; }
        public virtual DbSet<hubs_admins> hubs_admins { get; set; }
        public virtual DbSet<order_item> order_items { get; set; }
        public virtual DbSet<order> orders { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //for identity user intialization 
             base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<hub_travel_cost>()
                       .HasRequired(m => m.From)
                       .WithMany(t => t.FromCosts)
                       .HasForeignKey(m => m.FromId)
                       .WillCascadeOnDelete(false);

            modelBuilder.Entity<hub_travel_cost>()
                        .HasRequired(m => m.To)
                        .WithMany(t => t.ToCosts)
                        .HasForeignKey(m => m.ToId)
                        .WillCascadeOnDelete(false);

            

        }
    }
}