using System.Collections.Generic;
using Scripts.MasterDatas;
using UnityEngine;
using UnityEngine.UIElements;
///
/// 参考 : https://github.com/baobao/UnityAstarSample/tree/master/Assets/Astar
///       https://qiita.com/2dgames_jp/items/f29e915357c1decbc4b7
///

namespace Scripts
{
    public class Aster
    {
        private int _fieldSize;
        private Node[,] _nodes;
        private Node[,] _openNodes;
        private Node[,] _closedNodes;

        private float _diagonalMoveCost;

        public void Init()
        {
            _fieldSize = GameController.Instance.field.FieldSize;
            _nodes = new Node[_fieldSize,_fieldSize];
            _openNodes = new Node[_fieldSize,_fieldSize];
            _closedNodes = new Node[_fieldSize,_fieldSize];
            SetDiagonalMoveCost(Mathf.Sqrt(1f));

            for (int x = 0; x < _fieldSize; x++)
            {
                for (int y = 0; y < _fieldSize; y++)
                {
                    _nodes[x,y] = Node.CreateBlankNode(new Vector2Int(x,y));
                    _openNodes[x, y] = Node.CreateBlankNode(new Vector2Int(x, y));
                    _closedNodes[x, y] = Node.CreateBlankNode(new Vector2Int(x, y));
                }
            }
        }

        public bool SearchRoute(Vector2Int startNode, Vector2Int goalNode, List<Vector2Int> routeList)
        {
            ResetNode();
            if (startNode == goalNode) return false;
            // 全ノード更新
            for (int x = 0; x < _fieldSize; x++)
            {
                for (int y = 0; y < _fieldSize; y++)
                {
                    _nodes[x, y].UpdateGoalNode(goalNode);
                    _openNodes[x, y].UpdateGoalNode(goalNode);
                    _closedNodes[x, y].UpdateGoalNode(goalNode);
                }
            }
            
            _openNodes[startNode.x, startNode.y] = Node.CreateNode(startNode, goalNode);
            _openNodes[startNode.x, startNode.y].SetFromNode(startNode);
            _openNodes[startNode.x, startNode.y].Add();

            while (true)
            {
                var bestScoreNode = GetBestScoreNode();
                OpenNode(bestScoreNode, goalNode);
                if (bestScoreNode == goalNode) break;
            }
            ResolveRoute(startNode, goalNode, routeList);
            return true;
        }

        

        private void OpenNode(Vector2Int bestNode, Vector2Int goalNode)
        {
            // 4方向走査
            for (int dx = -1; dx < 2; dx++)
            {
                for (int dy = -1; dy < 2; dy++)
                {
                    int cx = bestNode.x + dx;
                    int cy = bestNode.y + dy;

                    if (CheckOutOfRange(dx,dy,bestNode.x, bestNode.y) == false)
                    {
                        continue;
                    }

                    if (_nodes[cx, cy].IsLock)
                    {
                        continue;
                    }

                    // 縦横で動く場合はコスト : 1
                    // 斜めに動く場合はコスト : _diagonalMoveCost
                    var addCost = dx * dy == 0 ? 1 : _diagonalMoveCost;
                    _nodes[cx, cy].SetMoveCost(_openNodes[bestNode.x, bestNode.y].MoveCost + addCost);
                    _nodes[cx, cy].SetFromNode(bestNode);

                    // ノードのチェック
                    UpdateNodeList(cx, cy, goalNode);
                }
            }
            
            // 展開が終わったノードは closed に追加する
            _closedNodes[bestNode.x, bestNode.y] = _openNodes[bestNode.x, bestNode.y];
            // closedNodesに追加
            _closedNodes[bestNode.x, bestNode.y].Add();
            // openNodesから削除
            _openNodes[bestNode.x, bestNode.y].Remove();
        }
        
        
        public Vector2Int GetBestScoreNode()
        {
            var result = new Vector2Int(0,0);
            double min = double.MaxValue;
            for (int x = 0; x < _fieldSize; x++)
            {
                for (int y = 0; y < _fieldSize; y++)
                {
                    if (_openNodes[x, y].IsActive == false) {continue;}

                    if (min > _openNodes[x, y].GetScore())
                    {
                        min = _openNodes[x, y].GetScore();
                        result = _openNodes[x, y].Position;
                    }
                    
                }
            }

            return result;
        }
        public bool CheckOutOfRange(int dx, int dy, int x, int y)
        {
            var cells = GameController.Instance.field.Cells.ArrayCells2D;
            if (dx == 0 && dy == 0) return false;
            int cx = x + dx;
            int cy = y + dy;
            if (cx < 0 || cx == _fieldSize || cy < 0 || cy == _fieldSize) return false;
            if (cells[cx, cy].State != MasterFieldData.floor) return false;
            if (cells[cx, cy].OnState == MasterFieldOnState.Player ||
                cells[cx, cy].OnState == MasterFieldOnState.Enemy) return false;
            return true;
        }

        public void UpdateNodeList(int x, int y, Vector2Int goalNode)
        {
            if (_openNodes[x, y].IsActive)
            {
                // より優秀なスコアであるならMoveCostとfromを更新する
                if (_openNodes[x, y].GetScore() > _nodes[x, y].GetScore())
                {
                    // Node情報の更新
                    _openNodes[x, y].SetMoveCost(_nodes[x, y].MoveCost);
                    _openNodes[x, y].SetFromNode(_nodes[x, y].Path);
                }
            }
            else if (_closedNodes[x, y].IsActive)
            {
                // より優秀なスコアであるなら closedNodesから除外しopenNodesに追加する
                if (_closedNodes[x, y].GetScore() > _nodes[x, y].GetScore())
                {
                    _closedNodes[x, y].Remove();
                    _openNodes[x, y].Add();
                    _openNodes[x, y].SetMoveCost(_nodes[x, y].MoveCost);
                    _openNodes[x, y].SetFromNode(_nodes[x, y].Path);
                }
            }
            else
            {
                _openNodes[x, y] = new Node(new Vector2Int(x, y), goalNode);
                _openNodes[x, y].SetFromNode(_nodes[x, y].Path);
                _openNodes[x, y].SetMoveCost(_nodes[x, y].MoveCost);
                _openNodes[x, y].Add();
            }
        }
        

       
        public void SetDiagonalMoveCost(float cost)
        {
            _diagonalMoveCost = cost;
        }

        

        private void ResetNode()
        {
            for (int x = 0; x < _fieldSize; x++)
            {
                for (int y = 0; y < _fieldSize; y++)
                {
                    _nodes[x, y].Clear();
                }
            }
        }
        public void ResolveRoute(Vector2Int startNode, Vector2Int goalNode, List<Vector2Int> result)
        {
            if (result == null)
            {
                result = new List<Vector2Int>();
            }
            else
            {
                result.Clear();
            }

            var node = _closedNodes[goalNode.x, goalNode.y];
            result.Add(goalNode);

            int cnt = 0;
            int tryCount = 1000;
            bool isSuccess = false;
            while (cnt++ < tryCount)
            {
                var beforeNode = result[0];
                if (beforeNode == node.Path)
                {
                    //失敗
                    break;
                }

                if (node.Path == startNode)
                {
                    isSuccess = true;
                    break;
                }
                else
                {
                    result.Insert(0,node.Path);
                }

                node = _closedNodes[node.Path.x, node.Path.y];
                
            }

            if (isSuccess == false)
            {
                Debug.Log("失敗");
            }
        }
    }
}