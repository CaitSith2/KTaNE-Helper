using System;
using System.CodeDom;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.String;

namespace KTaNE_Helper
{
    public partial class Form1 : Form
    {

        readonly string[,,] _manual241Mazes = 
        {
            {{"rd", "lr", "ld", "rd", "lr", "l"},{"ud", "rd", "ul", "ur", "lr", "ld"},{"ud", "ur", "ld", "rd", "lr", "uld"},{"ud", "r", "ulr", "lu", "r", "uld"},{"urd", "lr", "ld", "rd", "l", "ud"},{"ur", "l", "ur", "ul", "r", "ul"}},
            {{"r", "lrd", "l", "rd", "lrd", "l"},{"rd", "ul", "rd", "ul", "ur", "ld"},{"ud", "rd", "ul", "rd", "lr", "uld"},{"urd", "ul", "rd", "ul", "d", "ud"},{"ud", "d", "ud", "rd", "ul", "ud"},{"u", "ur", "ul", "ur", "lr", "lu"}},
            {{"dr", "lr", "ld", "d", "dr", "dl"},{"u", "d", "ud", "ur", "lu", "ud"},{"dr", "uld", "ud", "rd", "ld", "ud"},{"ud", "ud", "ud", "ud", "ud", "ud"},{"ud", "ur", "ul", "ud", "ud", "ud"},{"ur", "lr", "lr", "ul", "ur", "ul"}},
            {{"rd", "ld", "r", "lr", "lr", "ld"},{"ud", "ud", "dr", "lr", "lr", "uld"},{"ud", "ur", "lu", "rd", "l", "ud"},{"ud", "r", "lr", "lru", "lr", "lud"},{"udr", "lr", "lr", "lr", "ld", "ud"},{"ur", "lr", "l", "r", "ul", "u"}},
            {{"r", "lr", "lr", "lr", "lrd", "ld"},{"rd", "lr", "lr", "lrd", "lu", "u"},{"udr", "ld", "r", "ul", "rd", "ld"},{"ud", "ur", "lr", "ld", "u", "ud"},{"ud", "rd", "lr", "ulr", "l", "ud"},{"u", "ur", "lr", "lr", "lr", "lu"}},
            {{"d", "dr", "ld", "r", "ldr", "ld"},{"ud", "ud", "ud", "rd", "ul", "ud"},{"udr", "ul", "u", "ud", "rd", "ul"},{"ur", "ld", "dr", "udl", "ud", "d"},{"rd", "ul", "u", "ud", "ur", "uld"},{"ur", "lr", "lr", "ul", "r", "ul"}},
            {{"dr", "lr", "lr", "ld", "dr", "ld"},{"ud", "rd", "l", "ur", "lu", "ud"},{"ur", "ul", "rd", "l", "rd", "ul"},{"dr", "ld", "udr", "lr", "ul", "d"},{"ud", "u", "ur", "lr", "ld", "ud"},{"ur", "lr", "lr", "lr", "ulr", "ul"}},
            {{"d", "dr", "lr", "ld", "dr", "ld"},{"udr", "ulr", "l", "ur", "ul", "ud"},{"ud", "dr", "lr", "lr", "ld", "ud"},{"ud", "ur", "ld", "r", "ulr", "ul"},{"ud", "d", "ur", "lr", "lr", "l"},{"ur", "ulr", "lr", "lr", "lr", "l"}},
            {{"d", "dr", "lr", "lr", "ldr", "ld"},{"ud", "ud", "rd", "l", "ud", "ud"},{"udr", "ulr", "ul", "rd", "ul", "ud"},{"ud", "d", "dr", "ul", "r", "uld"},{"ud", "ud", "ud", "dr", "dl", "u"},{"ur", "ul", "ur", "ul", "ur", "l"}}
        };

        private readonly string[,,] _manual724Mazes = 
        {
            {{"d","rd","ld","r","ldr","l"},{"ud","u","ur","ld","ur","ld"},{"urd","lr","l","ud","rd","uld"},{"ud","rd","ld","ud","ud","ud"},{"ud","ud","ud","ur","ul","ud"},{"ur","ul","ur","lr","lr","ul"}},
            {{"rd","lr","ld","d","r","ld"},{"ur","ld","ud","ur","lr","uld"},{"rd","ul","ur","lr","l","ud"},{"ud","rd","ld","rd","lr","ul"},{"urd","ul","ud","ur","ld","d"},{"ur","l","ur","lr","ulr","ul"}},
            {{"rd","rl","rl","rl","rl","ld"},{"ur","rl","rld","ld","r","uld"},{"rd","ld","u","ur","ld","u"},{"ud","ur","lr","lrd","lu","d"},{"ud","r","lr","lu","rd","lu"},{"ur","lr","lr","lr","ulr","l"}},
            {{"rd","lr","lr","lrd","lr","ld"},{"ur","lr","ld","ud","d","ud"},{"r","ld","ud","ud","ud","ud"},{"d","ud","u","ud","ud","ud"},{"ud","ud","rd","lu","urd","lu"},{"ur","lur","lu","r","lur","l"}},
            {{"rd","lr","lr","lr","lr","ld"},{"ur","lr","ld","r","ld","ud"},{"d","rd","lur","lr","ul","ud"},{"ur","ul","rd","lr","lr","uld"},{"rd","ld","ud","rd","lr","ul"},{"u","ur","ul","ur","lr","l"}},
            {{"rd","lr","ldr","lr","ld","d"},{"ur","ld","ud","r","lur","uld"},{"d","ud","ur","lr","ld","ud"},{"urd","lur","l","rd","ul","ud"},{"ur","ld","rd","ul","rd","ul"},{"r","ul","u","r","lur","l"}},
            {{"rd","lr","ld","r","lrd","ld"},{"ud","r","lur","lr","lu","ud"},{"urd","lr","ld","rd","lr","ul"},{"ur","ld","ud","u","rd","ld"},{"rd","lu","ud","r","lu","ud"},{"ur","l","ur","lr","lr","lu"}},
            {{"rd","ld","rd","lrd","ld","d"},{"ud","ur","lu","ud","ud","ud"},{"ur","lr","ld","ud","ur","uld"},{"d","rd","lu","ur","ld","u"},{"ud","ud","rd","ld","ud","d"},{"ur","lur","lu","u","ur","lu"}},
            {{"rd","lr","lr","ldr","lr","ld"},{"u","rd","lr","lu","rd","lu"},{"d","ur","lr","ld","ud","d"},{"ur","ld","rd","lu","ur","uld"},{"rd","lu","ud","d","rd","lu"},{"ur","lr","lu","ur","lur","l"}}
        };

        private readonly int[,] _manual241Coordinates = 
            {{21,36},{25,42},{44,46},{11,41},{35,64},{15,53},{12,62},{14,43},{23,51}};

        private readonly int[,] _manual724Coordinates = 
            {{36,63},{11,64},{55,65},{31,66},{43,44},{14,61},{41,16},{22,56},{23,45}};

        private int _mazeSelection = -1;
        private int _mazeStartXY = 77;
        private int _mazeEndXY = 77;

        private readonly string[] _whosOnFirstStepOneWords =
        {
            string.Empty, "BLANK", "C", "CEE", "DISPLAY", "FIRST", "HOLD ON", "LEAD", "LED", "LEED", "NO", "NOTHING","OKAY", "READ",
            "RED", "REED", "SAYS", "SEE", "THEIR", "THERE", "THEY ARE", "THEY'RE", "UR", "YES", "YOU", "YOU ARE", "YOUR","YOU'RE"
        };

        private readonly int[] _whosOnFirstStepOneIndex241 =
        {
            2,4,3,5,5,3,5,5,1,2,5,1,3,4,4,2,5,5,4,5,1,2,0,1,4,5,4,4
        };

        private readonly int[] _whosOnFirstStapOneIndex724 =
        {
            2,4,2,3,5,0,0,2,5,0,5,0,1,4,3,5,2,2,2,0,2,5,5,2,1,3,1,4
        };


        private readonly string[,] _whosOnFirstStepTwoWords =
        {
            {"BLANK","FIRST","LEFT","MIDDLE","NO","NOTHING","OKAY","PRESS","READY","RIGHT","UHHH","WAIT","WHAT","YES"},
            {"DONE","HOLD","LIKE","NEXT","SURE","U","UH HUH","UH UH","UR","WHAT?","YOU","YOU ARE","YOUR","YOU'RE"}
        };

        private readonly int[] _whosOnFirstStepTwoWordIndex =
        {
            0 + 0, 14 + 0, 0 + 1, 14 + 1, 0 + 2, 14 + 2, 0 + 3, 14 + 3, 0 + 4, 0 + 5, 0 + 6, 0 + 7, 0 + 8, 0 + 9, 14 + 4,
            14 + 5, 14 + 6, 14 + 7, 0 + 10, 14 + 8, 0 + 11, 0 + 12, 14 + 9, 0 + 13, 14 + 10, 14 + 11, 14 + 12, 14 + 13
        };

        private readonly int[,,] _whosOnFirstStepTwoIndex241 =
        {
            {{11,9,6,3,0,7,8,5,4,12,2,10,13,1},{2,6,13,3,4,9,5,10,11,8,0,12,7,1},{9,2,1,4,3,13,0,12,10,11,7,8,6,5},{0,8,6,12,5,7,4,11,2,3,9,1,10,13},{0,10,11,1,12,8,9,13,5,2,7,6,4,3},{10,9,6,3,13,0,5,7,2,12,11,1,4,8},{3,4,1,13,10,5,11,6,2,8,0,7,12,9},{9,3,13,8,7,6,5,10,0,2,1,12,4,11},{13,6,12,3,2,7,9,0,8,4,1,10,5,11},{13,5,8,7,4,11,12,9,3,2,10,0,6,1},{8,5,2,12,6,13,9,4,7,0,10,3,11,1},{10,4,0,6,13,2,1,7,12,11,5,8,9,3},{10,12,2,5,8,0,3,4,6,1,11,13,7,9},{6,9,10,3,1,12,7,8,5,13,2,0,4,11}},
            {{4,6,3,9,12,8,13,1,2,10,5,11,7,0},{11,5,0,7,10,8,4,9,13,3,1,6,12,2},{13,3,5,8,1,0,7,9,6,13,2,4,11,12},{9,6,7,12,1,4,3,2,0,11,8,13,5,10},{11,0,2,13,10,1,6,8,4,5,9,3,12,7},{6,4,3,9,13,8,7,0,5,10,2,1,11,12},{6,12,11,10,0,1,7,3,4,2,13,8,5,9},{8,5,11,13,3,7,0,10,6,2,12,4,1,9},{0,5,8,6,9,4,12,1,13,2,3,7,11,10},{10,1,13,12,5,0,7,2,11,6,8,3,9,4},{4,11,12,13,3,6,8,1,9,10,7,2,0,5},{12,3,2,6,9,0,7,1,10,5,13,4,8,11},{7,11,6,12,3,8,4,5,13,10,9,1,2,0},{10,13,8,3,7,11,5,12,9,6,4,0,2,1}}
        };

        private readonly int[,,] _whosOnFirstStepTwoIndex724 =
        {
            {{7,6,8,0,11,10,12,2,9,4,3,13,1,5},{5,9,10,4,0,12,8,3,11,7,2,6,1,13},{2,7,11,10,5,3,4,1,6,12,13,8,9,0},{10,5,1,2,12,13,8,9,4,3,11,0,7,6},{7,11,3,13,4,5,12,0,9,8,1,6,10,2},{11,6,13,8,12,2,9,0,7,5,1,4,10,3},{9,6,1,11,2,8,7,3,12,5,0,13,10,4},{9,5,7,0,2,1,6,3,13,12,11,4,10,8},{1,13,6,10,2,0,5,9,12,3,7,8,11,4},{0,4,2,5,1,13,9,7,6,10,3,11,8,12},{2,13,8,3,9,12,1,7,4,6,11,10,0,5},{1,12,6,2,0,11,10,5,8,4,3,13,7,9},{2,0,11,7,9,3,1,6,4,8,13,10,12,5},{11,13,12,10,8,6,3,7,9,2,1,4,5,0}},
            {{8,3,5,11,13,4,6,12,2,0,10,9,7,1},{7,11,10,4,8,0,6,1,2,13,12,3,5,9},{12,5,7,8,9,11,10,3,6,13,0,2,4,1},{2,3,6,8,11,13,7,4,5,1,0,12,10,9},{10,6,9,7,5,0,12,4,3,1,2,13,11,8},{9,13,8,11,3,7,6,5,10,0,12,1,2,4},{6,9,7,0,13,1,5,8,3,11,2,10,12,4},{10,11,2,12,8,5,0,4,3,6,7,1,9,13},{0,5,10,6,9,12,7,4,8,2,1,3,13,11},{5,0,4,12,3,11,13,1,2,6,9,7,8,10},{2,10,6,3,13,9,8,7,5,4,0,11,12,1},{11,13,8,12,0,4,7,9,10,1,5,2,3,6},{6,11,5,3,13,12,8,4,1,7,10,2,9,0},{8,12,9,10,7,1,4,6,11,5,2,0,13,3}}
        };

        private int _whosOnFirstLookIndex = 6;


        private readonly int[] _wireSequenceRed241 = { 4, 2, 1, 5, 2, 5, 7, 3, 2 };
        private readonly int[] _wireSequenceBlue241 = { 2, 5, 2, 1, 2, 6, 4, 5, 1 };
        private readonly int[] _wireSequenceBlack241 = { 7, 5, 2, 5, 2, 6, 3, 4, 4 };

        private readonly int[] _wireSequenceRed724 = { 5, 2, 5, 7, 2, 0, 6, 6, 7 };
        private readonly int[] _wireSequenceBlue724 = { 4, 0, 5, 7, 0, 6, 1, 4, 3 };
        private readonly int[] _wireSequenceBlack724 = { 5, 0, 6, 1, 0, 2, 4, 4, 4 };


        private readonly int[] _complicatedWires241 = { 0, 1, 1, 1, 2, 3, 4, 1, 0, 0, 2, 4, 3, 3, 4, 2 };
        private readonly int[] _complicatedWires724 = { 0, 1, 2, 1, 3, 3, 1, 0, 0, 3, 0, 4, 4, 2, 4, 0 };

        private readonly string[] _keypadOrder241 = { "ϘѦƛϞѬϗϿ", "ӬϘϿҨ☆ϗ¿", "©ѼҨҖԆƛ☆", "Ϭ¶ѣѬҖ¿ټ", "ΨټѣϾ¶ѯ★", "ϬӬ҂ӕΨҊΩ" };
        private readonly string[] _keypadOrder724 = { "ϬҨҖ☆¶Ͽζ", "ҨҊƛѦϫ¶Җ", "ѬϬϗζΨƛѼ", "Ѭټ◌©ϞϿϗ", "Ϙ©¿Ѫ☆★ϫ", "ӕԆӬѪѣѼΨ" };

        private readonly string[] _keypadSymbols241 = { "¿","©","¶","☆","★","҂","ƛ","Ͼ","Ͽ","Ψ","Ω","Ϟ","Ϙ","ϗ","Ϭ","Җ","Ҋ","Ҩ","Ӭ","ѣ","Ѧ","Ѭ","ѯ","Ѽ","ӕ","Ԇ","ټ"};
        private readonly string[] _keypadSymbols724 = { "¿","©","¶","☆","★","◌","ƛ","ζ","Ͽ","Ψ","Ϟ","Ϙ","ϗ","ϫ","Ϭ","Җ","Ҋ","Ҩ","Ӭ","ѣ","Ѧ","Ѫ","Ѭ","Ѽ","ӕ","Ԇ","ټ"};

        private int _manualVersion;

        private readonly List<TabPage> _allPages = new List<TabPage>();
        private readonly List<TabPage> _noModPages = new List<TabPage>();

        

        public Form1()
        {
            InitializeComponent();
        }

        private void wsReset_Click(object sender, EventArgs e)
        {
            ws_input.Text = "";
        }

        

        private void Complicated_Wires_Event(object sender, EventArgs e)
        {
            var batts = (int)nudBatteriesAA.Value + (int)nudBatteriesD.Value;
            cw_input.Text = cw_input.Text.ToUpper();
            cw_input.SelectionStart = cw_input.Text.Length;


            var cwcode = (_manualVersion == 0 ? _complicatedWires241 : _complicatedWires724);

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
                    var i = 0;
                    if (wire.Contains("r")) i += 1;
                    if (wire.Contains("b")) i += 2;
                    if (wire.Contains("l")) i += 4;
                    if (wire.Contains("s")) i += 8;
                    switch (cwcode[i])
                    {
                        case 0:
                            //Cut, period.
                            outputStr += @"Cut";
                            typestr += @"C";
                            break;
                        case 1:
                            outputStr += (!SerialNumberLastDigitOdd() ? "Cut" : "Leave");
                            typestr += @"S";
                            //Cut if last digit of serial is Even
                            break;
                        case 2:
                            outputStr += @"Leave";
                            typestr += @"D";
                            //Don't Cut, Period.
                            break;
                        case 3:
                            outputStr += (batts >= 2 ? "Cut" : "Leave");
                            typestr += @"B";
                            //Cut if 2 or more Batteries
                            break;
                        case 4:
                            outputStr += (nudPortParallel.Value > 0 ? "Cut" : "Leave");
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

        private static void Simon_Says_Set_Label(Control label, int color)
        {
            var labelText = new[] { "Blue", "Red", "Green", "Yellow" };
            var labelColor = new[] { Color.DodgerBlue, Color.Red, Color.Green, Color.Yellow };
            if (color > 3) return;
            label.Text = labelText[color];
            label.ForeColor = labelColor[color];
        }

        private void Simon_Says_Event()
        {
            var strikes = facts_strike.SelectedIndex;

            var red    = (_manualVersion == 0 ? new[] { 0,3,2,0,1,3 } : new[] { 3,2,3,1,0,3 });
            var blue   = (_manualVersion == 0 ? new[] { 1,2,1,3,0,2 } : new[] { 1,1,3,3,0,1 });
            var green  = (_manualVersion == 0 ? new[] { 3,0,3,2,3,0 } : new[] { 3,2,3,1,0,3 });
            var yellow = (_manualVersion == 0 ? new[] { 2,1,0,1,2,1 } : new[] { 0,2,2,1,2,3 });

            var i = (SerialNumberContainsVowel() ? 0 : 3) + strikes;

            Simon_Says_Set_Label(ss_red, red[i]);
            Simon_Says_Set_Label(ss_green, green[i]);
            Simon_Says_Set_Label(ss_blue, blue[i]);
            Simon_Says_Set_Label(ss_yellow, yellow[i]);
        }

        private void Button_Event(object sender, EventArgs e)
        {
            var color = button_color.SelectedIndex;
            var name = button_name.SelectedIndex;
            var batts = (int) (nudBatteriesAA.Value + nudBatteriesD.Value);

            if (_manualVersion == 0)
            {
                //button_color.Visible = name != 1;

                if (color == 1) lblButtonQuery.Text += @"Look for a Lit CAR" + Environment.NewLine;

                if ((color == 0) && (name == 0)) lblButtonQuery.Text = "";
                else if (name == 1) lblButtonQuery.Text = @"Is there at least 2 Batteries?";
                else if ((color == 1))
                    lblButtonQuery.Text = @"Is there a Lit CAR Indicator" + Environment.NewLine +
                                          @"If Not, is there at least 3 batteries" + Environment.NewLine + @"and Lit FRK Indicator?";
                else if (color == 2) lblButtonQuery.Text = @"Is there at least 3 batteries"+Environment.NewLine+@"and Lit FRK Indicator?";
                else if ((color == 3) && (name == 3)) lblButtonQuery.Text = "";
                else lblButtonQuery.Text = @"Is there at least 3 batteries" + Environment.NewLine + @"and Lit FRK Indicator?";

                if ((color == 0) && (name == 0)) button_label.Text = @"Hold the Button";
                else if ((name == 1) && (batts > 1)) button_label.Text = @"Press and Release";
                else if ((color == 1) && nudLitCAR.Value > 0) button_label.Text = @"Hold the Button";
                else if ((batts > 2) && nudLitFRK.Value > 0) button_label.Text = @"Press and Release";
                else if (color == 2) button_label.Text = @"Hold the Button";
                else if ((color == 3) && (name == 3)) button_label.Text = @"Press and Release";
                else button_label.Text = @"Hold the Button";
            }
            else
            {
                if ((color == 1) && nudLitCAR.Value > 0) button_label.Text = @"Press and Release";
                else if ((color == 3) && (batts < 2)) button_label.Text = @"Press and Release";
                else button_label.Text = @"Hold the Button";
            }
        }

        private void ManualVersionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            _manualVersion = ManualVersionSelect.SelectedIndex;
            if (_manualVersion == 0)
            {
                linkLabel1.Text = @"http://www.bombmanual.com";
                button_name.Visible = true;
                bluestrip.Text = @"Blue - 4";
                yellowstrip.Text = @"Yellow - 5";
                whitestrip.Text = @"Other - 1";
                otherstrip.Text = @"";
            }
            else
            {
                linkLabel1.Text = @"http://www.lthummus.com/";
                button_name.Visible = false;
                bluestrip.Text = @" Red - 5";
                yellowstrip.Text = @"Yellow - 3";
                whitestrip.Text = @"White - 3";
                otherstrip.Text = @"Other - 4";
            }

            _initLettersNotPresent = false;
            keypadReset_Click(sender, e);
            Needy_Knob_CheckedChanged(sender, e);
            Simon_Says_Event();
            Button_Event(sender, e);
            Complicated_Wires_Event(sender, e);
            wsReset_Click(sender, e);
            simpleWires_Event(sender, e);
            Password_TextChanged(sender, e);
            MemoryReset_Click(sender, e);
            MorseCodeInput_TextChanged(sender, e);
            mazeSelection_TextChanged(sender, e);
            wofStep1.Checked = true;
            wofStep1_CheckedChanged(sender, e);
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
            wireReset_Click(sender, e);
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
                        _moduleNameToTab.Add(x.Trim(), p);

                    if (sm[0].Trim() == "About") continue;
                    if ((string) p?.Tag == "mods")
                        _moduleNames.Add("  " + sm[0].Trim());
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

            checkBox1_CheckedChanged(null, null);
            btnSillySlotsReset_Click(null, null);
        }

        private void wireReset_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < 6; i++)
                ((ComboBox) fpWires.Controls[i]).SelectedIndex = 0;
            wires_input.Text = "";
        }

        private void simpleWires_Event(object sender, EventArgs e)
        {
          //  facts_serial_last_digit.SelectedIndex = (wires_serial_odd.Checked ? 0 : 1);
          //  facts_serial_starts_with_letter.Checked = wires_serial_letter.Checked;
            var conditions = "";
            string result;
            var wires = new int[6];
            for (var i = 0; i < 6; i++)
                wires[i] = ((ComboBox) fpWires.Controls[i]).SelectedIndex;

            var wirecounts = new int[6];
            for (var i = 0; i < 6; i++)
                if (wires[i] >= 0)
                    wirecounts[wires[i]]++;
                else
                    wirecounts[Wires.None]++;

            var count = 6 - wirecounts[Wires.None];

            if (count < 3) return;
            if (count < 6)
            {
                for (var i = 0; i < 6; i++)
                {
                    if (wires[i] != Wires.None) continue;
                    for (var j = i; j < 5; j++)
                    {
                        wires[j] = wires[j + 1];
                    }
                    wires[5] = Wires.None;
                }
            }
            if (_manualVersion == 0)
            {
                if (count == 3)
                {
                    if (wirecounts[Wires.Red] == 0)
                        result = @"Cut Second Wire";
                    else if (wires[2] == Wires.White)
                        result = @"Cut Last Wire";
                    else if (wirecounts[Wires.Blue] > 1)
                        result = @"Cut Last Blue Wire";
                    else
                        result = @"Cut Last Wire";
                }
                else if (count == 4)
                {
                    if (wirecounts[Wires.Red] > 1)
                        conditions = @"Is last digit of Serial Odd?";
                    if ((wirecounts[Wires.Red] > 1) && SerialNumberLastDigitOdd())
                        result = @"Cut Last Red Wire";
                    else if ((wires[3] == Wires.Yellow) && (wirecounts[Wires.Red] == 0))
                        result = @"Cut First Wire";
                    else if (wirecounts[Wires.Blue] == 1)
                        result = @"Cut First Wire";
                    else if (wirecounts[Wires.Yellow] > 1)
                        result = @"Cut Last Wire";
                    else
                        result = @"Cut Second Wire";
                }
                else if (count == 5)
                {
                    if (wires[4] == Wires.Black)
                        conditions = @"Is last digit of Serial Odd?";
                    if ((wires[4] == Wires.Black) && SerialNumberLastDigitOdd())
                        result = @"Cut Fourth Wire";
                    else if ((wirecounts[Wires.Red] == 1) && (wirecounts[Wires.Yellow] > 1))
                        result = @"Cut First Wire";
                    else if (wirecounts[Wires.Black] == 0)
                        result = @"Cut Second Wire";
                    else
                        result = @"Cut First Wire";
                }
                else
                {
                    if (wirecounts[Wires.Yellow] == 0)
                        conditions = @"Is last digit of Serial Odd?";
                    if ((wirecounts[Wires.Yellow] == 0) && SerialNumberLastDigitOdd())
                        result = @"Cut Third Wire";
                    else if ((wirecounts[Wires.Yellow] == 1) && (wirecounts[Wires.White] > 1))
                        result = @"Cut Fourth Wire";
                    else if (wirecounts[Wires.Red] == 0)
                        result = @"Cut Last Wire";
                    else
                        result = @"Cut Fourth Wire";
                }
            }
            else
            {
                if (count == 3)
                {
                    if(wirecounts[Wires.White] == 0)
                        conditions = @"Is the first character of Serial a Letter?";
                    if (wirecounts[Wires.White] == 0 && SerialNumberBeginsWithLetter())
                        result = @"Cut the Second Wire";
                    else if (wirecounts[Wires.Red] == 1)
                        result = @"Cut the First Wire";
                    else if (wirecounts[Wires.Blue] > 1)
                        result = @"Cut the First Blue Wire";
                    else if (wires[2] == Wires.Red)
                        result = @"Cut the Last Wire";
                    else
                        result = @"Cut the Second Wire";
                }
                else if (count == 4)
                {
                    if (wirecounts[Wires.Yellow] == 1 && wires[3] == Wires.Red)
                        result = @"Cut the Third Wire";
                    else if (wires[3] == Wires.White)
                        result = @"Cut the Second Wire";
                    else if (wirecounts[Wires.Yellow] == 0)
                        result = @"Cut the First Wire";
                    else
                        result = @"Cut the Last Wire";
                }
                else if (count == 5)
                {
                    if(wirecounts[Wires.Black] > 1)
                        conditions = @"Is the first character of Serial a Letter?";
                    if (wirecounts[Wires.Black] > 1 && SerialNumberBeginsWithLetter())
                        result = @"Cut the Second Wire";
                    else if (wires[4] == Wires.Blue && wirecounts[Wires.Red] == 1)
                        result = @"Cut the First Wire";
                    else if (wires[4] == Wires.Red)
                        result = @"Cut the Fourth Wire";
                    else if (wirecounts[Wires.Red] == 0)
                        result = @"Cut the Third Wire";
                    else
                        result = @"Cut the First Wire";
                }
                else
                {
                    if (wirecounts[Wires.Red] == 1)
                        result = @"Cut the Red Wire";
                    else if (wires[5] == Wires.Red)
                        result = @"Cut the Last Wire";
                    else if (wirecounts[Wires.Yellow] == 0)
                        result = @"Cut the Fourth Wire";
                    else
                        result = @"Cut the Second Wire";
                }
            }

            txtSimpleWireOutput.Text = result;
            if (conditions != "")
                txtSimpleWireOutput.Text += @", " + conditions;

        }

        private bool _initLettersNotPresent;
        private void Password_TextChanged(object sender, EventArgs e)
        {
            var passwords = new List<string>
            {
                "about","after","again","below","could","every","first",
                "found","great","house","large","learn","never","other",
                "place","plant","point","right","small","sound","spell",
                "still","study","their","there","these","thing","think",
                "three","water","where","which","world","would","write"
            };
            if(_manualVersion == 1)
            {
                passwords = new List<string>
                {
                    "aloof","arena","bleat","boxed","butts","caley","crate",
                    "feret","freak","humus","jewej","joule","joust","knobs",
                    "local","pause","press","prime","rings","sails","snake",
                    "space","splat","spoon","steel","tangy","texas","these",
                    "those","toons","tunes","walks","weird","wodar","words"
                };
            }

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
                        if (found) continue;
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
                    if (((MaskedTextBox)fpPassword.Controls[i]).Text != Empty)
                        includePassword = ((MaskedTextBox) fpPassword.Controls[i]).Text.ToLower().Contains(pass.Substring(i, 1));
                if(includePassword)
                    result.Add(pass);
            }

            var count = 0;
            passResults.Text = Empty;
            foreach (var pass in result)
            {
                if (count != 0)
                    passResults.Text += ((count++ % 8) == 0) ? Environment.NewLine : ", ";
                else
                    count++;
                passResults.Text += pass;
                
            }

        }

        private void PasswordClear_Click(object sender, EventArgs e)
        {
            foreach (var textbox in fpPassword.Controls)
                ((MaskedTextBox) textbox).Text = Empty;
            Password_TextChanged(sender, e);
        }

        private string _memoryInstruction;
        private bool _memoryState = true;
        private int _memoryStage;
        private int _memoryStageNumber;
        private int[] _memoryNumbers = new int[5];
        private int[] _memoryPositions = new int[5];
        private int[] _memoryRules = new int[20];

        void MemoryButtonStates(bool state)
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

        void ProcessNumberRules(int number)
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

        void ProcessPositionRules(int number)
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

            if (_manualVersion == 0)
                _memoryRules = new[]
                {
                    MemoryRules.SecondPos, MemoryRules.SecondPos, MemoryRules.ThirdPos, MemoryRules.FourthPos,
                    MemoryRules.Four, MemoryRules.StageOnePos, MemoryRules.FirstPos, MemoryRules.StageOnePos,
                    MemoryRules.StageTwoLabel, MemoryRules.StageOneLabel, MemoryRules.ThirdPos, MemoryRules.Four,
                    MemoryRules.StageOnePos, MemoryRules.FirstPos, MemoryRules.StageTwoPos, MemoryRules.StageTwoPos,
                    MemoryRules.StageOneLabel,MemoryRules.StageTwoLabel,MemoryRules.StageFourLabel,MemoryRules.StageThreeLabel
                };
            else
            {
                _memoryRules = new[]
                {
                    MemoryRules.FirstPos, MemoryRules.ThirdPos, MemoryRules.SecondPos, MemoryRules.FourthPos,
                    MemoryRules.StageOnePos, MemoryRules.Four, MemoryRules.StageOnePos, MemoryRules.FirstPos,
                    MemoryRules.StageOneLabel, MemoryRules.SecondPos, MemoryRules.Four, MemoryRules.ThirdPos,
                    MemoryRules.StageThreePos, MemoryRules.StageOnePos, MemoryRules.StageTwoPos,MemoryRules.StageThreePos,
                    MemoryRules.StageTwoLabel, MemoryRules.StageFourLabel, MemoryRules.StageOneLabel,MemoryRules.StageThreeLabel
                };
            }

        }

        private void Needy_Knob_CheckedChanged(object sender, EventArgs e)
        {
            var total = 0;
            var directions = new [] { "Up", "Up", "Down", "Down", "Left", "Left", "Right", "Right", ""};
            for(var i = 0; i<6; i++)
                if (((CheckBox) fpKnob.Controls[i]).Checked) total += 1 << i;
            if (_manualVersion == 1 && nk64.Checked) total += 0x40;
            var nkLeds = new[] {0x3C, 0x35, 0x3E, 0x15, 0x08, 0x00, 0x3D, 0x3D};
            if (_manualVersion == 1)
                nkLeds = new[] {0xE2, 0x6D, 0xA2, 0x1E, 0x70, 0x36, 0x77, 0x55 };
            int result;
            for (result = 0; result < 8; result++)
            {
                var mask = ((nkLeds[result] & 128) == 128) ? 127 : 63;
                nk64.Visible = ((nkLeds[result] & 128) == 128);
                if ((total & mask) == (nkLeds[result] & mask))
                    break;
            }

            txtNeedyKnobOut.Text = directions[result];
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabel1.Text);
        }

        private static string WordToMorseCode(string word, int freq)
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
                string letter;
                if (!morseCode.TryGetValue(word.Substring(i, 1), out letter)) continue;
                morse += letter;
                if (spacedMorse != "")
                    spacedMorse += " ";
                spacedMorse += letter;
            }
            return "|" + spacedMorse + "|" + morse + "|" + word +  "," 
                + word + " - 3." + freq + " Mhz" + Environment.NewLine;
        }

        private void MorseCodeInput_TextChanged(object sender, EventArgs e)
        {
            var words = new Dictionary<string, int>
            {
                {"shell",505},{"halls",515},{"slick",522},{"sting",592},
                {"steak",582},{"vector",595},{"strobe",545},{"flick",555},
                {"leaks",542},{"bistro",552},{"beats",600},{"brick",575},
                {"break",572},{"bombs",565},{"trick",532},{"boxes",535}
            };
            if (_manualVersion == 1)
            {
                words = new Dictionary<string, int>
                {
                    {"shell",562},{"halls",552},{"slick",512},{"sting",595},
                    {"vector",565},{"strobe",515},{"flick",582},{"beats",535},
                    {"brick",572},{"break",532},{"bombs",545},{"trick",600},
                    {"bravo",502},{"alien",505},{"hello",575},{"brain",585}
                };
            }
            MorseCodeOutput.Text = "";

            // ReSharper disable once InconsistentNaming
            foreach (var code in words.Select(Entry => 
                new {Entry}).Select(word => WordToMorseCode(word.Entry.Key, word.Entry.Value)).Where(code => code.Split(',')[0].Contains(MorseCodeInput.Text)))
            {
                MorseCodeOutput.Text += code.Split(',')[1];
            }
        }

        private void wires_Input_TextChanged(object sender, EventArgs e)
        {
            txtSimpleWireOutput.Text = "";
            wires_input.Text = wires_input.Text.ToUpper();
            wires_input.SelectionStart = wires_input.Text.Length;
            var wirecount = wires_input.Text.Length;
            var wires = new int[6];
            for (var i = 0; i < wirecount; i++)
            {
                wires[i] = ("|rwkby").IndexOf(wires_input.Text.ToLower().Substring(i, 1), StringComparison.Ordinal);
            }
            for (var i = 0; i < 6; i++)
                ((ComboBox) fpWires.Controls[i]).SelectedIndex = wires[i];
        }

        private void ws_input_TextChanged(object sender, EventArgs e)
        {
            ws_input.Text = ws_input.Text.ToUpper();
            ws_input.SelectionStart = ws_input.Text.Length;
            var sequenceRed = (_manualVersion == 0 ? _wireSequenceRed241 : _wireSequenceRed724);
            var sequenceBlue = (_manualVersion == 0 ? _wireSequenceBlue241 : _wireSequenceBlue724);
            var sequenceBlack = (_manualVersion == 0 ? _wireSequenceBlack241 : _wireSequenceBlack724);

            var wirePairs = ws_input.Text.ToLower().Split(' ');
            
            var redCount = 0;
            var blueCount = 0;
            var blackCount = 0;
            ws_output.Text = "";
            if (ws_input.Text.Length == 0) return;

            foreach (var wire in wirePairs.Where(wire => wire.Length >= 2))
            {
                if (ws_output.Text != "") ws_output.Text += @", ";
                var i = (" ab c").IndexOf(wire.Substring(1, 1), StringComparison.Ordinal);
                switch (wire.Substring(0, 1))
                {
                    case "r":
                        if (redCount == 9) continue;
                        ws_output.Text += (sequenceRed[redCount++] & i) == i ? @"Cut" : @"Leave";
                        break;
                    case "b":
                        if (blueCount == 9) continue;
                        ws_output.Text += (sequenceBlue[blueCount++] & i) == i ? @"Cut" : @"Leave";
                        break;
                    case "k":
                        if (blackCount == 9) continue;
                        ws_output.Text += (sequenceBlack[blackCount++] & i) == i ? @"Cut" : @"Leave";
                        break;

                }
            }

        }

        private void mazeSelection_TextChanged(object sender, EventArgs e)
        {
            var coordinates = (_manualVersion == 0 ? _manual241Coordinates : _manual724Coordinates);
            if (mazeSelection.Text.Length < 2) return;

            var x = Convert.ToInt32(mazeSelection.Text.Substring(0, 1));
            var y = Convert.ToInt32(mazeSelection.Text.Substring(1, 1));
            var xy = (y*10) + x;
            _mazeSelection = -1;
            for (var i = 0; i < 9; i++)
            {
                if (coordinates[i, 0] != xy && coordinates[i, 1] != xy) continue;
                _mazeSelection = i;
                break;
            }
            if (_mazeSelection == -1)
            {
                mazeSelection.Text = "";
                Refresh();
                return;
            }

            if (mazeStart.Text.Length >= 2)
            {
                x = Convert.ToInt32(mazeStart.Text.Substring(0, 1));
                y = Convert.ToInt32(mazeStart.Text.Substring(1, 1));
                _mazeStartXY = (y*10) + x;
            }
            else
            {
                _mazeStartXY = 77;
            }

            if (mazeEnd.Text.Length >= 2)
            {
                x = Convert.ToInt32(mazeEnd.Text.Substring(0, 1));
                y = Convert.ToInt32(mazeEnd.Text.Substring(1, 1));
                _mazeEndXY = (y*10) + x;
            }
            else
            {
                _mazeEndXY = 77;
            }

            Refresh();
        }

        private void pbMaze_Paint(object sender, PaintEventArgs e)
        {
            var mazes = (_manualVersion == 0 ? _manual241Mazes : _manual724Mazes);
            var coordinates = (_manualVersion == 0 ? _manual241Coordinates : _manual724Coordinates);
            e.Graphics.FillRectangle(new SolidBrush(Color.Black),0,0,pbMaze.Size.Width,pbMaze.Size.Height );
            int x;
            int y;

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
                    if(!mazes[_mazeSelection,j,i].Contains("u"))
                        e.Graphics.DrawWall(i, j, "Up");
                    if (!mazes[_mazeSelection, j, i].Contains("d"))
                        e.Graphics.DrawWall(i, j, "Down");
                    if (!mazes[_mazeSelection, j, i].Contains("l"))
                        e.Graphics.DrawWall(i, j, "Left");
                    if (!mazes[_mazeSelection, j, i].Contains("r"))
                        e.Graphics.DrawWall(i, j, "Right");

                    e.Graphics.FillRectangle(new SolidBrush(Color.DarkSlateGray), (j * 47) + 18, (i * 47) + 18, 10, 10);
                }
            }
            for (var i = 0; i < 2; i++)
            {
                x = coordinates[_mazeSelection, i]%10;
                y = coordinates[_mazeSelection, i]/10;
                x--;
                y--;
                e.Graphics.DrawEllipse(new Pen(Color.Green,3f),(x*47)+10,(y*47)+10,47-20,47-20 );
            }

            x = _mazeEndXY % 10;
            y = _mazeEndXY / 10;
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
            var mazes = (_manualVersion == 0 ? _manual241Mazes : _manual724Mazes);
            var maze = _mazeSelection;
            var x = startXY%10;
            var y = startXY/10;

            if ((x > 5) || (y > 5) || (maze == -1) || (_endXY == 66)) return false;
            var directions = mazes[maze, y, x];
            if (startXY == _endXY) return true;
            _explored[startXY] = true;


            var directionLetter = new[] {"u", "d", "l", "r"};
            var directionInt = new[] {-10, 10, -1, 1};
            var directionReturn = new[] {"Up", "Down", "Left", "Right"};

            for (var i = 0; i < 4; i++)
            {
                if (!directions.Contains(directionLetter[i])) continue;
                if (_explored[startXY + directionInt[i]]) continue;
                if (!GenerateMazeSolution(startXY + directionInt[i])) continue;
                _mazeStack.Push(directionReturn[i]);
                return true;
            }

            return false;
        }

        private void pbMaze_Click(object sender, EventArgs e)
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
                mazeSelection.Text = xy.ToString();
            }
            else if (rbStart.Checked)
            {
                mazeStart.Text = xy.ToString();
                rbEnd.Checked = cbAutoAdvance.Checked;
            }
            else
            {
                mazeEnd.Text = xy.ToString();
                rbGreenCircle.Checked = cbAutoAdvance.Checked;
            }
        }

        private void wofStep1_CheckedChanged(object sender, EventArgs e)
        {
            whosOnFirstStep1.Visible = !wofStep1.Checked;
            wofOutput.Visible = wofStep1.Checked;
            if (!wofStep1.Checked) return;

            _whosOnFirstLookIndex = 6;
            wofStep1Label.Text = "";
            rbWOF_CheckedChanged(sender, e);
            for (var i = 0; i < 28; i++)
            {
                ((Button) wofButtons.Controls[i]).Text = _whosOnFirstStepOneWords[i];
                ((Button) wofButtons.Controls[i]).Tag = i;
            }
        }

        private void wofStep2_CheckedChanged(object sender, EventArgs e)
        {
            if (!wofStep2.Checked) return;
            wofOutput.Text = "";
            wofStep2Label.Text = "";
            for (var i = 0; i < 28; i++)
            {
                var x = _whosOnFirstStepTwoWordIndex[i] / 14;
                var y = _whosOnFirstStepTwoWordIndex[i] % 14;
                ((Button) wofButtons.Controls[i]).Text = _whosOnFirstStepTwoWords[x, y];
                ((Button) wofButtons.Controls[i]).Tag = _whosOnFirstStepTwoWordIndex[i];
            }
        }

        private void wofButton_Click(object sender, EventArgs e)
        {
            var step1Index = (_manualVersion == 0 ? _whosOnFirstStepOneIndex241 : _whosOnFirstStapOneIndex724);
            var step2Index = (_manualVersion == 0 ? _whosOnFirstStepTwoIndex241 : _whosOnFirstStepTwoIndex724);
            var text = ((Button) sender).Text;
            var index = Convert.ToInt32(((Button) sender).Tag);
            if (wofStep1.Checked)
            {
                _whosOnFirstLookIndex = step1Index[index];
                rbWOF_CheckedChanged(sender, e);
                wofStep1Label.Text = text;
                wofStep2.Checked = true;
            }
            else
            {
                var x = index/14;
                var y = index%14;
                wofStep2Label.Text = _whosOnFirstStepTwoWords[x,y];
                for (var i = 0; i < 9; i++)
                {
                    var z = step2Index[x, y, i];
                    wofOutput.Text += _whosOnFirstStepTwoWords[x, z] + Environment.NewLine;
                    if (wofStep2Label.Text == _whosOnFirstStepTwoWords[x, z]) break;
                }
                wofStep1.Checked = true;
            }
        }

        private void rbWOF_CheckedChanged(object sender, EventArgs e)
        {
            ((RadioButton) wofLook.Controls[_whosOnFirstLookIndex]).Checked = true;
        }

        private void cw_reset_Click(object sender, EventArgs e)
        {
            cw_input.Text = "";
        }

        private void cw_all_wires_Click(object sender, EventArgs e)
        {
            cw_input.Text = @"W WS WL WSL\R RS RL RSL\B BS BL BSL\RB RBS RBL RBSL";
        }

        readonly string[] _keypadSelection = {"","","","","","","",""};

        private void keypadReset_Click(object sender, EventArgs e)
        {
            var keypadsymbols = (_manualVersion == 0 ? _keypadSymbols241 : _keypadSymbols724);
            

            for (var i = 0; i < 27; i++)
            {
                ((Button) fpKeypadSymbols.Controls[i]).Text = keypadsymbols[i];
            }

            for (var i = 0; i < 6; i++)
                ((Button)fpKeypadOrder.Controls[i]).Visible = false;

            for (var i = 0; i < 8; i++)
                ((Button) fpKeypadSelection.Controls[i]).Text = _keypadSelection[i] = "";
                
            fpKeypadLabel.Visible = false;
        }

        

        private void KeypadSymbol_Click(object sender, EventArgs e)
        {
            var max = cbShowAddonModules.Checked ? 8 : 4;
            var keypadorder = (_manualVersion == 0 ? _keypadOrder241 : _keypadOrder724);

            for (var i = 0; i < max; i++)
                if (((Button) sender).Text == _keypadSelection[i])
                {
                    keypadSelection_Click(fpKeypadSelection.Controls[i], e);
                    break;
                }

            for (var i = max - 1; i > 0; i--)
                _keypadSelection[i] = _keypadSelection[i - 1];

            _keypadSelection[0] = ((Button) sender).Text;

            for (var i = 0; i < max; i++)
                ((Button) fpKeypadSelection.Controls[i]).Text = _keypadSelection[i];
            

            if (_keypadSelection[3] == "") return;
            var keypadFound = true;
            if (_keypadSelection[4] == "")
            {
                for (var i = 0; i < 6; i++)
                {
                    keypadFound = true;
                    for (var j = 0; keypadFound && j < 4; j++)
                    {
                        keypadFound = keypadorder[i].Contains(_keypadSelection[j]);
                    }
                    if (!keypadFound) continue;
                    var order = new Dictionary<int, string>();
                    for (var j = 0; j < 4; j++)
                    {
                        order.Add(keypadorder[i].IndexOf(_keypadSelection[j], StringComparison.Ordinal),
                            _keypadSelection[j]);
                    }
                    var k = 0;
                    for (var j = 0; j < 7 && k < 4; j++)
                    {
                        string result;
                        if (!order.TryGetValue(j, out result)) continue;
                        ((Button) fpKeypadOrder.Controls[k]).Visible = true;
                        ((Button) fpKeypadOrder.Controls[k++]).Text = result;
                    }
                    break;
                }
                fpKeypadLabel.Visible = keypadFound;
                fpKeypadLabel.Text = @"Push the Keypad in this Order";
                if (keypadFound) return;
                for (var i = 0; i < 6; i++)
                {
                    ((Button)fpKeypadOrder.Controls[i]).Visible = false;
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
                        if (keypadorder[i].Contains(_keypadSelection[j]))
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
                    if (keypadorder[keypadtouse].Contains(_keypadSelection[i])) continue;
                    ((Button) fpKeypadOrder.Controls[k]).Visible = true;
                    ((Button) fpKeypadOrder.Controls[k++]).Text = _keypadSelection[i];
                }
            }
            
        }

        private void keypadOrder_Click(object sender, EventArgs e)
        {

        }

        private void keypadSelection_Click(object sender, EventArgs e)
        {
            var i = 0;
            var selection = new string[7];
            for (var j=0;j<8;j++)
                if (_keypadSelection[j] != ((Button) sender).Text)
                    selection[i++] = _keypadSelection[j];
            for (var j = 0; j < 7; j++)
                _keypadSelection[j] = selection[j];
            _keypadSelection[7] = "";
            for (var j = 0; j < 8; j++)
            {
                ((Button) fpKeypadSelection.Controls[j]).Text = _keypadSelection[j];
            }
            for (var j = 0; j < 6; j++)
                ((Button)fpKeypadOrder.Controls[j]).Visible = false;
            fpKeypadLabel.Visible = false;
        }

        private int GetDigitFromCharacter(string text)
        {
            int result;
            if (int.TryParse(text, out result)) return result;
            return -1;
        }

        private int NumberUnlitIndicators()
        {
            var x = (from object c in gbIndicators.Controls
                     where c.GetType() == typeof(NumericUpDown)
                        select ((NumericUpDown) c) into y
                        where y.Name.Contains("nudUnlit")
                            select (int) y.Value).Sum();
            return x;
        }

        private int NumberLitIndicators()
        {
            var x = (from object c in gbIndicators.Controls
                     where c.GetType() == typeof(NumericUpDown)
                        select ((NumericUpDown)c) into y
                        where y.Name.Contains("nudLit")
                            select (int)y.Value).Sum();
            return x;
        }

        private void ForgetMeNot_Event(object sender, EventArgs e)
        {
            txtForgetMeNotOut.Text = "";
            if (txtSerialNumber.Text.Trim().Length < 6)
            {
                txtForgetMeNotOut.Text = @"The Full serial number is needed to calculate the solution";
                return;
            }
            var smallestOdd = 9;
            var largestDigit = 0;
            var numDigits = 0;
            var lastDigit = 0;
            for (var i = 0; i < txtSerialNumber.Text.Trim().Length; i++)
            {
                var num = GetDigitFromCharacter(txtSerialNumber.Text.ToUpper().Substring(i, 1));
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
                var solution = new int[line.Length];
                for (var i = 0; i < line.Length; i++)
                {
                    var num = GetDigitFromCharacter(line.Substring(i, 1));
                    switch (i)
                    {
                        case 0:
                            if (nudUnlitCAR.Value > 0)
                                solution[i] = num + 2;
                            else if (unlit > lit)
                                solution[i] = num + 7;
                            else if (unlit == 0)
                                solution[i] = num + lit;
                            else
                                solution[i] = num + lastDigit;
                            break;
                        case 1:
                            if (nudPortSerial.Value > 0 && (numDigits >= 3))
                                solution[i] = num + 3;
                            else if ((solution[i - 1] % 2) == 0)
                                solution[i] = num + solution[i - 1] + 1;
                            else
                                solution[i] = num + solution[i - 1] - 1;
                            break;
                        default:
                            if ((solution[i - 1] == 0) || (solution[i - 2] == 0))
                                solution[i] = num + largestDigit;
                            else if (((solution[i - 1] % 2) == 0) && ((solution[i - 2] % 2) == 0))
                                solution[i] = num + smallestOdd;
                            else
                            {
                                var x = solution[i - 1] + solution[i - 2];
                                while (x >= 10)
                                    x /= 10;
                                solution[i] = num + x;
                            }
                            break;
                    }
                    solution[i] %= 10;
                    solutionStr += solution[i] + " ";
                }
                txtForgetMeNotOut.Text += solutionStr.Trim() + Environment.NewLine;
            }
        }

        private readonly List<string> _twoBitLookup = new List<string>
            {
                "kb","dk","gv","tk","pv","kp","bv","vt","pz","dt",
                "ee","zk","ke","ck","zp","pp","tp","tg","pd","pt",
                "tz","eb","ec","cc","cz","zv","cv","gc","bt","gt",
                "bz","pk","kz","kg","vd","ce","vb","kd","gg","dg",
                "pb","vv","ge","kv","dz","pe","db","cd","td","cb",
                "gb","tv","kk","bg","bp","vp","ep","tt","ed","zg",
                "de","dd","ev","te","zd","bb","pc","bd","kc","zb",
                "eg","bc","tc","ze","zc","gp","et","vc","tb","vz",
                "ez","ek","dv","cg","ve","dp","bk","pg","gk","gz",
                "kt","ct","zz","vg","gd","cp","be","zt","vk","dc"
            };

        private void txtTwoBitsIN_TextChanged(object sender, EventArgs e)
        {
            var lookup = GetDigitFromCharacter(txtTwoBitsIN.Text);
            txtTwoBitsOUT.Text = lookup > -1 ? _twoBitLookup[lookup] : "";
        }

        private void CalculateInitialTwoBitsCode()
        {
            var batts = (int)(nudBatteriesD.Value + nudBatteriesAA.Value);
            if (txtSerialNumber.Text.Trim().Length < 6)
            {
                txtTwoBitsInitialValue.Text = "";
                return;
            }
            var dict = new Dictionary<string, int>
            {
                {"0", 0},{"1", 0},{"2", 0},{"3", 0},{"4", 0},{"5", 0},
                {"6", 0},{"7", 0},{"8", 0},{"9", 0},{"A", 1},{"B", 2},
                {"C", 3},{"D", 4},{"E", 5},{"F", 6},{"G", 7},{"H", 8},
                {"I", 9},{"J", 10},{"K", 11},{"L", 12},{"M", 13},{"N", 14},
                {"O", 15},{"P", 16},{"Q", 17},{"R", 18},{"S", 19},{"T", 20},
                {"U", 21},{"V", 22},{"W", 23},{"X", 24},{"Y", 25},{"Z", 26},
            };

            var initial = 0;
            for (var i = 0; i < txtSerialNumber.Text.Trim().Length; i++)
            {
                if (dict[txtSerialNumber.Text.Substring(i, 1).ToUpper()] <= 0) continue;
                initial = dict[txtSerialNumber.Text.Substring(i, 1).ToUpper()];
                break;
            }
            initial += ( batts * GetDigitFromCharacter(txtSerialNumber.Text.Substring(txtSerialNumber.Text.Trim().Length - 1, 1)));
            if (nudPortRCA.Value > 0 && nudPortRJ45.Value == 0)
                initial *= 2;
            txtTwoBitsInitialValue.Text = _twoBitLookup[initial%100] + @" / " + (initial % 100);

        }

        private bool SerialNumberContainsVowel()
        {
            var vowels = new List<string> { "A", "E", "I", "O", "U" };
            for (var i = 0; i < 5; i++)
                if (txtSerialNumber.Text.ToUpper().Contains(vowels[i]))
                    return true;
            return false;
        }

        private bool SerialNumberBeginsWithLetter()
        {
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return txtSerialNumber.Text.Trim().Length != 0 
                && letters.Contains(txtSerialNumber.Text.ToUpper().Substring(0, 1));
        }

        private bool SerialNumberLastDigitOdd()
        {
            var odd = "13579";
            return txtSerialNumber.Text.Trim().Length != 0 
                && odd.Contains(txtSerialNumber.Text.Substring(txtSerialNumber.Text.Trim().Length - 1, 1));
        }

        // ReSharper disable once UnusedMember.Local
        private int CountSerialNumberLetters()
        {
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var count = 0;
            for(var i=0;i<txtSerialNumber.Text.Trim().Length;i++)
                if (letters.Contains(txtSerialNumber.Text.ToUpper().Substring(i, 1)))
                    count++;
            return count;
        }

        private int CountSerialNumberDigits()
        {
            var digits = "0123456789";
            var count = 0;
            for (var i = 0; i < txtSerialNumber.Text.Trim().Length; i++)
                if (digits.Contains(txtSerialNumber.Text.Substring(i, 1)))
                    count++;
            return count;
        }

        private bool IsInputValid(string input, string pattern, bool serialRequired=false)
        {
            if (serialRequired && txtSerialNumber.Text.Trim().Length < 6) return false;
            return new Regex(pattern).IsMatch(input);
        }

        private void txtConnections_TextChanged(object sender, EventArgs e)
        {
            txtConnectionCheckOut.Text = "";
            if (!IsInputValid(txtConnections.Text, "[1-8]{2} [1-8]{2} [1-8]{2} [1-8]{2}",true)) return;

            var batts = (int) (nudBatteriesD.Value + nudBatteriesAA.Value);
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
                counts[i] = txtConnections.Text.Count(f => f == digits[i]);
            var unique = 0;
            for (var i = 0; i < 8; i++)
                if (counts[i] > 0)
                    unique++;

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

            var serialchar = txtSerialNumber.Text.ToUpper().Substring(serialdigit - 1, 1);
            var group = -1;
            for(var i=0;i<8 && group == -1;i++)
                if (serials[i].Contains(serialchar))
                    group = i;

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
            if (txtSerialNumber.Text.Trim().Length < 6) return;

            var offset = CountUniquePorts()*7;
            foreach (var c in gbIndicators.Controls)
            {
                if (c.GetType() != typeof(NumericUpDown)) continue;
                var numericUpDown = c as NumericUpDown;
                if (numericUpDown == null) continue;
                if (numericUpDown.Value == 0) continue;
                if (numericUpDown.Name.StartsWith("nudLit"))
                {
                    for (var i = 0; i < 3; i++)
                    {
                        if (!txtSerialNumber.Text.ToUpper().Contains(numericUpDown.Name.Substring(6 + i, 1))) continue;
                        offset += (int) numericUpDown.Value*5;
                        break;
                    }
                }
                if (numericUpDown.Name.StartsWith("nudUnlit"))
                {
                    for (var i = 0; i < 3; i++)
                    {
                        if (!txtSerialNumber.Text.ToUpper().Contains(numericUpDown.Name.Substring(8 + i, 1))) continue;
                        offset += (int) numericUpDown.Value;
                        break;
                    }
                }
            }
            for (var i = 0; i < 6; i++)
            {
                var x = offset;

                try
                {
                    x += safeOffsets[txtSerialNumber.Text.Substring(i, 1).ToUpper()][i];
                }
                catch
                {
                    return;
                }

                if (i == 5)
                    for (var j = 0; j < 5; j++)
                        x += safeOffsets[txtSerialNumber.Text.Substring(j, 1).ToUpper()][5];

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
            Needy_Knob_CheckedChanged(null, null);
            nudBatteryHolders.Value = (nudBatteriesD.Value + (nudBatteriesAA.Value / 2));

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
            simpleWires_Event(null, null);
            Complicated_Wires_Event(null, null);
            //Wire sequences does not depend on bomb information

            //--- Passwords, Morse Code ---//
            //Nothing on this tab depends on bomb information
            Password_TextChanged(null, null);
            MorseCodeInput_TextChanged(null, null);

            //--- Caesar Cipher, Combination Lock, Number Pads, Resistors, Semaphore ---//
            txtCaesarCipherIn_TextChanged(null, null);
            txtCombinationLockIn_TextChanged(null, null);
            txtNumberPadIn_TextChanged(null, null);
            txtResistorsIn_TextChanged(null, null);
            txtSemaphoreIn_TextChanged(null, null);

            //--- Chess, Connection Check, Emoji Math, Flashing Colors, Lettered Keys, Logic, Plumbing, Safety Safe, Two Bits ---//
            txtChessInput_TextChanged(null, null);
            txtConnections_TextChanged(null, null);
            //Emoji Math does not depend on bomb information
            //Flashing Colors does not depend on bomb information
            txtLetteredKeysIn_TextChanged(null, null);
            txtLogicAND_TextChanged(null, null); txtLogicOR_TextChanged(null, null);
            cbPlumbingRedIn_CheckedChanged();
            CalculateSafetySafe();
            CalculateInitialTwoBitsCode();

            //--- Adventure Game, Alphabet, Anagram, Silly Slots, Word Scramble ---//
            txtAdventureGameSTR_TextChanged(null, null);

            //--- Cryptography, Gamepad, Microcontrollers, Murder ---//
            cbMurderRoom_SelectedIndexChanged(null, null);
            cbMicrocontroller_SelectedIndexChanged(null, null);
            txtGamePadX_TextChanged(null, null);

            //--- Forget Me Not ---//
            ForgetMeNot_Event(null, null);

            //--- 3D Maze  (and anything else that requires picture box refresh) ---//
            if(lbModules.SelectedItem?.ToString().Trim() == "3D Maze")
                Refresh();

            txtTwoBitsIN_TextChanged(null, null);
            

        }

        private bool DuplicateSerialCharacters()
        {
            for(var i = 0; i < txtSerialNumber.Text.Trim().Length; i++)
                for(var j = i + 1; j < txtSerialNumber.Text.Trim().Length; j++)
                    if (txtSerialNumber.Text.ToUpper().Substring(i, 1) == txtSerialNumber.Text.ToUpper().Substring(j, 1))
                    {
                        return true;
                    }
            return false;
        }

        private bool DuplicatePorts(int port=Ports.All)
        {
            var portnames = new List<string>
            {
                "nudPort",
                "nudPortDVID",
                "nudPortParallel",
                "nudPortRJ45",
                "nudPortPS2",
                "nudPortSerial",
                "nudPortRCA"
            };
            return (from object c in gbPorts.Controls
                    where c.GetType() == typeof(NumericUpDown)
                        select (NumericUpDown) c into y
                        where y.Name.Contains(portnames[port]) && !y.Name.Contains("nudPortPlates")
                            select y).Aggregate(false, (current, y) => current | y.Value > 1);
        }

        private int CountUniquePorts()
        {
            var x = 0;
            if (nudPortParallel.Value > 0) x++;
            if (nudPortDVID.Value > 0) x++;
            if (nudPortPS2.Value > 0) x++;
            if (nudPortRCA.Value > 0) x++;
            if (nudPortRJ45.Value > 0) x++;
            if (nudPortSerial.Value > 0) x++;
            return x;
        }

        private int CountTotalPorts()
        {
            var x = nudPortParallel.Value;
            x += nudPortDVID.Value;
            x += nudPortPS2.Value;
            x += nudPortRCA.Value;
            x += nudPortRJ45.Value;
            x += nudPortSerial.Value;
            return (int) x;
        }

        private void cbPlumbingRedIn_CheckedChanged()
        {
            var countFor = 0;
            var countAgainst = 0;

            var inputActive = 0;
            var outputActive = 0;

            if (txtSerialNumber.Text.Contains("1"))
                countFor++;
            if (nudPortRJ45.Value == 1)
                countFor++;
            if (DuplicatePorts())
                countAgainst++;
            if (DuplicateSerialCharacters())
                countAgainst++;

            cbPlumbingRedIn.Checked = countFor > countAgainst;
            inputActive += countFor > countAgainst?1:0;
            countFor = countAgainst = 0;

            if (txtSerialNumber.Text.Contains("2"))
                countFor++;
            if (nudPortRCA.Value > 0)
                countFor++;
            if (!DuplicatePorts())
                countAgainst++;
            if (txtSerialNumber.Text.Contains("1") || txtSerialNumber.Text.ToUpper().Contains("L"))
                countAgainst++;

            cbPlumbingYellowIn.Checked = countFor > countAgainst;
            inputActive += countFor > countAgainst ? 1 : 0;
            countFor = countAgainst = 0;

            if (CountSerialNumberDigits() >= 3)
                countFor++;
            if (nudPortDVID.Value > 0)
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
                if ((nudBatteriesAA.Value + nudBatteriesD.Value) >= 4)
                    countFor++;
                if (CountUniquePorts() == 0)
                    countAgainst++;
                if ((nudBatteriesAA.Value + nudBatteriesD.Value) == 0)
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

            if (nudPortSerial.Value > 0)
                countFor++;
            if ((nudBatteriesAA.Value + nudBatteriesD.Value) == 1)
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
            if (txtSerialNumber.Text.Contains("4") || txtSerialNumber.Text.Contains("8"))
                countFor++;
            if (!txtSerialNumber.Text.Contains("2"))
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
                if ((nudBatteriesAA.Value + nudBatteriesD.Value) < 2)
                    countAgainst++;
                if (nudPortParallel.Value == 0)
                    countAgainst++;

                cbPlumbingBlueOut.Checked = countFor > countAgainst;
            }
            else
            {
                cbPlumbingBlueOut.Checked = true;
            }


        }

        private bool _resetBomb;
        private void button42_Click(object sender, EventArgs e)
        {
            _resetBomb = true;
            foreach (var c in gbPorts.Controls)
            {
                if (c.GetType() == typeof(NumericUpDown))
                    ((NumericUpDown) c).Value = 0;
            }
            foreach (var c in gbIndicators.Controls)
            {
                if (c.GetType() == typeof(NumericUpDown))
                    ((NumericUpDown)c).Value = 0;
            }
            foreach (var c in gbBatteries.Controls)
            {
                if (c.GetType() == typeof(NumericUpDown))
                    ((NumericUpDown)c).Value = 0;
            }
            facts_strike.SelectedIndex = 0;
            txtSerialNumber.Text = "";
            _resetBomb = false;
            UpdateBombSolution(null, null);
        }

        private Dictionary<string, bool> BuildTruthTable()
        {
            var batts = nudBatteriesD.Value + nudBatteriesAA.Value;
            return new Dictionary<string, bool>
            {
                {"A",batts > 2},
                {"B",nudPortSerial.Value > 0 },
                {"C",nudPortParallel.Value > 0 },
                {"D",SerialNumberContainsVowel() },
                {"E",!SerialNumberContainsVowel() },
                {"F",nudPortRCA.Value > 0 },
                {"G",nudLitCLR.Value > 0 },
                {"H",nudLitIND.Value > 0 },
                {"I",batts == 0 },
                {"J",nudLitMSA.Value > 0 },
                {"K",SerialNumberLastDigitOdd() },
                {"L", !SerialNumberLastDigitOdd() },
                {"M", nudLitFRK.Value > 0 },
                {"N", batts == 1 },
                {"O", batts == 0 },
                {"P", nudPortRJ45.Value > 0 },
                {"Q",nudPortDVID.Value > 0 },
                {"R",batts > 5 },
                {"S", nudLitSIG.Value > 0 && nudLitCAR.Value > 0 },
                {"T", batts >= 2 && nudPortPS2.Value > 0 },
                {"U",nudPortParallel.Value > 0 && nudPortSerial.Value > 0 },
                {"V", nudLitBOB.Value > 0 },
                {"W", CountSerialNumberDigits() == 6 },
                {"X", CountUniquePorts() >= 4 },
                {"Y", NumberLitIndicators() == 0 },
                {"Z", nudPortRJ45.Value > 0 && nudPortSerial.Value > 0 }
            };
        }

        private void txtLogicAND_TextChanged(object sender, EventArgs e)
        {
            
            if (txtLogicAND.Text.Trim().Length == 0)
            {
                txtLogicAND.ResetBackColor();
                return;
            }
            var truth = BuildTruthTable();
            var and = txtLogicAND.Text.Trim().ToCharArray().Aggregate(true, (current, t) => current & truth[t.ToString()]);


            txtLogicAND.BackColor = and ? Color.Green : Color.Red;
        }

        private void txtLogicOR_TextChanged(object sender, EventArgs e)
        {
            if (txtLogicOR.Text.Trim().Length == 0)
            {
                txtLogicOR.ResetBackColor();
                return;
            }
            var truth = BuildTruthTable();

            var or = txtLogicOR.Text.Trim().ToCharArray().Aggregate(false, (current, t) => current | truth[t.ToString()]);

            txtLogicOR.BackColor = or ? Color.Green : Color.Red;
        }

        private void txtChessInput_TextChanged(object sender, EventArgs e)
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
                        for (j = x + 1; j < 6 && chessboard[j, y] != 1; j++) chessboard[j, y] = 2;
                        for (j = x - 1; j >= 0 && chessboard[j, y] != 1; j--) chessboard[j, y] = 2;
                        for (j = y + 1; j < 6 && chessboard[x, j] != 1; j++) chessboard[x, j] = 2;
                        for (j = y - 1; j >= 0 && chessboard[x, j] != 1; j--) chessboard[x, j] = 2;
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
                        for (j = x + 1, k = y + 1; j < 6 && k < 6 && chessboard[j, k] != 1; j++, k++) chessboard[j, k] = 2;
                        for (j = x - 1, k = y + 1; j >= 0 && k < 6 && chessboard[j, k] != 1; j--, k++) chessboard[j, k] = 2;
                        for (j = x + 1, k = y - 1; j < 6 && k >= 0 && chessboard[j, k] != 1; j++, k--) chessboard[j, k] = 2;
                        for (j = x - 1, k = y - 1; j >= 0 && k >= 0 && chessboard[j, k] != 1; j--, k--) chessboard[j, k] = 2;
                        break;
                    case ChessPieces.Queen:
                        for (j = x + 1; j < 6 && chessboard[j, y] != 1; j++) chessboard[j, y] = 2;
                        for (j = x - 1; j >= 0 && chessboard[j, y] != 1; j--) chessboard[j, y] = 2;
                        for (j = y + 1; j < 6 && chessboard[x, j] != 1; j++) chessboard[x, j] = 2;
                        for (j = y - 1; j >= 0 && chessboard[x, j] != 1; j--) chessboard[x, j] = 2;
                        for (j = x + 1, k = y + 1; j < 6 && k < 6 && chessboard[j, k] != 1; j++, k++) chessboard[j, k] = 2;
                        for (j = x - 1, k = y + 1; j >= 0 && k < 6 && chessboard[j, k] != 1; j--, k++) chessboard[j, k] = 2;
                        for (j = x + 1, k = y - 1; j < 6 && k >= 0 && chessboard[j, k] != 1; j++, k--) chessboard[j, k] = 2;
                        for (j = x - 1, k = y - 1; j >= 0 && k >= 0 && chessboard[j, k] != 1; j--, k--) chessboard[j, k] = 2;
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
                for (var j = 0; j < 6; j++)
                    if (chessboard[i, j] == 0)
                        txtChessSolution.Text += positionsLetters.Substring(i, 1)+positionNumbers.Substring(j, 1);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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
                    lbModules.Items.Add(m);
            }

            if (cbShowAddonModules.Checked)
            {
                foreach (var m in _moduleNames)
                    lbModules.Items.Add(m);
            }

            var index = lbModules.Items.IndexOf(selected);
            lbModules.SelectedIndex = index == -1 ? 0 : index;
            lbModules_SelectedIndexChanged(null, null);

            lblBatteryD.Text = cbShowAddonModules.Checked ? "D" : "Batteries";

            //Keypad specific mod
            for (var i = 4; i < 8; i++)
                ((Button) fpKeypadSelection.Controls[i]).Visible = cbShowAddonModules.Checked;
            while (!cbShowAddonModules.Checked && _keypadSelection[4] != "")
                keypadSelection_Click(fpKeypadSelection.Controls[4], e);
            if (cbShowStockModules.Checked && cbShowAddonModules.Checked)
                gbKeypads.Text = @"Keypads, Round Keypads";
            else if (cbShowStockModules.Checked)
                gbKeypads.Text = @"Keypads";
            else
                gbKeypads.Text = @"Round Keypads";

            gbSimonSays.Visible = cbShowStockModules.Checked;

        }

        private void txtLetteredKeysIn_TextChanged(object sender, EventArgs e)
        {
            txtLetteredKeysOut.Text = "";
            var num = GetDigitFromCharacter(txtLetteredKeysIn.Text);
            var batts = nudBatteriesD.Value + nudBatteriesAA.Value;
            if (num == -1) return;

            if (num == 69) txtLetteredKeysOut.Text = @"D";
            else if ((num%6) == 0) txtLetteredKeysOut.Text = @"A";
            else if (batts >= 2 && (num%3) == 0) txtLetteredKeysOut.Text = @"B";
            else if (txtSerialNumber.Text.ToUpper().Contains("C")
                     || txtSerialNumber.Text.ToUpper().Contains("E")
                     || txtSerialNumber.Text.Contains("3"))
                txtLetteredKeysOut.Text = num >= 22 && num <= 79 ? "B" : "C";
            else txtLetteredKeysOut.Text = num < 46 ? "D" : "A";
        }

        private void txtEmojiMathIn_TextChanged(object sender, EventArgs e)
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

        private void txtStroopColors_TextChanged(object sender, EventArgs e)
        {
            txtStroopAnswer.Text = new Stroop().GetAnswer(txtStroopColors.Text, txtStroopWords.Text);
        }

        private void txtNumberPadIn_TextChanged(object sender, EventArgs e)
        {
            var pad = new NumberPad(txtNumberPadIn.Text,(int)nudBatteriesD.Value + (int)nudBatteriesAA.Value,
                CountTotalPorts(),!SerialNumberLastDigitOdd(),SerialNumberContainsVowel());
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

        private void txtCombinationLockIn_TextChanged(object sender, EventArgs e)
        {
            var input = txtCombinationLockIn.Text.ToUpper();
            var twoFactor = input.Split(' ');
            txtCombinationLockOut.Text = @"Input [Solved modules] [list of two factors]  or [solved modules] S [total modules]";
            if (twoFactor.Length < 2) return;

            var step1 = (int)(nudBatteriesD.Value + nudBatteriesAA.Value);
            var step2 = GetDigitFromCharacter(twoFactor[0]);
            if (input.Contains("S"))
            {
                 if (twoFactor.Length < 3) return;
                if (txtSerialNumber.Text.Trim().Length < 6) return;
                step1 += GetDigitFromCharacter(txtSerialNumber.Text.ToUpper().Substring(5, 1));
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

        private void txtSemaphoreIn_TextChanged(object sender, EventArgs e)
        {
            txtSemaphoreOut.Text = new Semaphore(txtSerialNumber.Text.ToUpper()).GetAnswer(txtSemaphoreIn.Text);
        }

        private void txtCaesarCipherIn_TextChanged(object sender, EventArgs e)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var offset = (int)(nudBatteriesAA.Value + nudBatteriesD.Value);
            if (SerialNumberContainsVowel()) offset--;
            if (!SerialNumberLastDigitOdd()) offset++;
            if (nudLitCAR.Value > 0 || nudUnlitCAR.Value > 0) offset++;

            if (nudPortParallel.Value > 0 && nudLitNSA.Value > 0)
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

        private void txtResistorsIn_TextChanged(object sender, EventArgs e)
        {
            const string digits = "0123456789";
            const string ohm = "Ω";
            const string colors = "SDKNROYGBVAW";
            var resistorsTxt = txtResistorsIn.Text.ToUpper().Split(' ');
            var batts = (int) (nudBatteriesD.Value + nudBatteriesAA.Value);
            if (batts > 6) batts = 6;
            txtResistorsOut.Text = "";

            if (txtSerialNumber.Text.Trim().Length < 6) return;
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
            var targetOut = digits.IndexOf(txtSerialNumber.Text.Substring(5, 1), StringComparison.Ordinal);
            var digitFound = false;
            for (var i = 0; i < 6; i++)
            {
                if (!digits.Contains(txtSerialNumber.Text.Substring(i, 1))) continue;
                resistance *= 10;
                var j = digits.IndexOf(txtSerialNumber.Text.Substring(i, 1), StringComparison.Ordinal);
                resistance += (ulong)j;
                if (!digitFound)
                {
                    targetIn = j;
                    digitFound = true;
                }
                else
                    break;
            }
            resistance *= (ulong) Math.Pow(10.0f, batts);
            if (targetOut < 0)
                targetOut = targetIn;

            var primaryIn = (targetIn%2) == 0 ? "A" : "B";
            var secondaryIn = (targetIn % 2) == 1 ? "A" : "B";
            var primaryOut = (targetOut%2) == 0 ? "C" : "D";
            var secondaryOut = (targetOut % 2) == 1 ? "C" : "D";
            if (nudLitFRK.Value > 0) primaryOut = "C+D";
            else if (nudBatteriesD.Value > 0)
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

        private bool[] txtContainsFrequencies(string text)
        {
            var frequencies = new bool[5];
            switch (text.Trim())
            {

                case "10 22":
                    frequencies[2] = true;
                    frequencies[3] = true;
                    break;
                case "10 50":
                    frequencies[1] = true;
                    frequencies[3] = true;
                    break;
                case "10 60":
                    frequencies[1] = true;
                    frequencies[2] = true;
                    break;
                case "22 50":
                    frequencies[0] = true;
                    frequencies[3] = true;
                    break;
                case "22 60":
                    frequencies[0] = true;
                    frequencies[2] = true;
                    break;
                case "50 60":
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

        private int ProbingCommonFrequency(string pair1, string pair2, bool opposite=false)
        {
            if (IsNullOrWhiteSpace(pair1) || IsNullOrWhiteSpace(pair2)) return -1;
            if (pair1 == pair2) return -1;
            if (!opposite)
            {
                if (pair1.Contains("10") && pair2.Contains("10")) return 1;
                if (pair1.Contains("22") && pair2.Contains("22")) return 2;
                if (pair1.Contains("50") && pair2.Contains("50")) return 5;
                if (pair1.Contains("60") && pair2.Contains("60")) return 6;
            }
            else
            {
                if (pair1.Contains("10") && pair2.Contains("10"))
                {
                    if (pair1.Contains("22")) return 2;
                    if (pair1.Contains("50")) return 5;
                    return 6;
                }
                if (pair1.Contains("22") && pair2.Contains("22"))
                {
                    if (pair1.Contains("10")) return 1;
                    if (pair1.Contains("50")) return 5;
                    return 6;
                }
                if (pair1.Contains("50") && pair2.Contains("50"))
                {
                    if (pair1.Contains("22")) return 2;
                    if (pair1.Contains("10")) return 1;
                    return 6;
                }
                if (pair1.Contains("60") && pair2.Contains("60"))
                {
                    if (pair1.Contains("22")) return 2;
                    if (pair1.Contains("50")) return 5;
                    return 1;
                }
            }
            return -1;
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


        private void txtProbing12_TextChanged(object sender, EventArgs e)
        {
            var wires = new[]
            {
                new ProbingSet(), new ProbingSet(), new ProbingSet(),
                new ProbingSet(), new ProbingSet(), new ProbingSet()
            };
            
            txtProbingOut.Text = @"""I still maintain ""reading order"" on probing is some BS"" - LtHummus (Sept 16, 2016)";
            /*var textBoxes = new[]
            {
                txtProbing12.Text, txtProbing34.Text, txtProbing56.Text,
                txtProbing14.Text, txtProbing25.Text, txtProbing36.Text,
                txtProbing13.Text, txtProbing24.Text, txtProbing35.Text,
                txtProbing46.Text, txtProbing15.Text, txtProbing26.Text,
                txtProbing16.Text, txtProbing23.Text, txtProbing45.Text
            };*/
            var textBoxes = new[]
            {
                txtProbing12.Text, txtProbing34.Text, txtProbing56.Text,
                txtProbing14.Text, txtProbing25.Text
            };
            /*var pairs = new[]
            {
                new[] {0, 1}, new[] {2, 3}, new[] {4, 5},
                new[] {0, 3}, new[] {1, 4}, new[] {2, 5},
                new[] {0, 2}, new[] {1, 3}, new[] {2, 4},
                new[] {3, 5}, new[] {0, 4}, new[] {1, 5},
                new[] {0, 5}, new[] {1, 2}, new[] {3, 4}
            };*/
            var pairs = new[]
            {
                new[] {0, 1}, new[] {0, 2}, new[] {0,3},
                new[] {0, 4}, new[] {0,5}
            };

            for (var i = 0; i < 5; i++)
            {
                if (textBoxes[i].Trim().Length != 5) continue;
                if (txtContainsFrequencies(textBoxes[i])[4])
                {
                    wires[pairs[i][0]].PairsWith.Add(pairs[i][1]);
                    wires[pairs[i][1]].PairsWith.Add(pairs[i][0]);
                    continue;
                }
                for (var j = i+1; j < 5; j++)
                {
                    if (textBoxes[j].Trim().Length != 5) continue;
                    var cn = CommonNumber(pairs[i], pairs[j]);
                    if (cn == -1) continue;
                    var missing = ProbingCommonFrequency(textBoxes[i], textBoxes[j]);
                    if (missing == -1) continue;
                    switch (missing)
                    {
                        case 1:
                            wires[cn].Frequencies[1] = true;
                            wires[cn].Frequencies[2] = true;
                            wires[cn].Frequencies[3] = true;
                            break;
                        case 2:
                            wires[cn].Frequencies[0] = true;
                            wires[cn].Frequencies[2] = true;
                            wires[cn].Frequencies[3] = true;
                            break;
                        case 5:
                            wires[cn].Frequencies[1] = true;
                            wires[cn].Frequencies[0] = true;
                            wires[cn].Frequencies[3] = true;
                            break;
                        case 6:
                            wires[cn].Frequencies[1] = true;
                            wires[cn].Frequencies[2] = true;
                            wires[cn].Frequencies[0] = true;
                            break;

                    }

                    cn = CommonNumber(pairs[i], pairs[j], true);
                    missing = ProbingCommonFrequency(textBoxes[i], textBoxes[j], true);
                    switch (missing)
                    {
                        case 1:
                            wires[cn].Frequencies[1] = true;
                            wires[cn].Frequencies[2] = true;
                            wires[cn].Frequencies[3] = true;
                            break;
                        case 2:
                            wires[cn].Frequencies[0] = true;
                            wires[cn].Frequencies[2] = true;
                            wires[cn].Frequencies[3] = true;
                            break;
                        case 5:
                            wires[cn].Frequencies[1] = true;
                            wires[cn].Frequencies[0] = true;
                            wires[cn].Frequencies[3] = true;
                            break;
                        case 6:
                            wires[cn].Frequencies[1] = true;
                            wires[cn].Frequencies[2] = true;
                            wires[cn].Frequencies[0] = true;
                            break;

                    }

                    cn = CommonNumber(pairs[j], pairs[i], true);
                    missing = ProbingCommonFrequency(textBoxes[j], textBoxes[i], true);
                    switch (missing)
                    {
                        case 1:
                            wires[cn].Frequencies[1] = true;
                            wires[cn].Frequencies[2] = true;
                            wires[cn].Frequencies[3] = true;
                            break;
                        case 2:
                            wires[cn].Frequencies[0] = true;
                            wires[cn].Frequencies[2] = true;
                            wires[cn].Frequencies[3] = true;
                            break;
                        case 5:
                            wires[cn].Frequencies[1] = true;
                            wires[cn].Frequencies[0] = true;
                            wires[cn].Frequencies[3] = true;
                            break;
                        case 6:
                            wires[cn].Frequencies[1] = true;
                            wires[cn].Frequencies[2] = true;
                            wires[cn].Frequencies[0] = true;
                            break;

                    }
                }
                /*
                var wire = txtContainsFrequencies(textBoxes[i]);
                for (var j = 0; j < 4; j++)
                {
                    wires[pairs[i][0]].Frequencies[j] |= wire[j];
                    wires[pairs[i][1]].Frequencies[j] |= wire[j];
                }
                if (!wire[4]) continue;
                wires[pairs[i][0]].PairsWith.Add(pairs[i][1]);
                wires[pairs[i][1]].PairsWith.Add(pairs[i][0]); */
            }

            for (var i = 0; i < 6; i++)
            {
                foreach (var pair in wires[i].PairsWith)
                {
                    for (var j = 0; j < 4; j++)
                        wires[pair].Frequencies[j] |= wires[i].Frequencies[j];
                }
            }

            var counts = new int[6];
            for (var i = 0; i < 6; i++)
                for (var j = 0; j < 4; j++)
                    if (wires[i].Frequencies[j]) counts[i]++;

            var missingFreqs = new bool[4];
            var wiresComplete = 0;
            var incomplete = -1;
            for (var i = 0; i < 6; i++)
            {
                if (counts[i] != 3)
                {
                    incomplete = i;
                    continue;
                }
                wiresComplete++;
                for (var j = 0; j < 4; j++)
                {
                    missingFreqs[j] |= !wires[i].Frequencies[j];
                }
            }
            if (wiresComplete == 5)
            {
                var missing = -1;
                var missingCount = 4;
                for(var j = 0; j < 4; j++)
                    if (!missingFreqs[j])
                    {
                        missing = j;
                    }
                    else
                    {
                        missingCount--;
                    }

                if (missingCount == 1)
                {
                    switch (missing)
                    {
                        case 0:
                            wires[incomplete].Frequencies[1] = true;
                            wires[incomplete].Frequencies[2] = true;
                            wires[incomplete].Frequencies[3] = true;
                            counts[incomplete] = 3;
                            break;
                        case 1:
                            wires[incomplete].Frequencies[0] = true;
                            wires[incomplete].Frequencies[2] = true;
                            wires[incomplete].Frequencies[3] = true;
                            counts[incomplete] = 3;
                            break;
                        case 2:
                            wires[incomplete].Frequencies[1] = true;
                            wires[incomplete].Frequencies[0] = true;
                            wires[incomplete].Frequencies[3] = true;
                            counts[incomplete] = 3;
                            break;
                        case 3:
                            wires[incomplete].Frequencies[1] = true;
                            wires[incomplete].Frequencies[2] = true;
                            wires[incomplete].Frequencies[0] = true;
                            counts[incomplete] = 3;
                            break;
                    }
                }
            }

            

            if (counts[0] == 3 && counts[4] == 3)
            {
                //We might have a solution to this probing.
                //Check for Red soltution
                var red = -1;
                var blue = -1;
                if (wires[0].Frequencies[2])
                {
                    for (var i = 0; i < 6; i++)
                    {
                        if (counts[i] != 3 || wires[i].Frequencies[2]) continue;
                        red = i;
                        break;
                    }
                }
                else if (!wires[4].Frequencies[0])
                {
                    for (var i = 0; i < 6; i++)
                    {
                        if (counts[i] != 3 || wires[i].Frequencies[0]) continue;
                        red = i;
                        break;
                    }
                }
                else
                {
                    for (var i = 0; i < 6; i++)
                    {
                        if (counts[i] != 3 || wires[i].Frequencies[3]) continue;
                        red = i;
                        break;
                    }
                }

                if (wires[4].Frequencies[0])
                {
                    for (var i = 0; i < 6; i++)
                    {
                        if (red == i || counts[i] != 3 || wires[i].Frequencies[1]) continue;
                        blue = i;
                        break;
                    }
                }
                else
                {
                    for (var i = 0; i < 6; i++)
                    {
                        if (red == i || counts[i] != 3 || wires[i].Frequencies[3]) continue;
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
                var freqtxt = new[] {"10", "22", "50", "60"};
                var count = 0;
                if (counts[i] == 0) continue;
                if (text == txtProbingOut.Text) txtProbingOut.Text = "";
                txtProbingOut.Text += (i+1) + @" = ";
                for (var j = 0; j < 4; j++)
                {
                    if (!wires[i].Frequencies[j]) continue;
                    if (count++ > 0) txtProbingOut.Text += @",";
                    txtProbingOut.Text += freqtxt[j];
                }
                txtProbingOut.Text += @"  ";
            }

        }

        private void btnResetAll2_Click(object sender, EventArgs e)
        {
            btnResetCombinationLock_Click(sender, e);
            btnResetSemaphore_Click(sender, e);
            btnResetResistors_Click(sender, e);
            btnResetProbing_Click(sender, e);
            btnResetCaesar_Click(sender, e);
            btnResetNumberPads_Click(sender, e);
        }


        private SillySlots _sillyslots = new SillySlots();
        private void btnSillySlotsReset_Click(object sender, EventArgs e)
        {
            Slots.PopulateSubstitionTable();
            _sillyslots = new SillySlots();
            cboSillySlotsKeyWord.SelectedIndex = 0;
            cboSillySlotsSlot1.SelectedIndex = 0;
            cboSillySlotsSlot2.SelectedIndex = 0;
            cboSillySlotsSlot3.SelectedIndex = 0;
            txtSillySlotsResult.Text = "";
        }

        private void btnSillySlotsSubmit_Click(object sender, EventArgs e)
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

        private void btnSillySlotsDebugDump_Click(object sender, EventArgs e)
        {
            _sillyslots.DumpStateToClipboard();
        }

        private void txtWordScrambleIn_TextChanged(object sender, EventArgs e)
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
                    if (ww != txtWordScrambleIn.Text.ToLower())
                        txtWordScrambleOut.Text += ww + @" ";
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

        

        private void txtAlphabetIn_TextChanged(object sender, EventArgs e)
        {
            txtAlphabetOut.Text = Alphabet.GetOrder(txtAlphabetIn.Text);
        }

        private void txtAdventureGameSTR_TextChanged(object sender, EventArgs e)
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


            var ag = new AdventureGame(txtSerialNumber.Text.ToUpper(), NumberLitIndicators(),NumberUnlitIndicators(),
                (int)(nudBatteriesD.Value + nudBatteriesAA.Value),DuplicatePorts());

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

        private void btnResetAll3_Click(object sender, EventArgs e)
        {
            btnResetAdventureGame_Click(sender, e);
            btnResetAlphabet_Click(sender,e);
            btnResetWordScramble_Click(sender, e);
            btnResetSwitches_Click(sender, e);
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        private void lbModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            Text = @"Keep Talking and Nobody Explodes Helper";
            var name = lbModules.SelectedItem.ToString().Trim();
            TabPage page;
            if (!_moduleNameToTab.TryGetValue(name, out page))
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
                Text += EdgeWork.GetRequiredEdgeWork(name);
            }
            tcTabs.TabPages.Clear();
            tcTabs.TabPages.Add(page);
            Update();
        }

        

        private void cbSwitchesCurrent1_CheckedChanged(object sender, EventArgs e)
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
            var textBox = sender as MaskedTextBox;

            if (textBox != null)
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

        private void btnResetProbing_Click(object sender, EventArgs e)
        {
            txtProbing12.Text = "";
            txtProbing14.Text = "";
            txtProbing25.Text = "";
            txtProbing34.Text = "";
            txtProbing56.Text = "";
        }

        private void btnResetNumberPads_Click(object sender, EventArgs e)
        {
            txtNumberPadIn.Text = "";
        }

        private void btnResetCaesar_Click(object sender, EventArgs e)
        {
            txtCaesarCipherIn.Text = "";
        }

        private void btnResetCombinationLock_Click(object sender, EventArgs e)
        {
            txtCombinationLockIn.Text = "";
        }

        private void btnResetSemaphore_Click(object sender, EventArgs e)
        {
            txtSemaphoreIn.Text = "";
        }

        private void btnResetResistors_Click(object sender, EventArgs e)
        {
            txtResistorsIn.Text = "";
        }

        private void btnResetAdventureGame_Click(object sender, EventArgs e)
        {
            txtAdventrueGamePressure.Text = "";
            txtAdventrueGameGravity.Text = "";
            txtAdventrueGameTemp.Text = "";
            txtAdventureGameSTR.Text = "";
            txtAdventrueGameDEX.Text = "";
            txtAdventrueGameHeight.Text = "";
            txtAdventrueGameINT.Text = "";
            foreach (var c in flpAdventureGameCBO.Controls)
                ((ComboBox)c).SelectedIndex = 0;
        }

        private void btnResetAlphabet_Click(object sender, EventArgs e)
        {
            txtAlphabetIn.Text = "";
        }

        private void btnResetWordScramble_Click(object sender, EventArgs e)
        {
            txtWordScrambleIn.Text = "";
        }

        private void btnResetSwitches_Click(object sender, EventArgs e)
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

        private void cbMurderRoom_SelectedIndexChanged(object sender, EventArgs e)
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
                "Miss Scarlett","Professor Plum","Mrs Peacock",
                "Reverend Green","Colonel Mustard","Mrs White"
            };
            var weaponList = new List<string>
            {
                "Candlestick","Dagger","Lead Pipe",
                "Revolver","Rope","Spanner"
            };

            var rows = new int[][]
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

            if (nudLitTRN.Value > 0)
                suspects = 5;
            else if (murderRoom == 3)
                suspects = 7;
            else if (nudPortRCA.Value >= 2)
                suspects = 8;
            else if (nudBatteriesD.Value == 0)
                suspects = 2;
            else if (murderRoom == 8)
                suspects = 4;
            else if ((nudBatteriesD.Value + nudBatteriesAA.Value) >= 5)
                suspects = 9;
            else if (nudUnlitFRQ.Value > 0)
                suspects = 1;
            else if (murderRoom == 2)
                suspects = 3;

            if (murderRoom == 7)
                weapons = 3;
            else if ((nudBatteriesD.Value + nudBatteriesAA.Value) >= 5)
                weapons = 1;
            else if (nudPortSerial.Value > 0)
                weapons = 9;
            else if (murderRoom == 1)
                weapons = 4;
            else if (nudBatteryHolders.Value == 0)
                weapons = 6;
            else if (NumberLitIndicators() == 0)
                weapons = 5;
            else if (murderRoom == 4)
                weapons = 7;
            else if (nudPortRCA.Value >= 2)
                weapons = 2;

            suspects--;
            weapons--;

            txtMurderOut.Text = @"Possible Accusations:" + Environment.NewLine;
            for(var i = 0; i < 6; i++)
                for (var j = 0; j < 6; j++)
                {
                    if (rows[suspects][i] != rows[weapons][j]) continue;
                    txtMurderOut.Text += Environment.NewLine;
                    txtMurderOut.Text += suspectList[i] + 
                        @" with the " + weaponList[j] + 
                        @" in the " + roomList[rows[suspects][i]];
                }
        }

        private void cbMicrocontroller_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = cbMicrocontroller.SelectedIndex - 1;
            txtMicrocontrollerOut.Text = "";
            if (index < 0) return;
            var colors = new List<string> {"White","Green","Red","Yellow","Blue","Magenta"};
            if (txtMicrocontrollerLastDigit.Text == @"1" || txtMicrocontrollerLastDigit.Text == @"4")
            {
                colors = new List<string>() {"White","Yellow","Magenta","Green","Blue","Red"};
            }
            else if (nudLitSIG.Value > 0 || nudPortRJ45.Value > 0)
            {
                colors = new List<string>() {"White","Yellow","Red","Magenta","Green","Blue"};
            }
            else if ("CLRX18".ToCharArray().Any(c => txtSerialNumber.Text.Trim().Contains(c.ToString())))
            {
                colors = new List<string>() {"White","Red","Magenta","Green","Blue","Yellow"};
            }
            else if ((nudBatteriesD.Value + nudBatteriesAA.Value).ToString(CultureInfo.InvariantCulture) == txtMicrocontrollerSecondDigit.Text)
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

        private void txtGamePadX_TextChanged(object sender, EventArgs e)
        {
            txtGamePadOut.Text = "";
            if (txtGamePadX.Text.Trim().Length == 0 || txtGamePadY.Text.Trim().Length == 0) return;

            var hcn = new List<int> {1,2,4,6,12,24,36,48,60};

            var x = int.Parse(txtGamePadX.Text.Trim());
            var y = int.Parse(txtGamePadY.Text.Trim());
            string solution;
           
            var numbatteries = nudBatteriesD.Value + nudBatteriesAA.Value;

            var xA = x / 10;
            var xB = (xA * -10) + x;
            var yA = y / 10;
            var yB = (xB * -10) + x;
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
            else if (((x % 3) == 2) || nudUnlitSND.Value > 0)
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
            else if (((yA - yB) == 4) && nudPortRCA.Value > 0)
            {
                solution = solution + "▶A▼▼";
            }
            else if (((y % 4) == 2) || nudLitFRQ.Value > 0)
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
            else if (((y % 4) == 3) || nudPortPS2.Value > 0)
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

        private void btnGamepadReset_Click(object sender, EventArgs e)
        {
            txtGamePadY.Text = "";
            txtGamePadX.Text = "";
        }

        private void btnMicroControllerReset_Click(object sender, EventArgs e)
        {
            txtMicrocontrollerLastDigit.Text = "";
            txtMicrocontrollerSecondDigit.Text = "";
            cbMicrocontroller.SelectedIndex = 0;
        }

        private void btnResetMurder_Click(object sender, EventArgs e)
        {
            cbMurderRoom.SelectedIndex = 0;
        }

        private void txtCryptograpyLengths_TextChanged(object sender, EventArgs e)
        {
            txtCryptographyOut.Text = new Cryptography().GetLetterOrder(txtCryptograpyLengths.Text,
                txtCryptographyLetters.Text);
        }

        private void pb3DMaze_Paint(object sender, PaintEventArgs e)
        {
            
            var maze = _3Dmaze.GetMaze(txt3DMazeLetters.Text);
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, pb3DMaze.Size.Width, pb3DMaze.Size.Height);

            var x = -1;
            var y = -1;

            if (txtSerialNumber.Text.Trim().Length == 6)
            {
                const string digits = "0123456789";

                x = digits.IndexOf(txtSerialNumber.Text.Substring(5, 1), StringComparison.Ordinal);
                for (var i = 0; i < 6 && y < 0; i++)
                    y = digits.IndexOf(txtSerialNumber.Text.Substring(i, 1), StringComparison.Ordinal);

                if (x >= 0 && y >= 0)
                {
                    foreach (var c in gbIndicators.Controls)
                    {
                        if (c.GetType() != typeof(NumericUpDown)) continue;
                        var cc = (NumericUpDown) c;
                        var lit = cc.Name.Split(new[] {"nudLit", "nudUnlit"}, StringSplitOptions.RemoveEmptyEntries);

                        var flag = false;
                        if (cc.Name.Contains("nudUnlit"))
                        {
                            foreach (var l in lit[0].ToCharArray())
                            {
                                if ("MAZE GAMER".Contains(l)) flag = true;
                            }
                            if (flag) y += (int) cc.Value;
                        }
                        else
                        {
                            foreach (var l in lit[0].ToCharArray())
                            {
                                if ("HELP IM LOST".Contains(l)) flag = true;
                            }
                            if (flag) x += (int) cc.Value;
                        }
                    }

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
                        e.Graphics.DrawString(i.ToString(), txt3DMazeLetters.Font, Brushes.Red, new Point((i * 47) + 10, (0 * 47) + 10));
                    for (var j = 1; j < 8; j++)
                        e.Graphics.DrawString(j.ToString(), txt3DMazeLetters.Font, Brushes.Red, new Point((0 * 47) + 10, (j * 47) + 10));
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
                        if(!_3Dmaze.IsTravelPossible(maze,i,j,directions.Substring(k,1)))
                            e.Graphics.DrawWall(Color.Red, 7f, j, i, directions.Substring(k,1));

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
        private void txt3DMazeLetters_TextChanged(object sender, EventArgs e)
        {
            Refresh();
            txt3DMazeOut.Text = _3Dmaze.FindLocation(txt3DMazeLine.Text, txt3DMazeLetters.Text);
        }

        private void txt3DMazeLine_TextChanged(object sender, EventArgs e)
        {
            Refresh();
            txt3DMazeOut.Text = _3Dmaze.FindLocation(txt3DMazeLine.Text, txt3DMazeLetters.Text);
        }
    }

    public class ProbingSet
    {
        public bool[] Frequencies = new bool[4];
        public List<int> PairsWith = new List<int>();
    }
}