using System;
using System.Linq;
using System.Windows.Forms;
using VanillaRuleGenerator.Extensions;
using VanillaRuleGenerator.Rules;
using VanillaRuleGenerator.Rules.BombGame;

namespace VanillaRuleGenerator.Modules
{
    public class ButtonComponent : BombComponent
    {
        private ButtonComponent()
        {
        }
        
        public void InitializeHoldRules(FlowLayoutPanel panel, Label pressLabel, ComboBox buttonColor, ComboBox buttonName)
        {
            var queries = RuleManager.Instance.ButtonRuleSet.RuleList.SelectMany(rule => rule.Queries).Select(x => x.Property.Text).ToList();

            buttonColor.Visible = queries.Any(x => x.Equals(QueryableButtonProperty.IsButtonColor.Text, StringComparison.Ordinal));
            buttonName.Visible = queries.Any(x => x.Equals(QueryableButtonProperty.IsButtonInstruction.Text, StringComparison.Ordinal));

            var holdInstructions = RuleManager.Instance.ButtonRuleSet.HoldRuleList.Select(rule => $@"{rule.GetQueryString()} {rule.GetSolutionString()}").ToList();
            var labels = panel.Controls.Cast<Control>().Select(x => (x as Label)).Where(y => y != null).ToList();
            if (labels.Count != holdInstructions.Count)
            {
                for (var i = panel.Controls.Count - 1; i >= 0; i--)
                {
                    var label = panel.Controls[i];
                    panel.Controls.RemoveAt(i);
                    label.Dispose();
                }
                foreach (var holdInstruction in holdInstructions)
                {
                    var label = pressLabel.Clone();
                    label.Text = holdInstruction;
                    panel.Controls.Add(label);
                }
            }
            else
            {
                for (var i = 0; i < holdInstructions.Count; i++)
                {
                    labels[i].Text = holdInstructions[i];
                }
            }
        }

        private static ButtonComponent _instance;
        public static ButtonComponent Instance => _instance ?? (_instance = new ButtonComponent());

        public bool IsHolding;
        public ButtonColor ButtonColor;
        public ButtonInstruction ButtonInstruction;
        public BigButtonLEDColor IndicatorColor;
    }
}