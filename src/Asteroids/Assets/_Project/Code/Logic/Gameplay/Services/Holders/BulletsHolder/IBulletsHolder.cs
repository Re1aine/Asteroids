using System.Collections.Generic;
using _Project.Code.Logic.Gameplay.Projectiles.Bullet;

namespace _Project.Code.Logic.Gameplay.Services.Holders.BulletsHolder
{
    public interface IBulletsHolder
    {
        IReadOnlyList<Bullet> Bullets { get; }
        void Add(Bullet bullet);
        void Remove(Bullet bullet);
        void DestroyAll();
    }
}