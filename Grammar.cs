using Sprache;
using System;
static class Grammar
{
    private static readonly Parser<char> SeparatorChar =
        Parse.Chars("()<>@,;:\\\"/[]?={} \t");

    private static readonly Parser<char> ControlChar =
        Parse.Char(Char.IsControl, "Control character");

}