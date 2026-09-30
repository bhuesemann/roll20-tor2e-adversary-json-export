# Roll20 Adversary Exporter for The One Ring 2e

This little project aims to create JSON and YAML files for adversaries listed in PDF files. After conversion this data can be used for personal rpg adventures, e.g. there is an import mechanism for Roll20. YAML output was added so the same adversary data can also be used with the [Obsidian "The One Ring 2E Statblocks" plugin](https://github.com/modality/obsidian-the-one-ring-2e-statblocks) (`tor2e` code blocks).

> This is a fork of [bhuesemann/roll20-tor2e-adversary-json-export](https://github.com/bhuesemann/roll20-tor2e-adversary-json-export), maintained by [telimektar3](https://github.com/telimektar3/roll20-tor2e-adversary-json-export). All functionality through release 0.1.8 is the original work of bhuesemann; the YAML output and all newly-supported PDF sources starting with release 0.2.0 (see [CHANGELOG.md](CHANGELOG.md)) were added in this fork.

## Supported Files

Currently the following PDF files are fully supported:

- Official TOR2e Core Rule Book
- CircleOfNoms adversary compendium based on several Tor1e sources.
- Official TOR2e Tales from the Lone-lands
- Official TOR2e Strider Mode (roll tables only)
- Official TOR2e Realms of the Three Rings

These PDF files are partially supported:

- Official TOR2e Hands of the White Wizard
- Official TOR2e Moria - Through the Doors of Durin
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
The generated JSON files are in out/json and the generated YAML files are in out/yaml. The YAML files match the schema expected by the Obsidian "The One Ring 2E Statblocks" plugin (name, description, features, level, endurance, might, hate/resolve, parry, armour, proficiencies, abilities), with special damage types and Fell Abilities already formatted as Obsidian wiki-links (e.g. `[[Fell Abilities#Fierce|Fierce]]`).

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

- Publish a release via the github pipeline
-- add release notes under `## [Unreleased]` in CHANGELOG.md and commit them
-- run the release script from `master` (requires PowerShell 7):
``` powershell
./release.ps1 <version>          # e.g. ./release.ps1 0.2.2
./release.ps1 <version> -WhatIf  # dry run
./release.ps1 <version> -NoPush  # commit and tag locally only
```
The script moves the unreleased notes into a dated section for the version, updates RELEASE, builds, commits, tags and pushes. Pushing the tag starts the release workflow.

- Publish manually
``` cmd
dotnet publish -c Release --property:./dist -p:PublishProfile=Release
```

- Development
Install Visual Source Code and install everything needed for a c# project. 