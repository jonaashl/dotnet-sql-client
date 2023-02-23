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
