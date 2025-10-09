using System.Collections.Generic;

namespace Code.Logic.Gameplay.Services.BulletsHolder
{
    public interface IBulletsHolder
    {
        IReadOnlyList<Bullet.Bullet> Bullets { get; }
        void Add(Bullet.Bullet bullet);
        void Remove(Bullet.Bullet bullet);
        void DestroyAll();
    }
}