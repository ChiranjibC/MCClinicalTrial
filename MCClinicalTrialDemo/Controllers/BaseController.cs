using MultiChainLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace MCClinicalTrialDemo.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private MultiChainClient mcClient = null;
        private string _currentUserRole = "UserRole";
        protected Dictionary<string, string> AllowedUsers;
        public string Role_Publisher = "Publisher";
        public string Role_Researcher = "Researcher";
        protected MultiChainClient GetMultiChainClient(string chainName = "")
        {
            if (string.IsNullOrEmpty(chainName)) {
                chainName = ConfigurationManager.AppSettings["TrialRepository"];
            }
            var multiChainIP = ConfigurationManager.AppSettings["MultiChainIPAddress"];
            var multiChainPort = Convert.ToInt32(ConfigurationManager.AppSettings["MultiChainPort"]);
            var multiChainUsername = ConfigurationManager.AppSettings["MultiChainUsername"];
            var multiChainPassword = ConfigurationManager.AppSettings["MultiChainPassword"];
            mcClient = new MultiChainClient(multiChainIP, multiChainPort, false, multiChainUsername, multiChainPassword, chainName);
            return mcClient;
        }

        protected string GetTrialStream()
        {
            return ConfigurationManager.AppSettings["TrialStream"];
        }
        protected string GetTrialDownloadStream()
        {
            return ConfigurationManager.AppSettings["TrialDownloadStream"];
        }

        public BaseController()
        {
            AllowedUsers = new Dictionary<string, string>();
            AllowedUsers.Add("testuser1", Role_Researcher);
            AllowedUsers.Add("testuser2", Role_Researcher);
            AllowedUsers.Add("testuser3", Role_Researcher);
            AllowedUsers.Add("admin1", Role_Publisher);
            AllowedUsers.Add("admin2", Role_Publisher);
        }

        public string CurrentUserRole {
            get { var userRole = Session[_currentUserRole] as string;
                if (string.IsNullOrEmpty(userRole))
                {
                    userRole = Role_Researcher;
                }
                return userRole;
            }
            set { Session[_currentUserRole] = value; }
        }

    }
}