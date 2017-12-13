using BombGame;

namespace KTaNE_Helper.Modules
{
    public class ButtonComponent : BombComponent
    {
        private ButtonComponent()
        {
        }

        private static ButtonComponent _instance;
        public static ButtonComponent Instance => _instance ?? (_instance = new ButtonComponent());

        public bool IsHolding;
        public ButtonColor ButtonColor;
        public ButtonInstruction ButtonInstruction;
        public BigButtonLEDColor IndicatorColor;
    }
}