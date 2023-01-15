using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;
using DataAccess;
using GameBetiaryMVC.Models;

namespace GameBetiaryMVC.Controllers
{ 
    public class BeastController : Controller
    {
        BeastManager beastManager = new BeastManager();
        BeastAccessor beastAccessor = new BeastAccessor();
        // GET: Beast
        public ActionResult BeastList()
        {
            List<Beast> beastList = new List<Beast>();
            beastList = beastManager.RetrieveAllBeasts();
            return View(beastList);
        }


        // GET: Beast/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult CreateBeast()
        {
            return View();
        }

        // POST: Beast/CreateBeast
        [HttpPost]
        public ActionResult CreateBeast(Beast beast, FormCollection collection)
        {
            int BeastID = beast.BeastID;
            string GameID = beast.GameID;
            string AlignmentID = beast.AlignmentID;
            string BeastTypeID = beast.BeastTypeID;
            string BeastSubTypeID = beast.BeastSubTypeID;
            string TerrainID = beast.TerrainID;
            string BeastSizeID = beast.BeastSizeID;
            string BeastName = beast.BeastName;
            int ChallengeRating = beast.ChallengeRating;
            string Treasure = beast.Treasure;
            int Experience = beast.Experience;
            string BeastDescription = beast.BeastDescription;
            bool Active = beast.Active;
            try
            {
                beastManager.CreateBeast(beast);
            }
            catch
            {
                return View("CreateBeast");
            }

            return RedirectToAction("BeastList");
        }

        // GET: Beast/Edit/5
        public ActionResult Edit(int id)
        {
            Beast beast = new Beast();

            try
            {
                beast = beastManager.RetrieveBeastByBeastID(id);
                if (beast != null)
                {
                    return View(beast);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View("BeastList");
        }

        // POST: Beast/Edit/5
        [HttpPost]
        public ActionResult Edit(Beast newBeast, FormCollection collection)
        {
            Beast oldBeast = new Beast();
            oldBeast = beastManager.RetrieveBeastByBeastID(newBeast.BeastID);

            int BeastID = newBeast.BeastID;
            string GameID = newBeast.GameID;
            string AlignmentID = newBeast.AlignmentID;
            string BeastTypeID = newBeast.BeastTypeID;
            string BeastSubTypeID = newBeast.BeastSubTypeID;
            string TerrainID = newBeast.TerrainID;
            string BeastSizeID = newBeast.BeastSizeID;
            string BeastName = newBeast.BeastName;
            int ChallengeRating = newBeast.ChallengeRating;
            string Treasure = newBeast.Treasure;
            int Experience = newBeast.Experience;
            string BeastDescription = newBeast.BeastDescription;
            bool Active = newBeast.Active;

            try
            {
                // TODO: Add update logic here
                beastManager.EditBeast(oldBeast, newBeast);
            }
            catch
            {
                return View("Edit");
            }

            return RedirectToAction("BeastList");
        }

        // GET: Beast/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Beast/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("BeastList");
            }
            catch
            {
                return View();
            }
        }
    }
}
