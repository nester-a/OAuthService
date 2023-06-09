﻿using OAuthService.Core.Constans;
using OAuthService.Core.Exceptions.Base;

namespace OAuthService.Core.Exceptions
{
    /// <summary>Выбрасывается когда сервис не поддерживает грант тип пришедший в запросе</summary>
    public class UnsupportedGrantTypeException : OAuthException
    {
        public UnsupportedGrantTypeException(string? errorDescription)
            : base(OAuthError.UnsupportedGrantType, errorDescription) { }
    }
}
