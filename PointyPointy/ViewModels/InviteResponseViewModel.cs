using PointyPointy.Data.Entities;

namespace PointyPointy.ViewModels
{
    public class InviteResponseViewModel : InviteViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public bool Accept { get; set; }

        public string Key { get; set; }

        public ScrumInviteUser InviteUser { get; set; }
    }
}