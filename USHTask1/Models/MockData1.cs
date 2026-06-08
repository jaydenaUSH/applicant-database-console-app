using System;
using System.Collections.Generic;

namespace USHTask1.Models;

public partial class MockData1
{
    public short ApplicantId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string Address2 { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? State { get; set; }

    public string? Zip { get; set; }

    public string Country { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public string Story { get; set; } = null!;

    public string GenderAvatar { get; set; } = null!;

    public bool EnoughFood { get; set; }

    public byte HouseholdSize { get; set; }

    public string InsecurityFrequency { get; set; } = null!;

    public string Illnesses { get; set; } = null!;

    public bool Posted { get; set; }

    public string Source { get; set; } = null!;

    public string ApplicantStatus { get; set; } = null!;

    public byte PriorityId { get; set; }

    public bool HealthInsurance { get; set; }

    public string HealthPlan { get; set; } = null!;

    public byte ChildrenInHousehold { get; set; }

    public byte SeniorsInHousehold { get; set; }

    public bool JobLossOrReducedHours { get; set; }

    public bool MedicalTransportation { get; set; }

    public bool GroceryTransportation { get; set; }

    public bool DentalTransportation { get; set; }
}
