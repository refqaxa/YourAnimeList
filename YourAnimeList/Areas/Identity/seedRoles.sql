--Assign the Role to a User
USE [YourAnimeListDB]
GO

INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES ('', '');

-- Seed Animes
INSERT INTO [YourAnimeListDB].[dbo].[Animes]
([Id], [Name], [Description], [Episodes], [Aired], [AddedBy])
VALUES
(NEWID(), 'Attack on Titan', 'Humans fight Titans to survive.', 87, '2013-04-07', 'admin'),
(NEWID(), 'One Piece', 'Pirates seek the ultimate treasure.', 1000, '1999-10-20', 'admin'),
(NEWID(), 'Naruto', 'A ninja seeks recognition in his village.', 220, '2002-10-03', 'admin'),
(NEWID(), 'Bleach', 'A high schooler becomes a Soul Reaper.', 366, '2004-10-05', 'admin'),
(NEWID(), 'Demon Slayer', 'A boy battles demons to save his sister.', 44, '2019-04-06', 'admin'),
(NEWID(), 'My Hero Academia', 'Superpowered students train to become heroes.', 138, '2016-04-03', 'admin'),
(NEWID(), 'Fullmetal Alchemist: Brotherhood', 'Two brothers seek the Philosopher’s Stone.', 64, '2009-04-05', 'admin'),
(NEWID(), 'Death Note', 'A student gains the power to kill with a notebook.', 37, '2006-10-04', 'admin'),
(NEWID(), 'Hunter x Hunter', 'A boy becomes a Hunter to find his father.', 148, '2011-10-02', 'admin'),
(NEWID(), 'Sword Art Online', 'Players are trapped in a virtual reality MMORPG.', 96, '2012-07-08', 'admin'),

-- Next 10 records with AddedBy set to "test"
(NEWID(), 'One Punch Man', 'A hero defeats enemies with one punch.', 24, '2015-10-05', 'test'),
(NEWID(), 'Fairy Tail', 'Wizards form a guild to take on missions.', 328, '2009-10-12', 'test'),
(NEWID(), 'Tokyo Ghoul', 'A boy turns into a half-ghoul.', 48, '2014-07-04', 'test'),
(NEWID(), 'Dragon Ball Z', 'Earth’s defenders battle powerful foes.', 291, '1989-04-26', 'test'),
(NEWID(), 'Black Clover', 'Two friends seek to become the Wizard King.', 170, '2017-10-03', 'test'),
(NEWID(), 'Re:Zero', 'A boy is trapped in a fantasy world with a time loop.', 50, '2016-04-04', 'test'),
(NEWID(), 'Steins;Gate', 'A group invents time travel by accident.', 24, '2011-04-06', 'test'),
(NEWID(), 'The Rising of the Shield Hero', 'A hero defends a kingdom with a shield.', 38, '2019-01-09', 'test'),
(NEWID(), 'No Game No Life', 'Siblings enter a world where games decide everything.', 12, '2014-04-09', 'test'),
(NEWID(), 'Your Lie in April', 'A pianist is inspired to play again.', 22, '2014-10-09', 'test'),

-- Remaining records back to "admin"
(NEWID(), 'Erased', 'A man travels back in time to stop crimes.', 12, '2016-01-08', 'admin'),
(NEWID(), 'Made in Abyss', 'Explorers journey into a mysterious abyss.', 25, '2017-07-07', 'admin'),
(NEWID(), 'Violet Evergarden', 'An ex-soldier writes letters to connect with people.', 13, '2018-01-11', 'admin'),
(NEWID(), 'The Seven Deadly Sins', 'Exiled knights fight to reclaim their kingdom.', 100, '2014-10-05', 'admin'),
(NEWID(), 'Code Geass', 'A prince gains the power to control minds.', 50, '2006-10-05', 'admin');
