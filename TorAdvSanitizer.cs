using System.IO;
using System.Text;


namespace roll20_adv_import_c {
    public class TorAdvSanitizer{
        public static string Sanitize(string filepath) {
            string input = File.ReadAllText(filepath, Encoding.UTF8);
            string sanitized = input.Replace('\u00A0', ' ');
            sanitized = sanitized.Replace('\uF0A8', ' ');
            sanitized = sanitized.Replace("- ", "-");
            sanitized = sanitized.Replace("HUORNS Ancient, Vengeful", "HUORNS. Ancient, Vengeful");
            sanitized = sanitized.Replace("VALTER’S OUTLAWS", "VALTER’S Outlaws");
            sanitized = sanitized.Replace("to hit Raenar’s Weak Spot.", "to hit Raenar’s weak spot.");
            return sanitized;
        }
    }
}
