using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlowpointSupport.FlowpointDb;

public partial class FlowpointSupportTicket
{
    [Display(Name = "ID")]
    public int ITicketId { get; set; }

    public int IVendorId { get; set; }

    [Display(Name = "Message")]
    [Required(ErrorMessage = "Message is required")]
    [StringLength(3000, ErrorMessage = "Message must be 3000 characters or less")]
    public string VTicketMessage { get; set; } = null!;

    [Display(Name = "Date Created")]
    public DateTime DtCreatedDate { get; set; }

    [Display(Name = "Last Modified")]
    public DateTime DtModifiedDate { get; set; }

    [Display(Name = "Created By")]
    public int ICreatedBy { get; set; }

    [Display(Name = "Last Modified By")]
    public int IModifiedBy { get; set; }

    public bool BIsDeleted { get; set; }

    public virtual FlowpointSupportVendor? IVendor { get; set; } = null!;
}
