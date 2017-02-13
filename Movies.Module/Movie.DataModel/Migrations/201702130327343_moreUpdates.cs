namespace Movie.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreUpdates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MoviesOwned", "StorageTypeId", "dbo.StorageTypes");
            DropIndex("dbo.MoviesOwned", new[] { "StorageTypeId" });
            AlterColumn("dbo.MoviesOwned", "StorageTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.MoviesOwned", "StorageTypeId");
            AddForeignKey("dbo.MoviesOwned", "StorageTypeId", "dbo.StorageTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MoviesOwned", "StorageTypeId", "dbo.StorageTypes");
            DropIndex("dbo.MoviesOwned", new[] { "StorageTypeId" });
            AlterColumn("dbo.MoviesOwned", "StorageTypeId", c => c.Int());
            CreateIndex("dbo.MoviesOwned", "StorageTypeId");
            AddForeignKey("dbo.MoviesOwned", "StorageTypeId", "dbo.StorageTypes", "Id");
        }
    }
}
