using System;
using System.IO;
using System.Text;
using Sprache;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

namespace roll20_adv_import_c
{
    class Program
    {
        public class FellAbility
        {
            public string abilityname { get; set; }
            public string description { get; set; }
        }

        public class WeaponProficiency
        {
            public string weaponname { get; set; }
            public string rating { get; set; }
            public string damage { get; set; }
            public string injury { get; set; }
            public string special { get; set; }
        }
        public class Adversary
        {
            public string name { get; set; }
            public string distinctiveFeatures { get; set; }
            public string attributeLevel { get; set; }
            public string endurance { get; set; }
            public string might { get; set; }
            public string hateresolve { get; set; }
            public string parry { get; set; }
            public string armour { get; set; }
            public WeaponProficiency[] weaponProficiencies { get; set; }
            public FellAbility[] fellAbilities { get; set; }
        }

        public static readonly List<string> FellAbilitiesTokenList = new List<string>{
            "Beast-Tamer",
            "Berserk Rage",
            "Bewilder",
            "Black Breath",
            "Black Dread",
            "Countless Children",
            "Cowardly",
            "Craven",
            "Crazed",
            "Cruel Stroke",
            "Dark Glamour",
            "Darker than the Darkness",
            "Deadly Elusiveness",
            "Deadly Misfortune",
            "Deadly Voice",
            "Deadly Wound",
            "Deathless",
            "Defend Ally",
            "Denizen of the Dark",
            "Disgorge",
            "Dreadful Spells",
            "Drowning in Sorrow",
            "Dwimmerlaik",
            "Enthrall",
            "Fear of Fire",
            "Feast on Suffering",
            "Fell Speed",
            "Fierce Folk",
            "Fire Breath",
            "Formidable",
            "Foul Reek",
            "Four-Armed",
            "Ghost Form",
            "Gorlanc’s Poison",
            "Great Leap",
            "Hate Sunlight",
            "Hatred (Beornings & Elves)",
            "Hatred (Beornings)",
            "Hatred (Dunedain)",
            "Hatred (Dunedain) and (Elves)",
            "Hatred (Dwarves)",
            "Hatred (Elves)",
            "Hatred (Everyone)",
            "Hatred (First Foe to Strike Him)",
            "Hatred (Hobbits)",
            "Hatred (Men of Rohan)",
            "Hatred (Orcs)",
            "Hatred (Riders of Rohan)",
            "Heartless",
            "Hero is caught up in fantasies",
            "Hideous Toughness",
            "Horrible Strength",
            "Horror of the Wood",
            "Many Poisons",
            "Mirkwood Dweller",
            "Paralyzing-Poison",
            "Poison Blast",
            "Poisoned Blade",
            "Prohibition",
            "Raven Spirits",
            "Reckless Hate",
            "Savage Assault",
            "Seize and Drown",
            "Shade Caller",
            "Shadow of Fear",
            "Sleep",
            "Snake-Like Speed",
            "Strange Venoms",
            "Strike Fear",
            "Terror of Desire",
            "Thick Hide",
            "Thing of Terror",
            "Thrall (Spiders)",
            "Two-Headed",
            "Two-Headed OR Four-Armed",
            "Venomous Breath",
            "Visions of Torment",
            "Weak Spot",
            "Weakened",
            "Web",
            "Webs of Illusion",
            "Wicked Cunning",
            "Words of Power and Terror",
            "Wraith-Like",
            "Wrapped in Shadow",
            "Yell of Triumph"
        };
        public static readonly List<string> AdversaryTokenList = new List<string> {
            // Additional
            "BLACK URUK"
            , "MESSENGER OF LUGBURZ"
            , "SNAGA TRACKER"
            , "ORCS OF MOUNT GRAM"
            , "GOBLINS OF CARN DUM"
            , "ORCS OF THE WHITE MOUNTAINS"
            , "HALF-ORCS"
            , "URUK-HAI SOLDIERS"
            , "URUK-HAI CAPTAIN [E]"
            , "GOBLIN-MEN"
            , "ORCISH WOLF-RIDERS"
            , "HOBGOBLINS"
            , "MARSH HAGS [E]"
            , "FOREST GOBLIN"
            , "THE PALE ONES"
            , "COMMON HILL TROLL"
            , "HILL-TROLL CHIEF [E]"
            , "MOUNTAIN TROLL [E]"
            , "ETTINS"
            , "FUNGAL TROLL"
            , "MARSH-OGRE"
            , "ATTERCOP"
            , "GREAT SPIDER"
            , "HUNTER SPIDER"
            , "GREAT BAT"
            , "SECRET SHADOW [D]"
            , "HILL-MEN OF RHUDAUR"
            , "GUNDABAD SPIRIT WARGS"
            , "HILL-MEN OF GUNDABAD"
            , "WILD MEN OF MIRKWOOD"
            , "DUNLENDING RAIDERS"
            , "WULFING RIDERS"
            , "WARRIORS OF THE GAESELA"
            , "MEN OF ISENGARD"
            , "BOG SOLDIERS [M]"
            , "DEAD MEN OF DUNHARROW"
            , "SPECTRES"
            , "WOOD-WIGHT"
            , "GRIM HAWKS"
            , "BASILISK"
            , "HUORNS."
            , "THE QUEEN ON CASTLE HILL [E]"
            , "BLOODSTUMP THE HUNTER [E]"
            , "GORGOL, SON OF BOLG [E]"
            , "MAGHAZ, ORC-CAPTAIN [E]"
            , "THE NEW GREAT GOBLIN [E]"
            , "NAGRHAW, CHIEF OF THE WARGS [E]"
            , "BLACK WARG OF METHEDRAS [E]"
            , "STONECLAWS THE BEAR [E]"
            , "GREAT BOAR OF EVERHOLT [E]"
            , "DREORG THE WARGLING [E]"
            , "RADGUL THE ORC-CHIEF [E]"
            , "THE WIGHT-KING [E][D]"
            , "THE WITCH-KING OF ANGMAR"
            , "THE HORSE-EATER [E]"
            , "CASELWUN"
            , "BLODRED"
            , "RHONWEN"
            , "DUNLENDING WARRIORS"
            , "THE BARROW-WITCH"
            , "THE GREY HORSE [D]"
            , "HORSE-LORDS SPECTRE"
            , "AGENTS OF MORDOR"
            , "BANDITS FROM THE SOUTH"
            , "HIRDAN, BANDIT LEADER [E]"
            , "MALTHOR, THE AXE-BITTEN [E]"
            , "GAZHUR THREE-DEATHS [E]"
            , "THUGS"
            , "THE THING IN THE WELL"
            , "NIGHT-WIGHT"
            , "VALTER THE BLOODY"
            , "OUTLAWS"
            , "FARON THE TRAPPER"
            , "ODERIC"
            , "UNDEAD WARRIORS [M]"
            , "GHOR THE DESPOILER [E]"
            , "EASTERLING WARRIORS"
            , "GEROLD THE BEORNING"
            , "ELSTAN, CAPTAIN OF DALE [E]"
            , "ELSTAN FOLLOWERS"
            , "THE GIBBET KINGN [E] [M]"
            , "SNOW TROLLS"
            , "RAENAR THE COLD-DRAKE [E] [D]"
            , "SAVAGE WOLFDOGS"
            , "HEDDWYN AS SPIRIT-WARG"
            , "THE LURKER IN THE LONG VALLEY"
            , "HEDDWYN [D]"
            , "SERGEANT CYRNAN"
            , "RUTHLESS BANDITS"
            , "ETTIN GUARDIAN"
            , "CAPTAIN MORMOG [E]"
            , "HOBBIT SPECTRES"
            , "FELL WRAITH"
            , "CARADOG, DUNLENDING HUNTER"
            , "UATACH"
            , "ARMED MEN"
            , "DUNLENDING WARRIORS"
            , "ARMED VILLAGERS"
            , "ELWIN"
            , "FAY"
            , "HERBERT"
            , "FOLULF AND ARNULF"
            , "HULDRAHIR [D]"
            , "THE DEADLY ONE [E]"
            , "GOBLIN HORDE"
            , "THIEVING DWARVES"
            , "LOFAR LIGHT-FINGER"
            , "THE OLD TROLL [E]"
            , "BRUTES OF BREE"
            , "GROR THE DWARF"
            , "EDORIC"
            , "VOGAR"
            , "NARVI"
            , "HIRLINION"
            , "BERELAS [E]"
            , "THE COLD SHADE [D]"
            , "WHITE WOLF OF THE NORTH [E]"
            , "GORLANC’S WARRIORS"
            , "BLUEBELL WOOD OAKMEN"
            , "GORLANC’S FOLLOWERS"
            , "GORLANC [D]"
            , "LONGO’S LIEUTENANTS"
            , "LONGO"
            , "GUNVAR’S BODYGUARDS"
            , "GUNVAR’S MEN-AT-ARMS"
            , "FIRBUL, SNAGA TRACKER [D]"
            , "CUTTHROATS OF THE DALELANDS"
            , "SILENT VULTURES [D]"
            , "SKARF, LORD OF BRECH"
            , "DWARVEN ASSASSIN"
            , "RAENAR THE PLUNDERER [E] [D]"
            , "WRUNELE THE FIERY [E] [D]"
            , "MUD-MAN"
            , "THE GUTTERMAW"
            , "WOLF OF THE WASTE [E]"
            , "THE DEVOURER [E]"
            , "RAVENOUS GOBLINS"
            , "THE STALKER IN THE DEEPS [E]"
            , "THE SORCERER OF FOROD"
            , "SERVANTS OF TYRANTS HILL"
            , "THE FOREST DRAGON"
            , "RAEGENHERE"
            , "VALDIS THE GREAT VAMPIRE"
            , "LIEUTENANT OF DOL GULDUR"
            , "THE GHOST OF THE FOREST"
            , "THE MESSENGER OF MORDOR"
            , "TYULQIN THE WEAVER"
            , "SARQIN, THE MOTHER-OF-ALL"
            , "TAULER THE HUNTER"
            // Core
            , "SOUTHERNER RAIDER"
            , "SOUTHERNER CHAMPION"
            , "ORC SOLDIER"
            , "FOOTPAD"
            , "RUFFIAN CHIEF"
            , "HIGHWAY  ROBBER"
            , "GREAT ORC CHIEF"
            , "GREAT ORC BODYGUARD"
            , "GOBLIN ARCHER"
            , "ORC-CHIEFTAIN"
            , "ORC GUARD"
            , "ORC SOLDIER"
            , "GREAT CAVE-TROLL"
            , "CAVE-TROLL SLINKER"
            , "STONE-TROLL ROBBER"
            , "STONE-TROLL CHIEF"
            , "BARROW-WIGHT"
            , "FELL WRAITH"
            , "MARSH DWELLERS"
            , "WILD WOLF"
            , "WOLF-CHIEFTAIN"
            , "HOUND OF SAURON"
            , "THE WIGHT-KING"
            , "BÚRZGUL"
            , "ASH"
        };
        public static readonly List<string> AdversaryEndTokenList = new List<string> {
            // Additional
            "Don't delete me"
            , "Great Spider Great Spiders display"
            , "spiders of mirkwood"
            , "Secret Shadow"
            , "Bog Soldiers Dead soldiers"
            , "Basilisks Called Sarnlug"
            , "27 kinstrife & dark tidings"
            // Core
            , "Southerner Raider"
            , "Southerner Champion"
            , "Orc Soldier"
            , "Footpad"
            , "Ruffian Chief"
            , "Highway Robber"
            , "orcs of the north"
            , "Orc-chieftain"
            , "Great Orc Chief"
            , "Great Orc Bodyguard"
            , "Goblin Archer"
            , "Orc Guard"
            , "Orc-Soldier"
            , "Great Cave-Troll"
            , "Cave-troll Slinker"
            , "stone-trolls"
            , "Stone-Troll Robber"
            , "Stone-Troll Chief"
            , "Barrow-Wight"
            , "Fell Wraith"
            , "Marsh-dwellers"
            , "werewolves"
            , "Wild Wolf"
            , "Wolf-Chieftain"
            , "Hound of Sauron"
            , "Bodo Hüsemann"
            , "the north downs"
            , "the weather hills"
        };


        public static Parser<string> ListParser(List<string> lst)
        {
            var parser = Parse.String(lst.First()).Text();
            foreach (var item in lst.Skip(1))
            {
                parser = parser.Or(Parse.String(item)).Text();
            }
            return parser;
        }
        public static Parser<string> listParserFellAbilities = ListParser(FellAbilitiesTokenList);
        public static Parser<string> listParserAdversaries = ListParser(AdversaryTokenList);
        public static Parser<string> listParserAdversariesEnd = ListParser(AdversaryEndTokenList);
        public static Parser<string> TokenAttributeLevel = Parse.IgnoreCase("ATTRIBUTE LEVEL").Token().Text();
        public static Parser<string> TokenEndurance = Parse.IgnoreCase("ENDURANCE").Token().Text();
        public static Parser<string> TokenMight = Parse.IgnoreCase("MIGHT").Token().Text();
        public static Parser<string> TokenResolve = Parse.IgnoreCase("RESOLVE").Token().Text();
        public static Parser<string> TokenHateResolve = Parse.IgnoreCase("HATE").Or(Parse.IgnoreCase("RESOLVE")).Token().Text();
        public static Parser<string> TokenParry = Parse.IgnoreCase("PARRY").Token().Text();
        public static Parser<string> TokenArmour = Parse.IgnoreCase("ARMOUR").Token().Text();
        public static Parser<string> TokenCombatProficiencies = Parse.IgnoreCase("COMBAT PROFICIENCIES:").Token().Text();
        public static Parser<string> TokenFellAbilities = Parse.IgnoreCase("FELL ABILITIES:").Token().Text();

        public static Parser<FellAbility> FellAbilityParser =
            from fellAbilityName in listParserFellAbilities
            from sep in Parse.Char('.')
            from fellAbilityDescription in Parse.AnyChar
                    .Except(listParserFellAbilities)
                    .Except(TokenAttributeLevel)
                    .Except(listParserAdversaries)
                    .Except(listParserAdversariesEnd)
                    .Many()
                    .Token()
                    .Text()
            select new FellAbility()
            {
                abilityname = fellAbilityName.Trim(),
                description = fellAbilityDescription.Trim()
            };

        public static readonly Parser<FellAbility[]> fellAbilityList =
            from abilities in FellAbilityParser.Many()
            from rest in Parse.AnyChar
                .Except(TokenAttributeLevel)
                .Except(listParserAdversaries)
                .Except(listParserAdversariesEnd)
                .Many().Token().Text().Optional()
            select abilities.ToArray();

        public static Parser<string> WordParser = Parse.Letter.Many().Token().Or(Parse.String("2-Handed")).Text();
        public static Parser<string> WordOrMinusParser = Parse.Letter.Or(Parse.Char('-')).Many().Token().Text();

        public static Parser<string> NumberOrMinus =
            from min in Parse.Char('-').Optional()
            from num in Parse.Number.Token().Optional()
            select num.IsDefined ? num.Get() : min.IsDefined ? min.Get().ToString() : "";

        public static Parser<string> PhraseParser =
            from leading in Parse.Letter.Many().Token().Text()
            from rest in Parse.Chars(' ', '-', ',', ':').Many().Then(_ => WordParser).Many()
            select leading + " " + String.Join(" ", rest);

        public static Parser<string> DistinctiveFeatureParser =
            from first in WordOrMinusParser
                // Folulf and Arnulf -> Swift (Folulf), Wary (Arnulf)
            from left in Parse.Char('(').Optional()
            from middle in WordParser.Optional()
            from right in Parse.Char(')').Optional()
            from sep in Parse.Char(',').Token()
            from second in WordOrMinusParser
            from rest in Parse.AnyChar.Except(TokenCombatProficiencies).Many().Token().Text()
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

        public static readonly Parser<WeaponProficiency[]> weapons =
             from a in WeaponParser.DelimitedBy(Parse.Chars(',', ' ', '.').Many().Token())
             from weaponProfRest in Parse.AnyChar
                 .Except(TokenFellAbilities)
                 .Except(listParserAdversaries)
                 .Except(listParserAdversariesEnd)
                 .Many().Token().Text().Optional()
             select a.ToArray();

        public static Parser<Adversary> adv_add =
            from leading in Parse.AnyChar.Except(listParserAdversaries).Many()
            from name in listParserAdversaries
            from dfeat in DistinctiveFeatureParser.Optional()
            from weaponProf in TokenCombatProficiencies.Optional().Then(_ => weapons.Optional())
            from fellAbilities in TokenFellAbilities.Optional().Then(_ => fellAbilityList.Optional())
            from attributeLevel in TokenAttributeLevel.Optional().Then(_ => Parse.Number.Token().Optional())
            from endurance in TokenEndurance.Optional().Then(_ => Parse.Number.Token().Optional())
            from might in TokenMight.Token().Optional().Then(_ => Parse.Number.Token().Optional())
            from hateResolve in TokenHateResolve.Token().Optional().Then(_ => Parse.Number.Token().Optional())
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
                distinctiveFeatures = dfeat.IsDefined ? dfeat.Get() : "",
                attributeLevel = attributeLevel.IsDefined ? attributeLevel.Get() : "",
                endurance = endurance.IsDefined ? endurance.Get() : "",
                might = might.IsDefined ? might.Get() : "",
                hateresolve = hateResolve.IsDefined ? hateResolve.Get() : "",
                parry = (parry.IsDefined ? parry.Get() : "") + (hateResolveRest.IsDefined ? hateResolveRest.Get() : ""),
                armour = armour.IsDefined ? armour.Get() : "",
                weaponProficiencies = weaponProf.IsDefined ? weaponProf.Get() : new WeaponProficiency[0],
                fellAbilities = fellAbilities.IsDefined ? fellAbilities.Get() : new FellAbility[0]
            };

        public static Parser<Adversary> adv_core =
            from leading in Parse.AnyChar.Except(listParserAdversaries).Many()
            from name in listParserAdversaries
            from dfeat in DistinctiveFeatureParser.Optional()
            from tokenAttributeLevel in TokenAttributeLevel.Token()
            from attributeLevel in Parse.Number.Token()
            from tokenEndurance in TokenEndurance.Token()
            from endurance in Parse.Number.Token()
            from tokenMight in TokenMight.Token()
            from might in Parse.Number.Token()
            from tokenHateResolve in TokenHateResolve.Token()
            from hateResolve in Parse.Number.Token()
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
                hateresolve = hateResolve,
                parry = parry,
                armour = armour,
                weaponProficiencies = weaponProf.IsDefined ? weaponProf.Get() : new WeaponProficiency[0],
                fellAbilities = fellAbilities.IsDefined ? fellAbilities.Get() : new FellAbility[0]
            };

        private static readonly Parser<Adversary[]> advs_core =
            from a in adv_core.DelimitedBy(Parse.AnyChar.Except(listParserAdversaries).Many().Text())
            select a.ToArray();

        private static readonly Parser<Adversary[]> advs_add =
            from a in adv_add.DelimitedBy(Parse.AnyChar.Except(listParserAdversaries).Many().Text())
            select a.ToArray();


        static void Main(string[] args)
        {
            PdfConverter pdf = new PdfConverter();
            //pdf.convert(@"pdf\The_One_Ring_Core_Rules.pdf", @"out/log.txt");
            pdf.convert(@"./pdf/Adversary Conversion.pdf", @"out/log.txt");
            string input = File.ReadAllText("./out/log.txt", Encoding.UTF8);
            //  string input =
            //      "same as other folk…”Southerner RaiderWhen a particularly harsh winter has passed, Men from the South may assemble war parties and look for some isolated homestead to plunder, before retreating just as quickly back into the mists where they came from.SOUTHERNER RAIDERCanny, HardenedATTRIBUTE LEVEL4ENDURANCE16MIGHT1RESOLVE4PARRY+1ARMOUR2COMBAT PROFICIENCIES: Axe 3 (5/18),  Short Spear 2 (3/14, Pierce)FELL ABILITIES: Fierce Folk. Spend 1 Resolve point to gain (1d) and make the attack roll Favoured. "
            //      + "Favoured.Southerner ChampionA Southerner Champion may be a chieftain from Dun-land, a bandit lord capable of uniting a number of frac-tious warriors into a small army, or just a particularly vicious brigand.SOUTHERNER CHAMPIONCruel, ToughATTRIBUTE LEVEL5ENDURANCE20MIGHT1RESOLVE5PARRY+2ARMOUR3COMBAT PROFICIENCIES: Spear 3 (4/14, Pierce),  Long- hafted Axe 3 (6/18, Break Shield) FELL ABILITIES: Fierce Folk. Spend 1 Resolve point to gain (1d) on an attack and to make the roll Favoured.Bodo Hüsemann (Order #31660777)";
            // string input = " they once used to love or inhabit. SPECTRES Restless, Sorrowful       Combat Proficiencies:  Fell Abilities: Ghost Form. The creature is incorporeal and partially, if not completely, invisible. It cannot normally harm nor can be harmed physically by the living. When this creatures Hate score is reduced to 0, it disappears and reappears the next night with its hate score refilled. Weapons that do not possess Enchanted qualities cannot affect this creature. Dreadful Spells. Spend 1 Hate to force a player-hero to gain 2 points of Shadow (Sorcery). If the hero fails their Shadow Test, they are Wounded as an old injury reopens. Visions of Torment. Spend 1 Hate to force a player-hero to gain 1 point of Shadow (Dread). If the hero fails their Shadow Test, they lose a number of Endurance points equal to twice their current Shadow score. Bog Soldiers Dead soldiers of a war long past, lying in rest beneath the Ettenmoors..BOG SOLDIERS [M] Relentless, Foul";
            string sanitized = input.Replace('\u00A0', ' ');
            sanitized = sanitized.Replace("- ", "-");
            sanitized = sanitized.Replace("HUORNS Ancient, Vengeful", "HUORNS. Ancient, Vengeful");
            sanitized = sanitized.Replace("VALTER’S OUTLAWS", "VALTER’S Outlaws");

            //var parsed = advs_core.Parse(sanitized);
            var parsed = advs_add.Parse(sanitized);
            string fileName = "./out/result.json";
            JsonSerializerOptions jso = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string jsonString = JsonSerializer.Serialize(parsed, jso);
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine(jsonString);
        }
    }
}