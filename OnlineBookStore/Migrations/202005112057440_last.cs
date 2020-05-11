namespace OnlineBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class last : DbMigration
    {
        public override void Up()
        {
            AddColumn("mydb.orders", "OrderCreated", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));

          
            DropColumn("mydb.orders", "OrderDate");
        }
        
        public override void Down()
        {
            AddColumn("mydb.orders", "OrderDate", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            DropColumn("mydb.orders", "OrderCreated");
        }
    }
}
