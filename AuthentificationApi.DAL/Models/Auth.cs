using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AuthentificationApi.DAL.Models
{
    public partial class Auth
    {
        [Key]
        public int IdAuth { get; set; }
        public string Code { get; set; }
        public string State { get; set; }
        public string AppId { get; set; }
    }
}
