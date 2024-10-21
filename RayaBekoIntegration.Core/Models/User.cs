using System;
using System.Collections.Generic;

namespace RayaBekoIntegration.EF;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? AccessToken { get; set; }

    public DateTime? AccessTokenExpiry { get; set; }

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
