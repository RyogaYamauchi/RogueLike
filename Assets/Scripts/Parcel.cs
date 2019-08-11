namespace DefaultNamespace
{
    public class Parcel
    {
        /// <summary>
        /// 部屋のID
        /// </summary>
        public int Id;

        /// <summary>
        /// 部屋の左下のX座標
        /// </summary>
        public int X;

        /// <summary>
        /// 部屋の右下のY座標
        /// </summary>
        public int Y;
    
        /// <summary>
        /// 部屋のxのながさ
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
        /// //部屋のyの高さ
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
        /// 体積
        /// </summary>
        public int Volume;

        public Parcel SetParcel(int id,int x,int y,int xRange,int yRange)
        {
            Id = id;
            X = x;
            Y = y;
            XRange = xRange;
            YRange = yRange;
            Volume = xRange * yRange;
            var Cells = GameController.Instance.field.Cells;
            Parcel room = new Parcel();
            for (int i = y; i < y + yRange; i++)
            {
                for (int j = x; j < x + xRange;j++)
                {
                    Cells.ArrayCells2D[j, i].State = MasterFieldData.floor;
                    Cells.ArrayCells2D[j, i].ParcelId = id;
                }
            }
            return room;
        }
    }
}