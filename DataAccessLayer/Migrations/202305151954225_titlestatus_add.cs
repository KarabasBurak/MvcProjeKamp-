﻿namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class titlestatus_add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Titles", "TitleStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Titles", "TitleStatus");
        }
    }
}
