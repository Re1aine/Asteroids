namespace Code.Logic.Gameplay.Services.Holders.VFXHolder
{
    public interface IVFXHolder
    {
        void Add(VFX vfx);
        void Remove(VFX vfx);
        void DestroyAll();
    }
}