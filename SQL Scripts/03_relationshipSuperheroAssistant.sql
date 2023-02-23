ALTER TABLE Assistant
ADD [SuperheroId] INT NOT NULL
FOREIGN KEY (SuperheroId) REFERENCES Superhero(Id);