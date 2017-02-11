namespace Movie.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRedundantOnUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "OpentDt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "OpentDt", c => c.DateTime(nullable: false));
        }
    }
}
