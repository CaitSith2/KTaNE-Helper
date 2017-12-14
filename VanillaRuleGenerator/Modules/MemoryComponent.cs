namespace VanillaRuleGenerator.Modules
{
    public class MemoryComponent : BombComponent
    {
        public TextMesh DisplayText;
        public static int NUM_STAGES = 5;

        public int GetIndexOfButtonLabelled(int label)
        {
            return 0;
        }

        public int GetIndexOfButtonPressedInStage(int stage)
        {
            return 0;
        }

        public int GetIndexOfButtonWithSameLabelPressedInStage(int stage)
        {
            return 0;
        }

    }

    public class TextMesh
    {
        public string text;
    }
}