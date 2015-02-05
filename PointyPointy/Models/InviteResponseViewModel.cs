using PointyPointy.Data.Entities;

namespace PointyPointy.Models
{
    public class InviteResponseViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public bool Accept { get; set; }

        public ScrumInviteUser InviteUser { get; set; }
    }
}