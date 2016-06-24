using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImplementRole.Models
{
    
    public class Country
    {
        public Country()
        {
            StateList = new List<State>();
        }
        //public string CountryId { get { return Guid.NewGuid().ToString(); } }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CountryId { get; set; }
        public int CountryName { get; set; }

        //public virtual CustomUser CustomeUesr { get; set; }
        public virtual ICollection<State> StateList { get; set; }
    }

    public class State
    {
        public int CountryId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StateId { get; set; }
        [Display(Name = "State Name")]
        public string StateName { get; set; }

        public virtual Country Country { get; set; }
    }
}