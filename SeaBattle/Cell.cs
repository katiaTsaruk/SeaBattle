namespace SeaBattle
{
    public class Cell
    {
        public int x, y;
        public bool isFree;

        public Cell( bool SetIsFree)
        {
            isFree = SetIsFree;
        }
        public Cell(int setX, int setY)
        {
            x=setX; 
            y=setY;
        }
    }
}