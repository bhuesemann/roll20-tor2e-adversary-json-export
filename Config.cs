using System.Collections.Generic;

namespace roll20_adv_import_c
{
    public class Config
    {
        public static void InitAdd()
        {
            Config.AdversaryTokenList = AdversaryTokenListAdd;
        }
        public static void InitCore()
        {
            Config.AdversaryTokenList = AdversaryTokenListCore;
        }
        public static readonly List<string> AdversaryTokenListCore = new List<string> {
            // Core
            "SOUTHERNER RAIDER"
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
            , "ELWEN FELL WRAITH"
            , "MARSH DWELLERS"
            , "WILD WOLF"
            , "WOLF-CHIEFTAIN"
            , "HOUND OF SAURON"
            , "THE WIGHT-KING"
            , "BÚRZGUL"
            , "ASH"
        };
        public static readonly List<string> AdversaryTokenListAdd = new List<string> {
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
            , "the north downs"
            , "the weather hills"
            , "TUTORIAL — BUILDING UP THE CLIMAX"
        };

        public static readonly List<string> AdversaryTokenListStart = new List<string>
        {
            "...Southerner Raider"
            , "Southerner Champion"
            , "Orc Soldier"
            , "Footpad"
            // , "Ruffian Chief"
            // , "Highway Robber"
            // , "orcs of the north"
            // , "Orc-chieftain"
            // , "Great Orc Chief"
            // , "Great Orc Bodyguard"
            // , "Goblin Archer"
            // , "Orc Guard"
            // , "Orc-Soldier"
            // , "Great Cave-Troll"
            // , "Cave-troll Slinker"
            // , "stone-trolls"
            // , "Stone-Troll Robber"
            // , "Stone-Troll Chief"
            // , "Barrow-Wight"
            // , "Fell Wraith"
            // , "Marsh-dwellers"
            // , "werewolves"
            // , "Wild Wolf"
            // , "Wolf-Chieftain"
            // , "Hound of Sauron"
        };
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
            "Howl of Triumph.",
            "Many Poisons.",
            "Mirkwood Dweller.",
            "Paralyzing-Poison.",
            "Poison Blast.",
            "Poisoned Blade.",
            "Poison.",
            "Prohibition.",
            "Raven Spirits.",
            "Reckless Hate.",
            "Savage Assault.",
            "Seize and Drown.",
            "Seize Victim.",
            "Shade Caller.",
            "Shadow of Fear.",
            "Sleep.",
            "Snake-Like Speed.",
            "Snake-like Speed.",
            "Strange Venoms.",
            "Strike Fear.",
            "Sweeping Stroke.",
            "Terror of Desire.",
            "Thick Hide.",
            "Thing of Terror.",
            "Thrall (Spiders).",
            "Two-Headed.",
            "Two-Headed OR Four-Armed.",
            "Unliving.",
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
        public static readonly List<string> RolltablesTokenList = new List<string>
        {
            "EXPERIENCE MILESTONES",
            "ILL-­F ORTUNETABLE",
            "FORTUNE TABLE",
            "TELLING TABLE",
            "FEAT DIE:1",
            "FEAT DIE:2",
            "FEAT DIE:3",
            "FEAT DIE:4",
            "FEAT DIE:5",
            "FEAT DIE:6",
            "FEAT DIE:7",
            "FEAT DIE:8",
            "FEAT DIE:9",
            "FEAT DIE:10",
            "FEAT DIE:11",
            "FEAT DIE:12",
            "SKILL SPECIAL SUCCESS TABLE",
            "RISK LEVELS",
            "SOLO JOURNEY EVENTS",
            "EVENT DETAIL: TERRIBLE MISFORTUNE",
            "EVENT DETAIL: DESPAIR",
            "EVENT DETAIL: ILL CHOICES",
            "EVENT DETAIL: SHORT CUT",
            "EVENT DETAIL: MISHAP",
            "EVENT DETAIL: CHANCE MEETING",
            "EVENT DETAIL: JOYFUL SIGHT",
            "REVELATION EPISODE TABLE",
            "HUNT THRESHOLD REGION TABLE",
            "HUNT MODIFIERS TABLE",
            "PATRON QUESTS: BALIN, SON OF FUNDIN",
            "PATRON QUESTS: BILBO BAGGINS",
            "PATRON QUESTS: GANDALF THE GREY",
            "PATRON QUESTS: CIRDAN THE SHIPWRIGHT",
            "PATRON QUESTS: GILRAEN, DAUGHTER OF DIRHAEL",
            "PATRON QUESTS: TOM BOMBADIL AND LADY GOLDBERRY",
        };
        public static readonly List<string> RolltableColHeaderList = new List<string>
        {
            "SUCCESS DIEACTIONASPECTFOCUS",
            "MILESTONEREWARD",
            "FEAT DIERESULT",
            "CHANCEROLL A FEAT DIE.  THE ANSWER IS YES IF YOU ROLL...",
            "SUCCESS DIEQUEST",
            "SPEND 1 SUCCESS ICON TO…DESCRIPTION",
            "RISK LEVELTHE FAILED ROLL RESULTS IN…EXAMPLES",
            "FEAT DIEEVENTCONSEQUENCES OF THE SKILL ROLLFATIGUE POINTS GAINED",
            "SUCCESS DIEEVENTOUTCOME",
            "THE REGION TRAVERSED IS A…HUNT THRESHOLD",
            "HUNT MODIFIERDESCRIPTION",
            "FEAT DIEEPISODE",
            "SUCCESS DIEQUEST",
        };
        public static List<string> AdversaryTokenList = AdversaryTokenListCore;

    }
}