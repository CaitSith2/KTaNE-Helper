using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KTaNE_Helper.Properties;
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


        int _wiresequenceRed;
        int _wiresequenceBlue;
        int _wiresequenceBlack;
        int _wiresequencePlace = 1;

        private readonly int[] _complicatedWires241 = { 0, 1, 1, 1, 2, 3, 4, 1, 0, 0, 2, 4, 3, 3, 4, 2 };
        private readonly int[] _complicatedWires724 = { 0, 1, 2, 1, 3, 3, 1, 0, 0, 3, 0, 4, 4, 2, 4, 0 };

        private readonly string[] _keypadOrder241 = { "ϘѦƛϞѬϗϿ", "ӬϘϿҨ☆ϗ¿", "©ѼҨҖԆƛ☆", "Ϭ¶ѣѬҖ¿ټ", "ΨټѣϾ¶ѯ★", "ϬӬ҂ӕΨҊΩ" };
        private readonly string[] _keypadOrder724 = { "ϬҨҖ☆¶Ͽζ", "ҨҊƛѦϫ¶Җ", "ѬϬϗζΨƛѼ", "Ѭټ҈©ϞϿϗ", "Ϙ©¿Ѫ☆★ϫ", "ӕԆӬѪѣѼΨ" };

        private readonly string _keypadSymbols241 = "¿©¶☆★҂ƛϾϿΨΩϞϘϗϬҖҊҨӬѣѦѬѯѼӕԆټ";
        private readonly string _keypadSymbols724 = "¿©¶☆★҈ƛζϿΨϞϘϗϫϬҖҊҨӬѣѦѪѬѼӕԆټ";

        int _manualVersion;


        public Form1()
        {
            InitializeComponent();
        }

        private void wsReset_Click(object sender, EventArgs e)
        {
            _wiresequenceBlack = _wiresequenceBlue = _wiresequenceRed = 0;
            label1.Text = "";
            wsRedButton.Text = @"Red: " + _wiresequenceRed;
            wsBlueButton.Text = @"Blue: " + _wiresequenceBlue;
            wsBlackButton.Text = @"Black: " + _wiresequenceBlack;
            ws_input.Text = "";
        }

        private void wsRedButton_Click(object sender, EventArgs e)
        {
            label1.Text = Resources.cw_dont_cut;
            var sequence = (_manualVersion == 0 ? _wireSequenceRed241 : _wireSequenceRed724);
            if (_wiresequenceRed == 9) return;
            if ((sequence[_wiresequenceRed++] & _wiresequencePlace) == _wiresequencePlace) label1.Text = Resources.cw_cut;
            wsRedButton.Text = @"Red: " + _wiresequenceRed;
        }

        private void wsBlueButton_Click(object sender, EventArgs e)
        {
            label1.Text = Resources.cw_dont_cut;
            var sequence = (_manualVersion == 0 ? _wireSequenceBlue241 : _wireSequenceBlue724);
            if (_wiresequenceBlue == 9) return;
            if ((sequence[_wiresequenceBlue++] & _wiresequencePlace) == _wiresequencePlace) label1.Text = Resources.cw_cut;
            wsBlueButton.Text = @"Blue: " + _wiresequenceBlue;
        }

        private void wsBlackButton_Click(object sender, EventArgs e)
        {
            label1.Text = Resources.cw_dont_cut;
            var sequence = (_manualVersion == 0 ? _wireSequenceBlack241 : _wireSequenceBlack724);
            if (_wiresequenceBlack == 9) return;
            if ((sequence[_wiresequenceBlack++] & _wiresequencePlace) == _wiresequencePlace) label1.Text = Resources.cw_cut;
            wsBlackButton.Text = @"Black: " + _wiresequenceBlack;
        }

        private void Complicated_Wires_Event(object sender, EventArgs e)
        {
            cw_input.Text = cw_input.Text.ToUpper();
            cw_input.SelectionStart = cw_input.Text.Length;
            cw_serial.Visible = cw_batt.Visible = cw_pp.Visible = false;
            facts_serial_last_digit.SelectedIndex = (cw_serial.Checked ? 1 : 0);
            facts_has_pp.Checked = cw_pp.Checked;
            if (cw_batt.Checked)
            {
                if (facts_battery.SelectedIndex < 1)
                    facts_battery.SelectedIndex = 1;
            }
            else
            {
                facts_battery.SelectedIndex = 0;
            }

                
            var i = 0;
            if (cw_red.Checked) i += 1;
            if (cw_blue.Checked) i += 2;
            if (cw_led.Checked) i += 4;
            if (cw_star.Checked) i += 8;
            var cwcode = (_manualVersion == 0 ? _complicatedWires241 : _complicatedWires724);

            cw_label.Text = Resources.cw_dont_cut;
            switch (cwcode[i])
            {
                case 0:
                    cw_label.Text = Resources.cw_cut;
                    break;
                case 1:
                    if (cw_serial.Checked) cw_label.Text = Resources.cw_cut;
                    cw_serial.Visible = true;
                    break;
                case 2:
                    cw_label.Text = Resources.cw_dont_cut;
                    break;
                case 3:
                    if (cw_batt.Checked) cw_label.Text = Resources.cw_cut;
                    cw_batt.Visible = true;
                    break;
                case 4:
                    if (cw_pp.Checked) cw_label.Text = Resources.cw_cut;
                    cw_pp.Visible = true;
                    break;
            }
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
                    i = 0;
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
                            outputStr += (cw_serial.Checked ? "Cut" : "Leave");
                            cw_serial.Visible = true;
                            typestr += @"S";
                            //Cut if last digit of serial is Even
                            break;
                        case 2:
                            outputStr += @"Leave";
                            typestr += @"D";
                            //Don't Cut, Period.
                            break;
                        case 3:
                            outputStr += (cw_batt.Checked ? "Cut" : "Leave");
                            cw_batt.Visible = true;
                            typestr += @"B";
                            //Cut if 2 or more Batteries
                            break;
                        case 4:
                            outputStr += (cw_pp.Checked ? "Cut" : "Leave");
                            cw_pp.Visible = true;
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

        private void Simon_Says_Event(object sender, EventArgs e)
        {
            facts_strike.SelectedIndex = SimonSaysStrikes.SelectedIndex;
            facts_serial_has_vowel.Checked = ss_vowels.Checked;

            var red    = (_manualVersion == 0 ? new[] { 0,3,2,0,1,3 } : new[] { 3,2,3,1,0,3 });
            var blue   = (_manualVersion == 0 ? new[] { 1,2,1,3,0,2 } : new[] { 1,1,3,3,0,1 });
            var green  = (_manualVersion == 0 ? new[] { 3,0,3,2,3,0 } : new[] { 3,2,3,1,0,3 });
            var yellow = (_manualVersion == 0 ? new[] { 2,1,0,1,2,1 } : new[] { 0,2,2,1,2,3 });

            var i = (ss_vowels.Checked ? 0 : 3) + SimonSaysStrikes.SelectedIndex;

            Simon_Says_Set_Label(ss_red, red[i]);
            Simon_Says_Set_Label(ss_green, green[i]);
            Simon_Says_Set_Label(ss_blue, blue[i]);
            Simon_Says_Set_Label(ss_yellow, yellow[i]);
        }

        private void Button_Event(object sender, EventArgs e)
        {
            var color = button_color.SelectedIndex;
            var name = button_name.SelectedIndex;
            var battery = button_battery.SelectedIndex;

            facts_battery.SelectedIndex = battery;
            facts_CAR.Checked = button_car.Checked;
            facts_FRK.Checked = button_frk.Checked;


            if (_manualVersion == 0)
            {
                button_color.Visible = name != 1;
                button_battery.Visible = (!((color == 0) && (name == 0)) && !((color == 3) && (name == 3)));
                button_frk.Visible = (name != 1 && (battery == 2) && button_battery.Visible);
                button_battery.Visible &= !((color == 1) && button_car.Checked);
                button_frk.Visible &= !((color == 1) && button_car.Checked);
                button_car.Visible = ((color == 1) && name != 1);

                if ((color == 0) && (name == 0)) button_label.Text = @"Hold the Button";
                else if ((name == 1) && (battery > 0)) button_label.Text = @"Press and Release";
                else if ((color == 1) && button_car.Checked) button_label.Text = @"Hold the Button";
                else if ((battery == 2) && button_frk.Checked) button_label.Text = @"Press and Release";
                else if (color == 2) button_label.Text = @"Hold the Button";
                else if ((color == 3) && (name == 3)) button_label.Text = @"Press and Release";
                else button_label.Text = @"Hold the Button";
            }
            else
            {
                button_car.Visible = (color == 1);
                button_battery.Visible = (color == 3);
                if ((color == 1) && button_car.Checked) button_label.Text = @"Press and Release";
                else if ((color == 3) && (battery == 0)) button_label.Text = @"Press and Release";
                else button_label.Text = @"Hold the Button";
            }
        }

        private void ws_A_CheckedChanged(object sender, EventArgs e)
        {
            _wiresequencePlace = 1;
        }

        private void ws_B_CheckedChanged(object sender, EventArgs e)
        {
            _wiresequencePlace = 2;
        }

        private void ws_C_CheckedChanged(object sender, EventArgs e)
        {
            _wiresequencePlace = 4;
        }

        private void ManualVersionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            _manualVersion = ManualVersionSelect.SelectedIndex;
            if (_manualVersion == 0)
            {
                facts_serial_starts_with_letter.Visible = false;
                linkLabel1.Text = @"http://www.bombmanual.com";
                button_frk.Visible = true;
                button_name.Visible = true;
                button_car.Text = facts_CAR.Text = @"CAR";
                bluestrip.Text = @"Blue - 4";
                yellowstrip.Text = @"Yellow - 5";
                whitestrip.Text = @"Other - 1";
                otherstrip.Text = @"";
            }
            else
            {
                facts_serial_starts_with_letter.Visible = true;
                linkLabel1.Text = @"http://www.lthummus.com/";
                button_frk.Visible = false;
                button_name.Visible = false;
                button_car.Text = facts_CAR.Text = @"BOB";
                bluestrip.Text = @" Red - 5";
                yellowstrip.Text = @"Yellow - 3";
                whitestrip.Text = @"White - 3";
                otherstrip.Text = @"Other - 4";
            }

            keypadReset_Click(sender, e);
            Needy_Knob_CheckedChanged(sender, e);
            Simon_Says_Event(sender, e);
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



        }

        private void wireReset_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < 6; i++)
                ((ComboBox) fpWires.Controls[i]).SelectedIndex = 0;
            wires_input.Text = "";
        }

        private void simpleWires_Event(object sender, EventArgs e)
        {
            facts_serial_last_digit.SelectedIndex = (wires_serial_odd.Checked ? 0 : 1);
            facts_serial_starts_with_letter.Checked = wires_serial_letter.Checked;
            wires_serial_letter.Visible = wires_serial_odd.Visible = false;
            wires_label.Text = "";
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
                        wires_label.Text = @"Cut Second Wire";
                    else if (wires[2] == Wires.White)
                        wires_label.Text = @"Cut Last Wire";
                    else if (wirecounts[Wires.Blue] > 1)
                        wires_label.Text = @"Cut Last Blue Wire";
                    else
                        wires_label.Text = @"Cut Last Wire";
                }
                else if (count == 4)
                {
                    wires_serial_odd.Visible = (wirecounts[Wires.Red] > 1);
                    if ((wirecounts[Wires.Red] > 1) && wires_serial_odd.Checked)
                        wires_label.Text = @"Cut Last Red Wire";
                    else if ((wires[3] == Wires.Yellow) && (wirecounts[Wires.Red] == 0))
                        wires_label.Text = @"Cut First Wire";
                    else if (wirecounts[Wires.Blue] == 1)
                        wires_label.Text = @"Cut First Wire";
                    else if (wirecounts[Wires.Yellow] > 1)
                        wires_label.Text = @"Cut Last Wire";
                    else
                        wires_label.Text = @"Cut Second Wire";
                }
                else if (count == 5)
                {
                    wires_serial_odd.Visible = (wires[4] == Wires.Black);
                    if ((wires[4] == Wires.Black) && wires_serial_odd.Checked)
                        wires_label.Text = @"Cut Fourth Wire";
                    else if ((wirecounts[Wires.Red] == 1) && (wirecounts[Wires.Yellow] > 1))
                        wires_label.Text = @"Cut First Wire";
                    else if (wirecounts[Wires.Black] == 0)
                        wires_label.Text = @"Cut Second Wire";
                    else
                        wires_label.Text = @"Cut First Wire";
                }
                else
                {
                    wires_serial_odd.Visible = (wirecounts[Wires.Yellow] == 0);
                    if ((wirecounts[Wires.Yellow] == 0) && wires_serial_odd.Checked)
                        wires_label.Text = @"Cut Third Wire";
                    else if ((wirecounts[Wires.Yellow] == 1) && (wirecounts[Wires.White] > 1))
                        wires_label.Text = @"Cut Fourth Wire";
                    else if (wirecounts[Wires.Red] == 0)
                        wires_label.Text = @"Cut Last Wire";
                    else
                        wires_label.Text = @"Cut Fourth Wire";
                }
            }
            else
            {
                if (count == 3)
                {
                    wires_serial_letter.Visible = (wirecounts[Wires.White] == 0);
                    if (wirecounts[Wires.White] == 0 && wires_serial_letter.Checked)
                        wires_label.Text = @"Cut the Second Wire";
                    else if (wirecounts[Wires.Red] == 1)
                        wires_label.Text = @"Cut the First Wire";
                    else if (wirecounts[Wires.Blue] > 1)
                        wires_label.Text = @"Cut the First Blue Wire";
                    else if (wires[2] == Wires.Red)
                        wires_label.Text = @"Cut the Last Wire";
                    else
                        wires_label.Text = @"Cut the Second Wire";
                }
                else if (count == 4)
                {
                    if (wirecounts[Wires.Yellow] == 1 && wires[3] == Wires.Red)
                        wires_label.Text = @"Cut the Third Wire";
                    else if (wires[3] == Wires.White)
                        wires_label.Text = @"Cut the Second Wire";
                    else if (wirecounts[Wires.Yellow] == 0)
                        wires_label.Text = @"Cut the First Wire";
                    else
                        wires_label.Text = @"Cut the Last Wire";
                }
                else if (count == 5)
                {
                    wires_serial_letter.Visible = (wirecounts[Wires.Black] > 1);
                    if (wirecounts[Wires.Black] > 1 && wires_serial_letter.Checked)
                        wires_label.Text = @"Cut the Second Wire";
                    else if (wires[4] == Wires.Blue && wirecounts[Wires.Red] == 1)
                        wires_label.Text = @"Cut the First Wire";
                    else if (wires[4] == Wires.Red)
                        wires_label.Text = @"Cut the Fourth Wire";
                    else if (wirecounts[Wires.Red] == 0)
                        wires_label.Text = @"Cut the Third Wire";
                    else
                        wires_label.Text = @"Cut the First Wire";
                }
                else
                {
                    if (wirecounts[Wires.Red] == 1)
                        wires_label.Text = @"Cut the Red Wire";
                    else if (wires[5] == Wires.Red)
                        wires_label.Text = @"Cut the Last Wire";
                    else if (wirecounts[Wires.Yellow] == 0)
                        wires_label.Text = @"Cut the Fourth Wire";
                    else
                        wires_label.Text = @"Cut the Second Wire";
                }
            }

        }

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

        private void facts_serial_has_vowel_CheckedChanged(object sender, EventArgs e)
        {
            ss_vowels.Checked = facts_serial_has_vowel.Checked;
        }

        private void facts_strike_SelectedIndexChanged(object sender, EventArgs e)
        {
            SimonSaysStrikes.SelectedIndex = facts_strike.SelectedIndex;
        }

        private bool _factsBatteryEvent;
        private void facts_battery_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_factsBatteryEvent) return;
            _factsBatteryEvent = true;
            button_battery.SelectedIndex = facts_battery.SelectedIndex;
            cw_batt.Checked = (facts_battery.SelectedIndex > 0);
            _factsBatteryEvent = false;
        }

        private bool _factsSerialLastDigitEvent;
        private void facts_serial_last_digit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_factsSerialLastDigitEvent) return;
            _factsSerialLastDigitEvent = true;
            wires_serial_odd.Checked = (facts_serial_last_digit.SelectedIndex == 0);
            cw_serial.Checked = (facts_serial_last_digit.SelectedIndex == 1);
            _factsSerialLastDigitEvent = false;
        }

        private void facts_serial_starts_with_letter_CheckedChanged(object sender, EventArgs e)
        {
            wires_serial_letter.Checked = facts_serial_starts_with_letter.Checked;
        }

        private void facts_has_pp_CheckedChanged(object sender, EventArgs e)
        {
            cw_pp.Checked = facts_has_pp.Checked;
        }

        private void facts_CAR_CheckedChanged(object sender, EventArgs e)
        {
            button_car.Checked = facts_CAR.Checked;
        }

        private void facts_FRK_CheckedChanged(object sender, EventArgs e)
        {
            button_frk.Checked = facts_FRK.Checked;
        }

        readonly string[] _keypadSelection = {"","","",""};

        private void keypadReset_Click(object sender, EventArgs e)
        {
            var keypadsymbols = (_manualVersion == 0 ? _keypadSymbols241 : _keypadSymbols724);
            

            for (var i = 0; i < 27; i++)
            {
                ((Button) fpKeypadSymbols.Controls[i]).Text = keypadsymbols.Substring(i, 1);
            }

            for (var i = 0; i < 4; i++)
            {
                ((Button) fpKeypadSelection.Controls[i]).Text = _keypadSelection[i] = "";
                ((Button) fpKeypadOrder.Controls[i]).Visible = false;
            }
            fpKeypadLabel.Visible = false;
        }

        

        private void KeypadSymbol_Click(object sender, EventArgs e)
        {
            var keypadorder = (_manualVersion == 0 ? _keypadOrder241 : _keypadOrder724);

            for (var i = 0; i < 4; i++)
                if (((Button) sender).Text == _keypadSelection[i]) return;

            for (var i = 3; i > 0; i--)
                _keypadSelection[i] = _keypadSelection[i - 1];

            _keypadSelection[0] = ((Button) sender).Text;

            for (var i = 0; i < 4; i++)
                ((Button) fpKeypadSelection.Controls[i]).Text = _keypadSelection[i];
            

            if (_keypadSelection[3] == "") return;
            var keypadFound = true;
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
                    order.Add(keypadorder[i].IndexOf(_keypadSelection[j], StringComparison.Ordinal), _keypadSelection[j]);
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
            if (keypadFound) return;
            for (var i = 0; i < 4; i++)
            {
                ((Button)fpKeypadOrder.Controls[i]).Visible = false;
            }
            fpKeypadLabel.Visible = false;
        }

        private void keypadOrder_Click(object sender, EventArgs e)
        {

        }

        private void keypadSelection_Click(object sender, EventArgs e)
        {
            var i = 0;
            var selection = new string[3];
            for (var j=0;j<4;j++)
                if (_keypadSelection[j] != ((Button) sender).Text)
                    selection[i++] = _keypadSelection[j];
            for (var j = 0; j < 3; j++)
                _keypadSelection[j] = selection[j];
            _keypadSelection[3] = "";
            for (var j = 0; j < 4; j++)
            {
                ((Button) fpKeypadSelection.Controls[j]).Text = _keypadSelection[j];
                ((Button) fpKeypadOrder.Controls[j]).Visible = false;
            }
            fpKeypadLabel.Visible = false;
        }
    }

    public static class MemoryRules
    {
        public const int FirstPos = 0;
        public const int SecondPos = 1;
        public const int ThirdPos = 2;
        public const int FourthPos = 3;
        public const int One = 4;
        public const int Two = 5;
        public const int Three = 6;
        public const int Four = 7;
        public const int StageOnePos = 8;
        public const int StageTwoPos = 9;
        public const int StageThreePos = 10;
        public const int StageFourPos = 11;
        public const int StageOneLabel = 12;
        public const int StageTwoLabel = 13;
        public const int StageThreeLabel = 14;
        public const int StageFourLabel = 15;
    }

    public static class Wires
    {
        public const int None = 0;
        public const int Red = 1;
        public const int White = 2;
        public const int Black = 3;
        public const int Blue = 4;
        public const int Yellow = 5;
    }
}
