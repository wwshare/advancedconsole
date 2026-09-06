# Changelog

## [1.1.9] - Fixes
- Add hotkey configuration 
- Add more configurations
- Fix Revive players with correct teleport
- `Teleport.TeleportToPoint` - Teleport to looking point 
- `Teleport.TeleportPlayerToPoint [player]` - Teleport other players to looking point

## [1.1.8] - More fixes and new features
- Removed commands: `SpawnCommands.ObjectSpawnMushroom [size]` `SpawnCommands.ObjectSpawnRope [length]` `SpawnCommands.ObjectSpawnLuggage [count]` `SpawnCommands.ObjectSpawnTree [height]` `SpawnCommands.ObjectSpawnRock [size]` `SpawnCommands.ObjectSpawnJellyfish [size]` `SpawnCommands.ClearSpawnedObjects`
- Remove checking for spoofing steam ID. 
+ Added new option in Server Info tab (Edit player slots and etc) 🔒MC 
+ New commands:
- `FunCommands.DropCoconutOnPlayer PlayerName` - Drop coconut on player head ⚡RPC
- `ServerAdmin.ListPhotonObjects` - List Photon Objects on map
- `ServerAdmin.ListSpawnablePrefabs` - List Spawnable Prefabs on map
- `ServerAdmin.DebugSpawn` - Debug Spawn item on map 🔒MC ⚡RPC
- `ServerAdmin.ListAllItems` - List all items
- `SpawnCommands.SpawnShitPiton` - Spawn Shit Piton 🔒MC ⚡RPC
- `SpawnCommands.SpawnBeeSwarm` - Spawn Bee Swarm
- `SpawnCommands.SpawnLuggageString [size]` - Spawn Luggage by size. Sizes: small, big, epic, ancient
- `SpawnCommands.SpawnAncientLuggage` - Spawn Ancient Luggage
- `SpawnCommands.SpawnEpicLuggage` - Spawn Epic Luggage
- `SpawnCommands.SpawnBigLuggage` - Spawn Big Luggage
- `SpawnCommands.SpawnSmallLuggage` - Spawn Small Luggage
- `SpawnCommands.SpawnRopeAnchorWithRope [segments]` - Spawn rope on loking point
- `SpawnCommands.SpawnChain` - Spawn chain on lookgin point

## [1.1.7] - Enhanced Console Log System
- Smart grouping of duplicate log messages with Bootstrap-style badges
- Time-based grouping to reduce spam
- **Solves spam issues** like "Couldn't intersect with near plane raycasting to the mirrors corners?"

## [1.1.6] - Achievement Management System

### Added
- **Achievement Management System**
  - `AchievementCommands.GrantAchievement` - Grant specific achievements with autocomplete support
  - `AchievementCommands.GrantAchievementByName` - Grant achievements by name string
  - `AchievementCommands.ClearAllAchievements` - Clear all achievements and reset progress
  - `AchievementCommands.ListAchievements` - Show all available achievements with unlock status
  - `AchievementCommands.AchievementInfo` - Show detailed info about specific achievement
  - `AchievementCommands.GrantRandomAchievement` - Grant a random achievement
  - `AchievementCommands.GrantAllAchievements` - Grant all achievements (cheat mode)
  - `AchievementCommands.AchievementStats` - Show achievement statistics and progress
  - **32 Available Achievements**: All biome badges, activity badges, skill badges and special achievements
  - **CLI AutoComplete**: Full autocomplete support for achievement types
  - **Progress Tracking**: Shows unlocked/locked status and completion statistics


## [1.1.5] - Enhanced Anti-Cheat System

### Added
- **Advanced Anti-Cheat System with Enhanced Detection**
  - Professional-grade cheat detection methods with comprehensive analysis
  - Extended mod detection for Atlas, Cherry and owner status identification
  - Automatic cheat function detection (Speed Hack, Fly Hack, NoClip, God Mode, etc.)
  - Advanced soft-lock system replacing simple kick methods

- **Enhanced Steam ID Detection System**
  - New Steam lobby integration method with reflection support
  - Improved Photon to Steam name matching system
  - Name and ID spoofing detection capabilities
  - Support for multiple players with identical names

- **Extended Player Information in Server Info**
  - New status indicators: ATLAS OWNER, CHERRY OWNER, CHEATER
  - Display of detected cheat functions for each player
  - Color-coded threat level indicators (red for cheats, green for legitimate mods)
  - Detailed mod information and version tracking

- **Advanced Console Commands for Server Administration**
  - `SoftLockPlayer` - Advanced soft-lock system for player restriction
  - `BlackScreenKick` - Black screen kick method via infinite teleportation
  - `SlowPlayer` - Player slowdown via physics manipulation
  - `SpeedPlayer` - Player acceleration via physics enhancement
  - Enhanced `KickPlayerReason`, `ForceKickPlayer`, `KickAllCheaters` commands
  - Improved `ListPlayers` and `PlayerInfo` with detailed cheat information

### Changed
- **Redesigned Player Kick System**
  - Replacement of simple kick with advanced soft-lock system
  - AirportCheckInKiosk integration for cheater blocking
  - Alternative blocking methods via "black screen" techniques
  - Automatic kick reason detection and logging
  - Advanced player management and control techniques

- **Enhanced Mod Detection System**
  - Replacement of ModDetection with new PlayerCheatInfo system
  - More accurate mod type and function identification
  - Cheat pattern recognition support
  - Detection of 17+ different cheat function types

### Enhanced
- **Console Commands**: Complete server administration system overhaul
  - New kick methods using RPC and character teleportation
  - Physical player effects (slowdown/speedup via ragdoll system)
  - Multiple fallback mechanisms for command reliability
  - Comprehensive logging of all administrative actions

### Fixed
- Fixed Steam ID detection issues for certain players
- Improved stability when working with player custom properties
- Fixed mod information display errors
- Resolved console kick command reliability issues

### Technical
- Added reflection support for Steam API integration
- Steamworks integration for accurate user identification
- New classes: PlayerCheatInfo, comprehensive cheat function detection system
- Enhanced error handling and logging capabilities

---

## [1.1.4-beta] - Server Info, Player Management & Map Control

### Fixed
- **MAJOR**: Fixed segment/biome synchronization issue where segment commands (GoToBeach, GoToTropics, GoToAlpine, GoToCaldera, GoToKiln, GoToPeak) didn't work for all players
- Segment jumps to all map locations (Beach, Tropics, Alpine, Caldera, TheKiln, Peak) now properly synchronize for all players including those without the mod
- All players now move together when using any segment/biome commands instead of only the command issuer
- Map transitions and biome changes now work correctly in multiplayer sessions

### Added
- **New `SegmentSync` system**: Advanced multi-method segment synchronization
  - Method 1: Campfire-based synchronization (native game mechanic)
  - Method 2: Direct teleportation with RPC sync
  - Method 3: Room property synchronization for compatibility
  - Method 4: Custom RPC fallback for cross-player sync
- **New RPC Segment Commands**: Full network synchronization for all map segments
  - `RPCCommands.SyncGoToBeach` - Synchronized Beach segment jump for all players
  - `RPCCommands.SyncGoToTropics` - Synchronized Tropics segment jump for all players
  - `RPCCommands.SyncGoToAlpine` - Synchronized Alpine segment jump for all players
  - `RPCCommands.SyncGoToCaldera` - Synchronized Caldera segment jump for all players
  - `RPCCommands.SyncGoToKiln` - Synchronized TheKiln segment jump for all players
  - `RPCCommands.SyncGoToPeak` - Synchronized Peak segment jump for all players
  - `RPCCommands.SyncJumpToSegment` - Synchronized jump to any segment for all players
- **Enhanced `MapCommands.JumpToSegment`**: Now automatically uses synchronized jumps for Master Client
- **Master Client Utilities**: New centralized system for all Master Client checks
  - All critical commands now properly restricted to server host
  - Centralized Master Client validation with bypass option
- **Time of Day Synchronization**: New synchronized time commands for all players
  - `RPCCommands.SyncTimeOfDay` - Synchronize custom time for all players
  - `RPCCommands.SetDayTime` - Set noon for all players
  - `RPCCommands.SetNightTime` - Set midnight for all players  
  - `RPCCommands.SetDawnTime` - Set dawn for all players
  - `RPCCommands.SetDuskTime` - Set dusk for all players
- **Enhanced RPC Commands**: Improved multiplayer synchronization
  - `EnvironmentCommands.ExplodeAt` - Now with RPC synchronization for all players
  - `EnvironmentCommands.SetGravity` - Protected Master Client only (prevents game breaking)
  - All campfire lighting commands now use RPC for proper sync
- **Custom RPC**: `SyncSegmentJumpRPC` for cross-player segment synchronization

### Added
- **Advanced Server Info Page**: Comprehensive server and player management interface:
  - Detailed server information (room name, current map, player count, master client, ping, region)
  - Real-time player list with advanced status indicators (HOST, YOU, mod badges)
  - Automatic refresh every 5 seconds with manual refresh option
  - Steam profile integration - click "Profile" to open player's Steam profile
  - Expandable player details with character status, position, and health information

- **Intelligent Mod Detection System**: 
  - Real-time scanning and identification of player modifications
  - Visual mod badges with color coding (ATLAS/Cherry in red, Console in green)
  - Automatic detection of mod types: ATLAS users, Cherry users, Advanced Console users
  - Owner status detection for premium mod versions
  - Cheater identification system with visual warnings

- **Advanced Player Management** (Host-only features):
  - **Smart Player Kick System**: Multi-method player removal with automatic fallbacks
  - **Instant UI Updates**: Player list refreshes immediately after admin actions
  - **Intelligent Kick Routing**: System tries multiple kick approaches for maximum reliability
  - **Reason Tracking**: All kicks logged with specific reasons (mod conflicts, rule violations)
  - **Safety Protections**: Cannot kick yourself, only available to lobby host

- **Dynamic Map Control System**:
  - **Live Map Switching**: Change maps instantly without restarting lobby
  - **Visual Map Selector**: Easy-to-use dropdown interface with all available maps
  - **Seamless Player Transitions**: All connected players automatically follow map changes
  - **Host-only Access**: Map control restricted to lobby host for security
  - **Real-time Synchronization**: Instant map changes with full network sync
  - **Enhanced Map Loading System**: Fixed map loading issues with multi-method approach to prevent freezing
  - **Universal Map Compatibility**: Works for all players, even those without the mod installed

- **Advanced Server Administration System** (Host-only console commands):
  - **Multiple Kick Methods**: `ServerAdmin.SoftLockPlayer`, `ServerAdmin.ForceKickPlayer`, `ServerAdmin.BlackScreenKick`, `ServerAdmin.SlowPlayer`, `ServerAdmin.SpeedPlayer`
  - **Custom Kick Reasons**: `ServerAdmin.KickPlayerReason` with personalized messages
  - **Auto-Cheater Detection**: `ServerAdmin.KickAllCheaters` automatically removes mod users
  - **Player Management**: `ServerAdmin.ListPlayers`, `ServerAdmin.PlayerInfo` for detailed oversight
  - **Kick Method Varieties**: Standard (multi-method), Force (Photon direct), Soft (Airport redirect)

- **Player Effect Commands**: Complete suite for controlling other players:
  - `PlayerEffectCommands.SetPlayerEmotion PlayerName EmotionID` - Force player emotes
  - `PlayerEffectCommands.RestorePlayerStamina PlayerName` - Restore player's stamina
  - `PlayerEffectCommands.HealPlayer PlayerName` - Remove all debuffs and restore health
  - `PlayerEffectCommands.KillPlayer PlayerName` - Kill target player
  - `PlayerEffectCommands.RevivePlayer PlayerName` - Revive dead players
  - `PlayerEffectCommands.FreezePlayer PlayerName [intensity]` - Apply cold effect
  - `PlayerEffectCommands.StarvePlayer PlayerName [intensity]` - Apply hunger effect
  - `PlayerEffectCommands.PoisonPlayer PlayerName [intensity]` - Apply poison effect
  - `PlayerEffectCommands.ClearPlayerEffects PlayerName` - Remove all negative effects
  - `PlayerEffectCommands.TeleportPlayerToPlayer Player1 Player2` - Teleport between players
  - `PlayerEffectCommands.SetPlayerInvisible PlayerName [true/false]` - Toggle player visibility

- **Bee Swarm Command**:
  - `PlayerEffectCommands.SwarmWithBees PlayerName` - Spawn angry bee swarm on target player

- **Enhanced Spawn Commands**:
  - `SpawnCommands.RemoveMyItems` - Clear all items from local player inventory
  - `SpawnCommands.FreezeAllItems` - Freeze all world items in place
  - `SpawnCommands.UnfreezeAllItems` - Unfreeze all world items
  - `SpawnCommands.SpinAllItems` - Make all world items spin
  - `SpawnCommands.FlagGun` - Place visual flags in world via raycast
  - `SpawnCommands.RemoveAllFlags` - Remove all placed flags
  - `SpawnCommands.DrawWithSelectedItem` - Create item trails/drawings

- **Mod Detection System**:
  - Intelligent mod signature detection using Photon custom properties
  - Real-time mod information broadcasting and synchronization
  - Compatible with existing anticheat systems and mod detection protocols
  - Hash-based mod verification for detecting modifications
  - Cross-player mod comparison for lobby compatibility checking

### Technical Improvements
- **New UI Components**:
  - `ServerInfoPage.cs` with modern UI Elements framework and responsive design
  - `MapSelectionPage.cs` for intuitive map control interface  
  - `ModDetection.cs` system for comprehensive player mod tracking
  - Enhanced player card display with dynamic status indicators

- **Advanced Network Systems**:
  - Multi-method player kick system with automatic fallback mechanisms
  - Real-time map synchronization using lobby command integration
  - Robust map loading system with multi-method fallback chain (MapLoadingFix)
  - Resource cleanup and memory management before map loading to prevent freezing
  - Photon custom properties for mod detection and player status
  - Steam profile integration via platform URL handling
  - **Segment/biome synchronization** using native game mechanics (campfire activation) for maximum compatibility
  - **Fallback systems** ensure synchronization works even when primary methods fail
  - **Room properties** for maintaining segment state for joining players
  - **Master Client Protection**: All critical commands now properly validate Master Client status and block non-host execution
  - **Centralized Master Client Utils**: All Master Client checks now use unified `MasterClientUtils` system

- **Performance Optimizations**:
  - Automatic refresh system with 5-second intervals
  - Efficient mod detection caching and lazy loading
  - Expandable UI containers with scroll support for large player lists
  - Memory-efficient player status tracking and cleanup
  - Resource cleanup before map changes to prevent freezing
  - Smart map synchronization for all players (even those without mods)
  - Multi-method map loading with automatic fallbacks for stability
  - **Enhanced error handling** and logging for debugging segment sync issues
  - **Security improvements**: Critical commands (gravity, explosions, spawning) now properly restricted to server host

### Improved
- **Map & Segment Commands**: All segment/biome commands (Beach, Tropics, Alpine, Caldera, TheKiln, Peak) now work reliably in multiplayer sessions
- **Compatibility**: Segment synchronization works with both modded and vanilla clients
- **User Experience**: Clear indicators for Master Client vs Client command execution
- **Security**: All critical commands now properly restricted to server host with Master Client validation
- **Network Sync**: All RPC commands now have improved multiplayer synchronization
- **Documentation**: Updated with new synchronized segment and time commands


## [1.1.3-beta] - ScoutMaster Commands

### Added
- **ScoutMaster Command Suite**: Complete set of commands for controlling the ScoutMaster entity:
  - `ScoutMasterCommands.CallScoutmaster PlayerName` - Target ScoutMaster on specific player (30 seconds)
  - `ScoutMasterCommands.CallScoutmasterTime PlayerName [seconds]` - Target with custom duration
  - `ScoutMasterCommands.CallScoutmasterRandom [seconds]` - Target random player
  - `ScoutMasterCommands.StopScoutmaster` - Stop ScoutMaster hunting
- **Bugle Effect Commands**: Simulate the whistle/bugle item effects:
  - `ScoutMasterCommands.Bugle` - Quick self-targeting (30 seconds)
  - `ScoutMasterCommands.BugleCall [seconds]` - Self-targeting with custom duration
  - `ScoutMasterCommands.BugleCallOnPlayer PlayerName [seconds]` - Apply bugle effect on other players
- **ScoutMaster Management**: Administrative commands for ScoutMaster control:
  - `ScoutMasterCommands.SpawnScoutmaster` - Create new ScoutMaster (Master Client only)
  - `ScoutMasterCommands.RemoveScoutmaster` - Remove ScoutMaster from map (Master Client only)
  - `ScoutMasterCommands.TeleportScoutmaster PlayerName` - Teleport ScoutMaster to player location
  - `ScoutMasterCommands.ScoutmasterStatus` - Display current target information
- **Special Effects**:
  - `ScoutMasterCommands.CursePlayer PlayerName` - Extended hunting effect (10 minutes)

### Technical
- All commands use proper RPC networking via `SetCurrentTarget()` and `RPCA_SetCurrentTarget`
- ScoutMaster teleportation uses `WarpPlayerRPC` for network synchronization
- Commands integrate with existing ScoutMaster game mechanics
- Proper error handling and Master Client validation

## [1.1.2-beta] - Enhanced Hotkeys Interface

### Added
- **Enhanced Hotkeys UI**: Completely redesigned the Hotkeys tab in the console with improved interface:
  - Click-to-set key binding buttons with "WAITING FOR INPUT..." feedback
  - Visual key binding with timeout and ESC to cancel
  - Red delete buttons (✕) for each hotkey entry
  - Improved layout with proper spacing and sizing
- **Better User Experience**: 
  - No more manual typing of KeyCode names
  - Visual feedback during key binding process
  - One-click hotkey deletion
  - Proper field heights and text visibility

### Fixed
- **Hotkey Deletion**: Fixed non-working delete functionality for hotkey entries
- **UI Layout**: Fixed text field heights and visibility issues in hotkey interface
- **Key Input**: Improved key binding process with proper validation and feedback

### Technical Improvements
- Harmony patches for ConsoleHotkeyCell to replace original UI
- Enhanced error handling and logging for hotkey operations
- Proper cleanup and state management for key listening

## [1.1.1-beta] - Console DPI Reset Feature

### Added
- **F2 Key Binding**: Added F2 key to reset console DPI to default value (96)
- **Console DPI Protection**: F2 now provides instant fix for broken console display caused by `ConsoleSettings.SetDPI`

### Fixed
- **Improved UI Text Visibility**: Fixed white text on white background issue in input fields
- **Better Input Field Contrast**: All text fields, dropdowns, and search fields now have proper dark backgrounds with light text
- **UI Layout Improvements**: Better spacing and sizing for dropdown lists and search fields

### Documentation
- Updated README.md with F2 key binding information
- Added warning about SetDPI command and F2 reset solution

## [1.1.0-beta] - Advanced UI Integration

### Added
- **New Advanced Console UI Tab**: Integrated a modern UIElements-based tab into the in-game F1 console
- **Command Grouping**: Commands are now organized into logical groups (Teleportation, Fun Commands, Environment, etc.)
- **Visual Command Interface**: Each command now has a user-friendly interface with:
  - Dropdown selectors for player and enum parameters
  - Text fields for numeric and string inputs
  - Execute buttons for each command
  - Visual badges for RPC and MasterClient-only commands
- **Search and Filter**: Added search functionality and group filtering
- **Real-time Player Lists**: Player dropdowns update automatically when players join/leave

### Technical Improvements
- Replaced old MonoBehaviour-based UI with UIElements-based DebugPage
- Proper integration with game's existing console system via `DebugUIHandler.RegisterPage()`
- Improved command scanning and reflection-based parameter handling
- Better error handling and user feedback

### Fixed
- Removed legacy UI code that was causing conflicts
- Fixed object spawn commands to use proper local GameObject creation
- Improved command registration and discovery

## [1.0.1-beta] - Mod Structure Fixes

### Fixed
- Fixed mod structure and dependencies
- Improved command organization and RPC synchronization

## [1.0.0-beta] - First Release

### Added
- Complete Advanced Console mod with 100+ commands
- Teleportation, fun, environment, spawn, info, and sync commands
- Multiplayer RPC synchronization
- Russian comments with English logging
- Comprehensive documentation
