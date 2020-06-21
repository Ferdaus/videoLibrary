namespace videoLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Genre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES (1,'Action')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2,'Thriller')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3,'Drams')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4,'Comedy')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (5,'Romantic')");
        }
        
        public override void Down()
        {
        }
    }
}
