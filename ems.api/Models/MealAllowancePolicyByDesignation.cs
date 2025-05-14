﻿using System;
using System.Collections.Generic;

namespace ems.api.Models;

public partial class MealAllowancePolicyByDesignation
{
    public int Id { get; set; }

    public int PolicyTypeId { get; set; }

    public int EmployeeTypeId { get; set; }

    public int DesignationId { get; set; }

    public int? MinDaysRequired { get; set; }

    public decimal? FixedFoodAllowance { get; set; }

    public decimal? BreakfastAllowance { get; set; }

    public decimal? LunchAllowance { get; set; }

    public decimal? DinnerAllowance { get; set; }

    public bool? IsMetro { get; set; }

    public decimal? MetroBonus { get; set; }

    public string? RequiredDocuments { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsSoftDelete { get; set; }

    public DateTime? SoftDeleteDateTime { get; set; }

    public int AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public int? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual Designation Designation { get; set; } = null!;

    public virtual EmployeeType EmployeeType { get; set; } = null!;

    public virtual PolicyType PolicyType { get; set; } = null!;
}
