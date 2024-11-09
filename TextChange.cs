using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace SkillTrackerColoring
{
    [HarmonyPatch(typeof(WidgetTracker))]
    [HarmonyPatch("Refresh")]
    internal class TextChange
    {
        static void Postfix(WidgetTracker __instance)
        {
            Text objtext = __instance.text;
            string strtext = objtext.text;

            string[] strengthText = new string[] { "" };
            string[] endurancehText = new string[] { "" };
            string[] dexterityText = new string[] { "" };
            string[] perceptionText = new string[] { "" };
            string[] learningText = new string[] { "" };
            string[] willText = new string[] { "" };
            string[] magicText = new string[] { "" };
            string[] charismaText = new string[] { "" };

            Color strengthColor = new Color(0.8f, 0.0f, 0.0f);      // 筋力（赤）
            Color enduranceColor = new Color(0.8f, 0.6f, 0.2f);     // 耐久（明るい茶）
            Color dexterityColor = new Color(0.0f, 0.6f, 0.2f);     // 器用（緑）
            Color perceptionColor = new Color(0.0f, 0.6f, 0.8f);    // 感覚（水色）
            Color learningColor = new Color(0.2f, 0.3f, 0.8f);      // 学習（青）
            Color willColor = new Color(0.6f, 0.2f, 0.8f);          // 意志（青紫）
            Color magicColor = new Color(0.8f, 0.0f, 0.6f);         // 魔力（赤紫）
            Color charismaColor = new Color(1.0f, 0.5f, 0.0f);      // 魅力（オレンジ）

            string lang = EClass.core.config.lang;

            if (lang == "JP")
            {
                strengthText = new string[] { "筋力", "重量上げ", "採掘", "穴掘り", "木工", "鍛冶", "両手持ち", "格闘", "長剣", "斧", "鎌" };
                endurancehText = new string[] { "耐久", "水泳", "木こり", "調教", "農業", "自然治癒", "彫刻", "重装備", "盾", "杖", "槍", "鈍器"};
                dexterityText = new string[] { "器用", "鍵開け", "窃盗", "罠解体", "宝石細工", "裁縫", "製作", "投擲", "軽装備", "二刀流", "弓", "短剣" };
                perceptionText = new string[] { "感覚", "隠密", "探索", "戦術", "射撃", "心眼", "回避", "見切り", "銃", "クロスボウ" };
                learningText = new string[] { "学習", "採取", "読書", "鑑定", "解剖学", "暗記", "錬金", "料理", "戦略" };
                willText = new string[] { "意志", "乗馬", "旅歩き", "釣り", "瞑想", "信仰" };
                magicText = new string[] { "魔力", "魔力制御", "魔力の限界", "詠唱", "魔道具"};
                charismaText = new string[] { "魅力", "共存", "演奏", "交渉術", "投資"};
            }
            else if (lang == "EN")
            {
                strengthText = new string[] { "Strength", "Weightlifting", "Mining", "Digging", "Carpentry", "Blacksmith", "Two Handed", "Martial Art", "Long Sword", "Axe", "Scythe" };
                endurancehText = new string[] { "Endurance", "Swimming", "Taming", "Farming", "Regeneration", "Sculpture", "Heavy Armor", "Shield", "Staff", "Polearm", "Mace" };
                dexterityText = new string[] { "Dexterity", "Lockpicking", "Pickpocket", "Disarm Trap", "Jewelry", "Weaving", "Crafting", "Throwing", "Light Armor", "Dual Wield", "Bow", "Short Sword" };
                perceptionText = new string[] { "Perception", "Stealth", "Spot Hidden", "Tactics", "Marksman", "Eye of Mind", "Evasion", "Greater Evasion", "Gun", "Crossbow" };
                learningText = new string[] { "Learning", "Gathering", "Literacy", "Appraising", "Anatomy", "Memorization", "Alchemy", "Cooking", "Strategy" };
                willText = new string[] { "Will", "Riding", "Travel", "Fishing", "Meditation", "Faith" };
                magicText = new string[] { "Magic", "Control Magic", "Magic Capacity", "Casting", "Magic Device" };
                charismaText = new string[] { "Charisma", "Symbiosis", "Music", "Negotiation", "Investing" };
            }
            else if (lang == "CN")
            {
                strengthText = new string[] { "力量" ,"举重", "挖掘", "挖洞", "木工", "锻造", "双手武器", "格斗", "长剑", "斧", "镰" };
                endurancehText = new string[] { "体质", "游泳", "伐木", "驯兽", "农业", "自愈", "雕刻", "重装备", "盾", "杖", "长枪", "钝器" };
                dexterityText = new string[] { "灵巧", "开锁", "偷窃", "解除陷阱", "宝石加工", "裁缝", "制造", "投掷", "轻装备", "二刀流", "弓", "短剑" };
                perceptionText = new string[] { "感知", "隐蔽", "探索", "战术", "射击", "心眼", "闪避", "看穿", "弩", "弩机" };
                learningText = new string[] { "学习","采集", "读书", "鉴定", "解剖学", "背诵", "炼金", "烹饪", "战略" };
                willText = new string[] { "意志", "骑乘", "旅行", "钓鱼", "冥想", "信仰" };
                magicText = new string[] { "魔力" ,"魔力控制", "魔力极限", "咏唱", "魔道具" };
                charismaText = new string[] { "魅力","共存", "演奏", "交涉", "投资" };
            }


            string[] lines = objtext.text.Split('\n');
            string coloredText = "";

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                bool matched = false;

                // 各キーワード配列と色を順にチェック
                if (ContainsKeyword(line, strengthText))
                {
                    coloredText += ColorizeLine(line, strengthColor);
                    matched = true;
                }
                else if (ContainsKeyword(line, endurancehText))
                {
                    coloredText += ColorizeLine(line, enduranceColor);
                    matched = true;
                }
                else if (ContainsKeyword(line, dexterityText))
                {
                    coloredText += ColorizeLine(line, dexterityColor);
                    matched = true;
                }
                else if (ContainsKeyword(line, perceptionText))
                {
                    coloredText += ColorizeLine(line, perceptionColor);
                    matched = true;
                }
                else if (ContainsKeyword(line, learningText)) {
                    coloredText += ColorizeLine(line, learningColor);
                    matched = true;
                } else if (ContainsKeyword(line, willText))
                {
                    coloredText += ColorizeLine(line, willColor);
                    matched = true;
                }
                else if (ContainsKeyword(line, magicText))
                {
                    coloredText += ColorizeLine(line, magicColor);
                    matched = true;
                }
                else if (ContainsKeyword(line, charismaText))
                {
                    coloredText += ColorizeLine(line, charismaColor);
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

            objtext.text = coloredText;  // 色付けしたテキストを適用

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