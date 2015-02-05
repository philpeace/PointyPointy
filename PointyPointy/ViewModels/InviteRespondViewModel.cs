using PointyPointy.Data.Entities;

namespace PointyPointy.ViewModels
{
    public class InviteRespondViewModel : InviteViewModel
    {
        public string Key { get; set; }

        public string Email { get; set; }

        public ScrumInviteUser InviteUser { get; set; }
    }
}