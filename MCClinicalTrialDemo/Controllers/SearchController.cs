using MCClinicalTrialDemo.Models;
using MultiChainLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCClinicalTrialDemo.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        public ActionResult Index()
        {
            ICollection<TrialViewModel> list = new List<TrialViewModel>();

            MultiChainClient mcClient = new MultiChainClient("54.234.132.18", 2766, false, "multichainrpc", "testmultichain", "TrialRepository");

            var info = mcClient.ListStreamItems("TrialStream");

            foreach (var item in info.Result)
            {
                string response = item.Data;
                string convertedData = ConvertHex(response);

                list.Add(JsonConvert.DeserializeObject<TrialViewModel>(convertedData));
            }

            info.AssertOk();

            return View(list);
        }

        public string ConvertHex(String hexString)
        {
            try
            {
                string ascii = string.Empty;

                for (int i = 0; i < hexString.Length; i += 2)
                {
                    String hs = string.Empty;

                    hs = hexString.Substring(i, 2);
                    uint decval = System.Convert.ToUInt32(hs, 16);
                    char character = System.Convert.ToChar(decval);
                    ascii += character;

                }

                return ascii;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return string.Empty;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return RedirectToAction("Index");
        }
        
        //
        // GET: /Search/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Search/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Search/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Search/Edit/5
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
        // GET: /Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Search/Delete/5
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
