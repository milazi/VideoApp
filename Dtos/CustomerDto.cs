using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Customer's Name")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public byte MembershipTypeId { get; set; }
        public MembershipTypeDto Membership { get; set;  }
        // [Min18YearsIfAMember]
        public Nullable<DateTime> Birthdate { get; set; }
    }
}