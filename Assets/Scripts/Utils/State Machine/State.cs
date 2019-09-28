using System;
public abstract class State<T>
{
    public abstract void OnStateEnter(T owner);
    public abstract void OnStateExit(T owner);
    public abstract void OnStateUpdate(T owner);
}
