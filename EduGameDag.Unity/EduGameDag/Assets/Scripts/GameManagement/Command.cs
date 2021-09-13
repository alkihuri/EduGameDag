namespace Game
{
    public interface Command
    {
        void Execute();
        void Undo();
    }
}