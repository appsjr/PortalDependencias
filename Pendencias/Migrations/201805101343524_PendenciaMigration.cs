namespace Pendencias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PendenciaMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DTOPendencias",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Titulo = c.String(),
                        Descricao = c.String(),
                        Status = c.Int(nullable: false),
                        Responsavel = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DTOPendencias");
        }
    }
}
