﻿using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using static SkillTrackerColoring.Plugin;

namespace SkillTrackerColoring
{
    [HarmonyPatch(typeof(WidgetTracker))]
    [HarmonyPatch("Refresh")]
    internal class TextChange
    {
        static void Postfix(WidgetTracker __instance)
        {
            Text objText = __instance.text;

            string[] strengthText = { "" };
            string[] enduranceText = { "" };
            string[] dexterityText = { "" };
            string[] perceptionText = { "" };
            string[] learningText = { "" };
            string[] willText = { "" };
            string[] magicText = { "" };
            string[] charismaText = { "" };

            string lang = EClass.core.config.lang;

            if (lang == "JP")
            {
                strengthText = new[] { "筋力", "重量上げ", "採掘", "穴掘り", "木工", "鍛冶", "両手持ち", "格闘", "長剣", "斧", "鎌" };
                enduranceText = new[] { "耐久", "水泳", "木こり", "調教", "農業", "自然治癒", "彫刻", "重装備", "盾", "杖", "槍", "鈍器"};
                dexterityText = new[] { "器用", "鍵開け", "窃盗", "罠解体", "宝石細工", "裁縫", "製作", "投擲", "軽装備", "二刀流", "弓", "短剣" };
                perceptionText = new[] { "感覚", "隠密", "探索", "戦術", "射撃", "心眼", "回避", "見切り", "銃", "クロスボウ" };
                learningText = new[] { "学習", "採取", "読書", "鑑定", "解剖学", "暗記", "錬金", "料理", "戦略" };
                willText = new[] { "意志", "乗馬", "旅歩き", "釣り", "瞑想", "信仰" };
                magicText = new[] { "魔力", "魔力制御", "魔力の限界", "詠唱", "魔道具"};
                charismaText = new[] { "魅力", "共存", "演奏", "交渉術", "投資"};
            }
            else if (lang == "EN")
            {
                strengthText = new[] { "Strength", "Weightlifting", "Mining", "Digging", "Carpentry", "Blacksmith", "Two Handed", "Martial Art", "Long Sword", "Axe", "Scythe" };
                enduranceText = new[] { "Endurance", "Swimming", "Taming", "Farming", "Regeneration", "Sculpture", "Heavy Armor", "Shield", "Staff", "Polearm", "Mace" };
                dexterityText = new[] { "Dexterity", "Lockpicking", "Pickpocket", "Disarm Trap", "Jewelry", "Weaving", "Crafting", "Throwing", "Light Armor", "Dual Wield", "Bow", "Short Sword" };
                perceptionText = new[] { "Perception", "Stealth", "Spot Hidden", "Tactics", "Marksman", "Eye of Mind", "Evasion", "Greater Evasion", "Gun", "Crossbow" };
                learningText = new[] { "Learning", "Gathering", "Literacy", "Appraising", "Anatomy", "Memorization", "Alchemy", "Cooking", "Strategy" };
                willText = new[] { "Will", "Riding", "Travel", "Fishing", "Meditation", "Faith" };
                magicText = new[] { "Magic", "Control Magic", "Magic Capacity", "Casting", "Magic Device" };
                charismaText = new[] { "Charisma", "Symbiosis", "Music", "Negotiation", "Investing" };
            }
            else if (lang == "CN")
            {
                strengthText = new[] { "力量" ,"举重", "挖掘", "挖洞", "木工", "锻造", "双手武器", "格斗", "长剑", "斧", "镰" };
                enduranceText = new[] { "体质", "游泳", "伐木", "驯兽", "农业", "自愈", "雕刻", "重装备", "盾", "杖", "长枪", "钝器" };
                dexterityText = new[] { "灵巧", "开锁", "偷窃", "解除陷阱", "宝石加工", "裁缝", "制造", "投掷", "轻装备", "二刀流", "弓", "短剑" };
                perceptionText = new[] { "感知", "隐蔽", "探索", "战术", "射击", "心眼", "闪避", "看穿", "弩", "弩机" };
                learningText = new[] { "学习","采集", "读书", "鉴定", "解剖学", "背诵", "炼金", "烹饪", "战略" };
                willText = new[] { "意志", "骑乘", "旅行", "钓鱼", "冥想", "信仰" };
                magicText = new[] { "魔力" ,"魔力控制", "魔力极限", "咏唱", "魔道具" };
                charismaText = new[] { "魅力","共存", "演奏", "交涉", "投资" };
            } else if (lang == "ZHTW")
            {
                strengthText = new[] { "力量", "舉重", "挖掘", "挖洞", "木工", "鍛造", "雙手武器", "格鬥", "長劍", "斧", "鐮" };
                enduranceText = new[] { "體質", "游泳", "伐木", "馴獸", "農業", "自愈", "雕刻", "重裝備", "盾", "杖", "長槍", "鈍器" };
                dexterityText = new[] { "靈巧", "開鎖", "偷竊", "解除陷阱", "寶石加工", "裁縫", "製造", "投擲", "輕裝備", "二刀流", "弓", "短劍" };
                perceptionText = new[] { "感知", "隱蔽", "探索", "戰術", "射擊", "心眼", "閃避", "看穿", "槍械", "弩" };
                learningText = new[] { "學習", "採集", "讀書", "鑒定", "解剖學", "背誦", "煉金", "烹飪", "戰略" };
                willText = new[] { "意志", "騎乘", "旅行", "釣魚", "冥想", "信仰" };
                magicText = new[] { "魔力", "魔力控制", "魔力極限", "詠唱", "魔道具" };
                charismaText = new[] { "魅力", "共存", "演奏", "交渉", "投資" };
            }

            string[] lines = objText.text.Split('\n');
            string coloredText = "";

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                bool matched = false;

                // 各キーワード配列と色を順にチェック
                if (ContainsKeyword(line, strengthText))
                {
                    coloredText += ColorizeLine(line, StrengthColor.Value);
                    matched = true;
                }
                else if (ContainsKeyword(line, enduranceText))
                {
                    coloredText += ColorizeLine(line, EnduranceColor.Value);
                    matched = true;
                }
                else if (ContainsKeyword(line, dexterityText))
                {
                    coloredText += ColorizeLine(line, DexterityColor.Value);
                    matched = true;
                }
                else if (ContainsKeyword(line, perceptionText))
                {
                    coloredText += ColorizeLine(line, PerceptionColor.Value);
                    matched = true;
                }
                else if (ContainsKeyword(line, learningText)) {
                    coloredText += ColorizeLine(line, LearningColor.Value);
                    matched = true;
                } else if (ContainsKeyword(line, willText))
                {
                    coloredText += ColorizeLine(line, WillColor.Value);
                    matched = true;
                }
                else if (ContainsKeyword(line, magicText))
                {
                    coloredText += ColorizeLine(line, MagicColor.Value);
                    matched = true;
                }
                else if (ContainsKeyword(line, charismaText))
                {
                    coloredText += ColorizeLine(line, CharismaColor.Value);
                    matched = true;
                }


                // どのキーワードにもマッチしない場合はデフォルトの色
                if (!matched)
                {
                    coloredText += line;
                }

                // 最後の行でない場合のみ改行を追加
                if (i < lines.Length - 1)
                {
                    coloredText += "\n";
                }
            }

            objText.text = coloredText;  // 色付けしたテキストを適用

        }

        // 指定した行にキーワードが含まれているか確認するメソッド
        static bool ContainsKeyword(string line, string[] keywords)
        {
            foreach (var keyword in keywords)
            {
                if (line.Contains(keyword))
                {
                    return true;
                }
            }
            return false;
        }

        // 行を指定した色で色付けするメソッド
        static string ColorizeLine(string line, Color color)
        {
            string colorHex = ColorUtility.ToHtmlStringRGB(color);
            return $"<color=#{colorHex}>{line}</color>";
        }
    }
}