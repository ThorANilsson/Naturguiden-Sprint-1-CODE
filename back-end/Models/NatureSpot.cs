using System;
using System.Collections.Generic;

namespace NaturguidenServerPrototype.Models;

public partial class NatureSpot {
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public bool? HasElectricity { get; set; }

    public bool? HasWater { get; set; }

    public bool? HasToilets { get; set; }

    public bool? HasCarParking { get; set; }

    public DateTime? CreatedAt { get; set; }

}
