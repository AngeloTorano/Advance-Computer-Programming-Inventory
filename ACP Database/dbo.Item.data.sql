SET IDENTITY_INSERT [dbo].[Item] ON
INSERT INTO [dbo].[Item] ([itemId], [ItemName], [quantity], [price], [unit], [supplier_id]) VALUES (300, N'Miki', 100, CAST(70.00 AS Decimal(10, 2)), N'kilo', 107)
INSERT INTO [dbo].[Item] ([itemId], [ItemName], [quantity], [price], [unit], [supplier_id]) VALUES (301, N'Coke Mismo 300ml', 200, CAST(19.99 AS Decimal(10, 2)), N'pieces', 105)
INSERT INTO [dbo].[Item] ([itemId], [ItemName], [quantity], [price], [unit], [supplier_id]) VALUES (302, N'Coke 1.5L', 150, CAST(67.99 AS Decimal(10, 2)), N'pieces', 105)
INSERT INTO [dbo].[Item] ([itemId], [ItemName], [quantity], [price], [unit], [supplier_id]) VALUES (303, N'Royal Mismo 300ml', 300, CAST(19.99 AS Decimal(10, 2)), N'pieces', 105)
INSERT INTO [dbo].[Item] ([itemId], [ItemName], [quantity], [price], [unit], [supplier_id]) VALUES (304, N'Sprite 1.5L', 180, CAST(65.99 AS Decimal(10, 2)), N'pieces', 105)
INSERT INTO [dbo].[Item] ([itemId], [ItemName], [quantity], [price], [unit], [supplier_id]) VALUES (305, N'Salt', 100, CAST(14.99 AS Decimal(10, 2)), N'kilo', 106)
INSERT INTO [dbo].[Item] ([itemId], [ItemName], [quantity], [price], [unit], [supplier_id]) VALUES (306, N'Pepper', 100, CAST(3.50 AS Decimal(10, 2)), N'grams', 106)
INSERT INTO [dbo].[Item] ([itemId], [ItemName], [quantity], [price], [unit], [supplier_id]) VALUES (307, N'Soy Sauce', 250, CAST(2.00 AS Decimal(10, 2)), N'ml', 106)
INSERT INTO [dbo].[Item] ([itemId], [ItemName], [quantity], [price], [unit], [supplier_id]) VALUES (308, N'Vinegar', 100, CAST(69.99 AS Decimal(10, 2)), N'ml', 106)
INSERT INTO [dbo].[Item] ([itemId], [ItemName], [quantity], [price], [unit], [supplier_id]) VALUES (309, N'Calamansi', 300, CAST(81.15 AS Decimal(10, 2)), N'kilo', 106)
INSERT INTO [dbo].[Item] ([itemId], [ItemName], [quantity], [price], [unit], [supplier_id]) VALUES (310, N'Garlic Powder', 120, CAST(2.50 AS Decimal(10, 2)), N'grams', 106)
SET IDENTITY_INSERT [dbo].[Item] OFF
