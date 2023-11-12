public abstract class ASystem
{
    protected PlayerInstance _player;
    public PlayerInstance player
    {
        get { return _player; }
        set { if (_player == null) _player = value; }
    }
}
