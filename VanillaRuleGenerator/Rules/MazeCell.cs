namespace VanillaRuleGenerator.Rules
{
    public class MazeCell
    {
        public MazeCell(int x, int y)
        {
            this.Visited = false;
            this.WallAbove = true;
            this.WallBelow = true;
            this.WallLeft = true;
            this.WallRight = true;
            this.HideLeft = false;
            this.HideRight = false;
            this.HideAbove = false;
            this.HideBelow = false;
            this.X = x;
            this.Y = y;
        }

        public static void RemoveWalls(MazeCell m1, MazeCell m2)
        {
            if (m1.X - m2.X == 1)
            {
                m1.WallLeft = false;
                m2.WallRight = false;
            }
            if (m1.X - m2.X == -1)
            {
                m1.WallRight = false;
                m2.WallLeft = false;
            }
            if (m1.Y - m2.Y == 1)
            {
                m1.WallAbove = false;
                m2.WallBelow = false;
            }
            if (m1.Y - m2.Y == -1)
            {
                m1.WallBelow = false;
                m2.WallAbove = false;
            }
        }

        public static bool WallBetween(MazeCell m1, MazeCell m2, bool useHidden)
        {
            if (m1.X - m2.X == 1)
            {
                return m1.WallLeft && (!m1.HideLeft || useHidden);
            }
            if (m1.X - m2.X == -1)
            {
                return m1.WallRight && (!m1.HideRight || useHidden);
            }
            if (m1.Y - m2.Y == 1)
            {
                return m1.WallAbove && (!m1.HideAbove || useHidden);
            }
            return m1.Y - m2.Y == -1 && m1.WallBelow && (!m1.HideBelow || useHidden);
        }

        public int ToInt()
        {
            int num = 0;
            num |= ((!this.WallLeft) ? 0 : 1);
            num <<= 1;
            num |= ((!this.WallRight) ? 0 : 1);
            num <<= 1;
            num |= ((!this.WallAbove) ? 0 : 1);
            num <<= 1;
            num |= ((!this.WallBelow) ? 0 : 1);
            num <<= 1;
            num |= ((!this.IsIdentifier) ? 0 : 1);
            num <<= 1;
            num |= ((!this.HideLeft) ? 0 : 1);
            num <<= 1;
            num |= ((!this.HideRight) ? 0 : 1);
            num <<= 1;
            num |= ((!this.HideAbove) ? 0 : 1);
            num <<= 1;
            return num | ((!this.HideBelow) ? 0 : 1);
        }

        public void FromInt(int value)
        {
            this.HideBelow = ((value | 1) == value);
            value >>= 1;
            this.HideAbove = ((value | 1) == value);
            value >>= 1;
            this.HideRight = ((value | 1) == value);
            value >>= 1;
            this.HideLeft = ((value | 1) == value);
            value >>= 1;
            this.IsIdentifier = ((value | 1) == value);
            value >>= 1;
            this.WallBelow = ((value | 1) == value);
            value >>= 1;
            this.WallAbove = ((value | 1) == value);
            value >>= 1;
            this.WallRight = ((value | 1) == value);
            value >>= 1;
            this.WallLeft = ((value | 1) == value);
        }

        public bool Visited;

        public bool IsIdentifier;

        public bool HideLeft;

        public bool HideRight;

        public bool HideAbove;

        public bool HideBelow;

        public bool WallAbove;

        public bool WallBelow;

        public bool WallLeft;

        public bool WallRight;

        public int X;

        public int Y;
    }
}
