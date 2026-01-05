using UnityEngine;

namespace Code.Logic.Gameplay
{
    public class WaitForSecondsUnPaused : CustomYieldInstruction
    {
        private float _seconds;
        private readonly bool _isPaused;

        public WaitForSecondsUnPaused(bool isPaused, float seconds)
        {
            _isPaused = isPaused;
            _seconds = seconds;
        }
    
        public override bool keepWaiting
        {
            get
            {
                if (_isPaused)
                    return true;
            
                _seconds -= Time.deltaTime;
                return _seconds > 0f;
            }
        }
    }
}