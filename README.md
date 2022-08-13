# Roll20 Adversary Exporter for The One Ring 2e

This little project aims to create JSON files for adversaries listed in PDF files. After JSON conversion this data can be used for personal rpg adventures, e.g. there is an import mechanism for Roll20.

Currently the following PDF files are supported:

- Official TOR2e Core Rule Book
- CircleOfNoms adversary compendium based on several Tor1e sources.

Please note that due to copyright restrictions we will not include any copyrighted materials (e.g. the PDFs itself). All material is parsed and extracted from the pdf documents and that are not part of this source code.

## HowTo

- Place PDFs
Please put the PDFs to be parsed in the subdirectory: pdf

- Build and run
Building from the command line is easy:

``` cmd
dotnet restore
dotnet run
```

- Where to find the JSONs?
The generated JSON files can be found in the subdirectory: out

## Development

The whole parsing is basend on the excellent framework "Sprache". You can get a good introduction here:
<https://justinpealing.me.uk/post/2020-03-11-sprache1-chars/>
