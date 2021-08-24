using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scenario.Target
{
    public class ShatterEffect : MonoBehaviour
    {
        private const float EffectDuration = 0.2f;
        private const float ShatterForce = 50;
        private const float OpacityDelta = 0.1f;
        private static readonly int ColourId = Shader.PropertyToID("TargetColour");
        private static readonly int AlphaId = Shader.PropertyToID("TargetAlpha");
        private IEnumerable<Material> _materials;

        public void Awake()
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
            yield return new WaitForSeconds(EffectDuration);
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
                var newAlpha = material.GetFloat(AlphaId) - OpacityDelta;
                material.SetFloat(AlphaId, newAlpha);
            }
        }
        
        public void SetColour(Color colour)
        {
            foreach (var material in _materials)
            {
                material.SetColor(ColourId, colour);
            }
        }
    }
}