# Roll20 Adversary Exporter for The One Ring 2e

This little project aims to create JSON files for adversaries listed in PDF files. After JSON conversion this data can be used for personal rpg adventures, e.g. there is an import mechanism for Roll20.

Currently the following PDF files are supported:

- Official TOR2e Core Rule Book
- CircleOfNoms adversary compendium based on several Tor1e sources.

Please note that due to copyright restrictions we will not include any copyrighted materials (e.g. the PDFs itself). All material is parsed and extracted from the pdf documents and that are not part of this source code.

## HowTo

- Place PDFs
Please put the PDFs to be parsed in the subdirectory: pdf

- Run
Execute open a cmd/powershell/terminal and start: roll20_adv_json_exporter.exe

- Where to find the JSONs?
The generated JSON files can be found in the subdirectory: out

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

- Publish manually
``` cmd
dotnet publish -c Release --property:./dist -p:PublishProfile=Release
```

- Development
Install Visual Source Code and install everything needed for a c# project. 