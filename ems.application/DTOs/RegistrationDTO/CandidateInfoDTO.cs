using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.RegistrationDTO
{
    public class CandidateInfoDTO
    {
        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string? Pan { get; set; }

        public string? Aadhaar { get; set; }

        public string CandidateReferenceCode { get; set; } = null!;

        public string? ResumeUrl { get; set; }

        public decimal ExperienceYears { get; set; }

        public string? CurrentLocation { get; set; }

        public decimal? ExpectedSalary { get; set; }

        public string? CurrentCompany { get; set; }

        public int? NoticePeriod { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? SkillSet { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<CandidateDepartmentModuleSkill> CandidateDepartmentModuleSkills { get; set; } = new List<CandidateDepartmentModuleSkill>();

        public virtual ICollection<CandidateHistory> CandidateHistories { get; set; } = new List<CandidateHistory>();

        public virtual ICollection<InterviewFeedback> InterviewFeedbacks { get; set; } = new List<InterviewFeedback>();

        public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; } = new List<InterviewSchedule>();
    }
}
