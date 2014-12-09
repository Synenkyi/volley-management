﻿USE master;
GO

USE VolleyManagement;
GO

/*************************************************
  Inserting data to database
*************************************************/

INSERT INTO dbo.Tournament(
[Name],
[Scheme],
[Season],
[Description],
[RegulationsLink]
)
VALUES
('Первый чемпионат любительской лиги', '1', '2012/2013', 'Любительская лига',''),
('Второй чемпионат любительской лиги', '1', '2013/2014', 'Любительская лига','Volley.dp.ua'),
('Третий чемпионат любительской лиги', '2', '2014/2015', 'Любительская лига','123123'),
('Четвертый чемпионат любительской лиги', '3', '2014/2015', 'Любительская лига','Volleyball.ua'),
('Пятый чемпионат любительской лиги', '1', '2014/2015', 'Любительская лига','');
GO
