﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingEvents.Model
{
    public class RolesView
    {   
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        
        public string RoleId { get; set; }
        public string Name { get; set; }
    }
}
