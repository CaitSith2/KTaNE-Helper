using System;
using System.Windows.Forms;
using VanillaRuleGenerator;

namespace VanillaRuleManualGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var manager = RuleManager.Instance;
            manager.Initialize((int) numericUpDown1.Value);
            var manaulgen = ManualGenerator.Instance;
            manaulgen.WriteAllManuals((int) numericUpDown1.Value);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = int.MaxValue;
            numericUpDown1.Minimum = int.MinValue;
            numericUpDown1.DecimalPlaces = 0;
            numericUpDown1.Increment = 1;
        }
    }
}
