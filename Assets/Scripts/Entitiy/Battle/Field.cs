using Assets.Scripts.Presentation.States;

namespace DefaultNamespace
{
    public class Field
    {
        public int FieldSizeX;
        public int FieldSizeY;
        public FieldState Fieldstate;

        public Field()
        {
            Field filed = MakeField();
        }

        public Field MakeField()
        {
            Field field = new Field();
            return field;
        }

        private Road GetRoad(Position position,int length)
        {
            Road road = new Road();
            return road;    
        }
        
    }
    
}