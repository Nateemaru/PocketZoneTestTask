using System.Collections;
using UnityEngine;

namespace _Scripts.Services.CoroutineRunnerService
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}