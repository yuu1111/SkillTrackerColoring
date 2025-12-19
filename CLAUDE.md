# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## プロジェクト概要

Elin (Steam roguelike game) 用のBepInExモッド。スキルトラッカーウィジェットのスキル名を、対応する主属性 (筋力、耐久など) に基づいて色分け表示する。

## ビルド

```bash
# Visual Studio または MSBuild でビルド
msbuild SkillTrackerColoring.sln /p:Configuration=Debug

# 出力先: bin\Debug\SkillTrackerColoring.dll
```

## 依存ライブラリ

`lib/` フォルダにゲーム本体およびBepInExのDLLが必要:
- Elinゲームディレクトリから `Assembly-CSharp.dll`, `Elin.dll` 等をコピー
- BepInEx 5.x の `BepInEx.Core.dll`, `0Harmony.dll` 等

## アーキテクチャ

- **Plugin.cs**: BepInExプラグインエントリポイント。Harmonyパッチの初期化とConfig設定を管理
- **TextChange.cs**: `SkillTrackerColorPatch` クラス。`WidgetTracker.Refresh()` へのPostfixパッチでスキル名テキストを主属性に応じた色でラップ

### スキル属性の取得

`EClass.sources.elements.rows` から `SourceElement.Row` を走査し、`aliasParent` フィールドで各スキルの親属性を判定。ゲーム更新でスキル名が変更されても自動追従。

### 色のカスタマイズ

BepInEx Config (`BepInEx/config/com.github.yuu1111.skilltrackercoloring.cfg`) で各属性の色を変更可能。

デフォルト色:
| 属性 | Config Key | デフォルト |
|------|------------|-----------|
| 筋力 | STR_Strength | #CC0000 |
| 耐久 | END_Endurance | #CC9933 |
| 器用 | DEX_Dexterity | #009933 |
| 感覚 | PER_Perception | #0099CC |
| 学習 | LER_Learning | #6666CC |
| 意志 | WIL_Will | #9933CC |
| 魔力 | MAG_Magic | #CC0099 |
| 魅力 | CHA_Charisma | #FF8000 |

## バージョン管理

バージョン番号は `Plugin.cs` の `PluginVersion` 定数で管理。変更履歴は `resouce/Changelog.md` に記載。
