using System;
using System.IO;
using System.Text;
using Sprache;
using System.Text.Json;

namespace roll20_adv_import_c
{
    class Program
    {
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
            sanitized = sanitized.Replace('\uF0A8', ' ');
            sanitized = sanitized.Replace("- ", "-");
            sanitized = sanitized.Replace("HUORNS Ancient, Vengeful", "HUORNS. Ancient, Vengeful");
            sanitized = sanitized.Replace("VALTER’S OUTLAWS", "VALTER’S Outlaws");
            sanitized = sanitized.Replace("to hit Raenar’s Weak Spot.", "to hit Raenar’s weak spot.");

            var parsed = TorAdvParserAdd.advs_add.Parse(sanitized);
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