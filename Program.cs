using System;
using System.IO;
using Sprache;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace roll20_adv_import_c
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This tool will convert the PDF files found in path ./pdf and create JSON files in ./out!");
            string[] filePaths = Directory.GetFiles(@"./pdf", "*.pdf", SearchOption.AllDirectories);
            if (filePaths.Length == 0)
            {
                Console.WriteLine("No files found to be processed!");
            }
            else
            {
                Console.WriteLine("Found files: " + filePaths.Length);

                foreach (string pdfPath in filePaths)
                {
                    // pdf conversion 
                    string basename = Path.GetFileName(pdfPath).Replace(Path.GetExtension(pdfPath), "");
                    string txtPath = @$"./out/{basename}.txt";
                    string jsonPath = @$"./out/{basename}.json";

                    Console.WriteLine("Processing file: " + basename);

                    PdfConverter.convert(pdfPath, txtPath);
                    string sanitized = TorAdvSanitizer.Sanitize(txtPath);

                    // parsing
                    Rolltable[] tabs = null;
                    Adversary[] advs = null;
                    if (basename.Contains("Adversary"))
                    {
                        TorAdvParserAdd.Init();
                        advs = TorAdvParserAdd.advs.Parse(sanitized);
                    }
                    else if (basename.Contains("Strider"))
                    {
                        TorStriderParser.Init();
                        tabs = TorStriderParser.tabs.Parse(sanitized);
                    }
                    else
                    {
                        TorAdvParserCore.Init();
                        advs = TorAdvParserCore.advs.Parse(sanitized);
                    }

                    // serialization (remove null properties)
                    JsonSerializerOptions jso = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    };
                    Object[] parsed = advs == null ? tabs : advs;
                    string jsonString = JsonSerializer.Serialize(parsed, jso);
                    File.WriteAllText(jsonPath, jsonString);

                    // analyse result and provide feedback for advs
                    if (advs != null)
                    {
                        HashSet<string> advs_hashed = advs.Select(adv => adv.name).ToHashSet();
                        int found = 0;
                        List<string> missing = new List<string> { };
                        int total = Config.AdversaryTokenList.Count;
                        foreach (String adv in Config.AdversaryTokenList)
                        {
                            if (advs_hashed.Contains(adv))
                            {
                                found++;
                            }
                            else
                            {
                                missing.Add(adv);
                            }
                        };
                        Console.WriteLine($"Parsed {found} / {total} adversaries.");
                        if (missing.Count > 0)
                        {
                            Console.WriteLine($"Missing: {string.Join(", ", missing)}");
                        }
                    }
                }
                Console.WriteLine("Processing completed!");
            }
        }
    }
}