using System;
using UnityEngine;
using Scripts.MasterDatas;

namespace Scripts
{
    public struct Node
    {
        public Vector2Int Position; //ノードの位置
        public double CostToGoal;      //推定コスト（ゴールまでのコスト）
        public Vector2Int Path;     //親のパス（ポインタ）
        internal bool IsLock;
        internal bool IsActive;
        internal double MoveCost;

        public Node(Vector2Int position,Vector2Int goalPosition) : this()
        {
            Position = position;
            IsLock = false;
            MoveCost = 0;
            Remove();
            UpdateGoalNode(goalPosition);

        }

        public static Node CreateBlankNode(Vector2Int position)
        {
            return new Node(position,new Vector2Int(-1,-1));
        }

        public void Remove()
        {
            IsActive = false;
        }

        public void UpdateGoalNode(Vector2Int goalPosition)
        {
            CostToGoal = Mathf.Sqrt(Mathf.Pow(goalPosition.x - Position.x, 2) +
                                    Mathf.Pow(goalPosition.y - Position.y , 2));
        }

        public double GetScore()
        {
            return MoveCost + CostToGoal;
        }

        public void Clear()
        {
            Remove();
            UpdateGoalNode(new Vector2Int(-1,-1));
        }

        public void SetFromNode(Vector2Int value)
        {
            Path = value;
        }

        public void Add()
        {
            IsActive = true;
        }

        public static Node CreateNode(Vector2Int position, Vector2Int goalPosition)
        {
            return new Node(position,goalPosition);
        }
        

        public void SetMoveCost(double cost)
        {
            MoveCost = cost;
        }
    }
}