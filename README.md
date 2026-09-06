# Advanced Console for PEAK

A BepInEx mod for [PEAK](https://store.steampowered.com/app/2239350/PEAK/) that adds an enhanced in-game console with
teleportation, item/prefab spawning, status effect control, RPC-synced commands, and an improved UI.

- **Plugin GUID**: `com.github.wwshare.advancedconsole`
- **Author**: wwshare
- **Version**: 1.1.9
- **Game**: PEAK (Unity 6000.3, BepInEx 5.4.2403)

## Repository Structure

```
.
├── src/                  # 插件源码 (C#, netstandard2.1)
│   └── com.github.wwshare.advancedconsole.csproj
├── package/              # Thunderstore 包内容 (打包时直接压缩此目录)
│   ├── manifest.json
│   ├── README.md
│   ├── CHANGELOG.md
│   ├── icon.png
│   ├── LICENSE
│   └── BepInEx/plugins/  # 构建产物 DLL
├── tools/publicize/      # 工具: 将游戏程序集公开化 (生成 refs/Assembly-CSharp.publicized.dll)
├── refs/                 # 公开化的游戏程序集 (构建依赖, 不入库, 可用工具重新生成)
└── .gitignore
```

## Building

前置条件:
- .NET SDK (支持 LangVersion 14)
- 已安装 PEAK 游戏 (默认路径 `D:\SteamLibrary\steamapps\common\PEAK`,可在 csproj 中修改 `GameDir`)
- `refs/Assembly-CSharp.publicized.dll` (若缺失, 用 `tools/publicize` 重新生成)

```powershell
cd src
dotnet build -c Release
```

产物: `src/bin/Release/netstandard2.1/com.github.wwshare.advancedconsole.dll`

### 部署到游戏 (本地测试)

```powershell
Copy-Item src/bin/Release/netstandard2.1/com.github.wwshare.advancedconsole.dll `
  <游戏目录>/BepInEx/plugins/
```

### 打包 Thunderstore 包

将 `package/` 目录内容压缩为 `<namespace>-<name>-<version>.zip` (当前为 `wwshare-AdvancedConsole-1.1.9.zip`),
zip 内应直接包含 `manifest.json`、`README.md`、`icon.png`、`CHANGELOG.md`、`LICENSE` 与 `BepInEx/` 目录。

## 功能概览

- 传送、物品/预制件生成、状态效果控制 (15 种)、RPC 同步命令
- 高级控制台 UI (搜索/分组/收藏/一键复制/右键复制)
- 控制台日志右键复制、Tab 命令补全、更醒目的光标
- 服务器信息页、玩家管理、Mod 检测

## 许可

见 `package/LICENSE`。
