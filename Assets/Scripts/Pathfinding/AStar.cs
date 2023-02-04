using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AStarNode
{
	public float GCost;
	public float HCost;
	public float FCost;
	public int XIndex;
	public int YIndex;
	public AStarNode Parent;

	public AStarNode(float gCost, float hCost, int xIndex, int yIndex, AStarNode parent = null)
	{
		GCost = gCost;
		HCost = hCost;
		FCost = gCost + hCost;
		XIndex = xIndex;
		YIndex = yIndex;
		Parent = parent;
	}

	public AStarNode(int xIndex, int yIndex, AStarNode parent = null)
	{
		XIndex = xIndex;
		YIndex = yIndex;
		Parent = parent;
	}

	public void SetParrent(AStarNode parent)
	{
		Parent = parent;
	}

	public void SetCosts(float gCost, float hCost)
	{
		GCost = gCost;
		HCost = hCost;
		FCost = gCost + hCost;
	}

	public override int GetHashCode()
	{
		return XIndex.GetHashCode() ^ YIndex.GetHashCode();
	}

	public override bool Equals(object obj)
	{
		//Check for null and compare run-time types.
		if ((obj == null) || !this.GetType().Equals(obj.GetType()))
		{
			return false;
		}
		else
		{
			AStarNode node = (AStarNode)obj;
			return (this.XIndex == node.XIndex) && (this.YIndex == node.YIndex);
		}
	}
}

public class AStar <T>
{
	private static bool IsInBounds(int x, int y, float[,] weightMap)
	{
		return x >= 0 && x < weightMap.GetLength(1) && y >= 0 && y < weightMap.GetLength(1);
	}

	private static AStarNode[] GetNeighbours(AStarNode aStarNode, float[,] weightMap)
	{
		List<AStarNode> neighbours = new List<AStarNode>();

		for (int y = -1; y <= 1; y++)
		{
			for (int x = -1; x <= 1; x++)
			{
				if (x == 0 && y == 0)
				{
					continue;
				}

				if (!IsInBounds(aStarNode.XIndex + x, aStarNode.YIndex + y, weightMap))
				{
					continue;
				}

				float neighbourWeight = weightMap[aStarNode.XIndex + x, aStarNode.YIndex + y];

				if (neighbourWeight != -1)
				{
					AStarNode newNeighbour = new AStarNode(aStarNode.XIndex + x, aStarNode.YIndex + y, aStarNode);
					if (Mathf.Abs(x) == 1 && Mathf.Abs(y) == 1)
					{
						neighbourWeight *= Mathf.Sqrt(2);
					}

					newNeighbour.GCost = aStarNode.GCost + neighbourWeight;
					newNeighbour.FCost = newNeighbour.GCost + newNeighbour.HCost;

					neighbours.Add(newNeighbour);
				}
			}
		}

		return neighbours.ToArray();
	}

	private static T[] BacktracePath(AStarNode goal, T[,] objectMap)
	{
		List<T> path = new List<T>();
		AStarNode current = goal;

		while (current != null)
		{
			path.Add(objectMap[current.XIndex, current.YIndex]);
			current = current.Parent;
		}

		path.Reverse();
		return path.ToArray();
	}

	public static T[] PathFind(T[,] objectMap, float[,] weightMap, int startX, int startY, int goalX, int goalY, CancellationTokenSource cancellationToken)
	{
		if (!IsInBounds(startX, startY, weightMap) || !IsInBounds(goalX, goalY, weightMap))
		{
			return new T[0];
		}

		PriorityQueue<AStarNode> open = new PriorityQueue<AStarNode>();
		List<AStarNode> closed = new List<AStarNode>();

		AStarNode startNode = new AStarNode(0, 0, startX, startY);
		closed.Add(startNode);
		open.Insert(0, startNode);

		while (!open.IsEmpty)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new T[0];
			}

			AStarNode current = open.RemoveMax();

			if (current.XIndex == goalX && current.YIndex == goalY)
			{
				return BacktracePath(current, objectMap);
			}

			foreach (AStarNode neighbour in GetNeighbours(current, weightMap))
			{
				neighbour.HCost = Vector2.Distance(new Vector2(neighbour.XIndex, neighbour.YIndex), new Vector2(goalX, goalY));
				neighbour.FCost = neighbour.GCost + neighbour.HCost;

				int dupeIndex = -1;

				for (int i = 0; i < closed.Count; i++)
				{
					if (closed[i].Equals(neighbour))
					{
						dupeIndex = i;
						break;
					}
				}

				if (dupeIndex != -1 && closed[dupeIndex].FCost <= neighbour.FCost)
				{
					continue;
				}

				if (dupeIndex == -1)
				{
					closed.Add(neighbour);
				}
				else
				{
					closed[dupeIndex] = neighbour;
				}

				open.Insert(neighbour.FCost, neighbour);
			}
		}

		return new T[0];
	}
}
