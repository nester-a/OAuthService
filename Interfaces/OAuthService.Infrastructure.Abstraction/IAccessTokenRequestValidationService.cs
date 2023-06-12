﻿using OAuth.Types.Abstraction;

namespace OAuthService.Interfaces.Validation
{
    public interface IAccessTokenRequestValidationService
    {
        Task ValidateAsync(IAccessTokenRequest request, CancellationToken cancellation = default);
    }
}