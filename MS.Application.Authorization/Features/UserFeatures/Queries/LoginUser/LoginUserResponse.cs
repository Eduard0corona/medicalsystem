namespace MS.Application.Authorization.Features.UserFeatures.Queries.LoginUser
{
    public sealed record LoginUserResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken {  get; set; } = string.Empty;
    }
}
