﻿namespace SuperCarga.Application.Domain.Users.Dto
{
    public class TokenDto
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiry { get; set; }
    }
}
