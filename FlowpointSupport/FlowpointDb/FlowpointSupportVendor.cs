using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlowpointSupport.FlowpointDb;

public partial class FlowpointSupportVendor
{
    [Display(Name = "ID")]
    public int IVendorId { get; set; }

    public int ICompanyId { get; set; }

    [Display(Name = "Vendor Name")]
    [Required(ErrorMessage = "Vendor Name is required")]
    public string VVendorName { get; set; } = null!;

    [Display(Name = "Street 1")]
    [Required(ErrorMessage = "Street 1 is required")]
    [StringLength(128, ErrorMessage = "Street 1 must be 128 characters or less")]
    public string VStreet1 { get; set; } = null!;

    [Display(Name = "Street 2")]
    [StringLength(128, ErrorMessage = "Street 2 must be 128 characters or less")]
    public string? VStreet2 { get; set; }

    [Display(Name = "City")]
    [StringLength(128, ErrorMessage = "City must be 128 characters or less")]
    public string? VCity { get; set; }

    [Display(Name = "Province")]
    [StringLength(50, ErrorMessage = "Province must be 50 characters or less")]
    public string? VProvince { get; set; }

    [Display(Name = "Postal Code")]
    [StringLength(32, ErrorMessage = "Postal Code must be 32 characters or less")]
    public string? VPostalCode { get; set; }

    [Display(Name = "Country")]
    [StringLength(50, ErrorMessage = "Country must be 50 characters or less")]
    public string? VCountry { get; set; }

    [Display(Name = "Contact")]
    [StringLength(64, ErrorMessage = "Contact must be 64 characters or less")]
    public string? VContact { get; set; }

    [Display(Name = "Phone")]
    [StringLength(50, ErrorMessage = "Phone must be 50 characters or less")]
    public string? VPhone { get; set; }

    [Display(Name = "Fax")]
    [StringLength(50, ErrorMessage = "Fax must be 50 characters or less")]
    public string? VFax { get; set; }

    [Display(Name = "Email")]
    [StringLength(50, ErrorMessage = "Email must be 50 characters or less")]
    public string? VEmail { get; set; }

    [Display(Name = "Date Created")]
    public DateTime? DtCreated { get; set; }

    public bool BIsDeleted { get; set; }

    public virtual ICollection<FlowpointSupportTicket> FlowpointSupportTickets { get; set; } = new List<FlowpointSupportTicket>();

    public virtual FlowpointSupportCompany? ICompany { get; set; } = null!;
}
