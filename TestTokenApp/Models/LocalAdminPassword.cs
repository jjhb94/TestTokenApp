using System;
using System.Collections.Generic;
// added these using statements below
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestTokenApp.Models;

namespace TestTokenApp.Models
{
    public partial class LocalAdminPassword
    {
        // added these lines below

        public int LocalAdminPasswordId { get; set; }

        [Column("HostName")]
        [StringLength(128)]
        public string HostName { get; set; }

        [Column("Password")]
        [StringLength(128)]
        public string Password { get; set; }

        [Column("RemoteIpAddress")]
        [StringLength(128)]
        public string RemoteIpAddress { get; set; }


    }
}
