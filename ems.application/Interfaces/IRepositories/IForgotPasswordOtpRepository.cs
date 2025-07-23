﻿using System.Threading.Tasks;
using ems.domain.Entity; // ← jahan ForgotPasswordOtp entity rakhi hai

namespace ems.application.Interfaces.IRepositories
{
    public interface IForgotPasswordOtpRepository
    {
        Task<ForgotPasswordOTPDetail?> GetValidOtpByEmployeeIdAsync(long employeeId, long? TenantId);
        Task<ForgotPasswordOTPDetail?> GetOtpValidateTrueAndUsedFalseByEmployeeIdAsync(long employeeId, long? TenantId);


        Task<long> AddAsync(ForgotPasswordOTPDetail otp);
        Task DeleteAsync(ForgotPasswordOTPDetail otp);

        Task<ForgotPasswordOTPDetail?> GetByOtpAndEmployeeIdAsync(string otp, long employeeId);

        Task<bool> UpdateOTPAsync(ForgotPasswordOTPDetail otp); // ✅ New method
    }
}
