using System;
using System.Collections.Generic;

namespace EPAPI.Models;

public partial class SellerSocialMedium
{
    public int Id { get; set; }

    public int SellerId { get; set; }

    public string SocialMediaName { get; set; } = null!;

    public string SocialMediaUrl { get; set; } = null!;

    public virtual Seller Seller { get; set; } = null!;
}
