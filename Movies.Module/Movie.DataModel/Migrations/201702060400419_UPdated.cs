namespace Movie.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPdated : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.MoviesOwned", "MovieTitlesId");
            AddForeignKey("dbo.MoviesOwned", "MovieTitlesId", "dbo.MovieTitles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MoviesOwned", "MovieTitlesId", "dbo.MovieTitles");
            DropIndex("dbo.MoviesOwned", new[] { "MovieTitlesId" });
        }
    }
}
