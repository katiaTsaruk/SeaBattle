namespace SeaBattle
{
    public class Field
    {
        public readonly (int x, int y) Size = (10, 10);
        public Cell[,] CellCoord;

        public Field()
        {
            CellCoord = new Cell[Size.x, Size.y];
        }
        public bool IsPlaceFree(int x, int y, int length, bool isRow)
        {
            int counter = 0;
            
            while (counter < length && IsCellFree(x,y))
            {
                if (isRow)
                {
                    x++;
                }
                else
                {
                    y++;
                }
                counter++;
            }
            return counter == length;
        }
        public bool IsCellFree(int x, int y)
        {
            bool isCellFree=CellCoord[x, y].isFree;
            if (x < Size.x && isCellFree)
            {
                isCellFree = CellCoord[x+1, y].isFree;
            }
            if (x > 2 && isCellFree)
            {
                isCellFree = CellCoord[x-1, y].isFree;
            }
            if (y != 2 && y != Size.y + 3 && isCellFree) 
            {
                isCellFree = CellCoord[x, y-1].isFree;
            }
            if (y != Size.y && y != Size.y * 2 + 1 && isCellFree) 
            {
                isCellFree = CellCoord[x, y+1].isFree;
            }
            return isCellFree;
        } 
    }
} 