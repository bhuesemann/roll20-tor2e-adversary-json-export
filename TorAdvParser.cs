using System;
using Sprache;
using System.Collections.Generic;
using System.Linq;

namespace roll20_adv_import_c
{
    public class TorAdvParser
    {

        public static Parser<string> ListParser(List<string> lst)
        {
            var parser = Parse.String(lst.First()).Text();
            foreach (var item in lst.Skip(1))
            {
                parser = parser.Or(Parse.String(item)).Text();
            }
            return parser;
        }

        public static Parser<string> listParserAdversaries = ListParser(Config.AdversaryTokenList);
        public static Parser<string> listParserFellAbilities = ListParser(Config.FellAbilitiesTokenList);
        public static Parser<string> listParserAdversariesStart = ListParser(Config.AdversaryTokenListStart);
        public static Parser<string> listParserAdversariesEnd = ListParser(Config.AdversaryEndTokenList);
        public static Parser<string> TokenAttributeLevel = Parse.IgnoreCase("ATTRIBUTE LEVEL").Token().Text();
        public static Parser<string> TokenEndurance = Parse.IgnoreCase("ENDURANCE").Token().Text();
        public static Parser<string> TokenMight = Parse.IgnoreCase("MIGHT").Token().Text();
        public static Parser<string> TokenResolve = Parse.IgnoreCase("RESOLVE").Token().Text();
        public static Parser<string> TokenHateResolve = Parse.IgnoreCase("HATE").Or(Parse.IgnoreCase("RESOLVE")).Token().Text();
        public static Parser<string> TokenParry = Parse.IgnoreCase("PARRY").Token().Text();
        public static Parser<string> TokenArmour = Parse.IgnoreCase("ARMOUR").Token().Text();
        public static Parser<string> TokenCombatProficiencies = Parse.IgnoreCase("COMBAT PROFICIENCIES:").Token().Text();
        public static Parser<string> TokenFellAbilities = Parse.IgnoreCase("FELL ABILITIES:").Token().Text();

        public static Parser<string> WordParser = Parse.Letter.Many().Token().Or(Parse.String("2-Handed")).Text();
        public static Parser<string> WordOrMinusParser =
            Parse.Letter.Or(Parse.Char('-'))
            .Except(TokenAttributeLevel)
            .Many().Token().Text();

        public static Parser<string> NumberOrMinus =
            from min in Parse.Char('-').Optional()
            from num in Parse.Number.Token().Optional()
            select num.IsDefined ? num.Get() : min.IsDefined ? min.Get().ToString() : "";

        public static Parser<string> PhraseParser =
            from leading in Parse.Letter.Many().Token().Text()
            from rest in Parse.Chars(' ', '-', ',', ':').Many().Then(_ => WordParser).Many()
            select leading + " " + String.Join(" ", rest);

        public static Parser<string> TokenOrder =
            from name in PhraseParser
            from leftpart in Parse.String("(Order #")
            from ordernumber in Parse.Number.Token()
            from rightpart in Parse.Char(')')
            select ordernumber;

        public static Parser<FellAbility> FellAbilityParser =
            from fellAbilityName in listParserFellAbilities
            from fellAbilityDescription in Parse.AnyChar
                    .Except(TokenAttributeLevel)
                    .Except(listParserAdversaries)
                    .Except(listParserAdversariesStart)
                    .Except(listParserAdversariesEnd)
                    .Except(listParserFellAbilities)
                    .Except(TokenOrder)
                    .Many()
                    .Token()
                    .Text()
            select new FellAbility()
            {
                abilityname = fellAbilityName.Trim(),
                description = fellAbilityDescription.Trim()
            };

        public static Parser<FellAbility[]> fellAbilityList =
                from abilities in FellAbilityParser.Many()
                from rest in Parse.AnyChar
                    .Except(listParserAdversariesStart)
                    .Except(TokenAttributeLevel)
                    .Except(listParserAdversaries)
                    .Except(listParserAdversariesEnd)
                    .Many().Token().Text().Optional()
                select abilities.ToArray();

        public static Parser<string> DistinctiveFeatureParser =
            from first in WordOrMinusParser
                // Folulf and Arnulf -> Swift (Folulf), Wary (Arnulf)
            from left in Parse.Char('(').Optional()
            from middle in WordParser.Optional()
            from right in Parse.Char(')').Optional()
            from sep in Parse.Char(',').Token()
            from second in WordOrMinusParser
            from rest in Parse.AnyChar
                .Except(TokenCombatProficiencies)
                .Except(TokenAttributeLevel)
                .Many().Token().Text()
            select first + ", " + second;

        public static Parser<WeaponProficiency> WeaponParser =
            from weaponname in PhraseParser.Text()
            from s1 in Parse.WhiteSpace.Many()
            from rating in Parse.Number.Token().Optional()
            from s2 in Parse.WhiteSpace.Many()
            from lpar in Parse.Char('(')
            from damage in NumberOrMinus.Token()
            from sep in Parse.Char('/')
            from injury in NumberOrMinus.Token()
            from comma in Parse.Char(',').Many().Token()
            from special in PhraseParser.Text()
            from rpar in Parse.Char(')')
            from rest in Parse.AnyChar
                .Except(Parse.Chars(',', '.', ' '))
                .Except(TokenFellAbilities)
                .Except(listParserAdversaries)
                .Except(listParserAdversariesEnd)
                .Many().Text()
            from point in Parse.Char('.').Optional()
            select new WeaponProficiency()
            {
                weaponname = weaponname.Trim(),
                rating = rating.IsDefined ? rating.Get() : "",
                damage = damage,
                injury = injury,
                special = special.Trim()
            };

        public static Parser<WeaponProficiency[]> weapons =
            from a in WeaponParser.DelimitedBy(Parse.Chars(',', ' ', '.').Many().Token())
            from weaponProfRest in Parse.AnyChar
                .Except(TokenFellAbilities)
                .Except(TokenAttributeLevel)
                .Except(listParserAdversaries)
                .Except(listParserAdversariesEnd)
                .Many().Token().Text().Optional()
            select a.ToArray();
    }
}
