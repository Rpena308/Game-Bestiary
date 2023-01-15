using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    public interface IBeastAccessor
    {
        List<Beast> SelectBeastsByBeastType(string BeastTypeID);
        Beast SelectBeastByBeastID(int BeastID);
        List<Beast> SelectAllBeasts();
        List<Beast> SelectAllActiveBeasts();
        List<GameCompany> SelectAllGameCompanies();
        List<GameCompany> SelectAllActiveGameCompanies();
        List<Game> SelectAllActiveGames();
        List<Game> SelectAllGames();
        List<Alignment> SelectAllAlignments();
        List<BeastType> SelectAllBeastTypes();
        List<BeastSubType> SelectAllBeastSubTypes();
        List<BeastSize> SelectAllBeastSizes();
        List<Terrain> SelectAllTerrains();
        int UpdateBeast(Beast oldBeast, Beast newBeast);
        int InsertBeast(Beast newBeast);
        int UpdateGameCompany(GameCompany oldGameCompany, GameCompany newGameCompany);
        int InsertGameCompany(GameCompany newGameCompany);
        int UpdateGame(Game oldGame, Game newGame);
        int InsertGame(Game newGame);
    }
}
