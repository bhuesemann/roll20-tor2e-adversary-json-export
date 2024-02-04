using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;

namespace roll20_adv_import_c
{
    public class TorStriderParser : TorParser
    {
        public static void Init()
        {
            Console.WriteLine("Nothing to initialize");
        }

        private static Parser<string> listParserTables = ListParser(Config.RolltablesTokenList);
        private static Parser<string> listParserColHeaders = ListParser(Config.RolltableColHeaderList);

        private static Parser<RolltableRow> tablerow =
            from add1 in Parse.Number.Optional()
            from add2 in Parse.AnyChar
                .Except(listParserTables)
                .Except(Parse.Number)
                .Except(TokenOrder)
                .Many().Text().Optional()
            select new RolltableRow()
            {
                add1 = add1.IsDefined ? add1.Get() : null,
                add2 = add2.IsDefined ? add2.Get() : null,
            };
        private static Parser<RolltableRow[]> tablerows =
            from a in tablerow.Many().Optional()
            select a.GetOrElse(new RolltableRow[] { }).ToArray();

        private static Parser<Rolltable> tab =
            from leading in Parse.AnyChar.Except(listParserTables).Many().Text()
            from title in listParserTables.Token()
            from columns in listParserColHeaders.Token().Optional()
            from rows in tablerows.Optional()
            from rest in Parse.AnyChar.Except(listParserTables).Many().Text().Optional()
            select new Rolltable()
            {
                name = title,
                columns = columns.IsDefined ? columns.Get() : null,
                description = rest.IsDefined ? rest.Get() : null,
                entries = rows.IsDefined ? rows.Get() : null,
            };
        public static readonly Parser<Rolltable[]> tabs =
        from a in tab.DelimitedBy(Parse.AnyChar.Except(listParserTables).Many().Text())
        select a.ToArray();

    }
}
