using System;
using System.Collections.Generic;

namespace NaturguidenServerPrototype.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<PlaceVisit> PlaceVisits { get; set; } = new List<PlaceVisit>();
}
