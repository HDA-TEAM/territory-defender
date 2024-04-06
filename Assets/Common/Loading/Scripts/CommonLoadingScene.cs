using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Common.Loading.Scripts
{
    public class CommonLoadingScene : ScriptableObject
    {
        public Action OnLoadingCompleted;
        public virtual void ReleaseResource()
        {
            
        }
        public virtual void StartLoading(Action onCompleted,IProgress<float> progress)
        {
            
        }
        public virtual bool IsLoaded() => true;
    }
}
