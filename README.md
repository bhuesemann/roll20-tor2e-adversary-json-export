# Roll20 Adversary Exporter for The One Ring 2e

This little project aims to create JSON and YAML files for adversaries listed in PDF files. After conversion this data can be used for personal rpg adventures, e.g. there is an import mechanism for Roll20. YAML output was added so the same adversary data can also be used as notes in [Obsidian](https://obsidian.md/).

> This is a fork of [bhuesemann/roll20-tor2e-adversary-json-export](https://github.com/bhuesemann/roll20-tor2e-adversary-json-export), maintained by [telimektar3](https://github.com/telimektar3/roll20-tor2e-adversary-json-export). All functionality through release 0.1.8 is the original work of bhuesemann; the YAML output and all newly-supported PDF sources starting with release 0.2.0 (see [CHANGELOG.md](CHANGELOG.md)) were added in this fork.

Currently the following PDF files are supported:

- Official TOR2e Core Rule Book
- CircleOfNoms adversary compendium based on several Tor1e sources.
- Official TOR2e Tales from the Lone-lands
- Official TOR2e Strider Mode (roll tables only)
- Official TOR2e Hands of the White Wizard
- Official TOR2e Moria - Through the Doors of Durin
- Official TOR2e Realms of the Three Rings
- Official TOR2e Ruins of the Lost Realm
- Official TOR2e Starter Set - The Shire
- Official TOR2e Starter Set - The Adventures
- Official TOR2e Starter Set 2 - Adventure booklet
- The Old Dwarf-mines (excerpt)

Note: Rivendell (Loremaster's Screen & Rivendell Compendium) and Peoples of Wilderland are lore/culture supplements with no adversary stat blocks, so there is nothing for this tool to extract from them.

Please note that due to copyright restrictions we will not include any copyrighted materials (e.g. the PDFs itself). All material is parsed and extracted from the pdf documents and that are not part of this source code.

## HowTo

- Place PDFs
Please put the PDFs to be parsed in the subdirectory: pdf

- Run
Execute open a cmd/powershell/terminal and start: roll20_adv_json_exporter.exe

- Where to find the JSONs?
The generated JSON and YAML files can be found in the subdirectory: out. The YAML files can be dropped straight into an Obsidian vault as notes.

- To take a look at the JSON files I prefer using XIMPLE (http://www.ximple.cz/). It is free for uncommercial use.

## Create Adversary in Roll20
- Create new Adversary
- Copy JSON of adversary into the notes section, e.g. 

  {
    "name": "SAVAGE WOLFDOGS",
    "distinctiveFeatures": "Wild, Fierce",
    "attributeLevel": "4",
    "endurance": "16",
    "might": "1",
    "resolve": "4",
    "parry": "+1",
    "armour": "2",
    "weaponProficiencies": [
      {
        "weaponname": "Bite",
        "rating": "2",
        "damage": "4",
        "injury": "14",
        "special": "Pierce"
      }
    ],
    "fellAbilities": [
      {
        "abilityname": "Great Leap.",
        "description": "Spend 1 Resolve to attack any player-hero, in any combat stance including Rearward."
      }
    ]
  }
- Leave the focus of the notes area (click somewhere else in the sheet)
- Magic happens


## Development

The whole parsing is basend on the excellent framework "Sprache". You can get a good introduction here:
<https://justinpealing.me.uk/post/2020-03-11-sprache1-chars/>

- Building manually
Clone the git repo and bild from the command line:

``` cmd
dotnet restore
dotnet run
```

- Publich github pipeline
-- edit release nr. in RELEASE
``` cmd
git tag <version>
git push --tags --force
```

- Publish manually
``` cmd
dotnet publish -c Release --property:./dist -p:PublishProfile=Release
```

- Development
Install Visual Source Code and install everything needed for a c# project. 