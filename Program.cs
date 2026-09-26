using System;
using System.IO;
using Sprache;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;

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
                    string yamlPath = @$"./out/{basename}.yaml";

                    Console.WriteLine("Processing file: " + basename);

                    try
                    {
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
                        else if (basename.Contains("Tales"))
                        {
                            TorAdvParserTales.Init();
                            advs = TorAdvParserTales.advs.Parse(sanitized);
                        }
                        else if (basename.Contains("Core_Rules") || basename.Contains("Core Rules"))
                        {
                            TorAdvParserCore.Init();
                            advs = TorAdvParserCore.advs.Parse(sanitized);
                        }
                        else if (basename.Contains("Hands"))
                        {
                            TorAdvParserHands.Init();
                            advs = TorAdvParserHands.advs.Parse(sanitized);
                        }
                        else if (basename.Contains("Moria"))
                        {
                            TorAdvParserMoria.Init();
                            advs = TorAdvParserMoria.advs.Parse(sanitized);
                        }
                        else if (basename.Contains("Realms"))
                        {
                            TorAdvParserRealms.Init();
                            advs = TorAdvParserRealms.advs.Parse(sanitized);
                        }
                        else if (basename.Contains("Ruins") || basename.Contains("Dwarf-mines"))
                        {
                            TorAdvParserRuins.Init();
                            advs = TorAdvParserRuins.advs.Parse(sanitized);
                        }
                        else if (basename.Contains("Shire"))
                        {
                            TorAdvParserShire.Init();
                            advs = TorAdvParserShire.advs.Parse(sanitized);
                        }
                        else if (basename.Contains("SS2_Adventure"))
                        {
                            TorAdvParserSS2Adventure.Init();
                            advs = TorAdvParserSS2Adventure.advs.Parse(sanitized);
                        }
                        else if (basename.Contains("Adventures"))
                        {
                            TorAdvParserStarterAdventures.Init();
                            advs = TorAdvParserStarterAdventures.advs.Parse(sanitized);
                        }
                        else
                        {
                            Console.WriteLine("No parser known for this source, skipping bestiary parsing.");
                            continue;
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

                        ISerializer yamlSerializer = new SerializerBuilder()
                            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
                            .Build();
                        string yamlString = yamlSerializer.Serialize(parsed);
                        File.WriteAllText(yamlPath, yamlString);

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
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to process {basename}: {ex.Message}");
                    }
                }
                Console.WriteLine("Processing completed!");
            }
        }
    }
}
