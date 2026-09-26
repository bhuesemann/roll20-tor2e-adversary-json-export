# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

Releases up to and including 0.1.8 are the original work of [bhuesemann](https://github.com/bhuesemann/roll20-tor2e-adversary-json-export). All releases from 0.2.0 onward are maintained in the [telimektar3 fork](https://github.com/telimektar3/roll20-tor2e-adversary-json-export).

## [Unreleased]

## [0.2.0] - 2026-09-26
### Added
- YAML output (in addition to JSON) for every parsed source
- Support for Hands of the White Wizard
- Support for Moria - Through the Doors of Durin
- Support for Realms of the Three Rings
- Support for Ruins of the Lost Realm
- Support for Starter Set - The Shire
- Support for Starter Set - The Adventures
- Support for Starter Set 2 - Adventure booklet
- Support for The Old Dwarf-mines (excerpt, reuses the Ruins of the Lost Realm parser)
- `EASTERLING WARRIORS` token variant to the CircleofNoms Adversary Conversion list, for compatibility with newer PDF revisions
### Fixed
- Grammar now handles em dashes used as "no value"/"no weapons" placeholders, footnote asterisks on weapon ratings and special text, apostrophes in weapon names, parenthetical weapon qualifiers, and numeric rating modifiers (e.g. `2 (3)`)
- Pipeline now continues processing remaining PDFs after a per-file parse error instead of aborting the whole batch

## [0.1.7] - 2024-02-04
### Changed
- fixed parsing of the core rules for updated pdf version 2401
- updated parsing for CircleOfNoms Adversary PDF
- started parsing of Strider Mode
- added support for Tales of the Lone Lands
## [0.1.6] - 2022-08-21
### Changed
- fixed parsing of the core rules for updated pdf version 2203
## [0.1.5] - 2022-08-15
### Fixed
- parsing order tags
## [0.1.3] - 2022-08-14
### Changed
- README.md
## [0.1.2] - 2022-08-14
### Fixed
- Single file build
## [0.1.1] - 2022-08-14
### Added
- Startup and progress messages
## [0.1.0] - 2022-08-14
This is the initial release of the project. Please note that this is an beta release to get first feedback. 
### Added
- Everything