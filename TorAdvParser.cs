using System;
using Sprache;
using System.Collections.Generic;
using System.Linq;

namespace roll20_adv_import_c
{
    public class TorAdvParser{
        public static readonly List<string> FellAbilitiesTokenList = new List<string>{
            "Beast-Tamer.",
            "Berserk Rage.",
            "Bewilder.",
            "Black Breath.",
            "Black Dread.",
            "Countless Children.",
            "Cowardly.",
            "Craven.",
            "Crazed.",
            "Cruel Stroke.",
            "Dark Glamour.",
            "Darker than the Darkness.",
            "Deadly Elusiveness.",
            "Deadly Misfortune.",
            "Deadly Voice.",
            "Deadly Wound.",
            "Deathless.",
            "Defend Ally.",
            "Denizen of the Dark.",
            "Disgorge.",
            "Dreadful Spells.",
            "Drowning in Sorrow.",
            "Dwimmerlaik.",
            "Enthrall.",
            "Fear of Fire.",
            "Feast on Suffering.",
            "Fell Speed.",
            "Fierce Folk.",
            "Fire Breath.",
            "Formidable.",
            "Foul Reek.",
            "Four-Armed.",
            "Ghost Form.",
            "Gorlanc’s Poison.",
            "Great Leap.",
            "Hate Sunlight.",
            "Hatred (Beornings & Elves).",
            "Hatred (Beornings).",
            "Hatred (Dunedain).",
            "Hatred (Dunedain) and (Elves).",
            "Hatred (Dwarves).",
            "Hatred (Elves).",
            "Hatred (Everyone).",
            "Hatred (First Foe to Strike Him).",
            "Hatred (Hobbits).",
            "Hatred (Men of Rohan).",
            "Hatred (Orcs).",
            "Hatred (Riders of Rohan).",
            "Heartless.",
            "Hideous Toughness.",
            "Horrible Strength.",
            "Horror of the Wood.",
            "Many Poisons.",
            "Mirkwood Dweller.",
            "Paralyzing-Poison.",
            "Poison Blast.",
            "Poisoned Blade.",
            "Prohibition.",
            "Raven Spirits.",
            "Reckless Hate.",
            "Savage Assault.",
            "Seize and Drown.",
            "Shade Caller.",
            "Shadow of Fear.",
            "Sleep.",
            "Snake-Like Speed.",
            "Strange Venoms.",
            "Strike Fear.",
            "Terror of Desire.",
            "Thick Hide.",
            "Thing of Terror.",
            "Thrall (Spiders).",
            "Two-Headed.",
            "Two-Headed OR Four-Armed.",
            "Venomous Breath.",
            "Visions of Torment.",
            "Weak Spot.",
            "Weakened.",
            "Web.",
            "Webs of Illusion.",
            "Wicked Cunning.",
            "Words of Power and Terror.",
            "Wraith-Like.",
            "Wrapped in Shadow.",
            "Yell of Triumph.",
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
                 .Except(TokenAttributeLevel)
                 .Except(listParserAdversaries)
                 .Except(listParserAdversariesEnd)
                 .Many().Token().Text().Optional()
             select a.ToArray();

    }
}
