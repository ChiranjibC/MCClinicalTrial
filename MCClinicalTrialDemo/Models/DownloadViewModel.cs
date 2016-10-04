using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MCClinicalTrialDemo.Models
{
    public class DownloadViewModel
    {
        [Required]
        [Display(Name = "Trial Name")]
        public string TrialName { get; set; }

        [Required]
        [Display(Name = "Trial Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Protocol Name")]
        public string ProtocolName { get; set; }

        [Required]
        [Display(Name = "Study Number")]
        public string StudyNumber { get; set; }

        [Required]
        [Display(Name = "Downloaded By")]
        public string DownloadedBy { get; set; }

        [Required]
        [Display(Name = "Downloaded On")]
        public DateTime DownloadedOn { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }
    }
}