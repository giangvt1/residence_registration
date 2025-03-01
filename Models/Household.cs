using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Household
{
    public int HouseholdId { get; set; }

    public int HeadOfHouseholdId { get; set; }

    public int AddressId { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual User HeadOfHousehold { get; set; } = null!;

    public virtual ICollection<HouseholdMember> HouseholdMembers { get; set; } = new List<HouseholdMember>();

    public virtual ICollection<HouseholdTransfer> HouseholdTransfers { get; set; } = new List<HouseholdTransfer>();
}
