namespace BookingEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Booking2",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        creator = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        NumPeople = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                        EventCost = c.Double(nullable: false),
                        venueId = c.Int(nullable: false),
                        TotalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        approval = c.String(),
                        Venuu = c.String(),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Event_Type", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        cart_id = c.String(nullable: false, maxLength: 128),
                        date_created = c.DateTime(nullable: false),
                        Booking2_BookingId = c.Int(),
                        Order_OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.cart_id)
                .ForeignKey("dbo.Booking2", t => t.Booking2_BookingId)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .Index(t => t.Booking2_BookingId)
                .Index(t => t.Order_OrderId);
            
            CreateTable(
                "dbo.Cart_Item",
                c => new
                    {
                        cart_item_id = c.String(nullable: false, maxLength: 128),
                        cart_id = c.String(maxLength: 128),
                        item_id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                        UserEmail = c.String(),
                        OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.cart_item_id)
                .ForeignKey("dbo.Carts", t => t.cart_id)
                .ForeignKey("dbo.Items", t => t.item_id, cascadeDelete: true)
                .Index(t => t.cart_id)
                .Index(t => t.item_id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemCode = c.Int(nullable: false, identity: true),
                        Department_ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 80),
                        Description = c.String(nullable: false, maxLength: 255),
                        QuantityInStock = c.Int(nullable: false),
                        Picture = c.Binary(),
                        Price = c.Double(nullable: false),
                        CostPrice = c.Double(nullable: false),
                        Category_Category_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ItemCode)
                .ForeignKey("dbo.Categories", t => t.Category_Category_ID)
                .ForeignKey("dbo.Departments", t => t.Department_ID, cascadeDelete: true)
                .Index(t => t.Department_ID)
                .Index(t => t.Category_Category_ID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Department_ID = c.Int(nullable: false, identity: true),
                        Department_Name = c.String(nullable: false, maxLength: 80),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Department_ID)
                .Index(t => t.Department_Name, unique: true, name: "Department_Index");
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Category_ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 80),
                        Department_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Category_ID)
                .ForeignKey("dbo.Departments", t => t.Department_ID, cascadeDelete: true)
                .Index(t => t.Department_ID);
            
            CreateTable(
                "dbo.FoodOrders",
                c => new
                    {
                        cart_item_id = c.String(nullable: false, maxLength: 128),
                        cart_id = c.String(),
                        item_id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                        ItemName = c.String(),
                        UserEmail = c.String(),
                        Picture = c.Binary(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderStatus = c.String(),
                        OrderDate = c.String(),
                        DayOfWik = c.String(),
                        OrderId = c.Int(),
                        Item_ItemCode = c.Int(),
                    })
                .PrimaryKey(t => t.cart_item_id)
                .ForeignKey("dbo.Items", t => t.Item_ItemCode)
                .Index(t => t.Item_ItemCode);
            
            CreateTable(
                "dbo.Event_Type",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false),
                        EventDescription = c.String(),
                        BasicPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        contactId = c.Int(nullable: false, identity: true),
                        CuName = c.String(nullable: false),
                        CuLName = c.String(nullable: false),
                        Cuaddress = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Subject = c.String(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.contactId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        PhoneNum = c.String(),
                    })
                .PrimaryKey(t => t.CustId);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        eqpId = c.Int(nullable: false, identity: true),
                        Dj = c.Boolean(nullable: false),
                        SoundSystem = c.Boolean(nullable: false),
                        stage = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.eqpId);
            
            CreateTable(
                "dbo.MealOrders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderNumber = c.String(),
                        UserOrder = c.String(),
                        Total = c.Double(nullable: false),
                        Status = c.String(),
                        OrderDate = c.String(),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        TotalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        location = c.String(),
                        approval = c.String(),
                        creator = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        NumPeople = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                        EventCost = c.Double(nullable: false),
                        venue = c.Double(nullable: false),
                        venueId = c.Int(nullable: false),
                        Venuu = c.String(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Event_Type", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Venue1cs", t => t.venueId, cascadeDelete: true)
                .Index(t => t.EventId)
                .Index(t => t.venueId);
            
            CreateTable(
                "dbo.Venue1cs",
                c => new
                    {
                        venueId = c.Int(nullable: false, identity: true),
                        venueName = c.String(nullable: false),
                        NumGests = c.Int(nullable: false),
                        Address = c.String(),
                        Cost = c.Double(nullable: false),
                        ImageType = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.venueId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PhoneNum = c.String(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Orders", "venueId", "dbo.Venue1cs");
            DropForeignKey("dbo.Orders", "EventId", "dbo.Event_Type");
            DropForeignKey("dbo.Carts", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.Booking2", "EventId", "dbo.Event_Type");
            DropForeignKey("dbo.Carts", "Booking2_BookingId", "dbo.Booking2");
            DropForeignKey("dbo.Cart_Item", "item_id", "dbo.Items");
            DropForeignKey("dbo.FoodOrders", "Item_ItemCode", "dbo.Items");
            DropForeignKey("dbo.Items", "Department_ID", "dbo.Departments");
            DropForeignKey("dbo.Items", "Category_Category_ID", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Department_ID", "dbo.Departments");
            DropForeignKey("dbo.Cart_Item", "cart_id", "dbo.Carts");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Orders", new[] { "venueId" });
            DropIndex("dbo.Orders", new[] { "EventId" });
            DropIndex("dbo.FoodOrders", new[] { "Item_ItemCode" });
            DropIndex("dbo.Categories", new[] { "Department_ID" });
            DropIndex("dbo.Departments", "Department_Index");
            DropIndex("dbo.Items", new[] { "Category_Category_ID" });
            DropIndex("dbo.Items", new[] { "Department_ID" });
            DropIndex("dbo.Cart_Item", new[] { "item_id" });
            DropIndex("dbo.Cart_Item", new[] { "cart_id" });
            DropIndex("dbo.Carts", new[] { "Order_OrderId" });
            DropIndex("dbo.Carts", new[] { "Booking2_BookingId" });
            DropIndex("dbo.Booking2", new[] { "EventId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Venue1cs");
            DropTable("dbo.Orders");
            DropTable("dbo.MealOrders");
            DropTable("dbo.Equipments");
            DropTable("dbo.Customers");
            DropTable("dbo.Contacts");
            DropTable("dbo.Event_Type");
            DropTable("dbo.FoodOrders");
            DropTable("dbo.Categories");
            DropTable("dbo.Departments");
            DropTable("dbo.Items");
            DropTable("dbo.Cart_Item");
            DropTable("dbo.Carts");
            DropTable("dbo.Booking2");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
