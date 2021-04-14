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
            if (x < Size.x-1 && isCellFree)
            {
                isCellFree = CellCoord[x+1, y].isFree;
            }
            if (x > 0 && isCellFree)
            {
                isCellFree = CellCoord[x-1, y].isFree;
            }
            if (y< Size.y-1 && isCellFree) 
            {
                isCellFree = CellCoord[x, y+1].isFree;
            }
            if (y >0 && isCellFree) 
            {
                isCellFree = CellCoord[x, y-1].isFree;
            }
            return isCellFree;
        } 
    }
} 