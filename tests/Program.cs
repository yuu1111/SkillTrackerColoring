using System;
using System.Collections.Generic;
using SkillTrackerColoring;

internal static class Program
{
    private static int _passed;

    private static void Equal(string requirement, string expected, string actual)
    {
        if (expected != actual) throw new Exception(requirement + ": expected [" + expected + "] but got [" + actual + "]");
        _passed++;
    }

    private static void Main()
    {
        var colors = new Dictionary<string, string> { { "Magic", "CC0099" }, { "Magic Device", "6666CC" } };
        var formatter = new TrackerTextFormatter();
        var raw = "Magic Device  10.25<size=13> (100)</size>\nMagic  3\nUnknown  2\n";
        var expected = "<color=#6666CC>Magic Device</color>  10.25<size=13> (100)</size>\n<color=#CC0099>Magic</color>  3\nUnknown  2\n";
        var colored = formatter.Format(raw, colors, 0);
        Equal("Only exact skill names are colored; values, potential tags and newlines are preserved", expected, colored);
        for (var i = 0; i < 1000; i++) colored = formatter.Format(colored, colors, 0);
        Equal("Repeated unchanged refreshes never accumulate tags", expected, colored);
        Equal("Unchanged refresh skips formatting", "False", formatter.NeedsFormatting(colored, 0).ToString());
        colors["Magic"] = "009933";
        Equal("Config changes recolor stored raw text", expected.Replace("CC0099", "009933"), formatter.Format(colored, colors, 1));
        Equal("Changed raw values are not replaced by stale cached values", "<color=#009933>Magic</color>  4", formatter.Format("Magic  4", colors, 1));
        Equal("Substring matches do not color unrelated skills", "Advanced Magic  5", formatter.Format("Advanced Magic  5", colors, 1));
        colors["魔法"] = "CC0099";
        Equal("Language changes use current localized names", "<color=#CC0099>魔法</color>  6", formatter.Format("魔法  6", colors, 1));
        Equal("Empty tracker placeholders stay unchanged", "None", formatter.Format("None", colors, 1));
        Equal("Empty strings stay unchanged", "", formatter.Format("", colors, 1));
        Equal("Null strings stay unchanged", null, formatter.Format(null, colors, 1));
        var independent = new TrackerTextFormatter();
        Equal("New widget state is independent", "<color=#009933>Magic</color>  7", independent.Format("Magic  7", colors, 1));
        Equal("Whitespace and optional hash are accepted", "aBc123", TrackerTextFormatter.NormalizeHex(" #aBc123 ", "CC0000"));
        Equal("Bare hex is accepted", "009933", TrackerTextFormatter.NormalizeHex("009933", "CC0000"));
        foreach (var invalid in new[] { null, "", "#", "red", "#123", "#12345678", "#GGGGGG", "##123456", "12345<", "</color>" })
            Equal("Invalid colors use the attribute default", "CC0000", TrackerTextFormatter.NormalizeHex(invalid, "CC0000"));
        Console.WriteLine("PASS: " + _passed + " checks; 0 failures.");
    }
}
