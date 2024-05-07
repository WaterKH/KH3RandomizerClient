using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using UE4DataTableInterpreter.Enums;

namespace KH3Randomizer.Data
{
    public static class Extensions
    {
        public static void ExecuteCommand(this string filePath)
        {
            try
            {
                var batFile = @$"{Path.Combine(Environment.CurrentDirectory, @$"wwwroot/pak/UnrealPak-With-Compression.bat")}";
                var processInfo = new ProcessStartInfo(batFile, filePath)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                };

                var process = Process.Start(processInfo);

                process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                    Console.WriteLine("output>>" + e.Data);
                process.BeginOutputReadLine();

                process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                    Console.WriteLine("error>>" + e.Data);
                process.BeginErrorReadLine();

                process.WaitForExit();

                Console.WriteLine("ExitCode: {0}", process.ExitCode);
                process.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Shuffle<T>(this IList<T> list, Random rng)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static long StringToSeed(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            var hash = 0L;
            foreach (var c in input.ToCharArray())
                hash = 31L * hash + c;

            return hash;
        }

        public static string QoLKeyToId(this string input)
        {
            var result = input;

            switch (input)
            {
                case "Easier Mini-UFO":
                    result = "BOSS_001";
                    break;
                case "Faster Raging Vulture":
                    result = "BOSS_002";
                    break;
                case "Dark Baymax Phase 1 Skip":
                    result = "BOSS_003";
                    break;
                case "Faster Lich Sequence":
                    result = "BOSS_004";
                    break;

                case "Frozen Chase Skip":
                    result = "EVENT_001";
                    break;
                case "Faster Crab Collection":
                    result = "EVENT_002";
                    break;
                case "Big Hero Rescue Skip":
                    result = "EVENT_003";
                    break;
                case "Faster Sora Collection":
                    result = "EVENT_004";
                    break;
                case "Union χ Skip":
                    result = "EVENT_005";
                    break;
                case "Guardians of Light Skip":
                    result = "EVENT_006";
                    break;
                case "Slow Mickey Skip":
                    result = "EVENT_007";
                    break;

                case "All Maps Unlocked":
                    result = "ITEM_003";
                    break;
                case "Easily Upgraded Keyblades":
                    result = "ITEM_004";
                    break;
                case "Max Level Ship":
                    result = "ITEM_002";
                    break;
            }

            return result;
        }

        public static string KeyIdToDisplay(this string input)
        {
            var inputSplit = input.Replace("\u0000", "").Split('_');

            if (inputSplit.Length == 3 && (inputSplit[1] == "SBOX" || inputSplit[1] == "LBOX"))
                return inputSplit[1] == "LBOX" ? $"Large Chest {int.Parse(inputSplit[2])}" : $"Small Chest {int.Parse(inputSplit[2])}";
            else if (inputSplit[0].ToLower() == "vbonus")
                if (inputSplit.Length == 3)
                    return $"Victory Bonus {inputSplit[1]} {int.Parse(inputSplit[2])}";
                else if (inputSplit[1].Contains("Minigame"))
                    return $"Victory Bonus {inputSplit[1]}";
                else
                    return $"Victory Bonus {int.Parse(inputSplit[1])}";
            else if (inputSplit[0] == "IW")
                return $"Level {(int.Parse(inputSplit[1]) % 10) + 1}";
            else if (inputSplit[0] == "IS")
                return $"Synthesis Item {int.Parse(inputSplit[1]) + 1}";


            return input.Replace("\u0000", "") switch
            {
                "m_PlayerSora" => "Sora",
                "I03001" => "Kingdom Key",
                "I03002" => "Hero's Origin",
                "I03003" => "Shooting Star",
                "I03004" => "Favorite Deputy",
                "I03005" => "Ever After",
                "I03006" => "Happy Gear",
                "I03007" => "Crystal Snow",
                "I03008" => "Hunny Spout",
                "I03009" => "Nano Gear",
                "I03010" => "Wheel of Fate",
                "I03011" => "Grand Chef",
                "I03012" => "Classic Tone",
                "I03013" => "Oathkeeper",
                "I03014" => "Oblivion",
                "I03015" => "Ultima Weapon",
                "I03018" => "Starlight",
                "I03019" => "Pandora's Power",
                "I04001" => "Hero's Belt",
                "I04002" => "Hero's Glove",
                "I04003" => "Shield Belt",
                "I04004" => "Defense Belt",
                "I04005" => "Guardian Belt",
                "I04006" => "Power Band",
                "I04007" => "Buster Band",
                "I04008" => "Buster Band+",
                "I04009" => "Cosmic Belt",
                "I04010" => "Cosmic Belt+",
                "I04011" => "Fire Bangle",
                "I04012" => "Firaga Bangle",
                "I04013" => "Firaza Bangle",
                "I04014" => "Fire Chain",
                "I04015" => "Blizzard Choker",
                "I04016" => "Blizzara Choker",
                "I04017" => "Blizaga Choker",
                "I04018" => "Blizzard Chain",
                "I04019" => "Thunder Trinket",
                "I04020" => "Thundaga Trinket",
                "I04021" => "Thundaza Trinket",
                "I04022" => "Thunder Chain",
                "I04023" => "Dark Anklet",
                "I04024" => "Midnight Anklet",
                "I04025" => "Chaos Anklet",
                "I04026" => "Dark Chain",
                "I04027" => "Divine Bandanna",
                "I04028" => "Elven Bandanna",
                "I04029" => "Aqua Chaplet",
                "I04030" => "Wind Fan",
                "I04031" => "Storm Fan",
                "I04032" => "Aero Armlet",
                "I04033" => "Aegis Chain",
                "I04034" => "Acrisius",
                "I04035" => "Cosmic Chain",
                "I04036" => "Petite Ribbon",
                "I04037" => "Ribbon",
                "I04038" => "Fira Bangle",
                "I04039" => "Blizzaza Choker",
                "I04040" => "Thundara Trinket",
                "I04041" => "Shadow Anklet",
                "I04042" => "Abas Chain",
                "I04043" => "Acrisius+",
                "I04044" => "Royal Ribbon",
                "I04045" => "Firefighter Rosette",
                "I04046" => "Umbrella Rosette",
                "I04047" => "Mask Rosette",
                "I04048" => "Snowman Rosette",
                "I04049" => "Insulator Rosette",
                "I04050" => "Power Weight",
                "I04051" => "Magic Weight",
                "I04052" => "Master Belt",
                "I05001" => "Laughter Pin",
                "I05002" => "Forest Clasp",
                "I05003" => "Ability Ring",
                "I05004" => "Ability Ring+",
                "I05005" => "Technician's Ring",
                "I05006" => "Technician's Ring+",
                "I05007" => "Skill Ring",
                "I05008" => "Skill Ring+",
                "I05009" => "Expert's Ring",
                "I05010" => "Master's Ring",
                "I05011" => "Cosmic Ring",
                "I05012" => "Power Ring",
                "I05013" => "Buster Ring",
                "I05014" => "Valor Ring",
                "I05015" => "Phantom Ring",
                "I05016" => "Orichalcum Ring",
                "I05017" => "Magic Ring",
                "I05018" => "Rune Ring",
                "I05019" => "Force Ring",
                "I05020" => "Sorcerer's Ring",
                "I05021" => "Wisdom Ring",
                "I05022" => "Bronze Necklace",
                "I05023" => "Silver Necklace",
                "I05024" => "Master's Necklace",
                "I05025" => "Bronze Amulet",
                "I05026" => "Silver Amulet",
                "I05027" => "Gold Amulet",
                "I05028" => "Junior Medal",
                "I05029" => "Star Medal",
                "I05030" => "Master Medal",
                "I05031" => "Mickey Clasp",
                "I05032" => "Soldier's Earring",
                "I05033" => "Fencer's Earring",
                "I05034" => "Mage's Earring",
                "I05035" => "Slayer's Earring",
                "I05036" => "Moon Amulet",
                "I05037" => "Star Charm",
                "I05038" => "Cosmic Arts",
                "I05039" => "Crystal Regalia",
                "I05040" => "Water Cufflink",
                "I05041" => "Thunder Cufflink",
                "I05042" => "Fire Cufflink",
                "I05043" => "Aero Cufflink",
                "I05044" => "Blizzard Cufflink",
                "I05045" => "Celestriad",
                "I05046" => "Yin-Yang Cufflink",
                "I05047" => "Gourmand's Ring",
                "I05048" => "Draw Ring",
                "I05049" => "Lucky Ring",
                "I05050" => "Flanniversary Badge",
                "I05051" or "I05052" or "I05053" or "I05054" or "I05055" or "I05056" or "I05057" or "I05058" or "I05059" or "I05060" or "I05061" or "I05062" => "Bronze Necklace",
                "I05063" or "I05064" or "I05065" or "I05066" or "I05067" or "I05068" or "I05069" or "I05070" or "I05071" or "I05072" or "I05073" => "Silver Necklace",
                "I05074" or "I05075" or "I05076" or "I05077" or "I05078" or "I05079" or "I05080" => "Master's Necklace",
                "I05081" or "I05082" => "Star Medal",
                "I05083" => "Junior Medal",
                "I05084" => "Star Medal",
                "I05085" or "I05086" or "I05087" or "I05088" or "I05089" or "I05090" or "I05091" or "I05092" => "Junior Medal",
                "I05093" => "Master Medal",
                "I05094" or "I05095" => "Star Medal",
                "I05096" => "Master Medal",
                "I05097" => "Star Medal",
                "I05098" => "Master Medal",
                "I05099" or "I05100" or "I05101" or "I05102" or "I05103" => "Star Medal",
                "I05104" or "I05105" or "I05106" or "I05107" or "I05108" or "I05109" or "I05110" => "Master Medal",
                "I05111" => "Breakthrough",
                "I05112" => "Crystal Regalia+",
                "item00" => "Base Pool",
                "item01" => "1 Star Pool",
                "item02" or "item03" or "item04" or "item05" => "2 Star Pool",
                "item06" or "item07" or "item08" or "item09" or "item10" => "3 Star Pool",
                "item11" or "item12" => "4 Star Pool",
                "item13" or "item14" => "5 Star Pool",
                "IW_0" => "Kingdom Key Upgrade 1",
                "IW_1" => "Kingdom Key Upgrade 2",
                "IW_2" => "Kingdom Key Upgrade 3",
                "IW_3" => "Kingdom Key Upgrade 4",
                "IW_4" => "Kingdom Key Upgrade 5",
                "IW_5" => "Kingdom Key Upgrade 6",
                "IW_6" => "Kingdom Key Upgrade 7",
                "IW_7" => "Kingdom Key Upgrade 8",
                "IW_8" => "Kingdom Key Upgrade 9",
                "IW_9" => "Kingdom Key Upgrade 10",
                "IW_10" => "Shooting Star Upgrade 1",
                "IW_11" => "Shooting Star Upgrade 2",
                "IW_12" => "Shooting Star Upgrade 3",
                "IW_13" => "Shooting Star Upgrade 4",
                "IW_14" => "Shooting Star Upgrade 5",
                "IW_15" => "Shooting Star Upgrade 6",
                "IW_16" => "Shooting Star Upgrade 7",
                "IW_17" => "Shooting Star Upgrade 8",
                "IW_18" => "Shooting Star Upgrade 9",
                "IW_19" => "Shooting Star Upgrade 10",
                "IW_20" => "Hero's Origin Upgrade 1",
                "IW_21" => "Hero's Origin Upgrade 2",
                "IW_22" => "Hero's Origin Upgrade 3",
                "IW_23" => "Hero's Origin Upgrade 4",
                "IW_24" => "Hero's Origin Upgrade 5",
                "IW_25" => "Hero's Origin Upgrade 6",
                "IW_26" => "Hero's Origin Upgrade 7",
                "IW_27" => "Hero's Origin Upgrade 8",
                "IW_28" => "Hero's Origin Upgrade 9",
                "IW_29" => "Hero's Origin Upgrade 10",
                "IW_30" => "Favorite Deputy Upgrade 1",
                "IW_31" => "Favorite Deputy Upgrade 2",
                "IW_32" => "Favorite Deputy Upgrade 3",
                "IW_33" => "Favorite Deputy Upgrade 4",
                "IW_34" => "Favorite Deputy Upgrade 5",
                "IW_35" => "Favorite Deputy Upgrade 6",
                "IW_36" => "Favorite Deputy Upgrade 7",
                "IW_37" => "Favorite Deputy Upgrade 8",
                "IW_38" => "Favorite Deputy Upgrade 9",
                "IW_39" => "Favorite Deputy Upgrade 10",
                "IW_40" => "Ever After Upgrade 1",
                "IW_41" => "Ever After Upgrade 2",
                "IW_42" => "Ever After Upgrade 3",
                "IW_43" => "Ever After Upgrade 4",
                "IW_44" => "Ever After Upgrade 5",
                "IW_45" => "Ever After Upgrade 6",
                "IW_46" => "Ever After Upgrade 7",
                "IW_47" => "Ever After Upgrade 8",
                "IW_48" => "Ever After Upgrade 9",
                "IW_49" => "Ever After Upgrade 10",
                "IW_50" => "Wheel of Fate Upgrade 1",
                "IW_51" => "Wheel of Fate Upgrade 2",
                "IW_52" => "Wheel of Fate Upgrade 3",
                "IW_53" => "Wheel of Fate Upgrade 4",
                "IW_54" => "Wheel of Fate Upgrade 5",
                "IW_55" => "Wheel of Fate Upgrade 6",
                "IW_56" => "Wheel of Fate Upgrade 7",
                "IW_57" => "Wheel of Fate Upgrade 8",
                "IW_58" => "Wheel of Fate Upgrade 9",
                "IW_59" => "Wheel of Fate Upgrade 10",
                "IW_60" => "Crystal Snow Upgrade 1",
                "IW_61" => "Crystal Snow Upgrade 2",
                "IW_62" => "Crystal Snow Upgrade 3",
                "IW_63" => "Crystal Snow Upgrade 4",
                "IW_64" => "Crystal Snow Upgrade 5",
                "IW_65" => "Crystal Snow Upgrade 6",
                "IW_66" => "Crystal Snow Upgrade 7",
                "IW_67" => "Crystal Snow Upgrade 8",
                "IW_68" => "Crystal Snow Upgrade 9",
                "IW_69" => "Crystal Snow Upgrade 10",
                "IW_70" => "Hunny Spout Upgrade 1",
                "IW_71" => "Hunny Spout Upgrade 2",
                "IW_72" => "Hunny Spout Upgrade 3",
                "IW_73" => "Hunny Spout Upgrade 4",
                "IW_74" => "Hunny Spout Upgrade 5",
                "IW_75" => "Hunny Spout Upgrade 6",
                "IW_76" => "Hunny Spout Upgrade 7",
                "IW_77" => "Hunny Spout Upgrade 8",
                "IW_78" => "Hunny Spout Upgrade 9",
                "IW_79" => "Hunny Spout Upgrade 10",
                "IW_80" => "Nano Gear Upgrade 1",
                "IW_81" => "Nano Gear Upgrade 2",
                "IW_82" => "Nano Gear Upgrade 3",
                "IW_83" => "Nano Gear Upgrade 4",
                "IW_84" => "Nano Gear Upgrade 5",
                "IW_85" => "Nano Gear Upgrade 6",
                "IW_86" => "Nano Gear Upgrade 7",
                "IW_87" => "Nano Gear Upgrade 8",
                "IW_88" => "Nano Gear Upgrade 9",
                "IW_89" => "Nano Gear Upgrade 10",
                "IW_90" => "Happy Gear Upgrade 1",
                "IW_91" => "Happy Gear Upgrade 2",
                "IW_92" => "Happy Gear Upgrade 3",
                "IW_93" => "Happy Gear Upgrade 4",
                "IW_94" => "Happy Gear Upgrade 5",
                "IW_95" => "Happy Gear Upgrade 6",
                "IW_96" => "Happy Gear Upgrade 7",
                "IW_97" => "Happy Gear Upgrade 8",
                "IW_98" => "Happy Gear Upgrade 9",
                "IW_99" => "Happy Gear Upgrade 10",
                "IW_100" => "Grand Chef Upgrade 1",
                "IW_101" => "Grand Chef Upgrade 2",
                "IW_102" => "Grand Chef Upgrade 3",
                "IW_103" => "Grand Chef Upgrade 4",
                "IW_104" => "Grand Chef Upgrade 5",
                "IW_105" => "Grand Chef Upgrade 6",
                "IW_106" => "Grand Chef Upgrade 7",
                "IW_107" => "Grand Chef Upgrade 8",
                "IW_108" => "Grand Chef Upgrade 9",
                "IW_109" => "Grand Chef Upgrade 10",
                "IW_110" => "Classic Tone Upgrade 1",
                "IW_111" => "Classic Tone Upgrade 2",
                "IW_112" => "Classic Tone Upgrade 3",
                "IW_113" => "Classic Tone Upgrade 4",
                "IW_114" => "Classic Tone Upgrade 5",
                "IW_115" => "Classic Tone Upgrade 6",
                "IW_116" => "Classic Tone Upgrade 7",
                "IW_117" => "Classic Tone Upgrade 8",
                "IW_118" => "Classic Tone Upgrade 9",
                "IW_119" => "Classic Tone Upgrade 10",
                "IW_120" => "Ultima Weapon Upgrade 1",
                "IW_121" => "Ultima Weapon Upgrade 2",
                "IW_122" => "Ultima Weapon Upgrade 3",
                "IW_123" => "Ultima Weapon Upgrade 4",
                "IW_124" => "Ultima Weapon Upgrade 5",
                "IW_125" => "Ultima Weapon Upgrade 6",
                "IW_126" => "Ultima Weapon Upgrade 7",
                "IW_127" => "Ultima Weapon Upgrade 8",
                "IW_128" => "Ultima Weapon Upgrade 9",
                "IW_129" => "Ultima Weapon Upgrade 10",
                "IW_150" => "Pandora's Power Upgrade 1",
                "IW_151" => "Pandora's Power Upgrade 2",
                "IW_152" => "Pandora's Power Upgrade 3",
                "IW_153" => "Pandora's Power Upgrade 4",
                "IW_154" => "Pandora's Power Upgrade 5",
                "IW_155" => "Pandora's Power Upgrade 6",
                "IW_156" => "Pandora's Power Upgrade 7",
                "IW_157" => "Pandora's Power Upgrade 8",
                "IW_158" => "Pandora's Power Upgrade 9",
                "IW_159" => "Pandora's Power Upgrade 10",
                "IW_160" => "Starlight Upgrade 1",
                "IW_161" => "Starlight Upgrade 2",
                "IW_162" => "Starlight Upgrade 3",
                "IW_163" => "Starlight Upgrade 4",
                "IW_164" => "Starlight Upgrade 5",
                "IW_165" => "Starlight Upgrade 6",
                "IW_166" => "Starlight Upgrade 7",
                "IW_167" => "Starlight Upgrade 8",
                "IW_168" => "Starlight Upgrade 9",
                "IW_169" => "Starlight Upgrade 10",
                "IW_170" => "Oathkeeper Upgrade 1",
                "IW_171" => "Oathkeeper Upgrade 2",
                "IW_172" => "Oathkeeper Upgrade 3",
                "IW_173" => "Oathkeeper Upgrade 4",
                "IW_174" => "Oathkeeper Upgrade 5",
                "IW_175" => "Oathkeeper Upgrade 6",
                "IW_176" => "Oathkeeper Upgrade 7",
                "IW_177" => "Oathkeeper Upgrade 8",
                "IW_178" => "Oathkeeper Upgrade 9",
                "IW_179" => "Oathkeeper Upgrade 10",
                "IW_180" => "Oblivion Upgrade 1",
                "IW_181" => "Oblivion Upgrade 2",
                "IW_182" => "Oblivion Upgrade 3",
                "IW_183" => "Oblivion Upgrade 4",
                "IW_184" => "Oblivion Upgrade 5",
                "IW_185" => "Oblivion Upgrade 6",
                "IW_186" => "Oblivion Upgrade 7",
                "IW_187" => "Oblivion Upgrade 8",
                "IW_188" => "Oblivion Upgrade 9",
                "IW_189" => "Oblivion Upgrade 10",
                "EVENT_001" => "Forge Knight's Shield+ for Goofy",
                "EVENT_002" => "Find Golden Herc Figure in Thebes: Alleyway, In Building",
                "EVENT_003" => "Find Golden Herc Figure in Thebes: Garden, Digged-Up Pit",
                "EVENT_004" => "Find Golden Herc Figure in Thebes: Garden, In Building",
                "EVENT_005" => "Find Golden Herc Figure in Thebes: Overlook, On Statue's Shield",
                "EVENT_006" => "Find Golden Herc Figure in Thebes: Agora, Near Save Point",
                "EVENT_007" => "Return All Golden Herc Figures",
                "EVENT_008" => "The Factory: Second Floor Unversed",
                "EVENT_009" => "Rescue Big Hero 6 from Darkubes",
                "TresUIMobilePortalDataAsset" => "Complete All Classic Kingdom Minigames",
                "EVENT_KEYBLADE_001" => "Complete Olympus",
                "EVENT_KEYBLADE_002" => "Complete Twilight Town",
                "EVENT_KEYBLADE_003" => "Complete Toy Box",
                "EVENT_KEYBLADE_004" => "Complete Kingdom of Corona",
                "EVENT_KEYBLADE_005" => "Complete Monstropolis",
                "EVENT_KEYBLADE_006" => "Complete 100 Acre Wood",
                "EVENT_KEYBLADE_007" => "Complete Arendelle",
                "EVENT_KEYBLADE_008" => "Complete The Caribbean",
                "EVENT_KEYBLADE_009" => "Complete San Fransokyo",
                "EVENT_KEYBLADE_010" => "Complete All Recipes",
                "EVENT_KEYBLADE_011" => "Defeat Demon Tide in The Keyblade Graveyard",
                "EVENT_KEYBLADE_012" => "Return the Proof of Promises to the Moogle",
                "EVENT_KEYBLADE_013" => "Return the Proof of Times Past to the Moogle",
                "EVENT_HEARTBINDER_001" => "Received After Returning to Yen Sid's Tower",
                "EVENT_HEARTBINDER_002" => "Complete Verum Rex: Beat of Lead during the Story",
                "EVENT_HEARTBINDER_003" => "The Power Plant: Tank Yard Heartless",
                "EVENT_HEARTBINDER_004" => "Complete Flash Tracer during the Story",
                "EVENT_REPORT_001a" or "EVENT_REPORT_001b" => "Complete Battlegate 1 in Olympus (Realm of the Gods: Courtyard)",
                "EVENT_REPORT_002a" or "EVENT_REPORT_002b" => "Complete Battlegate 2 in Olympus (Realm of the Gods: Apex)",
                "EVENT_REPORT_003a" or "EVENT_REPORT_003b" => "Complete Battlegate 3 in Twilight Town (The Old Mansion)",
                "EVENT_REPORT_004a" or "EVENT_REPORT_004b" => "Complete Battlegate 4 in Toy Box (Galaxy Toys: Kid Korral)",
                "EVENT_REPORT_005a" or "EVENT_REPORT_005b" => "Complete Battlegate 5 in Toy Box (Galaxy Toys: Main Floor: 1F)",
                "EVENT_REPORT_006a" or "EVENT_REPORT_006b" => "Complete Battlegate 6 in Kingdom of Corona (The Forest: Wetlands)",
                "EVENT_REPORT_007a" or "EVENT_REPORT_007b" => "Complete Battlegate 7 in Kingdom of Corona (The Forest: Hills)",
                "EVENT_REPORT_008a" or "EVENT_REPORT_008b" => "Complete Battlegate 8 in Monstropolis (The Power Plant: Tank Yard)",
                "EVENT_REPORT_009a" or "EVENT_REPORT_009b" => "Complete Battlegate 9 in Arendelle (The Labyrinth of Ice: Middle Tier)",
                "EVENT_REPORT_010a" or "EVENT_REPORT_010b" => "Complete Battlegate 10 in The Caribbean (The High Seas: Huddled Isles)",
                "EVENT_REPORT_011a" or "EVENT_REPORT_011b" => "Complete Battlegate 11 in San Fransokyo (The City: North District)",
                "EVENT_REPORT_012a" or "EVENT_REPORT_012b" => "Complete Battlegate 12 in San Fransokyo (The City: Central District)",
                "EVENT_REPORT_013a" or "EVENT_REPORT_013b" => "Complete Battlegate 13 in The Keyblade Graveyard (The Badlands)",
                "EVENT_REPORT_014" => "Defeat Dark Inferno",
                "EVENT_CKGAME_001" => "Complete Twilight Town",
                "EVENT_KEYITEM_001" => "Received After Returning to Yen Sid's Tower",
                "EVENT_KEYITEM_002" => "Meet Big Hero 6 in Hiro's Garage",
                "EVENT_KEYITEM_003" => "Find All Lucky Emblems",
                "EVENT_KEYITEM_004" => "Complete the Game on Critical",
                "EVENT_KEYITEM_005" => "Defeat Yozora",
                "EVENT_DATAB_001" => "Defeat Data Master Xehanort",
                "EVENT_DATAB_002" => "Defeat Data Ansem",
                "EVENT_DATAB_003" => "Defeat Data Xemnas",
                "EVENT_DATAB_004" => "Defeat Data Xigbar",
                "EVENT_DATAB_005" => "Defeat Data Luxord",
                "EVENT_DATAB_006" => "Defeat Data Larxene",
                "EVENT_DATAB_007" => "Defeat Data Marluxia",
                "EVENT_DATAB_008" => "Defeat Data Saïx",
                "EVENT_DATAB_009" => "Defeat Data Terra-Xehanort",
                "EVENT_DATAB_010" => "Defeat Data Dark Riku",
                "EVENT_DATAB_011" => "Defeat Data Vanitas",
                "EVENT_DATAB_012" => "Defeat Data Young Xehanort",
                "EVENT_DATAB_013" => "Defeat Data Xion",
                "EVENT_YOZORA_001" => "Defeat Yozora",
                _ => input,
            };
        }

        public static string KeyIdToDescription(this string input)
        {
            return input.Replace("\u0000", "") switch
            {
                "Vbonus_001" => "Olympus - Mount Olympus: Cliff Ascent Heartless",
                "Vbonus_002" => "Olympus - Thebes: Agora Flame Cores",
                "Vbonus_005" => "Olympus - Thebes: Overlook Flame Cores",
                "Vbonus_006" => "Olympus - Thebes: Gardens Flame Cores",
                "Vbonus_007" => "Olympus - Thebes: Alleyway Heartless",
                "Vbonus_008" => "Defeat Rock Troll in Olympus - Thebes: Agora",
                "Vbonus_009" => "After the Water Core(?) battle on the way up mountain in Olympus",
                "Vbonus_010" => "Defeat Rock Titan in Olympus: Mount Olympus - Summit",
                "Vbonus_011" => "Olympus - Realm of the Gods: Courtyard Satyrs",
                "Vbonus_013" => "Defeat Tornado Titan in Olympus: Realm of the Gods",
                "Vbonus_014" => "Defeat Demon Tide in Twilight Town - The Neighborhood: Tram Common",
                "Vbonus_015" => "Twilight Town - The Woods Powerwilds",
                "Vbonus_016" => "Twilight Town - The Old Mansion Heartless & Nobodies",
                "Vbonus_017" => "Toy Box - Andy's House Heartless",
                "Vbonus_018" => "Toy Box - Galaxy Toys: Main Floor: 1F Gigas",
                "Vbonus_019" => "Toy Box - Galaxy Toys: Action Figures Supreme Smashers",
                "Vbonus_020" => "Defeat Angelic Amber in Toy Box - Babies & Toddlers: Dolls",
                "Vbonus_021" => "After the UFO Mini-Boss in Toy Box - Babies & Toddlers: Outdoors",
                "Vbonus_022" => "Complete Verum Rex: Beat of Lead during the Story",
                "Vbonus_023" => "Defeat King of Toys in Toy Box: Galaxy Toys",
                "Vbonus_024" => "Kingdom of Corona - The Forest: Hills Heartless",
                "Vbonus_025" => "Kingdom of Corona - The Forest: Hills Chief Puffs & Puffballs",
                "Vbonus_026" => "Kingdom of Corona - The Forest: Hills Reapers",
                "Vbonus_027" => "Defeat Chaos Carriage in Kingdom of Corona",
                "Vbonus_028" => "Kingdom of Corona - The Kingdom: Wharf Nobodies",
                "Vbonus_029" => "Kingdom of Corona - The Forest: Tower Heartless",
                "Vbonus_030" => "Defeat Grim Guardianess in Kingdom of Corona - The Forest: Tower",
                "Vbonus_032" => "Monstropolis - Monsters Inc.: Lobby & Offices Unversed",
                "Vbonus_033" => "Monstropolis - Monsters Inc.: Laugh Floor Unversed",
                "Vbonus_034" => "Monstropolis - The Door Vault: Upper Level Heartless",
                "Vbonus_035" => "Monstropolis - The Factory: Second Floor Heartless & Unversed",
                "Vbonus_036" => "Monstropolis - The Factory: Second Floor Unversed",
                "Vbonus_037" => "Monstropolis - The Power Plant: Accessway Heartless & Unversed",
                "Vbonus_038" => "Monstropolis  - The Power Plant: Tank Yard Heartless",
                "Vbonus_039" => "Monstropolis - The Power Plant: Tank Yard Unversed",
                "Vbonus_040" => "Defeat Lump of Horror in Monstropolis - The Door Vault: Service Area",
                "Vbonus_041" => "Defeat Rock Troll & Winterhorns in Arendelle - The North Mountain: Gorge",
                "Vbonus_042" => "The Labyrinth of Ice: Middle Tier Ninjas I",
                "Vbonus_043" => "The Labyrinth of Ice: Lower Tier Ninjas II",
                "Vbonus_044" => "The Labyrinth of Ice: Middle Tier Ninjas III",
                "Vbonus_045" => "The Labyrinth of Ice: Middle Tier Ninjas IV",
                "Vbonus_047" => "Defeat Marshmallow in Arendelle - The North Mountain: Mountain Ridge",
                "Vbonus_048" => "Frozen Slider Frost Serpents",
                "Vbonus_049" => "Arendelle - The North Mountain: The Valley of Ice Heartless",
                "Vbonus_050" => "Defeat Skoll in Arendelle",
                "Vbonus_051" => "Defeat Metal Troll in San Fransokyo: The Bridge",
                "Vbonus_052" => "Meet Big Hero 6 in San Fransokyo - Hiro's Garage",
                "Vbonus_053" => "The City Heartless at Hiro's indicator in San Fransokyo - The City (Day)",
                "Vbonus_054" => "Defeat Catastrochorus in San Fransokyo - The City (Day)",
                "Vbonus_055" => "Rescue Big Hero 6 from Darkubes in San Fransokyo - The City (Night)",
                "Vbonus_056" => "Defeat Darkubes in San Fransokyo - The City (Night)",
                "Vbonus_057" => "Defeat Dark Baymax in San Fransokyo - The City (Day)",
                "Vbonus_058" => "Defeat Anchor Raiders in The Caribbean - Davy Jones' Locker",
                "Vbonus_059" => "1st Ship Battle in The Caribbean - Over the Edge",
                "Vbonus_060" => "Defeat Raging Vulture in The Caribbean - The High Seas: Southern Waters",
                "Vbonus_061" => "Defeat Lightning Angler in The Caribbean - The High Seas: Undersea Cavern",
                "Vbonus_062" => "Race Luxord to Port Royal in The Caribbean",
                "Vbonus_063" => "2nd Ship Battle in The Caribbean: The High Seas - Port Royal Waters",
                "Vbonus_064" => "3rd Ship Battle in The Caribbean: The High Seas - Forsaken Isle",
                "Vbonus_065" => "Defeat Kraken in The Caribbean - Shipwreck Cove",
                "Vbonus_066" => "Defeat Davy Jones in The Caribbean - Shipwreck Cove",
                "Vbonus_067" => "Defeat Anti-Aqua in The Dark World - The Realm of Darkness",
                "Vbonus_068" => "Defeat Lich in The Final World (San Fransokyo - The City: Central District)",
                "Vbonus_069" => "10,000 Enemy Fight in The Keyblade Graveyard: The Badlands",
                "Vbonus_070" => "Defeat Demon Tide in The Keyblade Graveyard: The Badlands",
                "Vbonus_071" => "Defeat Xigbar & Dark Riku in The Keyblade Graveyard - The Skein of Severance: Trail of Valediction",
                "Vbonus_072" => "Defeat Luxord, Marluxia, & Larxene in The Keyblade Graveyard - The Skein of Severance: Trail of Valediction",
                "Vbonus_073" => "Defeat Vanitas & Terra-Xehanort in The Keyblade Graveyard - The Skein of Severance: Twist of Isolation",
                "Vbonus_074" => "Defeat Saïx in The Keyblade Graveyard - The Skein of Severance: Twist of Isolation",
                "Vbonus_075" => "Defeat Young Xehanort, Ansem, & Xemnas in The Keyblade Graveyard: The Skein of Severance - Tower of Endings",
                "Vbonus_076" => "10,000 Enemy Fight in The Keyblade Graveyard: The Badlands",
                "Vbonus_082" => "Defeat Darkside in The Final World",
                "Vbonus_083" => "After collecting 222 Soras in The Final World",
                "Vbonus_084" => "After collecting 333 Soras in The Final World",
                "VBonus_Minigame001" => "Obtain A-rank in Verum Rex: Beat of Lead minigame in Toy Box",
                "VBonus_Minigame002" => "Obtain A-rank in the Festival Dance minigame in Kingdom of Corona",
                "VBonus_Minigame003" => "Obtain A-rank in the Frozen Slider minigame in Arendelle",
                "VBonus_Minigame004" => "After obtaining all the treasures in the Frozen Slider minigame in Arendelle",
                "VBonus_Minigame005" => "Obtain A-Rank in the Flash Tracer: Course A (Fred) minigame in San Fransokyo",
                "VBonus_Minigame006" => "Obtain A-Rank in the Flash Tracer: Course 2 (Go Go) minigame in San Fransokyo",
                "VBonus_Minigame007" => "Encounter Cherry Flan and obtain 20,000 points in Olympus",
                "VBonus_Minigame008" => "Encounter Strawberry Flan and obtain 17,000 points in Toy Box",
                "VBonus_Minigame009" => "Encounter Orange Flan and obtain 23,000 points in Kingdom of Corona",
                "VBonus_Minigame010" => "Encounter Banana Flan and obtain 20,000 points in Monstropolis",
                "VBonus_Minigame011" => "Encounter Grape Flan and obtain 20,000 points in Arendelle",
                "VBonus_Minigame012" => "Encounter Watermelon Flan and obtain 28,000 points in The Caribbean",
                "VBonus_Minigame013" => "Encounter Honeydew Flan and obtain 15,000 points in San Fransokyo",
                "VBonus_DLC_001" => "Defeat Dark Inferno χ in The Keyblade Graveyard (Re:Mind)",
                "VBonus_DLC_002" => "Defeat Anti-Aqua in The Keyblade Graveyard (Re:Mind)",
                "VBonus_DLC_003" => "Defeat Terra-Xehanort in The Keyblade Graveyard (Re:Mind)",
                "VBonus_DLC_004" => "Defeat Xigbar & Dark Riku in The Keyblade Graveyard (Re:Mind) - The Skein of Severance: Trail of Valediction",
                "VBonus_DLC_005" => "Defeat Luxord, Marluxia, & Larxene in The Keyblade Graveyard (Re:Mind) - The Skein of Severance: Trail of Valediction",
                "VBonus_DLC_006" => "Defeat Vanitas & Terra-Xehanort in The Keyblade Graveyard (Re:Mind) - The Skein of Severance: Twist of Isolation",
                "VBonus_DLC_007" => "After the Saïx boss battle in The Keyblade Graveyard (Re:Mind) - The Skein of Severance: Twist of Isolation",
                "VBonus_DLC_008" => "Defeat Young Xehanort, Ansem, & Xemnas in The Keyblade Graveyard (Re:Mind) - The Skein of Severance: Tower of Endings",
                "VBonus_DLC_009" => "Defeat the Special Shadow in Scala ad Caelum (Re:Mind) - The Stairway to the Sky",
                "VBonus_DLC_010" => "Defeat Darkside in Scala ad Caelum (Re:Mind) - Breezy Quarter",
                "VBonus_DLC_011" => "After collecting all of Kairi's heart fragments in Re:Mind",
                "VBonus_DLC_012" => "Defeat the Replica Xehanorts as the Guardians of Light in Re:Mind",
                "VBonus_DLC_013" => "Defeat the Replica Xehanorts as King Mickey in Re:Mind",
                "VBonus_DLC_014" => "After connecting all the keyholes in Re:Mind",
                "VBonus_DLC_015" => "Defeat Armored Xehanort in Re:Mind",
                "IS_0" => "Originally Ether",
                "IS_1" => "Originally Fire Bangle",
                "IS_2" => "Originally Fira Bangle",
                "IS_3" => "Originally Shadow Anklet",
                "IS_4" => "Originally Ability Ring+",
                "IS_5" => "Originally Elven Bandanna",
                "IS_6" => "Originally Thunder Trinket",
                "IS_7" => "Originally Thundara Trinket",
                "IS_8" => "Originally Refocuser",
                "IS_9" => "Originally Mythril Shard",
                "IS_10" => "Originally Wind Fan",
                "IS_11" => "Originally AP Boost",
                "IS_12" => "Originally Warhammer+",
                "IS_13" => "Originally Clockwork Shield+",
                "IS_14" => "Originally Technician's Ring+",
                "IS_15" => "Originally Ether",
                "IS_16" => "Originally Mega-Potion",
                "IS_17" => "Originally Dark Anklet",
                "IS_18" => "Originally Mythril Stone",
                "IS_19" => "Originally Aegis Shield+",
                "IS_20" => "Originally Blizzard Choker",
                "IS_21" => "Originally Blizzara Choker",
                "IS_22" => "Originally Skill Ring+",
                "IS_23" => "Originally Mega-Ether",
                "IS_24" => "Originally Strength Boost",
                "IS_25" => "Originally Magic Boost",
                "IS_26" => "Originally Defense Boost",
                "IS_27" => "Originally Firaga Bangle",
                "IS_28" => "Originally Blizzaga Choker",
                "IS_29" => "Originally Thundaga Trinket",
                "IS_30" => "Originally Divine Bandanna",
                "IS_31" => "Originally Storm Fan",
                "IS_32" => "Originally Midnight Anklet",
                "IS_33" => "Originally Astrolabe+",
                "IS_34" => "Originally Hi-Refocuser",
                "IS_35" => "Originally Mythril Gem",
                "IS_36" => "Originally Phantom Ring",
                "IS_37" => "Originally Sorcerer's Ring",
                "IS_38" => "Originally Firagun Bangle",
                "IS_39" => "Originally Blizzaza Choker",
                "IS_40" => "Originally Thundaza Trinket",
                "IS_41" => "Originally Chaos Anklet",
                "IS_42" => "Originally Acrisius",
                "IS_43" => "Originally Elixir",
                "IS_44" => "Originally Mythril Crystal",
                "IS_45" => "Originally Buster Band",
                "IS_46" => "Originally Orichalcum Ring",
                "IS_47" => "Originally Wisdom Ring",
                "IS_48" => "Originally Heartless Maul",
                "IS_49" => "Originally Nobody Guard",
                "IS_50" => "Originally Buster Band+",
                "IS_51" => "Originally Acrisius+",
                "IS_52" => "Originally Megalixir",
                "IS_53" => "Originally Heartless Maul+",
                "IS_54" => "Originally Nobody Guard+",
                "IS_55" => "Originally Save the Queen",
                "IS_56" => "Originally Save the King",
                "IS_57" => "Originally Cosmic Chain",
                "IS_58" => "Originally Ultima Weapon",
                "IS_59" => "Originally Save the Queen+",
                "IS_60" => "Originally Save the King+",
                "IS_61" => "Photo Mission Synthesis: Flame Core",
                "IS_62" => "Photo Mission Synthesis: Water Core",
                "IS_63" => "Photo Mission Synthesis: Big Beasts & Bugs Wall Display",
                "IS_64" => "Photo Mission Synthesis: Rapunzel's Tower",
                "IS_65" => "Photo Mission Synthesis: Chief Puff",
                "IS_66" => "Photo Mission Synthesis: CDA Agent",
                "IS_67" => "Photo Mission Synthesis: Statue of Hercules",
                "IS_68" => "Photo Mission Synthesis: Festival",
                "IS_69" => "Photo Mission Synthesis: Fire in the Secluded Forge",
                "IS_70" => "Photo Mission Synthesis: Ice Palace",
                "IS_71" => "Photo Mission Synthesis: Zeus",
                "IS_72" => "Photo Mission Synthesis: Olaf",
                "IS_73" => "Photo Mission Synthesis: Evening Star",
                "IS_74" => "Photo Mission Synthesis: Tram",
                "IS_75" => "Photo Mission Synthesis: Waterfall in Port Royal",
                "IS_76" => "Photo Mission Synthesis: Fish-Shaped Wind Socks in Broad Daylight",
                "IS_77" => "Photo Mission Synthesis: Big Green Cactuar",
                "IS_78" => "Photo Mission Synthesis: Scarecrow in the Fields",
                "IS_79" => "Photo Mission Synthesis: Demon Tower",
                "IS_80" => "Photo Mission Synthesis: Sora's Teammates",
                "IS_81" => "Originally Lucid Crystal",
                "IS_82" => "Originally Soothing Crystal",
                "IS_83" => "Originally Writhing Crystal",
                "IS_84" => "Originally Pulsing Crystal",
                "IS_85" => "Originally Blazing Crystal",
                "IS_86" => "Originally Frost Crystal",
                "IS_87" => "Originally Lightning Crystal",
                _ => input,
            };
        }

        public static string ValueIdToDisplay(this string input)
        {
            return input.Replace("\u0000", "") switch
            {
                "ITEM_POTION" => "Potion",
                "ITEM_HIGHPOTION" => "Hi-Potion",
                "ITEM_MEGAPOTION" => "Mega-Potion",
                "ITEM_ETHER" => "Ether",
                "ITEM_HIGHETHER" => "Hi-Ether",
                "ITEM_MEGAETHER" => "Mega-Ether",
                "ITEM_ELIXIR" => "Elixir",
                "ITEM_LASTELIXIR" => "Megalixir",
                "ITEM_FOCUSSUPPLY" => "Refocuser",
                "ITEM_HIGHFOCUSSUPPLY" => "Hi-Refocuser",
                "ITEM_ALLCURE" => "Panacea",
                "ITEM_TENT" => "Tent",
                "ITEM_POWERUP" => "Strength Boost",
                "ITEM_MAGICUP" => "Magic Boost",
                "ITEM_GUARDUP" => "Defense Boost",
                "ITEM_APUP" => "AP Boost",
                "WEP_KEYBLADE_SO_00" or "ETresItemDefWeapon::WEP_KEYBLADE00" => "Kingdom Key",
                "WEP_KEYBLADE_SO_01" or "ETresItemDefWeapon::WEP_KEYBLADE02" => "Hero's Origin",
                "WEP_KEYBLADE_SO_02" or "ETresItemDefWeapon::WEP_KEYBLADE01" => "Shooting Star",
                "WEP_KEYBLADE_SO_03" or "ETresItemDefWeapon::WEP_KEYBLADE03" => "Favorite Deputy",
                "WEP_KEYBLADE_SO_04" or "ETresItemDefWeapon::WEP_KEYBLADE04" => "Ever After",
                "WEP_KEYBLADE_SO_05" or "ETresItemDefWeapon::WEP_KEYBLADE09" => "Happy Gear",
                "WEP_KEYBLADE_SO_06" or "ETresItemDefWeapon::WEP_KEYBLADE06" => "Crystal Snow",
                "WEP_KEYBLADE_SO_07" or "ETresItemDefWeapon::WEP_KEYBLADE07" => "Hunny Spout",
                "WEP_KEYBLADE_SO_08" or "ETresItemDefWeapon::WEP_KEYBLADE08" => "Nano Gear",
                "WEP_KEYBLADE_SO_09" or "ETresItemDefWeapon::WEP_KEYBLADE05" => "Wheel of Fate",
                "WEP_KEYBLADE_SO_011" or "ETresItemDefWeapon::WEP_KEYBLADE11" => "Grand Chef",
                "WEP_KEYBLADE_SO_012" or "ETresItemDefWeapon::WEP_KEYBLADE10" => "Classic Tone",
                "WEP_KEYBLADE_SO_013" or "ETresItemDefWeapon::WEP_KEYBLADE12" => "Oathkeeper",
                "WEP_KEYBLADE_SO_014" or "ETresItemDefWeapon::WEP_KEYBLADE13" => "Oblivion",
                "WEP_KEYBLADE_SO_015" or "ETresItemDefWeapon::WEP_KEYBLADE14" => "Ultima Weapon",
                "WEP_KEYBLADE_SO_018" or "ETresItemDefWeapon::WEP_KEYBLADE17" => "Starlight",
                "WEP_KEYBLADE_SO_019" or "ETresItemDefWeapon::WEP_KEYBLADE18" => "Pandora's Power",
                "WEP_DONALD_01" => "Mage's Staff+",
                "WEP_DONALD_03" => "Warhammer+",
                "WEP_DONALD_05" => "Magician's Wand+",
                "WEP_DONALD_07" => "Nirvana+",
                "WEP_DONALD_09" => "Astrolabe+",
                "WEP_DONALD_011" => "Heartless Maul",
                "WEP_DONALD_012" => "Heartless Maul+",
                "WEP_DONALD_013" => "Save the Queen",
                "WEP_DONALD_014" => "Save the Queen+",
                "WEP_GOOFY_01" => "Knight's Shield+",
                "WEP_GOOFY_03" => "Clockwork Shield+",
                "WEP_GOOFY_05" => "Star Shield+",
                "WEP_GOOFY_07" => "Aegis Shield+",
                "WEP_GOOFY_09" => "Storm Anchor+",
                "WEP_GOOFY_011" => "Nobody Guard",
                "WEP_GOOFY_012" => "Nobody Guard+",
                "WEP_GOOFY_013" => "Save the King",
                "WEP_GOOFY_014" => "Save the King+",
                "PRT_ITEM01" => "Hero's Belt",
                "PRT_ITEM02" => "Hero's Glove",
                "PRT_ITEM03" => "Shield Belt",
                "PRT_ITEM04" => "Defense Belt",
                "PRT_ITEM05" => "Guardian Belt",
                "PRT_ITEM07" => "Buster Band",
                "PRT_ITEM08" => "Buster Band+",
                "PRT_ITEM09" => "Cosmic Belt",
                "PRT_ITEM10" => "Cosmic Belt+",
                "PRT_ITEM11" => "Fire Bangle",
                "PRT_ITEM12" => "Firaga Bangle",
                "PRT_ITEM13" => "Firaza Bangle",
                "PRT_ITEM14" => "Fire Chain",
                "PRT_ITEM15" => "Blizzard Choker",
                "PRT_ITEM16" => "Blizzara Choker",
                "PRT_ITEM17" => "Blizzaga Choker",
                "PRT_ITEM18" => "Blizzard Chain",
                "PRT_ITEM19" => "Thunder Trinket",
                "PRT_ITEM20" => "Thundaga Trinket",
                "PRT_ITEM21" => "Thundaza Trinket",
                "PRT_ITEM22" => "Thunder Chain",
                "PRT_ITEM23" => "Dark Anklet",
                "PRT_ITEM24" => "Midnight Anklet",
                "PRT_ITEM25" => "Chaos Anklet",
                "PRT_ITEM26" => "Dark Chain",
                "PRT_ITEM27" => "Divine Bandanna",
                "PRT_ITEM28" => "Elven Bandanna",
                "PRT_ITEM29" => "Aqua Chaplet",
                "PRT_ITEM30" => "Wind Fan",
                "PRT_ITEM31" => "Storm Fan",
                "PRT_ITEM32" => "Aero Armlet",
                "PRT_ITEM33" => "Aegis Chain",
                "PRT_ITEM34" => "Acrisius",
                "PRT_ITEM35" => "Cosmic Chain",
                "PRT_ITEM36" => "Petite Ribbon",
                "PRT_ITEM37" => "Ribbon",
                "PRT_ITEM38" => "Fira Bangle",
                "PRT_ITEM39" => "Blizzaza Choker",
                "PRT_ITEM40" => "Thundara Trinket",
                "PRT_ITEM41" => "Shadow Anklet",
                "PRT_ITEM42" => "Abas Chain",
                "PRT_ITEM43" => "Acrisius+",
                "PRT_ITEM44" => "Royal Ribbon",
                "PRT_ITEM45" => "Firefighter Rosette",
                "PRT_ITEM46" => "Umbrella Rosette",
                "PRT_ITEM47" => "Mask Rosette",
                "PRT_ITEM48" => "Snowman Rosette",
                "PRT_ITEM49" => "Insulator Rosette",
                "PRT_ITEM50" => "Power Weight",
                "PRT_ITEM51" => "Magic Weight",
                "PRT_ITEM52" => "Master Belt",
                "ACC_ITEM01" => "Laughter Pin",
                "ACC_ITEM02" => "Forest Clasp",
                "ACC_ITEM03" => "Ability Ring",
                "ACC_ITEM04" => "Ability Ring+",
                "ACC_ITEM06" => "Technician's Ring+",
                "ACC_ITEM08" => "Skill Ring+",
                "ACC_ITEM09" => "Expert's Ring",
                "ACC_ITEM10" => "Master's Ring",
                "ACC_ITEM11" => "Cosmic Ring",
                "ACC_ITEM12" => "Power Ring",
                "ACC_ITEM13" => "Buster Ring",
                "ACC_ITEM14" => "Valor Ring",
                "ACC_ITEM15" => "Phantom Ring",
                "ACC_ITEM16" => "Orichalcum Ring",
                "ACC_ITEM17" => "Magic Ring",
                "ACC_ITEM18" => "Rune Ring",
                "ACC_ITEM19" => "Force Ring",
                "ACC_ITEM20" => "Sorcerer's Ring",
                "ACC_ITEM21" => "Wisdom Ring",
                "ACC_ITEM22" => "Bronze Necklace",
                "ACC_ITEM23" => "Silver Necklace",
                "ACC_ITEM24" => "Master's Necklace",
                "ACC_ITEM25" => "Bronze Amulet",
                "ACC_ITEM26" => "Silver Amulet",
                "ACC_ITEM27" => "Gold Amulet",
                "ACC_ITEM28" => "Junior Medal",
                "ACC_ITEM29" => "Star Medal",
                "ACC_ITEM30" => "Master Medal",
                "ACC_ITEM31" => "Mickey Clasp",
                "ACC_ITEM32" => "Soldier's Earring",
                "ACC_ITEM33" => "Fencer's Earring",
                "ACC_ITEM34" => "Mage's Earring",
                "ACC_ITEM35" => "Slayer's Earring",
                "ACC_ITEM36" => "Moon Amulet",
                "ACC_ITEM37" => "Star Charm",
                "ACC_ITEM38" => "Cosmic Arts",
                "ACC_ITEM39" => "Crystal Regalia",
                "ACC_ITEM40" => "Water Cufflink",
                "ACC_ITEM41" => "Thunder Cufflink",
                "ACC_ITEM42" => "Fire Cufflink",
                "ACC_ITEM43" => "Aero Cufflink",
                "ACC_ITEM44" => "Blizzard Cufflink",
                "ACC_ITEM45" => "Celestriad",
                "ACC_ITEM46" => "Yin-Yang Cufflink",
                "ACC_ITEM47" => "Gourmand's Ring",
                "ACC_ITEM48" => "Draw Ring",
                "ACC_ITEM49" => "Lucky Ring",
                "ACC_ITEM50" => "Flanniversary Badge",
                "ACC_ITEM51" or "ACC_ITEM52" or "ACC_ITEM53" or "ACC_ITEM54" or "ACC_ITEM55" or "ACC_ITEM56" or "ACC_ITEM57" or "ACC_ITEM58" or "ACC_ITEM59" or "ACC_ITEM60" or "ACC_ITEM61" or "ACC_ITEM62" => "Bronze Necklace",
                "ACC_ITEM63" or "ACC_ITEM64" or "ACC_ITEM65" or "ACC_ITEM66" or "ACC_ITEM67" or "ACC_ITEM68" or "ACC_ITEM69" or "ACC_ITEM70" or "ACC_ITEM71" or "ACC_ITEM72" or "ACC_ITEM73" => "Silver Necklace",
                "ACC_ITEM74" or "ACC_ITEM75" or "ACC_ITEM76" or "ACC_ITEM77" or "ACC_ITEM78" or "ACC_ITEM79" or "ACC_ITEM80" => "Master's Necklace",
                "ACC_ITEM81" or "ACC_ITEM82" => "Star Medal",
                "ACC_ITEM83" => "Junior Medal",
                "ACC_ITEM84" => "Star Medal",
                "ACC_ITEM85" or "ACC_ITEM86" or "ACC_ITEM87" or "ACC_ITEM88" or "ACC_ITEM89" or "ACC_ITEM90" or "ACC_ITEM91" or "ACC_ITEM92" => "Junior Medal",
                "ACC_ITEM93" => "Master Medal",
                "ACC_ITEM94" or "ACC_ITEM95" => "Star Medal",
                "ACC_ITEM96" => "Master Medal",
                "ACC_ITEM97" => "Star Medal",
                "ACC_ITEM98" => "Master Medal",
                "ACC_ITEM99" or "ACC_ITEM100" or "ACC_ITEM101" or "ACC_ITEM102" or "ACC_ITEM103" => "Star Medal",
                "ACC_ITEM104" or "ACC_ITEM105" or "ACC_ITEM106" or "ACC_ITEM107" or "ACC_ITEM108" or "ACC_ITEM109" or "ACC_ITEM110" => "Master Medal",
                "ACC_ITEM111" => "Breakthrough",
                "ACC_ITEM112" => "Crystal Regalia+",
                "FOOD_ITEM41" => "Sea Bass en Papillote+",
                "FOOD_ITEM56" => "Tarte Aux Fruits+",
                "MAT_ITEM04" => "Blazing Crystal",
                "MAT_ITEM08" => "Frost Crystal",
                "MAT_ITEM12" => "Lightning Crystal",
                "MAT_ITEM16" => "Lucid Crystal",
                "MAT_ITEM20" => "Pulsing Crystal",
                "MAT_ITEM24" => "Writhing Crystal",
                "MAT_ITEM33" => "Mythril Shard",
                "MAT_ITEM34" => "Mythril Stone",
                "MAT_ITEM35" => "Mythril Gem",
                "MAT_ITEM36" => "Mythril Crystal",
                "MAT_ITEM44" => "Soothing Crystal",
                "MAT_ITEM47" => "Wellspring Gem",
                "MAT_ITEM48" => "Wellspring Crystal",
                "MAT_ITEM52" => "Hungry Crystal",
                "MAT_ITEM53" => "Fluorite",
                "MAT_ITEM54" => "Damascus",
                "MAT_ITEM55" => "Adamantite",
                "MAT_ITEM56" => "Orichalcum",
                "MAT_ITEM57" => "Orichalcum+",
                "MAT_ITEM58" => "Electrum",
                "MAT_ITEM59" => "Evanescent Crystal",
                "MAT_ITEM60" => "Illusory Crystal",
                "KEY_ITEM02" => "Gummiphone",
                "KEY_ITEM03" => "AR Device",
                "KEY_ITEM04" => "Prize Postcard",
                "KEY_ITEM05" => "M.O.G. Card",
                "KEY_ITEM06" => "Dream Heartbinder",
                "KEY_ITEM07" => "Pixel Heartbinder",
                "KEY_ITEM08" => "\'Ohana Heartbinder",
                "KEY_ITEM09" => "Pride Heartbinder",
                "KEY_ITEM10" => "Ocean Heartbinder",
                "KEY_ITEM11" => "Golden Herc Figure",
                "KEY_ITEM15" => "Proof of Promises",
                "KEY_ITEM16" => "Proof of Times Past",
                "KEY_ITEM14" => "Proof of Fantasy",
                "KEY_ITEM17" => "Heart Piece",
                "LSIGAME01" => "CK: Giantland",
                "LSIGAME02" => "CK: Mickey, The Mail Pilot",
                "LSIGAME03" => "CK: The Musical Farmer",
                "LSIGAME04" => "CK: Building a Building",
                "LSIGAME05" => "CK: The Mad Doctor",
                "LSIGAME06" => "CK: Mickey's Kitten Catch",
                "LSIGAME07" => "CK: The Klondike Kid",
                "LSIGAME08" => "CK: Fishin\' Frenzy",
                "LSIGAME09" => "CK: The Karnival Kid",
                "LSIGAME10" => "CK: Mickey Cuts Up",
                "LSIGAME11" => "CK: Mickey's Prison Escape",
                "LSIGAME12" => "CK: How to Play Baseball",
                "LSIGAME13" => "CK: How to Play Golf",
                "LSIGAME14" => "CK: Mickey's Circus",
                "LSIGAME15" => "CK: Camping Out",
                "LSIGAME16" => "CK: Taxi Troubles",
                "LSIGAME17" => "CK: Beach Party",
                "LSIGAME18" => "CK: The Wayward Canary",
                "LSIGAME19" => "CK: Mickey's Mechanical Man",
                "LSIGAME20" => "CK: The Barnyard Battle",
                "LSIGAME21" => "CK: Cast Out to Sea",
                "LSIGAME22" => "CK: Backyard Sports",
                "LSIGAME23" => "CK: Mickey Steps Out",
                "NAVI_MAP_HE01" => "Map: Realm of the Gods",
                "NAVI_MAP_HE02" => "Map: Mount Olympus",
                "NAVI_MAP_HE03" => "Map: Thebes",
                "NAVI_MAP_TT01" => "Map: The Neighborhood",
                "NAVI_MAP_RA01" => "Map: The Forest",
                "NAVI_MAP_RA02" => "Map: The Marsh",
                "NAVI_MAP_TS01" => "Map: Andy's House",
                "NAVI_MAP_TS02" => "Map: Galaxy Toys",
                "NAVI_MAP_MI01" => "Map: Monsters, Inc.",
                "NAVI_MAP_MI02" => "Map: The Factory",
                "NAVI_MAP_FZ01" => "Map: The North Mountain",
                "NAVI_MAP_FZ02" => "Map: The Labyrinth of Ice",
                "NAVI_MAP_CA01" => "Map: Port Royal Waters",
                "NAVI_MAP_CA02" => "Map: Isla de los Mastiles",
                "NAVI_MAP_CA03" => "Map: Ship's End",
                "NAVI_MAP_CA04" => "Map: Huddled Isles",
                "NAVI_MAP_CA05" => "Map: Sandbar Isle",
                "NAVI_MAP_BX01" => "Map: The City",
                "NAVI_MAP_KG01" => "Map: The Badlands",
                "NAVI_MAP_KG02" => "Map: The Skein of Severance",
                "NAVI_MAP_BT01" => "Map: The Stairway to the Sky",
                "NAVI_MAP_BT02" => "Map: Breezy Quarter",
                "REPORT_ITEM01" => "Secret Report NA",
                "REPORT_ITEM02" => "Secret Report 1",
                "REPORT_ITEM03" => "Secret Report 2",
                "REPORT_ITEM04" => "Secret Report 3",
                "REPORT_ITEM05" => "Secret Report 4",
                "REPORT_ITEM06" => "Secret Report 5",
                "REPORT_ITEM07" => "Secret Report 6",
                "REPORT_ITEM08" => "Secret Report 7",
                "REPORT_ITEM09" => "Secret Report 8",
                "REPORT_ITEM10" => "Secret Report 9",
                "REPORT_ITEM11" => "Secret Report 10",
                "REPORT_ITEM12" => "Secret Report 11",
                "REPORT_ITEM13" => "Secret Report 12",
                "REPORT_ITEM14" => "Secret Report 13",
                "ETresAbilityKind::AIR_RECOVERY" => "Ability: Aerial Recovery",
                "ETresAbilityKind::BLOW_COUNTER" => "Ability: Payback Strike",
                "ETresAbilityKind::REFLECT_GUARD" => "Ability: Block",
                "ETresAbilityKind::GUARD_COUNTER" => "Ability: Counter Slash",
                "ETresAbilityKind::REVENGEIMPACT" => "Ability: Counter Impact",
                "ETresAbilityKind::REVENGEDIVE" => "Ability: Counter Kick",
                "ETresAbilityKind::REVENGE_EX" => "Ability: Final Blow",
                "ETresAbilityKind::RISKDODGE" => "Ability: Risk Dodge",
                "ETresAbilityKind::SLASH_UPPER" => "Ability: Rising Spiral",
                "ETresAbilityKind::AIR_ROLL_BEAT" => "Ability: Groundbreaker",
                "ETresAbilityKind::AIR_DOWN" => "Ability: Falling Slash",
                "ETresAbilityKind::TRIPPLE_SLASH" => "Ability: Speed Slash",
                "ETresAbilityKind::CHARGE_THRUST" => "Ability: Triple Rush",
                "ETresAbilityKind::MAGICFLUSH" => "Ability: Magic Flash",
                "ETresAbilityKind::HIGHJUMP" => "Ability: High Jump",
                "ETresAbilityKind::DOUBLEFLIGHT" => "Ability: Doubleflight",
                "ETresAbilityKind::SUPERJUMP" => "Ability: Superjump",
                "ETresAbilityKind::SUPERSLIDE" => "Ability: Superslide",
                "ETresAbilityKind::GLIDE" => "Ability: Glide",
                "ETresAbilityKind::LIBRA" => "Ability: Scan",
                "ETresAbilityKind::DODGE" => "Ability: Dodge Roll",
                "ETresAbilityKind::AIRSLIDE" => "Ability: Air Slide",
                "ETresAbilityKind::AIRDODGE" => "Ability: Aerial Dodge",
                "ETresAbilityKind::MP_SAFETY" => "Ability: MP Safety",
                "ETresAbilityKind::EXPZERO" => "Ability: Zero EXP",
                "ETresAbilityKind::FRIEND_AID" => "Ability: Assist Friends",
                "ETresAbilityKind::COMBO_PLUS" => "Ability: Combo Plus",
                "ETresAbilityKind::AIRCOMBO_PLUS" => "Ability: Air Combo Plus",
                "ETresAbilityKind::COMBO_MASTER" => "Ability: Combo Master",
                "ETresAbilityKind::COMBO_UP" => "Ability: Combo Boost",
                "ETresAbilityKind::AIRCOMBO_UP" => "Ability: Air Combo Boost",
                "ETresAbilityKind::FIRE_UP" => "Ability: Fire Boost",
                "ETresAbilityKind::BLIZZARD_UP" => "Ability: Blizzard Boost",
                "ETresAbilityKind::THUNDER_UP" => "Ability: Thunder Boost",
                "ETresAbilityKind::WATER_UP" => "Ability: Water Boost",
                "ETresAbilityKind::AERO_UP" => "Ability: Aero Boost",
                "ETresAbilityKind::WIZZARD_STAR" => "Ability: Wizard's Ruse",
                "ETresAbilityKind::LUCK_UP" => "Ability: Lucky Strike",
                "ETresAbilityKind::ITEM_UP" => "Ability: Item Boost",
                "ETresAbilityKind::PRIZE_DRAW" => "Ability: Treasure Magnet",
                "ETresAbilityKind::LEAF_VEIL" => "Ability: Leaf Bracer",
                "ETresAbilityKind::LAST_LEAVE" => "Ability: Second Chance",
                "ETresAbilityKind::COMBO_LEAVE" => "Ability: Withstand Combo",
                "ETresAbilityKind::FOCUS_ASPIR" => "Ability: Focus Syphon",
                "ETresAbilityKind::ATTRACTION_TIME" => "Ability: Attraction Extender",
                "ETresAbilityKind::LINK_BOOST" => "Ability: Link Extender",
                "ETresAbilityKind::FORM_TIME" => "Ability: Formchange Extender",
                "ETresAbilityKind::DEFENDER" => "Ability: Defender",
                "ETresAbilityKind::CRITICAL_HALF" => "Ability: Damage Control",
                "ETresAbilityKind::DAMAGE_ASPIR" => "Ability: Damage Syphon",
                "ETresAbilityKind::MP_HASTE" => "Ability: MP Haste",
                "ETresAbilityKind::MP_HASTERA" => "Ability: MP Hastera",
                "ETresAbilityKind::MP_HASTEGA" => "Ability: MP Hastega",
                "ETresAbilityKind::MAGIC_COMBO_SAVE" => "Ability: Magic Combo Thrift",
                "ETresAbilityKind::MAGIC_COMBO_UP" => "Ability: Magic Galvanizer",
                "ETresAbilityKind::WALK_REGENE" => "Ability: MP Walker",
                "ETresAbilityKind::WALK_HEALING" => "Ability: HP Walker",
                "ETresAbilityKind::MAGIC_DRAW" => "Ability: Magic Treasure Magnet",
                "ETresAbilityKind::MASTER_DRAW" => "Ability: Master Treasure Magnet",
                "ETresAbilityKind::ATTRACTION_UP" => "Ability: Attraction Enhancer",
                "ETresAbilityKind::BURN_GUARD" => "Ability: Burn Protection",
                "ETresAbilityKind::CLOUD_GUARD" => "Ability: Cloud Protection",
                "ETresAbilityKind::SNEEZE_GUARD" => "Ability: Sneeze Protection",
                "ETresAbilityKind::FREEZE_GUARD" => "Ability: Freeze Protection",
                "ETresAbilityKind::DISCHARGE_GUARD" => "Ability: Electric Protection",
                "ETresAbilityKind::STUN_GUARD" => "Ability: Stun Protection",
                "ETresAbilityKind::COUNTER_UP" => "Ability: Reprisal Boost",
                "ETresAbilityKind::AUTO_FINISH" => "Ability: Auto-Finish",
                "ETresAbilityKind::FORM_UP" => "Ability: Situation Boost",
                "ETresAbilityKind::MAGIC_TIME" => "Ability: Grand Magic Extender",
                "ETresAbilityKind::AUTO_LOCK_MAGIC" => "Ability: Magic Lock-On",
                "ETresAbilityKind::GUARD_REGENE" => "Ability: Block Replenisher",
                "ETresAbilityKind::MP_SAVE" => "Ability: MP Thrift",
                "ETresAbilityKind::MP_LEAVE" => "Ability: Extra Cast",
                "ETresAbilityKind::FULLMP_BURST" => "Ability: Full MP Blast",
                "ETresAbilityKind::HARVEST" => "Ability: Harvester",
                "ETresAbilityKind::HP_CONVERTER" => "Ability: HP Converter",
                "ETresAbilityKind::MP_CONVERTER" => "Ability: MP Converter",
                "ETresAbilityKind::MUNNY_CONVERTER" => "Ability: Munny Converter",
                "ETresAbilityKind::ENDLESS_MAGIC" => "Ability: Endless Magic",
                "ETresAbilityKind::FP_CONVERTER" => "Ability: Focus Converter",
                "ETresAbilityKind::FIRE_ASPIR" => "Ability: Fire Syphon",
                "ETresAbilityKind::BLIZZARD_ASPIR" => "Ability: Blizzard Syphon",
                "ETresAbilityKind::THUNDER_ASPIR" => "Ability: Thunder Syphon",
                "ETresAbilityKind::WATER_ASPIR" => "Ability: Water Syphon",
                "ETresAbilityKind::AERO_ASPIR" => "Ability: Aero Syphon",
                "ETresAbilityKind::DARK_ASPIR" => "Ability: Dark Syphon",
                "ETresAbilityKind::SONIC_SLASH" => "Ability: Sonic Slash",
                "ETresAbilityKind::SONIC_DOWN" => "Ability: Sonic Cleave",
                "ETresAbilityKind::TURN_CUTTER" => "Ability: Buzz Saw",
                "ETresAbilityKind::SUMMERSALT" => "Ability: Somersault",
                "ETresAbilityKind::POLE_SPIN" => "Ability: Pole Spin",
                "ETresAbilityKind::POLE_SWING" => "Ability: Pole Swing",
                "ETresAbilityKind::WALL_KICK" => "Ability: Wall Kick",
                "ETresAbilityKind::BATTLE_GRAPHER" => "Ability: Frontline Photographer",
                "ETresAbilityKind::CHARISMA_CHEF" => "Ability: Chef Extraordinaire",
                "ETresAbilityKind::POWER_CURE" => "Ability: Cure Converter",
                "ETresAbilityKind::CRITICAL_COUNTER" => "Ability: Critical Counter",
                "ETresAbilityKind::CRITICAL_CHARGE" => "Ability: Critical Recharge",
                "ETresAbilityKind::CRITICAL_CONVERTER" => "Ability: Critical Converter",
                "ETresAbilityKind::QUICK_SHAFT" => "Ability: Quick Slash",
                "ETresAbilityKind::FLASH_STEP" => "Ability: Flash Step",
                "ETresAbilityKind::RADIAL_CUT" => "Ability: Radial Blaster",
                "ETresAbilityKind::FINAL_HEAVEN" => "Ability: Last Charge",
                "ETresAbilityKind::AERIAL_SWEEP" => "Ability: Aerial Sweep",
                "ETresAbilityKind::AERIAL_DIVE" => "Ability: Aerial Dive",
                "ETresAbilityKind::LUNCH_TIME" => "Ability: Cuisine Extender",
                "ETresAbilityKind::POWER_LUNCH" => "Ability: Hearty Meal",
                "ETresAbilityKind::OVER_TIME" => "Ability: Overtime",
                "ETresAbilityKind::BEST_CONDITION" => "Ability: Top Condition",
                "ETresAbilityKind::EXP_BARGAIN" => "Ability: EXP Incentive",
                "ETresAbilityKind::PRIZE_FEEVER" => "Ability: Prize Proliferator",
                "ETresAbilityKind::MILLIONAIRE" => "Ability: Rags to Riches",
                "ETresAbilityKind::CURAGAN" => "Ability: Curaza",
                "ETresAbilityKind::CHARGE_BERSERK" => "Ability: Berserk Charge",
                "ETresAbilityKind::OVERCOME" => "Ability: Hidden Potential",
                "ETresAbilityKind::GRAND_MAGIC" => "Ability: More Grand Magic",
                "ETresAbilityKind::FIRAGAN" => "Ability: Firaza",
                "ETresAbilityKind::BLIZZAGAN" => "Ability: Blizzaza",
                "ETresAbilityKind::THUNDAGAN" => "Ability: Thundaza",
                "ETresAbilityKind::WATAGAN" => "Ability: Waterza",
                "ETresAbilityKind::AEROGAN" => "Ability: Aeroza",
                "ETresAbilityKind::MAGIC_ROULETTE" => "Ability: Magic Roulette",
                "ETresAbilityKind::UNISON_FIRE" => "Ability: Unison Fire",
                "ETresAbilityKind::UNISON_BLIZZARD" => "Ability: Unison Blizzard",
                "ETresAbilityKind::UNISON_THUNDER" => "Ability: Unison Thunder",
                "ETresAbilityKind::FUSION_SPIN" => "Ability: Fusion Spin",
                "ETresAbilityKind::FUSION_ROCKET" => "Ability: Fusion Rocket",
                "ETresVictoryBonusKind::HP_UP5" => "Bonus: HP +5",
                "ETresVictoryBonusKind::HP_UP10" => "Bonus: HP +10",
                "ETresVictoryBonusKind::MP_UP3" => "Bonus: MP +3",
                "ETresVictoryBonusKind::MP_UP5" => "Bonus: MP +5",
                "ETresVictoryBonusKind::ITEM_SLOT_UP1" => "Bonus: Item Slot +1",
                "ETresVictoryBonusKind::ACC_SLOT_UP1" => "Bonus: Accessory Slot +1",
                "ETresVictoryBonusKind::DEF_SLOT_UP1" => "Bonus: Armor Slot +1",
                "ETresVictoryBonusKind::MELEM_FIRE" => "Magic: Fire",
                "ETresVictoryBonusKind::MELEM_WATER" => "Magic: Water",
                "ETresVictoryBonusKind::MELEM_CURE" => "Magic: Cure",
                "ETresVictoryBonusKind::MELEM_BLIZZARD" => "Magic: Blizzard",
                "ETresVictoryBonusKind::MELEM_THUNDER" => "Magic: Thunder",
                "ETresVictoryBonusKind::MELEM_AERO" => "Magic: Aero",
                "ETresAbilityKind::NONE" or "ETresVictoryBonusKind::NONE" => "",
                "Fire" or "Fira" or "Firaga" => "Fire Element",
                "Water" or "Watera" or "Waterga" => "Water Element",
                "Cure" or "Cura" or "Curaga" => "Cure Element",
                "Blizzard" or "Blizzara" or "Blizzaga" => "Blizzard Element",
                "Thunder" or "Thundara" or "Thundaga" => "Thunder Element",
                "Aero" or "Aerora" or "Aeroga" => "Aero Element",
                _ => input,
            };
        }

        public static string DataTableEnumToKey(this DataTableEnum dataTableEnum)
        {
            return dataTableEnum switch
            {
                DataTableEnum.ChrInit => "Starting Stats",
                DataTableEnum.EquipItem => "Equippables",
                DataTableEnum.Event => "Events",
                DataTableEnum.FullcourseAbility => "Fullcourse Abilities",
                DataTableEnum.LevelUp => "Level Ups",
                DataTableEnum.LuckyMark => "Lucky Emblems",
                DataTableEnum.VBonus => "Bonuses",
                DataTableEnum.WeaponEnhance => "Weapon Upgrades",
                DataTableEnum.SynthesisItem => "Synthesis Items",
                DataTableEnum.TreasureHE or DataTableEnum.TreasureTT or DataTableEnum.TreasureRA or DataTableEnum.TreasureTS or DataTableEnum.TreasureFZ or DataTableEnum.TreasureMI or DataTableEnum.TreasureCA or DataTableEnum.TreasureBX or DataTableEnum.TreasureKG or DataTableEnum.TreasureEW or DataTableEnum.TreasureBT => "Treasures",
                _ => "",
            };
        }

        public static DataTableEnum KeyToDataTableEnum(this string key)
        {
            return key switch
            {
                "Starting Stats" => DataTableEnum.ChrInit,
                "Equippables" => DataTableEnum.EquipItem,
                "Events" => DataTableEnum.Event,
                "Fullcourse Abilities" => DataTableEnum.FullcourseAbility,
                "Level Ups" => DataTableEnum.LevelUp,
                "Lucky Emblems" => DataTableEnum.LuckyMark,
                "Bonuses" => DataTableEnum.VBonus,
                "Weapon Upgrades" => DataTableEnum.WeaponEnhance,
                "Synthesis Items" => DataTableEnum.SynthesisItem,
                "Olympus" => DataTableEnum.TreasureHE,
                "Twilight Town" => DataTableEnum.TreasureTT,
                "Kingdom of Corona" => DataTableEnum.TreasureRA,
                "Toy Box" => DataTableEnum.TreasureTS,
                "Arendelle" => DataTableEnum.TreasureFZ,
                "Monstropolis" => DataTableEnum.TreasureMI,
                "The Caribbean" => DataTableEnum.TreasureCA,
                "San Fransokyo" => DataTableEnum.TreasureBX,
                "Keyblade Graveyard" => DataTableEnum.TreasureKG,
                "The Final World" => DataTableEnum.TreasureEW,
                "Re+Mind" => DataTableEnum.TreasureBT,
                "Base Sora Stats" => DataTableEnum.BaseCharStat,
                "Level Up Stats" => DataTableEnum.LevelUpStat,
                "Equipment Stats" => DataTableEnum.EquipItemStat,
                "Keyblade Enhance Stats" => DataTableEnum.WeaponEnhanceStat,
                "Food Effect Stats" => DataTableEnum.FoodItemEffectStat,
                "EXP Multiplier" => DataTableEnum.EXP,
                _ => DataTableEnum.None,
            };
        }

        public static string CategoryToKey(this string category, DataTableEnum dataTableEnum)
        {
            switch (dataTableEnum)
            {
                case DataTableEnum.ChrInit:
                    if (category == "Weapon")
                        return "Weapons";
                    else if (category != "Weapon" && !category.Contains("Crit"))
                        return "Abilities";
                    else if (category.Contains("Crit"))
                        return "Critical Abilities";

                    break;
                case DataTableEnum.EquipItem:
                    if (category.Contains("I03"))
                        return "Weapons";
                    else if (category.Contains("I04"))
                        return "Armor";
                    else if (category.Contains("I05"))
                        return "Accessories";

                    break;
                case DataTableEnum.Event:
                    if (!category.Contains("TresUIMobilePortalDataAsset") && !category.Contains("KEYBLADE") && !category.Contains("HEARTBINDER") && !category.Contains("REPORT") && !category.Contains("CKGAME") && !category.Contains("KEYITEM") && !category.Contains("DATAB") && !category.Contains("YOZORA"))
                        return "Events";
                    else if (category.Contains("KEYBLADE") || category.Equals("TresUIMobilePortalDataAsset"))
                        return "Keyblades";
                    else if (category.Contains("HEARTBINDER"))
                        return "Heartbinders";
                    else if (category.Contains("REPORT"))
                        return "Reports";
                    else if (category.Contains("CKGAME"))
                        return "Classic Kingdom";
                    else if (category.Contains("KEYITEM"))
                        return "Key Items";
                    else if (category.Contains("DATAB"))
                        return "Data Battles";
                    else if (category.Contains("YOZORA"))
                        return "Yozora";

                    break;
                case DataTableEnum.FullcourseAbility:
                    return "Abilities";
                case DataTableEnum.LevelUp:
                    return "Levels";
                case DataTableEnum.LuckyMark:
                    return "Lucky Emblems";
                case DataTableEnum.VBonus:
                    if (!category.Contains("Minigame"))
                        return "VBonus";
                    else if (category.Contains("Minigame") && int.Parse(category[^3..]) >= 7)
                        return "Flantastic Seven";
                    else if (category.Contains("Minigame") && int.Parse(category[^3..]) < 7)
                        return "Minigames";

                    break;
                case DataTableEnum.WeaponEnhance:
                    var splitCategory = int.Parse(category.Split('_')[1]);

                    if (splitCategory < 10)
                        return "Kingdom Key";
                    else if (splitCategory >= 10 && splitCategory < 20)
                        return "Shooting Star";
                    else if (splitCategory >= 20 && splitCategory < 30)
                        return "Hero's Origin";
                    else if (splitCategory >= 30 && splitCategory < 40)
                        return "Favorite Deputy";
                    else if (splitCategory >= 40 && splitCategory < 50)
                        return "Ever After";
                    else if (splitCategory >= 50 && splitCategory < 60)
                        return "Wheel of Fate";
                    else if (splitCategory >= 60 && splitCategory < 70)
                        return "Crystal Snow";
                    else if (splitCategory >= 70 && splitCategory < 80)
                        return "Hunny Spout";
                    else if (splitCategory >= 80 && splitCategory < 90)
                        return "Nano Gear";
                    else if (splitCategory >= 90 && splitCategory < 100)
                        return "Happy Gear";
                    else if (splitCategory >= 100 && splitCategory < 110)
                        return "Classic Tone";
                    else if (splitCategory >= 110 && splitCategory < 120)
                        return "Grand Chef";
                    else if (splitCategory >= 120 && splitCategory < 130)
                        return "Ultima Weapon";
                    else if (splitCategory >= 150 && splitCategory < 160)
                        return "Pandora's Power";
                    else if (splitCategory >= 160 && splitCategory < 170)
                        return "Starlight";
                    else if (splitCategory >= 170 && splitCategory < 180)
                        return "Oathkeeper";
                    else if (splitCategory >= 180 && splitCategory < 190)
                        return "Oblivion";

                    break;
                case DataTableEnum.SynthesisItem:
                    return "Synthesis Items";

                case DataTableEnum.TreasureHE:
                    return "Olympus";
                case DataTableEnum.TreasureTT:
                    return "Twilight Town";
                case DataTableEnum.TreasureRA:
                    return "Kingdom of Corona";
                case DataTableEnum.TreasureTS:
                    return "Toy Box";
                case DataTableEnum.TreasureFZ:
                    return "Arendelle";
                case DataTableEnum.TreasureMI:
                    return "Monstropolis";
                case DataTableEnum.TreasureCA:
                    return "The Caribbean";
                case DataTableEnum.TreasureBX:
                    return "San Fransokyo";
                case DataTableEnum.TreasureKG:
                    return "Keyblade Graveyard";
                case DataTableEnum.TreasureEW:
                    return "The Final World";
                case DataTableEnum.TreasureBT:
                    return "Re+Mind";

                default:
                    return "";
            }

            return "";
        }

        public static string ToLevelUpRoute(this string type)
        {
            return type switch
            {
                "TypeA" => "Wisdom",
                "TypeB" => "Vitality",
                "TypeC" => "Balance",
                _ => "",
            };
        }

        public static string GetChestLocation(this string chest, DataTableEnum dataTableEnum)
        {
            return dataTableEnum switch
            {
                DataTableEnum.TreasureHE => chest switch
                {
                    "Large Chest 1" => "Chest 29 (Large Chest, Realm of the Gods: Corridors, Near Save Point)",
                    "Large Chest 2" => "Chest 9 (Large Chest, Mount Olympus: Cliff Ascent, Below the Apple Tree)",
                    "Large Chest 3" => "Chest 31 (Large Chest, Realm of the Gods: Apex, Within Breakable Dive Box)",
                    "Large Chest 4" => "Chest 24 (Large Chest, Thebes: Overlook, Near Save Point)",
                    "Small Chest 1" => "Chest 25 (Small Chest, Realm of the Gods: Courtyard, Hallway Under Stairs)",
                    "Small Chest 2" => "Chest 26 (Small Chest, Realm of the Gods: Courtyard, Right of Entrance)",
                    "Small Chest 3" => "Chest 27 (Small Chest, Realm of the Gods: Courtyard, Left of Entrance)",
                    "Small Chest 4" => "Chest 28 (Small Chest, Realm of the Gods: Corridors, Near Lucky Emblem on Stairs Wall)",
                    "Small Chest 6" => "Chest 30 (Small Chest, Realm of the Gods: Cloud Ridge, After Rail Gauntlet)",
                    "Small Chest 8" => "Chest 32 (Small Chest, Realm of the Gods: Apex, 2nd Airstep Platform)",
                    "Small Chest 9" => "Chest 1 (Small Chest, Mount Olympus: Ravine, Inside the Foot Statue)",
                    "Small Chest 10" => "Chest 2 (Small Chest, Mount Olympus: Ravine, In the River)",
                    "Small Chest 11" => "Chest 3 (Small Chest, Mount Olympus: Cliff Ascent, Cave Alcove Northwest Corner)",
                    "Small Chest 12" => "Chest 4 (Small Chest, Mount Olympus: Cliff Ascent, Cave Alcove Southwest Corner)",
                    "Small Chest 13" => "Chest 5 (Small Chest, Mount Olympus: Cliff Ascent, Cave Alcove Northwest Corner)",
                    "Small Chest 14" => "Chest 6 (Small Chest, Mount Olympus: Cliff Ascent, On Lucky Emblem Mountain)",
                    "Small Chest 15" => "Chest 7 (Small Chest, Mount Olympus: Cliff Ascent, Near Lucky Emblem Mountain)",
                    "Small Chest 16" => "Chest 8 (Small Chest, Mount Olympus: Cliff Ascent, South Side of 1st Scalable Wall)",
                    "Small Chest 17" => "Chest 10 (Small Chest, Mount Olympus: Mountainside, Near the Top of the 1st Cliff)",
                    "Small Chest 20" => "Chest 11 (Small Chest, Mount Olympus: Summit, Top of the 2nd Cliff)",
                    "Small Chest 21" => "Chest 12 (Small Chest, Mount Olympus: Mountainside, High Platform After 1st Cliff)",
                    "Small Chest 22" => "Chest 13 (Small Chest, Thebes: Alleyway, Upper Grass Platform)",
                    "Small Chest 23" => "Chest 14 (Small Chest, Thebes: Alleyway, Inside Waterway)",
                    "Small Chest 24" => "Chest 15 (Small Chest, Thebes: Agora, Near Lucky Emblem on Stairs)",
                    "Small Chest 25" => "Chest 16 (Small Chest, Thebes: The Big Olive, Left Trinity Shield Path)",
                    "Small Chest 26" => "Chest 17 (Small Chest, Thebes: The Big Olive, Platform Above the Lava Pit)",
                    "Small Chest 27" => "Chest 18 (Small Chest, Thebes: Gardens, Break the Statue's Right Foot)",
                    "Small Chest 28" => "Chest 19 (Small Chest, Thebes: Overlook, Below Statue with Shield)",
                    "Small Chest 29" => "Chest 20 (Small Chest, Thebes: Overlook, End of Left Trinity Shield Path)",
                    "Small Chest 30" => "Chest 21 (Small Chest, Thebes: Gardens, Near Save Point)",
                    "Small Chest 31" => "Chest 22 (Small Chest, Thebes: Overlook, Northwest of Statue with Shield)",
                    "Small Chest 33" => "Chest 23 (Small Chest, Thebes: Overlook, Before Crossing the Roofs)",
                    _ => "",
                },
                DataTableEnum.TreasureTT => chest switch
                {
                    "Large Chest 1" => "Chest 1 (Large Chest, The Neighborhood: Tram Common, Near Gummi Shop)",
                    "Small Chest 1" => "Chest 2 (Small Chest, The Neighborhood: Tram Common, Near Theater)",
                    "Small Chest 2" => "Chest 3 (Small Chest, The Neighborhood: Tram Common, Underground Conduit Entrance)",
                    "Small Chest 3" => "Chest 4 (Small Chest, The Neighborhood: Tram Common, Rooftop)",
                    "Small Chest 4" => "Chest 5 (Small Chest, The Neighborhood: Tram Common, Near Le Grand Bistrot)",
                    "Small Chest 5" => "Chest 6 (Small Chest, Underground Conduit, Near The Woods Entrance)",
                    "Small Chest 6" => "Chest 7 (Small Chest, The Woods, On Northwest Log)",
                    "Small Chest 7" => "Chest 8 (Small Chest, The Woods, Inside Southwest Log)",
                    "Small Chest 8" => "Chest 9 (Small Chest, The Woods, Under Northeast Log)",
                    "Small Chest 9" => "Chest 10 (Small Chest, The Old Mansion, Right of Mansion Entrance)",
                    _ => "",
                },
                DataTableEnum.TreasureTS => chest switch
                {
                    "Large Chest 1" => "Chest 1 (Large Chest, Andy's House, Near Save Point)",
                    "Large Chest 2" => "Chest 2 (Large Chest, Andy's House, Inside the Red Tool Box)",
                    "Large Chest 3" => "Chest 5 (Large Chest, Galaxy Toys: Main Floor 1F, Inside Space Capsule)",
                    "Large Chest 4" => "Chest 13 (Large Chest, Galaxy Toys: Lower Vents, West of Save Point)",
                    "Large Chest 5" => "Chest 24 (Large Chest, Galaxy Toys: Kid Korral, Tunnel Playground: Intersection)",
                    "Large Chest 6" => "Chest 29 (Large Chest, Galaxy Toys: Main Floor: 3F, East of Green Rail)",
                    "Small Chest 1" => "Chest 3 (Small Chest, Andy's House, On Northeast Shelf)",
                    "Small Chest 2" => "Chest 4 (Small Chest, Andy's House, Under the Bed)",
                    "Small Chest 3" => "Chest 6 (Small Chest, Galaxy Toys: Main Floor: 2F, Above Galaxy Toys Entrance)",
                    "Small Chest 4" => "Chest 7 (Small Chest, Galaxy Toys: Main Floor: 3F, North of Green Rail)",
                    "Small Chest 5" => "Chest 8 (Small Chest, Galaxy Toys: Action Figures, Win Rumblin' Time Minigame)",
                    "Small Chest 6" => "Chest 9 (Small Chest, Galaxy Toys: Action Figures, Southwest of Dissidia Figures)",
                    "Small Chest 7" => "Chest 10 (Small Chest, Galaxy Toys: Action Figures, Near Customer Service)",
                    "Small Chest 8" => "Chest 11 (Small Chest, Galaxy Toys: Lower Vents, Near Lucky Emblem)",
                    "Small Chest 9" => "Chest 12 (Small Chest, Galaxy Toys: Lower Vents, Upper Level)",
                    "Small Chest 11" => "Chest 14 (Small Chest, Babies and Toddlers: Dolls, Near Upper Vents Entrance)",
                    "Small Chest 12" => "Chest 15 (Small Chest, Babies and Toddlers: Dolls, On Julia the Dress Queen Shelf)",
                    "Small Chest 13" => "Chest 16 (Small Chest, Babies and Toddlers: Dolls, On Patchwork Animals Shelf)",
                    "Small Chest 14" => "Chest 17 (Small Chest, Babies and Toddlers: Dolls, Inside My Little House)",
                    "Small Chest 15" => "Chest 18 (Small Chest, Babies and Toddlers: Outdoors, Near Kid Korral Upper Entrance)",
                    "Small Chest 16" => "Chest 19 (Small Chest, Babies and Toddlers: Outdoors, Behind Wood Building Blocks)",
                    "Small Chest 17" => "Chest 20 (Small Chest, Galaxy Toys: Video Games, Inside Checkout Counter)",
                    "Small Chest 18" => "Chest 21 (Small Chest, Galaxy Toys: Kid Korral, Climbing Playground: Blue Level)",
                    "Small Chest 19" => "Chest 22 (Small Chest, Galaxy Toys: Kid Korral, Climbing Playground: Yellow Level)",
                    "Small Chest 20" => "Chest 23 (Small Chest, Galaxy Toys: Kid Korral, Behind Dice Blocks East of Ball Pit)",
                    "Small Chest 22" => "Chest 25 (Small Chest, Galaxy Toys: Kid Korral, Inside Ball Pit Area - North)",
                    "Small Chest 23" => "Chest 26 (Small Chest, Galaxy Toys: Rest Area, Top of Drink Factory)",
                    "Small Chest 24" => "Chest 27 (Small Chest, Galaxy Toys: Main Floor 1F, Back of Delivery Service Counter)",
                    "Small Chest 25" => "Chest 28 (Small Chest, Babies and Toddlers: Outdoors, Inside Slide Set)",
                    _ => "",
                },
                DataTableEnum.TreasureRA => chest switch
                {
                    "Large Chest 1" => "Chest 7 (Large Chest, The Forest: Hills, Starting Area Up Against Tree)",
                    "Large Chest 2" => "Chest 10 (Large Chest, The Forest: Marsh, West of Save Point Up Against Tree)",
                    "Large Chest 3" => "Chest 9 (Large Chest, The Forest: Hills, Inside Cave)",
                    "Large Chest 4" => "Chest 22 (Large Chest, The Forest: Wildflower Clearing, Northwest of Save Point)",
                    "Large Chest 5" => "Chest 24 (Large Chest, The Kingdom: Thoroughfare, Perform on Barrel Near Save Point)",
                    "Large Chest 6" => "Chest 14 (Large Chest, The Forest: Wetlands, Within Breakable Dive Rock)",
                    "Small Chest 1" => "Chest 1 (Small Chest, The Forest: Tower, West of the Tower Near Pond)",
                    "Small Chest 2" => "Chest 2 (Small Chest, The Forest: Hills, Upper-Left Platform Before Marsh)",
                    "Small Chest 3" => "Chest 3 (Small Chest, The Forest: Hills, Upper-Right Platform Before Marsh)",
                    "Small Chest 4" => "Chest 4 (Small Chest, The Forest: Hills, A Gift from Rabbits)",
                    "Small Chest 5" => "Chest 5 (Small Chest, The Forest: Hills, Surrounded by Bushes)",
                    "Small Chest 6" => "Chest 6 (Small Chest, The Forest: Hills, Near Save Point Behind Log)",
                    "Small Chest 7" => "Chest 8 (Small Chest, The Forest: Hills, Across the Pond)",
                    "Small Chest 8" => "Chest 11 (Small Chest, The Forest: Marsh, Platform Southeast of Lucky Emblem)",
                    "Small Chest 9" => "Chest 12 (Small Chest, The Forest: Marsh, Within the Southwest River)",
                    "Small Chest 10" => "Chest 13 (Small Chest, The Forest: Wetlands, North Platform After Chaos Carriage)",
                    "Small Chest 11" => "Chest 15 (Small Chest, The Forest: Wetlands, High Platform South of Dive Rock)",
                    "Small Chest 12" => "Chest 16 (Small Chest, The Forest: Wetlands, Platform Near Wall Inside Cave)",
                    "Small Chest 13" => "Chest 17 (Small Chest, The Forest: Wetlands, Southwest Corner Inside Cave)",
                    "Small Chest 14" => "Chest 18 (Small Chest, The Forest: Wetlands, West of Campsite Save Point)",
                    "Small Chest 15" => "Chest 19 (Small Chest, The Forest: Campsite, Platform Before Save Point)",
                    "Small Chest 16" => "Chest 20 (Small Chest, The Forest: Shore, South of Bridge)",
                    "Small Chest 17" => "Chest 21 (Small Chest, The Forest: Wildflower Clearing, Hill West of Save Point)",
                    "Small Chest 18" => "Chest 23 (Small Chest, The Kingdom: Thoroughfare, Festival Dance Area)",
                    "Small Chest 19" => "Chest 25 (Small Chest, The Kingdom: Thoroughfare, Next to Wall Near the Save Point)",
                    "Small Chest 20" => "Chest 26 (Small Chest, The Kingdom: Wharf, End of East Dock)",
                    "Small Chest 21" => "Chest 27 (Small Chest, The Kingdom: Wharf, Top of the Mini-Tower)",
                    "Small Chest 22" => "Chest 28 (Small Chest, The Kingdom: Wharf, Hidden in Boxes and Barrels)",
                    _ => "",
                },
                DataTableEnum.TreasureMI => chest switch
                {
                    "Large Chest 1" => "Chest 1 (Large Chest, Monsters, Inc.: Lobby & Offices, Before Save Point)",
                    "Large Chest 2" => "Chest 9 (Large Chest, The Factory: Basement, Near Save Point)",
                    "Large Chest 3" => "Chest 19 (Large Chest, The Power Plant: Tank Yard, Within Breakable Dive Box)",
                    "Large Chest 4" => "Chest 10 (Large Chest, The Factory: Ground Floor, Electrical Path from Second Floor)",
                    "Small Chest 1" => "Chest 2 (Small Chest, Monsters, Inc.: Lobby & Offices, Behind Canisters)",
                    "Small Chest 2" => "Chest 3 (Small Chest, Monsters, Inc.: Laugh Floor, Behind Counter at Entrance)",
                    "Small Chest 3" => "Chest 4 (Small Chest, Monsters, Inc.: Laugh Floor, Back-Left Corner)",
                    "Small Chest 5" => "Chest 11 (Small Chest, The Factory: Ground Floor, Behind Boxes Next to Door E6)",
                    "Small Chest 6" => "Chest 12 (Small Chest, The Factory: Ground Floor, Canister Platform Near Save Point)",
                    "Small Chest 7" => "Chest 13 (Small Chest, The Factory: Second Floor, Middle of Lasers Path)",
                    "Small Chest 8" => "Chest 14 (Small Chest, The Factory: Second Floor, Northwest Canister Platform)",
                    "Small Chest 9" => "Chest 15 (Small Chest, The Power Plant: Accessway, Near Moogle Shop)",
                    "Small Chest 10" => "Chest 16 (Small Chest, The Power Plant: Accessway, Forced Fight Area)",
                    "Small Chest 11" => "Chest 17 (Small Chest, The Power Plant: Accessway, Before Circluar Ramp)",
                    "Small Chest 12" => "Chest 18 (Small Chest, The Power Plant: Tank Yard, Platform Near Save Point)",
                    "Small Chest 13" => "Chest 20 (Small Chest, The Power Plant: Vault Passage, Under Stairs)",
                    "Small Chest 14" => "Chest 21 (Small Chest, The Power Plant: Vault Passage, Behind Flames)",
                    "Small Chest 15" => "Chest 22 (Small Chest, The Power Plant: Accessway, Rest Area Before Bridge)",
                    "Small Chest 16" => "Chest 5 (Small Chest, Monsters, Inc.: Upper Level, Across from Banana Flan)",
                    "Small Chest 17" => "Chest 6 (Small Chest, Monsters, Inc.: Lower Level, Near Many Doors)",
                    "Small Chest 18" => "Chest 7 (Small Chest, The Door Vault: Service Area, Right of Stairs)",
                    "Small Chest 19" => "Chest 8 (Small Chest, The Door Vault: Service Area, Elevator Post World Visit)",
                    _ => "",
                },
                DataTableEnum.TreasureFZ => chest switch
                {
                    "Large Chest 1" => "Chest 1 (Large Chest, The North Mountain: Treescape, Near Moogle Shop)",
                    "Large Chest 2" => "Chest 10 (Large Chest, The Labyrinth of Ice: Middle Tier, Secret Passage Behind Ice Wall)",
                    "Large Chest 3" => "Chest 8 (Large Chest, The North Mountain: Treescape, Within Breakable Dive Icicle)",
                    "Large Chest 4" => "Chest 7 (Large Chest, The North Mountain: Snowfield, On a Snowy Rock)",
                    "Large Chest 5" => "Chest 14 (Large Chest, The Labyrinth of Ice: Lower Tier, Hidden Alcove Close to Save Point)",
                    "Large Chest 6" => "Chest 9 (Large Chest, The North Mountain: Snowfield, Within Breakable Dive Icicle)",
                    "Small Chest 1" => "Chest 2 (Small Chest, The North Mountain: Treescape, Snowy Rock Behind Trees)",
                    "Small Chest 2" => "Chest 3 (Small Chest, The North Mountain: Treescape, Behind Snowy Bushes)",
                    "Small Chest 4" => "Chest 4 (Small Chest, The North Mountain: Gorge, High Platform Near Icicles)",
                    "Small Chest 5" => "Chest 5 (Small Chest, The North Mountain: Gorge, East Alcove with Popcats)",
                    "Small Chest 7" => "Chest 6 (Small Chest, The North Mountain: Snowfield, East Path After Save Point)",
                    "Small Chest 11" => "Chest 11 (Small Chest, The Labyrinth of Ice: Upper Tier, Left of Exit Portal)",
                    "Small Chest 12" => "Chest 12 (Small Chest, The Labyrinth of Ice: Middle Tier, High Platform at East Corner)",
                    "Small Chest 13" => "Chest 13 (Small Chest, The Labyrinth of Ice: Upper Tier, Right of Exit Portal)",
                    "Small Chest 15" => "Chest 15 (Small Chest, The Labyrinth of Ice: Middle Tier, End of Hidden Rail Gauntlet Route)",
                    "Small Chest 16" => "Chest 16 (Small Chest, The North Mountain: Valley of Ice, Upper-Right Path Near Save Point)",
                    "Small Chest 17" => "Chest 17 (Small Chest, The North Mountain: Valley of Ice, Across from the Ice Wall)",
                    "Small Chest 19" => "Chest 18 (Small Chest, The North Mountain: Valley of Ice, Past the Trees After Helping Olaf)",
                    "Small Chest 20" => "Chest 19 (Small Chest, The North Mountain: Valley of Ice, East Upper Platform)",
                    "Small Chest 22" => "Chest 20 (Small Chest, The North Mountain: Valley of Ice, Very High Southwest Platform)",
                    "Small Chest 23" => "Chest 21 (Small Chest, The North Mountain: Frozen Wall, South of 1st Superjump)",
                    "Small Chest 24" => "Chest 22 (Small Chest, The North Mountain: Frozen Wall, Hidden Northwest Alcove)",
                    "Small Chest 25" => "Chest 23 (Small Chest, The North Mountain: Frozen Wall, Very High Ledge East of Exit)",
                    "Small Chest 27" => "Chest 24 (Small Chest, The North Mountain: Foothills, High Platform Near Lucky Emblem)",
                    "Small Chest 29" => "Chest 25 (Small Chest, The North Mountain: Foothills, Last Southwest Corner)",
                    _ => "",
                },
                DataTableEnum.TreasureCA => chest switch
                {
                    "Large Chest 1" => "Chest 51 (Large Chest, Port Royal: Docks, North Side Near Docks)",
                    "Large Chest 2" => "Chest 17 (Large Chest, Isla de los Mastiles, Inside Cave)",
                    "Large Chest 3" => "Chest 18 (Large Chest, Ship's End, On a Sunken Ship)",
                    "Large Chest 4" => "Chest 19 (Large Chest, Sandbar Isle, On Left Island Surrounded by Water)",
                    "Large Chest 5" => "Chest 20 (Large Chest, Huddled Isles, Near Save Point)",
                    "Large Chest 6" => "Chest 47 (Large Chest, Port Royal: Fort, In the Prison Cell)",
                    "Large Chest 7" => "Chest 4 (Large Chest, Huddled Isles: Undersea Cavern, 4th Dead End)",
                    "Large Chest 8" => "Chest 10 (Large Chest, Isla Verdemontana, Within Breakable Dive Box)",
                    "Large Chest 9" => "Chest 13 (Large Chest, Confinement Island, Ambush Alcove)",
                    "Large Chest 10" => "Chest 15 (Large Chest, Huddled Isles: Undersea Cavern, Lightning Angler's Lair)",
                    "Large Chest 11" => "Chest 6 (Large Chest, Isla de los Mastiles, Within Breakable Dive Box)",
                    "Small Chest 1" => "Chest 46 (Small Chest, Port Royal: Fort, Northeast Side of Rampart)",
                    "Small Chest 2" => "Chest 48 (Small Chest, Port Royal: Seaport, Behind the Waterfall)",
                    "Small Chest 3" => "Chest 49 (Small Chest, Port Royal: Seaport, North Dock Near Water)",
                    "Small Chest 4" => "Chest 50 (Small Chest, Port Royal: Seaport, Under the Bridge Near Settlement)",
                    "Small Chest 5" => "Chest 52 (Small Chest, Port Royal: Settlement, South Side On Roof)",
                    "Small Chest 6" => "Chest 53 (Small Chest, Port Royal: Docks, Southwest Corner Near Building)",
                    "Small Chest 7" => "Chest 54 (Small Chest, Port Royal: Underwater, In a Broken Ship)",
                    "Small Chest 8" => "Chest 55 (Small Chest, Port Royal: Settlement, West Side Behind Boxes)",
                    "Small Chest 9" => "Chest 56 (Small Chest, Port Royal: Seaport, Behind the Waterfall)",
                    "Small Chest 10" => "Chest 1 (Small Chest, Huddled Isles, Within a Sunken Ship)",
                    "Small Chest 11" => "Chest 2 (Small Chest, Huddled Isles: Undersea Cavern, 2nd Dead End)",
                    "Small Chest 12" => "Chest 3 (Small Chest, Huddled Isles: Undersea Cavern, 3rd Dead End)",
                    "Small Chest 13" => "Chest 5 (Small Chest, Isla de los Mastiles: Underwater Alcove)",
                    "Small Chest 14" => "Chest 7 (Small Chest, Ship's End, Within a Sunken Ship)",
                    "Small Chest 15" => "Chest 8 (Small Chest, Ship's End, Within a Sunken Ship)",
                    "Small Chest 16" => "Chest 9 (Small Chest, Isla Verdemontana, Top of 1st Cliff)",
                    "Small Chest 17" => "Chest 11 (Small Chest, Sandbar Isle: Underwater Alcove)",
                    "Small Chest 18" => "Chest 12 (Small Chest, Exile Island)",
                    "Small Chest 19" => "Chest 14 (Small Chest, The Gateway of Regret)",
                    "Small Chest 20" => "Chest 16 (Small Chest, Horseshoe Island)",
                    "Small Chest 21" => "Chest 21 (Small Chest, Huddled Isles, Within a Sunken Ship)",
                    "Small Chest 22" => "Chest 22 (Small Chest, Huddled Isles, Within a Sunken Ship)",
                    "Small Chest 23" => "Chest 23 (Small Chest, Isla de los Mastiles: Underwater Alcove)",
                    "Small Chest 24" => "Chest 24 (Small Chest, Isla de los Mastiles: Underwater Alcove)",
                    "Small Chest 25" => "Chest 45 (Small Chest, The Leviathan)",
                    "Small Chest 26" => "Chest 25 (Small Chest, Isla Verdemontana, Top of 2nd Cliff)",
                    "Small Chest 27" => "Chest 26 (Small Chest, Sandbar Isle: Underwater, North Side)",
                    "Small Chest 28" => "Chest 27 (Small Chest, Sandbar Isle, On Right Island Surrounded by Water)",
                    "Small Chest 29" => "Chest 28 (Small Chest, Sandbar Isle: Underwater, South Side Near Seaweed)",
                    "Small Chest 30" => "Chest 29 (Small Chest, Sandbar Isle: Underwater, South Side Near Broken Box)",
                    "Small Chest 31" => "Chest 30 (Small Chest, Sandbar Isle: Underwater, South Side On Rock Near Surface)",
                    "Small Chest 32" => "Chest 31 (Small Chest, Sandbar Isle: Underwater Alcove, Inside the Lucky Emblem)",
                    "Small Chest 33" => "Chest 32 (Small Chest, Sandbar Isle: Underwater Alcove, Inside the Lucky Emblem)",
                    "Small Chest 34" => "Chest 33 (Small Chest, Sandbar Isle: Underwater Alcove)",
                    "Small Chest 35" => "Chest 34 (Small Chest, Sandbar Isle: Underwater Alcove)",
                    "Small Chest 36" => "Chest 35 (Small Chest, Sandbar Isle: Underwater Alcove)",
                    "Small Chest 37" => "Chest 36 (Small Chest, Sandbar Isle: Underwater Alcove)",
                    "Small Chest 38" => "Chest 37 (Small Chest, Sandbar Isle: Underwater Alcove)",
                    "Small Chest 39" => "Chest 38 (Small Chest, Sandbar Isle: Underwater Alcove)",
                    "Small Chest 40" => "Chest 39 (Small Chest, Sandbar Isle: Underwater Alcove)",
                    "Small Chest 41" => "Chest 40 (Small Chest, Sandbar Isle: Underwater Alcove)",
                    "Small Chest 42" => "Chest 41 (Small Chest, Horseshoe Island, Behind Rocks)",
                    "Small Chest 43" => "Chest 42 (Small Chest, Horseshoe Island)",
                    "Small Chest 44" => "Chest 43 (Small Chest, Horseshoe Island)",
                    "Small Chest 45" => "Chest 44 (Small Chest, Horseshoe Island)",
                    _ => "",
                },
                DataTableEnum.TreasureBX => chest switch
                {
                    "Large Chest 1" => "Chest 3 (Large Chest, The City: South District, On Sidewalk North of M Building)",
                    "Large Chest 5" => "Chest 34 (Large Chest, The City: Central District, On the Red Circle Platform)",
                    "Large Chest 6" => "Chest 35 (Large Chest, The City: South District, On Top of M Building)",
                    "Large Chest 7" => "Chest 36 (Large Chest, The City: Central District, Within Breakable Dive Object)",
                    "Small Chest 1" => "Chest 1 (Small Chest, The City: Central District, Top of East Building Under Platform)",
                    "Small Chest 2" => "Chest 2 (Small Chest, The City: South District, Southeast Building Roof, South Side)",
                    "Small Chest 3" => "Chest 4 (Small Chest, The City: South District, Underneath West Ramp to Highway)",
                    "Small Chest 4" => "Chest 5 (Small Chest, The City: North District, South of Monorail Behind Boxes)",
                    "Small Chest 5" => "Chest 6 (Small Chest, The City: North District), Northeast Building Lower Platform",
                    "Small Chest 6" => "Chest 7 (Small Chest, The City: South District, On Sidewalk East of M Building)",
                    "Small Chest 7" => "Chest 8 (Small Chest, The City: Central District, Between the Air Vents)",
                    "Small Chest 8" => "Chest 9 (Small Chest, The City: Central District, Upper Level of Blue Circle Building)",
                    "Small Chest 9" => "Chest 10 (Small Chest, The City: North District, South of Monorail East Side)",
                    "Small Chest 10" => "Chest 11 (Small Chest, The City: South District, On Top of Triple Billboards Building)",
                    "Small Chest 11" => "Chest 12 (Small Chest, The City: South District, On Top of Triple Billboards Building)",
                    "Small Chest 12" => "Chest 13 (Small Chest, The City: North District, Underneath Monorail West Side)",
                    "Small Chest 13" => "Chest 14 (Small Chest, The City: North District, On Sloped Roof North of Monorail)",
                    "Small Chest 14" => "Chest 15 (Small Chest, The City: North District, On Flat Roof North on Monorail)",
                    "Small Chest 15" => "Chest 16 (Small Chest, The City: Central District, Balcony Underneath West Ramp)",
                    "Small Chest 16" => "Chest 17 (Small Chest, The City: Central District, Next to Green Roofs Building)",
                    "Small Chest 17" => "Chest 18 (Small Chest, The City: South District, On Top of Triple Billboards Building)",
                    "Small Chest 18" => "Chest 19 (Small Chest, The City: North District, Northeast Building On Roof)",
                    "Small Chest 19" => "Chest 20 (Small Chest, The City: South District, Southeast of Save Point)",
                    "Small Chest 20" => "Chest 21 (Small Chest, The City: South District, On Sidewalk at M Building)",
                    "Small Chest 21" => "Chest 22 (Small Chest, The City: North District, Near Save Point in Parking Lot)",
                    "Small Chest 22" => "Chest 23 (Small Chest, The City: Central District, North of Breakable Dive Object)",
                    "Small Chest 23" => "Chest 24 (Small Chest, The City: Central District, Lower Level of Blue Circle Building)",
                    "Small Chest 24" => "Chest 25 (Small Chest, The City: Central District, On Railing Underneath Highway)",
                    "Small Chest 25" => "Chest 26 (Small Chest, The City: Central District, Southeast of Red Circle Platform)",
                    "Small Chest 26" => "Chest 27 (Small Chest, The City: South District, East of Save Point Above Highway)",
                    "Small Chest 27" => "Chest 28 (Small Chest, The City: North District, On Building Railing Above Monorail)",
                    "Small Chest 28" => "Chest 29 (Small Chest, The City: North District, Alleyway Behind Boxes)",
                    "Small Chest 29" => "Chest 30 (Small Chest, The City: Central District, Top of East Building Under Platform)",
                    "Small Chest 30" => "Chest 31 (Small Chest, The City: South District, On Building Southeast of M Building)",
                    "Small Chest 31" => "Chest 32 (Small Chest, The City: North District, Alleyway East of Boxes)",
                    "Small Chest 32" => "Chest 33 (Small Chest, The City: North District, Top of Building Above Monorail)",
                    _ => "",
                },
                DataTableEnum.TreasureKG => chest switch
                {
                    "Large Chest 1" => "Chest 1 (Large Chest, The Badlands, Before Save Point)",
                    "Large Chest 2" => "Chest 3 (Large Chest, The Skein of Severance: Trail of Valediction, Near Save Point)",
                    "Small Chest 1" => "Chest 4 (Small Chest, The Skein of Severance: Trail of Valediction, Path Between 1st Org. XIII Fights)",
                    "Small Chest 2" => "Chest 5 (Small Chest, The Skein of Severance: Trail of Valediction, Switch Floor Maze)",
                    "Small Chest 3" => "Chest 6 (Small Chest, The Skein of Severance: Twist of Isolation, Path Between 2nd Org. XIII Fights)",
                    "Small Chest 4" => "Chest 2 (Small Chest, The Badlands, After Save Point)",
                    _ => "",
                },
                DataTableEnum.TreasureEW => chest switch
                {
                    "Large Chest 1" => "Chest 1 (Large Chest, The Final World, Between Two Portals)",
                    _ => "",
                },
                DataTableEnum.TreasureBT => chest switch
                {
                    "Large Chest 1" => "Chest 1 (Large Chest, The Stairway to the Sky, Near Save Point)",
                    "Large Chest 2" => "Chest 2 (Large Chest, Breezy Quarter, Near Moogle Shop)",
                    "Small Chest 1" => "Chest 3 (Small Chest, Breezy Quarter, Upper Area North of Save Point)",
                    "Small Chest 2" => "Chest 4 (Small Chest, Breezy Quarter, Within the Windmill Tower)",
                    "Small Chest 3" => "Chest 5 (Small Chest, Breezy Quarter, Near the Windmill Tower)",
                    "Small Chest 4" => "Chest 6 (Small Chest, Breezy Quarter, Waterway Below Battle Arena)",
                    "Small Chest 5" => "Chest 7 (Small Chest, Breezy Quarter, South of Fountain Near Stairs)",
                    "Small Chest 6" => "Chest 8 (Small Chest, Breezy Quarter, South of Daybreak Town's History)",
                    "Small Chest 7" => "Chest 9 (Small Chest, Breezy Quarter, Northmost Corner)",
                    _ => "",
                },
                _ => "",
            };
        }

        public static string FoodIdToFoodName(this string food)
        {
            return food switch
            {
                "FOOD_ITEM01" => "Pumpkin Velouté",
                "FOOD_ITEM02" => "Consommé",
                "FOOD_ITEM03" => "Carrot Potage",
                "FOOD_ITEM04" => "Crab Bisque",
                "FOOD_ITEM05" => "Cold Tomato Soup",

                "FOOD_ITEM06" => "Scallop Poêlé",
                "FOOD_ITEM07" => "Lobster Mousse",
                "FOOD_ITEM08" => "Mushroom Terrine",
                "FOOD_ITEM09" => "Ratatouille",
                "FOOD_ITEM10" => "Caprese Salad",

                "FOOD_ITEM11" => "Sole Meunière",
                "FOOD_ITEM12" => "Eel Matelote",
                "FOOD_ITEM13" => "Sea Bass en Papillote",
                "FOOD_ITEM14" => "Bouillabaisse",
                "FOOD_ITEM15" => "Seafood Tartare",
                "FOOD_ITEM16" => "Sea Bass Poêlé",

                "FOOD_ITEM17" => "Sweetbread Poêlé",
                "FOOD_ITEM18" => "Beef Sauté",
                "FOOD_ITEM19" => "Beef Bourguignon",
                "FOOD_ITEM20" => "Stuffed Quail",
                "FOOD_ITEM21" => "Filet Mignon Poêlé",

                "FOOD_ITEM22" => "Crêpes Suzette",
                "FOOD_ITEM23" => "Chocolate Mousse",
                "FOOD_ITEM24" => "Fresh Fruit Compote",
                "FOOD_ITEM25" => "Berries au Fromage",
                "FOOD_ITEM26" => "Warm Banana Soufflé",
                "FOOD_ITEM27" => "Fruit Gelée",
                "FOOD_ITEM28" => "Tarte aux Fruits",


                "FOOD_ITEM29" => "Pumpkin Velouté+",
                "FOOD_ITEM30" => "Consommé+",
                "FOOD_ITEM31" => "Carrot Potage+",
                "FOOD_ITEM32" => "Crab Bisque+",
                "FOOD_ITEM33" => "Cold Tomato Soup+",

                "FOOD_ITEM34" => "Scallop Poêlé+",
                "FOOD_ITEM35" => "Lobster Mousse+",
                "FOOD_ITEM36" => "Mushroom Terrine+",
                "FOOD_ITEM37" => "Ratatouille+",
                "FOOD_ITEM38" => "Caprese Salad+",

                "FOOD_ITEM39" => "Sole Meunière+",
                "FOOD_ITEM40" => "Eel Matelote+",
                "FOOD_ITEM41" => "Sea Bass en Papillote+",
                "FOOD_ITEM42" => "Bouillabaisse+",
                "FOOD_ITEM43" => "Seafood Tartare+",
                "FOOD_ITEM44" => "Sea Bass Poêlé+",

                "FOOD_ITEM45" => "Sweetbread Poêlé+",
                "FOOD_ITEM46" => "Beef Sauté+",
                "FOOD_ITEM47" => "Beef Bourguignon+",
                "FOOD_ITEM48" => "Stuffed Quail+",
                "FOOD_ITEM49" => "Filet Mignon Poêlé+",

                "FOOD_ITEM50" => "Crêpes Suzette+",
                "FOOD_ITEM51" => "Chocolate Mousse+",
                "FOOD_ITEM52" => "Fresh Fruit Compote+",
                "FOOD_ITEM53" => "Berries au Fromage+",
                "FOOD_ITEM54" => "Warm Banana Soufflé+",
                "FOOD_ITEM55" => "Fruit Gelée+",
                "FOOD_ITEM56" => "Tarte aux Fruits+",

                _ => food
            };
        }

        public static string AccessoryIdToAccessoryName(this string accessory)
        {
            return accessory switch
            {
                "" => "",
                _ => accessory
            };
        }

        public static string WorldPrefixToWorldName(this string prefix)
        {
            return prefix switch
            {
                "HE" => "Olympus",
                "TT" => "Twilight Town",
                "TS" => "Toy Box",
                "RA" => "Kingdom of Corona",
                "MI" => "Monstropolis",
                "FZ" => "Arendelle",
                "BX" => "San Fransokyo",
                "CA" => "The Caribbean",
                "KG" => "Keyblade Graveyard",
                "DW" => "Dark World",
                "EW" => "The Final World",
                "EX" => "Battle Portal",
                "BT" => "Scala ad Caelum",
                "SS" => "Unreality",
                _ => prefix
            };
        }

        public static string WorldNameToWorldPrefix(this string prefix)
        {
            return prefix switch
            {
                "Olympus" => "HE",
                "Twilight Town" => "TT",
                "Toy Box" => "TS",
                "Kingdom of Corona" => "RA",
                "Monstropolis" => "MI",
                "Arendelle" => "FZ",
                "San Fransokyo" => "BX",
                "The Caribbean" => "CA",
                "Keyblade Graveyard" => "KG",
                "Dark World" => "DW",
                "The Final World" => "EW",
                "Battle Portal" => "EX",
                "Re+Mind" => "BT",
                "Unreality" => "SS",
                _ => prefix
            };
        }

        public static string LevelIdToLevelName(this string level)
        {
            return level.ToLower() switch
            {
                "he_01" => "Realm of the Gods",
                "he_02" => "Mount of Olympus",
                "he_03" => "Ruined Thebes",
                "he_04" => "Restored Thebes",
                "he_05" => "Ice & Lava Titan Arena",
                "he_06" => "Tornado Titan Arena",

                "tt_01" => "The Neighborhood",

                "ts_01" => "Andy's House",
                "ts_02" => "Galaxy Toys",
                "ts_03" => "Verum Rex: Beat of Lead",
                "ts_04" => "King of Toys Arena",

                "ra_01" => "The Forest",
                "ra_02" => "The Kingdom",

                "mi_01" => "Monsters Inc.",
                "mi_02" => "The Factory",
                "mi_03" => "The Power Plant",
                "mi_04" => "The Door Vault",

                "fz_01" => "The North Mountain",
                "fz_02" => "The Labyrinth of Ice",
                "fz_03" => "Down the Mountain",
                "fz_04" => "The North Mountain",
                "fz_06" => "Skoll Arena",

                "bx_01" => "The Bridge",
                "bx_02" => "The City",

                "ca_01" => "Port Royal",
                "ca_02" => "The High Seas",
                "ca_03" => "Shipwreck Cove",
                "ca_04" => "Davy Jones' Locker",

                "dw_21" or "dw_22" => "The Realm of Darkness",

                "kg_01" => "The Badlands",
                "kg_02" or "kg_05" => "The Skein of Severance",
                "kg_04" => "Dark Inferno Arena",
                "kg_06" => "Replica Xehanort Arena",
                "kg_08" => "Terra-Xehanort Arena",

                "ew_01" => "Tutorial Arena",
                "ew_21" => "Dark Olympus",
                "ew_22" => "Dark Kingdom of Corona",
                "ew_23" => "Dark Monstropolis",
                "ew_24" => "Dark Toy Box",
                "ew_25" => "Dark Arendelle",
                "ew_26" => "Dark Caribbean",
                "ew_27" => "Dark San Fransokyo",

                "bt_01" or "bt_08" => "Stairway to the Sky",
                "bt_02" or "bt_03" or "bt_04" => "Armored Xehanort Arena",
                "bt_05" => "Master Xehanort Arena",
                "bt_07" => "Breezy Quarter",

                "ex_21" or "ex_22" => "Olympus",
                "ex_23" => "Twilight Town",
                "ex_24" or "ex_25" => "Toy Box",
                "ex_26" or "ex_27" => "Kingdom of Corona",
                "ex_29" => "Monstropolis",
                "ex_31" => "Arendelle",
                "ex_33" => "The Caribbean",
                "ex_34" or "ex_35" => "San Fransokyo",
                "ex_37" or "ex_38" => "Keyblade Graveyard",
                "ex_39" => "Dark World",

                "rg_10" => "Master Xehanort Arena",
                "rg_11" => "Ansem Arena",
                "rg_12" => "Xemnas Arena",
                "rg_13" => "Xigbar Arena",
                "rg_14" => "Luxord Arena",
                "rg_15" => "Larxene Arena",
                "rg_16" => "Marluxia Arena",
                "rg_17" => "Saïx Arena",
                "rg_18" => "Terra-Xehanort Arena",
                "rg_19" => "Dark Riku Arena",
                "rg_20" => "Vanitas Arena",
                "rg_21" => "Young Xehanort Arena",
                "rg_22" => "Xion Arena",

                "ss_01" => "Unreality",

                _ => level
            };
        }

        public static string EnemyIdToEnemyName(this string enemy)
        {
            var subStringEnemy = enemy.Substring(0, 7);
            return subStringEnemy switch
            {
                "e_ex001" => "Shadow",
                "e_bx001" => "Shadow (Big Hero 6)",
                "e_ex002" => "Large Body",
                "e_ca002" => "Large Body (Caribbean)",
                "e_ex003" => "Flame Core",
                "e_ex004" => "Water Core",
                "e_ca004" => "Water Core (Caribbean)",
                "e_ex005" => "Earth Core",
                "e_ca005" => "Earth Core (Caribbean)",
                "e_ex006" => "Satyr",
                "e_ex007" => "Bizarre Archer",
                "e_ex009" => "Soldier",
                "e_bx009" => "Soldier (Big Hero 6)",
                "e_ex010" => "Air Soldier",
                "e_ex011" => "Chaos Carriage",
                "e_ex013" => "Chief Puff",
                "e_ex014" => "Puffball",
                "e_ex015" => "Toy Trooper",
                "e_ex016" => "Neoshadow",
                "e_bx016" => "Neoshadow (Big Hero 6)",
                "e_ex017" => "Vermilion Samba",
                "e_ex018" => "Gigas A",
                "e_ex020" => "Marionette",
                "e_ex026" => "Vaporfly",
                "e_ex028" => "Sea Sprite",
                "e_ex032" => "Gigas B",
                "e_ex033" => "Gigas C",
                "e_ex035" => "Powerwild",
                "e_ca035" => "Powerwild (Caribbean)",
                "e_ex036" => "Marine Rumba",
                "e_ca036" => "Marine Rumba (Caribbean)",
                "e_ex037" => "Gold Beat",
                "e_bx037" => "Gold Beat (Big Hero 6)",
                "e_ca037" => "Gold Beat (Caribbean)",
                "e_ex038" => "Malachite Bolero",
                "e_ca038" => "Malachite Bolero (Caribbean)",
                "e_ex039" => "Parasol Beauty",
                "e_ex044" => "Pole Cannon",
                "e_bx044" => "Pole Cannon (Big Hero 6)",
                "e_ex045" => "Winterhorn",
                "e_ex048" => "Tireblade",
                "e_bx048" => "Tireblade (Big Hero 6)",
                "e_ex061" => "Popcat",
                "e_ex065" => "Vitality Popcat",
                "e_ex066" => "Magic Popcat",
                "e_ex067" => "Focus Popcat",
                "e_ex068" => "Munny Popcat",
                "e_ex072" => "Spear Lizard",
                "e_ex073" => "Pogo Shovel",
                "e_ex081" => "Fluttering",
                "e_ex093" => "Mechanitaur",
                "e_ex094" => "High Soldier",
                "e_ex095" => "Helmed Body",
                "e_ex101" => "Dusk",
                "e_ex106" => "Sniper",
                "e_ex110" => "Gambler",
                "e_ca110" => "Gambler",
                "e_ex113" => "Reaper",
                "e_ex114" => "Ninja",
                "e_ex201" => "Flood",
                "e_ex202" => "Flowersnake",
                "e_ex205" => "TurtleToad",
                "e_ex830" => "Patchwork Panda",
                "e_ex831" => "Patchwork Lion",

                "e_ex041" => "Spiked Sandworm",
                "e_ca041" => "Spiked Sandworm (Caribbean)",
                "e_ex042" => "Anchor Raider",
                "e_ex046" => "(Flying) Frost Serpent",
                "e_ex059" => "Rock Troll",
                "e_bx059" => "Metal Troll",
                "e_ex082" => "Frost Serpent",
                "e_ex105" => "Sorcerer",
                "e_ex107" => "Berserker",
                "e_ex203" => "Spiked Turtletoad",
                "e_ex801" => "Supreme Smasher",

                "e_ex021" => "Raging Vulture",
                "e_ex027" => "Lightning Angler",
                "e_ex043" => "Dark Inferno",
                "e_ex047" => "Lich",
                "e_ca047" => "Lich",
                "e_ex054" => "Catastrochorus",
                "e_ex301" => "Master Xehanort",
                "e_ex302" => "Young Xehanort",
                "e_ex304" => "Xemnas",
                "e_ex305" => "Xigbar",
                "e_ex306" => "Saïx",
                "e_ex307" => "Luxord",
                "e_ex308" => "Marluxia",
                "e_ex309" => "Larxene",
                "e_ex310" => "Xion",
                "e_ex311" => "Vanitas",
                "e_ex313" => "Dark Riku",
                "e_ex316" => "Ansem",
                "e_ex322" => "Anti-Aqua",
                "e_ex325" => "Terra-Xehanort",
                "e_ex351" => "Data Young Xehanort",
                "e_ex352" => "Data Ansem",
                "e_ex353" => "Data Xemnas",
                "e_ex354" => "Data Xigbar",
                "e_ex355" => "Data Saïx",
                "e_ex356" => "Data Luxord",
                "e_ex357" => "Data Marluxia",
                "e_ex358" => "Data Larxene",
                "e_ex359" => "Data Xion",
                "e_ex360" => "Data Vanitas",
                "e_ex361" => "Data Dark Riku",
                "e_ex362" => "Data Terra-Xehanort",
                "e_ex363" => "Data Master Xehanort",
                "e_ex407" => "Darkside",
                "e_ex409" => "Kairi's Darkside",
                "e_ex701" => "Lump of Horror",
                "e_ex711" => "Lump of Horror",
                "e_ex721" => "Grim Guardianess",
                "e_ex731" => "Skoll",
                "e_ex751" => "Dark Inferno X",
                "e_ex761" => "Xehanort Replica Vessel",
                "e_ex771" => "Armored Xehanort",
                "e_ex773" => "Replica Xehanort",
                "e_ex781" => "Yozora",
                "e_ex816" => "Angelic Amber",
                "e_fz903" => "Marshmallow",
                "e_he001" => "Rock Titan",
                "e_he902" => "Ice Titan",
                "e_he903" => "Tornado Titan",
                "e_he904" => "Lava Titan",
                "e_bx901" => "Dark Baymax",
                "e_ca901" => "Davy Jones",
                "e_dw401" => "Demon Tide",
                "e_dw402" => "Demon Tower",
                "e_dw407" => "Darkside",

                _ => enemy
            };
        }

        public static string BossIdToBossName(this string boss)
        {
            return boss.Replace("\u0000", "") switch
            {
                "BOSS_001" or "e_he001a_Pawn" => "Rock Titan",
                "BOSS_002" or "e_he904_Pawn" => "Lava Titan",
                "BOSS_003" or "e_he902_Pawn" => "Ice Titan",
                "BOSS_004" or "e_he903_Pawn" => "Wind Titan",
                "BOSS_005" or "e_dw401_Pawn_s0" => "Demon Tide (Twilight Town)",
                "BOSS_006" or "e_ex816_Pawn" => "Angelic Amber",
                "BOSS_007" or "e_ex092_Pawn_s0" => "Hyper Ace Gigas",
                "BOSS_008" or "e_ex711_Pawn" => "King of Toys",
                "BOSS_009" or "e_ex721_Pawn" => "Mother Gothel",
                "BOSS_010" or "e_ex701_Pawn" => "Lump of Horror",
                "BOSS_011" or "e_fz903_Pawn" => "Marshmallow",
                "BOSS_012" or "e_ex731_Pawn" => "Skoll",
                "BOSS_013" or "e_ex021_Pawn" => "Raging Vulture",
                "BOSS_014" or "e_ex027_Pawn" => "Electric Fish",
                "BOSS_015" or "e_ca901_Pawn" => "Davy Jones",
                "BOSS_016" or "e_ex054_Pawn" => "Catastrochorus",
                "BOSS_017" or "e_bx903_Pawn" => "Darkubes",
                "BOSS_018" or "e_bx901_Pawn" => "Dark Baymax",
                "BOSS_019" or "e_ex322_Pawn" => "Dark Aqua",
                "BOSS_020" or "e_dw401_Pawn_s1" => "Demon Tide (Keyblade Graveyard)",
                "BOSS_021" or "e_ex313_Pawn" => "Dark Riku",
                "BOSS_022" or "e_ex305_Pawn" => "Xigbar",
                "BOSS_023" or "e_ex309_Pawn" => "Larxene",
                "BOSS_024" or "e_ex308_Pawn" => "Marluxia",
                "BOSS_025" or "e_ex307_Pawn" => "Luxord",
                "BOSS_026" or "e_ex311_Pawn" => "Vanitas",
                "BOSS_027" or "e_ex325_Pawn" => "Terra-Xehanort",
                "BOSS_028" or "e_ex310_Pawn" => "Xion",
                "BOSS_029" or "e_ex306_Pawn_s0" => "Saïx 1",
                "BOSS_030" or "e_ex306_Pawn" => "Saïx 2",
                "BOSS_031" or "e_ex316_Pawn" => "Ansem Seeker of Darkness",
                "BOSS_032" or "e_ex304_Pawn" => "Xemnas",
                "BOSS_033" or "e_ex302_Pawn" => "Young Xehanort",
                "BOSS_034" or "e_ex407_Pawn" => "Darkside (The Final World)",
                "BOSS_035" or "e_ex047_Pawn" => "Lich 1",
                "BOSS_036" or "e_ex047_Pawn_s0" => "Lich 2",
                "BOSS_037" or "e_ex047_Pawn_s2" => "Lich 3",
                "BOSS_038" or "e_ex047_Pawn_s1" => "Lich 4",
                "BOSS_039" or "e_ex047_Pawn_s3" => "Lich 5",
                "BOSS_040" or "e_ca047_Pawn" => "Lich 6",
                "BOSS_041" or "e_ex047_Pawn_s4" => "Lich 7",
                "BOSS_042" or "e_ex761_Pawn_s9" => "Replica Xehanort 1",
                "BOSS_043" or "e_ex761_Pawn_s8" => "Replica Xehanort 2",
                "BOSS_044" or "e_ex761_Pawn_s7" => "Replica Xehanort 3",
                "BOSS_045" or "e_ex761_Pawn_s6" => "Replica Xehanort 4",
                "BOSS_046" or "e_ex761_Pawn_s5" => "Replica Xehanort 5",
                "BOSS_047" or "e_ex761_Pawn_s4" => "Replica Xehanort 6",
                "BOSS_048" or "e_ex761_Pawn_s3" => "Replica Xehanort 7",
                "BOSS_049" or "e_ex761_Pawn_s2" => "Replica Xehanort 8",
                "BOSS_050" or "e_ex761_Pawn_s1" => "Replica Xehanort 9",
                "BOSS_051" or "e_ex761_Pawn_s10" => "Replica Xehanort 10",
                "BOSS_052" or "e_ex761_Pawn_s0" => "Replica Xehanort 11",
                "BOSS_053" or "e_ex761_Pawn" => "Replica Xehanort 12",
                "BOSS_054" or "e_ex771_Pawn" => "Armored Xehanort 1",
                "BOSS_055" or "e_ex771_Pawn_s0" => "Armored Xehanort 2",
                "BOSS_056" or "e_ex771_Pawn_s1" => "Armored Xehanort 3",
                "BOSS_057" or "e_ex301_Pawn_s0" => "Master Xehanort 1",
                "BOSS_058" or "e_ex301_Pawn" => "Master Xehanort 2",
                "BOSS_059" or "e_dw402_Pawn_s200" => "Demon Tower",
                "BOSS_060" or "e_ex054_Pawn_s200" => "Catastrochorus (Battle Portal)",
                "BOSS_061" or "e_ex043_Pawn" => "Dark Inferno",
                "BOSS_062" or "e_ex751_Pawn" => "Dark Inferno X",
                "BOSS_063" or "e_ex313_Pawn_s10" => "Dark Riku (KG DLC)",
                "BOSS_064" or "e_ex305_Pawn_s10" => "Xigbar (KG DLC)",
                "BOSS_065" or "e_ex309_Pawn_s10" => "Larxene (KG DLC)",
                "BOSS_066" or "e_ex308_Pawn_s10" => "Marluxia (KG DLC)",
                "BOSS_067" or "e_ex307_Pawn_s10" => "Luxord (KG DLC)",
                "BOSS_068" or "e_ex325_Pawn_s10" => "Terra-Xehanort (KG DLC)",
                "BOSS_069" or "e_ex311_Pawn_s10" => "Vanitas (KG DLC)",
                "BOSS_070" or "e_ex310_Pawn_s10" => "Xion (KG DLC)",
                "BOSS_071" or "e_ex306_Pawn_s11" => "Saïx 1 (KG DLC)",
                "BOSS_072" or "e_ex306_Pawn_s10" => "Saïx 2 (KG DLC)",
                "BOSS_073" or "e_ex304_Pawn_s11" => "Xemnas (Saïx - KG DLC)",
                "BOSS_074" or "e_ex316_Pawn_s10" => "Ansem Seeker of Darkness (KG DLC)",
                "BOSS_075" or "e_ex304_Pawn_s10" => "Xemnas (KG DLC)",
                "BOSS_076" or "e_ex302_Pawn_s10" => "Young Xehanort (KG DLC)",
                "BOSS_077" or "e_ex773_Pawn" => "Replica Armored Xehanort",
                "BOSS_078" or "e_ex325_Pawn_s0" => "Terra-Xehanort (Lingering Will - KG DLC)",
                "BOSS_079" or "e_ex322_Pawn_s0" => "Dark Aqua (DLC)",
                "BOSS_080" or "e_ex409_Pawn" => "Darkside (Scala ad Caelum)",
                "BOSS_081" or "e_ex761_Pawn_s99" => "Replica Xehanort 1 (DLC)",
                "BOSS_082" or "e_ex761_Pawn_s110" => "Replica Xehanort 2 (DLC)",
                "BOSS_083" or "e_ex761_Pawn_s109" => "Replica Xehanort 3 (DLC)",
                "BOSS_084" or "e_ex761_Pawn_s108" => "Replica Xehanort 4 (DLC)",
                "BOSS_085" or "e_ex761_Pawn_s107" => "Replica Xehanort 5 (DLC)",
                "BOSS_086" or "e_ex761_Pawn_s106" => "Replica Xehanort 6 (DLC)",
                "BOSS_087" or "e_ex761_Pawn_s105" => "Replica Xehanort 7 (DLC)",
                "BOSS_088" or "e_ex761_Pawn_s104" => "Replica Xehanort 8 (DLC)",
                "BOSS_089" or "e_ex761_Pawn_s103" => "Replica Xehanort 9 (DLC)",
                "BOSS_090" or "e_ex761_Pawn_s102" => "Replica Xehanort 10 (DLC)",
                "BOSS_091" or "e_ex761_Pawn_s101" => "Replica Xehanort 11 (DLC)",
                "BOSS_092" or "e_ex761_Pawn_s100" => "Replica Xehanort 12 (DLC)",
                "BOSS_093" or "e_ex367_Pawn" => "Data Master Xehanort",
                "BOSS_094" or "e_ex352_Pawn" => "Data Ansem Seeker of Darkness",
                "BOSS_095" or "e_ex353_Pawn" => "Data Xemnas",
                "BOSS_096" or "e_ex354_Pawn" => "Data Xigbar",
                "BOSS_097" or "e_ex356_Pawn" => "Data Luxord",
                "BOSS_098" or "e_ex358_Pawn" => "Data Larxene",
                "BOSS_099" or "e_ex357_Pawn" => "Data Marluxia",
                "BOSS_0100" or "e_ex355_Pawn" => "Data Saïx",
                "BOSS_0101" or "e_ex362_Pawn" => "Data Terra-Xehanort",
                "BOSS_0102" or "e_ex361_Pawn" => "Data Dark Riku",
                "BOSS_0103" or "e_ex360_Pawn" => "Data Vanitas",
                "BOSS_0104" or "e_ex351_Pawn" => "Data Young Xehanort",
                "BOSS_0105" or "e_ex359_Pawn" => "Data Xion",
                "BOSS_0106" or "e_ex781_Pawn" => "Yozora",

                _ => boss
            };
        }

        public static string PartyIdToPartyName(this string party)
        {
            return party.Replace("\u0000", "") switch
            {
                "PartyMember_001" or "n_ex001" => "Donald",
                "PartyMember_002" or "n_ex002" => "Goofy",
                "PartyMember_003" or "n_he201" => "Hercules",
                "PartyMember_004" or "n_ts201" => "Woody",
                "PartyMember_005" or "n_ts202" => "Buzz Lightyear",
                "PartyMember_006" or "n_ra201" => "Rapunzel",
                "PartyMember_007" or "n_ra203" => "Flynn Rider",
                "PartyMember_008" or "n_mi201" => "Sulley",
                "PartyMember_009" or "n_mi202" => "Mike",
                "PartyMember_010" or "n_fz214" => "Marshmallow",
                "PartyMember_011" or "n_ca201" => "Captain Jack Sparrow",
                "PartyMember_012" or "n_bx202" => "Baymax",
                "PartyMember_013" or "n_ex003" => "Mickey",
                "PartyMember_014" or "n_ex004" => "Riku",
                "PartyMember_015" or "n_ex040" => "Aqua",
                "PartyMember_016" or "n_ex006" => "Ventus",
                "PartyMember_017" or "n_ex010" => "Lea",
                "PartyMember_018" or "n_ex005" => "Kairi",
                "PartyMember_019" or "n_ex009" => "Roxas",
                "PartyMember_020" or "n_ex033" => "Xion",
                "PartyMember_021" or "n_ex023" => "Sora",
                "PartyMember_022" or "n_ca001" => "Donald (Caribbean)",
                "PartyMember_023" or "n_ca002" => "Goofy (Caribbean)",
                "PartyMember_024" or "n_mi001" => "Donald (Monstropolis)",
                "PartyMember_025" or "n_mi002" => "Goofy (Monstropolis)",
                "PartyMember_026" or "n_ts001" => "Donald (Toy Box)",
                "PartyMember_027" or "n_ts002" => "Goofy (Toy Box)",
                _ => party
            };
        }
    }
}