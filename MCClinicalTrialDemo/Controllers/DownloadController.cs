using MCClinicalTrialDemo.Models;
using MultiChainLib.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCClinicalTrialDemo.Controllers
{
    public class DownloadController : BaseController
    {
        // GET: Download
        public ActionResult Index()
        {
            var publishersList = GetMultiChainClient().ListStreamKeys(GetTrialDownloadStream());
            return View(publishersList);
        }
        
        public ActionResult Details(string researcherName)
        {
            List<DownloadViewModel> downloadDetails = new List<DownloadViewModel>();

            var response = GetMultiChainClient().ListStreamKeyItems(GetTrialDownloadStream(), researcherName);
            foreach (var trialModel in response.Result)
            {
                var jsonTrialViewModel = Utility.HexadecimalEncoding.FromHexString(trialModel.Data);
                var downloadViewModel = JsonConvert.DeserializeObject<DownloadViewModel>(jsonTrialViewModel);
                downloadDetails.Add(downloadViewModel);
            }

            return PartialView(downloadDetails);
        }
    }
}