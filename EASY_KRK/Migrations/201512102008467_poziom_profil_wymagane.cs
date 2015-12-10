namespace EASY_KRK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poziom_profil_wymagane : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Kierunki", "IdPoziomu", "dbo.Poziomy");
            DropForeignKey("dbo.Kierunki", "IdProfilu", "dbo.Profile");
            DropIndex("dbo.Kierunki", new[] { "IdPoziomu" });
            DropIndex("dbo.Kierunki", new[] { "IdProfilu" });
            AlterColumn("dbo.Kierunki", "IdPoziomu", c => c.Int(nullable: false));
            AlterColumn("dbo.Kierunki", "IdProfilu", c => c.Int(nullable: false));
            CreateIndex("dbo.Kierunki", "IdPoziomu");
            CreateIndex("dbo.Kierunki", "IdProfilu");
            AddForeignKey("dbo.Kierunki", "IdPoziomu", "dbo.Poziomy", "IdPoziomu");
            AddForeignKey("dbo.Kierunki", "IdProfilu", "dbo.Profile", "IdProfilu");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kierunki", "IdProfilu", "dbo.Profile");
            DropForeignKey("dbo.Kierunki", "IdPoziomu", "dbo.Poziomy");
            DropIndex("dbo.Kierunki", new[] { "IdProfilu" });
            DropIndex("dbo.Kierunki", new[] { "IdPoziomu" });
            AlterColumn("dbo.Kierunki", "IdProfilu", c => c.Int());
            AlterColumn("dbo.Kierunki", "IdPoziomu", c => c.Int());
            CreateIndex("dbo.Kierunki", "IdProfilu");
            CreateIndex("dbo.Kierunki", "IdPoziomu");
            AddForeignKey("dbo.Kierunki", "IdProfilu", "dbo.Profile", "IdProfilu");
            AddForeignKey("dbo.Kierunki", "IdPoziomu", "dbo.Poziomy", "IdPoziomu");
        }
    }
}
