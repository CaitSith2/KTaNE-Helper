﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using KTaNE_Helper.Edgework;
using VanillaRuleGenerator.Edgework;
using VanillaRuleGenerator.Extensions;
using VanillaRuleGenerator.Modules;
using KTaNE_Helper.Modules.Modded;
using KTaNE_Helper.Modules.Vanilla;
using VanillaRuleGenerator.Rules.BombGame;
using VanillaRuleGenerator.Rules;
using VanillaRuleGenerator;
using static System.String;
using static VanillaRuleGenerator.Edgework.Indicators;
using static VanillaRuleGenerator.Edgework.PortPlate;
using static VanillaRuleGenerator.Edgework.SerialNumber;

namespace KTaNE_Helper
{
    public partial class Form1 : Form
    {

        private int _mazeSelection = -1;
        private int _mazeStartXY = 77;
        private int _mazeEndXY = 77;

        private int _whosOnFirstLookIndex = 6;

        private int _manualVersion;

        private readonly List<TabPage> _allPages = new List<TabPage>();
        private readonly List<TabPage> _noModPages = new List<TabPage>();

        public static Form1 Instance { get; private set; }

        public Form1()
        {
            InitializeComponent();
            Instance = this;
        }

        private void WsReset_Click(object sender, EventArgs e)
        {
            ws_input.Text = "";
        }

        private int GetModuleCount => int.TryParse(txtModuleCount.Text, out int x) ? x : 0;

	    public int GetBombTime
        {
            get
            {
                var split = txtBombTime.Text.Split(':');
				int.TryParse(split[1], out int seconds);
				seconds /= 30;
                seconds *= 30;
                if (int.TryParse(split[0], out int minutes))
                    seconds += minutes * 60;
                return seconds;
            }
        }

        public int GetBombMinutes => GetBombTime / 60;

        private void Complicated_Wires_Event(object sender, EventArgs e)
        {
            var batts = Batteries.TotalBatteries;
            cw_input.Text = cw_input.Text.ToUpper();
            cw_input.SelectionStart = cw_input.Text.Length;

            cw_output.Text = "";
            if (cw_input.Text == "") return;
            var cwWireGroups = cw_input.Text.ToLower().Split('\\');

            foreach (var cwWires in from @group in cwWireGroups let cwWires = @group.Split(' ') where @group.Length != 0 select cwWires)
            {
                var outputStr = "";
                var typestr = "";
                foreach (var wire in cwWires.Where(wire => wire.Length != 0))
                {
                    if (outputStr != "") outputStr += @", ";
                    VennWireState state = new VennWireState(wire.Contains("r"), wire.Contains("b"), wire.Contains("s"),wire.Contains("l"));
                    switch (RuleManager.Instance.VennWireRuleSet.RuleDict[state])
                    {
                        case CutInstruction.Cut:
                            //Cut, period.
                            outputStr += @"Cut";
                            typestr += @"C";
                            break;
                        case CutInstruction.CutIfSerialEven:
                            outputStr += (!SerialNumberLastDigitOdd() ? "Cut" : "Leave");
                            typestr += @"S";
                            //Cut if last digit of serial is Even
                            break;
                        case CutInstruction.DoNotCut:
                            outputStr += @"Leave";
                            typestr += @"D";
                            //Don't Cut, Period.
                            break;
                        case CutInstruction.CutIfTwoOrMoreBatteriesPresent:
                            outputStr += (batts >= 2 ? "Cut" : "Leave");
                            typestr += @"B";
                            //Cut if 2 or more Batteries
                            break;
                        case CutInstruction.CutIfParallelPortPresent:
                            outputStr += (IsPortPresent(PortTypes.Parallel) ? "Cut" : "Leave");
                            typestr += @"P";
                            //Cut if parallel port
                            break;
                    }
                }
                if (outputStr.Length <= 0) continue;
                if (cw_output.Text != "") cw_output.Text += Environment.NewLine;
                cw_output.Text += outputStr + @" - ( " + typestr + @" )";
            }
        }

        private static void Simon_Says_Set_Label(Control label, SimonColor color)
        {
            var labelText = new[] { "Red", "Blue", "Green", "Yellow" };
            var labelColor = new[] { Color.Red, Color.DodgerBlue, Color.Green, Color.Yellow };
            if (color > SimonColor.Yellow) return;
            label.Text = labelText[(int)color];
            label.ForeColor = labelColor[(int)color];
        }

        private void Simon_Says_Event()
        {
            var strikes = facts_strike.SelectedIndex;
            var ruleSet = RuleManager.Instance.SimonRuleSet.RuleList[SerialNumberContainsVowel() ? SimonRuleSet.HAS_VOWEL_STRING : SimonRuleSet.OTHERWISE_STRING][strikes];

            Simon_Says_Set_Label(ss_red, ruleSet[0]);
            Simon_Says_Set_Label(ss_blue, ruleSet[1]);
            Simon_Says_Set_Label(ss_green, ruleSet[2]);
            Simon_Says_Set_Label(ss_yellow, ruleSet[3]);
        }

        

        private void Button_Event(object sender, EventArgs e)
        {
            ButtonComponent.Instance.InitializeHoldRules(ButtonHoldRulesFlowLayoutPanel, button_label, button_color, button_name);
            var ruleSet = RuleManager.Instance.ButtonRuleSet;
            var component = ButtonComponent.Instance;
            component.ButtonColor = (ButtonColor)button_color.SelectedIndex;
            component.ButtonInstruction = (ButtonInstruction)button_name.SelectedIndex;
            var pressResult = ruleSet.ExecuteRuleList(component, ruleSet.RuleList);
            button_label.Text = ButtonSolutions.PressSolutions[pressResult].Text;
        }

        private void ManualVersionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            _manualVersion = ManualVersionSelect.SelectedIndex;
            var seeds = new[] {1, 2, 666, 6502};
            if (_manualVersion > 0 && _manualVersion < 4 && (int) nudVanillaSeed.Value != seeds[_manualVersion])
                nudVanillaSeed.Value = seeds[_manualVersion];
            switch (_manualVersion)
            {
                case 0:
                    linkLabel1.Text = @"http://www.bombmanual.com";
                    BigButtonEdgework.Text = @"Battery Count? Lit CAR/FRK Indicators?";
                    break;
                case 1:
                    BigButtonEdgework.Text = @"Battery Count? Lit BOB Indicator?";
                    linkLabel1.Text = @"http://www.lthummus.com/";
                    break;
                default:
                    BigButtonEdgework.Text = @"Need to know about all Edgework";
                    linkLabel1.Text = @"http://steamcommunity.com/sharedfiles/filedetails/?id=1224413364";
                    break;
            }
        }

        private void GenerateManual_Click(object sender, EventArgs e)
        {
            ManualGenerator.Instance.WriteManual((int)nudVanillaSeed.Value);
        }

        private void NudVanillaSeed_ValueChanged(object sender, EventArgs e)
        {
            switch ((int) nudVanillaSeed.Value)
            {
                case 1:
                    ManualVersionSelect.SelectedIndex = 0;
                    break;
                case 2:
                    ManualVersionSelect.SelectedIndex = 1;
                    break;
                case 666:
                    ManualVersionSelect.SelectedIndex = 2;
                    break;
                case 6502:
                    ManualVersionSelect.SelectedIndex = 3;
                    break;
                default:
                    ManualVersionSelect.SelectedIndex = 4;
                    break;
            }

            RuleManager.Instance.Initialize((int)nudVanillaSeed.Value);
            _initLettersNotPresent = false;
            KeypadReset_Click(sender, e);
            Needy_Knob_CheckedChanged(sender, e);
            Simon_Says_Event();
            Button_Event(sender, e);
            Complicated_Wires_Event(sender, e);
            WsReset_Click(sender, e);
            SimpleWires_Event(sender, e);
            Password_TextChanged(sender, e);
            MemoryReset_Click(sender, e);
            MorseCodeInput_TextChanged(sender, e);
            MazeSelection_TextChanged();
            wofStep1.Checked = true;
            WofStep1_CheckedChanged(sender, e);
            Refresh();
        }

        private readonly Dictionary<string, TabPage> _moduleNameToTab = new Dictionary<string, TabPage>();
        private readonly List<string> _stockModules = new List<string>();
        private readonly List<string> _moduleNames = new List<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            tcTabs.Multiline = true;
            Size = new Size(1142, 603);

            ManualVersionSelect.SelectedIndex = 0;
            ManualVersionSelect_SelectedIndexChanged(sender, e);
            NudVanillaSeed_ValueChanged(sender, e);
            WireReset_Click(sender, e);
            PasswordClear_Click(sender, e);

            var tt = new ToolTip
            {
                AutoPopDelay = 30000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                ShowAlways = true
            };

            tt.SetToolTip(cw_input, "Each wire is seperated by spaces, each group by a backslash" + Environment.NewLine +
                      "R for Red, B for Blue, L for Light, S for Star" + Environment.NewLine +
                      "Example: RS RBSL WS WSL B BS \\ W WS WL RS");

            tt.SetToolTip(wires_input, "Enter from these letters in the order the wires appear" + Environment.NewLine + 
                "R for Red, B for Blue, Y for Yellow, W for White, K for Black");

            tt.SetToolTip(ws_input, "Each Wire is seperated by a space. Enter in the form of Color, then Letter." + Environment.NewLine + 
                "Colors: R for Red, B for Blue, K for Black." + Environment.NewLine + 
                "Example: RB BC BC KA KC BA RC KB KC BB");

            tt.SetToolTip(MorseCodeInput, "Enter in the morse code here, in the form of - for dash (dah), . for dot (dit)" + Environment.NewLine + 
                "Example: -... .-. .. -.-. -.-" + Environment.NewLine + 
                "If you enter in |, that signifies beginning of word." + Environment.NewLine + 
                "If the defuser knows morse code, and gives you the letters directly, you can also enter them.  (Example: brick)");

            tt.SetToolTip(pass1, "Enter the 6 letters from the first password column here");
            tt.SetToolTip(pass2, "Enter the 6 letters from the second password column here");
            tt.SetToolTip(pass3, "Enter the 6 letters from the third password column here");
            tt.SetToolTip(pass4, "Enter the 6 letters from the fourth password column here");
            tt.SetToolTip(pass5, "Enter the 6 letters from the fifth password column here");

            UpdateBombSolution(null,null);

            foreach (TabPage p in tcTabs.TabPages)
            {
                var modulenames = p.Text.Split(',');
                foreach (var m in modulenames)
                {
                    var sm = m.Split(new[] {"|"}, StringSplitOptions.RemoveEmptyEntries);
                    foreach(var x in sm)
                    {
	                    _moduleNameToTab.Add(x.Trim(), p);
                    }

	                if (sm[0].Trim() == "About") continue;
                    if ((string) p.Tag == "mods")
                    {
	                    _moduleNames.Add("  " + sm[0].Trim());
                    }
                    else
                    {
                        _stockModules.Add("  " + sm[0].Trim());
                        if (sm.Length > 1)
                            _moduleNames.Add("  " + sm[1].Trim());
                    }
                }
            }
            _stockModules.Sort();
            _moduleNames.Sort();

            _stockModules.Insert(0, "----- Stock Modules -----");
            _stockModules.Insert(0, "");
            _moduleNames.Insert(0, "----- Add on Modules -----");
            _moduleNames.Insert(0, "");

            CheckBox1_CheckedChanged(null, null);
            BtnSillySlotsReset_Click(null, null);
        }

        private void WireReset_Click(object sender, EventArgs e)
        {
            wires_input.Text = "";
        }

        private void SimpleWires_Event(object sender, EventArgs e)
        {
            Wires_Input_TextChanged(sender, e);
        }

        private bool _initLettersNotPresent;
        private void Password_TextChanged(object sender, EventArgs e)
        {
            var passwords = RuleManager.Instance.PasswordRuleSet.possibilities;

            const string letters = "abcdefghijklmnopqrstuvwxyz";

            if (!_initLettersNotPresent)
            {
                for (var i = 0; i < 5; i++)
                {
                    ((Label) fpPasswordLettersNotPresent.Controls[i]).Text = "";
                    for (var j = 0; j < 26; j++)
                    {
                        var found = false;
                        for (var k = 0; k < 35 && !found; k++)
                        {
                            found = passwords[k].Substring(i, 1) == letters.Substring(j, 1);
                        }
                        if (!found) continue;
                        if (((Label) fpPasswordLettersNotPresent.Controls[i]).Text != "")
                            ((Label) fpPasswordLettersNotPresent.Controls[i]).Text += @"";
                        ((Label) fpPasswordLettersNotPresent.Controls[i]).Text += letters.Substring(j, 1).ToUpper();
                    }
                }
                _initLettersNotPresent = true;
            }


            var result = new List<string>();

            foreach (var pass in passwords)
            {
                var includePassword = true;
                for(var i = 0; i < 5 && includePassword; i++)
                {
	                if (((MaskedTextBox)fpPassword.Controls[i]).Text != Empty)
		                includePassword = ((MaskedTextBox) fpPassword.Controls[i]).Text.ToLower().Contains(pass.Substring(i, 1));
                }
	            if(includePassword)
                    result.Add(pass);
            }

            var count = 0;
            passResults.Text = Empty;
            foreach (var pass in result)
            {
                if (txtPasswordSubmitID.Text == "")
                {
                    if (count != 0)
                        passResults.Text += ((count++%8) == 0) ? Environment.NewLine : ", ";
                    else
                        count++;
                    passResults.Text += pass;
                }
                else
                {
                    passResults.Text += @"!" + txtPasswordSubmitID.Text + @" " + pass + Environment.NewLine;
                    
                }
            }

        }

        private void PasswordClear_Click(object sender, EventArgs e)
        {
            foreach (var textbox in fpPassword.Controls)
            {
	            ((MaskedTextBox) textbox).Text = Empty;
            }
	        Password_TextChanged(sender, e);
        }

        private string _memoryInstruction;
        private bool _memoryState = true;
        private int _memoryStage;
        private int _memoryStageNumber;
        private int[] _memoryNumbers = new int[5];
        private int[] _memoryPositions = new int[5];
        private int[] _memoryRules = new int[20];

	    private void MemoryButtonStates(bool state)
        {
            _memoryState = state;

            memoryDebug.Text = _memoryInstruction + Environment.NewLine;
            memoryDebug.Text += Environment.NewLine;
            memoryDebug.Text += @"Stage " + (_memoryStage + 1) + @" - " + (state ? "Display number" : "Defuser Input") + Environment.NewLine;
            memoryDebug.Text += Environment.NewLine;
            // ReSharper disable once LocalizableElement
            memoryDebug.Text += "Num\tPos" + Environment.NewLine;
            for (var i = 0; i <= _memoryStage; i++)
            {
                if (i == 5) break;
                // ReSharper disable once LocalizableElement
                memoryDebug.Text += _memoryNumbers[i] + "\t" + _memoryPositions[i] + Environment.NewLine;
            }


        }

	    private void ProcessNumberRules(int number)
        {
            _memoryStageNumber = number;
            var rule = _memoryRules[(_memoryStage*4) + number];
            switch (rule)
            {
                case MemoryRules.FirstPos:
                case MemoryRules.SecondPos:
                case MemoryRules.ThirdPos:
                case MemoryRules.FourthPos:
                    _memoryPositions[_memoryStage] = (rule - MemoryRules.FirstPos) + 1;
                    _memoryInstruction = @"Press Button in Position " + _memoryPositions[_memoryStage];
                    memoryNumberLabel.Visible = true;
                    break;
                case MemoryRules.One:
                case MemoryRules.Two:
                case MemoryRules.Three:
                case MemoryRules.Four:
                    _memoryNumbers[_memoryStage] = (rule - MemoryRules.One) + 1;
                    _memoryInstruction = @"Press Button labelled " + _memoryNumbers[_memoryStage];
                    memoryPositionLabel.Visible = true;
                    break;
                case MemoryRules.StageOnePos:
                case MemoryRules.StageTwoPos:
                case MemoryRules.StageThreePos:
                case MemoryRules.StageFourPos:
                    _memoryPositions[_memoryStage] = _memoryPositions[rule - MemoryRules.StageOnePos];
                    _memoryInstruction = @"Press Button in Position " + _memoryPositions[_memoryStage];
                    memoryNumberLabel.Visible = true;
                    break;
                case MemoryRules.StageOneLabel:
                case MemoryRules.StageTwoLabel:
                case MemoryRules.StageThreeLabel:
                case MemoryRules.StageFourLabel:
                    _memoryNumbers[_memoryStage] = _memoryNumbers[rule - MemoryRules.StageOneLabel];
                    _memoryInstruction = @"Press Button labelled " + _memoryNumbers[_memoryStage];
                    memoryPositionLabel.Visible = true;
                    break;
            }
            MemoryButtonStates(false);

            if (_memoryStage != 4) return;
            memoryNumberLabel.Visible = false;
            memoryPositionLabel.Visible = false;
            _memoryStage = 0;
            _memoryNumbers = new int[5];
            _memoryPositions = new int[5];
            MemoryButtonStates(true);

        }

        private void Num1_Click(object sender, EventArgs e)
        {
            if(_memoryState)
                ProcessNumberRules(Convert.ToInt32(((Button)sender).Tag));
            else
                ProcessPositionRules(Convert.ToInt32(((Button)sender).Tag));
        }

	    private void ProcessPositionRules(int number)
        {
            var rule = _memoryRules[(_memoryStage * 4) + _memoryStageNumber];
            switch (rule)
            {
                case MemoryRules.FirstPos:
                case MemoryRules.SecondPos:
                case MemoryRules.ThirdPos:
                case MemoryRules.FourthPos:
                case MemoryRules.StageOnePos:
                case MemoryRules.StageTwoPos:
                case MemoryRules.StageThreePos:
                case MemoryRules.StageFourPos:
                    _memoryNumbers[_memoryStage] = number + 1;
                    break;
                case MemoryRules.One:
                case MemoryRules.Two:
                case MemoryRules.Three:
                case MemoryRules.Four:
                case MemoryRules.StageOneLabel:
                case MemoryRules.StageTwoLabel:
                case MemoryRules.StageThreeLabel:
                case MemoryRules.StageFourLabel:
                    _memoryPositions[_memoryStage] = number + 1;
                    break;
            }
            memoryNumberLabel.Visible = memoryPositionLabel.Visible = false;
            _memoryStage++;

            _memoryInstruction = "";
            MemoryButtonStates(true);
        }

        private void MemoryReset_Click(object sender, EventArgs e)
        {
            _memoryInstruction = "";
            memoryDebug.Text = "";
            _memoryStage = 0;
            _memoryNumbers = new int[5];
            _memoryPositions = new int[5];
            MemoryButtonStates(true);

            var memoryRules = new List<int>();
            for (var i = 0; i < 5; i++)
            {
                memoryRules.AddRange(RuleManager.Instance.MemoryRuleSet.RulesDictionary[i].Select(rule => rule.Solution.SolutionMethod(new BombComponent(), rule.SolutionArgs)));
            }

            _memoryRules = memoryRules.ToArray();

        }

        private void Needy_Knob_CheckedChanged(object sender, EventArgs e)
        {
            var totalChecked = 0;
            var totalMask = 0;
            for (var i = 0; i < 12; i++)
            {
                if (((CheckBox) fpKnob.Controls[i]).CheckState == CheckState.Checked) totalChecked += 1 << i;
                if (((CheckBox) fpKnob.Controls[i]).CheckState != CheckState.Indeterminate) totalMask += 1 << i;
            }
            var Checked = NeedyKnobRuleSetGenerator.intToBoolArray(totalChecked, 12);
            var masked = NeedyKnobRuleSetGenerator.intToBoolArray(totalMask, 12);
            var solution = new List<string>();
            foreach (var rule in RuleManager.Instance.NeedyKnobRuleSet.Rules)
            {
                var match = true;
                foreach (var query in rule.Queries)
                {
                    var leds = (bool[])query.Args[NeedyKnobRuleSet.LED_CONFIG_ARG_KEY];
                    
                    for (var i = 0; i < 12 && match; i++)
                    {
                        if (!masked[i]) continue;
                        match &= (leds[i] == Checked[i]);
                    }
                }
                if (match && !solution.Contains(rule.GetSolutionString()))
                    solution.Add(rule.GetSolutionString());
            }
            txtNeedyKnobOut.Text = solution.Count == 1 ? solution[0] : $@"{solution.Count} Possible Directions";

        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabel1.Text);
        }

        public static string WordToMorseCode(string word, int freq, bool morseAMaze = false)
        {
            var morseCode = new Dictionary<string, string>
            {
                {"a", ".-"},{"b", "-..."},{"c", "-.-."},{"d", "-.."},{"e", "."},{"f", "..-."},
                { "g","--."},{"h","...."},{"i",".."},{"j",".---"},{"k","-.-"},{"l",".-.."},
                { "m","--"},{"n","-."},{"o","---"},{"p",".--."},{"q","--.-"},{"r",".-."},
                { "s","..."},{"t","-"},{"u","..-"},{"v","...-"},{"w",".--"},{"x","-..-"},
                {"y","-.--"},{"z","--.."},{"1",".----"},{"2","..---"},{"3","...--"},{"4","....-"},
                {"5","....."},{"6","-...."},{"7","--..."},{"8","---.."},{"9","----."},{"0","-----"}
            };

            var spacedMorse = "";
            var morse = "";

            for (var i = 0; i < word.Length; i++)
            {
				if (!morseCode.TryGetValue(word.Substring(i, 1).ToLowerInvariant(), out string letter)) continue;
				morse += letter;
                if (spacedMorse != "")
                    spacedMorse += " ";
                spacedMorse += letter;
            }
            return morseAMaze
                ? $"|{spacedMorse}|{morse}|{word},{freq}" 
                : $"|{spacedMorse}|{morse}|{word},{word} - 3.{freq} Mhz{Environment.NewLine},{freq}{Environment.NewLine}";
        }

        private void MorseCodeInput_TextChanged(object sender, EventArgs e)
        {
            var words = RuleManager.Instance.MorseCodeRuleSet.WordDict;

            MorseCodeOutput.Text = "";

            // ReSharper disable once InconsistentNaming
            foreach (var code in words.Select(Entry => 
                new {Entry}).OrderBy(x => x.Entry.Key).Select(word => WordToMorseCode(word.Entry.Value, word.Entry.Key)).Where(code => code.Split(',')[0].Contains(MorseCodeInput.Text)))
            {
                if (txtPasswordSubmitID.Text == "")
                    MorseCodeOutput.Text += code.Split(',')[1];
                else
                    MorseCodeOutput.Text += @"!" + txtPasswordSubmitID.Text + @" transmit " + code.Split(',')[2];
            }
        }

        private void Wires_Input_TextChanged(object sender, EventArgs e)
        {
            txtSimpleWireOutput.Text = WireSetComponent.Instance.GetSolution(wires_input.Text.Trim());
        }

        private void Ws_input_TextChanged(object sender, EventArgs e)
        {
            WireSequenceRuleSet ruleSet = RuleManager.Instance.CurrentRules.WireSequenceRuleSet;
            ws_input.Text = ws_input.Text.ToUpper();
            ws_input.SelectionStart = ws_input.Text.Length;

            var wirePairs = ws_input.Text.ToLower().Split(' ');
            
            var redCount = 0;
            var blueCount = 0;
            var blackCount = 0;
            ws_output.Text = "";
            if (ws_input.Text.Length == 0) return;

            foreach (var wire in wirePairs.Where(wire => wire.Length >= 2))
            {
                if (ws_output.Text != "") ws_output.Text += @", ";
                var i = ("abc").IndexOf(wire.Substring(1, 1), StringComparison.Ordinal);
                switch (wire.Substring(0, 1))
                {
                    case "r":
                        if (redCount == 9) continue;
                        ws_output.Text += ruleSet.ShouldBeSnipped(WireColor.red,redCount++,i) ? @"Cut" : @"Leave";
                        break;
                    case "b":
                        if (blueCount == 9) continue;
                        ws_output.Text += ruleSet.ShouldBeSnipped(WireColor.blue, blueCount++, i) ? @"Cut" : @"Leave";
                        break;
                    case "k":
                        if (blackCount == 9) continue;
                        ws_output.Text += ruleSet.ShouldBeSnipped(WireColor.black, blackCount++, i) ? @"Cut" : @"Leave";
                        break;

                }
            }

        }

        private string _mazeSelectionText = Empty;
        private string _mazeStartText = Empty;
        private string _mazeEndText = Empty;
        private bool _mazeRefreshing;
        private void MazeSelection_TextChanged()
        {
            if (_mazeRefreshing) return;
            var ruleSet = RuleManager.Instance.MazeRuleSet.GetMazes();
            int x;
            int y;
            _useMorseAMaze &= IsNullOrEmpty(_mazeSelectionText);
            if (!_useMorseAMaze)
            {
                _mazeRefreshing = true;
                txtMorseAMaze.Text = "";
                txtMorseAMazeParams.Text = "";
                _mazeRefreshing = false;
            }
            if (_mazeSelectionText.Length == 2)
            {
                x = Convert.ToInt32(_mazeSelectionText.Substring(0, 1));
                y = Convert.ToInt32(_mazeSelectionText.Substring(1, 1));


                _mazeSelection = -1;
                for (var i = 0; i < 9; i++)
                {
                    if (!ruleSet[i].GetCell(x - 1, y - 1).IsIdentifier) continue;
                    _mazeSelection = i;
                    break;
                }
                if (_mazeSelection == -1)
                {
                    _mazeSelectionText = "";
                    Refresh();
                    return;
                }
            }

            if (_mazeStartText.Length >= 2)
            {
                x = Convert.ToInt32(_mazeStartText.Substring(0, 1));
                y = Convert.ToInt32(_mazeStartText.Substring(1, 1));
                _mazeStartXY = (y*10) + x;
            }
            else
            {
                _mazeStartXY = 77;
            }

            if (_mazeEndText.Length >= 2)
            {
                x = Convert.ToInt32(_mazeEndText.Substring(0, 1));
                y = Convert.ToInt32(_mazeEndText.Substring(1, 1));
                _mazeEndXY = (y*10) + x;
            }
            else
            {
                _mazeEndXY = 77;
            }

            Refresh();
        }

        private static List<Maze> _morseAMazeSet;
        private static int _morseAMazeSeed=1;

        private static List<Maze> MorseAMazeSet
        {
            get
            {
                if (_morseAMazeSet != null && _morseAMazeSeed == Instance.nudVanillaSeed.Value) return _morseAMazeSet;
                _morseAMazeSeed = (int)Instance.nudVanillaSeed.Value;
                var mazeGenerator = new MazeRuleSetGenerator();
                switch (_morseAMazeSeed)
                {
                    case 1:
                        var set1 = (MazeRuleSet) mazeGenerator.GenerateRuleSet(1);
                        var set2 = (MazeRuleSet) mazeGenerator.GenerateRuleSet(2);
                        _morseAMazeSet = new List<Maze>(set1.GetMazes())
                        {
                            set2.GetMazes()[2],
                            set2.GetMazes()[3],
                            set2.GetMazes()[8],
                            set2.GetMazes()[6],
                            set2.GetMazes()[1],
                            set2.GetMazes()[0],
                            set2.GetMazes()[4],
                            set2.GetMazes()[5],
                            set2.GetMazes()[7]
                        };
                        break;
                    case 2:
                        var seed2 = (MazeRuleSet) mazeGenerator.GenerateRuleSet(2);
                        seed2.ClearMazes();
                        mazeGenerator.MakeMazes(seed2);
                        mazeGenerator.MakeMazes(seed2);
                        _morseAMazeSet = new List<Maze>(seed2.GetMazes());
                        break;
                    default:
                        var seedDefault = (MazeRuleSet) mazeGenerator.GenerateRuleSet(_morseAMazeSeed);
                        mazeGenerator.MakeMazes(seedDefault);
                        _morseAMazeSet = new List<Maze>(seedDefault.GetMazes());
                        break;
                }
                return _morseAMazeSet;
            }
        }
        private bool _useMorseAMaze;
        private void PbMaze_Paint(object sender, PaintEventArgs e)
        {
            var mazes = _useMorseAMaze ? MorseAMazeSet : RuleManager.Instance.MazeRuleSet.GetMazes();

            e.Graphics.FillRectangle(new SolidBrush(Color.Black),0,0,pbMaze.Size.Width,pbMaze.Size.Height );

	        if (_mazeSelection < 0)
            {
                for (var i = 0; i < 6; i++)
                {
                    for (var j = 0; j < 6; j++)
                    {
                        e.Graphics.DrawWall(i, j, "Up");
                        e.Graphics.DrawWall(i, j, "Down");
                        e.Graphics.DrawWall(i, j, "Left");
                        e.Graphics.DrawWall(i, j, "Right");
                        e.Graphics.FillRectangle(new SolidBrush(Color.DarkSlateGray), (j * 47) + 18, (i * 47) + 18, 10, 10);
                    }
                }
                mazeOutput.Text = "";
                rbGreenCircle.Checked = true;
                return;
            }

            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    var cell = mazes[_mazeSelection % mazes.Count].GetCell(i, j);
                    

                    if(cell.WallAbove)
                        e.Graphics.DrawWall(i, j, "Up");
                    if(cell.WallBelow)
                        e.Graphics.DrawWall(i, j, "Down");
                    if(cell.WallLeft)
                        e.Graphics.DrawWall(i, j, "Left");
                    if(cell.WallRight)
                        e.Graphics.DrawWall(i, j, "Right");

                    e.Graphics.FillRectangle(new SolidBrush(Color.DarkSlateGray), (j * 47) + 18, (i * 47) + 18, 10, 10);

                    if(cell.IsIdentifier && !_useMorseAMaze)
                        e.Graphics.DrawEllipse(new Pen(Color.Green, 3f), (i * 47) + 10, (j * 47) + 10, 47 - 20, 47 - 20);

                }
            }

            int x = _mazeEndXY % 10;
            int y = _mazeEndXY / 10;
            x--;
            y--;
            e.Graphics.FillRectangle(new SolidBrush(Color.Red), (x * 47) + 15, (y * 47) + 15, 16, 16);
            x = _mazeStartXY % 10;
            y = _mazeStartXY / 10;
            x--;
            y--;
            e.Graphics.FillRectangle(new SolidBrush(Color.White), (x * 47) + 15, (y * 47) + 15, 16, 16);


            mazeOutput.Text = "";
            _explored = new bool[60];
            _endXY = _mazeEndXY - 11;

            if (!GenerateMazeSolution(_mazeStartXY-11)) return;
            var count = _mazeStack.Count;
            for (var i=_mazeStack.Count; i > 0; i--)
            {
                if (mazeOutput.Text != "")
                    mazeOutput.Text += @", ";
                mazeOutput.Text += _mazeStack.Pop();
            }
            if (mazeOutput.Text == "") return;
            mazeOutput.Text += Environment.NewLine + Environment.NewLine +
                               @"Total Steps: " + count;

        }

        private readonly Stack<string> _mazeStack = new Stack<string>(); 
        private bool [] _explored = new bool[60];
        private int _endXY;
        private bool GenerateMazeSolution(int startXY)
        {
            var mazes = _useMorseAMaze ? MorseAMazeSet : RuleManager.Instance.MazeRuleSet.GetMazes();
            var maze = _mazeSelection;
            var x = startXY%10;
            var y = startXY/10;

            if ((x > 5) || (y > 5) || (maze == -1) || (_endXY == 66)) return false;
            var cell = mazes[maze % mazes.Count].GetCell(x, y);
            if (startXY == _endXY) return true;
            _explored[startXY] = true;


            var wallDirection = new[] {cell.WallAbove, cell.WallBelow, cell.WallLeft, cell.WallRight};
            var directionInt = new[] {-10, 10, -1, 1};
            var directionReturn = new[] {"Up", "Down", "Left", "Right"};

            for (var i = 0; i < 4; i++)
            {
                if (wallDirection[i]) continue;
                if (_explored[startXY + directionInt[i]]) continue;
                if (!GenerateMazeSolution(startXY + directionInt[i])) continue;
                _mazeStack.Push(directionReturn[i]);
                return true;
            }

            return false;
        }

        private void PbMaze_Click(object sender, EventArgs e)
        {
            var me = (MouseEventArgs) e;
            var coordinates = me.Location;
            var x = coordinates.X/47;
            var y = coordinates.Y/47;
            x++;
            y++;
            var xy = (x*10) + y;
            if (rbGreenCircle.Checked)
            {
                rbStart.Checked = cbAutoAdvance.Checked;
                _mazeSelectionText = xy.ToString();
            }
            else if (rbStart.Checked)
            {
                _mazeStartText = xy.ToString();
                rbEnd.Checked = cbAutoAdvance.Checked;
            }
            else
            {
                _mazeEndText = xy.ToString();
                rbGreenCircle.Checked = cbAutoAdvance.Checked;
            }
            MazeSelection_TextChanged();
        }

        private void WofStep1_CheckedChanged(object sender, EventArgs e)
        {
            whosOnFirstStep1.Visible = !wofStep1.Checked;
            wofOutput.Visible = wofStep1.Checked;
            if (!wofStep1.Checked) return;

            _whosOnFirstLookIndex = 6;
            wofStep1Label.Text = "";
            RbWOF_CheckedChanged(sender, e);
            for (var i = 0; i < 28; i++)
            {
                ((Button) wofButtons.Controls[i]).Text = WhosOnFirstComponent.Instance.WhosOnFirstStep1WordList[i];
                ((Button)wofButtons.Controls[i]).Tag = WhosOnFirstComponent.Instance.WhosOnFistStep1Index(WhosOnFirstComponent.Instance.WhosOnFirstStep1WordList[i]);
            }
        }

        private void WofStep2_CheckedChanged(object sender, EventArgs e)
        {
            if (!wofStep2.Checked) return;
            wofOutput.Text = "";
            wofStep2Label.Text = "";
            for (var i = 0; i < 28; i++)
            {
                ((Button)wofButtons.Controls[i]).Text = WhosOnFirstComponent.Instance.WhosOnFirstStep2WordList[i];
                ((Button)wofButtons.Controls[i]).Tag = WhosOnFirstComponent.Instance.Step2WordOrder(WhosOnFirstComponent.Instance.WhosOnFirstStep2WordList[i]);
            }
        }

        private void WofButton_Click(object sender, EventArgs e)
        {
            var text = ((Button)sender).Text;
            var tag = ((Button) sender).Tag;

            if (wofStep1.Checked)
            {
                _whosOnFirstLookIndex = (int) tag;
                RbWOF_CheckedChanged(sender, e);
                wofStep1Label.Text = text;
                wofStep2.Checked = true;
            }
            else
            {
                var wordlist = (string[]) tag;
                wofStep2Label.Text = text;
                for (var i = 0; i < 9; i++)
                {
                    wofOutput.Text += wordlist[i] + Environment.NewLine;
                    if (wordlist[i].Equals(text)) break;
                }
                wofStep1.Checked = true;
            } 
        }

        private void RbWOF_CheckedChanged(object sender, EventArgs e)
        {
            var buttons = new[] {rbWOF1, rbWOF2, rbWOF3, rbWOF4, rbWOF5, rbWOF6, rbWOF6};
            buttons[_whosOnFirstLookIndex].Checked = true;
        }

        private void Cw_reset_Click(object sender, EventArgs e)
        {
            cw_input.Text = "";
        }

        private void Cw_all_wires_Click(object sender, EventArgs e)
        {
            cw_input.Text = @"W WS WL WSL\R RS RL RSL\B BS BL BSL\RB RBS RBL RBSL";
        }


	    private readonly string[] _keypadSelection = {"","","","","","","",""};
        private readonly Bitmap[] _keypadImages = {new Bitmap(1, 1), new Bitmap(1, 1), new Bitmap(1, 1), new Bitmap(1, 1), new Bitmap(1, 1), new Bitmap(1, 1), new Bitmap(1, 1), new Bitmap(1, 1)};

        private void KeypadReset_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < 31; i++)
            {
                ((Button) fpKeypadSymbols.Controls[i]).BackgroundImage = SymbolPool.GetImage(SymbolPool.Symbols[i]);
                ((Button) fpKeypadSymbols.Controls[i]).Tag = SymbolPool.Symbols[i];
            }

            for (var i = 0; i < 6; i++)
            {
	            ((Button)fpKeypadOrder.Controls[i]).Visible = false;
            }

	        for (var i = 0; i < 8; i++)
            {
                ((Button) fpKeypadSelection.Controls[i]).Tag = _keypadSelection[i] = "";
                ((Button) fpKeypadSelection.Controls[i]).BackgroundImage = _keypadImages[i] = new Bitmap(1, 1);
            }

            fpKeypadLabel.Visible = false;
        }

        private static readonly KeypadRuleSet RoundKeypadRules = (KeypadRuleSet) new KeypadRuleSetGenerator().GenerateRuleSet(1);

        private void KeypadSymbol_Click(object sender, EventArgs e)
        {
            var max = cbShowAddonModules.Checked ? 8 : 4;

            for (var i = 0; i < max; i++)
            {
                if ((string) (((Button) sender).Tag) != _keypadSelection[i]) continue;
                KeypadSelection_Click(fpKeypadSelection.Controls[i], e);
                break;
            }

            for (var i = max - 1; i > 0; i--)
            {
                _keypadSelection[i] = _keypadSelection[i - 1];
                _keypadImages[i] = _keypadImages[i - 1];
            }

            _keypadSelection[0] = (string)((Button) sender).Tag;
            _keypadImages[0] = (Bitmap)((Button) sender).BackgroundImage;

            for (var i = 0; i < max; i++)
            {
                ((Button) fpKeypadSelection.Controls[i]).Tag = _keypadSelection[i];
                ((Button) fpKeypadSelection.Controls[i]).BackgroundImage = _keypadImages[i];
            }


            if (_keypadSelection[3] == "") return;
            var keypadFound = true;
            if (_keypadSelection[4] == "")
            {
                for (var i = 0; i < 6; i++)
                {
                    keypadFound = true;
                    for (var j = 0; keypadFound && j < 4; j++)
                    {
                        keypadFound = RuleManager.Instance.KeypadRuleSet.PrecedenceLists[i].Contains(_keypadSelection[j]);
                    }
                    if (!keypadFound) continue;
                    var order = new Dictionary<int, string>();
                    for (var j = 0; j < 4; j++)
                    {
                        order.Add(RuleManager.Instance.KeypadRuleSet.PrecedenceLists[i].IndexOf(_keypadSelection[j]),
                            _keypadSelection[j]);
                    }
                    var k = 0;
                    for (var j = 0; j < 7 && k < 4; j++)
                    {
						if (!order.TryGetValue(j, out string result)) continue;
						((Button) fpKeypadOrder.Controls[k]).Visible = true;
                        ((Button) fpKeypadOrder.Controls[k]).BackgroundImage = SymbolPool.GetImage(result);
                        ((Button) fpKeypadOrder.Controls[k++]).Tag = result;

                    }
                    break;
                }
                fpKeypadLabel.Visible = keypadFound;
                fpKeypadLabel.Text = @"Push the Keypad in this Order";
                if (keypadFound) return;
                for (var i = 0; i < 6; i++)
                {
                    ((Button)fpKeypadOrder.Controls[i]).Visible = false;
                    ((Button) fpKeypadOrder.Controls[i]).BackgroundImage = new Bitmap(1, 1);
                }
                
            }
            else
            {
                if (!cbShowAddonModules.Checked) return;
                if (_keypadSelection[7] == "") return;
                fpKeypadLabel.Visible = true;
                fpKeypadLabel.Text = @"Push the following keys";
                var keypadsmatched = new int[6];
                for (var i = 0; i < 6; i++)
                {
                    for (var j = 0; j < 8; j++)
                    {
                        if (RoundKeypadRules.PrecedenceLists[i].Contains(_keypadSelection[j]))
                            keypadsmatched[i]++;
                    }
                }
                var keypadtouse = 6;
                var keysfound = 0;
                for (var i = 0; i < 6; i++)
                {
                    if (keypadsmatched[5 - i] <= keysfound) continue;
                    keypadtouse = 5 - i;
                    keysfound = keypadsmatched[5 - i];
                }
                var k = 0;
                for (var i = 0; i < 8 && k < 6; i++)
                {
                    if (RoundKeypadRules.PrecedenceLists[keypadtouse].Contains(_keypadSelection[i])) continue;
                    ((Button) fpKeypadOrder.Controls[k]).Visible = true;
                    ((Button) fpKeypadOrder.Controls[k]).BackgroundImage = _keypadImages[i];
                    ((Button) fpKeypadOrder.Controls[k++]).Tag = _keypadSelection[i];
                }
            }
            
        }

        private void KeypadOrder_Click(object sender, EventArgs e)
        {

        }

        private void KeypadSelection_Click(object sender, EventArgs e)
        {
            var i = 0;
            var selection = new string[7];
            var images = new Bitmap[7];
            for (var j = 0; j < 8; j++)
            {
                if (_keypadSelection[j] == (string) ((Button) sender).Tag) continue;
                images[i] = _keypadImages[j];
                selection[i++] = _keypadSelection[j];
            }
            for (var j = 0; j < 7; j++)
            {
                _keypadSelection[j] = selection[j];
                _keypadImages[j] = images[j];
            }
            _keypadSelection[7] = "";
            _keypadImages[7] = new Bitmap(1, 1);
            for (var j = 0; j < 8; j++)
            {
                ((Button) fpKeypadSelection.Controls[j]).Tag = _keypadSelection[j];
                ((Button) fpKeypadSelection.Controls[j]).BackgroundImage = _keypadImages[j];
            }
            for (var j = 0; j < 6; j++)
            {
	            ((Button)fpKeypadOrder.Controls[j]).Visible = false;
            }
	        fpKeypadLabel.Visible = false;
        }

        private int GetDigitFromCharacter(string text)
        {
			if (int.TryParse(text, out int result)) return result;
			return -1;
        }

        private int NumberUnlitIndicators()
        {
            return UnlitIndicators.Length;
        }

        private int NumberLitIndicators()
        {
            return LitIndicators.Length;
        }

        private void ForgetMeNot_Event(object sender, EventArgs e)
        {
            txtForgetMeNotOut.Text = "";
            if (!IsSerialValid)
            {
                txtForgetMeNotOut.Text = @"The Full serial number is needed to calculate the solution";
                return;
            }
            var smallestOdd = 9;
            var largestDigit = 0;
            var numDigits = 0;
            var lastDigit = 0;
            for (var i = 0; i < Serial.Length; i++)
            {
                var num = GetDigitFromCharacter(Serial.Substring(i, 1));
                if (num == -1) continue;
                numDigits++;
                if((num%2)==1)
                    if (num < smallestOdd)
                        smallestOdd = num;
                if (num > largestDigit)
                    largestDigit = num;
                lastDigit = num;
            }
            foreach (var line in txtForgetMeNotIn.Text.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
            {
                

                var unlit = NumberUnlitIndicators();
                var lit = NumberLitIndicators();
                var solutionStr = "";
                var tpLines = line.Split(':');
                if (tpLines.Length > 2) continue;
                var tpID = "";
                var lineData = tpLines[0];

                if (tpLines.Length == 2)
                {
                    tpID = tpLines[0];
                    lineData = tpLines[1];
                }
                var fmnNumbers = new List<int>();
                foreach (var c in lineData)
                {
					if (int.TryParse(c.ToString(), out int test))
						fmnNumbers.Add(test);
				}

                var solution = new int[fmnNumbers.Count];
                


                for (var i = 0; i < fmnNumbers.Count; i++)
                {
                    var num = fmnNumbers[i];
                    switch (i)
                    {
                        case 0:
                            if (IsIndicatorUnlit("CAR"))
                                solution[i] = num + 2;
                            else if (unlit > lit)
                                solution[i] = num + 7;
                            else if (unlit == 0)
                                solution[i] = num + lit;
                            else
                                solution[i] = num + lastDigit;
                            break;
                        case 1:
                            if (IsPortPresent(PortTypes.Serial) && (numDigits >= 3))
                                solution[i] = num + 3;
                            else if ((solution[i - 1] % 2) == 0)
                                solution[i] = num + solution[i - 1] + 1;
                            else
                                solution[i] = num + solution[i - 1] - 1;
                            break;
                        default:
                            if ((solution[i - 1] == 0) || (solution[i - 2] == 0))
                            {
	                            solution[i] = num + largestDigit;
                            }
                            else if (((solution[i - 1] % 2) == 0) && ((solution[i - 2] % 2) == 0))
                            {
	                            solution[i] = num + smallestOdd;
                            }
                            else
                            {
                                var x = solution[i - 1] + solution[i - 2];
                                while (x >= 10)
                                {
	                                x /= 10;
                                }
	                            solution[i] = num + x;
                            }
                            break;
                    }
                    solution[i] %= 10;
                    solutionStr += solution[i];
                    if (i % 3 == 2)
                        solutionStr += " ";
                }
                if (tpID == "")
                    txtForgetMeNotOut.Text += solutionStr.Trim() + Environment.NewLine;
                else
                    txtForgetMeNotOut.Text += @"!" + tpID.Trim() + @" press " + solutionStr.Trim() + Environment.NewLine;
            }
        }

        

        private void TxtTwoBitsIN_TextChanged(object sender, EventArgs e)
        {
            var lookup = GetDigitFromCharacter(txtTwoBitsIN.Text);
            txtTwoBitsOUT.Text = TwoBits.Instance.TwoBitsLookup(lookup);
        }

        

        private bool IsInputValid(string input, string pattern, bool serialRequired=false)
        {
            if (serialRequired && !IsSerialValid) return false;
            return new Regex(pattern).IsMatch(input);
        }

        private void TxtConnections_TextChanged(object sender, EventArgs e)
        {
            txtConnectionCheckOut.Text = "";
            if (!IsInputValid(txtConnections.Text, "[1-8]{2} [1-8]{2} [1-8]{2} [1-8]{2}",true)) return;

            var batts = Batteries.TotalBatteries;
            var serials = new List<string> {"7HPJ", "34XYZ", "SLIM", "15BRO", "20DGT", "8CAKE", "9QVN", "6WUF"};
            var connectionPairs = new List<string>
            {
                "12-13-21-23-31-32-46-47-56-57-64-65-74-75",
                "12-14-16-21-23-24-32-41-42-47-56-61-65-67-68-74-76-78-86-87",
                "12-13-16-21-26-31-34-36-43-45-46-47-48-54-56-57-61-62-63-64-65-74-75-78-84-87",
                "12-17-21-27-34-38-43-48-56-57-65-67-71-72-75-76-83-84",
                "12-13-21-24-27-31-35-37-42-46-47-48-53-56-57-64-65-68-72-73-74-75-84-86",
                "12-13-16-18-21-24-26-31-34-36-37-38-42-43-45-46-47-54-57-58-61-62-63-64-73-74-75-78-81-83-85-87",
                "12-14-18-21-23-26-27-32-34-36-37-41-43-45-54-56-58-62-63-65-67-72-73-76-78-81-85-87",
                "12-16-17-21-23-27-28-32-35-36-38-45-47-48-53-54-56-57-61-63-65-67-71-72-74-75-76-78-82-83-84-87"
            };
            var entries = txtConnections.Text.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            
            int serialdigit;
            var counts = new int[8];
            var digits = new[] {'1', '2', '3', '4', '5', '6', '7', '8'};
            for (var i = 0; i < 8; i++)
            {
	            counts[i] = txtConnections.Text.Count(f => f == digits[i]);
            }
	        var unique = 0;
            for (var i = 0; i < 8; i++)
            {
	            if (counts[i] > 0)
		            unique++;
            }

	        if (unique == 8)
                serialdigit = 6;
            else if (counts[0] > 1)
                serialdigit = 1;
            else if (counts[6] > 1)
                serialdigit = 6;
            else if (counts[1] >= 3)
                serialdigit = 2;
            else if (counts[4] == 0)
                serialdigit = 5;
            else if (counts[7] == 2)
                serialdigit = 3;
            else if (batts > 6 || batts == 0)
                serialdigit = 6;
            else
                serialdigit = batts;

            var serialchar = Serial.Substring(serialdigit - 1, 1);
            var group = -1;
            for(var i=0;i<8 && group == -1;i++)
            {
	            if (serials[i].Contains(serialchar))
		            group = i;
            }

	        if (group == -1) return;

            var sanity = "";
            foreach (var entry in entries)
            {
                txtConnectionCheckOut.Text += connectionPairs[group].Contains(entry)
                    ? "G "
                    : "R ";

                sanity += connectionPairs[group].Contains(entry.Substring(1, 1) + entry.Substring(0, 1))
                    ? "G "
                    : "R ";
            }
            if (txtConnectionCheckOut.Text != sanity)
                txtConnectionCheckOut.Text += @"(POSSIBLY INVALID)";    //Should not happen.

        }

        private void CalculateSafetySafe()
        {
            var safeOffsets = new Dictionary<string, int[]>
            {
                {"A", new []{8,3,4,8,9,0 } },
                {"B", new []{10,1,3,7,3,8} },
                {"C", new []{2,1,1,5,3,6} },
                {"D", new []{11,6,11,11,7,7} },
                {"E", new []{0,5,5,8,2,1} },
                {"F", new []{4,2,7,7,1,5} },
                {"G", new []{7,4,4,2,10,5} },
                {"H", new []{8,3,6,6,6,5} },
                {"I", new []{0,11,0,0,9,10} },
                {"J", new []{2,11,8,0,5,6} },
                {"K", new []{5,2,5,1,0,4} },
                {"L", new []{1,9,8,11,11,11} },
                {"M", new []{1,7,9,5,6,2} },
                {"N", new []{9,5,1,4,4,9} },
                {"O", new []{5,9,8,10,2,8} },
                {"P", new []{3,10,9,1,9,7} },
                {"Q", new []{4,10,6,1,4,8} },
                {"R", new []{8,0,4,0,6,11} },
                {"S", new []{9,4,0,6,3,10} },
                {"T", new []{7,6,7,11,5,3} },
                {"U", new []{11,9,6,3,11,1} },
                {"V", new []{11,11,2,8,1,0} },
                {"W", new []{6,0,11,6,11,2} },
                {"X", new []{4,2,7,2,8,10} },
                {"Y", new []{10,7,10,10,8,9} },
                {"Z", new []{3,7,1,10,0,4} },
                {"0", new []{7,0,3,5,8,6} },
                {"1", new []{9,10,10,9,1,2} },
                {"2", new []{2,5,11,7,7,3} },
                {"3", new []{10,8,10,4,10,4} },
                {"4", new []{6,8,0,3,5,0} },
                {"5", new []{6,3,3,3,0,11} },
                {"6", new []{1,1,5,2,7,3} },
                {"7", new []{0,6,2,4,2,1} },
                {"8", new []{5,4,9,9,10,7} },
                {"9", new []{3,8,2,9,4,9} }

            };
            txtSafetySafe.Text = "";
            if (!IsSerialValid) return;

            var offset = CountUniquePorts()*7;

            foreach (var indicator in LitIndicators)
            {
                if (indicator.Any(t => Serial.Contains(t)))
                {
                    offset += 5;
                }
            }

            foreach (var indicator in UnlitIndicators)
            {
                if (indicator.Any(t => Serial.Contains(t)))
                {
                    offset += 1;
                }
            }

            for (var i = 0; i < 6; i++)
            {
                var x = offset;

                try
                {
                    x += safeOffsets[Serial.Substring(i, 1).ToUpper()][i];
                }
                catch
                {
                    return;
                }

                if (i == 5)
                    for (var j = 0; j < 5; j++)
	                    {
		                    x += safeOffsets[Serial.Substring(j, 1).ToUpper()][5];
	                    }

	            x %= 12;
                txtSafetySafe.Text += x + @" ";
            }

        }


        //Anything that depends on bomb information will be called by this function
        //the moment said information is updated.
        private void UpdateBombSolution(object sender, EventArgs e)
        {
            if (_resetBomb) return;
            
            //--- Bomb information side box ---//
            //Needy Knobs does not depend on information, just manual version alone.
            ParsePortPlates(txtPortPlates.Text);
            SetSerialNumber(txtSerialNumber.Text);
            SetIndicators(txtLitIndicators.Text, txtUnlitIndicators.Text);

            if (!IsSerialValid && Serial.Length != 0)
                txtSerialNumber.BackColor = Color.Red;
            else
                txtSerialNumber.BackColor = default(Color);

            Needy_Knob_CheckedChanged(null, null);
            Batteries.SetBatteries(txtBatteries.Text);

            //--- The Button ---//
            Button_Event(null, null);

            //--- Keypads, Simon Says ---//
            //keypad does not depend on bomb info
            Simon_Says_Event();

            //--- Who's on First, Memory ---//
            //Nothing on this tab depends on bomb information

            //--- Mazes ---//
            //Nothing on this tab depends on bomb information

            //--- Simple Wires, Complicated wires, Wire Sequences ---//
            SimpleWires_Event(null, null);
            Complicated_Wires_Event(null, null);
            //Wire sequences does not depend on bomb information

            //--- Passwords, Morse Code ---//
            //Nothing on this tab depends on bomb information
            Password_TextChanged(null, null);
            MorseCodeInput_TextChanged(null, null);

            //--- Caesar Cipher, Combination Lock, Number Pads, Resistors, Semaphore ---//
            TxtCaesarCipherIn_TextChanged(null, null);
            TxtCombinationLockIn_TextChanged(null, null);
            TxtNumberPadIn_TextChanged(null, null);
            TxtResistorsIn_TextChanged(null, null);
            TxtSemaphoreIn_TextChanged(null, null);

            //--- Battleship, Blind Alley, Chess, Connection Check, Emoji Math, Flashing Colors, Lettered Keys, Logic, Plumbing, Safety Safe, Two Bits ---//
            TxtChessInput_TextChanged(null, null);
            TxtConnections_TextChanged(null, null);
            //Emoji Math does not depend on bomb information
            //Flashing Colors does not depend on bomb information
            TxtLetteredKeysIn_TextChanged(null, null);
            TxtLogicAND_TextChanged(txtLogicAND, null); TxtLogicAND_TextChanged(txtLogicOR, null);
            CbPlumbingRedIn_CheckedChanged();
            CalculateSafetySafe();
            txtTwoBitsInitialValue.Text = TwoBits.Instance.CalculateInitialTwoBitsCode();
            CbBlindAlleyTM_CheckedChanged();
            TxtBattleShipSafeSpots_TextChanged();

            //--- Adventure Game, Alphabet, Anagram, Silly Slots, Word Scramble ---//
            TxtAdventureGameSTR_TextChanged(null, null);

            //--- Cryptography, Gamepad, Light Cycle, Microcontrollers, Murder, Skewed Slots ---//
            CbMurderRoom_SelectedIndexChanged(null, null);
            CbMicrocontroller_SelectedIndexChanged(null, null);
            TxtGamePadX_TextChanged(null, null);
            TxtLightCycleIn_TextChanged(null, null);
            TxtSkewedIn_TextChanged(null, null);
            ComputeBitWiseOperators();
            TxtFizzBuzz1IN_TextChanged(null, null);

            //--- Forget Me Not ---//
            ForgetMeNot_Event(null, null);

            //--- 3D Maze  (and anything else that requires picture box refresh) ---//
            if(lbModules.SelectedItem?.ToString().Trim() == "3D Maze")
                Refresh();

            TxtTwoBitsIN_TextChanged(null, null);

            //--- Laundry ---//
            TxtLaundryIn_TextChanged(null, null);
            CbAcidColor_SelectedIndexChanged(null, null);

	        // ReSharper disable once InconsistentNaming
            var RPSLS = new RockPaperScissorsLizardSpock();
            txtRPSLSPrimary.Text = RPSLS.GetPrimaryAnswer();
            txtRPSLSSecondary.Text = RPSLS.GetSecondaryAnswer();
            txtRPSLSDecoy.Text = RPSLS.GetPrimaryDecoy();

            TxtRubiksCubeIn_TextChanged(null, null);
            TxtMorseMaticsIN_TextChanged(null, null);
        }

        private void CbPlumbingRedIn_CheckedChanged()
        {
            var countFor = 0;
            var countAgainst = 0;

            var inputActive = 0;
            var outputActive = 0;

            if (Serial.Contains("1"))
                countFor++;
            if (PortCount(PortTypes.RJ45) == 1)
                countFor++;
            if (DuplicatePorts())
                countAgainst++;
            if (DuplicateSerialCharacters())
                countAgainst++;

            cbPlumbingRedIn.Checked = countFor > countAgainst;
            inputActive += countFor > countAgainst?1:0;
            countFor = countAgainst = 0;

            if (Serial.Contains("2"))
                countFor++;
            if (IsPortPresent(PortTypes.StereoRCA))
                countFor++;
            if (!DuplicatePorts())
                countAgainst++;
            if (Serial.Contains("1") || Serial.Contains("L"))
                countAgainst++;

            cbPlumbingYellowIn.Checked = countFor > countAgainst;
            inputActive += countFor > countAgainst ? 1 : 0;
            countFor = countAgainst = 0;

            if (CountSerialNumberDigits() >= 3)
                countFor++;
            if (IsPortPresent(PortTypes.DVI))
                countFor++;
            if (!cbPlumbingRedIn.Checked)
                countAgainst++;
            if (!cbPlumbingYellowIn.Checked)
                countAgainst++;

            cbPlumbingGreenIn.Checked = countFor > countAgainst;
            inputActive += countFor > countAgainst ? 1 : 0;
            countFor = countAgainst = 0;

            if (inputActive > 0)
            {
                if (CountUniquePorts() >= 4)
                    countFor++;
                if (Batteries.TotalBatteries >= 4)
                    countFor++;
                if (CountUniquePorts() == 0)
                    countAgainst++;
                if (Batteries.TotalBatteries == 0)
                    countAgainst++;

                cbPlumbingBlueIn.Checked = countFor > countAgainst;
                inputActive += countFor > countAgainst ? 1 : 0;
                countFor = countAgainst = 0;
            }
            else
            {
                inputActive++;
                cbPlumbingBlueIn.Checked = true;
            }

            if (IsPortPresent(PortTypes.Serial))
                countFor++;
            if (Batteries.TotalBatteries == 1)
                countFor++;
            if (CountSerialNumberDigits() > 2)
                countAgainst++;
            if (inputActive > 2)
                countAgainst++;

            cbPlumbingRedOut.Checked = countFor > countAgainst;
            outputActive += countFor > countAgainst ? 1 : 0;
            countFor = countAgainst = 0;

            if (DuplicatePorts())
                countFor++;
            if (Serial.Contains("4") || Serial.Contains("8"))
                countFor++;
            if (!Serial.Contains("2"))
                countAgainst++;
            if (cbPlumbingGreenIn.Checked)
                countAgainst++;

            cbPlumbingYellowOut.Checked = countFor > countAgainst;
            outputActive += countFor > countAgainst ? 1 : 0;
            countFor = countAgainst = 0;

            if (inputActive == 3)
                countFor++;
            if (CountTotalPorts() == 3)
                countFor++;
            if (CountTotalPorts() < 3)
                countAgainst++;
            if (CountSerialNumberDigits() > 3)
                countAgainst++;

            cbPlumbingGreenOut.Checked = countFor > countAgainst;
            outputActive += countFor > countAgainst ? 1 : 0;
            countFor = countAgainst = 0;

            if (outputActive > 0)
            {
                if (inputActive == 4)
                    countFor++;
                if (outputActive < 3)
                    countFor++;
                if (Batteries.TotalBatteries < 2)
                    countAgainst++;
                if (!IsPortPresent(PortTypes.Parallel))
                    countAgainst++;

                cbPlumbingBlueOut.Checked = countFor > countAgainst;
            }
            else
            {
                cbPlumbingBlueOut.Checked = true;
            }


        }

        private bool _resetBomb;
        private void Button42_Click(object sender, EventArgs e)
        {
            _resetBomb = true;
            txtPortPlates.Text = Empty;
            txtLitIndicators.Text = Empty;
            txtUnlitIndicators.Text = Empty;
            txtBatteries.Text = Empty;
            facts_strike.SelectedIndex = 0;
            txtSerialNumber.Text = "";
            _resetBomb = false;
            UpdateBombSolution(null, null);
        }

        private Dictionary<string, bool> BuildTruthTable()
        {
            var batts = Batteries.TotalBatteries;
            return new Dictionary<string, bool>
            {
                {"A", batts == (LitIndicators.Length + UnlitIndicators.Length)},
                {"B", SerialNumberLetters.Length > SerialNumberDigits.Length },
                {"C", IsIndicatorPresent("IND") },
                {"D", IsIndicatorPresent("FRK") },
                {"E", UnlitIndicators.Length == 1 },
                {"F", CountUniquePorts() > 1 },
                {"G", batts >= 2 },
                {"H", batts < 2 },
                {"I", SerialNumberLastDigitOdd() },
                {"J", batts > 4 },
                {"K", LitIndicators.Length == 1 },
                {"L", (LitIndicators.Length + UnlitIndicators.Length) > 2 },
                {"M", !DuplicatePorts() },
                {"N", Batteries.Holders.Length > 2 },
                {"O", LitIndicators.Length > 0 && UnlitIndicators.Length > 0 },
                {"P", IsPortPresent(PortTypes.Parallel) },
                {"Q", CountTotalPorts() == 2 },
                {"R", IsPortPresent(PortTypes.PS2) },
                {"S", SerialNumberSum > 10 },
                {"T", IsIndicatorPresent("MSA") },
                {"U", Batteries.Holders.Length == 1 },
                {"V", SerialNumberContainsVowel() },
                {"W", (LitIndicators.Length + UnlitIndicators.Length) == 0 },
                {"X", (LitIndicators.Length + UnlitIndicators.Length) == 1},
                {"Y", CountTotalPorts() > 5 },
                {"Z", CountTotalPorts() < 2 }
            };
        }

        private void TxtLogicAND_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is MaskedTextBox)) return;
            MaskedTextBox maskedTextBox = (MaskedTextBox) sender;

            string text = maskedTextBox.Text.ToUpperInvariant().Trim();
            if (text.Length < 5 || (text[1] != 'A' && text[1] != 'O') || (text[3] != 'A' && text[3] != 'O'))
            {
                maskedTextBox.ResetBackColor();
                return;
            }
            var truth = BuildTruthTable();

            bool andOr = true;
            if (text[1] == 'A')
                andOr &= (truth[text[0].ToString()] && truth[text[2].ToString()]);
            else
                andOr &= (truth[text[0].ToString()] || truth[text[2].ToString()]);

            if (text[3] == 'A')
                andOr &= truth[text[4].ToString()];
            else
                andOr |= truth[text[0].ToString()];

            maskedTextBox.BackColor = andOr ? Color.Green : Color.Red;
        }

        private void TxtChessInput_TextChanged(object sender, EventArgs e)
        {
            var positionsLetters = "ABCDEF";
            var positionNumbers = "123456";
            var whitefields = "B1,D1,F1,A2,C2,E2,B3,D3,F3,A4,C4,E4,B5,D5,F5,A6,C6,E6";
            var chessboard = new int[6, 6];
            var knightMovesX = new [] {1, 2, 2, 1, -1, -2, -2, -1};
            var knightMovesY = new [] {2, 1, -1, -2, -2, -1, 1, 2};
            var kingMovesX = new[] {1, 1, 1, 0, -1, -1, -1, 0};
            var kingMovesY = new[] {1, 0, -1, -1, -1, 0, 1, 1};
            txtChessSolution.Text = "";

            if (!(new Regex("[A-F][1-6] [A-F][1-6] [A-F][1-6] [A-F][1-6] [A-F][1-6] [A-F][1-6]")
                    .IsMatch(txtChessInput.Text.ToUpper())))
                return;
            var inputs = txtChessInput.Text.ToUpper().Split(' ');
            /*
            if (txtChessInput.TextLength < 17 || txtChessInput.Text.Split(' ').Length < 6)
                return;
            
            if (inputs.Any(x => x.Length < 2
                || !positionsLetters.Contains(x.Substring(0, 1))
                || !positionNumbers.Contains(x.Substring(1, 1))))
                return;*/


            var pieces = new int[6];
            //{
            //    whitefields.Contains(inputs[4])                                     ? ChessPieces.King : ChessPieces.Bishop,
            //    SerialNumberLastDigitOdd()                                          ? ChessPieces.Rook : ChessPieces.Knight,
            //    !SerialNumberLastDigitOdd() && !whitefields.Contains(inputs[4])     ? ChessPieces.Queen : ChessPieces.King,
            //    /* Always a Rook */                                                   ChessPieces.Rook,
            //    whitefields.Contains(inputs[4])                                     ? ChessPieces.Queen : ChessPieces.Rook,
            //                                                                          ChessPieces.None
            //};
            pieces[3] = ChessPieces.Rook;
            pieces[1] = SerialNumberLastDigitOdd() ? ChessPieces.Rook : ChessPieces.Knight;
            pieces[4] = whitefields.Contains(inputs[4]) ? ChessPieces.Queen : ChessPieces.Rook;
            pieces[2] = (pieces[1] != ChessPieces.Rook && pieces[4] != ChessPieces.Rook)
                ? ChessPieces.Queen
                : ChessPieces.King;
            pieces[0] = pieces[4] == ChessPieces.Queen
                ? ChessPieces.King
                : ChessPieces.Bishop;
            

            if (pieces[2] != ChessPieces.Queen && pieces[4] != ChessPieces.Queen) pieces[5] = ChessPieces.Queen;
            else if (pieces[1] != ChessPieces.Knight) pieces[5] = ChessPieces.Knight;
            else pieces[5] = ChessPieces.Bishop;

            for (var i = 0; i < 6; i++)
            {
                var x = positionsLetters.IndexOf(inputs[i].Substring(0, 1), StringComparison.Ordinal);
                var y = positionNumbers.IndexOf(inputs[i].Substring(1, 1), StringComparison.Ordinal);
                chessboard[x, y] = 1;
            }


            for (var i = 0; i < 6; i++)
            {
                var x = positionsLetters.IndexOf(inputs[i].Substring(0, 1), StringComparison.Ordinal);
                var y = positionNumbers.IndexOf(inputs[i].Substring(1, 1), StringComparison.Ordinal);
                int j,k;

                switch (pieces[i])
                {
                    case ChessPieces.Rook:
                        for (j = x + 1; j < 6 && chessboard[j, y] != 1; j++)
                        {
	                        chessboard[j, y] = 2;
                        }
	                    for (j = x - 1; j >= 0 && chessboard[j, y] != 1; j--)
	                    {
		                    chessboard[j, y] = 2;
	                    }
	                    for (j = y + 1; j < 6 && chessboard[x, j] != 1; j++)
	                    {
		                    chessboard[x, j] = 2;
	                    }
	                    for (j = y - 1; j >= 0 && chessboard[x, j] != 1; j--)
	                    {
		                    chessboard[x, j] = 2;
	                    }
	                    break;
                    case ChessPieces.Knight:
                        for (j = 0; j < 8; j++)
                        {
                            var xx = knightMovesX[j] + x;
                            var yy = knightMovesY[j] + y;
                            if (xx >= 0 && xx < 6 && yy >= 0 && yy < 6 && chessboard[xx, yy] != 1)
                                chessboard[xx, yy] = 2;
                        }
                        break;
                    case ChessPieces.Bishop:
                        for (j = x + 1, k = y + 1; j < 6 && k < 6 && chessboard[j, k] != 1; j++, k++)
                        {
	                        chessboard[j, k] = 2;
                        }
	                    for (j = x - 1, k = y + 1; j >= 0 && k < 6 && chessboard[j, k] != 1; j--, k++)
	                    {
		                    chessboard[j, k] = 2;
	                    }
	                    for (j = x + 1, k = y - 1; j < 6 && k >= 0 && chessboard[j, k] != 1; j++, k--)
	                    {
		                    chessboard[j, k] = 2;
	                    }
	                    for (j = x - 1, k = y - 1; j >= 0 && k >= 0 && chessboard[j, k] != 1; j--, k--)
	                    {
		                    chessboard[j, k] = 2;
	                    }
	                    break;
                    case ChessPieces.Queen:
                        for (j = x + 1; j < 6 && chessboard[j, y] != 1; j++)
                        {
	                        chessboard[j, y] = 2;
                        }
	                    for (j = x - 1; j >= 0 && chessboard[j, y] != 1; j--)
	                    {
		                    chessboard[j, y] = 2;
	                    }
	                    for (j = y + 1; j < 6 && chessboard[x, j] != 1; j++)
	                    {
		                    chessboard[x, j] = 2;
	                    }
	                    for (j = y - 1; j >= 0 && chessboard[x, j] != 1; j--)
	                    {
		                    chessboard[x, j] = 2;
	                    }
	                    for (j = x + 1, k = y + 1; j < 6 && k < 6 && chessboard[j, k] != 1; j++, k++)
	                    {
		                    chessboard[j, k] = 2;
	                    }
	                    for (j = x - 1, k = y + 1; j >= 0 && k < 6 && chessboard[j, k] != 1; j--, k++)
	                    {
		                    chessboard[j, k] = 2;
	                    }
	                    for (j = x + 1, k = y - 1; j < 6 && k >= 0 && chessboard[j, k] != 1; j++, k--)
	                    {
		                    chessboard[j, k] = 2;
	                    }
	                    for (j = x - 1, k = y - 1; j >= 0 && k >= 0 && chessboard[j, k] != 1; j--, k--)
	                    {
		                    chessboard[j, k] = 2;
	                    }
	                    break;
                    case ChessPieces.King:
                        for (j = 0; j < 8; j++)
                        {
                            var xx = kingMovesX[j] + x;
                            var yy = kingMovesY[j] + y;
                            if (xx >= 0 && xx < 6 && yy >= 0 && yy < 6 && chessboard[xx, yy] != 1)
                                chessboard[xx, yy] = 2;
                        }
                        break;
                }
            }
            for (var i = 0; i < 6; i++)
            {
	            for (var j = 0; j < 6; j++)
	            {
		            if (chessboard[i, j] == 0)
			            txtChessSolution.Text += positionsLetters.Substring(i, 1)+positionNumbers.Substring(j, 1);
	            }
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var d in gbBombInformation.Controls)
            {
                if (d.GetType() != typeof(GroupBox)) continue;
                var f = (GroupBox) d;
                foreach (var c in f.Controls)
                {
                    if (c.GetType() == typeof(Label))
                    {
                        var l = (Label) c;
                        if ((string)l.Tag == "mods")
                            l.Visible = cbShowAddonModules.Checked;
                    }
                    else if (c.GetType() == typeof(NumericUpDown))
                    {
                        var n = (NumericUpDown) c;
                        if ((string)n.Tag == "mods")
                            n.Visible = cbShowAddonModules.Checked;
                    }
                }
            }
            var selected = "About";
            if(lbModules.Items.Count > 0)
                selected = lbModules.SelectedItem.ToString();

            lbModules.Items.Clear();
            lbModules.Items.Add("About");

            if (cbShowStockModules.Checked)
            {
                foreach (var m in _stockModules)
                {
	                lbModules.Items.Add(m);
                }
            }

            if (cbShowAddonModules.Checked)
            {
                foreach (var m in _moduleNames)
                {
	                lbModules.Items.Add(m);
                }
            }

            var index = lbModules.Items.IndexOf(selected);
            lbModules.SelectedIndex = index == -1 ? 0 : index;
            LbModules_SelectedIndexChanged(null, null);

            //Keypad specific mod
            for (var i = 4; i < 8; i++)
            {
	            ((Button) fpKeypadSelection.Controls[i]).Visible = cbShowAddonModules.Checked;
            }
	        while (!cbShowAddonModules.Checked && _keypadSelection[4] != "")
	        {
		        KeypadSelection_Click(fpKeypadSelection.Controls[4], e);
	        }
	        if (cbShowStockModules.Checked && cbShowAddonModules.Checked)
                gbKeypads.Text = @"Keypads, Round Keypads";
            else if (cbShowStockModules.Checked)
                gbKeypads.Text = @"Keypads";
            else
                gbKeypads.Text = @"Round Keypads";

            gbSimonSays.Visible = cbShowStockModules.Checked;

        }

        private void TxtLetteredKeysIn_TextChanged(object sender, EventArgs e)
        {
            txtLetteredKeysOut.Text = "";
            var num = GetDigitFromCharacter(txtLetteredKeysIn.Text);
            var batts = Batteries.TotalBatteries;
            if (num == -1) return;

            if (num == 69) txtLetteredKeysOut.Text = @"D";
            else if ((num%6) == 0) txtLetteredKeysOut.Text = @"A";
            else if (batts >= 2 && (num%3) == 0) txtLetteredKeysOut.Text = @"B";
            else if (Serial.Contains("C")
                     || Serial.Contains("E")
                     || Serial.Contains("3"))
                txtLetteredKeysOut.Text = num >= 22 && num <= 79 ? "B" : "C";
            else txtLetteredKeysOut.Text = num < 46 ? "D" : "A";
        }

        private void TxtEmojiMathIn_TextChanged(object sender, EventArgs e)
        {
            const string numbers = ":) =( (: )= :( ): =) (= :| |:";
            var num1 = 0;
            var sign = 0;
            var num2 = 0;

            txtEmojiMathOut.Text = "";
            var chars = txtEmojiMathIn.Text.ToCharArray();
            for (var i = 0; i < chars.Length;)
            {
                switch (chars[i])
                {
                    case '|':
                    case ')':
                    case '(':
                    case ':':
                    case '=':
                        if ((i + 1) == chars.Length) return;
                        var n = numbers.IndexOf(("" + chars[i] + chars[i + 1]), StringComparison.Ordinal);
                        if (n < 0) return;

                        if (sign == 0)
                        {
                            num1 *= 10;
                            num1 += n/3;
                        }
                        else
                        {
                            num2 *= 10;
                            num2 += n/3;
                        }
                        i++;
                        break;
                    case '+':
                    case '-':
                        if (sign != 0) return;
                        sign = "- +".IndexOf(chars[i].ToString(), StringComparison.Ordinal) - 1;
                        if (sign < -1) return;
                        break;
                    case ' ':
                        break;
                    default:
                        return;
                }
                i++;
            }

            if (sign == 0) return;
            txtEmojiMathOut.Text = (sign > 0 ? num1 + num2 : num1 - num2).ToString();
        }

        private void TxtStroopColors_TextChanged(object sender, EventArgs e)
        {
            txtStroopAnswer.Text = new Stroop().GetAnswer(txtStroopColors.Text, txtStroopWords.Text);
        }

        private void TxtNumberPadIn_TextChanged(object sender, EventArgs e)
        {
            var pad = new NumberPad(txtNumberPadIn.Text);
            txtNumberPadOut.Text = "";
            if (txtNumberPadIn.Text.Trim().Length < 10 || pad.GetColorCount(pad.ColorAll) < 10) return;
            try
            {

                txtNumberPadOut.Text = pad.GetCorrectCode();
            }
            catch
            {
                txtNumberPadOut.Text = @"Module solved itself";
            }
        }

        private void TxtCombinationLockIn_TextChanged(object sender, EventArgs e)
        {
            var input = txtCombinationLockIn.Text.ToUpper();
            var twoFactor = input.Split(' ');
            txtCombinationLockOut.Text = @"Input [Solved modules] [list of two factors]  or [solved modules] S [total modules]";
            if (twoFactor.Length < 2) return;

            var step1 = Batteries.TotalBatteries;
            var step2 = GetDigitFromCharacter(twoFactor[0]);
            if (input.Contains("S"))
            {
                 if (twoFactor.Length < 3) return;
                if (!IsSerialValid) return;
                step1 += GetDigitFromCharacter(Serial.Substring(5, 1));
                step1 += GetDigitFromCharacter(twoFactor[0]);
                step2 += GetDigitFromCharacter(twoFactor[2]);
            }
            else
            {
                for (var i = 1; i < twoFactor.Length; i++)
                {
                    if (twoFactor[i].Length != 2) return;
                    step2 += GetDigitFromCharacter(twoFactor[i].Substring(0, 1));
                    step1 += GetDigitFromCharacter(twoFactor[i].Substring(1, 1));
                }
            }
            txtCombinationLockOut.Text = @"Right " + (step1%20) + 
                                        @", Left " + (step2%20) + 
                                        @", Right " + (((step1%20) + (step2%20))%20);
        }

        private void TxtSemaphoreIn_TextChanged(object sender, EventArgs e)
        {
            txtSemaphoreOut.Text = new Semaphore().GetAnswer(txtSemaphoreIn.Text);
        }

        private void TxtCaesarCipherIn_TextChanged(object sender, EventArgs e)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var offset = Batteries.TotalBatteries;
            if (SerialNumberContainsVowel()) offset--;
            if (!SerialNumberLastDigitOdd()) offset++;
            if (IsIndicatorPresent("CAR")) offset++;

            if (IsPortPresent(PortTypes.Parallel) && IsIndicatorLit("NSA"))
            {
                offset = 0;
            }
            

            txtCaesarCipherOut.Text = "";
            for (var i = 0; i < txtCaesarCipherIn.Text.Trim().Length; i++)
            {
                var c = letters.IndexOf(txtCaesarCipherIn.Text.Substring(i, 1), StringComparison.Ordinal);
                if (c == -1)
                {
                    txtCaesarCipherOut.Text = @"Input only Letters";
                    return;
                }
                c += offset;
                c %= 26;
                if (c < 0)
                    c += 26;
                txtCaesarCipherOut.Text += letters.Substring(c, 1);
            }
            if (txtCaesarCipherOut.TextLength == 0)
                txtCaesarCipherOut.Text = @"Offset = " + offset;
        }

        private bool RoughEqual(double x, double y)
        {
            if (double.IsInfinity(x))
            {
                return double.IsInfinity(y);
            }
            if (double.IsInfinity(y))
            {
                return double.IsInfinity(x);
            }
            return ((((x * 0.95) <= y) && (y <= (x * 1.0526315789473684))) || (((x - 0.1) <= y) && (y <= (x + 0.1))));
        }

        private void TxtResistorsIn_TextChanged(object sender, EventArgs e)
        {
            const string digits = "0123456789";
            const string ohm = "Ω";
            const string colors = "SDKNROYGBVAW";
            var resistorsTxt = txtResistorsIn.Text.ToUpper().Split(' ');
            var batts = Batteries.TotalBatteries;
            if (batts > 6) batts = 6;
            txtResistorsOut.Text = "";

            if (!IsSerialValid) return;
            if (resistorsTxt.Length < 2) return;
            if (resistorsTxt.Any(r => r.Length != 3)) return;
            var resistors = new float[2];
            
            for(var x=0; x<2;x++)
            {
                var i = colors.IndexOf(resistorsTxt[x].Substring(0, 1), StringComparison.Ordinal)-2;    //Gold/Siver not Valid
                var j = colors.IndexOf(resistorsTxt[x].Substring(1, 1), StringComparison.Ordinal)-2;    //Gold/Silver not Valid
                var k = colors.IndexOf(resistorsTxt[x].Substring(2, 1), StringComparison.Ordinal)-2;    //Gray/White not Valid
                if (i < 0 || j < 0 || k < -2 || k > 7) return;
                var r = (i*10.0f) + j;
                switch (k)
                {
                    case -1:
                        r /= 10.0f;
                        break;
                    case -2:
                        r /= 100.0f;
                        break;
                    default:
                        r *= (float)Math.Pow(10.0, k);
                        break;
                }
                resistors[x] = r;
            }
            
            var resistance = 0ul;
            var targetIn = 0;
            var targetOut = digits.IndexOf(Serial.Substring(5, 1), StringComparison.Ordinal);
            var digitFound = false;
            for (var i = 0; i < 6; i++)
            {
                if (!digits.Contains(Serial.Substring(i, 1))) continue;
                resistance *= 10;
                var j = digits.IndexOf(Serial.Substring(i, 1), StringComparison.Ordinal);
                resistance += (ulong)j;
                if (!digitFound)
                {
                    targetIn = j;
                    digitFound = true;
                }
                else
                {
	                break;
                }
            }
            resistance *= (ulong) Math.Pow(10.0f, batts);
            if (targetOut < 0)
                targetOut = targetIn;

            var primaryIn = (targetIn%2) == 0 ? "A" : "B";
            var secondaryIn = (targetIn % 2) == 1 ? "A" : "B";
            var primaryOut = (targetOut%2) == 0 ? "C" : "D";
            var secondaryOut = (targetOut % 2) == 1 ? "C" : "D";
            if (IsIndicatorLit("FRK")) primaryOut = "C+D";
            else if (Batteries.DBatteries > 0)
                primaryOut += ", 0" + ohm + " from " + secondaryIn + " to " + secondaryOut;

            var parallel = (resistors[0]*resistors[1])/(resistors[0] + resistors[1]);
            var serial = resistors[0] + resistors[1];

            var p = parallel >= 1000000
                ? (parallel/1000000.0f) + "M" + ohm
                : (parallel >= 1000)
                    ? (parallel/1000.0f) + "K" + ohm
                    : parallel + ohm;
            var s = serial >= 1000000
                ? (serial / 1000000.0f) + "M" + ohm
                : (serial >= 1000)
                    ? (serial / 1000.0f) + "K" + ohm
                    : serial + ohm;
            var t = resistance >= 1000000
                ? (resistance / 1000000.0f) + "M" + ohm
                : (resistance >= 1000)
                    ? (resistance / 1000.0f) + "K" + ohm
                    : resistance + ohm;

            var r0 = resistors[0] >= 1000000
                ? (resistors[0] / 1000000.0f) + "M" + ohm
                : (resistors[0] >= 1000)
                    ? (resistors[0] / 1000.0f) + "K" + ohm
                    : resistors[0] + ohm;

            var r1 = resistors[1] >= 1000000
                ? (resistors[1] / 1000000.0f) + "M" + ohm
                : (resistors[1] >= 1000)
                    ? (resistors[1] / 1000.0f) + "K" + ohm
                    : resistors[1] + ohm;


            if (resistance == 0)
                txtResistorsOut.Text = @"Connect " + primaryIn + @" to " + primaryOut;
            else if (RoughEqual(resistance,parallel))
                txtResistorsOut.Text = @"Connect the Resistors in Parallel from " + primaryIn + @" to " + primaryOut;
            else if (RoughEqual(resistance,serial))
                txtResistorsOut.Text = @"Connect the Resistors in Series from " + primaryIn + @" to " + primaryOut;
            else if (RoughEqual(resistance,resistors[0]))
                txtResistorsOut.Text = @"Connect Resistor One from " + primaryIn + @" to " + primaryOut;
            else if (RoughEqual(resistance,resistors[1]))
                txtResistorsOut.Text = @"Connect Resistor Two from " + primaryIn + @" to " + primaryOut;
            else
                txtResistorsOut.Text = @"R1=" + r0 + @" R2=" + r1 + @" Parallel=" + p + @" Series=" + s + @" Target = " + t + @" From " + primaryIn + @" to " + primaryOut;

        }

        private bool[] TxtContainsFrequencies(string text)
        {
            var frequencies = new bool[5];
            switch (text.Trim())
            {

                case "10 22":
                case "22 10":
                    frequencies[2] = true;
                    frequencies[3] = true;
                    break;
                case "10 50":
                case "50 10":
                    frequencies[1] = true;
                    frequencies[3] = true;
                    break;
                case "10 60":
                case "60 10":
                    frequencies[1] = true;
                    frequencies[2] = true;
                    break;
                case "22 50":
                case "50 22":
                    frequencies[0] = true;
                    frequencies[3] = true;
                    break;
                case "22 60":
                case "60 22":
                    frequencies[0] = true;
                    frequencies[2] = true;
                    break;
                case "50 60":
                case "60 50":
                    frequencies[0] = true;
                    frequencies[1] = true;
                    break;
                case "00 00":
                case "99 99":
                    frequencies[4] = true;
                    break;
            }
            return frequencies;
        }

        private ProbingFrequencies ProbingCommonFrequency(string pair1, string pair2, bool opposite=false)
        {
            if (IsNullOrWhiteSpace(pair1) || IsNullOrWhiteSpace(pair2)) return ProbingFrequencies.Unknown;
            if (pair1 == pair2) return ProbingFrequencies.Unknown;
            if (!opposite)
            {
                if (pair1.Contains("10") && pair2.Contains("10")) return ProbingFrequencies.TenHz;
                if (pair1.Contains("22") && pair2.Contains("22")) return ProbingFrequencies.TwentyTwoHz;
                if (pair1.Contains("50") && pair2.Contains("50")) return ProbingFrequencies.FiftyHz;
                if (pair1.Contains("60") && pair2.Contains("60")) return ProbingFrequencies.SixtyHz;
            }
            else
            {
                if (pair1.Contains("10") && pair2.Contains("10"))
                {
                    if (pair1.Contains("22")) return ProbingFrequencies.TwentyTwoHz;
                    if (pair1.Contains("50")) return ProbingFrequencies.FiftyHz;
                    return ProbingFrequencies.SixtyHz;
                }
                if (pair1.Contains("22") && pair2.Contains("22"))
                {
                    if (pair1.Contains("10")) return ProbingFrequencies.TenHz;
                    if (pair1.Contains("50")) return ProbingFrequencies.FiftyHz;
                    return ProbingFrequencies.SixtyHz;
                }
                if (pair1.Contains("50") && pair2.Contains("50"))
                {
                    if (pair1.Contains("22")) return ProbingFrequencies.TwentyTwoHz;
                    if (pair1.Contains("10")) return ProbingFrequencies.TenHz;
                    return ProbingFrequencies.SixtyHz;
                }
                if (pair1.Contains("60") && pair2.Contains("60"))
                {
                    if (pair1.Contains("22")) return ProbingFrequencies.TwentyTwoHz;
                    if (pair1.Contains("50")) return ProbingFrequencies.FiftyHz;
                    return ProbingFrequencies.TenHz;
                }
            }
            return ProbingFrequencies.Unknown;
        }

        private int CommonNumber(int[] pair1, int[] pair2, bool opposite=false)
        {
            if (opposite)
            {
                if (pair1[0] == pair2[0]) return pair1[1];
                if (pair1[0] == pair2[1]) return pair1[1];
                if (pair1[1] == pair2[0]) return pair1[0];
                if (pair1[1] == pair2[1]) return pair1[0];
            }
            else
            {
                if (pair1[0] == pair2[0]) return pair1[0];
                if (pair1[0] == pair2[1]) return pair1[0];
                if (pair1[1] == pair2[0]) return pair1[1];
                if (pair1[1] == pair2[1]) return pair1[1];
            }


            return -1;
        }


        private void TxtProbing12_TextChanged(object sender, EventArgs e)
        {
            var wires = new[]
            {
                new ProbingSet(), new ProbingSet(), new ProbingSet(),
                new ProbingSet(), new ProbingSet(), new ProbingSet()
            };
            
            txtProbingOut.Text = @"""I still maintain ""reading order"" on probing is some BS"" - LtHummus (Sept 16, 2016)";
            var textBoxes = new[]
            {
                txtProbing12.Text, txtProbing34.Text, txtProbing56.Text,
                txtProbing14.Text, txtProbing25.Text
            };
            var pairs = new[]
            {
                new[] {0, 1}, new[] {0, 2}, new[] {0,3},
                new[] {0, 4}, new[] {0,5}
            };

            for (var i = 0; i < 5; i++)
            {
                if (i == 0 && textBoxes[i].Equals(textBoxes[3])) continue;

                if (textBoxes[i].Trim().Length != 5) continue;
                if (TxtContainsFrequencies(textBoxes[i])[4])
                {
                    wires[pairs[i][1]].Even = true;
                    continue;
                }
                for (var j = i+1; j < 5; j++)
                {
                    if (TxtContainsFrequencies(textBoxes[j])[4])
                    {
                        wires[pairs[j][1]].Even = true;
                        continue;
                    }
                    if (textBoxes[j].Trim().Length != 5) continue;
                    var cn = CommonNumber(pairs[i], pairs[j]);
                    var missing = ProbingCommonFrequency(textBoxes[i], textBoxes[j]);
                    if (missing == ProbingFrequencies.Unknown) continue;
                    wires[cn].Missing = missing;

                    cn = CommonNumber(pairs[i], pairs[j], true);
                    missing = ProbingCommonFrequency(textBoxes[i], textBoxes[j], true);
                    wires[cn].Missing = missing;

                    cn = CommonNumber(pairs[j], pairs[i], true);
                    missing = ProbingCommonFrequency(textBoxes[j], textBoxes[i], true);
                    wires[cn].Missing = missing;
                }
                break;
            }

            for (var i = 1; i < 6; i++)
            {
                if (wires[i].Even)
                    wires[i].Missing = wires[0].Missing;
            }

            var missingFreqs = new bool[4];
            var wiresComplete = 0;
            var incomplete = -1;
            for (var i = 0; i < 6; i++)
            {
                wiresComplete++;
                switch (wires[i].Missing)
                {
                    case ProbingFrequencies.Unknown:
                        wiresComplete--;
                        incomplete = i;
                        break;
                    case ProbingFrequencies.TenHz:
                        missingFreqs[0] = true;
                        break;
                    case ProbingFrequencies.TwentyTwoHz:
                        missingFreqs[1] = true;
                        break;
                    case ProbingFrequencies.FiftyHz:
                        missingFreqs[2] = true;
                        break;
                    case ProbingFrequencies.SixtyHz:
                        missingFreqs[3] = true;
                        break;
                }
            }
            if (wiresComplete == 5)
            {
                var missing = -1;
	            // ReSharper disable once NotAccessedVariable
                var missingCount = 4;
                for(var j = 0; j < 4; j++)
                {
	                if (!missingFreqs[j])
	                {
		                missing = j;
	                }
	                else
	                {
		                missingCount--;
	                }
                }

	            wires[incomplete].Missing = (ProbingFrequencies) missing;
            }

            

            if (wires[0].Missing != ProbingFrequencies.Unknown && wires[4].Missing != ProbingFrequencies.Unknown)
            {
                //We might have a solution to this probing.
                //Check for Red soltution
                var red = -1;
                var blue = -1;
                if (wires[0].Missing != ProbingFrequencies.FiftyHz)
                {
                    for (var i = 0; i < 6; i++)
                    {
                        if (wires[i].Missing != ProbingFrequencies.FiftyHz) continue;
                        red = i;
                        break;
                    }
                }
                else if (wires[4].Missing == ProbingFrequencies.TenHz)
                {
                    red = 4;
                }
                else
                {
                    for (var i = 0; i < 6; i++)
                    {
                        if (wires[i].Missing != ProbingFrequencies.SixtyHz) continue;
                        red = i;
                        break;
                    }
                }

                if (wires[4].Missing != ProbingFrequencies.TenHz)
                {
                    for (var i = 0; i < 6; i++)
                    {
                        if (red == i || wires[i].Missing != ProbingFrequencies.TwentyTwoHz) continue;
                        blue = i;
                        break;
                    }
                }
                else
                {
                    for (var i = 0; i < 6; i++)
                    {
                        if (red == i || wires[i].Missing != ProbingFrequencies.SixtyHz) continue;
                        blue = i;
                        break;
                    }
                }

                if (red != -1 && blue != -1)
                {
                    txtProbingOut.Text = @"Connect Red on " + (red + 1) + @" and Blue on " + (blue + 1);
                    return;
                }
            }

            var text = txtProbingOut.Text;
            for (var i = 0; i < 6; i++)
            {
                var freqtxt = new[] {"10", "22","50", "60"};
                if (wires[i].Missing == ProbingFrequencies.Unknown) continue;
                if (text == txtProbingOut.Text) txtProbingOut.Text = "";
                txtProbingOut.Text += (i + 1) + @" = " + freqtxt[(int)wires[i].Missing] + @", ";
            }

        }

        private void BtnResetAll2_Click(object sender, EventArgs e)
        {
            BtnResetCombinationLock_Click(sender, e);
            BtnResetSemaphore_Click(sender, e);
            BtnResetResistors_Click(sender, e);
            BtnResetProbing_Click(sender, e);
            BtnResetCaesar_Click(sender, e);
            BtnResetNumberPads_Click(sender, e);
        }


        private SillySlots _sillyslots = new SillySlots();
        private void BtnSillySlotsReset_Click(object sender, EventArgs e)
        {
            Slots.PopulateSubstitionTable();
            _sillyslots = new SillySlots();
            cboSillySlotsKeyWord.SelectedIndex = 0;
            cboSillySlotsSlot1.SelectedIndex = 0;
            cboSillySlotsSlot2.SelectedIndex = 0;
            cboSillySlotsSlot3.SelectedIndex = 0;
            txtSillySlotsResult.Text = "";
        }

        private void TxtSillySlotsInput_TextChanged(object sender, EventArgs e)
        {
            var keywords = new List<string> {"SAS","SIL","SOG","SAL","SIM","SAU","STE"};
            var color = new List<string> {"R", "B", "G"};
            var shape = new List<string> {"G", "Y", "B", "N"};

            if (txtSillySlotsInput.Text.Trim().Length != 11) return;

            var keyword = keywords.IndexOf(txtSillySlotsInput.Text.Split(' ')[0]);
            var colors = txtSillySlotsInput.Text.Split(' ')[1];
            var shapes = txtSillySlotsInput.Text.Split(' ')[2];

            cboSillySlotsKeyWord.SelectedIndex = keyword + 1;
            for (var i = 0; i < 3; i++)
            {
                var c = color.IndexOf(colors[i].ToString());
                var s = shape.IndexOf(shapes[i].ToString());
                if (c == -1 || s == -1) return;
                switch (i)
                {
                    case 0:
                        cboSillySlotsSlot1.SelectedIndex = (c*4) + s + 1;
                        break;
                    case 1:
                        cboSillySlotsSlot2.SelectedIndex = (c*4) + s + 1;
                        break;
                    default:
                        cboSillySlotsSlot3.SelectedIndex = (c*4) + s + 1;
                        break;
                }
            }
        }


        private void BtnSillySlotsSubmit_Click(object sender, EventArgs e)
        {
            txtSillySlotsResult.Text = "";
            if (cboSillySlotsKeyWord.SelectedIndex < 1
                || cboSillySlotsSlot1.SelectedIndex < 1
                || cboSillySlotsSlot2.SelectedIndex < 1
                || cboSillySlotsSlot3.SelectedIndex < 1)
            {
                return;
            }
            txtSillySlotsResult.Text = _sillyslots.CheckSlots(
                cboSillySlotsKeyWord.SelectedIndex - 1,
                Slots.slots[cboSillySlotsSlot1.SelectedIndex - 1],
                Slots.slots[cboSillySlotsSlot2.SelectedIndex - 1],
                Slots.slots[cboSillySlotsSlot3.SelectedIndex - 1])
                ? "Keep"
                : "Spin";
            cboSillySlotsKeyWord.SelectedIndex = 0;
            cboSillySlotsSlot1.SelectedIndex = 0;
            cboSillySlotsSlot2.SelectedIndex = 0;
            cboSillySlotsSlot3.SelectedIndex = 0;
        }

        private void BtnSillySlotsDebugDump_Click(object sender, EventArgs e)
        {
            _sillyslots.DumpStateToClipboard();
        }

        private void TxtWordScrambleIn_TextChanged(object sender, EventArgs e)
        {
            var wordsAnagram = new List<IList<string>>()
            {
                new List<string> {"stream", "master", "tamers"},
                new List<string> {"looped", "poodle", "pooled"},
                new List<string> {"cellar", "caller", "recall"},
                new List<string> {"seated", "sedate", "teased"},
                new List<string> {"rescue", "secure", "recuse"},
                new List<string> {"rashes", "shears", "shares"},
                new List<string> {"barely", "barley", "bleary"},
                new List<string> {"duster", "rusted", "rudest"}
            };
            var wordsScramble = new List<string>()
            {
                "module","ottawa","banana","kaboom","letter","widget",
                "person","sapper","wiring","archer","device","rocket",
                "damage","defuse","flames","semtex","cannon","blasts",
                "attack","weapon","charge","napalm","mortar","bursts",
                "casing","disarm","keypad","button","robots","kevlar"
            };
            txtWordScrambleOut.Text = "";
            if (txtWordScrambleIn.TextLength < 6) return;
            foreach (var w in wordsAnagram)
            {
                if (!w.Contains(txtWordScrambleIn.Text.ToLower())) continue;
                foreach (var ww in w)
                {
	                if (ww != txtWordScrambleIn.Text.ToLower())
		                txtWordScrambleOut.Text += ww + @" ";
                }
	            return;
            }
            var x = Concat(txtWordScrambleIn.Text.ToLower().OrderBy(c => c));
            foreach (var w in wordsScramble)
            {
                var y = Concat(w.OrderBy(c => c));
                if (x != y) continue;
                txtWordScrambleOut.Text = w.ToUpper();
                break;
            }
        }

        

        private void TxtAlphabetIn_TextChanged(object sender, EventArgs e)
        {
            txtAlphabetOut.Text = Alphabet.GetOrder(txtAlphabetIn.Text);
        }

        private void TxtAdventureGameSTR_TextChanged(object sender, EventArgs e)
        {
            txtAdventureGameOut.Text = "";
            if (txtAdventrueGameDEX.TextLength == 0
                || txtAdventrueGameGravity.TextLength == 0
                || txtAdventrueGameHeight.TextLength == 0
                || txtAdventrueGameINT.TextLength == 0
                || txtAdventrueGamePressure.TextLength == 0
                || txtAdventrueGameTemp.TextLength == 0
                || txtAdventureGameSTR.TextLength == 0
                || cboAdventureGameMonster.SelectedIndex <= 0
                || cboAdventureGameWeapon1.SelectedIndex <= 0
                || cboAdventureGameWeapon2.SelectedIndex <= 0
                || cboAdventureGameWeapon3.SelectedIndex <= 0
                || cboAdventureGameItem1.SelectedIndex < 0
                || cboAdventureGameItem2.SelectedIndex < 0
                || cboAdventureGameItem3.SelectedIndex < 0
                || cboAdventureGameItem4.SelectedIndex < 0
                || cboAdventureGameItem5.SelectedIndex < 0) return;

            var itemsnotselected = 0;
            if (cboAdventureGameItem1.SelectedIndex == 0) itemsnotselected++;
            if (cboAdventureGameItem2.SelectedIndex == 0) itemsnotselected++;
            if (cboAdventureGameItem3.SelectedIndex == 0) itemsnotselected++;
            if (cboAdventureGameItem4.SelectedIndex == 0) itemsnotselected++;
            if (cboAdventureGameItem5.SelectedIndex == 0) itemsnotselected++;
            if (itemsnotselected > 2) return;


            var ag = new AdventureGame();

            var monster = (Monsters)cboAdventureGameMonster.SelectedIndex - 1;
            var items = new[]
            {
                (Items)cboAdventureGameItem1.SelectedIndex - 1,
                (Items)cboAdventureGameItem2.SelectedIndex - 1,
                (Items)cboAdventureGameItem3.SelectedIndex - 1,
                (Items)cboAdventureGameItem4.SelectedIndex - 1,
                (Items)cboAdventureGameItem5.SelectedIndex - 1
            };
            var weapons = new[]
            {
                (Weapons)cboAdventureGameWeapon1.SelectedIndex - 1,
                (Weapons)cboAdventureGameWeapon2.SelectedIndex - 1,
                (Weapons)cboAdventureGameWeapon3.SelectedIndex - 1
            };
            var stats = new int[7];
            try
            {
                int.TryParse(txtAdventureGameSTR.Text, out stats[0]);
                int.TryParse(txtAdventrueGameDEX.Text, out stats[1]);
                int.TryParse(txtAdventrueGameINT.Text, out stats[2]);
                stats[3] = ag.ParseHeight(txtAdventrueGameHeight.Text);
                int.TryParse(txtAdventrueGameTemp.Text, out stats[4]);
                stats[5] = ag.ParseGravity(txtAdventrueGameGravity.Text);
                int.TryParse(txtAdventrueGamePressure.Text, out stats[6]);
            }
            catch
            {
                return;
            }

            txtAdventureGameOut.Text = ag.GetAdventrueGameResults(stats, monster, weapons, items);
        }

        private void BtnResetAll3_Click(object sender, EventArgs e)
        {
            BtnResetAdventureGame_Click(sender, e);
            BtnResetAlphabet_Click(sender,e);
            BtnResetWordScramble_Click(sender, e);
            BtnResetSwitches_Click(sender, e);
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        private void LbModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            Text = @"Keep Talking and Nobody Explodes Helper";
            var name = lbModules.SelectedItem.ToString().Trim();
			if (!_moduleNameToTab.TryGetValue(name, out TabPage page))
			{
				for (var i = lbModules.SelectedIndex; i < lbModules.Items.Count; i++)
				{
					if (!_moduleNameToTab.TryGetValue(lbModules.Items[i].ToString().Trim(), out page)) continue;
					lbModules.SelectedIndex = i;
					break;
				}
				if (page == null)
				{
					lbModules.SelectedIndex = lbModules.Items.Count > 0 ? 0 : -1;
					page = _moduleNameToTab["About"];
				}
			}
			else
			{
				Text += RequiredEdgeWork.GetRequiredEdgeWork(name);
			}
			tcTabs.TabPages.Clear();
            tcTabs.TabPages.Add(page);
            Update();
        }

        

        private void CbSwitchesCurrent1_CheckedChanged(object sender, EventArgs e)
        {
            txtSwitchesOut.Text = "";

            var current = cbSwitchesCurrent1.Checked ? "1" : "0";
            current += cbSwitchesCurrent2.Checked ? "1" : "0";
            current += cbSwitchesCurrent3.Checked ? "1" : "0";
            current += cbSwitchesCurrent4.Checked ? "1" : "0";
            current += cbSwitchesCurrent5.Checked ? "1" : "0";

            var desired = cbSwitchesDesired1.Checked ? "1" : "0";
            desired += cbSwitchesDesired2.Checked ? "1" : "0";
            desired += cbSwitchesDesired3.Checked ? "1" : "0";
            desired += cbSwitchesDesired4.Checked ? "1" : "0";
            desired += cbSwitchesDesired5.Checked ? "1" : "0";

            txtSwitchesOut.Text = Switches.Solve(current, desired);

        }

        private void MaskTextBox_Enter(object sender, EventArgs e)
        {
            //This method will prevent the cursor from being positioned in the middle 
            //of a textbox when the user clicks in it.

	        if (sender is MaskedTextBox textBox)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    var pos = textBox.SelectionStart;
                    var slen = textBox.SelectionLength;
                    var tlen = textBox.Text.Trim().Length;

                    if (pos > tlen)
                    {
                        var diff = pos - tlen;
                        pos = tlen;
                        slen -= diff;
                        if (slen < 0) slen = 0;
                    }

                    if ((pos + slen) > tlen)
                    {
                        var diff = (pos + slen) - tlen;
                        slen -= diff;
                    }

                    textBox.Select(pos, slen);
                });
            }
        }

        private void BtnResetProbing_Click(object sender, EventArgs e)
        {
            txtProbing12.Text = "";
            txtProbing14.Text = "";
            txtProbing25.Text = "";
            txtProbing34.Text = "";
            txtProbing56.Text = "";
        }

        private void BtnResetNumberPads_Click(object sender, EventArgs e)
        {
            txtNumberPadIn.Text = "";
        }

        private void BtnResetCaesar_Click(object sender, EventArgs e)
        {
            txtCaesarCipherIn.Text = "";
        }

        private void BtnResetCombinationLock_Click(object sender, EventArgs e)
        {
            txtCombinationLockIn.Text = "";
        }

        private void BtnResetSemaphore_Click(object sender, EventArgs e)
        {
            txtSemaphoreIn.Text = "";
        }

        private void BtnResetResistors_Click(object sender, EventArgs e)
        {
            txtResistorsIn.Text = "";
        }

        private void BtnResetAdventureGame_Click(object sender, EventArgs e)
        {
            txtAdventrueGamePressure.Text = "";
            txtAdventrueGameGravity.Text = "";
            txtAdventrueGameTemp.Text = "";
            txtAdventureGameSTR.Text = "";
            txtAdventrueGameDEX.Text = "";
            txtAdventrueGameHeight.Text = "";
            txtAdventrueGameINT.Text = "";
            foreach (var c in flpAdventureGameCBO.Controls)
            {
	            ((ComboBox)c).SelectedIndex = 0;
            }
        }

        private void BtnResetAlphabet_Click(object sender, EventArgs e)
        {
            txtAlphabetIn.Text = "";
        }

        private void BtnResetWordScramble_Click(object sender, EventArgs e)
        {
            txtWordScrambleIn.Text = "";
        }

        private void BtnResetSwitches_Click(object sender, EventArgs e)
        {
            cbSwitchesCurrent1.Checked = false;
            cbSwitchesCurrent2.Checked = false;
            cbSwitchesCurrent3.Checked = false;
            cbSwitchesCurrent4.Checked = false;
            cbSwitchesCurrent5.Checked = false;
            cbSwitchesDesired1.Checked = false;
            cbSwitchesDesired2.Checked = false;
            cbSwitchesDesired3.Checked = false;
            cbSwitchesDesired4.Checked = false;
            cbSwitchesDesired5.Checked = false;
        }

        private void CbMurderRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMurderOut.Text = "";
            if (cbMurderRoom.SelectedIndex < 1) return;

            var murderRoom = cbMurderRoom.SelectedIndex - 1;

            var roomList = new List<string>
            {
                "Ballroom","Billiard Room","Conservatory",
                "Dining Room","Hall","Kitchen",
                "Library","Lounge","Study"
            };
            var suspectList = new List<string>
            {
                "Scarlett","Plum","Peacock",
                "Green","Mustard","White"
            };
            var weaponList = new List<string>
            {
                "Candlestick","Dagger","Lead Pipe",
                "Revolver","Rope","Spanner"
            };

            var rows = new[]
            {
                new[] {3,6,7,5,8,2},
                new[] {8,4,1,7,5,6},
                new[] {5,1,0,6,2,3},
                new[] {7,0,3,2,4,5},
                new[] {1,5,8,0,3,4},
                new[] {2,7,6,8,1,0},
                new[] {0,2,5,4,6,8},
                new[] {4,8,2,3,7,1},
                new[] {6,3,4,1,0,7}
            };

            var suspects = 6;
            var weapons = 8;

            if (IsIndicatorLit("TRN"))
                suspects = 5;
            else if (murderRoom == 3)
                suspects = 7;
            else if (PortCount(PortTypes.StereoRCA) >= 2)
                suspects = 8;
            else if (Batteries.DBatteries == 0)
                suspects = 2;
            else if (murderRoom == 8)
                suspects = 4;
            else if (Batteries.TotalBatteries >= 5)
                suspects = 9;
            else if (IsIndicatorUnlit("FRQ"))
                suspects = 1;
            else if (murderRoom == 2)
                suspects = 3;

            if (murderRoom == 7)
                weapons = 3;
            else if (Batteries.TotalBatteries >= 5)
                weapons = 1;
            else if (IsPortPresent(PortTypes.Serial))
                weapons = 9;
            else if (murderRoom == 1)
                weapons = 4;
            else if (Batteries.Holders.Length == 0)
                weapons = 6;
            else if (NumberLitIndicators() == 0)
                weapons = 5;
            else if (murderRoom == 4)
                weapons = 7;
            else if (PortCount(PortTypes.StereoRCA) >= 2)
                weapons = 2;

            suspects--;
            weapons--;

            txtMurderOut.Text = @"Possible Accusations:" + Environment.NewLine;
            for(var i = 0; i < 6; i++)
            {
	            for (var j = 0; j < 6; j++)
	            {
		            if (rows[suspects][i] != rows[weapons][j]) continue;
		            txtMurderOut.Text += Environment.NewLine;
		            txtMurderOut.Text += @"It was " + suspectList[i] + 
		                                 @", with the " + weaponList[j] + 
		                                 @", in the " + roomList[rows[suspects][i]];
	            }
            }
        }

        private void CbMicrocontroller_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = cbMicrocontroller.SelectedIndex - 1;
            txtMicrocontrollerOut.Text = "";
            if (index < 0) return;
            var colors = new List<string> {"White","Green","Red","Yellow","Blue","Magenta"};
            if (txtMicrocontrollerLastDigit.Text == @"1" || txtMicrocontrollerLastDigit.Text == @"4")
            {
                colors = new List<string>() {"White","Yellow","Magenta","Green","Blue","Red"};
            }
            else if (IsIndicatorLit("SIG") || IsPortPresent(PortTypes.RJ45))
            {
                colors = new List<string>() {"White","Yellow","Red","Magenta","Green","Blue"};
            }
            else if ("CLRX18".ToCharArray().Any(c => Serial.Contains(c.ToString())))
            {
                colors = new List<string>() {"White","Red","Magenta","Green","Blue","Yellow"};
            }
            else if (Batteries.TotalBatteries.ToString(CultureInfo.InvariantCulture) == txtMicrocontrollerSecondDigit.Text)
            {
                colors = new List<string>() {"White","Green","Red","Yellow","Blue","Magenta"};
            }

            var pinmaps = new[]
            {
                new[] {2,1,5,3,4,0},
                new[] {2,4,0,3,1,0,5,0},
                new[] {0,0,0,0,2,3,0,1,5,4},
                new[] {4,5,1,3,2,0},
                new[] {4,3,1,0,2,0,5,0},
                new[] {4,2,3,0,0,0,0,5,1,0},
                new[] {0,2,4,1,3,5},
                new[] {4,0,0,1,2,0,3,5},
                new[] {4,3,2,0,0,1,0,0,5,0},
                new[] {4,1,5,2,3,0},
                new[] {2,0,5,0,1,0,3,4},
                new[] {5,3,1,0,0,0,2,0,4,0} 
            };

            txtMicrocontrollerOut.Text = @"Pin Map:" + Environment.NewLine;
            for (var i = 0; i < pinmaps[index].Length; i+=2)
            {
                txtMicrocontrollerOut.Text += Environment.NewLine;
                txtMicrocontrollerOut.Text += (i + 1) + @" = " + colors[pinmaps[index][i]];
                txtMicrocontrollerOut.Text += @", " + (i + 2) + @" = " + colors[pinmaps[index][i + 1]];
            }
            //
        }

        private static bool IsPrime(int num)
        {
            var prime = new List<int> {2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61,67,71,73,79,83,89,97};
            return prime.Contains(num);
        }

        private static bool IsPerfectSquare(int num)
        {
            var perfectsquare = new List<int> {1,4,9,16,25,36,49,64,81};
            return perfectsquare.Contains(num);
        }

        private static bool IsMultiple(int num, int multiple)
        {
            return (num%multiple) == 0;
        }

        private static string SwapCharacters(string str, int n, int n2)
        {
            var chArray = str.ToCharArray();
            var ch = chArray[n];
            chArray[n] = chArray[n2];
            chArray[n2] = ch;
            return new string(chArray);
        }

        private void TxtGamePadX_TextChanged(object sender, EventArgs e)
        {
            txtGamePadOut.Text = "";
            if (txtGamePadX.Text.Trim().Length < 2 || txtGamePadY.Text.Trim().Length < 2) return;

            var hcn = new List<int> {1,2,4,6,12,24,36,48,60};

            var x = int.Parse(txtGamePadX.Text.Trim());
            var y = int.Parse(txtGamePadY.Text.Trim());
            string solution;
           
            var numbatteries = Batteries.TotalBatteries;

            var xA = x / 10;
            var xB = (xA * -10) + x;
            var yA = y / 10;
            var yB = (yA * -10) + y;
            if (IsPrime(x))
            {
                solution = "▲▲▼▼";
            }
            else if (IsMultiple(x, 12))
            {
                solution = "▲A◀◀";
            }
            else if (((xA + xB) == 10) && SerialNumberLastDigitOdd())
            {
                solution = "AB◀▶";
            }
            else if (((x % 6) == 3) || ((x % 10) == 5))
            {
                solution = "▼◀A▶";
            }
            else if (IsMultiple(x, 7) && !IsMultiple(y, 7))
            {
                solution = "◀◀▲B";
            }
            else if (x == (yA * yB))
            {
                solution = "A▲◀◀";
            }
            else if (IsPerfectSquare(x))
            {
                solution = "▶▶A▼";
            }
            else if (((x % 3) == 2) || IsIndicatorUnlit("SND"))
            {
                solution = "▶AB▲";
            }
            else if (((60 <= x) && (x < 90)) && (numbatteries == 0))
            {
                solution = "BB▶◀";
            }
            else if (IsMultiple(x, 6))
            {
                solution = "ABA▶";
            }
            else if (IsMultiple(x, 4))
            {
                solution = "▼▼◀▲";
            }
            else
            {
                solution = "A◀B▶";
            }
            if (IsPrime(y))
            {
                solution = solution + "◀▶◀▶";
            }
            else if (IsMultiple(y, 8))
            {
                solution = solution + "▼▶B▲";
            }
            else if (((yA - yB) == 4) && IsPortPresent(PortTypes.StereoRCA))
            {
                solution = solution + "▶A▼▼";
            }
            else if (((y % 4) == 2) || IsIndicatorLit("FRQ"))
            {
                solution = solution + "B▲▶A";
            }
            else if (IsMultiple(y, 7) && !IsMultiple(x, 7))
            {
                solution = solution + "◀◀▼A";
            }
            else if (IsPerfectSquare(y))
            {
                solution = solution + "▲▼B▶";
            }
            else if (y == (xA * xB))
            {
                solution = solution + "A▲◀▼";
            }
            else if (((y % 4) == 3) || IsPortPresent(PortTypes.PS2))
            {
                solution = solution + "▲BBB";
            }
            else if ((yA > yB) && (numbatteries >= 2))
            {
                solution = solution + "AA▲▼";
            }
            else if (IsMultiple(y, 5))
            {
                solution = solution + "BAB◀";
            }
            else if (IsMultiple(y, 3))
            {
                solution = solution + "▶▲▲◀";
            }
            else
            {
                solution = solution + "B▲A▼";
            }
            if (IsMultiple(x, 11))
            {
                solution = SwapCharacters(SwapCharacters(solution, 4, 6), 0, 1);
            }
            if (xA == (1 + yB))
            {
                solution = SwapCharacters(SwapCharacters(solution, 5, 7), 2, 3);
            }
            if (hcn.Contains<int>(x) || hcn.Contains<int>(y))
            {
                solution = solution.Substring(4, 4) + solution.Substring(0, 4);
            }
            if (IsPerfectSquare(x) && IsPerfectSquare(y))
            {
                var array = solution.ToCharArray();
                Array.Reverse(array);
                solution = new string(array);
            }
            txtGamePadOut.Text = solution;

        }

        private void BtnGamepadReset_Click(object sender, EventArgs e)
        {
            txtGamePadY.Text = "";
            txtGamePadX.Text = "";
        }

        private void BtnMicroControllerReset_Click(object sender, EventArgs e)
        {
            txtMicrocontrollerLastDigit.Text = "";
            txtMicrocontrollerSecondDigit.Text = "";
            cbMicrocontroller.SelectedIndex = 0;
        }

        private void BtnResetMurder_Click(object sender, EventArgs e)
        {
            cbMurderRoom.SelectedIndex = 0;
        }

        private void TxtCryptograpyLengths_TextChanged(object sender, EventArgs e)
        {
            txtCryptographyOut.Text = new Cryptography().GetLetterOrder(txtCryptograpyLengths.Text,
                txtCryptographyLetters.Text);
        }

        private void Pb3DMaze_Paint(object sender, PaintEventArgs e)
        {
            
            var maze = _3Dmaze.GetMaze(txt3DMazeLetters.Text);
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, pb3DMaze.Size.Width, pb3DMaze.Size.Height);

            var x = -1;
            var y = -1;

            if (IsSerialValid)
            {
                const string digits = "0123456789";

                x = digits.IndexOf(Serial.Substring(5, 1), StringComparison.Ordinal);
                for (var i = 0; i < 6 && y < 0; i++)
                {
	                y = digits.IndexOf(Serial.Substring(i, 1), StringComparison.Ordinal);
                }

	            if (x >= 0 && y >= 0)
                {
                    y += UnlitIndicators.Count(indicator => indicator.Any(l => "MAZE GAMER".Contains(l)));
                    x += LitIndicators.Count(indicator => indicator.Any(l => "HELP IM LOST".Contains(l)));

                    x %= 8;
                    y %= 8;
                    e.Graphics.DrawMarker(Color.Green,x,y);
                }
            }

            if (maze == null || maze[0].Length == 1)
            {
                if (maze?[0].Length == 1)
                {
                    for (var i = 0; i < 8; i++)
                    {
	                    e.Graphics.DrawString(i.ToString(), txt3DMazeLetters.Font, Brushes.Red, new Point((i * 47) + 10, (0 * 47) + 10));
                    }
	                for (var j = 1; j < 8; j++)
	                {
		                e.Graphics.DrawString(j.ToString(), txt3DMazeLetters.Font, Brushes.Red, new Point((0 * 47) + 10, (j * 47) + 10));
	                }
                }
                for (var i = 0; i < 8; i++)
                {
                    for (var j = 0; j < 8; j++)
                    {
                        e.Graphics.DrawWall(Color.Red, 7f, i, j, "Up");
                        e.Graphics.DrawWall(Color.Red, 7f, i, j, "Down");
                        e.Graphics.DrawWall(Color.Red, 7f, i, j, "Left");
                        e.Graphics.DrawWall(Color.Red, 7f, i, j, "Right");
                    }
                }
                mazeOutput.Text = "";
                rbGreenCircle.Checked = true;
                return;
            }

            var locations = _3Dmaze.GetLocationList(txt3DMazeLine.Text, txt3DMazeLetters.Text);
            if(locations.Count <= 6)
                foreach (var l in locations)
                {
                    var color = Color.Magenta;
                    if (l[0] == y && l[1] == x)
                        color = Color.Cyan;
                    e.Graphics.DrawMarker(color, l[1], l[0], l[2]);
                }
           

            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    const string directions = "UDLR";
                    for(var k = 0; k < 4; k++)
                    {
	                    if(!_3Dmaze.IsTravelPossible(maze,i,j,directions.Substring(k,1)))
		                    e.Graphics.DrawWall(Color.Red, 7f, j, i, directions.Substring(k,1));
                    }

	                e.Graphics.DrawString(_3Dmaze.MazeLetterAtLocation(maze,j,i).ToUpper(),
                        txt3DMazeLetters.Font,Brushes.Red,new Point((i*47)+10,(j*47)+10) );
                }
            }

            if (x < 0 || y < 0) return;
            if (txt3DMazeCardinal.Text.Length != 1) return;

            if (!"NEWSnews".Contains(txt3DMazeCardinal.Text)) return;

            var wallhit = false;
            while (!wallhit)
            {
                wallhit = !_3Dmaze.IsTravelPossible(maze, y, x, txt3DMazeCardinal.Text);
                switch (txt3DMazeCardinal.Text)
                {
                    case "N":
                    case "n":
                        if (wallhit) e.Graphics.DrawWall(Color.Green, 7f, x, y, "North");
                        y--;
                        if (y < 0) y = 7;
                        if (wallhit) e.Graphics.DrawWall(Color.Green, 7f, x, y, "South");
                        break;
                    case "S":
                    case "s":
                        if (wallhit) e.Graphics.DrawWall(Color.Green, 7f, x, y, "South");
                        y++;
                        y %= 8;
                        if (wallhit) e.Graphics.DrawWall(Color.Green, 7f, x, y, "North");
                        break;
                    case "E":
                    case "e":
                        if (wallhit) e.Graphics.DrawWall(Color.Green, 7f, x, y, "East");
                        x++;
                        x %= 8;
                        if (wallhit) e.Graphics.DrawWall(Color.Green, 7f, x, y, "West");
                        break;
                    case "W":
                    case "w":
                        if (wallhit) e.Graphics.DrawWall(Color.Green, 7f, x, y, "West");
                        x--;
                        if (x < 0) x = 7;
                        if (wallhit) e.Graphics.DrawWall(Color.Green, 7f, x, y, "East");
                        break;
                }
            }
        }

        private readonly _3DMaze _3Dmaze = new _3DMaze();
        private void Txt3DMazeLetters_TextChanged(object sender, EventArgs e)
        {
            Refresh();
            txt3DMazeOut.Text = _3Dmaze.FindLocation(txt3DMazeLine.Text, txt3DMazeLetters.Text);
        }

        private void Txt3DMazeLine_TextChanged(object sender, EventArgs e)
        {
            Refresh();
            txt3DMazeOut.Text = _3Dmaze.FindLocation(txt3DMazeLine.Text, txt3DMazeLetters.Text);
        }

        private void TxtPasswordSubmitID_TextChanged(object sender, EventArgs e)
        {
            Password_TextChanged(sender, e);
            MorseCodeInput_TextChanged(sender, e);
        }

        private void TxtSkewedIn_TextChanged(object sender, EventArgs e)
        {
            txtSkewedOut.Text = new SkewedSlots().GetAnswer(txtSkewedIn.Text);
        }

        private void TxtLightCycleIn_TextChanged(object sender, EventArgs e)
        {
            txtLightCycleOut.Text = LightCycle.GetAnswer(txtLightCycleIn.Text.Trim().ToCharArray());
        }

        private void CbBlindAlleyTM_CheckedChanged()
        {
            var score = new int[10];
            var indicatorNames = new[]
            {
                "BOB", "CAR", "CLR", "FRK", "FRQ", "IND", "MSA", "NSA", "SIG", "SND", "TRN"
            };
            var unlitIndex = new[] {1, 2, 9, 8, 4, 4, 9, 2, 5, 5, 4};
            var litIndex = new[] {6, 1, 6, 2, 7, 1, 8, 5, 7, 9, 7};

            var ports = new[]
            {
                IsPortPresent(PortTypes.DVI), IsPortPresent(PortTypes.Parallel),
                IsPortPresent(PortTypes.PS2), IsPortPresent(PortTypes.StereoRCA),
                IsPortPresent(PortTypes.RJ45), IsPortPresent(PortTypes.Serial)
            };
            var portIndex = new[] {4, 8, 6, 9, 2, 6};

            var scoreIndex = new[] {null, cbBlindAlleyTL, cbBlindAlleyTM, null, cbBlindAlleyML, cbBlindAlleyMM, cbBlindAlleyMR, cbBlindAlleyBL, cbBlindAlleyBM, cbBlindAlleyBR };

            if (SerialNumberDigits.Any(digit => digit % 2 == 0))
                score[7]++;
            if (SerialNumberContainsVowel())
                score[8]++;
            for (var i = 0; i < indicatorNames.Length; i++)
            {
                if (UnlitIndicators.Contains(indicatorNames[i]))
                    score[unlitIndex[i]]++;
                if (LitIndicators.Contains(indicatorNames[i]))
                    score[litIndex[i]]++;
            }
            for (var i = 0; i < ports.Length; i++)
            {
                if (ports[i])
                    score[portIndex[i]]++;
            }

            if (Batteries.TotalBatteries % 2 == 0)
                score[5]++;
            if (Batteries.Holders.Length % 2 == 0)
                score[1]++;

            for (var i = 0; i < score.Length; i++)
            {
                if (scoreIndex[i] == null) continue;
                scoreIndex[i].Checked = score[i] == score.Max();
            }
        }

        private void TxtBattleShipSafeSpots_TextChanged()
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "1234567890";
            txtBattleShipSafeSpots.Text = "";
            for (var i = 0; i < SerialNumberDigits.Length && i < SerialNumberLetters.Length; i++)
            {
                var idx1 = letters.IndexOf(SerialNumberLetters[i], StringComparison.Ordinal);
                var idx2 = numbers.IndexOf(SerialNumberDigits[i].ToString(), StringComparison.Ordinal);
                idx1 %= 5;
                idx2 %= 5;
                var safe = $@"{letters[idx1]}{numbers[idx2]}";
                if(!txtBattleShipSafeSpots.Text.Contains(safe))
                    txtBattleShipSafeSpots.Text += safe + @" ";
            }
            var ports = CountTotalPorts() + 4;
            var battIndicators = Batteries.TotalBatteries + LitIndicators.Length +
                                 UnlitIndicators.Length + 4;
            ports %= 5;
            battIndicators %= 5;
            var safe2 = $@"{letters[ports]}{numbers[battIndicators]}";
            if (!txtBattleShipSafeSpots.Text.Contains(safe2))
                txtBattleShipSafeSpots.Text += safe2;
        }

        private void ComputeBitWiseOperators()
        {
            var byte1 = new[]
            {
                Batteries.AABatteries == 0,
                IsPortPresent(PortTypes.Parallel),
                LitIndicators.Contains("NSA"),
                GetModuleCount > GetBombMinutes,
                LitIndicators.Length > 1,
                GetModuleCount % 3 == 0,
                Batteries.DBatteries < 2,
                CountTotalPorts() < 4
            };
            var byte2 = new[]
            {
                Batteries.DBatteries > 0,
                CountTotalPorts() >= 3,
                Batteries.Holders.Length >= 2,
                LitIndicators.Contains("BOB"),
                UnlitIndicators.Length > 1,
                SerialNumberLastDigitOdd(),
                GetModuleCount % 2 == 0,
                Batteries.TotalBatteries >= 2
            };
            txtBitWiseOperatorsAND.Text = "";
            txtBitWiseOperatorsNOT.Text = "";
            txtBitWiseOperatorsOR.Text = "";
            txtBitWiseOperatorsXOR.Text = "";
            for (var i = 0; i < 8; i++)
            {
                txtBitWiseOperatorsAND.Text += byte1[i] && byte2[i] ? "1" : "0";
                txtBitWiseOperatorsOR.Text  += byte1[i] || byte2[i] ? "1" : "0";
                txtBitWiseOperatorsXOR.Text += (byte1[i] && !byte2[i]) || (!byte1[i] && byte2[i]) ? "1" : "0";
                txtBitWiseOperatorsNOT.Text += !byte1[i] ? "1" : "0";
            }
        }

        private void TxtFizzBuzz1IN_TextChanged(object sender, EventArgs e)
        {
            txtFizzBuzz1OUT.Text = FizzBuzz.ComputeFizzBuzz(txtFizzBuzz1IN.Text, facts_strike.SelectedIndex);
            txtFizzBuzz2OUT.Text = FizzBuzz.ComputeFizzBuzz(txtFizzBuzz2IN.Text, facts_strike.SelectedIndex);
            txtFizzBuzz3OUT.Text = FizzBuzz.ComputeFizzBuzz(txtFizzBuzz3IN.Text, facts_strike.SelectedIndex);
        }

        private void TxtLaundryIn_TextChanged(object sender, EventArgs e)
        {
	        txtLaundryOut.Text = Laundry.GetLaundrySolution(txtLaundryIn.Text, GetModuleCount);
        }

        private void TxtAdjacentLettersIN_TextChanged(object sender, EventArgs e)
        {
            txtAdjacentLettersOUT.Text = AdjacentLetters.GetAnswer(txtAdjacentLettersIN.Text.Replace(" ","").ToUpperInvariant());
        }

        private void CbAcidColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNeutralizationOut.Text = new Neutralization(cbAcidColor.SelectedIndex, cbAcidVolume.SelectedIndex).GetAnswer();
        }

        private void TxtRubiksCubeIn_TextChanged(object sender, EventArgs e)
        {
            txtRubiksCubeOut.Text = new RubiksCube().GetAnswer(txtRubiksCubeIn.Text);
        }

        private void TxtMorseMaticsIN_TextChanged(object sender, EventArgs e)
        {
            txtMorseMaticsOut.Text = new MorseMatics().GenSolution(txtMorseMaticsIN.Text);
        }

        private void TxtMorseAMaze_TextChanged(object sender, EventArgs e)
        {
            if (_mazeRefreshing) return;
            
            _useMorseAMaze = true;
            _mazeRefreshing = true;
            _mazeSelectionText = "";
            _mazeRefreshing = false;
            
            var selection = MorseAMaze.Instance.GetMaze(txtMorseAMaze.Text, txtMorseAMazeParams.Text, lblMorseAMazeParameters);
            flpMorseAMazeParameters.Visible = lblMorseAMazeParameters.Text != Empty;
            if (_mazeSelection == selection) return;
            _mazeSelection = selection;
            rbStart.Checked = true;
            Refresh();
        }

        private void TxtCruelPianoInput_TextChanged(object sender, EventArgs e)
        {
            txtCruelPianoOutput.Text = Empty;
            var input = txtCruelPianoInput.Text.Trim().ToLowerInvariant().Split(new [] {" "},StringSplitOptions.RemoveEmptyEntries);
            if (input.Length <= 0) return;

			Note[] notes;
			if (!int.TryParse(input[0], out int index))
            {
                switch (txtCruelPianoInput.Text.ToLowerInvariant())
                {
                    case "ff":
                        notes = MelodyDatabase.FinalFantasyVictory.Notes;
                        break;
                    case "guiles":
                        notes = MelodyDatabase.GuilesTheme.Notes;
                        break;
                    case "jamesbond":
                        notes = MelodyDatabase.JamesBond.Notes;
                        break;
                    case "jurrasicpark":
                        notes = MelodyDatabase.JurrasicPark.Notes;
                        break;
                    case "supermario":
                    case "super mario":
                        notes = MelodyDatabase.SuperMarioBros.Notes;
                        break;
                    case "pinkpanther":
                        notes = MelodyDatabase.PinkPanther.Notes;
                        break;
                    case "super man":
                        notes = MelodyDatabase.Superman.Notes;
                        break;
                    case "tetris":
                        notes = MelodyDatabase.TetrisA.Notes;
                        break;
                    case "starwars":
                        notes = MelodyDatabase.EmpireStrikesBack.Notes;
                        break;
                    case "fairy":
                        notes = MelodyDatabase.ZeldasLullaby.Notes;
                        break;
                    default:
	                    List<Melody> melodies = new List<Melody>(MelodyDatabase.StandardMelodies);
	                    melodies.AddRange(MelodyDatabase.FestiveMelodies);
	                    melodies.Add(MelodyDatabase.GenerateCarolOfTheBells(SerialNumberDigits.Length > 0 ? SerialNumberDigits.Max() : 0));
						notes = (from melody in melodies where melody.Name.ToLowerInvariant().Contains(txtCruelPianoInput.Text.ToLowerInvariant()) select melody.Notes).FirstOrDefault();
                        if (notes == null) return;
                        break;
                }
            }
            else
            {
                if (index < 0 || index >= 10) return;
                notes = MelodyDatabase.SerialismMelodies[index].Notes;
                if (input.Length > 1)
                {
					var transposeparsed = int.TryParse(input[1], out int transpose);
					switch (input[1])
                    {
                        case "p":
                            break;
                        case "i":
                            notes = notes.Inversion().ToArray();
                            break;
                        case "r":
                            notes = notes.Retrograde().ToArray();
                            break;
                        case "ri":
                        case "ir":
                            notes = notes.Inversion().Retrograde().ToArray();
                            break;
                    }

                    if (transposeparsed || (input.Length > 2 && int.TryParse(input[2], out transpose)))
                    {
                        notes = notes.Transpose(transpose).ToArray();
                    }
                }
            }
            foreach (var note in notes)
            {
                txtCruelPianoOutput.Text += note.Semitone.GetDescription().Split('/')[note.UseAlternate ? 1 : 0] + @" ";
            }
        }

        private void NkReset_Click(object sender, EventArgs e)
        {
            foreach (var cb in fpKnob.Controls.Cast<Control>().Select(x => (x as CheckBox)).Where(y => y != null).ToList())
            {
                cb.CheckState = CheckState.Indeterminate;
            }
        }
	}


	public enum ProbingFrequencies
    {
        Unknown = -1,
        TenHz,
        TwentyTwoHz,
        FiftyHz,
        SixtyHz
    }

    public class ProbingSet
    {
        public ProbingFrequencies Missing = ProbingFrequencies.Unknown;
        public bool Even;
    }
}