namespace SeaBattle
{
    public class Cell
    {
        public int width, height;
        public bool isFree;

        public Cell( bool SetIsFree)
        {
            isFree = SetIsFree;
        }
        public Cell(int setWidth, int setHeight)
        {
            width=setWidth; 
            height=setHeight;
        }
    }
}