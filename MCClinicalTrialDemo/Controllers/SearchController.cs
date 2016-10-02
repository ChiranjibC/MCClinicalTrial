using MCClinicalTrialDemo.Models;
using MultiChainLib;
using MultiChainLib.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace MCClinicalTrialDemo.Controllers
{
    public class SearchController : BaseController
    {
        ICollection<TrialViewModel> list = new List<TrialViewModel>();
        //
        // GET: /Search/
        public ActionResult Index()
        {
            if (TempData["FilteredData"] == null)
            {
                RetriveData();
            }
            else
            {
                if (TempData["FilteredData"] != null)
                {
                    list = (ICollection<TrialViewModel>)TempData["FilteredData"];
                }
            }

            return View(list);
        }

        private void RetriveData()
        {
            var info = GetMultiChainClient().ListStreamItems(GetTrialStream());

            foreach (var item in info.Result)
            {
                string response = item.Data;

                string convertedData = Utility.HexadecimalEncoding.FromHexString(response);

                try
                {
                    TrialViewModel viewModel = new TrialViewModel();

                    viewModel = JsonConvert.DeserializeObject<TrialViewModel>(convertedData);
                    viewModel.TxId = item.TxId;

                    if (viewModel.TrialName != null)
                    {
                        list.Add(viewModel);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.ToString();
                }
            }
            info.AssertOk();
        }

        private void RetriveData(string trialKey)
        {
            var info = GetMultiChainClient().ListStreamKeyItems(GetTrialStream(), trialKey);

            foreach (var item in info.Result)
            {
                string response = item.Data;

                string convertedData = Utility.HexadecimalEncoding.FromHexString(response);

                try
                {
                    TrialViewModel viewModel = new TrialViewModel();

                    viewModel = JsonConvert.DeserializeObject<TrialViewModel>(convertedData);
                    viewModel.TxId = item.TxId;

                    if (viewModel.TrialName != null)
                    {
                        list.Add(viewModel);
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.ToString();
                }
            }
            info.AssertOk();
        }
        public ActionResult Search(string SearchText)
        {
            RetriveData(SearchText);

            if (list.Count > 0)
            {
                TempData["FilteredData"] = list;
            }
            else
            {
                TempData["FilteredData"] = string.Empty;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Get()
        {
            return RedirectToAction("Index");
        }

        //
        // GET: /Search/Details/5
        public ActionResult Details(string transactionId)
        {
            return View();
        }

        //
        // GET: /Search/Download/5
        public ActionResult Download(string selectedTrialKey, string selectedTxId)
        {
            var trialList = GetMultiChainClient().ListStreamKeyItems(GetTrialStream(), selectedTrialKey);
            var trialModel = trialList.Result.Where(item => item.TxId == selectedTxId).FirstOrDefault();
            var jsonTrialViewModel = Utility.HexadecimalEncoding.FromHexString(trialModel.Data);
            var trialViewModel = JsonConvert.DeserializeObject<TrialViewModel>(jsonTrialViewModel);

            if (string.IsNullOrEmpty(trialViewModel.DocumentUrl))
            {
                ViewBag.Error = "File was not uploaded for this trial.";
                return View("Error");
            }

            var serverFileHash = Utility.GetHash(trialViewModel.DocumentUrl);
            if (string.IsNullOrEmpty(serverFileHash))
            {
                ViewBag.Error = "File not found on the server. It could have been moved or deleted from the server.";
                return View("Error");
            }
            if (serverFileHash != trialViewModel.DocumentHash)
            {
                ViewBag.Error = "File seems to have been corrupted or modified.";
                return View("Error");
            }

            //continue with logging download request, and actual file download
            var downloadViewModel = new DownloadViewModel()
            {
                TrialName = trialViewModel.TrialName,
                Description = trialViewModel.TrialDescription,
                StudyNumber = trialViewModel.StudyNumber,
                ProtocolName = trialViewModel.ProtocolName,
                Location = trialViewModel.Location,
                DownloadedOn = DateTime.UtcNow, 
                DownloadedBy = HttpContext.User.Identity.Name
            };

            var downloadTrialModel = GetTrialModel(trialViewModel.TrialName, downloadViewModel);

            //Create a download entry for the Current logged in User, requesting for the TrialKey file download
            MultiChainClient mcClient = GetMultiChainClient();
            var info = mcClient.PublishStream(GetTrialDownloadStream(), downloadTrialModel.TrialName, downloadTrialModel.TrialData);
            info.AssertOk();

            //Download the selected file
            string filePath = Path.Combine(Server.MapPath(trialViewModel.DocumentUrl));
            var stream = System.IO.File.OpenRead(filePath);
            return File(stream, "application/octet-stream", Path.GetFileName(trialViewModel.DocumentUrl));
        }

        private TrialModel GetTrialModel(string key, DownloadViewModel downloadViewModel)
        {
            //Populate from collection
            //a. Generate Json without trial-key
            string jsonDownloadModel = JsonConvert.SerializeObject(downloadViewModel);
            //b. Generate Hex from json as source
            string hexString = Utility.HexadecimalEncoding.ToHexString(jsonDownloadModel);

            return new TrialModel()
            {
                TrialName = key,
                TrialData = hexString
            };
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
