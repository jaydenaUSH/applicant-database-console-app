using System;
using System.Collections.Generic;

namespace USHTask1.Models;

public partial class MockApplicant
{
    public long ApplicantId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public string? Country { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Reason { get; set; }

    public string? Story { get; set; }

    public string? GenderAvatar { get; set; }

    public bool? EnoughFood { get; set; }

    public int? HousholdSize { get; set; }

    public string? InsecurityFrequency { get; set; }

    public string? Illnesses { get; set; }

    public DateTime Posted { get; set; }

    public string? Source { get; set; }

    public string? County { get; set; }

    public bool? ApplicantStatus { get; set; }

    public bool PriorityId { get; set; }

    public bool? HealthInsurance { get; set; }

    public string? HealthPlan { get; set; }

    public int? ChildrenInHousehold { get; set; }

    public int? SeniorsInHousehold { get; set; }

    public bool? JobLossOrReducedHours { get; set; }

    public bool? MedicalTransportation { get; set; }

    public bool? GroceryTransportation { get; set; }

    public bool? DentalTransportation { get; set; }

    public bool? InternetAccess { get; set; }

    public bool? ComputerInHome { get; set; }

    public string? ReferralSource { get; set; }

    public bool? LivesInFoodDesert { get; set; }

    public bool? TractLowIncome { get; set; }

    public double? TractPovertyRate { get; set; }

    public double? TractMedianIncome { get; set; }

    public string? PreferredLanguage { get; set; }

    public bool? EnglishAsSecondLanguage { get; set; }

    public string? Race { get; set; }

    public string? Ethnicity { get; set; }

    public bool? Urban { get; set; }

    public string? CantAffordBalancedMeals { get; set; }

    public bool? SkippedMeals { get; set; }

    public string? Benefits { get; set; }

    public string? LivingSituationToday { get; set; }

    public string? ProblemsWithHousing { get; set; }

    public string? RiskOfLosingServices { get; set; }

    public string? WantHelpWithEmployment { get; set; }

    public string? HowHardToPayForBasics { get; set; }

    public string? IncomeBracket { get; set; }

    public bool? LiveWithIllness { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? WorryFoodRunOut { get; set; }

    public string? EducationLevel { get; set; }
}
