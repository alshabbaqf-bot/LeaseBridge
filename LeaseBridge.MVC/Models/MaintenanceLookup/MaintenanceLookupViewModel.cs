using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.MVC.Models.MaintenanceLookup
{
    public class MaintenanceLookupViewModel
    {
        [Required]
        [Display(Name = "Maintenance Ticket Number")]
        public string TicketNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Registered Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        public PublicMaintenanceLookupResultViewModel? Result { get; set; }
    }

    public class PublicMaintenanceLookupResultViewModel
    {
        public int RequestId { get; set; }

        public string TicketNumber { get; set; } = string.Empty;

        public string TenantName { get; set; } = string.Empty;

        public string UnitNumber { get; set; } = string.Empty;

        public string PropertyName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string PriorityName { get; set; } = string.Empty;

        public string StatusName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public List<PublicMaintenanceUpdateViewModel> Updates { get; set; } = new();
    }

    public class PublicMaintenanceUpdateViewModel
    {
        public DateTime CreatedAt { get; set; }

        public string OldStatusName { get; set; } = string.Empty;

        public string NewStatusName { get; set; } = string.Empty;

        public string UpdatedByName { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;
    }
}