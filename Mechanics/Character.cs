using System.Drawing;

namespace pac_man;

public class Character
{
    public Point GridPos { get; set; }
    public Point Direction { get; set; }
    public Color Color { get; set; }

    public Character(int x, int y, Color color)
    {
        GridPos = new Point(x, y);
        Direction = new Point(0, 0);
        Color = color;
    }

    public void Move(int[,] map)
    {
        int nextX = GridPos.X + Direction.X;
        int nextY = GridPos.Y + Direction.Y;
        if (nextY >= 0 && nextY < map.GetLength(0) &&
            nextX >= 0 && nextX < map.GetLength(1) &&
            map[nextY, nextX] != 1)
        {
            GridPos = new Point(nextX, nextY);
        }
    }
}