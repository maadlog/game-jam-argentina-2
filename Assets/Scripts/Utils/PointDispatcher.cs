using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PointDispatcher
{
    public class RandomPointFromArraySet: IPointDispatcherStrategy
    {

        public List<Vector3> AvailablePoints;
        public Dictionary<int, Vector3> TakenPoints;
       
        public RandomPointFromArraySet(IEnumerable<Vector3> points)
        {
            this.AvailablePoints = points.ToList();
            this.TakenPoints = new Dictionary<int, Vector3>();
        }

        public Vector3 GetNextPoint(int requester)
        {
            Vector3 newPoint = GetAvailablePoint();
            
            ResetPointFor(requester);
            
            MarkPointTakenBy(requester, newPoint);

            return newPoint;
        }

        private Vector3 GetAvailablePoint() {
            return this.AvailablePoints.ElementAt(Random.Range(0,this.AvailablePoints.Count));
        }
        private void ResetPointFor(int requester) {
            if (TakenPoints.ContainsKey(requester)){
                Vector3 point = TakenPoints[requester];
                AvailablePoints.Add(point);
                TakenPoints.Remove(requester);
            }
        }
        private void MarkPointTakenBy(int requester,Vector3 point){
            AvailablePoints.Remove(point);
            TakenPoints.Add(requester, point);
        }


        public void Reset(int requester)
        {
            ResetPointFor(requester);
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
        public void Reset(int requester)
        {
            //Do nothing
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

        public void Reset(int requester)
        {
            //Do nothing
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
    public void Reset(int requester = 0)
    {
        this.strategy.Reset(requester);
    }
}
public interface IPointDispatcherStrategy
{
    Vector3 GetNextPoint(int requester);
    void Reset(int requester);
}


