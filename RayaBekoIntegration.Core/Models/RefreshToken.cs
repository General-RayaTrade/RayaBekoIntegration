﻿using System;
using System.Collections.Generic;

namespace RayaBekoIntegration.EF;

public partial class RefreshToken
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime ExpiryDate { get; set; }

    public bool IsRevoked { get; set; }

    public virtual User User { get; set; } = null!;
}
