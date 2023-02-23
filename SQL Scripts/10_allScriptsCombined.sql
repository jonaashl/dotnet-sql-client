-- Didn't like running CREATE DATABASE and USE in same query, so run this first:
-- CREATE DATABASE SuperheroesDb;

USE SuperheroesDb

CREATE TABLE Superhero (
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(50),
	[Alias] NVARCHAR(50),
	[Origin] NVARCHAR(50)
);

CREATE TABLE Assistant (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [Name] NVARCHAR(50)
);

CREATE TABLE Power (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [Name] NVARCHAR(50),
  [Description] NVARCHAR(255)
);

ALTER TABLE Assistant
ADD [SuperheroId] INT NOT NULL
FOREIGN KEY (SuperheroId) REFERENCES Superhero(Id);

CREATE TABLE SuperheroPowerRelation (
  [SuperheroId] INT NOT NULL,
  [PowerId] INT NOT NULL,
  PRIMARY KEY (SuperheroId, PowerId),
  FOREIGN KEY (SuperheroId) REFERENCES Superhero(Id),
  FOREIGN KEY (PowerId) REFERENCES Power(Id)
);

-- Insert superheroes
INSERT INTO Superhero (Name, Alias, Origin)
VALUES	('Peter Parker', 'Spider-Man', 'New York City'),
		('Tony Stark', 'Iron Man', 'New York City'),
		('Steve Rogers', 'Captain America', 'Brooklyn');

-- Insert assistants and link them to superheroes
INSERT INTO Assistant (Name, SuperheroId)
VALUES	('Mary Jane Watson', 1),	-- Mary Jane Watson assists Spider-Man
		('Pepper Potts', 2),		-- Pepper Potts assists Iron Man
		('Bucky Barnes', 3);		-- Bucky Barnes assists Captain America

-- Insert powers
INSERT INTO Power (Name, Description)
VALUES	('Web-Slinging', 'Ability to shoot webs and swing through the air'),
		('Flight', 'Ability to fly using suit propulsion'),
		('Super Strength', 'Enhanced strength'),
		('Shield', 'Vibranium shield that can be used for both offense and defense');

-- Link powers to superheroes
INSERT INTO SuperheroPowerRelation (SuperheroId, PowerId)
VALUES	(1, 1),		-- Spider-Man has the power of web-slinging
		(1, 3),		-- Spider-Man har Super Strength
		(2, 2),		-- Iron Man can fly using his suit
		(3, 3),		-- Captain America has Super Strength
		(3, 4);		-- Captain America wields a vibranium shield

UPDATE Superhero
SET Name = 'Miles Morales'
WHERE Id = 1;

DELETE FROM Assistant WHERE [Name] = 'Pepper Potts';