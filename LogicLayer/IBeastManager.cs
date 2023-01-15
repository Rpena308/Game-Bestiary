using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface IBeastManager
    {
        List<Beast> RetrieveAllBeasts();
        List<Beast> RetrieveBeastsByBeastType(string BeastTypeID);
        Beast RetrieveBeastByBeastID(int BeastID);
        List<Beast> RetrieveAllActiveBeasts();
        List<GameCompany> RetrieveAllGameCompanies();
        List<GameCompany> RetrieveAllActiveGameCompanies();
        List<Game> RetrieveAllActiveGames();
        List<Game> RetrieveAllGames();
        List<Alignment> RetrieveAllAlignments();
        List<BeastType> RetrieveAllBeastTypes();
        List<BeastSubType> RetrieveAllBeastSubTypes();
        List<BeastSize> RetrieveAllBeastSizes();
        List<Terrain> RetrieveAllTerrains();
        bool EditBeast(Beast oldBeast, Beast newBeast);
        bool CreateBeast(Beast newBeast);
        bool EditGameCompany(GameCompany oldGameCompany, GameCompany newGameCompany);
        bool CreateGameCompany(GameCompany newGameCompany);
        bool EditGame(Game oldGame, Game newGame);
        bool CreateGame(Game newGame);
    }
}
