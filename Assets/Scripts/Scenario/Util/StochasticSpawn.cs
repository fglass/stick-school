using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Util
{
    public static class StochasticSpawn
    {
        public static Vector2 InBounds(Vector2 origin, float minX, float maxX, float minY, float maxY)
        {
            var x = Random.Range(minX, maxX) + 1;
            var y = Random.Range(minY, maxY) + 1;
            return origin + new Vector2(x, y);
        }

        public static Vector2 InHollowRectangle(Vector2 origin, float width, float height, float thickness)
        {
            // TODO: refactor
            var varyX = RandomBool();
            var x = varyX ? Random.Range(width - thickness, width) : Random.Range(0f, width);
            var y = varyX ? Random.Range(0f, height) : Random.Range(height - thickness, height);

            x = RandomBool() ? x : -x;
            y = RandomBool() ? y : -y;
            var offset = new Vector2(x, y); 
            
            return origin + offset;
        }
        
        public static Vector2 InAnnulus(Vector2 origin, float minRadius, float maxRadius)
        {
            var direction = Random.insideUnitCircle.normalized;
            var radius = Random.Range(minRadius, maxRadius);
            return origin + direction * radius;
        }

        private static bool RandomBool() => Random.value > 0.5f;
    }
}