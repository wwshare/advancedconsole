# Advanced Console for PEAK

A comprehensive console mod for PEAK that adds numerous fun and utility commands, plus an enhanced UI interface with Server Info and Player Management. Press `F1` to open the console and access both the traditional command interface and the new Advanced Console UI tabs.

## Installation

1. Install BepInEx 5.4.2403 for PEAK
2. Download the latest release
3. Extract DLL to `PEAK/BepInEx/plugins/`
4. Launch the game and press `F1` to open console

## Key Bindings

- **F1** - Toggle console on/off
- **F2** - Reset console DPI to default (use if console display is broken after using `ConsoleSettings.SetDPI`)
- **Custom Hotkeys** - Set up custom hotkeys for any console command using the enhanced Hotkeys tab

## Using the Advanced Console

### Enhanced UI Tabs
The mod adds several new tabs to the in-game console:

#### Advanced Console Tab
1. Press `F1` to open console, then click the **"Advanced Console"** tab
2. Use the visual interface to execute commands:
   - **Search**: Type to filter commands by name or description
   - **Group Filter**: Select a specific command category (Player Effects, Spawn Commands, etc.)
   - **Command Interface**: Each command has input fields for parameters and an Execute button
   - **Player Dropdowns**: Automatically updated with current connected players
   - **Visual Indicators**: and 🔒MC badges show command types

#### Server Info Tab
1. Click the **"Server Info"** tab to view comprehensive server and player information:
   - **Server Details**: Room name, current map, player count, master client, ping, etc.
   - **Player Management**: Detailed player cards with status indicators and mod detection
   - **Advanced Player Control**: Host-only features for managing players with multiple kick methods
   - **Mod Detection**: Automatically detect and display player mods (ATLAS, Cherry, Console)
   - **Steam Integration**: Click "Profile" to open player's Steam profile
   - **Status Indicators**: HOST, YOU, mod badges with color coding
   - **Real-time Updates**: Automatic refresh every 5 seconds or manual refresh button
   - **Advanced Kick System**: Host gets 5 different kick methods for each player:
     - **Soft-Lock**: Advanced soft-lock using AirportCheckInKiosk RPC (most effective)
     - **Force**: Direct Photon disconnection (immediate removal)
     - **Black**: Black screen kick via infinite teleportation (visual punishment)
     - **Slow**: Slowdown player physics (punishment/testing)
     - **Speed**: Speed up player physics (for fun/testing)

#### Enhanced Hotkeys Interface
The mod significantly improves the built-in Hotkeys tab:
1. Press `F1` to open console, then click the **"Hotkeys"** tab
2. Click **"Add New Hotkey"** to create a new hotkey (default: Insert key for Console.Clear)
3. **Set Key Binding**: Click the blue key button and press any key to bind it
   - Shows "WAITING FOR INPUT..." feedback
   - Press ESC to cancel, or wait 10 seconds for timeout
4. **Command Field**: Type any console command (e.g., `Console.Clear`, `Character.InfiniteStamina`)
5. **Delete Hotkey**: Click the red ✕ button to remove a hotkey
6. All changes are automatically saved

### Method 2: Traditional Text Commands
You can still type commands manually in the console using the format `ClassName.CommandName parameter1 parameter2`

## Commands

### Player Effect Commands 🆕

Control other players with comprehensive effect management:

- `PlayerEffectCommands.SetPlayerEmotion PlayerName EmotionID` - Force player to perform specific emotes
- `PlayerEffectCommands.RestorePlayerStamina PlayerName` - Instantly restore player's stamina
- `PlayerEffectCommands.HealPlayer PlayerName` - Remove all debuffs and restore full health
- `PlayerEffectCommands.KillPlayer PlayerName` - Kill target player
- `PlayerEffectCommands.RevivePlayer PlayerName` - Revive dead players
- `PlayerEffectCommands.FreezePlayer PlayerName [intensity]` - Apply cold effect (0.0-1.0)
- `PlayerEffectCommands.StarvePlayer PlayerName [intensity]` - Apply hunger effect (0.0-1.0)
- `PlayerEffectCommands.PoisonPlayer PlayerName [intensity]` - Apply poison effect (0.0-1.0)
- `PlayerEffectCommands.ClearPlayerEffects PlayerName` - Remove all negative status effects
- `PlayerEffectCommands.TeleportPlayerToPlayer Player1 Player2` - Teleport first player to second player
- `PlayerEffectCommands.SetPlayerInvisible PlayerName [true/false]` - Toggle player visibility
- `PlayerEffectCommands.SwarmWithBees PlayerName` - Spawn angry bee swarm on target player 🐝

### Enhanced Spawn Commands 🆕

Advanced item manipulation commands:

- `SpawnCommands.RemoveMyItems` - Clear all items from your inventory
- `SpawnCommands.FreezeAllItems` - Freeze all world items in place
- `SpawnCommands.UnfreezeAllItems` - Unfreeze all world items
- `SpawnCommands.SpinAllItems` - Make all world items spin continuously
- `SpawnCommands.FlagGun` - Place visual flags in world using raycast
- `SpawnCommands.RemoveAllFlags` - Remove all placed flags from world
- `SpawnCommands.DrawWithSelectedItem` - Create item trails/drawings with held item
- `SpawnCommands.SpawnShitPiton` - Spawn Shit Piton 🔒MC
- `SpawnCommands.SpawnBeeSwarm` - Spawn Bee Swarm
- `SpawnCommands.SpawnLuggageString [size]` - Spawn Luggage by size. Sizes: small, big, epic, ancient
- `SpawnCommands.SpawnAncientLuggage` - Spawn Ancient Luggage
- `SpawnCommands.SpawnEpicLuggage` - Spawn Epic Luggage
- `SpawnCommands.SpawnBigLuggage` - Spawn Big Luggage
- `SpawnCommands.SpawnSmallLuggage` - Spawn Small Luggage
- `SpawnCommands.SpawnRopeAnchorWithRope [segments]` - Spawn rope on loking point
- `SpawnCommands.SpawnChain` - Spawn chain on lookgin point

### Teleport Commands

- `Teleport.To PlayerName` - Teleport yourself to a specific player
- `Teleport.AllToMe` - Teleport all players to your location
- `Teleport.PlayerToPlayer PlayerName1 PlayerName2` - Teleport first player to second player
- `Teleport.PlayerToMe PlayerName` - Teleport a specific player to your location
- `Teleport.RandomPlayer` - Teleport yourself to a random player
- `Teleport.TeleportToPoint` - Teleport to looking point 
- `Teleport.TeleportPlayerToPoint [player]` - Teleport other players to looking point

### Fun Commands

- `FunCommands.KillPlayer PlayerName` - Kill a specific player
- `FunCommands.RevivePlayer PlayerName` - Revive a dead player
- `FunCommands.FreezePlayer PlayerName` - Freeze a player in place
- `FunCommands.UnfreezePlayer PlayerName` - Unfreeze a frozen player
- `FunCommands.InvisiblePlayer PlayerName` - Make a player invisible
- `FunCommands.VisiblePlayer PlayerName` - Make an invisible player visible again
- `FunCommands.SetPlayerSize PlayerName [scale]` - Change player size (default: 1.0)
- `FunCommands.DropCoconutOnPlayer PlayerName` - Drop coconut on player head 

### RPC Commands (Full Network Synchronization)

**Player Character RPC Commands:**
- `RPCCommands.JumpPlayer PlayerName` - Make a player jump
- `RPCCommands.JumpAllPlayers` - Make all players jump
- `RPCCommands.FallPlayer PlayerName [force]` - Make a player fall (default force: 7.0)
- `RPCCommands.FallAllPlayers [force]` - Make all players fall
- `RPCCommands.PassOutPlayer PlayerName` - Make a player pass out
- `RPCCommands.UnPassOutPlayer PlayerName` - Bring a player back to consciousness
- `RPCCommands.PassOutAllPlayers` - Make all players pass out
- `RPCCommands.UnPassOutAllPlayers` - Bring all players back to consciousness
- `RPCCommands.RevivePlayerRPC PlayerName [applyStatus]` - Revive a player (default: true)
- `RPCCommands.ReviveAllPlayers [applyStatus]` - Revive all players
- `RPCCommands.StopClimbingPlayer PlayerName` - Stop all climbing for a player
- `RPCCommands.StopClimbingAllPlayers` - Stop all climbing for all players
- `RPCCommands.StartCarryPlayer CarrierName TargetName` - Make one player carry another
- `RPCCommands.RenderPlayerDead PlayerName` - Render player appearance as dead
- `RPCCommands.RenderPlayerPassedOut PlayerName` - Render player appearance as passed out
- `RPCCommands.RenderAllPlayersDead` - Render all players as dead
- `RPCCommands.DestroyHeldItem PlayerName` - Destroy item in player's hand
- `RPCCommands.DestroyAllHeldItems` - Destroy all held items

**World Objects RPC Commands:**
- `RPCCommands.TriggerHelicopter` - Summon rescue helicopter
- `RPCCommands.LightAllFlares` - Light all flares in the scene
- `RPCCommands.LightAllCampfires` - Light all campfires
- `RPCCommands.ExtinguishAllCampfires` - Extinguish all campfires
- `RPCCommands.ShakeAllIcicles` - Shake all icicles
- `RPCCommands.ShakeAllBridges` - Shake all bridges
- `RPCCommands.FireAllArrows` - Fire all arrow shooters
- `RPCCommands.SpawnEruption PlayerName` - Spawn volcano eruption on player
- `RPCCommands.TriggerBananaPeel PlayerName` - Trigger banana peel slip effect

**Item RPC Commands:**
- `RPCCommands.CookAllItems [cookLevel]` - Cook all items to level (default: 100)
- `RPCCommands.FinishCookingAllItems` - Finish cooking all items
- `RPCCommands.ToggleCookingSmoke [enabled]` - Toggle cooking smoke (default: true)
- `RPCCommands.DenyPickupAllItems` - Prevent pickup of all items

**Game State RPC Commands:**
- `RPCCommands.ForceWin` - Force game victory
- `RPCCommands.UpdatePeakTimer [seconds]` - Update peak timer (default: 10)
- `RPCCommands.BeginIslandLoad [sceneName] [loadType]` - Start island loading

### Environment Commands

- `EnvironmentCommands.SetGravity [value]` - Change gravity (default: -9.81) Synced
- `EnvironmentCommands.GetGravity` - Display current gravity value
- `EnvironmentCommands.ToggleFog` - Toggle fog on/off
- `EnvironmentCommands.SetFogDensity [density]` - Set fog density (default: 0.01)
- `EnvironmentCommands.SetFogColor [r] [g] [b]` - Set fog color (RGB values 0-1)
- `EnvironmentCommands.LightNearestCampfire` - Light nearest campfire within 10m
- `EnvironmentCommands.ExtinguishNearbyFires [radius]` - Extinguish fires within radius (default: 20m)
- `EnvironmentCommands.GodModeEnvironment` - Disable environmental damage
- `EnvironmentCommands.ExplodeAt PlayerName [force]` - Create explosion at player location
- `EnvironmentCommands.Lightning` - Create lightning effect
- `EnvironmentCommands.ClearMobs` - Remove all mobs from the map
- `EnvironmentCommands.SetGameSpeed [speed]` - Change game speed multiplier (default: 1.0)
- `EnvironmentCommands.PauseGame` - Pause the game (speed = 0)
- `EnvironmentCommands.ResumeGame` - Resume the game (speed = 1)

### Spawn Commands

- `SpawnCommands.ListItems` - Show list of all available items with IDs
- `SpawnCommands.FindItem ItemName` - Search for items by name (partial matching)
- `SpawnCommands.SearchItems SearchTerm` - Advanced search with autocomplete hints
- `SpawnCommands.ShowPopularItems` - Display commonly used/useful items
- `SpawnCommands.ShowItemsByCategory Category` - Show items by category (food, tools, medical, survival, weapons, clothes)
- `SpawnCommands.GiveItemByName PlayerName ItemNameOrID` - Give item to player by name or ID
- `SpawnCommands.SpawnItemByName ItemNameOrID` - Spawn item in world by name or ID
- `SpawnCommands.SpawnRandomItem` - Spawn a random item
- `SpawnCommands.TickEveryone` - Spawn ticks on all players
- `SpawnCommands.TickPlayer PlayerName` - Spawn a tick on a specific player
- `SpawnCommands.GiveItem PlayerName ItemID` - Give item to player by ID (legacy)
- `SpawnCommands.ClearItems` - Remove all spawned items from the map

### Map & Segment Commands

- `MapCommands.ListSegments` - Show all available map segments
- `MapCommands.JumpToSegment SegmentNameOrNumber` - Jump to specific segment (Beach/0, Tropics/1, Alpine/2, Caldera/3, TheKiln/4, Peak/5) with automatic sync for Master Client
- `MapCommands.GetCurrentSegment` - Show current segment
- `MapCommands.NextSegment` - Move to next segment
- `MapCommands.PreviousSegment` - Move to previous segment
- `MapCommands.SyncAllPlayersToSegment` - Teleport all players to current segment
- `MapCommands.GoToBeach` - Quick jump to Beach segment
- `MapCommands.GoToTropics` - Quick jump to Tropics segment
- `MapCommands.GoToAlpine` - Quick jump to Alpine segment
- `MapCommands.GoToCaldera` - Quick jump to Caldera segment
- `MapCommands.GoToKiln` - Quick jump to TheKiln segment
- `MapCommands.GoToPeak` - Quick jump to Peak segment

**NEW: Synchronized Segment Commands ( Network Synced):**
- `RPCCommands.SyncGoToBeach` - Synchronized jump to Beach for ALL players 🔒MC
- `RPCCommands.SyncGoToTropics` - Synchronized jump to Tropics for ALL players 🔒MC
- `RPCCommands.SyncGoToAlpine` - Synchronized jump to Alpine for ALL players 🔒MC
- `RPCCommands.SyncGoToCaldera` - Synchronized jump to Caldera for ALL players 🔒MC
- `RPCCommands.SyncGoToKiln` - Synchronized jump to TheKiln for ALL players 🔒MC
- `RPCCommands.SyncGoToPeak` - Synchronized jump to Peak for ALL players 🔒MC
- `RPCCommands.SyncJumpToSegment SegmentNameOrNumber` - Synchronized jump to any segment for ALL players 🔒MC

### Server Administration Commands 🆕

Advanced server administration and player management:

- `ServerAdmin.SoftLockPlayer playerName` - Soft-lock player using advanced methods 🔒MC
- `ServerAdmin.KickPlayerReason playerName reason` - Kick player with custom reason (uses soft-lock) 🔒MC  
- `ServerAdmin.ForceKickPlayer playerName` - Force disconnect player via Photon 🔒MC
- `ServerAdmin.BlackScreenKick playerName` - Create black screen effect for player 🔒MC
- `ServerAdmin.SlowPlayer playerName` - Slow down player movement and physics 🔒MC
- `ServerAdmin.SpeedPlayer playerName` - Speed up player movement and physics 🔒MC
- `ServerAdmin.KickAllCheaters` - Kick all players with cheat mods (ATLAS/Cherry) 🔒MC
- `ServerAdmin.ListPlayers` - Show detailed player list with mod status
- `ServerAdmin.PlayerInfo playerName` - Show comprehensive player information
- `ServerAdmin.ListPhotonObjects` - List Photon Objects on map
- `ServerAdmin.ListSpawnablePrefabs` - List Spawnable Prefabs on map
- `ServerAdmin.DebugSpawn` - Debug Spawn item on map 🔒MC
- `ServerAdmin.ListAllItems` - List all items

Advanced lobby management and map control for hosts:

- `LobbyCommands.SetMap sceneName` - Change current map/level instantly with reliable loading (Master Client only) 🔒MC
- `LobbyCommands.ForceLoadScene sceneName` - Alternative map loading with enhanced reliability (Master Client only) 🔒MC
- `LobbyCommands.ListMaps` - Show all available maps with their scene names
- `LobbyCommands.GetCurrentMap` - Display current map information
- `LobbyCommands.ForceLoadScene sceneName` - Force load specific scene (Master Client only) 🔒MC

**Available Maps:**
- `level0` - Level 0, `level1` - Level 1, `level2` - Level 2, `level3` - Level 3
- `level4` - Level 4, `level5` - Level 5, `level6` - Level 6, `level7` - Level 7
- `level8` - Level 8, `level9` - Level 9, `level10` - Level 10, `level11` - Level 11
- `level12` - Level 12, `level13` - Level 13, `level14` - Level 14

### Character Afflictions (Status Effects)
- `CharacterAfflictions.AddCold` - Add cold status
- `CharacterAfflictions.AddCurse` - Add curse status
- `CharacterAfflictions.AddDrowsy` - Add drowsiness
- `CharacterAfflictions.AddHot` - Add heat status
- `CharacterAfflictions.AddHunger` - Add hunger
- `CharacterAfflictions.AddInjury` - Add injury status
- `CharacterAfflictions.AddPoison` - Add poison status
- `CharacterAfflictions.ClearAll` - Clear all status effects
- `CharacterAfflictions.ClearAllAilments` - Clear all negative effects
- `CharacterAfflictions.ClearCold` - Remove cold status
- `CharacterAfflictions.ClearCurse` - Remove curse status
- `CharacterAfflictions.ClearDrowsy` - Remove drowsiness
- `CharacterAfflictions.ClearHot` - Remove heat status
- `CharacterAfflictions.ClearHunger` - Remove hunger
- `CharacterAfflictions.ClearInjury` - Remove injury
- `CharacterAfflictions.ClearPoison` - Remove poison
- `CharacterAfflictions.Die` - Kill via afflictions
- `CharacterAfflictions.Starve` - Apply starvation

### Status Commands

- `StatusCommands.ClearCold PlayerName` - Remove cold status from player
- `StatusCommands.AddCold PlayerName [amount]` - Add cold status to player
- `StatusCommands.ClearHunger PlayerName` - Remove hunger from player
- `StatusCommands.AddHunger PlayerName [amount]` - Add hunger to player
- `StatusCommands.ClearPoison PlayerName` - Remove poison from player
- `StatusCommands.AddPoison PlayerName [amount]` - Add poison to player
- `StatusCommands.ClearCurse PlayerName` - Remove curse from player
- `StatusCommands.AddCurse PlayerName [amount]` - Add curse to player
- `StatusCommands.ClearDrowsy PlayerName` - Remove drowsiness from player
- `StatusCommands.AddDrowsy PlayerName [amount]` - Add drowsiness to player
- `StatusCommands.ClearInjury PlayerName` - Remove injury from player
- `StatusCommands.AddInjury PlayerName [amount]` - Add injury to player
- `StatusCommands.ClearHot PlayerName` - Remove heat status from player
- `StatusCommands.AddHot PlayerName [amount]` - Add heat status to player
- `StatusCommands.ClearAllStatuses PlayerName` - Remove all status effects from player

### Weather Commands

- `WeatherCommands.ToggleSnowStorm` - Toggle snow storm/wind on/off

### ScoutMaster Commands

**Basic Control:**
- `ScoutMasterCommands.CallScoutmaster PlayerName` - Make ScoutMaster hunt a specific player for 30 seconds
- `ScoutMasterCommands.CallScoutmasterTime PlayerName [seconds]` - Make ScoutMaster hunt player for custom time
- `ScoutMasterCommands.CallScoutmasterRandom [seconds]` - Make ScoutMaster hunt a random player
- `ScoutMasterCommands.StopScoutmaster` - Stop ScoutMaster from hunting anyone

**Bugle Effect (Whistle Simulation):**
- `ScoutMasterCommands.Bugle` - Quick call ScoutMaster to hunt yourself for 30 seconds (bugle effect)
- `ScoutMasterCommands.BugleCall [seconds]` - Call ScoutMaster to hunt yourself for custom time
- `ScoutMasterCommands.BugleCallOnPlayer PlayerName [seconds]` - Apply bugle effect on another player

**Management:**
- `ScoutMasterCommands.SpawnScoutmaster` - Spawn a new ScoutMaster on the map 🔒MC
- `ScoutMasterCommands.RemoveScoutmaster` - Remove ScoutMaster from the map 🔒MC
- `ScoutMasterCommands.TeleportScoutmaster PlayerName` - Teleport ScoutMaster to a player 🔒MC
- `ScoutMasterCommands.ScoutmasterStatus` - Show current ScoutMaster target information

**Special Effects:**
- `ScoutMasterCommands.CursePlayer PlayerName` - Curse a player - ScoutMaster hunts them for 10 minutes

### Information Commands

- `InfoCommands.ListPlayers` - Show list of all players with their status
- `InfoCommands.PlayerInfo PlayerName` - Show detailed information about a player
- `InfoCommands.ServerInfo` - Show server and room information
- `InfoCommands.GetPos [PlayerName]` - Get position of player (or yourself if no name given)
- `InfoCommands.PerfStats` - Show performance statistics
- `InfoCommands.Distance PlayerName1 PlayerName2` - Show distance between two players
- `InfoCommands.FindNearest [PlayerName]` - Find nearest player to you or specified player

### Achievement Commands 🆕

Manage in-game achievements and badges:

- `AchievementCommands.GrantAchievement AchievementType` - Grant specific achievement by type (supports autocomplete)
- `AchievementCommands.GrantAchievementByName AchievementName` - Grant achievement by name string
- `AchievementCommands.ClearAllAchievements` - Clear all achievements and reset progress
- `AchievementCommands.ListAchievements` - Show all available achievements with unlock status
- `AchievementCommands.AchievementInfo AchievementType` - Show detailed info about specific achievement
- `AchievementCommands.GrantRandomAchievement` - Grant a random achievement
- `AchievementCommands.GrantAllAchievements` - Grant all achievements (cheat mode)
- `AchievementCommands.AchievementStats` - Show achievement statistics and progress

**Available Achievement Types:**
- **Biome Badges**: `BeachcomberBadge`, `AlpinistBadge`, `VolcanologyBadge`, etc.
- **Activity Badges**: `CookingBadge`, `BoulderingBadge`, `ForagingBadge`, `FirstAidBadge`
- **Special Badges**: `PeakBadge`, `LoneWolfBadge`, `ClutchBadge`, `EsotericaBadge`
- **Skill Badges**: `KnotTyingBadge`, `MycologyBadge`, `NaturalistBadge`, `SurvivalistBadge`
- And many more! Use `ListAchievements` to see all available achievements.

### Console Logs
**Enhanced Log Features:**
- **Smart Grouping**: Duplicate messages are automatically grouped with Bootstrap-style badges

### Server Administration Commands 🆕

Advanced server management commands for lobby hosts:

- `ServerAdmin.SoftLockPlayer PlayerName` - Soft-lock player using advanced methods 🔒MC
- `ServerAdmin.KickPlayerReason PlayerName [reason]` - Kick a player with custom reason (uses soft-lock) 🔒MC
- `ServerAdmin.ForceKickPlayer PlayerName` - Force disconnect player via Photon 🔒MC
- `ServerAdmin.BlackScreenKick PlayerName` - Create black screen effect for player 🔒MC
- `ServerAdmin.SlowPlayer PlayerName` - Slow down player movement and physics 🔒MC
- `ServerAdmin.SpeedPlayer PlayerName` - Speed up player movement and physics 🔒MC
- `ServerAdmin.KickAllCheaters` - Automatically kick all players with cheat mods 🔒MC
- `ServerAdmin.ListPlayers` - Show detailed player list with mod status
- `ServerAdmin.PlayerInfo PlayerName` - Show comprehensive player information

**Kick Methods Explained:**
- **Standard Kick**: Uses advanced multi-method system (Airport Kiosk → Character Warp → Photon)
- **Force Kick**: Direct Photon disconnect (most reliable)
- **Soft Kick**: Sends player back to Airport lobby (less aggressive)
- **Auto-Kick Cheaters**: Automatically removes players with ATLAS/Cherry mods

## Default/Vanilla Game Commands

These commands are built into the base game and available without any mods:

### Character Commands
- `Character.Die` - Kill your character
- `Character.GainFullStamina` - Restore full stamina
- `Character.InfiniteStamina` - Toggle infinite stamina
- `Character.LockStatuses` - Lock current status effects
- `Character.PassOut` - Make your character pass out
- `Character.Revive` - Revive your character
- `Character.TestWin` - Test victory condition

### Items & Equipment
- `ItemDatabase.Add ItemName` - Add item to world (supports autocomplete)
- `Backpack.PrintBackpacks` - Show backpack information

### Map & Progress
- `MapHandler.JumpToSegment SegmentName` - Jump to map segment
- `AchievementManager.ClearAchievements` - Clear all achievements
- `AchievementManager.Grant AchievementType` - Grant specific achievement
- `Ascents.LockAll` - Lock all ascent routes
- `Ascents.UnlockAll` - Unlock all ascent routes
- `Ascents.UnlockOne` - Unlock one ascent route

### Customization & Testing
- `CharacterCustomization.Randomize` - Randomize character appearance
- `PassportManager.TestAllCosmetics` - Test all cosmetic items

### System & Debug
- `ApplicationCLI.SetTargetFramerate FPS` - Set target FPS
- `ConsoleSettings.Clear` - Clear console
- `ConsoleSettings.ClearMuted` - Clear muted commands
- `ConsoleSettings.Pause` - Pause console updates
- `ConsoleSettings.SetDPI Value` - Set console DPI ⚠️ **WARNING: Can break console display! Use F2 to reset to default (96)**
- `ConsoleSettings.Unpause` - Unpause console
- `Script.Execute ScriptPath` - Execute console script
- `IBinarySerializable.EnableLog` - Enable serialization logging
- `SmallShadowHandler.DebugDisable` - Disable shadow debugging
- `SmallShadowHandler.DebugEnable` - Enable shadow debugging
- `Modal.TestModal` - Test modal dialogs

**Note:** These default commands use different syntax than mod commands. Most support autocomplete when you start typing.

## Multiplayer Notes

- Synced commands automatically sync changes across all players
- Many commands require Master Client privileges to work properly
- **New Host Features**: Master Client can now kick players and change maps instantly via UI or commands
- **Map Control**: Use `LobbyCommands.SetMap` to change levels instantly without restarting the lobby
- **Enhanced Map Loading**: Improved multi-method map loading system prevents game freezes and works for all players
- **Compatibility Layer**: Map changes sync even to players without the mod
- **Player Management**: Advanced kick system with multiple methods for reliable player removal

### 🆕 **NEW: Segment/Biome Synchronization**
**PROBLEM SOLVED**: Previously, commands like `GoToAlpine` only worked for the person using the command. Other players would not follow to the new biome/segment.

**SOLUTION**: Use the new `RPCCommands.Sync*` commands for guaranteed synchronization:
- **OLD**: `MapCommands.GoToAlpine` (only works for you)
- **NEW**: `RPCCommands.SyncGoToAlpine` (works for ALL players)

**How it works:**
1. **Multi-Method Sync**: Uses campfire activation, direct teleportation, and room properties
2. **Universal Compatibility**: Works for players with and without the mod
3. **Master Client Required**: Only the lobby host can initiate synchronized segment jumps
4. **Automatic Fallback**: If one sync method fails, others are attempted automatically

**Commands to use:**
- Use `RPCCommands.SyncGoToAlpine` instead of `MapCommands.GoToAlpine`
- Use `RPCCommands.SyncGoToBeach` instead of `MapCommands.GoToBeach`
- Use `RPCCommands.SyncJumpToSegment` for any segment with full sync

- Commands will show warnings if you're not the Master Client but will still attempt to execute
- Player names with spaces should be written with underscores (e.g., "John_Doe")
- Use the F1 key to open/close the console
- All commands are case-sensitive

## Examples

```
# Advanced Console Mod Commands
Teleport.To Player_Name
FunCommands.SetPlayerSize Player_Name 2.0
StatusCommands.AddPoison Player_Name 0.5
EnvironmentCommands.SetGravity -5.0
EnvironmentCommands.LightNearestCampfire
MapCommands.JumpToSegment Beach
MapCommands.ListSegments
SpawnCommands.SpawnItemByName Flashlight
SpawnCommands.SearchItems rope
SpawnCommands.ObjectSpawnMushroom 2.0
InfoCommands.ListPlayers

# Achievement Management Commands
AchievementCommands.ListAchievements
AchievementCommands.GrantAchievement PeakBadge
AchievementCommands.GrantAchievementByName CookingBadge
AchievementCommands.AchievementInfo AlpinistBadge
AchievementCommands.AchievementStats
AchievementCommands.GrantAllAchievements
AchievementCommands.ClearAllAchievements

# New Lobby & Map Control Commands (Host only)
LobbyCommands.ListMaps
LobbyCommands.GetCurrentMap

# Change maps instantly (works for all players, even those without mods)
LobbyCommands.SetMap level0  # Main loading method
LobbyCommands.ForceLoadScene level5  # Alternative loading method

# Server Administration Commands (Host only)
ServerAdmin.ListPlayers
ServerAdmin.PlayerInfo Player_Name
ServerAdmin.KickPlayer Player_Name
ServerAdmin.KickPlayerReason Player_Name "Cheating detected"
ServerAdmin.ForceKickPlayer Player_Name
ServerAdmin.SoftLockPlayer Player_Name
ServerAdmin.KickAllCheaters

# Default Game Commands (no mod required)
Character.InfiniteStamina
ItemDatabase.Add Flashlight
CharacterAfflictions.ClearAll
MapHandler.JumpToSegment Alpine
```

## Advanced Player Management Features

### Intelligent Mod Detection System
The Advanced Console includes a sophisticated mod detection system that automatically identifies what mods other players are using:

- **Real-time Detection**: Automatically scans all connected players for mod signatures
- **Visual Status Indicators**: Color-coded badges show mod types (ATLAS in red, Console in green)
- **Compatibility Tracking**: Displays mod compatibility and potential conflicts
- **Host Awareness**: Know exactly what mods are in your lobby before starting

### Host Player Control System
When you're the lobby host (Master Client), you gain access to powerful player management tools:

- **Advanced Kick System**: Remove problematic players with multiple kick methods for reliability
- **Intelligent Targeting**: System automatically tries different kick approaches if one fails
- **Instant UI Updates**: Player list refreshes immediately after actions
- **Reason Tracking**: Kicks are logged with appropriate reasons (mod conflicts, rule violations)
- **Failsafe Protection**: Cannot accidentally kick yourself or when not host

### Dynamic Map Control
Hosts can change maps instantly without leaving the lobby:

- **Live Map Switching**: Change maps while players are connected
- **Seamless Transitions**: All players automatically follow to the new map
- **Visual Map Selector**: Easy-to-use dropdown with all available maps
- **No Lobby Restart**: Keep the same lobby settings and players
- **Instant Effect**: Map changes happen immediately with full synchronization

These features work together to give lobby hosts complete control over their game environment, ensuring the best possible experience for all players.

## Troubleshooting

If commands don't work:
1. Make sure you're in a multiplayer game
2. Check that BepInEx is properly installed
3. Verify the console opens with `F1` key
4. Some commands only work if you're the Master Client
5. Item spawning: Use `SpawnCommands.ListItems` or `SpawnCommands.SearchItems` to find valid item names
6. For item spawning, try both item names and IDs: `SpawnCommands.SpawnItemByName Flashlight` or `SpawnCommands.SpawnItemByName 1`
7. Map commands require you to be the Master Client in multiplayer
8. Default game commands use different syntax - try autocomplete by typing part of the command
9. If a command seems to not work, check the console output for error messages

### Common Issues
- **"Player not found"**: Make sure player name is spelled correctly, use underscores for spaces
- **"ItemDatabase not found"**: Wait for the game to fully load before using item commands
- **"MapHandler not found"**: Map commands only work during gameplay, not in menus
- **"Not MasterClient"**: Some commands require host privileges in multiplayer
- **"Only the master client can change the map"**: Map control commands are restricted to lobby host
- **Player kick not working**: The advanced kick system tries multiple methods; check console for details
- **Map change not working**: Ensure you're using correct map names from `LobbyCommands.ListMaps`

---
Thanks to [Console Unlocker](https://thunderstore.io/c/peak/p/Spoopylocal/Console_Unlocker/) to open dev console code.