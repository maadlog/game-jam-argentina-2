using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PointDispatcher
{
    public class RandomPointFromArraySet: IPointDispatcherStrategy
    {
        public Dictionary<int, int> onUse = new Dictionary<int, int>();
        public List<Vector3> points;
        private List<Vector3> pointsOnUse = new List<Vector3>();
        public RandomPointFromArraySet(IEnumerable<Vector3> points)
        {
            this.points = points.ToList();
        }

        public Vector3 GetNextPoint(int requester)
        {
            Vector3 result;
            
            result = points[Random.Range(0, points.Count)];

            if (onUse.ContainsKey(requester))
            {
                int index = onUse[requester];
                Vector3 point = pointsOnUse[index];
                pointsOnUse.RemoveAt(index);
                points.Add(point);
            }

            pointsOnUse.Add(result);
            points.Remove(result);
            onUse[requester] = pointsOnUse.IndexOf(result);

            return result; // Normalize direction and get distance
        }
    }

    public class RandomAtFixedDistanceFromCenter: IPointDispatcherStrategy
    {
        public Vector3 center;
        public float spawnDistance;
        public RandomAtFixedDistanceFromCenter(Vector3 center, float spawnDistance)
        {
            this.center = center;
            this.spawnDistance = spawnDistance;
        }

        public Vector3 GetNextPoint(int requester)
        {
            Vector3 spawnPosition = center;

            Vector3 randomCirclePosition = Random.insideUnitCircle;

            return spawnPosition + randomCirclePosition.normalized * spawnDistance; // Normalize direction and get distance
        }
    }

    public class RandomAtFixedDistanceFromPoints : IPointDispatcherStrategy
    {
        public Vector3[] centerPoints;
        public float spawnDistance;
        public RandomAtFixedDistanceFromPoints(Vector3[] centerPoints, float spawnDistance)
        {
            this.centerPoints = centerPoints;
            this.spawnDistance = spawnDistance;
        }

        public Vector3 GetNextPoint(int requester)
        {
            Vector3 spawnPosition = centerPoints[Random.Range(0, centerPoints.Length)];

            Vector3 randomCirclePosition = Random.insideUnitCircle;

            return spawnPosition + randomCirclePosition.normalized * spawnDistance; // Normalize direction and get distance
        }
    }


    public IPointDispatcherStrategy strategy;
    public PointDispatcher(IPointDispatcherStrategy strategy)
    {
        this.strategy = strategy;
    }

    public Vector3 GetNextPoint(int requester = 0)
    {
        return this.strategy.GetNextPoint(requester);
    }
}
public interface IPointDispatcherStrategy
{
    Vector3 GetNextPoint(int requester);
}


