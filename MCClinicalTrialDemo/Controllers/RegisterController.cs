using MCClinicalTrialDemo.Models;
using MultiChainLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCClinicalTrialDemo.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/
        public ActionResult Index()
        {
            return View("Index");
        }

        //
        // GET: /Register/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Register/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Register/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            TrialModel trialModel = new TrialModel();
            try
            {
                MultiChainClient mcClient = new MultiChainClient("54.234.132.18", 2766, false, "multichainrpc", "testmultichain", "TrialRepository");
                
                
                //populate trial-model by trial-view-model
                trialModel = GetTrialModel(collection);

                //Keep following line commented till the time GetTrialModel implemented.
                var info = mcClient.ListStreamItems("TrialStream");
                //var info = mcClient.PublishStream("TrialStream", trialModel.TrialKey, trialModel.TrialData);
                info.AssertOk();
                

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        private TrialModel GetTrialModel(FormCollection collection)
        {
            //Populate from collection
            //a. Generate Json without trial-key
            //b. Generate Hex from json as source
            return new TrialModel() { 
                //Remove Hardcoding
                TrialKey = "T1", 
                TrialData = "7B0A0922547269616C44617465223A202230312F30312F32303136222C0A092252657365617263684E616D65223A20225231222C0D0A0922526573656172636865724E616D65223A202243686972616E6A6962222C0D0A092252657365617263684F6E223A2022484956222C0A09224F62736572766174696F6E223A2022476F6F642E220A7D" };
        }

        //
        // GET: /Register/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Register/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Register/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Register/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
