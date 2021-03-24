using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game
{
    public class ShatterScript : MonoBehaviour
    {
        private const float ShatterForce = 50;
        private const float OpacityDelta = 0.1f;
        private const float Duration = 0.2f;
        private IEnumerable<Material> _materials;

        public void Start()
        {
            Shatter();
            _materials = gameObject.GetComponentsInChildren<Renderer>().Select(fragment => fragment.material);
            StartCoroutine(DespawnRoutine());
        }

        private void Shatter()
        {
            foreach (var rb in GetComponentsInChildren<Rigidbody>())
            {
                var force = (rb.transform.position - transform.position).normalized * ShatterForce;
                rb.AddForce(force);
            }
        }
        
        private IEnumerator DespawnRoutine() {
            yield return new WaitForSeconds(Duration);
            Destroy(gameObject);
        }
        
        public void FixedUpdate()
        {
            ReduceOpacity();
        }
        
        private void ReduceOpacity()
        {
            foreach (var material in _materials)
            {
                var colour = material.color;
                colour.a -= OpacityDelta;
                material.color = colour;
            }
        }
    }
}