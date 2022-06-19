using Sprache;
namespace roll20_adv_import_c
{
    public static class TorCoreParser
    {
        public class Adversary
        {
            public string name;
            public string distinctiveFeatures;
            public string attributeLevel;
            public string endurance;

        }
        public static Parser<string> TokenAttributeLevel = Parse.String("ATTRIBUTE LEVEL").Text();
        public static Parser<string> TokenEndurance = Parse.String("ENDURANCE").Text();
        public static Parser<string> TokenMight = Parse.String("MIGHT").Text();
        public static Parser<string> TokenResolve = Parse.String("RESOLVE").Text();
        public static Parser<string> TokenHate = Parse.String("HATE").Text();
        public static Parser<string> TokenParry = Parse.String("PARRY").Text();

        public static Parser<Adversary> adv =
            from leading in Parse.AnyChar.Many().Text()
            from attributeLevel in TokenAttributeLevel
            from attributeLevelValue in Parse.Numeric.Many().Text()
            from endurance in TokenEndurance
            from enduranceValue in Parse.Numeric.AtLeastOnce().Text()
            select new Adversary() { endurance = enduranceValue };
    }
}
