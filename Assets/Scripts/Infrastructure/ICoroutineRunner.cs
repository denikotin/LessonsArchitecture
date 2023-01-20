using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public interface ICoroutineRunner
    {
       public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
