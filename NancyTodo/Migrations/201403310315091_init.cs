namespace NancyTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SessionTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(),
                        UserName = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TodoItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Priority = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        OwnerId = c.Int(nullable: false),
                        TodoList_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TodoLists", t => t.TodoList_Id)
                .Index(t => t.TodoList_Id);
            
            CreateTable(
                "dbo.TodoLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ListName = c.String(),
                        OwnerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TodoUserIdentities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Created = c.DateTime(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TodoItems", "TodoList_Id", "dbo.TodoLists");
            DropIndex("dbo.TodoItems", new[] { "TodoList_Id" });
            DropTable("dbo.TodoUserIdentities");
            DropTable("dbo.TodoLists");
            DropTable("dbo.TodoItems");
            DropTable("dbo.SessionTokens");
        }
    }
}
