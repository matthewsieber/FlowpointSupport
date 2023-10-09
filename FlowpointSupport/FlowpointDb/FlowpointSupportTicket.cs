using System;
using System.Collections.Generic;

namespace FlowpointSupport.FlowpointDb;

public partial class FlowpointSupportTicket
{
    public int ITicketId { get; set; }

    public int IVendorId { get; set; }

    public string VTicketMessage { get; set; } = null!;

    public DateTime DtCreatedDate { get; set; }

    public DateTime DtModifiedDate { get; set; }

    public int ICreatedBy { get; set; }

    public int IModifiedBy { get; set; }

    public bool BIsDeleted { get; set; }

    public virtual FlowpointSupportVendor IVendor { get; set; } = null!;
}
