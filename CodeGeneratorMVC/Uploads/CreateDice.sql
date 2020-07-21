CREATE TABLE Rarites(
  RarityID int IDENTITY(1,1) PRIMARY KEY
  ,Name nvarchar(50) NOT NULL
)

CREATE TABLE Dice(
  DieID int IDENTITY(1,1) PRIMARY KEY
  ,Name nvarchar(50) NOT NULL
  ,RarityID int NOT NULL
)

CREATE TABLE Glyphs(
  GlyphID int IDENTITY(1,1) PRIMARY KEY
  ,Name nvarchar(50) NOT NULL
  ,ImagePath nvarchar(200) NOT NULL
)

CREATE TABLE Attributes(
  AttributeID int IDENTITY(1,1) PRIMARY KEY
  ,Name nvarchar(50) NOT NULL
  ,Color binary(32) NOT NULL
)

CREATE TABLE Sides(
  SideID int IDENTITY(1,1) PRIMARY KEY
  ,DieID int NOT NULL
  ,GlyphID int NOT NULL
  ,AttributeID int NOT NULL
  ,SwordPips int NOT NULL
  ,PlusPips int NOT NULL
  ,IsNamed bit NOT NULL
)

CREATE TABLE Characters(
  CharacterID int IDENTITY(1,1) PRIMARY KEY
  ,FirstName nvarchar(50) NOT NULL
  ,LastName nvarchar(50) NOT NULL
)

CREATE TABLE CharacterDice(
  CharacterDieID int IDENTITY(1,1) PRIMARY KEY
  ,CharacterID int NOT NULL
  ,DieID int NOT NULL
  ,OrderAdded int NOT NULL
)
