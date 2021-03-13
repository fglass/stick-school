using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game
{
    public class TargetScript : MonoBehaviour
    {
        public bool isHit;
        
        private void Update()
        {
            if (isHit)
            {
                Destroy(gameObject);
            }
        }
    }
}