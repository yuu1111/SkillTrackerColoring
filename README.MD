# SkillTrackerColoring

[Elin](https://store.steampowered.com/app/2135150/Elin/) 用のBepInExモッド

スキルトラッカーウィジェットのスキル名を、対応する主能力に応じて色分け表示します。

---

## 機能

- スキルトラッカーのスキル名を主能力ごとに色分け
- BepInEx Configで色をカスタマイズ可能
- ゲームアップデートによるスキル名変更に自動追従

## デフォルトの色

| 主能力 | 色 |
|--------|------|
| 筋力 (STR) | 赤 |
| 耐久 (END) | 茶色 |
| 器用 (DEX) | 緑 |
| 感覚 (PER) | 水色 |
| 学習 (LER) | 青 |
| 意志 (WIL) | 青紫 |
| 魔力 (MAG) | 赤紫 |
| 魅力 (CHA) | オレンジ |

## インストール

Steam Workshopから [SkillTrackerColoring](https://steamcommunity.com/sharedfiles/filedetails/?id=3362885911) をサブスクライブ

## 色のカスタマイズ

ゲーム起動後、以下のファイルで色を変更できます:

```
BepInEx/config/com.github.yuu1111.skilltrackercoloring.cfg
```

色は `#RRGGBB` 形式で指定します。

## 変更履歴

[Changelog.md](resouce/Changelog.md) を参照

## ライセンス

[MIT License](LICENCE)
