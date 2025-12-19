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

- **Plugin.cs**: BepInExプラグインエントリポイント。Harmonyパッチを初期化
- **TextChange.cs**: `WidgetTracker.Refresh()` へのPostfixパッチ。スキル名テキストを主属性に応じた色でラップ

### 多言語対応

`TextChange.cs` 内で4言語 (JP, EN, CN, ZHTW) のスキル名配列を定義。`EClass.core.config.lang` でゲームの言語設定を取得し、対応する配列を使用。

### 属性と色の対応

| 属性 | 色 |
|------|------|
| 筋力 (Strength) | 赤 |
| 耐久 (Endurance) | 明るい茶 |
| 器用 (Dexterity) | 緑 |
| 感覚 (Perception) | 水色 |
| 学習 (Learning) | 青 |
| 意志 (Will) | 青紫 |
| 魔力 (Magic) | 赤紫 |
| 魅力 (Charisma) | オレンジ |

## バージョン管理

バージョン番号は `Plugin.cs` の `PluginVersion` 定数で管理。変更履歴は `resouce/Changelog.md` に記載。
