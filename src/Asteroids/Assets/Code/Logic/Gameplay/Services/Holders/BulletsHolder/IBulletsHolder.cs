using System.Collections.Generic;
using Code.Logic.Gameplay.Projectiles.Bullet;

namespace Code.Logic.Gameplay.Services.Holders.BulletsHolder
{
    public interface IBulletsHolder
    {
        IReadOnlyList<Bullet> Bullets { get; }
        void Add(Bullet bullet);
        void Remove(Bullet bullet);
        void DestroyAll();
    }
}