namespace OceanProj
{
    public class OceanRealizeInterface : IOceanInterface
    {

        private int currentCursorY = 0;

        private Cell[,] prevFieldState = null;
        private bool isBorderPrinted = false;

        public int oceanWidth { get; }
        public int oceanHeight { get; }

        public OceanRealizeInterface(int width, int height)
        {
            oceanWidth = width;
            oceanHeight = height;
            prevFieldState = new Cell[height, width];
            Console.Clear();
        }

        private void FillingHorizontalBorders(int length)
        {
            Console.Write("+");

            for (int i = 0; i < length; i++)
            {
                Console.Write("-");
            }

            Console.Write("+\n");
        }

        private void PrintBorder()
        {
            FillingHorizontalBorders(oceanWidth);
            for (int i = 0; i < oceanHeight; i++)
            {
                Console.Write('|');
                for (int j = 0; j < oceanWidth; j++)
                {
                    Console.Write(' ');
                }
                Console.Write("|\n");
            }
            FillingHorizontalBorders(oceanWidth);
        }

        private void SetCell(int x, int y, Cell cell)
        {
            Console.SetCursorPosition(x + 1, currentCursorY + y + 1);
            Console.Write(cell.Image);
        }
        private void DisplayFilling(in Cell[,] field)
        {
            currentCursorY = Console.GetCursorPosition().Top;

            if(!isBorderPrinted)
            {
                PrintBorder();
                isBorderPrinted = true;
            }

            for (int i = 0; i < oceanHeight; i++)
            {
                for (int j = 0; j < oceanWidth; j++)
                {
                    if (prevFieldState[i, j] == null || prevFieldState[i, j] != field[i, j])
                    {
                        SetCell(j, i, field[i, j]);
                    }
                    prevFieldState[i, j] = field[i, j];
                }
            }

            Console.SetCursorPosition(0, currentCursorY + oceanHeight + 2);
        }

        public void SetCursorToEnd()
        {
            Console.SetCursorPosition(0, currentCursorY + oceanHeight + 7);
        }

        public void DisplayStats(in OceanStats stats)
        {
            Console.WriteLine($"\nCycle: {stats.NumCycle} ");
            Console.WriteLine($"Predators: {stats.NumPredators} ");
            Console.WriteLine($"Prey: {stats.NumPreys} ");
            Console.WriteLine($"Obstacles: {stats.NumObstacles} ");
        }

        public void Display(in Cell[,] field, in OceanStats stats)
        {
            DisplayFilling(field);
            DisplayStats(stats);
            Console.SetCursorPosition(0, 0);
        }
    }
}
