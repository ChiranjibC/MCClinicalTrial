using MCClinicalTrialDemo.Models;
using MultiChainLib;
using MultiChainLib.Helper;
using MultiChainLib.Model;
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

        public ActionResult Details(string trialName)
        {
            List<DownloadViewModel> downloadDetails = new List<DownloadViewModel>();
            JsonRpcResponse<List<StreamResponse>> response = null;

            if (trialName.Trim().ToUpper() == "")
            {
                response = GetMultiChainClient().ListStreamItems(GetTrialDownloadStream());
            }
            else
            {
                response = GetMultiChainClient().ListStreamKeyItems(GetTrialDownloadStream(), trialName);
            }

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