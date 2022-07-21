using Sprache;
using System.Linq;

namespace roll20_adv_import_c
{
    public class TorCoreAdvParser : TorAdvParser
    {
        private readonly static Parser<Adversary> adv_core =
            from leading in Parse.AnyChar.Except(listParserAdversaries).Many()
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
            from weaponProf in TokenCombatProficiencies.Then(_ => weapons.Optional())
            from fellAbilities in TokenFellAbilities.Then(_ => fellAbilityList.Optional())
            select new Adversary()
            {
                name = name,
                distinctiveFeatures = dfeat.IsDefined ? dfeat.Get() : "",
                attributeLevel = attributeLevel,
                endurance = endurance,
                might = might,
                hate = tokenHateResolve.IsDefined && tokenHateResolve.Get() == "Hate" ? 
                    (hateResolve.IsDefined ? hateResolve.Get() : "") : "",
                resolve = tokenHateResolve.IsDefined && tokenHateResolve.Get() == "Resolve" ? 
                    (hateResolve.IsDefined ? hateResolve.Get() : "") : "",
                parry = parry,
                armour = armour,
                weaponProficiencies = weaponProf.IsDefined ? weaponProf.Get() : new WeaponProficiency[0],
                fellAbilities = fellAbilities.IsDefined ? fellAbilities.Get() : new FellAbility[0]
            };
        public static readonly Parser<Adversary[]> advs_core =
            from a in adv_core.DelimitedBy(Parse.AnyChar.Except(listParserAdversaries).Many().Text())
            select a.ToArray();

    }
}