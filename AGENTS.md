# プロジェクト概要

Elin (Steam roguelike game) 用のBepInExモッド。スキルトラッカーウィジェットのスキル名を、対応する主属性 (筋力、耐久など) に基づいて色分け表示する。

# ビルド

```bash
# Visual Studio または MSBuild でビルド
dotnet build SkillTrackerColoring.csproj -c Release
dotnet run --project tests/SkillTrackerColoring.Tests.csproj -c Release

# 出力先: bin/Release/SkillTrackerColoring.dll
```

# 依存ライブラリ

`lib/` フォルダにゲーム本体およびBepInExのDLLが必要:
- Elinゲームディレクトリから `Assembly-CSharp.dll`, `Elin.dll` 等をコピー
- インストール済みBepInEx 6.x の `BepInEx.Core.dll`, `0Harmony.dll` 等
- 現在の検証対象はElin Stable 23.355。BepInExは自動更新せず、インストール済みのDLLと一致させる
- ゲーム・BepInExの参照DLLはビルド出力へコピーしない

# アーキテクチャ

- **Plugin.cs**: BepInExプラグインエントリポイント。Harmonyパッチの初期化とConfig設定を管理
- **TextChange.cs**: `SkillTrackerColorPatch` クラス。`WidgetTracker.Refresh()` へのPostfixパッチでスキル名テキストを主属性に応じた色でラップ

## スキル属性の取得

`EClass.player.trackedElements` の各IDから現在の `Element` を取得し、`source.alias` と `source.aliasParent` で主属性を判定。表示名は `Element.Name` を使用する。

`TrackerTextFormatter.cs` はスキル名の完全一致で名前だけを装飾する。ウィジェットごとの弱参照キャッシュに元テキストと装飾済みテキストを保持し、ゲームがテキストを更新しないRefreshでもタグを重ねない。Config変更時は再装飾する。

`tests/` の依存パッケージ不要な .NET 8 コンソールテストは、同じフォーマッタの再装飾・名前一致・数値維持・色入力を検証する。Unity上の実行テストは含まない。

## 色のカスタマイズ

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

# バージョン管理

バージョン番号は `Plugin.cs` の `PluginVersion` 定数で管理。変更履歴は `resouce/Changelog.md` に記載。
