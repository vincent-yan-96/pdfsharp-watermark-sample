using System.Reflection;

internal static class FontHelper
{
    public static byte[] ArialBold => LoadFontData("WaterMarkConsole.fonts.Arial-bold.ttf");

    /// <summary>
    /// Returns the specified font from an embedded resource.
    /// </summary>
    public static byte[] LoadFontData(string name)
    {
        var assembly = Assembly.GetExecutingAssembly();

        using Stream stream = assembly.GetManifestResourceStream(name);
        if (stream == null)
        {
            throw new ArgumentException("No resource with name " + name);
        }

        int count = (int)stream.Length;
        byte[] data = new byte[count];
        stream.ReadExactly(data, 0, count);
        return data;
    }
}
