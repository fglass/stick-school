using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game
{
    public class StaticTargetScript : MonoBehaviour
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