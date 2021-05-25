CREATE DATABASE QLYNhaHang
GO

USE QLYNhaHang
GO
--Staff
--Food
--Table
--Food Categories
--LoginAccount
--Bill
--BillInfor

CREATE TABLE TableFood
(
	id INT IDENTITY PRIMARY KEY,
	tname NVARCHAR(100),
	stat NVARCHAR(100) DEFAULT N'Trống', --trống || đã đặt
)
GO
CREATE TABLE Account
(
	id INT IDENTITY PRIMARY KEY,
	Username NVARCHAR(100) NOT NULL,
	Password NVARCHAR(1000) NOT NULL,
	DisplayName NVARCHAR(100) NOT NULL,
	Type INT NOT NULL DEFAULT 0-- 1 Manager ,0 STAFF,
)
GO
CREATE TABLE FoodCatories
(
	id INT IDENTITY PRIMARY KEY,
	fcname NVARCHAR(100) NOT NULL DEFAULT N'Chưa Đặt Tên',
	
)
GO
CREATE TABLE Food
(
	id INT IDENTITY PRIMARY KEY,
	fname NVARCHAR(100) DEFAULT N'Chưa Đặt Tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0
	FOREIGN KEY (idCategory) REFERENCES dbo.FoodCatories(id)
)
GO
CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	fname NVARCHAR(100),
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	idTable INT NOT NULL,
	stat INT NOT NULL DEFAULT 0--- 1: đã thanh toán, 0: chưa thanh toán
	FOREIGN KEY (idTable) REFERENCES dbo.TableFood(id)
)
GO
CREATE TABLE BillInfor
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	count INT NOT NULL DEFAULT 0
	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
	FOREIGN KEY (idFood) REFERENCES dbo.Food(id)
)
GO