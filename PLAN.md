# Fell Abilities Audit Plan

## Background

`Config.FellAbilitiesTokenList` is a single shared list used by every adversary
parser (via `TorParser.listParserFellAbilities` / `fellAbilityList`). The
`FellAbilityParser` in [TorParser.cs](TorParser.cs) matches ability names strictly
against this list. If an adversary's first fell ability isn't in the list, the
`Many()` match fails immediately and **all** of that adversary's abilities are
silently dropped (swallowed by the trailing catch-all text parser) — not just
the unrecognized one.

## Completed this session

- **Realms of the Three Rings** ([TorAdvParserRealms.cs](TorAdvParserRealms.cs)):
  audited all 13 adversaries against [DesiredOutputDoNotUpload/Three Realms](DesiredOutputDoNotUpload/Three%20Realms),
  added 20 missing ability tokens to `Config.FellAbilitiesTokenList`, and fixed
  the `OSKLHÛGI` → `OSKLHÛG` adversary token (glued-heading bug that ate the "I"
  from "Idle").

## Not yet audited

The same class of bug (missing token → whole ability list dropped) has **not**
been checked for these modules. No reference/expected-output file exists for
them yet, so each needs either a hand-verified reference doc or a manual read
of the generated YAML against the sourcebook:

- [ ] **Core Rules** — `TorAdvParserCore.cs` / `out/yaml/TOR_Core_Rules.yaml`
- [ ] **Tales from the Lone-lands** — `TorAdvParserTales.cs` / `out/yaml/TOR Tales from the Lone-lands.yaml`
- [ ] **Hands of the White Wizard** — `TorAdvParserHands.cs` / `out/yaml/TOR_Hands_of_the_White_Wizard_260128.yaml`
- [ ] **Moria - Through the Doors of Durin** — `TorAdvParserMoria.cs` / `out/yaml/TOR_Moria.yaml`
- [ ] **Ruins of the Lost Realm** — `TorAdvParserRuins.cs` / `out/yaml/TOR_Ruins_of_the_Lost_Realm.yaml`
- [ ] **Starter Set - The Shire** — `TorAdvParserShire.cs` / `out/yaml/TOR_Starter_Set_The_Shire.yaml`
- [ ] **Starter Set 2 - Adventure booklet** — `TorAdvParserSS2Adventure.cs` / `out/yaml/TOR_SS2_Adventure_booklet_2506.yaml`
- [ ] **Starter Set (original) - The Adventures** — `TorAdvParserStarterAdventures.cs` / `out/yaml/TOR_Starter_Set_The_Adventures.yaml`
- [ ] **Additional Adversaries** — `TorAdvParserAdd.cs` / `out/yaml/Adversary Conversion.yaml` (also currently only parses 151/160 adversary names — separate `AdversaryTokenListAdd` gap)
- [ ] **The Old Dwarf-mines** — `TorAdvParserRuins.cs` (shares Ruins parser) / `out/yaml/The One Ring - The Old Dwarf-mines.yaml` (only parses 2/27 — likely needs its own `AdversaryTokenList` entries, separate from the Fell Abilities issue)

## Suggested approach per module

1. Get/confirm a trusted reference YAML (hand-transcribed or otherwise) for the module.
2. Regenerate output via `dotnet run --project roll20_adv_json_exporter.csproj -r osx-arm64 --self-contained`.
3. Diff reference vs. generated output, ignoring `description` lines.
4. For any adversary missing its entire `abilities:` block, find its first
   ability name in the source PDF text and check whether it's present (and
   correctly spelled/punctuated) in `Config.FellAbilitiesTokenList`.
5. Add missing tokens; re-run and re-diff until clean.
