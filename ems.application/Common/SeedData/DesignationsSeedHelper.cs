﻿using ems.application.Constants;
using ems.domain.Entity;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Serilog.Core;

namespace ems.application.Common.SeedData
{
    public static class DesignationsSeedHelper
    {
        /// <summary>
        /// Maps static seed data to Designation entities using DepartmentName → DepartmentId mapping.
        /// </summary>
        public static List<Designation> GetRuntimeDesignationsToSeed(
            long tenantId,
            long addedById,
            Dictionary<string, int> departmentNameIdMap)
        {
            var designations = new List<Designation>();

            // Basic validations
            if (tenantId <= 0)
                throw new ArgumentException("Invalid tenantId");

            if (addedById <= 0)
                throw new ArgumentException("Invalid addedById");

            if (departmentNameIdMap == null || !departmentNameIdMap.Any())
                return designations;

            foreach (var dept in departmentNameIdMap)
            {
                if (string.IsNullOrWhiteSpace(dept.Key) || dept.Value <= 0)
                    continue;

                var departmentName = dept.Key.Trim().ToLower();
                var departmentId = dept.Value;
                switch (departmentName?.ToLower())
                {
                    case "it-department":
                        designations.AddRange(new[]
                        {
            CreateDesignation("Software Developer", "Develops and maintains software solutions."),
            CreateDesignation("QA Engineer", "Ensures the quality of applications through testing."),
            CreateDesignation("DevOps Engineer", "Handles CI/CD pipelines and deployment.")
        });
                        break;

                    case "hr-department":
                        designations.AddRange(new[]
                        {
                          CreateDesignation("HR Executive", "Manages employee relations and policies."),
                          CreateDesignation("Recruiter", "Sources, screens, and hires talent."),
                          CreateDesignation("HR Manager", "Oversees HR operations and compliance.")
                     });
                        break;

                    case "finance-department":
                        designations.AddRange(new[]
                        {
                           CreateDesignation("Accountant", "Manages financial records and transactions."),
                            CreateDesignation("Finance Manager", "Oversees budgeting, audits, and financial planning.")
                        });
                        break;

                    case "asset-department":
                        designations.AddRange(new[]
                        {
                         CreateDesignation("Asset Executive", "Maintains asset inventory and tracking."),
                         CreateDesignation("Asset Manager", "Manages asset lifecycle and allocation.")
                            });
                        break;

                    case "executive-office":
                        designations.AddRange(new[]
                        {
                           CreateDesignation("CEO", "Chief Executive Officer - Overall company leadership.")
                           //CreateDesignation("COO", "Chief Operating Officer - Oversees daily operations."),
                           //CreateDesignation("CTO", "Chief Technology Officer - Leads technology strategy.")
                          });
                        break;

                    default:
                         Console.Write("No designation seed defined for department: {Department}", departmentName);
                        break;
                }


                // Helper local function to reduce redundancy
                Designation CreateDesignation(string name, string desc) => new Designation
                {
                    TenantId = tenantId,
                    DepartmentId = departmentId,
                    DesignationName = name,
                    Description = desc,
                    IsActive = true,
                    IsSoftDeleted = false,
                    AddedById = addedById,
                    AddedDateTime = DateTime.UtcNow
                };
            }

            return designations;
        }
    }
}
