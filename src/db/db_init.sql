USE [master]
GO

IF DB_ID('products_db') IS NULL
  BEGIN

	PRINT 'Creating Database products_db...'

  END

CREATE DATABASE [products_db]
GO


USE [products_db]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- IF NOT EXISTS (SELECT 1
-- FROM sys.database_principals
-- WHERE name = N'redarbor_user')
-- BEGIN

--   CREATE LOGIN [redarbor_user] WITH PASSWORD = 'Passw0rd'
--   GO

--   CREATE USER [redarbor_user] FOR LOGIN [redarbor_user] WITH DEFAULT_SCHEMA=[dbo]
--   GO

--   EXEC sp_addrolemember N'db_owner', N'redarbor_user'
--   GO

-- END








-- Categories tables exists check
-- Create it if doesn´t exist
IF OBJECT_ID('dbo.category','U') IS NULL
BEGIN
	CREATE TABLE category(
		category_id SMALLINT IDENTITY NOT NULL,
		name VARCHAR(100) NOT NULL,
		description VARCHAR(4096) NULL,
		created_at SMALLDATETIME NOT NULL DEFAULT (GETDATE())
		CONSTRAINT category_pk PRIMARY KEY(category_id)
	);
END


-- Category entries check
-- If doesn´t exists, then insert
IF NOT EXISTS(SELECT 1 FROM dbo.category)
BEGIN
	SET IDENTITY_INSERT dbo.category ON;

		INSERT INTO category(category_id, name) VALUES(1,'Technology');
		INSERT INTO category(category_id, name) VALUES(2,'Tools');
		INSERT INTO category(category_id, name) VALUES(3,'Fashion');
		INSERT INTO category(category_id, name) VALUES(4,'Sport');
		INSERT INTO category(category_id, name) VALUES(5,'Health');
		INSERT INTO category(category_id, name) VALUES(6,'Pets');
	
	SET IDENTITY_INSERT dbo.category OFF;
END




-- Products table exists check; create if doesn´t exists
IF OBJECT_ID('dbo.products','U') IS NULL
BEGIN
	
	CREATE TABLE products(
	product_id INT IDENTITY NOT NULL,
	name VARCHAR(100) NOT NULL,
	description VARCHAR(4096) NULL,
	price DECIMAL(20,2) NOT NULL,
	created_at SMALLDATETIME NOT NULL DEFAULT(GETDATE()),
	category_id SMALLINT NOT NULL,
	CONSTRAINT product_pk PRIMARY KEY(product_id)
	);
	
END



-- Products entries check
-- If doesn´t exists, then insert
IF NOT EXISTS(SELECT 1 FROM dbo.products)
BEGIN
	
	SET IDENTITY_INSERT dbo.products ON;
	
	-- Technology category
	INSERT INTO products(product_id,name,description,price,category_id) VALUES(1,'Oppo Reno 14 F','The OPPO Reno14 F 5G is a smartphone that combines elegance and advanced technology to offer an exceptional user experience.',9259.98,1);
	INSERT INTO products(product_id,name,description,price,category_id) VALUES(2,'Sony headphones Wh-1000xm6','Wireless headphones with noise cancelling', 6450.50, 1);
	INSERT INTO products(product_id,name,description,price,category_id) VALUES(3,'Drone GT8 PRO','GT8 Obstacle Avoidance Belt 4.3 Inch Screen Aerial Photography Brushless Drone HD Camera 20 Minutes Flight Time Brushless Foldable RC Drone Quadcopter RTF',3550,1);
	
	-- Tools
	INSERT INTO products(product_id,name,description,price,category_id) VALUES(4,'Digital multimeter','Multifunctional digital multimeter: For measuring AC/DC current, AC/DC voltage, resistance, continuity and diodes, capacitance, frequency and temperature.',220.08,2);
	INSERT INTO products(product_id,name,description,price,category_id) VALUES(5,'Cordless Drill Kit Electric Screwdriver Piercing Kit','PINCHAN specializes in the development, manufacturing and distribution of power tools since [year missing], focusing on portable, lightweight and efficient products.', 297.11, 2);
	
	-- Fashion 
	INSERT INTO products(product_id,name,description,price,category_id) VALUES(6,'Ae Airflex+ Skinny Jean','AirFlex+: Authentic denim style with lightweight flexibility and comfort you have to feel to believe/High level of stretch that maintains its shape/Dark wash/With patches',1399.30,3);
	INSERT INTO products(product_id,name,description,price,category_id) VALUES(7,'Ésika Unisex Perfume It''s You Eau De Toilette 90 Ml','Esika YOU is a unisex fragrance that celebrates authenticity and enjoying life in every moment. With a vibrant blend of citrus and fresh notes, this eau de toilette is perfect for those seeking an energizing and unique fragrance.',175,3);
	
	-- Sport
	INSERT INTO products(product_id,name,description,price,category_id) VALUES(8,'Hexagonal Rubber Dumbbell 35 Lb (15 Kg) Gym Crossfit 1 Pc Black','A fixed hexagonal dumbbell can help you perform a variety of fitness exercises, especially for training your biceps and triceps.', 679, 4);
	INSERT INTO products(product_id,name,description,price,category_id) VALUES(9,'21-Speed ​​Mountain Bike with Suspension, Disc Brakes, 26-inch Wheels, Green, Frame Size 26','Adult 26-inch wheel mountain bike with 21 speeds and front suspension.',3149,4);
	
	-- Health
	INSERT INTO products(product_id,name,description,price,category_id) VALUES(10,'Heat-Sealed Face Mask','1000 Pieces Pleated Three-Layer Heat-Sealed Face Mask',585,5);
	INSERT INTO products(product_id,name,description,price,category_id) VALUES(11,'Centrum Women''s Multivitamin Tablets','Centrum Women''s Multivitamin Tablets, Multivitamin/Multimineral Supplement with Iron, Vitamin D3, B Vitamins and Antioxidant Vitamins C and E, Gluten Free, Non-GMO Ingredients - 200 Count',896.40,5);
	
	-- Pets
	INSERT INTO products(product_id,name,description,price,category_id) VALUES(12,'Dry dog ​​food','Ganador Adult Dry Dog Food for Medium and Large Breeds 20kg',949,6);
	INSERT INTO products(product_id,name,description,price,category_id) VALUES(13,'Adult Cat Food','Purina Excellent Urinary Adult Cat Food, Chicken and Rice Flavor, 7.5kg', 904, 6);
	
	
	SET IDENTITY_INSERT dbo.products OFF;
END





-- Inventory table creation
IF OBJECT_ID('dbo.inventory','U') IS NULL
BEGIN
	CREATE TABLE dbo.inventory(
	inventory_id INT IDENTITY NOT NULL,
	quantity INT NOT NULL,
	created_at SMALLDATETIME NOT NULL DEFAULT(GETDATE()),
	updated_at SMALLDATETIME NULL,
	product_id INT NOT NULL,
	CONSTRAINT inventory_pk PRIMARY KEY(inventory_id)
	);
END


-- Inventory table entries
IF NOT EXISTS (SELECT 1 FROM dbo.inventory)
BEGIN
		
	SET IDENTITY_INSERT dbo.inventory ON;
	
	INSERT INTO inventory(inventory_id, product_id, quantity) VALUES(1,1,100);
	INSERT INTO inventory(inventory_id, product_id, quantity) VALUES(2,2,245);
	INSERT INTO inventory(inventory_id, product_id, quantity) VALUES(3,3,38);
	INSERT INTO inventory(inventory_id, product_id, quantity) VALUES(4,4,2468);
	INSERT INTO inventory(inventory_id, product_id, quantity) VALUES(5,5,99);
	INSERT INTO inventory(inventory_id, product_id, quantity) VALUES(6,6,1500);
	INSERT INTO inventory(inventory_id, product_id, quantity) VALUES(7,7,9999);
	INSERT INTO inventory(inventory_id, product_id, quantity) VALUES(8,8,500);
	INSERT INTO inventory(inventory_id, product_id, quantity) VALUES(9,9,80);
	INSERT INTO inventory(inventory_id, product_id, quantity) VALUES(10,10,1000);
	INSERT INTO inventory(inventory_id, product_id, quantity) VALUES(11,11,148);
	INSERT INTO inventory(inventory_id, product_id, quantity) VALUES(12,12,50);
	INSERT INTO inventory(inventory_id, product_id, quantity) VALUES(13,13,60);
	
	
	SET IDENTITY_INSERT dbo.inventory OFF;
	
END





-- Transactions table creation
IF OBJECT_ID('dbo.transactions','U') IS NULL
BEGIN
	CREATE TABLE dbo.transactions(
	transaction_id BIGINT IDENTITY NOT NULL,
	quantity INT NOT NULL,
	transaction_at SMALLDATETIME NOT NULL DEFAULT(GETDATE()),
	transaction_type VARCHAR(100) NOT NULL,
	product_id INT NOT NULL,
	CONSTRAINT transaction_pk PRIMARY KEY(transaction_id),
	CONSTRAINT transaction_type_check CHECK(transaction_type IN ('Selled','Shrinkage','Discontinued'))
	);
END


-----------------------------------------------------
------------------ CheckoutProduct ------------------
-----------------------------------------------------
-- Stored Procedure to checkout products
-- It will reduce the inventory and create a transaction record
CREATE OR ALTER PROCEDURE CheckoutProduct(
	@productId INT,
	@quantity INT,
	@transaction_type VARCHAR(100)
)
AS
BEGIN
	DECLARE @AVAILABLE_QUANTITY INT = 0;
	DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;

	
	BEGIN TRY
		BEGIN TRANSACTION;
	
			SELECT @AVAILABLE_QUANTITY=quantity FROM inventory WHERE product_id = @productId;

			IF(@AVAILABLE_QUANTITY<=0)
			BEGIN
				RAISERROR('No hay artículos disponibles en el inventario',15,1);
			END
			
			
			IF(@AVAILABLE_QUANTITY<@quantity)
			BEGIN
				RAISERROR('No existe inventario suficiente',15,1);
			END

			UPDATE inventory SET quantity = quantity - @quantity, updated_at=GETDATE()  WHERE product_id = @productId;
			
			INSERT INTO transactions(quantity,transaction_type,product_id) 
			VALUES(@quantity,@transaction_type,@productId);
	
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION;
		
	  	SELECT
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();
	  	
	  	
	  	PRINT @ErrorMessage;
	  	
	    RAISERROR(@ErrorMessage, 15, @ErrorState);
	END CATCH
	
END