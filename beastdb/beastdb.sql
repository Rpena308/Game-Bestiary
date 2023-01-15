/* Check whether the database exists, and if it does, 
	delete it before trying to create it again. */
print '' print '*** checking for database beast_db_pm'
GO

IF EXISTS(SELECT 1 
			FROM master.dbo.sysdatabases 
			WHERE name = 'beast_db_pm')
BEGIN
	DROP DATABASE beast_db_pm
	print '' print '*** dropping database beast_db_pm'
END
GO

print '' print '*** creating database beast_db_pm'
GO

CREATE DATABASE beast_db_pm
GO

print '' print '*** using database beast_db_pm'
USE beast_db_pm
GO

/* UserAccount table section */
print '' print '***creating UserAccount table'
GO
CREATE TABLE [dbo].[UserAccount](
	[UserAccountID]	[int] IDENTITY(100000,1)	NOT NULL,
	[GivenName]		[nvarchar](50)				NOT NULL,
	[FamilyName]	[nvarchar](50)				NOT NULL,
	[Email]			[nvarchar](100)				NOT NULL,
	[PasswordHash]	[nvarchar](100)				NOT NULL DEFAULT 
		'9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E',
	[Active]		[bit]						NOT NULL DEFAULT 1,
	CONSTRAINT [pk_UserAccountID] PRIMARY KEY([UserAccountID] ASC),
	CONSTRAINT [ak_Email] UNIQUE([Email] ASC)
)
GO

print '' print '*** adding sample UserAccount records ***'
GO
INSERT INTO [dbo].[UserAccount]
		([GivenName], [FamilyName], [Email])
	VALUES
		('Tom', 'Kelly', 'tom@beast.com'),
		('Red', 'Robin', 'red@beast.com')
GO

/* UserAccountRole table for UserAccount UserAccountRoles */
print '' print '*** creating UserAccountRole table ***'
GO
CREATE TABLE [dbo].[UserAccountRole] (
	[UserAccountRoleID]		[nvarchar](50),
	[Description]			[nvarchar](255),
	CONSTRAINT [pk_UserAccountRoleID] PRIMARY KEY([UserAccountRoleID])
)
GO

print '' print '*** adding sample UserAccountRole records ***'
GO
INSERT INTO [dbo].[UserAccountRole]
		([UserAccountRoleID], [Description])
	VALUES
		('Admin', 'Administers UserAccount accounts and UserAccountRoles'),
		('ChiefEditor', 'Manages Bestiary')
GO

print '' print '*** creating the UserAssignedRole (join) table ***'
GO
CREATE TABLE [dbo].[UserAssignedRole](
	[UserAccountID]			[int] NOT NULL,
	[UserAccountRoleID]		[nvarchar](50),
	CONSTRAINT [fk_Employee_UserAccountID] FOREIGN KEY([UserAccountID])	
				REFERENCES [dbo].[UserAccount]([UserAccountID]),
	CONSTRAINT [fk_UserAccountRole_UserAccountRoleID] FOREIGN KEY([UserAccountRoleID])
				REFERENCES [dbo].[UserAccountRole]([UserAccountRoleID]) ON UPDATE CASCADE,
	CONSTRAINT [pk_UserAssignedRole] PRIMARY KEY([UserAccountID], [UserAccountRoleID])
)
GO

print '' print '*** adding sample UserAccount UserAccountRole, UserAssignedRole records ***'
GO
INSERT INTO [dbo].[UserAssignedRole]
		([UserAccountID], [UserAccountRoleID])
	VALUES
		(100000, 'Admin'),
		(100000, 'ChiefEditor'),
		(100001, 'ChiefEditor')
GO

/* GameCompany Table */
print '' print '*** creating GameCompany table ***'
GO
CREATE TABLE [dbo].[GameCompany](
	[GameCompanyID]		[nvarchar](50)				NOT NULL,
	[Email]				[nvarchar](100)				NULL,
	[Website]			[nvarchar](100)				NULL,
	[Active]			[bit]						NOT NULL DEFAULT 1,
	CONSTRAINT [pk_GameCompanyID] PRIMARY KEY([GameCompanyID])
)
GO

print '' print '*** adding sample GameCompany records ***'
GO
INSERT INTO [dbo].[GameCompany]
	([GameCompanyID], [Email], [Website], Active)
	VALUES
		('Wizards of The Coast', "", 'company.wizards.com', 1),
		('Paizo', 'customer.service@paizo.com', 'paizo.com', 1)
GO

/* Game Table */
print '' print '*** creating Game table ***'
GO
CREATE TABLE [dbo].[Game](
	[GameID]			[nvarchar](50)				NOT NULL,
	[GameCompanyID]		[nvarchar](50)				NOT NULL,
	[GameVersion]		[nvarchar](50)				NOT NULL,
	[Active]			[bit]						NOT NULL DEFAULT 1,
	CONSTRAINT [pk_GameID] PRIMARY KEY([GameID]),
	CONSTRAINT [fk_GameCompanyID] FOREIGN KEY([GameCompanyID])
		REFERENCES [GameCompany]([GameCompanyID])	
)
GO

print '' print '*** adding sample Game records ***'
GO
INSERT INTO [dbo].[Game]
	([GameID], [GameCompanyID], [GameVersion], [Active])
	VALUES
		('Pathfinder', 'Paizo', 'Second Edition', 1),
		('Dungeons and Dragons', 'Wizards of The Coast', 'Third Edition', 1)
GO

/* Alignment table for Beast */
print '' print '*** creating Alignment table ***'
GO
CREATE TABLE [dbo].[Alignment] (
	[AlignmentID]			[nvarchar](50)	NOT NULL,
	[AlignmentDescription]	[nvarchar](3000) NULL,
	CONSTRAINT [pk_AlignmenteID] PRIMARY KEY([AlignmentID])
)
GO

print '' print '*** adding sample Alignment records ***'
GO
INSERT INTO [dbo].[Alignment]
		([AlignmentID], [AlignmentDescription])
	VALUES
		('Neutral', 'Neutral is an alignment with multiple meanings, not all of which may apply to an individual.
		It is most commonly associated with those who do not have any particular moral inclinations,
		and is sometimes referred to as true neutrality'),
		('Neutral Evil', 'The neutral evil alignment promotes pain, anguish, misery, corruption, and destruction as tool to be used for the individual''s gain.'),
		('Lawful Good', 'Lawful good characters act as a good person is expected or required to act,
		combining a commitment to oppose evil with the discipline to fight relentlessly. ')
GO

/* BeastType table for Beast BeastType */
print '' print '*** creating BeastType table ***'
GO
CREATE TABLE [dbo].[BeastType] (
	[BeastTypeID]			[nvarchar](50)	NOT NULL,
	[BeastTypeDescription]	[nvarchar](3000) NULL,
	CONSTRAINT [pk_BeastTypeID] PRIMARY KEY([BeastTypeID])
)
GO

print '' print '*** adding sample BeastType records ***'
GO
INSERT INTO [dbo].[BeastType]
		([BeastTypeID], [BeastTypeDescription])
	VALUES
		('Humanoid', 'Humanoid is a broad classification, or type,
		of creatures that reasonably closely resemble humans.'),
		('Ooze', 'Oozes are a classification of simple,
		strange mutable creatures usually existing in an amorphous state.')
GO

/* BeastSubType table for Beast BeastSubType */
print '' print '*** creating BeastSubType table ***'
GO
CREATE TABLE [dbo].[BeastSubType] (
	[BeastSubTypeID]			[nvarchar](50)	NOT NULL,
	[BeastSubTypeDescription]	[nvarchar](3000) NULL,
	CONSTRAINT [pk_BeastSubTypeID] PRIMARY KEY([BeastSubTypeID])
)
GO

print '' print '*** adding sample BeastSubType records ***'
GO
INSERT INTO [dbo].[BeastSubType]
		([BeastSubTypeID], [BeastSubTypeDescription])
	VALUES
		('Aquatic', 'These creatures always have swim speeds and can move in water without making Swim checks.
		 	An aquatic creature can breathe water. It cannot breathe air unless it has the amphibious special quality.
			Aquatic creatures always treat Swim as a class skill'),
		('Human', 'This subtype is applied to humans and creatures related to humans.'),
		('Undead', 'Undead are once-living creatures animated by spiritual or supernatural forces.')
GO

/* Terrain table for Beast Terrain */
print '' print '*** creating Terrain table ***'
GO
CREATE TABLE [dbo].[Terrain] (
	[TerrainID]				[nvarchar](50)	NOT NULL,
	[TerrainDescription]	[nvarchar](3000) NULL,
	CONSTRAINT [pk_TerrainID] PRIMARY KEY([TerrainID])
)
GO

print '' print '*** adding sample Terrain records ***'
GO
INSERT INTO [dbo].[Terrain]
		([TerrainID], [TerrainDescription])
	VALUES
		('Aquatic', 'Aquatic terrain is the least hospitable to most PCs, because they can’t breathe there. Aquatic terrain doesn’t offer the variety that land terrain does. The ocean floor holds many marvels, including undersea analogues of any of the terrain elements described earlier in this section, but if characters find themselves in the water because they were bull rushed off the deck of a pirate ship, the tall kelp beds hundreds of feet below them don’t matter. Accordingly, these rules simply divide aquatic terrain into two categories: flowing water (such as streams and rivers) and non-flowing water (such as lakes and oceans)'),
		('Desert', 'Desert terrain exists in warm, temperate, and cold climates, but all deserts share one common trait: little rain. The three categories of desert terrain are tundra (cold desert), rocky deserts (often temperate), and sandy deserts (often warm)')
GO

/* BeastSize table for BeastSize */
print '' print '*** creating BeastSize table ***'
GO
CREATE TABLE [dbo].[BeastSize] (
	[BeastSizeID]			[nvarchar](50)	NOT NULL,
	[BeastSizeDescription]	[nvarchar](3000) NULL,
	CONSTRAINT [pk_BeastSizeID] PRIMARY KEY([BeastSizeID])
)
GO

print '' print '*** adding sample BeastSize records ***'
GO
INSERT INTO [dbo].[BeastSize]
		([BeastSizeID], [BeastSizeDescription])
	VALUES
		('Large', '8′ to 16 ft'),
		('Small', '2′ to 4 ft.')
GO

/*Creating Beast Table */
print '' print '*** creating Beast table ***'
GO
CREATE TABLE [dbo].[Beast](
	[BeastID]			[int] IDENTITY(100000,1)	NOT NULL,
	[GameID]			[nvarchar](50)				NOT NULL,
	[AlignmentID]		[nvarchar](50)				NOT NULL,
	[BeastTypeID]		[nvarchar](50)				NOT NULL,
	[BeastSubTypeID]	[nvarchar](50)				NOT NULL,
	[TerrainID]			[nvarchar](50)				NOT NULL,
	[BeastSizeID]		[nvarchar](50)				NOT NULL,
	[BeastName]			[nvarchar](250) 			NOT NULL,
	[ChallengeRating]	[int] 						NOT NULL,
	[Treasure]			[nvarchar](250)				NOT NULL,
	[Experience]		[int]						NOT NULL,
	[BeastDescription]	[nvarchar](250)				NOT NULL,
	[Active]			[bit]						NOT NULL DEFAULT 0,
	CONSTRAINT [pk_BeastID] PRIMARY KEY([BeastID]),
	CONSTRAINT [fk_GameID] FOREIGN KEY([GameID])
		REFERENCES [Game]([GameID]),
	CONSTRAINT [fk_AlignmentID] FOREIGN KEY([AlignmentID])
		REFERENCES [Alignment]([AlignmentID]),
	CONSTRAINT [fk_BeastTypeID] FOREIGN KEY([BeastTypeID])
		REFERENCES [BeastType]([BeastTypeID]),
	CONSTRAINT [fk_BeastSubTypeID] FOREIGN KEY([BeastSubTypeID])
		REFERENCES [BeastSubType]([BeastSubTypeID]),
	CONSTRAINT [fk_TerrainID] FOREIGN KEY([TerrainID])
		REFERENCES [Terrain]([TerrainID]),
	CONSTRAINT [fk_BeastSizeID] FOREIGN KEY([BeastSizeID])
		REFERENCES [BeastSize]([BeastSizeID])					
)
GO

print '' print '*** Adding sample Beast records ***'
GO
	INSERT INTO [dbo].[Beast]
		([GameID], [AlignmentID], [BeastTypeID], [BeastSubTypeID], 
		[TerrainID], [BeastSizeID], [BeastName], [ChallengeRating], [Treasure],
		[Experience], [BeastDescription], [Active])
		VALUES
		('Pathfinder', 'Neutral Evil', 'Humanoid', 'Undead', 'Desert', 'Large', 'Dread Zombie Cyclops', 6, 'Large Club', 1700, 'A Large Undead Cyclops', 1),
		('Pathfinder', 'Neutral', 'Ooze', 'Aquatic', 'Aquatic', 'Large', 'Sea Scourge', 6, 'Clump of Goo', 1700, 'A large amorphic Aquatic ooze', 0)
GO

/* stored procedures for login */
print '' print '*** creating sp_authenticate_user ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
(
	@Email 				[nvarchar](100),
	@PasswordHash		[nvarchar](100)
)
AS
	BEGIN
		SELECT COUNT([UserAccountID]) AS 'Authenticated'
		FROM 	[UserAccount]
		WHERE 	@Email = [Email]
		  AND	@PasswordHash = [PasswordHash]
		  AND	Active = 1
	END
GO

CREATE PROCEDURE [dbo].[sp_select_all_UserAccounts]
AS
	BEGIN
		SELECT 	[UserAccountID], [GivenName], [FamilyName],
				[Email], [Active]
		FROM 	[UserAccount]
	END
GO

print '' print '*** creating sp_select_user_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
(
	@Email 				[nvarchar](100)
)
AS
	BEGIN
		SELECT 	[UserAccountID], [GivenName], [FamilyName],
				[Email], [Active]
		FROM 	[UserAccount]
		WHERE 	@Email = [Email]
	END
GO

print '' print '*** creating sp_select_user_by_userAccountID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_userAccountID]
(
	@UserAccountID 				[int]
)
AS
	BEGIN
		SELECT 	[UserAccountID], [GivenName], [FamilyName],
				[Email], [Active]
		FROM 	[UserAccount]
		WHERE 	@UserAccountID = [UserAccountID]
	END
GO

print '' print '*** creating sp_select_UserAccountRoles_by_UserAccountID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_UserAccountRoles_by_UserAccountID]
(
	@UserAccountID 		int
)
AS
	BEGIN
		SELECT 	[UserAccountRoleID]
		FROM 	[UserAssignedRole]
		WHERE 	@UserAccountID = [UserAccountID]
	END
GO

print '' print '*** creating sp_update_passwordHash ***'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordHash]
(
	@Email 				[nvarchar](100),
	@OldPasswordHash	[nvarchar](100),
	@NewPasswordHash	[nvarchar](100)
)
AS
	BEGIN
		UPDATE	[UserAccount]
		SET		[PasswordHash] = @NewPasswordHash
		WHERE 	@EmaIl = [Email]
		  AND	@OldPasswordHash = [PasswordHash]
		RETURN @@ROWCOUNT
	END
GO


----------------------------------------------------- Start Here

print '' print '*** creating sp_select_beast_by_BeastID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_beast_by_BeastID]
(
	@BeastID				[int]
)
AS
	BEGIN
		SELECT 	[BeastID], [GameID], [AlignmentID], [BeastTypeID], [BeastSubTypeID], 
			[TerrainID], [BeastSizeID], [BeastName], [ChallengeRating], [Treasure],
			[Experience], [BeastDescription], [Active]
		FROM [Beast]
		WHERE 	@BeastID	= [BeastID]
	END
GO

print '' print '*** creating sp_select_Beasts_by_BeastType ***'
GO
CREATE PROCEDURE [dbo].[sp_select_Beasts_by_BeastType]
(
	@BeastTypeID 				[nvarchar](50)
)
AS
	BEGIN
		SELECT 	[BeastID], [GameID], [AlignmentID], [BeastTypeID], [BeastSubTypeID], 
			[TerrainID], [BeastSizeID], [BeastName], [ChallengeRating], [Treasure],
			[Experience], [BeastDescription], [Active]
		FROM [Beast]
		WHERE @BeastTypeID = BeastTypeID
	END
GO

print '' print '*** creating sp_select_active_Beasts_by_BeastType ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_Beasts_by_BeastType]
(
	@BeastTypeID 				[nvarchar](50)
)
AS
	BEGIN
		SELECT 	[BeastID], [GameID], [AlignmentID], [BeastTypeID], [BeastSubTypeID], 
			[TerrainID], [BeastSizeID], [BeastName], [ChallengeRating], [Treasure],
			[Experience], [BeastDescription], [Active]
		FROM [Beast]
		WHERE @BeastTypeID = BeastTypeID
		AND [Beast].[Active] = 1
	END
GO

print '' print '*** creating sp_select_all_Beasts ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_Beasts]
AS
	BEGIN
		SELECT 	[BeastID], [GameID], [AlignmentID], [BeastTypeID], [BeastSubTypeID], 
			[TerrainID], [BeastSizeID], [BeastName], [ChallengeRating], [Treasure],
			[Experience], [BeastDescription], [Active]
		FROM [Beast]
	END
GO

print '' print '*** creating sp_select_all_active_Beasts ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_active_Beasts]
AS
	BEGIN
		SELECT 	[BeastID], [GameID], [AlignmentID], [BeastTypeID], [BeastSubTypeID], 
			[TerrainID], [BeastSizeID], [BeastName], [ChallengeRating], [Treasure],
			[Experience], [BeastDescription], [Active]
		FROM [Beast]
		WHERE Beast.Active = 1 
	END
GO

print '' print '*** creating sp_select_all_active_GameCompanys ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_active_GameCompanys]
AS
	BEGIN
		SELECT 	
			[GameCompanyID], [Email],
			[Website], [Active]
		FROM [GameCompany]
		WHERE [Active] = 1
	END
GO

print '' print '*** creating sp_select_all_GameCompanys ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_GameCompanys]
AS
	BEGIN
		SELECT 	[GameCompanyID], [Website],
		 		[Email], [Active]
		FROM [GameCompany]
	END


print '' print '*** creating sp_select_all_active_Games ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_active_Games]
AS
	BEGIN
		SELECT 	
			[GameID],[GameCompanyID], [GameVersion], [Active]
		FROM [Game]
		WHERE [Active] = 1
	END
GO

print '' print '*** creating sp_select_all_Games ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_Games]
AS
	BEGIN
		SELECT 	
			[GameID],[GameCompanyID], [GameVersion], [Active]
		FROM [Game]
	END
GO

print '' print '*** creating sp_select_all_Alignments ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_Alignments]
AS
	BEGIN
		SELECT 	
			[AlignmentID],[AlignmentDescription]
		FROM [Alignment]
	END
GO

print '' print '*** creating sp_select_all_BeastTypes ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_BeastTypes]
AS
	BEGIN
		SELECT 	
			[BeastTypeID],[BeastTypeDescription]
		FROM [BeastType]
	END
GO

print '' print '*** creating sp_select_all_BeastSubTypes ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_BeastSubTypes]
AS
	BEGIN
		SELECT 	
			[BeastSubTypeID],[BeastSubTypeDescription]
		FROM [BeastSubType]
	END
GO

print '' print '*** creating sp_select_all_Terrains ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_Terrains]
AS
	BEGIN
		SELECT 	
			[TerrainID],[TerrainDescription]
		FROM [Terrain]
	END
GO

print '' print '*** creating sp_select_all_BeastSizes ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_BeastSizes]
AS
	BEGIN
		SELECT 	
			[BeastSizeID],[BeastSizeDescription]
		FROM [BeastSize]
	END
GO

print '' print '*** creating sp_select_Beast_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_Beast_by_id]
(	
	@BeastID 				[nvarchar](25)
)
AS
	BEGIN
		SELECT 	[BeastID], [GameID], [AlignmentID], [BeastTypeID], [BeastSubTypeID], 
			[TerrainID], [BeastSizeID], [BeastName], [ChallengeRating], [Treasure],
			[Experience], [BeastDescription], [Active]
		FROM [Beast]
		WHERE @BeastID = [BeastID]
	END
GO

print '' print '*** creating sp_update_Beast ***'
GO
CREATE PROCEDURE [dbo].[sp_update_Beast]
(
	@BeastID 				[int],
	@OldGameID				[nvarchar](50),
	@OldAlignmentID			[nvarchar](50),
	@OldBeastTypeID			[nvarchar](50),
	@OldBeastSubTypeID		[nvarchar](50),
	@OldTerrainID			[nvarchar](50),
	@OldBeastSizeID			[nvarchar](50),
	@OldBeastName			[nvarchar](250),
	@OldChallengeRating		[int],
	@OldTreasure			[nvarchar](250),
	@OldExperience			[int],
	@OldBeastDescription	[nvarchar](250),
	@OldActive				[bit],
	@NewGameID				[nvarchar](50),
	@NewAlignmentID			[nvarchar](50),
	@NewBeastTypeID			[nvarchar](50),
	@NewBeastSubTypeID		[nvarchar](50),
	@NewTerrainID			[nvarchar](50),
	@NewBeastSizeID			[nvarchar](50),
	@NewBeastName			[nvarchar](250),
	@NewChallengeRating		[int],
	@NewTreasure			[nvarchar](250),
	@NewExperience			[int],
	@NewBeastDescription	[nvarchar](250),
	@NewActive				[bit]
)
AS
	BEGIN
		UPDATE 	[Beast]
			SET		[GameID]				= @NewGameID,
					[AlignmentID]			= @NewAlignmentID,
					[BeastTypeID]			= @NewBeastTypeID,
					[BeastSubTypeID]		= @NewBeastSubTypeID,
					[TerrainID]				= @NewTerrainID,
					[BeastSizeID]			= @NewBeastSizeID,
					[BeastName]				= @NewBeastName,
					[ChallengeRating]		= @NewChallengeRating,
					[Treasure]				= @NewTreasure,
					[Experience]			= @NewExperience,
					[BeastDescription]		= @NewBeastDescription,
					[Active]				= @NewActive
		WHERE 		[BeastID] 				= @BeastID
		AND			[GameID]				= @OldGameID
		AND			[AlignmentID]			= @OldAlignmentID
		AND			[BeastTypeID]			= @OldBeastTypeID
		AND			[BeastSubTypeID]		= @OldBeastSubTypeID
		AND			[TerrainID]				= @OldTerrainID
		AND			[BeastSizeID]			= @OldBeastSizeID
		AND			[BeastName]				= @OldBeastName
		AND			[ChallengeRating]		= @OldChallengeRating
		AND			[Treasure]				= @OldTreasure
		AND			[Experience]			= @OldExperience
		AND			[BeastDescription]		= @OldBeastDescription
		AND			[Active]				= @OldActive
	END
GO

print '' print '*** creating sp_insert_Beast ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_Beast]
(
	@GameID 			[nvarchar](50),
	@AlignmentID		[nvarchar](50),
	@BeastTypeID		[nvarchar](50),
	@BeastSubTypeID		[nvarchar](50),
	@TerrainID			[nvarchar](50),
	@BeastSizeID		[nvarchar](50),
	@BeastName			[nvarchar](250),
	@ChallengeRating	[int],
	@Treasure			[nvarchar](250),
	@Experience			[int],
	@BeastDescription	[nvarchar](250),
	@Active				[bit]
)
AS
	BEGIN
		INSERT INTO [dbo].[Beast]
		([Beast].[GameID], [Beast].[AlignmentID], [Beast].[BeastTypeID], [Beast].[BeastSubTypeID], 
		[Beast].[TerrainID], [Beast].[BeastSizeID], [BeastName], [ChallengeRating], [Treasure],
		[Experience], [BeastDescription], [Active])
		VALUES
		(@GameID, @AlignmentID, @BeastTypeID, @BeastSubTypeID,
		 @TerrainID, @BeastSizeID, @BeastName, @ChallengeRating, @Treasure,
		 @Experience, @BeastDescription, @Active)
	END
GO

print '' print '*** creating sp_update_UserAccount ***'
GO
CREATE PROCEDURE [dbo].[sp_update_UserAccount]
(
	@UserAccountID			[int],
	@OldGivenName				[nvarchar](50),
	@OldFamilyName				[nvarchar](50),
	@OldEmail					[nvarchar](100),
	@OldActive					[bit],
	@NewGivenName				[nvarchar](50),
	@NewFamilyName				[nvarchar](50),
	@NewEmail					[nvarchar](100),
	@NewActive					[bit]
)
AS
	BEGIN
		UPDATE 	[UserAccount]
			SET		[GivenName]				= @NewGivenName,
					[FamilyName]			= @NewFamilyName,
					[Email]					= @NewEmail,
					[Active]				= @NewActive
		WHERE 		[GivenName] 			= @OldGivenName
		AND			[FamilyName]			= @OldFamilyName
		AND			[Email]					= @OldEmail
		AND			[Active]				= @OldActive
	END
GO

print '' print '*** creating sp_insert_UserAccount ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_UserAccount]
(
	@GivenName				[nvarchar](50),
	@FamilyName				[nvarchar](50),
	@Email					[nvarchar](100)
)
AS
	BEGIN
		INSERT INTO [dbo].[UserAccount]
		([GivenName], [FamilyName], [Email])
		VALUES
		(@GivenName, @FamilyName, @Email)
	END
GO

print '' print '*** creating sp_update_GameCompany ***'
GO
CREATE PROCEDURE [dbo].[sp_update_GameCompany]
(
	@GameCompanyID				[nvarchar](50),
	@OldEmail					[nvarchar](100),
	@OldWebsite					[nvarchar](100),
	@OldActive					[bit],
	@NewEmail					[nvarchar](100),
	@NewWebsite					[nvarchar](100),
	@NewActive					[bit]
)
AS
	BEGIN
		UPDATE 	[GameCompany]
			SET		[Email]					= @NewEmail,
					[Website]				= @NewWebsite,
					[Active]				= @NewActive
		WHERE 		[Email]					= @OldEmail
		AND			[Website]				= @OldWebsite
		AND			[Active]				= @OldActive
	END
GO

print '' print '*** creating sp_insert_GameCompany ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_GameCompany]
(
	@GameCompanyID			[nvarchar](50),
	@Email					[nvarchar](100),
	@Website				[nvarchar](100),
	@Active					[bit]
)
AS
	BEGIN
		INSERT INTO [dbo].[GameCompany]
		([GameCompanyID], [Email], [Website], [Active])
		VALUES
		(@GameCompanyID , @Email, @Website, @Active)
	END
GO

print '' print '*** creating sp_update_Game ***'
GO
CREATE PROCEDURE [dbo].[sp_update_Game]
(
	@GameID						[nvarchar](50),
	@OldGameCompanyID			[nvarchar](50),
	@OldGameVersion				[nvarchar](50),
	@OldActive					[bit],
	@NewGameCompanyID			[nvarchar](50),
	@NewGameVersion				[nvarchar](50),
	@NewActive					[bit]
)
AS
	BEGIN
		UPDATE 	[Game]
			SET		[GameCompanyID]			= @NewGameCompanyID,
					[GameVersion]			= @NewGameVersion,
					[Active]				= @NewActive
		WHERE 		[GameID]				= @GameID
		AND			[GameCompanyID]			= @OldGameCompanyID
		AND			[GameVersion]			= @OldGameVersion
		AND			[Active]				= @OldActive
	END
GO

print '' print '*** creating sp_insert_Game ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_Game]
(
	@GameID					[nvarchar](50),
	@GameCompanyID			[nvarchar](50),
	@GameVersion			[nvarchar](50),
	@Active					[bit]
)
AS
	BEGIN
		INSERT INTO [dbo].[Game]
		([GameID], [GameCompanyID], [GameVersion], [Active])
		VALUES
		(@GameID, @GameCompanyID, @GameVersion, @Active)
	END
GO
