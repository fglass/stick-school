using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UI
{
    public class FpsCounter : MonoBehaviour
    {
        private const int WindowSize = 1000;
        private static readonly TimeSpan UpdateInterval = TimeSpan.FromSeconds(1);
        
        private readonly Queue<float> _measurements = new Queue<float>(WindowSize);
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
        private TextMeshProUGUI _field;

        public void Awake()
        {
            _field = GetComponent<TextMeshProUGUI>();
        }

        public void Update() {
            TakeMeasurement();
            UpdateFieldIfReady();
        }

        private void TakeMeasurement()
        {
            _measurements.Enqueue(1 / Time.unscaledDeltaTime);

            if (_measurements.Count > WindowSize)
            {
                _measurements.Dequeue();
            }
        }

        private void UpdateFieldIfReady()
        {
            if (_stopwatch.Elapsed > UpdateInterval)
            {
                _field.text = CalculateMovingAverage().ToString(CultureInfo.InvariantCulture);
                _stopwatch.Restart();
            }
        }

        private int CalculateMovingAverage()
        {
            return Mathf.RoundToInt(_measurements.Average());
        }
    }
}