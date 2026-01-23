using Code.Logic.Services.Authentification;
using Code.Logic.Services.Repository.Player;
using Code.Logic.Services.SaveLoad.LocalStrategy.Storage;
using Cysharp.Threading.Tasks;
#if !UNITY_EDITOR
using UnityEngine.Device;
using GamePush;
#endif

namespace Code.Logic.Services.SaveLoad.LocalStrategy
{
    public class LocalSaveLoadStrategy : ILocalSaveLoadStrategy
    {
        private const string PlayerSaveDataKeyPrefix = "PlayerSaveData_ID_";
        
        private readonly ILocalSaveLoadStorage _localSaveLoadStorage;
        private readonly IAuthentification _authentification;

        private string _playerSaveKey;

        public LocalSaveLoadStrategy(ILocalSaveLoadStorage localSaveLoadStorage, IAuthentification authentification)
        {
            _localSaveLoadStorage = localSaveLoadStorage;
            _authentification = authentification;
        }
        
        public UniTask SetPlayerData(PlayerSaveData data)
        {
            _localSaveLoadStorage.SetPlayerData(data, _playerSaveKey);
            return default;
        }

        public UniTask<PlayerSaveData> GetPlayerData() => 
            UniTask.FromResult(_localSaveLoadStorage.GetPlayerData(_playerSaveKey));

        public void InitializeKey()
        {
#if UNITY_EDITOR
            _playerSaveKey = $"{PlayerSaveDataKeyPrefix}EDITOR";
#else
            //if (_authentification.IsGuest)
            //    _playerSaveKey = $"{PlayerSaveDataKeyPrefix}{SystemInfo.deviceUniqueIdentifier.GetHashCode():X}";
            //else
                _playerSaveKey = $"{PlayerSaveDataKeyPrefix}{GP_Player.GetID()}";
#endif
        }
    }
}