namespace DefaultNamespace
{
    public class Parcel
    {
        /// <summary>
        /// <param name="部屋のID"></param>
        /// </summary>
        public int ParcelId;

        /// <summary>
        /// <param name="部屋の左下のX座標"></param>
        /// </summary>
        public int X;

        /// <summary>
        /// 部屋の右下のY座標
        /// </summary>
        public int Y;
    
        /// <summary>
        /// <param name="xのながさ"></param>
        /// </summary>
        private int _xRange; 
        public int XRange
        {
            get => _xRange;
            set
            {
                _xRange = value;
                Volume = value * YRange;
            }
        }

        /// <summary>
        /// <param name="yのながさ"></param>
        /// </summary>
        private int _yRange;
        public int YRange
        {
            get => _yRange;
            set
            {
                _yRange = value;
                Volume = value * XRange;
            }
        }

        /// <summary>
        /// <param name="体積"></param>
        /// </summary>
        public int Volume;


        /// <summary>
        /// <param name="Room"></param>
        /// </summary>
        public Room Room;

        public Parcel SetParcel(int id,int x,int y,int xRange,int yRange)
        {
            ParcelId = id;
            X = x;
            Y = y;
            XRange = xRange;
            YRange = yRange;
            Volume = xRange * yRange;
            var Cells = GameController.Instance.field.Cells;
            Parcel parcel = new Parcel();
            for (int i = y; i < y + yRange; i++)
            {
                for (int j = x; j < x + xRange;j++)
                {
                    Cells.ArrayCells2D[j, i].State = MasterFieldData.floor;
                    Cells.ArrayCells2D[j, i].ParcelId = id;
                }
            }
            return parcel;
        }
    }
}