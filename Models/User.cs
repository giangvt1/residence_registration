﻿using Project.Enums;
namespace Project.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Role Role { get; set; }

    public int? AreaId { get; set; }

    public int CurrentAddressId { get; set; }

    public virtual Area? Area { get; set; }

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();

    public virtual Address CurrentAddress { get; set; } = null!;

    public virtual ICollection<HouseholdMember> HouseholdMembers { get; set; } = new List<HouseholdMember>();

    public virtual ICollection<HouseholdTransfer> HouseholdTransfers { get; set; } = new List<HouseholdTransfer>();

    public virtual ICollection<Household> Households { get; set; } = new List<Household>();

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<RegistrationApproval> RegistrationApprovals { get; set; } = new List<RegistrationApproval>();

    public virtual ICollection<Registration> RegistrationApprovedByNavigations { get; set; } = new List<Registration>();

    public virtual ICollection<Registration> RegistrationUsers { get; set; } = new List<Registration>();

    public virtual ICollection<UserContact> UserContacts { get; set; } = new List<UserContact>();
}
