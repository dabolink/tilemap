using System;

public interface IReceiver<T> where T : IComparable<T>
{
    bool Receive(T item, int direction);

    bool ReceiveRequest(int dir);
}