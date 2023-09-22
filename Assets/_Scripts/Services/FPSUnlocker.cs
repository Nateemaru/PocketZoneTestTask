using UnityEngine;

namespace _Scripts.Services
{
    public class FPSUnlocker
    {
        public FPSUnlocker()
        {
            if(Application.isMobilePlatform)
                Application.targetFrameRate = 60;
            else
                Application.targetFrameRate = -1;
        }
    }
}