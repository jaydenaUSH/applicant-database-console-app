using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace USHTask1.Models;

public partial class InternDbContext : DbContext
{
    public InternDbContext(DbContextOptions<InternDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MockApplicant> MockApplicants { get; set; }

    public virtual DbSet<MockData1> MockData1s { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MockApplicant>(entity =>
        {
            entity.HasKey(e => e.ApplicantId);

            entity.ToTable("MockApplicant");

            entity.Property(e => e.ApplicantId).HasColumnName("applicant_id");
            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .HasColumnName("address_1");
            entity.Property(e => e.Address2)
                .HasMaxLength(255)
                .HasColumnName("address_2");
            entity.Property(e => e.ApplicantStatus)
                .HasDefaultValue(true, "DF_MockApplicant_applicant_status")
                .HasColumnName("applicant_status");
            entity.Property(e => e.Benefits).HasColumnName("benefits");
            entity.Property(e => e.CantAffordBalancedMeals)
                .HasMaxLength(50)
                .HasColumnName("cant_afford_balanced_meals");
            entity.Property(e => e.ChildrenInHousehold).HasColumnName("children_in_household");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.ComputerInHome).HasColumnName("computer_in_home");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasDefaultValue("US", "DF_MockApplicant_country")
                .HasColumnName("country");
            entity.Property(e => e.County)
                .HasMaxLength(50)
                .HasColumnName("county");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DentalTransportation).HasColumnName("dental_transportation");
            entity.Property(e => e.EducationLevel)
                .HasMaxLength(50)
                .HasColumnName("education_level");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.EnglishAsSecondLanguage).HasColumnName("english_as_second_language");
            entity.Property(e => e.EnoughFood).HasColumnName("enough_food");
            entity.Property(e => e.Ethnicity)
                .HasMaxLength(1000)
                .HasColumnName("ethnicity");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.GenderAvatar)
                .HasMaxLength(50)
                .HasColumnName("gender_avatar");
            entity.Property(e => e.GroceryTransportation).HasColumnName("grocery_transportation");
            entity.Property(e => e.HealthInsurance).HasColumnName("health_insurance");
            entity.Property(e => e.HealthPlan)
                .HasMaxLength(300)
                .HasColumnName("health_plan");
            entity.Property(e => e.HousholdSize).HasColumnName("houshold_size");
            entity.Property(e => e.HowHardToPayForBasics)
                .HasMaxLength(100)
                .HasColumnName("how_hard_to_pay_for_basics");
            entity.Property(e => e.Illnesses).HasColumnName("illnesses");
            entity.Property(e => e.IncomeBracket)
                .HasMaxLength(100)
                .HasColumnName("income_bracket");
            entity.Property(e => e.InsecurityFrequency)
                .HasMaxLength(50)
                .HasColumnName("insecurity_frequency");
            entity.Property(e => e.InternetAccess).HasColumnName("internet_access");
            entity.Property(e => e.JobLossOrReducedHours)
                .HasDefaultValue(false, "DF_MockApplicant_job_loss_or_reduced_hours")
                .HasColumnName("job_loss_or_reduced_hours");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.LiveWithIllness).HasColumnName("live_with_illness");
            entity.Property(e => e.LivesInFoodDesert)
                .HasDefaultValue(false, "DF_MockApplicant_lives_in_food_desert")
                .HasColumnName("lives_in_food_desert");
            entity.Property(e => e.LivingSituationToday)
                .HasMaxLength(500)
                .HasColumnName("living_situation_today");
            entity.Property(e => e.MedicalTransportation).HasColumnName("medical_transportation");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Posted)
                .HasDefaultValueSql("(getdate())", "DF_MockApplicant_posted")
                .HasColumnType("datetime")
                .HasColumnName("posted");
            entity.Property(e => e.PreferredLanguage)
                .HasMaxLength(100)
                .HasColumnName("preferred_language");
            entity.Property(e => e.PriorityId).HasColumnName("priority_id");
            entity.Property(e => e.ProblemsWithHousing)
                .HasMaxLength(500)
                .HasColumnName("problems_with_housing");
            entity.Property(e => e.Race)
                .HasMaxLength(1000)
                .HasColumnName("race");
            entity.Property(e => e.Reason)
                .HasMaxLength(100)
                .HasColumnName("reason");
            entity.Property(e => e.ReferralSource)
                .HasMaxLength(255)
                .HasColumnName("referral_source");
            entity.Property(e => e.RiskOfLosingServices)
                .HasMaxLength(50)
                .HasColumnName("risk_of_losing_services");
            entity.Property(e => e.SeniorsInHousehold).HasColumnName("seniors_in_household");
            entity.Property(e => e.SkippedMeals).HasColumnName("skipped_meals");
            entity.Property(e => e.Source)
                .HasMaxLength(100)
                .HasColumnName("source");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");
            entity.Property(e => e.Story).HasColumnName("story");
            entity.Property(e => e.TractLowIncome).HasColumnName("tract_low_income");
            entity.Property(e => e.TractMedianIncome).HasColumnName("tract_median_income");
            entity.Property(e => e.TractPovertyRate).HasColumnName("tract_poverty_rate");
            entity.Property(e => e.Urban).HasColumnName("urban");
            entity.Property(e => e.WantHelpWithEmployment)
                .HasMaxLength(150)
                .HasColumnName("want_help_with_employment");
            entity.Property(e => e.WorryFoodRunOut)
                .HasMaxLength(50)
                .HasColumnName("worry_food_run_out");
            entity.Property(e => e.Zip)
                .HasMaxLength(50)
                .HasColumnName("zip");
        });

        modelBuilder.Entity<MockData1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("mockData");

            entity.Property(e => e.Address1)
                .HasMaxLength(50)
                .HasColumnName("address_1");
            entity.Property(e => e.Address2)
                .HasMaxLength(50)
                .HasColumnName("address_2");
            entity.Property(e => e.ApplicantId).HasColumnName("applicant_id");
            entity.Property(e => e.ApplicantStatus)
                .HasMaxLength(50)
                .HasColumnName("applicant_status");
            entity.Property(e => e.ChildrenInHousehold).HasColumnName("children_in_household");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.DentalTransportation).HasColumnName("dental_transportation");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.EnoughFood).HasColumnName("enough_food");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.GenderAvatar)
                .HasMaxLength(100)
                .HasColumnName("gender_avatar");
            entity.Property(e => e.GroceryTransportation).HasColumnName("grocery_transportation");
            entity.Property(e => e.HealthInsurance).HasColumnName("health_insurance");
            entity.Property(e => e.HealthPlan)
                .HasMaxLength(50)
                .HasColumnName("health_plan");
            entity.Property(e => e.HouseholdSize).HasColumnName("household_size");
            entity.Property(e => e.Illnesses)
                .HasMaxLength(150)
                .HasColumnName("illnesses");
            entity.Property(e => e.InsecurityFrequency)
                .HasMaxLength(50)
                .HasColumnName("insecurity_frequency");
            entity.Property(e => e.JobLossOrReducedHours).HasColumnName("job_loss_or_reduced_hours");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MedicalTransportation).HasColumnName("medical_transportation");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Posted).HasColumnName("posted");
            entity.Property(e => e.PriorityId).HasColumnName("priority_id");
            entity.Property(e => e.Reason)
                .HasMaxLength(150)
                .HasColumnName("reason");
            entity.Property(e => e.SeniorsInHousehold).HasColumnName("seniors_in_household");
            entity.Property(e => e.Source)
                .HasMaxLength(50)
                .HasColumnName("source");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");
            entity.Property(e => e.Story)
                .HasMaxLength(600)
                .HasColumnName("story");
            entity.Property(e => e.Zip)
                .HasMaxLength(50)
                .HasColumnName("zip");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
