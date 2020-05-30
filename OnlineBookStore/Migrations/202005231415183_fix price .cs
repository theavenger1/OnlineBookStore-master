namespace OnlineBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixprice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("mydb.books", "book_weight", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("mydb.books", "book_weight", c => c.Int());
        }
    }
}
