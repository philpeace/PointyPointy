using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PointyPointy.Data.Entities;

namespace PointyPointy.ViewModels
{
    public class InviteCreatedViewModel : InviteViewModel
    {
        public int Id { get; set; }

        public ScrumInvite ScrumInvite { get; set; }
    }
}