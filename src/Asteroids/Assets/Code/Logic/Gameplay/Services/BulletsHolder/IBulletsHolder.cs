using System.Collections.Generic;

public interface IBulletsHolder
{
    IReadOnlyList<Bullet> Bullets { get; }
    void Add(Bullet bullet);
    void Remove(Bullet bullet);
    void DestroyAll();
}