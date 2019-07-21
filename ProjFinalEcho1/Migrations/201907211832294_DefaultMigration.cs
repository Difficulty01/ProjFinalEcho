namespace ProjFinalEcho1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categorias", "Descricao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categorias", "Descricao", c => c.String());
        }
    }
}
