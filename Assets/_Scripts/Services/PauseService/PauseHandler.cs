using System.Collections.Generic;

namespace _Scripts.Services.PauseService
{
    public class PauseHandler : IPauseHandler
    {
        private List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();

        public bool IsPaused { get; private set; }

        public void Register(IPauseHandler handler)
        {
            if (_pauseHandlers.Contains(handler))
            {
                return;
            }
            
            _pauseHandlers.Add(handler);
        }

        public void Unregister(IPauseHandler handler)
        {
            if (!_pauseHandlers.Contains(handler))
            {
                return;
            }

            _pauseHandlers.Remove(handler);
        }

        public void SetPause(bool isPaused)
        {
            IsPaused = isPaused;
            
            foreach (IPauseHandler handler in _pauseHandlers)
            {
                if (handler == null && _pauseHandlers.Contains(handler))
                {
                    _pauseHandlers.Remove(handler);
                    continue;
                }
                
                handler.SetPause(IsPaused);
            }
        }
    }
}