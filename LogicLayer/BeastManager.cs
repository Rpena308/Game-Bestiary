using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccess;

namespace LogicLayer
{
    public class BeastManager : IBeastManager
    {

        private IBeastAccessor _beastAccessor = null;
        public BeastManager()
        {
            _beastAccessor = new BeastAccessor(); // default
        }

        public BeastManager(IBeastAccessor beastAccessor)
        {
            _beastAccessor = beastAccessor;
        }

        public bool CreateBeast(Beast newBeast)
        {
            bool result = false;
            try
            {
                result = (1 == _beastAccessor.InsertBeast(newBeast));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<Beast> RetrieveAllActiveBeasts()
        {
            List<Beast> beasts = new List<Beast>();

            try
            {
                beasts = _beastAccessor.SelectAllActiveBeasts();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return beasts;
        }

        public List<GameCompany> RetrieveAllActiveGameCompanies()
        {
            List<GameCompany> gameCompany = new List<GameCompany>();

            try
            {
                gameCompany = _beastAccessor.SelectAllActiveGameCompanies();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return gameCompany;
        }

        public List<Game> RetrieveAllActiveGames()
        {
            List<Game> games = new List<Game>();

            try
            {
                games = _beastAccessor.SelectAllActiveGames();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return games;
        }

        public List<Beast> RetrieveAllBeasts()
        {
            List<Beast> beasts = new List<Beast>();

            try
            {
                beasts = _beastAccessor.SelectAllBeasts();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return beasts;
        }

        public List<GameCompany> RetrieveAllGameCompanies()
        {
            List<GameCompany> gameCompanies = new List<GameCompany>();

            try
            {
                gameCompanies = _beastAccessor.SelectAllGameCompanies();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return gameCompanies;
        }

        public List<Game> RetrieveAllGames()
        {
            List<Game> games = new List<Game>();

            try
            {
                games = _beastAccessor.SelectAllGames();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return games;
        }

        public List<Beast> RetrieveBeastsByBeastType(string BeastTypeID)
        {
            List<Beast> beasts = new List<Beast>();

            try
            {
                beasts = _beastAccessor.SelectBeastsByBeastType(BeastTypeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return beasts;
        }



        public Beast RetrieveBeastByBeastID(int BeastID)
        {
            Beast beast = new Beast();

            try
            {
                beast = _beastAccessor.SelectBeastByBeastID(BeastID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return beast;
        }

        public bool EditBeast(Beast oldBeast, Beast newBeast)
        {
            bool result = false;
            try
            {
                result = (1 == _beastAccessor.UpdateBeast(oldBeast, newBeast));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<Alignment> RetrieveAllAlignments()
        {
            List<Alignment> alignments = new List<Alignment>();

            try
            {
                alignments = _beastAccessor.SelectAllAlignments();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return alignments;
        }

        public List<BeastType> RetrieveAllBeastTypes()
        {
            List<BeastType> beastTypes = new List<BeastType>();

            try
            {
                beastTypes = _beastAccessor.SelectAllBeastTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return beastTypes;
        }

        public List<BeastSubType> RetrieveAllBeastSubTypes()
        {
            List<BeastSubType> beastSubTypes = new List<BeastSubType>();

            try
            {
                beastSubTypes = _beastAccessor.SelectAllBeastSubTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return beastSubTypes;
        }

        public List<BeastSize> RetrieveAllBeastSizes()
        {
            List<BeastSize> beastSizes = new List<BeastSize>();

            try
            {
                beastSizes = _beastAccessor.SelectAllBeastSizes();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return beastSizes;
        }

        public List<Terrain> RetrieveAllTerrains()
        {
            List<Terrain> terrains = new List<Terrain>();

            try
            {
                terrains = _beastAccessor.SelectAllTerrains();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return terrains;
        }

        public bool EditGameCompany(GameCompany oldGameCompany, GameCompany newGameCompany)
        {
            bool result = false;
            try
            {
                result = (1 == _beastAccessor.UpdateGameCompany(oldGameCompany, newGameCompany));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool CreateGameCompany(GameCompany newGameCompany)
        {
            bool result = false;
            try
            {
                result = (1 == _beastAccessor.InsertGameCompany(newGameCompany));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool EditGame(Game oldGame, Game newGame)
        {
            bool result = false;
            try
            {
                result = (1 == _beastAccessor.UpdateGame(oldGame, newGame));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool CreateGame(Game newGame)
        {
            bool result = false;
            try
            {
                result = (1 == _beastAccessor.InsertGame(newGame));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
