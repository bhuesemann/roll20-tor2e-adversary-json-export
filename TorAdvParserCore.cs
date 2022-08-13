using Sprache;
using System;
using System.Linq;

namespace roll20_adv_import_c
{
    public class TorAdvParserCore : TorAdvParser
    {
        public static void Init()
        {
            Config.InitCore();
            listParserAdversaries = ListParser(Config.AdversaryTokenList);
        }
        private readonly static Parser<Adversary> adv =
            from leading in Parse.AnyChar
                .Except(listParserAdversariesStart)
                .Except(listParserAdversaries)
                .Many().Optional()
            from description_name in listParserAdversariesStart.Optional()
            from description in Parse.AnyChar.Except(listParserAdversaries).Many().Token().Text().Optional()
            from name in listParserAdversaries
            from dfeat in DistinctiveFeatureParser.Optional()
            from tokenAttributeLevel in TokenAttributeLevel.Token()
            from attributeLevel in Parse.Number.Token()
            from tokenEndurance in TokenEndurance.Token()
            from endurance in Parse.Number.Token()
            from tokenMight in TokenMight.Token()
            from might in Parse.Number.Token()
            from tokenHateResolve in TokenHateResolve.Token().Optional()
            from hateResolve in Parse.Number.Token().Optional()
            from hateResolveRest in Parse.AnyChar
                .Except(TokenParry)
                .Except(listParserAdversaries)
                .Except(listParserAdversariesEnd)
                .Many().Token().Text().Optional() // needed to parse VOGAR who has hate/resolve: 2/6
            from tokenParry in TokenParry.Token()
            from parry in Parse.AnyChar.Except(TokenArmour).Many().Token().Text()
            from tokenArmour in TokenArmour
            from armour in Parse.Number.Token()
            from armourrest in Parse.AnyChar
                .Except(TokenCombatProficiencies)
                .Except(listParserAdversaries)
                .Many().Token().Text().Optional() // needed to parse ELWEN who is on two separate pages
            from weaponProf in TokenCombatProficiencies.Then(_ => weapons.Optional())
            from fellAbilities in TokenFellAbilities.Then(_ => fellAbilityList.Optional())
            select new Adversary()
            {
                name = name,
                distinctiveFeatures = dfeat.IsDefined ? dfeat.Get() : "",
                attributeLevel = attributeLevel,
                endurance = endurance,
                might = might,
                hate = !tokenHateResolve.IsDefined || !tokenHateResolve.Get().Equals("Hate", StringComparison.OrdinalIgnoreCase) ?
                    null : (hateResolve.IsDefined ? hateResolve.Get() : null),
                resolve = tokenHateResolve.IsDefined && tokenHateResolve.Get().Equals("Resolve", StringComparison.OrdinalIgnoreCase) ?
                    (hateResolve.IsDefined ? hateResolve.Get() : null) : null,
                parry = parry,
                armour = armour,
                weaponProficiencies = weaponProf.IsDefined ? weaponProf.Get() : new WeaponProficiency[0],
                fellAbilities = fellAbilities.IsDefined ? fellAbilities.Get() : new FellAbility[0],
                description = description.IsDefined && description.Get() != "" ? description.Get() : null,
            };
        public static readonly Parser<Adversary[]> advs =
            from a in adv.DelimitedBy(Parse.AnyChar.Except(listParserAdversaries).Many().Text())
            select a.ToArray();

    }
}
