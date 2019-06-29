namespace DefaultNamespace
{
    public class Game
    {
        public static Game Instance = new Game();

        public Field Field()
        {
            Field Field = new Field();
            return Field;
        }
    }
}