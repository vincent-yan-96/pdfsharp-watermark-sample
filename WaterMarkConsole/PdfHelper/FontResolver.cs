using PdfSharp.Fonts;

internal sealed class FontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        return FontHelper.ArialBold;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo("Arial-bold");
    }
}
