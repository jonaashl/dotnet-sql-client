CREATE TABLE SuperheroPowerRelation (
  [SuperheroId] INT NOT NULL,
  [PowerId] INT NOT NULL,
  PRIMARY KEY (SuperheroId, PowerId),
  FOREIGN KEY (SuperheroId) REFERENCES Superhero(Id),
  FOREIGN KEY (PowerId) REFERENCES Power(Id)
);