namespace OceanProj
{
    public interface IOceanInterface
    {
        public void Display(in Cell[,] field, in OceanStats stats);
        public void SetCursorToEnd();

    }
}
