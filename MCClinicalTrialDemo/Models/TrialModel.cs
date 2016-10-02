using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCClinicalTrialDemo.Models
{
    public class TrialModel
    {
        public string TrialName { get; set; }
        /// <summary>
        /// Data in Hex format.
        /// </summary>
        public string TrialData { get; set; }
    }
}