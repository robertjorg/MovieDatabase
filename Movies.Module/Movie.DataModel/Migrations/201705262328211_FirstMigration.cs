namespace Movie.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoanDt = c.DateTime(nullable: false),
                        ReturnDt = c.DateTime(),
                        LoanToFirstName = c.String(),
                        LoanToLastName = c.String(),
                        MoviesOwnedId = c.Int(nullable: false),
                        DateModified = c.DateTime(),
                        DateAdded = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MoviesOwned", t => t.MoviesOwnedId, cascadeDelete: true)
                .Index(t => t.MoviesOwnedId);
            
            CreateTable(
                "dbo.MoviesOwned",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MovieTitlesId = c.Int(nullable: false),
                        StorageTypeId = c.Int(nullable: false),
                        DateModified = c.DateTime(),
                        DateAdded = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovieTitles", t => t.MovieTitlesId, cascadeDelete: true)
                .ForeignKey("dbo.StorageTypes", t => t.StorageTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieTitlesId)
                .Index(t => t.StorageTypeId);
            
            CreateTable(
                "dbo.MovieTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieTitle = c.String(),
                        StudiosId = c.Int(),
                        MovieDesc = c.String(),
                        ReleaseDt = c.DateTime(),
                        ImdbUrl = c.String(),
                        DateModified = c.DateTime(),
                        DateAdded = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Studios", t => t.StudiosId)
                .Index(t => t.StudiosId);
            
            CreateTable(
                "dbo.Studios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudioName = c.String(),
                        DateModified = c.DateTime(),
                        DateAdded = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StorageTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageName = c.String(),
                        Url = c.String(),
                        DateModified = c.DateTime(),
                        DateAdded = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserName = c.String(),
                        LastLoginDt = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        DateAdded = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MoviesOwned", "UserId", "dbo.Users");
            DropForeignKey("dbo.MoviesOwned", "StorageTypeId", "dbo.StorageTypes");
            DropForeignKey("dbo.MoviesOwned", "MovieTitlesId", "dbo.MovieTitles");
            DropForeignKey("dbo.MovieTitles", "StudiosId", "dbo.Studios");
            DropForeignKey("dbo.Loans", "MoviesOwnedId", "dbo.MoviesOwned");
            DropIndex("dbo.MovieTitles", new[] { "StudiosId" });
            DropIndex("dbo.MoviesOwned", new[] { "StorageTypeId" });
            DropIndex("dbo.MoviesOwned", new[] { "MovieTitlesId" });
            DropIndex("dbo.MoviesOwned", new[] { "UserId" });
            DropIndex("dbo.Loans", new[] { "MoviesOwnedId" });
            DropTable("dbo.Users");
            DropTable("dbo.StorageTypes");
            DropTable("dbo.Studios");
            DropTable("dbo.MovieTitles");
            DropTable("dbo.MoviesOwned");
            DropTable("dbo.Loans");
        }
    }
}
