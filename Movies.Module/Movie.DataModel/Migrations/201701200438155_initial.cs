namespace Movie.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
                        DateModified = c.DateTime(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MoviesOwned",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MovieTitlesId = c.Int(nullable: false),
                        StorageTypeId = c.Int(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StorageTypes", t => t.StorageTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.StorageTypeId);
            
            CreateTable(
                "dbo.MovieTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieTitle = c.String(),
                        StudiosId = c.Int(nullable: false),
                        MovieDesc = c.String(),
                        ReleaseDt = c.DateTime(),
                        ImdbUrl = c.String(),
                        DateModified = c.DateTime(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Studios", t => t.StudiosId, cascadeDelete: true)
                .Index(t => t.StudiosId);
            
            CreateTable(
                "dbo.Studios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudioName = c.String(),
                        DateModified = c.DateTime(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StorageTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageName = c.String(),
                        Url = c.String(),
                        DateModified = c.DateTime(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        OpentDt = c.DateTime(nullable: false),
                        LastLoginDt = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MoviesOwnedLoans",
                c => new
                    {
                        MoviesOwned_Id = c.Int(nullable: false),
                        Loan_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MoviesOwned_Id, t.Loan_Id })
                .ForeignKey("dbo.MoviesOwned", t => t.MoviesOwned_Id, cascadeDelete: true)
                .ForeignKey("dbo.Loans", t => t.Loan_Id, cascadeDelete: true)
                .Index(t => t.MoviesOwned_Id)
                .Index(t => t.Loan_Id);
            
            CreateTable(
                "dbo.MoviesOwnedMovieTitles",
                c => new
                    {
                        MoviesOwnedId = c.Int(nullable: false),
                        MovesOwnedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MoviesOwnedId, t.MovesOwnedId })
                .ForeignKey("dbo.MoviesOwned", t => t.MoviesOwnedId, cascadeDelete: true)
                .ForeignKey("dbo.MovieTitles", t => t.MovesOwnedId, cascadeDelete: true)
                .Index(t => t.MoviesOwnedId)
                .Index(t => t.MovesOwnedId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MoviesOwned", "UserId", "dbo.Users");
            DropForeignKey("dbo.MoviesOwned", "StorageTypeId", "dbo.StorageTypes");
            DropForeignKey("dbo.MoviesOwnedMovieTitles", "MovesOwnedId", "dbo.MovieTitles");
            DropForeignKey("dbo.MoviesOwnedMovieTitles", "MoviesOwnedId", "dbo.MoviesOwned");
            DropForeignKey("dbo.MovieTitles", "StudiosId", "dbo.Studios");
            DropForeignKey("dbo.MoviesOwnedLoans", "Loan_Id", "dbo.Loans");
            DropForeignKey("dbo.MoviesOwnedLoans", "MoviesOwned_Id", "dbo.MoviesOwned");
            DropIndex("dbo.MoviesOwnedMovieTitles", new[] { "MovesOwnedId" });
            DropIndex("dbo.MoviesOwnedMovieTitles", new[] { "MoviesOwnedId" });
            DropIndex("dbo.MoviesOwnedLoans", new[] { "Loan_Id" });
            DropIndex("dbo.MoviesOwnedLoans", new[] { "MoviesOwned_Id" });
            DropIndex("dbo.MovieTitles", new[] { "StudiosId" });
            DropIndex("dbo.MoviesOwned", new[] { "StorageTypeId" });
            DropIndex("dbo.MoviesOwned", new[] { "UserId" });
            DropTable("dbo.MoviesOwnedMovieTitles");
            DropTable("dbo.MoviesOwnedLoans");
            DropTable("dbo.Users");
            DropTable("dbo.StorageTypes");
            DropTable("dbo.Studios");
            DropTable("dbo.MovieTitles");
            DropTable("dbo.MoviesOwned");
            DropTable("dbo.Loans");
        }
    }
}
