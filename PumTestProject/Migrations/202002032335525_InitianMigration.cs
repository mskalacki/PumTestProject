namespace PumTestProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitianMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.companies",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        establishmentyear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "public.employees",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        surname = c.String(nullable: false),
                        birthdate = c.DateTime(nullable: false),
                        jobtitle = c.Int(nullable: false),
                        Company_Id = c.Long(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("public.companies", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.employees", "Company_Id", "public.companies");
            DropIndex("public.employees", new[] { "Company_Id" });
            DropTable("public.employees");
            DropTable("public.companies");
        }
    }
}
