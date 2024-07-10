using System;


public interface IGameListener
{
    static event Action<IGameListener> RegisterEvent;
    static event Action<IGameListener> UnregisterEvent;
    static void Register(IGameListener gameListener)
    {
        RegisterEvent?.Invoke(gameListener);
    }

    static void Unregister(IGameListener gameListener)
    {
        UnregisterEvent?.Invoke(gameListener);
    }
}
