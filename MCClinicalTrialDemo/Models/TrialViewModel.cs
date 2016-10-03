using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MCClinicalTrialDemo.Models
{
    public class TrialViewModel
    {
        public string TxId { get; set; }

        [Required]
        [Display(Name = "Trial Name")]
        public string TrialName { get; set; }

        [Required]
        [Display(Name = "Trial Description")]
        public string TrialDescription { get; set; }

        [Required]
        [Display(Name = "Date Of Commencement")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d MMM yyyy}")]
        public DateTime DateOfCommencement { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Protocol Name")]
        public string ProtocolName { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Required]
        [Display(Name = "Study Number")]
        public string StudyNumber { get; set; }

        /// <summary>
        /// Using this property only to hold and save the uploaded file
        /// </summary>
        [Required]
        [Display(Name = "Study Data")]
        [JsonIgnore]
        public HttpPostedFileBase StudyFacts { get; set; }

        [Display(Name = "Document Url")]
        public string DocumentUrl { get; set; }

        [Display(Name = "Document Hash")]
        public string DocumentHash { get; set; }

    }
}