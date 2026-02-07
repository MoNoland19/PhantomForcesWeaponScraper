using System;

namespace PFWeaponScraper.Utils;

public static class TextNormalizer
{
    public static string Normalize(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "None";

        string cleaned = input
            .Replace("\n", " ")
            .Replace("\r", " ")
            .Replace("\t", " ")
            .Trim();

        if (string.Equals(cleaned, "none", StringComparison.OrdinalIgnoreCase))
            return "None";

        return cleaned;
    }

    public static string NormalizeGunName(string? input)
    {
        return Normalize(input);
    }

    public static string NormalizeAttachment(string? input)
    {
        return Normalize(input);
    }
}