using System;
public class Machine<T>
{
    State<T> currentState;
    T owner;
    Machine(T owner)
    {
        this.owner = owner;
    }

    public void ChangeState(State<T> newState)
    {
        if (newState != currentState)
        {
            currentState?.OnStateExit(owner);
            currentState = newState;
            currentState.OnStateEnter(owner);
        }
    }

    public void Update()
    {
        currentState?.OnStateUpdate(owner);
    }
}
