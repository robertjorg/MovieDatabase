namespace Movie.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Loans", "DateModified", c => c.DateTime());
            AlterColumn("dbo.Loans", "DateAdded", c => c.DateTime());
            AlterColumn("dbo.MoviesOwned", "DateModified", c => c.DateTime());
            AlterColumn("dbo.MoviesOwned", "DateAdded", c => c.DateTime());
            AlterColumn("dbo.MovieTitles", "DateModified", c => c.DateTime());
            AlterColumn("dbo.MovieTitles", "DateAdded", c => c.DateTime());
            AlterColumn("dbo.StorageTypes", "DateModified", c => c.DateTime());
            AlterColumn("dbo.StorageTypes", "DateAdded", c => c.DateTime());
            AlterColumn("dbo.Users", "DateModified", c => c.DateTime());
            AlterColumn("dbo.Users", "DateAdded", c => c.DateTime());
            AlterColumn("dbo.Studios", "DateModified", c => c.DateTime());
            AlterColumn("dbo.Studios", "DateAdded", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Studios", "DateAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Studios", "DateModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "DateAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "DateModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.StorageTypes", "DateAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.StorageTypes", "DateModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MovieTitles", "DateAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MovieTitles", "DateModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MoviesOwned", "DateAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MoviesOwned", "DateModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Loans", "DateAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Loans", "DateModified", c => c.DateTime(nullable: false));
        }
    }
}
