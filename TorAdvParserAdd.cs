using Sprache;
using System;
using System.Linq;  

namespace roll20_adv_import_c
{
    public class TorAdvParserAdd : TorAdvParser
    {
        private readonly static Parser<Adversary> adv_add =
            from leading in Parse.AnyChar.Except(listParserAdversaries).Many()
            from name in listParserAdversaries
            from dfeat in DistinctiveFeatureParser.Optional()
            from weaponProf in TokenCombatProficiencies.Optional().Then(_ => weapons.Optional())
            from fellAbilities in TokenFellAbilities.Optional().Then(_ => fellAbilityList.Optional())
            from attributeLevel in TokenAttributeLevel.Optional().Then(_ => Parse.Number.Token().Optional())
            from endurance in TokenEndurance.Optional().Then(_ => Parse.Number.Token().Optional())
            from might in TokenMight.Token().Optional().Then(_ => Parse.Number.Token().Optional())
            from tokenHateResolve in TokenHateResolve.Token().Optional()
            from hateResolve in Parse.Number.Token().Optional()
            from hateResolveRest in Parse.AnyChar
                .Except(TokenParry)
                .Except(listParserAdversaries)
                .Except(listParserAdversariesEnd)
                .Many().Token().Text().Optional() // needed to parse VOGAR who has hate/resolve: 2/6
            from parry in TokenParry.Optional().Then(_ => Parse.AnyChar
                .Except(TokenArmour)
                .Except(listParserAdversaries)
                .Except(listParserAdversariesEnd)
                .Many().Token().Text().Optional())
            from armour in TokenArmour.Optional().Then(_ => Parse.Number.Token().Optional())
            from end in Parse.AnyChar
                .Except(listParserAdversaries)
                .Except(listParserAdversariesEnd)
                .Optional()
            select new Adversary()
            {
                name = name,
                distinctiveFeatures = dfeat.IsDefined ? dfeat.Get() : null,
                attributeLevel = attributeLevel.IsDefined ? attributeLevel.Get() : null,
                endurance = endurance.IsDefined ? endurance.Get() : null,
                might = might.IsDefined ? might.Get() : null,
                hate = tokenHateResolve.IsDefined && tokenHateResolve.Get() == "Hate" ? 
                    (hateResolve.IsDefined ? hateResolve.Get() : null) : null,
                resolve = tokenHateResolve.IsDefined && tokenHateResolve.Get() == "Resolve" ? 
                    (hateResolve.IsDefined ? hateResolve.Get() : null) : null,
                parry = (parry.IsDefined ? parry.Get() : null) + (hateResolveRest.IsDefined ? hateResolveRest.Get() : null),
                armour = armour.IsDefined ? armour.Get() : null,
                weaponProficiencies = weaponProf.IsDefined ? weaponProf.Get() : null,
                fellAbilities = fellAbilities.IsDefined ? fellAbilities.Get() : null
            };
        public static readonly Parser<Adversary[]> advs_add =
            from a in adv_add.DelimitedBy(Parse.AnyChar.Except(listParserAdversaries).Many().Text())
            select a.ToArray();
    }
}
