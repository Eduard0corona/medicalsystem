﻿using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Common.Interfaces
{
    public interface ISecurityService
    {
        string GenerateToken(User userInfo);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        int? ValidateJwtToken(string? token);
    }
}
