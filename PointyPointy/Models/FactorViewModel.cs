using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace PointyPointy.Models
{
    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }
}
