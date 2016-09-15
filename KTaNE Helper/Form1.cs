using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        int _manualVersion;

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

        private void Form1_Load(object sender, EventArgs e)
        {
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
                if ((string) p?.Tag != "mods")
                    _noModPages.Add(p);
                _allPages.Add(p);
            }

            if(!checkBox1.Checked)
                checkBox1_CheckedChanged(null, null);
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
                    if (((TextBox)fpPassword.Controls[i]).Text != Empty)
                        includePassword = ((TextBox) fpPassword.Controls[i]).Text.ToLower().Contains(pass.Substring(i, 1));
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
                ((TextBox) textbox).Text = Empty;
            Password_TextChanged(sender, e);
        }

        private bool _memoryState = true;
        private int _memoryStage;
        private int _memoryStageNumber;
        private int[] _memoryNumbers = new int[5];
        private int[] _memoryPositions = new int[5];
        private int[] _memoryRules = new int[20];

        void MemoryButtonStates(bool state)
        {
            _memoryState = state;

            memoryDebug.Text = @"Stage " + (_memoryStage + 1) + @" - " + (state ? "Display number" : "Defuser Input") + Environment.NewLine;
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
                    memoryLabel.Text = @"Press Button in Position " + _memoryPositions[_memoryStage];
                    memoryNumberLabel.Visible = true;
                    break;
                case MemoryRules.One:
                case MemoryRules.Two:
                case MemoryRules.Three:
                case MemoryRules.Four:
                    _memoryNumbers[_memoryStage] = (rule - MemoryRules.One) + 1;
                    memoryLabel.Text = @"Press Button labelled " + _memoryNumbers[_memoryStage];
                    memoryPositionLabel.Visible = true;
                    break;
                case MemoryRules.StageOnePos:
                case MemoryRules.StageTwoPos:
                case MemoryRules.StageThreePos:
                case MemoryRules.StageFourPos:
                    _memoryPositions[_memoryStage] = _memoryPositions[rule - MemoryRules.StageOnePos];
                    memoryLabel.Text = @"Press Button in Position " + _memoryPositions[_memoryStage];
                    memoryNumberLabel.Visible = true;
                    break;
                case MemoryRules.StageOneLabel:
                case MemoryRules.StageTwoLabel:
                case MemoryRules.StageThreeLabel:
                case MemoryRules.StageFourLabel:
                    _memoryNumbers[_memoryStage] = _memoryNumbers[rule - MemoryRules.StageOneLabel];
                    memoryLabel.Text = @"Press Button labelled " + _memoryNumbers[_memoryStage];
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

            MemoryButtonStates(true);
        }

        private void MemoryReset_Click(object sender, EventArgs e)
        {
            memoryLabel.Text = "";
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
            nn_label.Text = directions[result];
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
                        e.Graphics.DrawLine(new Pen(Color.White, 3f), new Point((i * 47), (j * 47)), new Point((i * 47) + 47, (j * 47)));
                        e.Graphics.DrawLine(new Pen(Color.White, 3f), new Point((i * 47), (j * 47) + 47), new Point((i * 47) + 47, (j * 47) + 47));
                        e.Graphics.DrawLine(new Pen(Color.White, 3f), new Point((i * 47), (j * 47)), new Point((i * 47), (j * 47) + 47));
                        e.Graphics.DrawLine(new Pen(Color.White, 3f), new Point((i * 47) + 47, (j * 47)), new Point((i * 47) + 47, (j * 47) + 47));
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
                        e.Graphics.DrawLine(new Pen(Color.White,3f),new Point((i*47),(j*47)),new Point((i*47)+47,(j*47))  );
                    if (!mazes[_mazeSelection, j, i].Contains("d"))
                        e.Graphics.DrawLine(new Pen(Color.White, 3f), new Point((i * 47), (j * 47)+47), new Point((i * 47) + 47, (j * 47)+47));
                    if (!mazes[_mazeSelection, j, i].Contains("l"))
                        e.Graphics.DrawLine(new Pen(Color.White, 3f), new Point((i * 47), (j * 47)), new Point((i * 47), (j * 47) + 47));
                    if (!mazes[_mazeSelection, j, i].Contains("r"))
                        e.Graphics.DrawLine(new Pen(Color.White, 3f), new Point((i * 47) + 47, (j * 47)), new Point((i * 47) + 47, (j * 47) + 47));

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
            var max = checkBox1.Checked ? 8 : 4;
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
                if (!checkBox1.Checked) return;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            tbFMNSolution.Text = "";
            if (txtSerialNumber.TextLength < 6)
            {
                tbFMNSolution.Text = @"The Full serial number is needed to calculate the solution";
                return;
            }
            var smallestOdd = 9;
            var largestDigit = 0;
            var numDigits = 0;
            var lastDigit = 0;
            for (var i = 0; i < txtSerialNumber.TextLength; i++)
            {
                var num = GetDigitFromCharacter(txtSerialNumber.Text.Substring(i, 1));
                if (num == -1) continue;
                numDigits++;
                if((num%2)==1)
                    if (num < smallestOdd)
                        smallestOdd = num;
                if (num > largestDigit)
                    largestDigit = num;
                lastDigit = num;
            }
            foreach (var line in textBox1.Text.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
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
                    solutionStr += solution[i]%10 + " ";
                }
                tbFMNSolution.Text += solutionStr.Trim() + Environment.NewLine;
            }
        }

        readonly List<string> _twoBitLookup = new List<string>
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var lookup = GetDigitFromCharacter(textBox2.Text);
            textBox4.Text = lookup > -1 ? _twoBitLookup[lookup] : "";
        }

        private void CalculateInitialTwoBitsCode()
        {
            var batts = (int)(nudBatteriesD.Value + nudBatteriesAA.Value);
            if (txtSerialNumber.TextLength < 6)
            {
                textBox5.Text = "";
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
            for (var i = 0; i < txtSerialNumber.TextLength; i++)
            {
                if (dict[txtSerialNumber.Text.Substring(i, 1).ToUpper()] <= 0) continue;
                initial = dict[txtSerialNumber.Text.Substring(i, 1).ToUpper()];
                break;
            }
            initial += ( batts * GetDigitFromCharacter(txtSerialNumber.Text.Substring(txtSerialNumber.TextLength - 1, 1)));
            if (nudPortRCA.Value > 0 && nudPortRJ45.Value == 0)
                initial *= 2;
            textBox5.Text = _twoBitLookup[initial%100];

        }

        private bool SerialNumberContainsVowel()
        {
            var vowels = new List<string> { "A", "E", "I", "O", "U" };
            for (var i = 0; i < 5; i++)
                if (txtSerialNumber.Text.Contains(vowels[i]))
                    return true;
            return false;
        }

        private bool SerialNumberBeginsWithLetter()
        {
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return txtSerialNumber.TextLength != 0 
                && letters.Contains(txtSerialNumber.Text.Substring(0, 1));
        }

        private bool SerialNumberLastDigitOdd()
        {
            var odd = "13579";
            return txtSerialNumber.TextLength != 0 
                && odd.Contains(txtSerialNumber.Text.Substring(txtSerialNumber.TextLength - 1, 1));
        }

        // ReSharper disable once UnusedMember.Local
        private int CountSerialNumberLetters()
        {
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var count = 0;
            for(var i=0;i<txtSerialNumber.TextLength;i++)
                if (letters.Contains(txtSerialNumber.Text.Substring(i, 1)))
                    count++;
            return count;
        }

        private int CountSerialNumberDigits()
        {
            var digits = "0123456789";
            var count = 0;
            for (var i = 0; i < txtSerialNumber.TextLength; i++)
                if (digits.Contains(txtSerialNumber.Text.Substring(i, 1)))
                    count++;
            return count;
        }

        private void txtConnections_TextChanged(object sender, EventArgs e)
        {
            var batts = (int) (nudBatteriesD.Value + nudBatteriesAA.Value);
            txtConnectionCheckOut.Text = "";
            var serials = new List<string> {"7HPJ", "34XYZ", "SLIM", "15BRO", "20DGT", "8CAKE", "9QVN", "6WUF"};
            var connectionPairs = new List<string>
            {
                "12-13-21-23-31-32-46-47-56-57-64-65-74-45",
                "12-14-46-21-23-24-32-41-42-47-56-65-61-67-68-74-76-78-87-86",
                "12-16-13-21-26-31-36-34-62-61-63-64-65-43-46-45-47-48-56-54-57-84-87-75-74-78",
                "12-17-21-27-71-72-75-76-57-56-67-65-34-38-43-48-83-84",
                "13-12-31-35-37-53-57-56-73-75-72-74-21-27-24-47-42-46-48-65-64-68-84-86",
                "12-16-13-18-21-26-24-61-62-64-63-31-36-34-37-38-42-46-43-47-45-73-74-75-78-81-83-87-85-54-57-58",
                "12-14-18-21-27-26-23-32-37-36-34-43-41-45-54-58-56-65-63-62-67-76-73-72-78-87-85-81",
                "12-17-16-21-27-28-23-61-63-65-67-76-75-74-78-72-71-84-83-82-87-53-54-57-56-48-47-45-32-38-35-36"
            };
            if (txtConnections.Text.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries).Length != 4) return;
            if (txtSerialNumber.TextLength < 6) return;
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

            var serialchar = txtSerialNumber.Text.Substring(serialdigit - 1, 1);
            var group = -1;
            for(var i=0;i<8 && group == -1;i++)
                if (serials[i].Contains(serialchar))
                    group = i;

            if (group == -1) return;

            foreach (var entry in txtConnections.Text.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries))
            {
                txtConnectionCheckOut.Text += connectionPairs[group].Contains(entry)
                    ? "Green "
                    : "Red ";
            }

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
            if (txtSerialNumber.TextLength < 6) return;

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

                x += safeOffsets[txtSerialNumber.Text.Substring(i, 1).ToUpper()][i];

                if (i == 5)
                    for (var j = 0; j < 5; j++)
                        x += safeOffsets[txtSerialNumber.Text.Substring(j, 1).ToUpper()][5];

                x %= 12;
                txtSafetySafe.Text += x + @" ";
            }

        }

        private void UpdateBombSolution(object sender, EventArgs e)
        {
            cbPlumbingRedIn_CheckedChanged();
            txtChessInput_TextChanged(null, null);
            Simon_Says_Event();
            Complicated_Wires_Event(null, null);
            Button_Event(null, null);
            wires_Input_TextChanged(null, null);
            Needy_Knob_CheckedChanged(null, null);
            textBox2_TextChanged(null, null);
            textBox1_TextChanged(null, null);
            txtConnections_TextChanged(null, null);
            Password_TextChanged(null, null);
            MorseCodeInput_TextChanged(null, null);
            txtLogicAND_TextChanged(null, null);
            txtLogicOR_TextChanged(null, null);
            CalculateInitialTwoBitsCode();
            CalculateSafetySafe();
            txtNumberPadIn_TextChanged(null, null);
        }

        private bool DuplicateSerialCharacters()
        {
            for(var i = 0; i < txtSerialNumber.TextLength; i++)
                for(var j = i + 1; j < txtSerialNumber.TextLength; j++)
                    if (txtSerialNumber.Text.Substring(i, 1) == txtSerialNumber.Text.Substring(j, 1))
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
            if (txtSerialNumber.Text.Contains("1") || txtSerialNumber.Text.Contains("L"))
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

        private void button42_Click(object sender, EventArgs e)
        {
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
            
            if (txtLogicAND.TextLength < 3)
            {
                txtLogicAND.ResetBackColor();
                return;
            }
            var truth = BuildTruthTable();
            txtLogicAND.BackColor = truth[txtLogicAND.Text.Substring(0, 1).ToUpper()]
                                    && truth[txtLogicAND.Text.Substring(1, 1).ToUpper()]
                                    && truth[txtLogicAND.Text.Substring(2, 1).ToUpper()]
                ? Color.Green
                : Color.Red;
        }

        private void txtLogicOR_TextChanged(object sender, EventArgs e)
        {
            if (txtLogicOR.TextLength < 3)
            {
                txtLogicOR.ResetBackColor();
                return;
            }
            var truth = BuildTruthTable();
            txtLogicOR.BackColor = truth[txtLogicOR.Text.Substring(0, 1).ToUpper()]
                                    || truth[txtLogicOR.Text.Substring(1, 1).ToUpper()]
                                    || truth[txtLogicOR.Text.Substring(2, 1).ToUpper()]
                ? Color.Green
                : Color.Red;
        }

        private void txtChessInput_TextChanged(object sender, EventArgs e)
        {
            var positionsLetters = "ABCDEF";
            var positionNumbers = "123456";
            var whitefields = "B1,D1,F1,A2,C2,E,B3,D3,F3,A4,C4,E4,B5,D5,F5,A6,C6,E6";
            var chessboard = new int[6, 6];
            var knightMovesX = new [] {1, 2, 2, 1, -1, -2, -2, -1};
            var knightMovesY = new [] {2, 1, -1, -2, -2, -1, 1, 2};
            var kingMovesX = new[] {1, 1, 1, 0, -1, -1, -1, 0};
            var kingMovesY = new[] {1, 0, -1, -1, -1, 0, 1, 1};
            txtChessSolution.Text = "";

            if (txtChessInput.TextLength < 17 || txtChessInput.Text.Split(' ').Length < 6)
                return;
            var inputs = txtChessInput.Text.ToUpper().Split(' ');
            if (inputs.Any(x => x.Length < 2
                || !positionsLetters.Contains(x.Substring(0, 1))
                || !positionNumbers.Contains(x.Substring(1, 1))))
                return;


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
                            l.Visible = checkBox1.Checked;
                    }
                    else if (c.GetType() == typeof(NumericUpDown))
                    {
                        var n = (NumericUpDown) c;
                        if ((string)n.Tag == "mods")
                            n.Visible = checkBox1.Checked;
                    }
                }
            }
            var list = checkBox1.Checked ? _allPages : _noModPages;
            tcTabs.TabPages.Clear();
            foreach (var p in list)
                tcTabs.TabPages.Add(p);

            //Keypad specific mod
            for (var i = 4; i < 8; i++)
                ((Button) fpKeypadSelection.Controls[i]).Visible = checkBox1.Checked;
            while (_keypadSelection[4] != "")
                keypadSelection_Click(fpKeypadSelection.Controls[4], e);

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
            else if (txtSerialNumber.Text.Contains("C")
                     || txtSerialNumber.Text.Contains("E")
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
            foreach (var num in txtEmojiMathIn.Text.Split(' '))
            {
                var x = numbers.IndexOf(num, StringComparison.Ordinal);
                if (x == -1)
                    sign = (num == "+") ? 1 : -1;
                else
                    switch (sign)
                    {
                        case 0:
                            num1 *= 10;
                            num1 += (x/3);
                            break;
                        default:
                            num2 *= 10;
                            num2 += (x/3);
                            break;
                    }
            }
            if (sign == 0)
            {
                txtEmojiMathOut.Text = "";
                return;
            }
            txtEmojiMathOut.Text = (sign > 0 ? num1 + num2 : num1 - num2).ToString();
        }

        private void txtStroopColors_TextChanged(object sender, EventArgs e)
        {
            const string colorLookup = "RYGBMW";
            txtStroopAnswer.Text = "";
            var answer = "";
            if (txtStroopColors.TextLength < 8 || txtStroopWords.TextLength < 8) return;
            var colors = new int[8];
            var words = new int[8];
            var colorMatchesWord = new bool[8];
            var totalMatches = 0;
            for (var i = 0; i < 8; i++)
            {
                colors[i] = colorLookup.IndexOf(txtStroopColors.Text.ToUpper().Substring(i, 1), StringComparison.Ordinal);
                words[i] = colorLookup.IndexOf(txtStroopWords.Text.ToUpper().Substring(i, 1), StringComparison.Ordinal);
                if (colors[i] == Stroop.None || words[i] == Stroop.None) return;
                colorMatchesWord[i] = colors[i] == words[i];
                if (colorMatchesWord[i]) totalMatches++;
            }

            var colorsCount = new int[6];
            var wordsCount = new int[6];
            var colorAsWords = new int[6];

            for (var i = 0; i < 8; i++)
            {
                colorsCount[colors[i]]++;
                wordsCount[words[i]]++;
                if (colors[i] == words[i]) colorAsWords[colors[i]]++;
            }

            var condition = false;
            var lastWord = words[0];
            var lastColor = colors[0];
            switch (colors[7])
            {
                case Stroop.Red:
                    if (wordsCount[Stroop.Green] >= 3)
                    {
                        var j = 0;
                        for (var i = 0; i < 8; i++)
                        {
                            if (colors[i] == Stroop.Green || words[i] == Stroop.Green)
                                j++;
                            if (j < 3) continue;
                            answer = "Press Yes on " + (i + 1) + " (R1)";
                            break;
                        }
                    }
                    else if (colorsCount[Stroop.Blue] == 1)
                    {
                        for (var i = 0; i < 8; i++)
                        {
                            if (words[i] != Stroop.Magenta) continue;
                            answer = "Press No on " + (i + 1) + " (R2)";
                            break;
                        }
                    }
                    else
                    {
                        for (var i = 7; i >= 0; i--)
                        {
                            if (colors[i] != Stroop.White && words[i] != Stroop.White) continue;
                            answer = "Press Yes on " + (i + 1) + " (R3)";
                            break;
                        }
                    }
                    break;
                case Stroop.Yellow:
                    for (var i = 0; i < 8 && !condition; i++)
                    {
                        condition = words[i] == Stroop.Blue && colors[i] == Stroop.Green;
                        if (!condition) continue;
                        for (var j = 0; j < 8; j++)
                        {
                            if (words[j] != Stroop.Green) continue;
                            answer = "Press Yes on " + (j + 1) + " (Y1)";
                            break;
                        }
                    }
                    for (var i = 0; i < 8 && !condition; i++)
                    {
                        condition = words[i] == Stroop.White &&
                                    (colors[i] == Stroop.White || colors[i] == Stroop.Red);
                        if (!condition) continue;
                        var k = 0;
                        for (var j = 0; j < 8; j++)
                        {
                            if (words[j] != colors[j]) k++;
                            if (k < 2) continue;
                            answer = "Press Yes on " + (j + 1) + " (Y2)";
                            break;
                        }
                    }
                    if (!condition)
                        answer = "Press No on " +
                                 (colorsCount[Stroop.Magenta] + wordsCount[Stroop.Magenta] - colorAsWords[Stroop.Magenta])
                                 + " (Y3)";
                    break;
                case Stroop.Green:
                    for (var i = 1; i < 8 && !condition; i++)
                    {
                        if (words[i] == lastWord && colors[i] != lastColor)
                        {
                            answer = "Press No on 5 (G1)";
                            condition = true;
                        }
                        else
                        {
                            lastWord = words[i];
                            lastColor = colors[i];
                        }
                    }
                    if (!condition && wordsCount[Stroop.Magenta] >= 3)
                    {
                        var j = 0;
                        for (var i = 0; i < 8; i++)
                        {
                            if (colors[i] == Stroop.Yellow || words[i] == Stroop.Yellow)
                                j++;
                            if (j < 1) continue;
                            answer = "Press No on " + (i + 1) + " (G2)";
                            break;
                        }
                    }
                    else if (!condition)
                    {
                        for (var i = 0; i < 8; i++)
                        {
                            if (colors[i] != words[i]) continue;
                            answer = "Press Yes on " + (i + 1) + " (G3)";
                            break;
                        }
                    }
                    break;
                case Stroop.Blue:
                    if (totalMatches <= 5)
                    {
                        for (var i = 0; i < 6 && !condition; i++)
                        {
                            if (colorMatchesWord[i]) continue;
                            answer = "Press Yes on " + (i + 1) + " (B1)";
                            condition = true;
                        }
                    }
                    if (!condition)
                    {
                        for (var i = 0; i < 8 && !condition; i++)
                        {
                            condition = (words[i] == Stroop.Red && colors[i] == Stroop.Yellow)
                                        || (words[i] == Stroop.Yellow && colors[i] == Stroop.White);
                            if (!condition) continue;
                            for (var j = 0; j < 8; j++)
                            {
                                if (words[j] != Stroop.White || colors[j] != Stroop.Red) continue;
                                answer = "Press No on " + (j + 1) + " (B2)";
                                break;
                            }
                        }
                    }
                    if (!condition)
                    {
                        for (var i = 0; i < 8; i++)
                        {
                            if (colors[i] == Stroop.Green || words[i] == Stroop.Green)
                                answer = "Press Yes on " + (i + 1) + " (B3)";
                        }
                        
                    }
                    break;
                case Stroop.Magenta:
                    for (var i = 1; i < 8 && !condition; i++)
                    {
                        if (words[i] != lastWord && colors[i] == lastColor)
                        {
                            answer = "Press Yes on 3 (M1)";
                            condition = true;
                        }
                        else
                        {
                            lastWord = words[i];
                            lastColor = colors[i];
                        }
                    }
                    if (wordsCount[Stroop.Yellow] > colorsCount[Stroop.Blue] && !condition)
                    {
                        condition = true;
                        for (var i = 0; i < 8; i++)
                            if (words[i] == Stroop.Yellow)
                                answer = "Press No on " + (i + 1) + " (M2)";

                    }
                    if (!condition)
                    {
                        for (var i = 0; i < 8; i++)
                        {
                            if (colors[i] != words[6]) continue;
                            answer = "Press No on " + (i + 1) + " (M3)";
                            break;
                        }
                    }

                    break;
                case Stroop.White:
                    if (colors[2] == words[3] || colors[2] == words[4])
                    {
                        for (var i = 0; i < 8; i++)
                        {
                            if (colors[i] != Stroop.Blue && words[i] != Stroop.Blue) continue;
                            answer = "Press No on " + (i + 1) + " (W1)";
                            break;
                        }
                        condition = true;
                    }
                    if (!condition)
                    {
                        for (var i = 0; i < 8 && !condition; i++)
                        {
                            condition = words[i] == Stroop.Yellow && colors[i] == Stroop.Red;
                            if (!condition) continue;
                            for (var j = 0; j < 8; j++)
                            {
                                if (colors[j] == Stroop.Blue)
                                    answer = "Press Yes on " + (j + 1) + " (W2)";
                            }
                            
                        }
                    }
                    if (!condition)
                        answer = "Press No (W3)";
                    break;
                default:
                    return;
            }
            txtStroopAnswer.Text = answer;
        }

        

        private void txtNumberPadIn_TextChanged(object sender, EventArgs e)
        {
            var pad = new NumberPad(txtNumberPadIn.Text,(int)nudBatteriesD.Value + (int)nudBatteriesAA.Value,
                CountTotalPorts(),!SerialNumberLastDigitOdd(),SerialNumberContainsVowel());
            txtNumberPadOut.Text = "";
            if (txtNumberPadIn.TextLength < 10 || pad.GetColorCount(pad.ColorAll) < 10) return;
            try
            {

                txtNumberPadOut.Text = pad.GetCorrectCode();
            }
            catch
            {
                txtNumberPadOut.Text = @"Module solved itself";
            }
        }

    }
}