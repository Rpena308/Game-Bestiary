using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BeastAccessor : IBeastAccessor
    {
        public List<Beast> SelectAllActiveBeasts()
        {
            List<Beast> beasts = new List<Beast>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_all_active_Beasts";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        beasts.Add(new Beast()
                        {
                            BeastID = reader.GetInt32(0),
                            GameID = reader.GetString(1),
                            AlignmentID = reader.GetString(2),
                            BeastTypeID = reader.GetString(3),
                            BeastSubTypeID = reader.GetString(4),
                            TerrainID = reader.GetString(5),
                            BeastSizeID = reader.GetString(6),
                            BeastName = reader.GetString(7),
                            ChallengeRating = reader.GetInt32(8),
                            Treasure = reader.GetString(9),
                            Experience = reader.GetInt32(10),
                            BeastDescription = reader.GetString(11),
                            Active = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return beasts;
        }

        public List<Game> SelectAllActiveGames()
        {
            List<Game> games = new List<Game>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_all_active_Games";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        games.Add(new Game()
                        {
                            GameID = reader.GetString(0),
                            GameCompanyID = reader.GetString(1),
                            GameVersion = reader.GetString(2),
                            Active = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return games;
        }

        public List<Game> SelectAllGames()
        {
            List<Game> games = new List<Game>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_all_Games";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        games.Add(new Game()
                        {
                            GameID = reader.GetString(0),
                            GameCompanyID = reader.GetString(1),
                            GameVersion = reader.GetString(2),
                            Active = reader.GetBoolean(3)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return games;
        }

        public List<Beast> SelectAllBeasts()
        {
            List<Beast> beasts = new List<Beast>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_all_beasts";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        beasts.Add(new Beast()
                        {
                            BeastID = reader.GetInt32(0),
                            GameID = reader.GetString(1),
                            AlignmentID = reader.GetString(2),
                            BeastTypeID = reader.GetString(3),
                            BeastSubTypeID = reader.GetString(4),
                            TerrainID = reader.GetString(5),
                            BeastSizeID = reader.GetString(6),
                            BeastName = reader.GetString(7),
                            ChallengeRating = reader.GetInt32(8),
                            Treasure = reader.GetString(9),
                            Experience = reader.GetInt32(10),
                            BeastDescription = reader.GetString(11),
                            Active = reader.GetBoolean(12)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return beasts;
        }

        public List<GameCompany> SelectAllGameCompanies()
        {
            List<GameCompany> companies = new List<GameCompany>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_all_GameCompanys";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        companies.Add(new GameCompany()
                        {
                            GameCompanyID = reader.GetString(0),
                            Website = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            Email = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            Active = reader.GetBoolean(3)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return companies;
        }

        public List<GameCompany> SelectAllActiveGameCompanies()
        {
            List<GameCompany> companies = new List<GameCompany>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_all_active_GameCompanys";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        companies.Add(new GameCompany()
                        {
                            GameCompanyID = reader.GetString(0),
                            Email = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            Website = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            Active = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return companies;
        }

        public List<Beast> SelectBeastsByBeastType(string BeastTypeID)
        {
            List<Beast> beasts = new List<Beast>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_active_Beasts_by_BeastType";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@BeastTypeID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@BeastTypeID"].Value = BeastTypeID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        beasts.Add(new Beast()
                        {
                            BeastID = reader.GetInt32(0),
                            GameID = reader.GetString(1),
                            AlignmentID = reader.GetString(2),
                            BeastTypeID = reader.GetString(3),
                            BeastSubTypeID = reader.GetString(4),
                            TerrainID = reader.GetString(5),
                            BeastSizeID = reader.GetString(6),
                            BeastName = reader.GetString(7),
                            ChallengeRating = reader.GetInt32(8),
                            Treasure = reader.IsDBNull(9) ? null : reader.GetString(9),
                            Experience = reader.GetInt32(10),
                            BeastDescription = reader.IsDBNull(11) ? null : reader.GetString(11),
                            Active = reader.GetBoolean(12)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return beasts;
        }

        public Beast SelectBeastByBeastID(int BeastID)
        {
            Beast beast = null;

            var conn = DBConnection.GetConnection();

            var cmdText = "sp_select_beast_by_BeastID";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BeastID", SqlDbType.Int);

            cmd.Parameters["@BeastID"].Value = BeastID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    beast = new Beast()
                    {
                        BeastID = reader.GetInt32(0),
                        GameID = reader.GetString(1),
                        AlignmentID = reader.GetString(2),
                        BeastTypeID = reader.GetString(3),
                        BeastSubTypeID = reader.GetString(4),
                        TerrainID = reader.GetString(5),
                        BeastSizeID = reader.GetString(6),
                        BeastName = reader.GetString(7),
                        ChallengeRating = reader.GetInt32(8),
                        Treasure = reader.GetString(9),
                        Experience = reader.GetInt32(10),
                        BeastDescription = reader.GetString(11),
                        Active = reader.GetBoolean(12)
                    };
                }
                else
                {
                    throw new ApplicationException("Beast not found.");
                }
            }
            catch (Exception)
            {
                throw;
            }
            return beast;
        }

        public int UpdateBeast(Beast oldBeast, Beast newBeast)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_update_Beast";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BeastID", oldBeast.BeastID);
            cmd.Parameters.AddWithValue("@OldGameID", oldBeast.GameID);
            cmd.Parameters.AddWithValue("@OldAlignmentID", oldBeast.AlignmentID);
            cmd.Parameters.AddWithValue("@OldBeastTypeID", oldBeast.BeastTypeID);
            cmd.Parameters.AddWithValue("@OldBeastSubTypeID", oldBeast.BeastSubTypeID);
            cmd.Parameters.AddWithValue("@OldTerrainID", oldBeast.TerrainID);
            cmd.Parameters.AddWithValue("@OldBeastSizeID", oldBeast.BeastSizeID);
            cmd.Parameters.AddWithValue("@OldBeastName", oldBeast.BeastName);
            cmd.Parameters.AddWithValue("@OldChallengeRating", oldBeast.ChallengeRating);
            cmd.Parameters.AddWithValue("@OldTreasure", oldBeast.Treasure);
            cmd.Parameters.AddWithValue("@OldExperience", oldBeast.Experience);
            cmd.Parameters.AddWithValue("@OldBeastDescription", oldBeast.BeastDescription);
            cmd.Parameters.AddWithValue("@OldActive", oldBeast.Active);

            cmd.Parameters.Add("@NewGameID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewGameID"].Value = newBeast.GameID;

            cmd.Parameters.Add("@NewAlignmentID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewAlignmentID"].Value = newBeast.AlignmentID;

            cmd.Parameters.Add("@NewBeastTypeID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewBeastTypeID"].Value = newBeast.BeastTypeID;


            cmd.Parameters.Add("@NewBeastSubTypeID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewBeastSubTypeID"].Value = newBeast.BeastSubTypeID;

            cmd.Parameters.Add("@NewTerrainID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewTerrainID"].Value = newBeast.TerrainID;


            cmd.Parameters.Add("@NewBeastSizeID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewBeastSizeID"].Value = newBeast.BeastSizeID;

            cmd.Parameters.Add("@NewBeastName", SqlDbType.NVarChar, 250);
            cmd.Parameters["@NewBeastName"].Value = newBeast.BeastName;

            cmd.Parameters.Add("@NewChallengeRating", SqlDbType.Int);
            cmd.Parameters["@NewChallengeRating"].Value = newBeast.ChallengeRating;

            cmd.Parameters.Add("@NewTreasure", SqlDbType.NVarChar, 250);
            cmd.Parameters["@NewTreasure"].Value = newBeast.Treasure;

            cmd.Parameters.Add("@NewExperience", SqlDbType.Int);
            cmd.Parameters["@NewExperience"].Value = newBeast.Experience;

            cmd.Parameters.Add("@NewBeastDescription", SqlDbType.NVarChar, 250);
            cmd.Parameters["@NewBeastDescription"].Value = newBeast.BeastDescription;

            cmd.Parameters.Add("@NewActive", SqlDbType.Bit);
            cmd.Parameters["@NewActive"].Value = newBeast.Active;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        }

        public int InsertBeast(Beast newBeast)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_Beast";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@GameID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@GameID"].Value = newBeast.GameID;

            cmd.Parameters.Add("@AlignmentID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@AlignmentID"].Value = newBeast.AlignmentID;

            cmd.Parameters.Add("@BeastTypeID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@BeastTypeID"].Value = newBeast.BeastTypeID;


            cmd.Parameters.Add("@BeastSubTypeID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@BeastSubTypeID"].Value = newBeast.BeastSubTypeID;

            cmd.Parameters.Add("@TerrainID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@TerrainID"].Value = newBeast.TerrainID;


            cmd.Parameters.Add("@BeastSizeID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@BeastSizeID"].Value = newBeast.BeastSizeID;

            cmd.Parameters.Add("@BeastName", SqlDbType.NVarChar, 250);
            cmd.Parameters["@BeastName"].Value = newBeast.BeastName;

            cmd.Parameters.Add("@ChallengeRating", SqlDbType.Int);
            cmd.Parameters["@ChallengeRating"].Value = newBeast.ChallengeRating;

            cmd.Parameters.Add("@Treasure", SqlDbType.NVarChar, 250);
            cmd.Parameters["@Treasure"].Value = newBeast.Treasure;

            cmd.Parameters.Add("@Experience", SqlDbType.Int);
            cmd.Parameters["@Experience"].Value = newBeast.Experience;

            cmd.Parameters.Add("@BeastDescription", SqlDbType.NVarChar, 250);
            cmd.Parameters["@BeastDescription"].Value = newBeast.BeastDescription;

            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters["@Active"].Value = newBeast.Active;
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        }

        public List<Alignment> SelectAllAlignments()
        {
            List<Alignment> alignments = new List<Alignment>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_all_Alignments";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        alignments.Add(new Alignment()
                        {
                            AlignmentID = reader.GetString(0),
                            AlignmentDescription = reader.GetString(1)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return alignments;
        }

        public List<BeastType> SelectAllBeastTypes()
        {
            List<BeastType> beastTypes = new List<BeastType>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_all_BeastTypes";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        beastTypes.Add(new BeastType()
                        {
                            BeastTypeID = reader.GetString(0),
                            BeastTypeDescription = reader.GetString(1)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return beastTypes;
        }

        public List<BeastSubType> SelectAllBeastSubTypes()
        {
            List<BeastSubType> beastSubTypes = new List<BeastSubType>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_all_BeastSubTypes";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        beastSubTypes.Add(new BeastSubType()
                        {
                            BeastSubTypeID = reader.GetString(0),
                            BeastSubTypeDescription = reader.GetString(1)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return beastSubTypes;
        }

        public List<BeastSize> SelectAllBeastSizes()
        {
            List<BeastSize> beastSizes = new List<BeastSize>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_all_BeastSizes";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        beastSizes.Add(new BeastSize()
                        {
                            BeastSizeID = reader.GetString(0),
                            BeastSizeDescription = reader.GetString(1)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return beastSizes;
        }

        public List<Terrain> SelectAllTerrains()
        {
            List<Terrain> terrains = new List<Terrain>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_all_Terrains";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        terrains.Add(new Terrain()
                        {
                            TerrainID = reader.GetString(0),
                            TerrainDescription = reader.GetString(1)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return terrains;
        }

        public int UpdateGameCompany(GameCompany oldGameCompany, GameCompany newGameCompany)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_update_GameCompany";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GameCompanyID", oldGameCompany.GameCompanyID);
            cmd.Parameters.AddWithValue("@OldEmail", oldGameCompany.Email);
            cmd.Parameters.AddWithValue("@OldWebsite", oldGameCompany.Website);
            cmd.Parameters.AddWithValue("@OldActive", oldGameCompany.Active);

            cmd.Parameters.Add("@NewEmail", SqlDbType.NVarChar, 100);
            cmd.Parameters["@NewEmail"].Value = newGameCompany.Email;

            cmd.Parameters.Add("@NewWebsite", SqlDbType.NVarChar, 100);
            cmd.Parameters["@NewWebsite"].Value = newGameCompany.Website;

            cmd.Parameters.Add("@NewActive", SqlDbType.Bit);
            cmd.Parameters["@NewActive"].Value = newGameCompany.Active;
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        }

        public int InsertGameCompany(GameCompany newGameCompany)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_GameCompany";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@GameCompanyID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@GameCompanyID"].Value = newGameCompany.GameCompanyID;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters["@Email"].Value = newGameCompany.Email;

            cmd.Parameters.Add("@Website", SqlDbType.NVarChar, 100);
            cmd.Parameters["@Website"].Value = newGameCompany.Website;

            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters["@Active"].Value = newGameCompany.Active;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        }

        public int UpdateGame(Game oldGame, Game newGame)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_update_Game";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GameID", oldGame.GameID);
            cmd.Parameters.AddWithValue("@OldGameCompanyID", oldGame.GameCompanyID);
            cmd.Parameters.AddWithValue("@OldGameVersion", oldGame.GameVersion);
            cmd.Parameters.AddWithValue("@OldActive", oldGame.Active);

            cmd.Parameters.Add("@NewGameCompanyID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewGameCompanyID"].Value = newGame.GameCompanyID;

            cmd.Parameters.Add("@NewGameVersion", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewGameVersion"].Value = newGame.GameVersion;

            cmd.Parameters.Add("@NewActive", SqlDbType.Bit);
            cmd.Parameters["@NewActive"].Value = newGame.Active;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        }

        public int InsertGame(Game newGame)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_Game";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@GameID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@GameID"].Value = newGame.GameCompanyID;

            cmd.Parameters.Add("@GameCompanyID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@GameCompanyID"].Value = newGame.GameCompanyID;

            cmd.Parameters.Add("@GameVersion", SqlDbType.NVarChar, 50);
            cmd.Parameters["@GameVersion"].Value = newGame.GameVersion;

            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters["@Active"].Value = newGame.Active;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        }
    }
}
