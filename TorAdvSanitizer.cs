using System.IO;
using System.Text;


namespace roll20_adv_import_c
{
    public class TorAdvSanitizer
    {
        public static string Sanitize(string filepath)
        {
            string input = File.ReadAllText(filepath, Encoding.UTF8);
            string sanitized = input.Replace('\u00A0', ' ');
            sanitized = sanitized.Replace('\uF0A8', ' ');
            sanitized = sanitized.Replace('\u00AD', ' ');// added for core's 2024 version
            sanitized = sanitized.Replace("orc- chieftain", "orc-chieftain");// added for core's 2024 version
            sanitized = sanitized.Replace("C- C", "C-C");// added for core's 2024 version
            sanitized = sanitized.Replace("E- T", "E-T");// added for core's 2024 version
            sanitized = sanitized.Replace("W- W", "W-W");// added for core's 2024 version
            sanitized = sanitized.Replace("F- C", "F-C");// added for core's 2024 version
            sanitized = sanitized.Replace("T- K", "T-K");// added for core's 2024 version
            sanitized = sanitized.Replace("Bitten — works like Seize, but the victim also loses 2 Endurance", "Bitten works like Seize, but the victim also loses two Endurance");
            sanitized = sanitized.Replace("HUNTING THE FASTITOCALON", "HUNTING THE HUNTING THE FASTITOCALN");
            sanitized = sanitized.Replace("OSMER THE “WIZARD”", "OSMER THE WIZARD");
            sanitized = sanitized.Replace("fight.12", "fight.");
            sanitized = sanitized.Replace("WORM- WIGHTFierce, Vengeful", "WORM-WIGHTFierce, Vengeful");
            sanitized = sanitized.Replace("ends. CORRUPTED DWARF", "ends. CORRUPTED DWARFS");
            sanitized = sanitized.Replace("sight. EASTERLING WARRIOR", "sight. EASTERLING-WARRIOR");
            sanitized = sanitized.Replace("Iron-Folk. DUNLENDING WARRIORS", "Iron-Folk. DUNLENDING-WARRIORS");
            sanitized = sanitized.Replace("foes. THE GHOST OF THE FOREST", "foes. NAZGUL");
            sanitized = sanitized.Replace("pay. THUGS", "pay. THUGS-TRIO");
            sanitized = sanitized.Replace("Wain. THUGS", "Wain. BRUTAL THUGS");            
            sanitized = sanitized.Replace("HUORNS Ancient, Vengeful", "HUORNS. Ancient, Vengeful");
            sanitized = sanitized.Replace("VALTER’S OUTLAWS", "VALTER’S Outlaws");
            sanitized = sanitized.Replace("ElwenFELL WRAITH", "ELWEN FELL WRAITH");
            sanitized = sanitized.Replace("to hit Raenar’s Weak Spot.", "to hit Raenar’s weak spot.");
            sanitized = sanitized.Replace("…”", "");
            // StriderMode
            sanitized = sanitized.Replace("FEAT DIE: SUCCESS DIEACTIONASPECTFOCUS1Abandon", "FEAT DIE11: SUCCESS DIEACTIONASPECTFOCUS1Abandon");
            sanitized = sanitized.Replace("FEAT DIE: SUCCESS DIEACTIONASPECTFOCUS1Believe", "FEAT DIE12: SUCCESS DIEACTIONASPECTFOCUS1Believe");
            return sanitized;
        }
    }
}