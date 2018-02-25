using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using VanillaRuleGenerator.Extensions;
using VanillaRuleGenerator.Helpers;
using VanillaRuleGenerator.Properties;
using VanillaRuleGenerator.Rules;
using VanillaRuleGenerator.Rules.BombGame;

namespace VanillaRuleGenerator
{
    public sealed class ManualGenerator
    {
        //public TextAsset[] ManualDataFiles;
        private readonly List<ManualFileName> _manualFileNames = new List<ManualFileName>()
        {
            //HTML Manuals
            new ManualFileName("Capacitor Discharge.html", Resources.Capacitor_Discharge),
            new ManualFileName("Complicated Wires.html", Resources.Complicated_Wires),
            new ManualFileName("Keypad.html", Resources.Keypads),
            new ManualFileName("Knob.html", Resources.Knobs),
            new ManualFileName("Maze.html", Resources.Mazes),
            new ManualFileName("Memory.html", Resources.Memory),
            new ManualFileName("Morse Code.html", Resources.Morse_Code),
            new ManualFileName("Password.html", Resources.Passwords),
            new ManualFileName("Simon Says.html", Resources.Simon_Says),
            new ManualFileName("The Button.html", Resources.The_Button),
            new ManualFileName("Venting Gas.html", Resources.Venting_Gas),
            new ManualFileName("Who’s on First.html", Resources.Whos_on_First),
            new ManualFileName("Wire Sequence.html", Resources.Wire_Sequences),
            new ManualFileName("Wires.html", Resources.Wires),
            new ManualFileName("index.html", Resources.index),

            //CSS
            new ManualFileName(Path.Combine("css", "dark-theme.css"), Resources.dark_theme),
            new ManualFileName(Path.Combine("css", "font.css"), Resources.font),
            new ManualFileName(Path.Combine("css", "jquery-ui.1.12.1.css"), Resources.jquery_ui_1_12_1),
            new ManualFileName(Path.Combine("css", "main.css"), Resources.main),
            new ManualFileName(Path.Combine("css", "main-before.css"), Resources.main_before),
            new ManualFileName(Path.Combine("css", "main-orig.css"), Resources.main_orig),
            new ManualFileName(Path.Combine("css", "normalize.css"), Resources.normalize),

            //Font
            new ManualFileName(Path.Combine("font", "SpecialElite.ttf"), Resources.SpecialElite),
            new ManualFileName(Path.Combine("font", "trebuc.ttf"), Resources.trebuc),
            new ManualFileName(Path.Combine("font", "trebucbd.ttf"), Resources.trebucbd),
            new ManualFileName(Path.Combine("font", "trebucbi.ttf"), Resources.trebucbi),
            new ManualFileName(Path.Combine("font", "trebucit.ttf"), Resources.trebucit),

            //img
            new ManualFileName(Path.Combine("img", "Bomb.svg"), Resources.Bomb),
            new ManualFileName(Path.Combine("img", "BombSide.svg"), Resources.BombSide),
            new ManualFileName(Path.Combine("img", "ktane-logo.png"), Resources.ktane_logo),
            new ManualFileName(Path.Combine("img", "page-bg-noise-01.png"), Resources.page_bg_noise_01),
            new ManualFileName(Path.Combine("img", "page-bg-noise-02.png"), Resources.page_bg_noise_02),
            new ManualFileName(Path.Combine("img", "page-bg-noise-03.png"), Resources.page_bg_noise_03),
            new ManualFileName(Path.Combine("img", "page-bg-noise-04.png"), Resources.page_bg_noise_04),
            new ManualFileName(Path.Combine("img", "page-bg-noise-05.png"), Resources.page_bg_noise_05),
            new ManualFileName(Path.Combine("img", "page-bg-noise-06.png"), Resources.page_bg_noise_06),
            new ManualFileName(Path.Combine("img", "page-bg-noise-07.png"), Resources.page_bg_noise_07),
            new ManualFileName(Path.Combine("img", "web-background.jpg"), Resources.web_background),
            new ManualFileName(Path.Combine("img", Path.Combine("appendix-batteries", "Battery-AA.svg")), Resources.Battery_AA),
            new ManualFileName(Path.Combine("img", Path.Combine("appendix-batteries", "Battery-D.svg")), Resources.Battery_D),
            new ManualFileName(Path.Combine("img", Path.Combine("appendix-ports", "DVI.svg")), Resources.DVI),
            new ManualFileName(Path.Combine("img", Path.Combine("appendix-ports", "Parallel.svg")), Resources.Parallel),
            new ManualFileName(Path.Combine("img", Path.Combine("appendix-ports", "PS2.svg")), Resources.PS2),
            new ManualFileName(Path.Combine("img", Path.Combine("appendix-ports", "RJ45.svg")), Resources.RJ45),
            new ManualFileName(Path.Combine("img", Path.Combine("appendix-ports", "Serial.svg")), Resources.Serial),
            new ManualFileName(Path.Combine("img", Path.Combine("appendix-ports", "StereoRCA.svg")), Resources.StereoRCA),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "Component.svg")), Resources.Component),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "IndicatorWidget.svg")), Resources.IndicatorWidget),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "NeedyComponent.svg")), Resources.NeedyComponent),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "TimerComponent.svg")), Resources.TimerComponent),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "Capacitor Discharge.svg")), Resources.Capacitor_Discharge1),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "Complicated Wires.svg")), Resources.Complicated_Wires1),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "Keypads.svg")), Resources.Keypads1),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "Knobs.svg")), Resources.Knobs1),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "Mazes.svg")), Resources.Mazes1),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "Memory.svg")), Resources.Memory1),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "Morse Code.svg")), Resources.Morse_Code1),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "Passwords.svg")), Resources.Passwords1),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "Simon Says.svg")), Resources.Simon_Says1),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "The Button.svg")), Resources.The_Button1),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "Venting Gas.svg")), Resources.Venting_Gas1),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "Who’s on First.svg")), Resources.Whos_on_First1),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "Wire Sequences.svg")), Resources.Wire_Sequences1),
            new ManualFileName(Path.Combine("img", Path.Combine("Component", "Wires.svg")), Resources.Wires1),
            new ManualFileName(Path.Combine("img", Path.Combine("Morsematics", "International_Morse_Code.svg")), Resources.International_Morse_Code),
            new ManualFileName(Path.Combine("img", Path.Combine("Simon Says", "SimonComponent_ColourLegend.svg")), Resources.Simon_Says1),
            new ManualFileName(Path.Combine("img", Path.Combine("Who’s on First", "eye-icon.png")), Resources.eye_icon),

            //js
            new ManualFileName(Path.Combine("js", "highlighter.js"), Resources.highlighter_js),
            new ManualFileName(Path.Combine("js", "jquery-ui.1.12.1.min.js"), Resources.jquery_3_1_1_min_js),
            new ManualFileName(Path.Combine("js", "jquery.3.1.1.min.js"), Resources.jquery_3_1_1_min_js),
        };

        private readonly List<ManualFileName> _keypadFiles = new List<ManualFileName>
        {
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "1-copyright.png")), Resources._1_copyright),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "2-filledstar.png")), Resources._2_filledstar),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "3-hollowstar.png")), Resources._3_hollowstar),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "4-smileyface.png")), Resources._4_smileyface),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "5-doublek.png")), Resources._5_doublek),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "6-omega.png")), Resources._6_omega),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "7-squidknife.png")), Resources._7_squidknife),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "8-pumpkin.png")), Resources._8_pumpkin),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "9-hookn.png")), Resources._9_hookn),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "10-teepee.png")), Resources._10_teepee),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "11-six.png")), Resources._11_six),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "12-squigglyn.png")), Resources._12_squigglyn),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "13-at.png")), Resources._13_at),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "14-ae.png")), Resources._14_ae),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "15-meltedthree.png")), Resources._15_meltedthree),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "16-euro.png")), Resources._16_euro),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "17-circle.png")), Resources._17_circle),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "18-nwithhat.png")), Resources._18_nwithhat),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "19-dragon.png")), Resources._19_dragon),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "20-questionmark.png")), Resources._20_questionmark),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "21-paragraph.png")), Resources._21_paragraph),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "22-rightc.png")), Resources._22_rightc),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "23-leftc.png")), Resources._23_leftc),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "24-pitchfork.png")), Resources._24_pitchfork),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "25-tripod.png")), Resources._25_tripod),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "26-cursive.png")), Resources._26_cursive),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "27-tracks.png")), Resources._27_tracks),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "28-balloon.png")), Resources._28_balloon),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "29-weirdnose.png")), Resources._29_weirdnose),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "30-upsidedowny.png")), Resources._30_upsidedowny),
            new ManualFileName(Path.Combine("img", Path.Combine("Round Keypad", "31-bt.png")), Resources._31_bt),
        };

        private Dictionary<string,object> HTMLManualGenerators = new Dictionary<string, object>();

        private ManualGenerator()
        {
            for (var i = 0; i <= (int) HTMLManualNames.Index; i++)
            {
                HTMLManualGenerators[_manualFileNames[i].Name] = (HTMLManualNames) i;
            }

            if (Directory.Exists("ModAssemblies"))
            {
                foreach (var file in Directory.GetFiles("ModAssemblies"))
                {
                    var modRuleGenerator = LoadModRuleGeneratorAssembly(file);
                    if (modRuleGenerator == null)
                        continue;
                    var manualName = modRuleGenerator.GetHTMLFileName();
                    HTMLManualGenerators[manualName] = modRuleGenerator;
                }
            }


        }
        public static ManualGenerator Instance => Nested.instance;

        // ReSharper disable once ClassNeverInstantiated.Local
        private class Nested
        {
            static Nested() { }
            // ReSharper disable once InconsistentNaming
            internal static readonly ManualGenerator instance = new ManualGenerator();
        }

        public static void DebugLog(string message, params object[] args)
        {
            CommonReflectedTypeInfo.DebugLog(message, args);
        }

        private static Dictionary<string, ModRuleGenerator> _ruleGenerators = new Dictionary<string, ModRuleGenerator>();
        private static Dictionary<string, bool> _assemblyLoadFailure = new Dictionary<string, bool>();

        public static ModRuleGenerator LoadModRuleGeneratorAssembly(string assemblyName)
        {
            if (_assemblyLoadFailure.ContainsKey(assemblyName))
                return null;

			if (!_ruleGenerators.TryGetValue(assemblyName, out ModRuleGenerator ruleGenerator))
			{
				try
				{
					ruleGenerator = ModRuleGenerator.GetRuleGenerator(Assembly.LoadFrom(assemblyName));
					if (ruleGenerator != null)
					{
						_ruleGenerators[assemblyName] = ruleGenerator;
					}
					else
					{
						_assemblyLoadFailure[assemblyName] = true;
						return null;
					}
				}
				catch (FileNotFoundException)
				{
					return null;
				}
				catch (FileLoadException)
				{
					return null;
				}
				catch (Exception ex)
				{
					_assemblyLoadFailure[assemblyName] = true;
					return null;
				}
			}
			return ruleGenerator;
        }

        public static bool GenerateModManual(string assemblyName, int seed)
        {
            var ruleGenerator = LoadModRuleGeneratorAssembly(assemblyName);
            if (ruleGenerator == null) return false;

            var manualPath = ManualGenerator.GetManualPath(seed);
            ruleGenerator.WriteAllFiles(seed, manualPath);
            return true;
        }

        private void WriteComplicatedWiresManual(string path, ManualFileName file, ref List<ReplaceText> replacements, bool outputFile = true)
        {

            var lineTypes = new List<string>
            {
                "15,40,4,10",
                string.Empty,
                "3",
                "8"
            };
            var labels = new List<string>
            {
                "Wire has red\ncoloring",
                "Wire has blue\ncoloring",
                "Has ★ symbol",
                "LED is on"
            };

            var ruleset = _ruleManager.VennWireRuleSet;
            var cutInstructionList = new List<CutInstruction>
            {
                ruleset.RuleDict[new VennWireState(true, false, false, false)],
                ruleset.RuleDict[new VennWireState(false, true, false, false)],
                ruleset.RuleDict[new VennWireState(false, false, true, false)],
                ruleset.RuleDict[new VennWireState(false, false, false, true)],
                ruleset.RuleDict[new VennWireState(true, false, true, false)],
                ruleset.RuleDict[new VennWireState(true, true, false, false)],
                ruleset.RuleDict[new VennWireState(false, true, false, true)],
                ruleset.RuleDict[new VennWireState(false, false, true, true)],
                ruleset.RuleDict[new VennWireState(false, true, true, false)],
                ruleset.RuleDict[new VennWireState(true, false, false, true)],
                ruleset.RuleDict[new VennWireState(true, true, true, false)],
                ruleset.RuleDict[new VennWireState(true, true, false, true)],
                ruleset.RuleDict[new VennWireState(false, true, true, true)],
                ruleset.RuleDict[new VennWireState(true, false, true, true)],
                ruleset.RuleDict[new VennWireState(true, true, true, true)],
                ruleset.RuleDict[new VennWireState(false, false, false, false)]
            };

            var vennList = new List<string>();
            using (var enumerator = cutInstructionList.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    // ReSharper disable once SwitchStatementMissingSomeCases
                    switch (enumerator.Current)
                    {
                        case CutInstruction.Cut:
                            vennList.Add("C");
                            break;
                        case CutInstruction.DoNotCut:
                            vennList.Add("D");
                            break;
                        case CutInstruction.CutIfSerialEven:
                            vennList.Add("S");
                            break;
                        case CutInstruction.CutIfParallelPortPresent:
                            vennList.Add("P");
                            break;
                        case CutInstruction.CutIfTwoOrMoreBatteriesPresent:
                            vennList.Add("B");
                            break;
                    }
                }
            }

            var vennSVG = new SVGGenerator(800, 650);
            var legendSVG = new SVGGenerator(275, 200);
            vennSVG.Draw4SetVennDiagram(vennList, lineTypes);
            legendSVG.DrawVennDiagramLegend(labels, lineTypes);
            replacements.Add(new ReplaceText {Original = "VENNDIAGRAMSVGDATA", Replacement = vennSVG.ToString()});
            replacements.Add(new ReplaceText {Original = "VENNLEGENDSVGDATA", Replacement = legendSVG.ToString()});
            file.WriteFile(path, replacements, outputFile);
        }

        private void WriteMazesManual(string path, ManualFileName file, ref List<ReplaceText> replacements, bool outputFile = true)
        {
            var mazes = _ruleManager.MazeRuleSet.GetMazes();
            for (var i = 0; i < mazes.Count; i++)
            {
                replacements.Add(new ReplaceText {Original = $"MAZE{i}SVGDATA", Replacement = mazes[i].ToSVG().Replace("<svg ", "<svg class=\"maze\" ") });
            }
            file.WriteFile(path, replacements, outputFile);
        }

        private void WriteSimonSaysManual(string path, ManualFileName file, ref List<ReplaceText> replacements, bool outputFile = true)
        {
            var rules = _ruleManager.SimonRuleSet.RuleList;
            foreach (var keyValuePair in rules)
            {
                var colors = new[] { "RED", "BLUE", "GREEN", "YELLOW" };
                for (var i = 0; i < keyValuePair.Value.Count; i++)
                {
                    for (var j = 0; j < 4; j++)
                    {
                        replacements.Add(new ReplaceText() { Original = $"{keyValuePair.Key}{i}{colors[j]}", Replacement = keyValuePair.Value[i][j].ToString() });
                    }
                }
            }
            file.WriteFile(path, replacements, outputFile);
        }

        private void WritePasswordManual(string path, ManualFileName file, ref List<ReplaceText> replacements, bool outputFile = true)
        {
            var passwordrules = _ruleManager.PasswordRuleSet.possibilities;
            for (var i = 0; i < passwordrules.Count; i++)
            {
                replacements.Add(new ReplaceText { Original = $"PASSWORD{i:00}", Replacement = passwordrules[i] });
            }

            file.WriteFile(path, replacements, outputFile);
        }

        private void WriteNeedyKnobManual(string path, ManualFileName file, ref List<ReplaceText> replacements, bool outputFile = true)
        {
            var replacement = string.Empty;
            var currentDirection = string.Empty;
            foreach (var rule in _ruleManager.NeedyKnobRuleSet.Rules)
            {
                var direction = rule.Solution.Text;
                if (currentDirection != direction)
                {
                    replacement += $"                            <h4>{direction}:</h4>\n";
                    currentDirection = direction;
                }
                foreach (var query in rule.Queries)
                {
                    var leds = (bool[])query.Args[NeedyKnobRuleSet.LED_CONFIG_ARG_KEY];
                    replacement += "                            <table style=\"display: inline-table\">\n";
                    for (var i = 0; i < NeedyKnobRuleSetGenerator.LED_ROWS; i++)
                    {
                        replacement += "                                <tr>\n";
                        for (var j = 0; j < NeedyKnobRuleSetGenerator.LED_COLS; j++)
                        {
                            if (leds[NeedyKnobRuleSetGenerator.LED_COLS * i + j])
                            {
                                replacement += "                                <td>X</td>\n";
                            }
                            else
                            {
                                replacement += "                                <td>&nbsp;</td>\n";
                            }
                        }
                        replacement += "                                </tr>\n";
                    }
                    replacement += "                            </table>\n";
                }
            }


            replacements.Add(new ReplaceText { Original = "NEEDYKNOBLIGHTCONFIGURATION", Replacement = replacement });
            file.WriteFile(path, replacements, outputFile);

        }

        private void WriteKeypadsManual(string path, ManualFileName file, ref List<ReplaceText> replacements, bool outputFile = true)
        {
            var table = string.Empty;
            var rules = _ruleManager.KeypadRuleSet.PrecedenceLists;

            for (var i = 0; i < rules[0].Count; i++)
            {
                table += "                                <tr>\n";
                for (var j = 0; j < rules.Count; j++)
                {
                    table += "                                    <td class=\"keypad-table-column\"><img class=\"keypad-symbol-image\" src=\"";

                    var index = SymbolPool.Symbols.IndexOf(rules[j][i]);
                    table += _keypadFiles[index].Name.Replace(Path.DirectorySeparatorChar, '/');
                    table += "\"></img>";
                    table += "                                    </td>\n";
                    if (j == (rules.Count - 1))
                        break;
                    table += "                                    <td class=\"keypad-table-spacer\"></td>\n";
                }
                table += "                                </tr>\n";
            }
            replacements.Add(new ReplaceText() { Original = "<!--KEYPADTABLE GOES HERE-->", Replacement = table });
            foreach (var imagefile in _keypadFiles)
            {
                imagefile.WriteFile(path, outputFile);
            }

            file.WriteFile(path, replacements, outputFile);
        }

        private void WriteWhosOnFirstManual(string path, ManualFileName file, ref List<ReplaceText> replacements, bool outputFile = true)
        {
            var step1Precedentlist = _ruleManager.WhosOnFirstRuleSet.displayWordToButtonIndexMap;

            var replace = string.Empty;
            for (var i = 0; i < 5; i++)
            {
                replace += "                                <tr>\n";
                if (i == 4)
                {
                    replace += "                                    <td></td>\n";
                }
                for (var j = 0; j < ((i == 4) ? 4 : 6); j++)
                {
                    var word = WhosOnFirstRuleSet.DisplayWords[(i * 6) + j];
                    var index = step1Precedentlist[word].ToString();

                    replace += "                                    <td>\n";
                    replace += "                                        <table>\n";
                    replace += "                                            <tr>\n";
                    replace += "                                                <th class=\"whos-on-first-look-at-display\" colspan=\"2\">";
                    replace += word.Trim();
                    replace += "</th>\n";
                    replace += "                                            </tr>\n";
                    for (var k = 0; k < 3; k++)
                    {
                        replace += "                                            <tr>\n";
                        for (var l = 0; l < 2; l++)
                        {
                            if (index.Trim().Equals(((k * 2) + l).ToString()))
                            {
                                replace += "<td class=\"whos-on-first-look-at\"><img src=\"img/Who’s on First/eye-icon.png\" alt=\"Look At\" style=\"height: 1em;\" /></td>";
                            }
                            else
                            {
                                replace += "                                                <td><br /></td>\n";
                            }
                        }
                        replace += "                                            </tr>\n";
                    }

                    replace += "                                        </table>\n";
                    replace += "                                    </td>\n";
                }
                replace += "                                </tr>\n";
            }
            replacements.Add(new ReplaceText { Original = "LOOKATDISPLAYMAP", Replacement = replace });
            replace = string.Empty;

            foreach (var map in _ruleManager.WhosOnFirstRuleSet.precedenceMap)
            {
                replace += "                                <tr>\n";
                replace += "                                    <th>";
                replace += map.Key;
                replace += "</th>\n";
                replace += "                                    <td>";
                replace += string.Join(", ", map.Value.ToArray());
                replace += "</td>";
                replace += "                                </tr>\n";
            }
            replacements.Add(new ReplaceText { Original = "PRECEDENTMAP", Replacement = replace });

            file.WriteFile(path, replacements, outputFile);
        }

        private void WriteWireSequenceManual(string path, ManualFileName file, ref List<ReplaceText> replacements, bool outputFile = true)
        {
            var wiresequencetable = string.Empty;
            var wireLetters = new[] { "A", "B", "C" };
            for (var i = WireSequenceRuleSetGenerator.NUM_COLOURS - 1; i >= 0; i--)
            {
                var color = (WireColor)i;
                wiresequencetable += $"<table class=\'{color}'>";
                wiresequencetable += $"<tr><th colspan=\'2\' class=\'header\'>{color.ToString().Capitalize()} Wire Occurrences</th></tr>";
                wiresequencetable += "<tr><th class=\'first-col\'>Wire Occurrence</th><th class=\'second-col\'>Cut if connected to:</th></tr>";
                for (var j = 0; j < WireSequenceRuleSetGenerator.NumWiresPerColour; j++)
                {
                    wiresequencetable += $"<tr><td class=\'first-col\'>{Util.OrdinalWord(j + 1)}&nbsp;{color} occurrence</td><td class=\'second-col\'>";
                    var list = new List<string>();
                    for (var k = 0; k < WireSequenceRuleSetGenerator.NUM_PER_PAGE; k++)
                    {
                        if (_ruleManager.WireSequenceRuleSet.ShouldBeSnipped(color, j, k))
                        {
                            list.Add(wireLetters[k]);
                        }
                    }
                    for (var k = 0; k < list.Count; k++)
                    {
                        wiresequencetable += list[k];
                        if (k == list.Count - 2)
                            wiresequencetable += " or ";
                        else if (k < list.Count - 2 && list.Count > 2)
                            wiresequencetable += ", ";
                    }
                    if (list.Count == 0)
                        wiresequencetable += "Never Cut";
                    wiresequencetable += "</td></tr>";
                }
                wiresequencetable += "</table>\n";
            }

            replacements.Add(new ReplaceText { Original = "WIRESEQUENCETABLES", Replacement = wiresequencetable });
            file.WriteFile(path, replacements, outputFile);
        }

        private void WriteMorseCodeManaul(string path, ManualFileName file, ref List<ReplaceText> replacements, bool outputFile = true)
        {
            var worddict = _ruleManager.MorseCodeRuleSet.WordDict;
            var validFreqs = _ruleManager.MorseCodeRuleSet.ValidFrequencies;
            var morsecodetable = validFreqs.Aggregate(string.Empty, (current, freq) => current + $"<tr><td>{worddict[freq]}</td><td>3.{freq} MHz</td></tr>\n");
            replacements.Add(new ReplaceText { Original = "MORSECODELOOKUP", Replacement = morsecodetable });
            file.WriteFile(path, replacements, outputFile);
        }

        private void WriteWiresManual(string path, ManualFileName file, ref List<ReplaceText> replacements, bool outputFile = true)
        {
            var wirecuttinginstructions = string.Empty;
            var wirerules = _ruleManager.WireRuleSet.RulesDictionary;

            foreach (var rules in wirerules)
            {
                var rule = new List<Rule>(rules.Value);

                wirecuttinginstructions += $"<tr><td><strong><em>{rules.Key} wires:</em></strong><br />\n";
                if (rule.Count == 1)
                {
                    wirecuttinginstructions += $"{rule[0].GetSolutionString()}.\n";
                }
                else
                {
                    wirecuttinginstructions += $"If {rule[0].GetQueryString()}, {rule[0].GetSolutionString()}.\n";
                    for (var i = 1; i < rule.Count - 1; i++)
                    {
                        wirecuttinginstructions += $"<br />Otherwise, If {rule[i].GetQueryString()}, {rule[i].GetSolutionString()}.\n";
                    }
                    wirecuttinginstructions += $"<br />Otherwise, {rule.Last().GetSolutionString()}.\n";
                }
            }


            replacements.Add(new ReplaceText { Original = "WIRECUTTINGINSTRUCTIONS", Replacement = wirecuttinginstructions });
            file.WriteFile(path, replacements, outputFile);
        }

        private void WriteMemoryManual(string path, ManualFileName file, ref List<ReplaceText> replacements, bool outputFile = true)
        {
            var memoryinstructions = string.Empty;

            foreach (var stage in _ruleManager.MemoryRuleSet.RulesDictionary)
            {
                memoryinstructions += $"                        <h4>Stage {stage.Key + 1}:</h4><p>";
                for (var i = 0; i < stage.Value.Count; i++)
                {
                    memoryinstructions += $"If {stage.Value[i].GetQueryString()}, {stage.Value[i].GetSolutionString()}.<br />";
                }
                memoryinstructions += "</p>\n";
            }

            replacements.Add(new ReplaceText { Original = "MEMORYRULES", Replacement = memoryinstructions });
            file.WriteFile(path, replacements, outputFile);
        }

        private void WriteButtonManual(string path, ManualFileName file, ref List<ReplaceText> replacements, bool outputFile = true)
        {
            var initial = string.Empty;
            var onhold = string.Empty;
            var portsused = false;

            foreach (var press in _ruleManager.ButtonRuleSet.RuleList)
            {
                portsused = press.Queries.Aggregate(portsused, (current, query) => current | query.Property == QueryablePorts.EmptyPortPlate);
                portsused = press.Queries.Aggregate(portsused, (current, query) => current | QueryablePorts.PortList.Contains(query.Property));
            }

            initial = _ruleManager.ButtonRuleSet.RuleList.Aggregate(initial, (current, press) => current + $"<li>If {press.GetQueryString()}, {press.GetSolutionString()}.</li>\n");
            onhold = _ruleManager.ButtonRuleSet.HoldRuleList.Aggregate(onhold, (current, hold) => current + $"<li><em>{hold.GetQueryString()}</em> {hold.GetSolutionString()}</li>\n");

            replacements.Add(new ReplaceText { Original = "APPENDIXCREFERENCE", Replacement = portsused ? "<br />See Appendix C for port identification reference." : "" });
            replacements.Add(new ReplaceText { Original = "INITIALBUTTONRULES", Replacement = initial });
            replacements.Add(new ReplaceText { Original = "ONBUTTONHOLDRULES", Replacement = onhold });
            file.WriteFile(path, replacements, outputFile);
        }

        private void WriteHTML(string path, ManualFileName file, ref List<ReplaceText> replacements, bool outputFile=true)
        {
            if (string.IsNullOrEmpty(file.Name))
                return;
            switch (file.Name)
            {
                case "The Button.html":
                    WriteButtonManual(path, file, ref replacements, outputFile);
                    break;
                case "Memory.html":
                    WriteMemoryManual(path, file, ref replacements, outputFile);
                    break;
                case "Wires.html":
                    WriteWiresManual(path, file, ref replacements, outputFile);
                    break;
                case "Wire Sequences.html":
                    WriteWireSequenceManual(path, file, ref replacements, outputFile);
                    break;
                case "Morse Code.html":
                    WriteMorseCodeManaul(path, file, ref replacements, outputFile);
                    break;
                case "Who’s on First.html":
                    WriteWhosOnFirstManual(path, file, ref replacements, outputFile);
                    break;
                case "Knob.html":
                    WriteNeedyKnobManual(path, file, ref replacements, outputFile);
                    break;
                case "Passwords.html":
                    WritePasswordManual(path, file, ref replacements, outputFile);
                    break;
                case "Simon Says.html":
                    WriteSimonSaysManual(path, file, ref replacements, outputFile);
                    break;
                case "Keypads.html":
                    WriteKeypadsManual(path, file, ref replacements, outputFile);
                    break;
                case "Mazes.html":
                    WriteMazesManual(path, file, ref replacements, outputFile);
                    break;
                case "Complicated Wires.html":
                    WriteComplicatedWiresManual(path, file, ref replacements, outputFile);
                    break;
                case "index.html":
                    file.WriteFile(path, replacements, outputFile);
                    break;
                default:
                    file.WriteFile(path, outputFile);
                    break;
            }
        }

        private RuleManager _ruleManager => RuleManager.Instance;

        private static readonly List<int> PreviousSeeds = new List<int>();

        public static string GetManualPath(int seed)
        {
            return Path.Combine("ModifiedVanillaManuals", seed.ToString());
        }

        private string InitializeManaulWriting(int seed, out List<ReplaceText> replacements, bool forceRewriteSeed=false)
        {
            if (!forceRewriteSeed)
            {
                if (PreviousSeeds.Contains(seed))
                {
                    if (seed != 1)
                        DebugLog($"Manual already written for seed #{seed}.");
                    replacements = null;
                    return null; //Seed 1 is the Original Vanilla seed.
                }
                PreviousSeeds.Add(seed);
            }

            _ruleManager.Initialize(seed);

            var path = GetManualPath(seed);
            //if (Directory.Exists(path))
            //    return;

            DebugLog($"Printing the Rules for seed #{seed}");
            _ruleManager.CurrentRules.PrintRules();

            if (_manualFileNames == null)
            {
                DebugLog("Can't write any manuals :(");
                replacements = null;
                return null;
            }

            replacements = new List<ReplaceText>
            {
                new ReplaceText {Original = "VANILLAMODIFICATIONSEED", Replacement = seed.ToString()},
                new ReplaceText {Original = "<span class=\"page-header-doc-title\">Keep Talking and Nobody Explodes v. 1</span>", Replacement = $"<span class=\"page-header-doc-title\">Keep Talking and Nobody Explodes - Seed #{seed}</span>"},
                new ReplaceText {Original = "<span class=\"page-header-doc-title\">Keep Talking and Nobody Explodes</span>", Replacement = $"<span class=\"page-header-doc-title\">Keep Talking and Nobody Explodes - Seed #{seed}</span>"}
            };

            return path;
        }

        public string GetHTMLManual(int seed, HTMLManualNames name)
        {
            if (name < 0 || name > HTMLManualNames.Index)
                return null;
			if (string.IsNullOrEmpty(InitializeManaulWriting(seed, out List<ReplaceText> replacements, true)))
				return null;
			for (var i = name == HTMLManualNames.Index ? 0 : (int)name; i <= (int) name; i++)
            {
                WriteHTML(null, _manualFileNames[i], ref replacements, false);
            }
            return _manualFileNames[(int) name].ToString(replacements);
        }

        public string GetHTMLManual(int seed, string name)
        {
			if (!HTMLManualGenerators.TryGetValue(name, out object generator))
				return string.Empty;

			if (generator is HTMLManualNames)
                return GetHTMLManual(seed, (HTMLManualNames) generator);
            else if (generator is ModRuleGenerator)
                return ((ModRuleGenerator) generator).GetHTML(seed);
            else
                return string.Empty;
        }

	    public string[] GetHTMLFileNames()
	    {
		    return HTMLManualGenerators.Select(x => x.Key).ToArray();
	    }


        public void WriteIndexManaul(int seed)
        {
			var path = InitializeManaulWriting(seed, out List<ReplaceText> replacements);
			if (string.IsNullOrEmpty(path))
                return;
            foreach (var manual in _manualFileNames)
            {
                WriteHTML(path, manual, ref replacements, !manual.Name.EndsWith(".html") || manual.Name.Equals("index.html") );
            }
        }

        public void WriteManual(int seed)
        {
			var path = InitializeManaulWriting(seed, out List<ReplaceText> replacements);
			if (string.IsNullOrEmpty(path))
                return;

            foreach (var manual in _manualFileNames)
            {
                WriteHTML(path, manual, ref replacements);
            }
        }

        public void WriteModManuals(int seed, bool writeSupportFiles=true)
        {
            var path = GetManualPath(seed);
            var wroteMod = false;
            foreach (var kvp in HTMLManualGenerators.Where(x => x.Value is ModRuleGenerator))
            {
                var modRuleGenerator = kvp.Value as ModRuleGenerator;
                if (modRuleGenerator == null) continue;
                wroteMod = true;
                modRuleGenerator.WriteAllFiles(seed, path);
            }
            if (!wroteMod || !writeSupportFiles) return;

            foreach (var manual in _manualFileNames.Where(x => !HTMLManualGenerators.ContainsKey(x.Name)))
            {
                manual.WriteFile(path, true);
            }
        }

        public void WriteAllManuals(int seed)
        {
            WriteManual(seed);
            WriteModManuals(seed, false);
        }
    }

    public enum HTMLManualNames
    {
        CapacitorDischarge,
        ComplicatedWires,
        Keypads,
        Knobs,
        Mazes,
        Memory,
        MorseCode,
        Passwords,
        SimonSays,
        TheButton,
        VentingGas,
        WhosOnFirst,
        WireSequences,
        Wires,
        Index
    }
}