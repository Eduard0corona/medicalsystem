namespace MS.Application.Authorization.Features.AuthFeatures.Commands.UpdateRefreshToken
{
    public sealed record UpdateRefreshTokenResponse
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
