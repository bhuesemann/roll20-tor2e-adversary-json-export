using System.Collections.Generic;
using System.Linq;

namespace roll20_adv_import_c
{
    // Obsidian-friendly YAML shape for an Adversary, distinct from the JSON output.
    public class ProficiencyYaml
    {
        public string name { get; set; }
        public string rating { get; set; }
        public string damage { get; set; }
        public string injury { get; set; }
        public string special { get; set; }
    }

    public class AdversaryYaml
    {
        public string name { get; set; }
        public List<string> features { get; set; }
        public string level { get; set; }
        public string endurance { get; set; }
        public string might { get; set; }
        public string hate { get; set; }
        public string resolve { get; set; }
        public string parry { get; set; }
        public string armour { get; set; }
        public List<ProficiencyYaml> proficiencies { get; set; }
        public List<string> abilities { get; set; }

        private static readonly HashSet<string> LowercaseWords = new HashSet<string> {
            "a", "an", "and", "as", "at", "but", "by", "for", "from",
            "in", "into", "nor", "of", "on", "or", "over", "the", "to", "with"
        };

        // ALL-CAPS adversary names (e.g. "BEAST OF ANGMAR") read as "Beast of Angmar".
        // Bracketed tags like "[E]"/"[D]" and hyphenated segments are preserved.
        private static string ToTitleCase(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            string[] words = name.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                if (word.Length == 0 || (word.StartsWith("[") && word.EndsWith("]")))
                {
                    continue;
                }
                bool isSmallWord = i > 0 && LowercaseWords.Contains(word.ToLower().TrimEnd(',', '.'));
                string[] parts = word.Split('-');
                for (int j = 0; j < parts.Length; j++)
                {
                    if (parts[j].Length == 0)
                    {
                        continue;
                    }
                    parts[j] = (isSmallWord && j == 0)
                        ? parts[j].ToLower()
                        : char.ToUpper(parts[j][0]) + (parts[j].Length > 1 ? parts[j].Substring(1).ToLower() : "");
                }
                words[i] = string.Join("-", parts);
            }
            return string.Join(" ", words);
        }

        public static AdversaryYaml FromAdversary(Adversary adv)
        {
            return new AdversaryYaml()
            {
                name = ToTitleCase(adv.name),
                features = string.IsNullOrEmpty(adv.distinctiveFeatures)
                    ? null
                    : adv.distinctiveFeatures.Split(',').Select(f => f.Trim()).Where(f => f != "").ToList(),
                level = adv.attributeLevel,
                endurance = adv.endurance,
                might = adv.might,
                hate = adv.hate,
                resolve = adv.resolve,
                parry = adv.parry,
                armour = adv.armour,
                proficiencies = adv.weaponProficiencies == null || adv.weaponProficiencies.Length == 0
                    ? null
                    : adv.weaponProficiencies.Select(w => new ProficiencyYaml()
                    {
                        name = w.weaponname,
                        rating = w.rating,
                        damage = w.damage,
                        injury = w.injury,
                        special = string.IsNullOrEmpty(w.special) ? "" : $"[[Special Damage Options#{w.special}|{w.special}]]"
                    }).ToList(),
                abilities = adv.fellAbilities == null || adv.fellAbilities.Length == 0
                    ? null
                    : adv.fellAbilities.Select(a =>
                    {
                        string cleanName = a.abilityname.TrimEnd('.').Trim();
                        return $"[[Fell Abilities#{cleanName}|{cleanName}]]";
                    }).ToList(),
            };
        }
    }
}
