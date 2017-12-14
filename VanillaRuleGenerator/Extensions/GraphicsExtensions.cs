using System.Drawing;

namespace VanillaRuleGenerator.Extensions
{
    public static class GraphicsExtensions
    {
        public static void DrawWall(this Graphics gr, int x, int y, string d)
        {
            gr.DrawWall(Color.White, 3f, x, y, d);
        }

        public static void DrawWall(this Graphics gr, Color color, int x, int y, string d)
        {
            gr.DrawWall(color, 3f, x, y, d);
        }

        public static void DrawWall(this Graphics gr, Color color, float width, int x, int y, string d)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (d.Substring(0,1).ToLower())
            {
                case "u":
                case "n":
                    gr.DrawLine(new Pen(color,width), new Point((x * 47), (y * 47)), new Point((x * 47) + 47, (y * 47)));
                    break;
                case "d":
                case "s":
                    gr.DrawLine(new Pen(color, width), new Point((x * 47), (y * 47) + 47), new Point((x * 47) + 47, (y * 47) + 47));
                    break;
                case "l":
                case "w":
                    gr.DrawLine(new Pen(color, width), new Point((x * 47), (y * 47)), new Point((x * 47), (y * 47) + 47));
                    break;
                case "r":
                case "e":
                    gr.DrawLine(new Pen(color, width), new Point((x * 47) + 47, (y * 47)), new Point((x * 47) + 47, (y * 47) + 47));
                    break;
            }
        }

        public static void DrawMarker(this Graphics gr, Color color, int x, int y, int direction = -1)
        {
            gr.DrawEllipse(new Pen(color, 3f), (x * 47) + 10, (y * 47) + 10, 47 - 20, 47 - 20);
            switch (direction)
            {
                default:
                    return;
                case 0:
                    gr.DrawLine(new Pen(color,3f),new Point((x*47) + 23, (y*47) + 12), new Point((x*47 + 23), (y*47)));
                    break;
                case 1:
                    gr.DrawLine(new Pen(color, 3f), new Point((x * 47) + 36, (y * 47) + 23), new Point((x * 47) + 47, (y * 47) + 23));
                    break;
                case 2:
                    gr.DrawLine(new Pen(color, 3f), new Point((x * 47) + 23, (y * 47) + 36), new Point((x * 47) + 23, (y * 47) + 47));
                    break;
                case 3:
                    gr.DrawLine(new Pen(color, 3f), new Point((x * 47) + 12, (y * 47) + 23), new Point((x * 47), (y * 47) + 23));
                    break;
            }
        }

    }
}