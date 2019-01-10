-- SELECT b.megnevezes, b.alapanyagid, b.meret, b.ar, b.id, a.megnevezes FROM butorok b INNER JOIN alapanyagok a ON b.alapanyagid = a.id
--   WHERE 1 = 1;

-- INSERT INTO butorok ( megnevezes, alapanyagid, meret, ar)
--   VALUES ('Komód', 1, 'kicsi', 3456.88);
  --
-- Create database "`14KE_Autokereskedes`"
--
CREATE DATABASE `14KE_Autokereskedes`
	CHARACTER SET utf8
	COLLATE utf8_hungarian_ci;

USE `14KE_Autokereskedes`;

CREATE TABLE AutoTipus (
  Id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Megnevezes varchar(100)NOT NULL UNIQUE
  );
CREATE TABLE Auto (
  Id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Rendszam varchar(6) NOT NULL UNIQUE,
  AutoTipusId int NOT NULL,
  Alvazszam varchar(30),
  Motorszam varchar(30),
  ElsoForgalombaHelyezes datetime,
  AutomataValto bit NOT NULL DEFAULT 0,
  KmOraAllas int,
  Uzemanyag int NOT NULL,
  CONSTRAINT FK_Auto_AutoTipusId FOREIGN KEY (AutoTipusId)
    REFERENCES AutoTipus(Id)
);

INSERT INTO AutoTipus (Megnevezes)
  VALUES ("Lada"),
          ("BMW"),
          ("Audi"),
          ("VW");
INSERT INTO Auto(Rendszam, AutoTipusId, Alvazszam, Motorszam, ElsoForgalombaHelyezes, AutomataValto, KmOraAllas, Uzemanyag)
  VALUES ( 'ABC123',2 , 'qwert456', 'mnbvc1234', NOW(), 0, 0, 2);  