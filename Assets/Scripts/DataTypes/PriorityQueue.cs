using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue <T>
{
	private List<(double, T)> heap;

	// Returns the amount of elements stored in the heap
	public int Size { get => heap.Count; }
	public T Top { get => heap[0].Item2; }
	public bool IsEmpty { get => Size == 0; }

	// Constructor empty
	public PriorityQueue()
	{
		heap = new List<(double, T)>();
	}

	// Return true if pos is a leaf position, false otherwise
	private bool IsLeaf(int pos)
	{
		return (pos >= Size / 2) && (pos < Size);
	}

	private int LeftChildPos(int pos)
	{
		if (pos >= Size / 2)
		{
			return -1;
		}
		return 2 * pos + 1;
	}

	private int RightChildPos(int pos)
	{
		if (pos >= (Size - 1) / 2)
		{
			return -1;
		}
		return 2 * pos + 2;
	}

	private int ParentPos(int pos)
	{
		if (pos <= 0)
		{
			return -1;
		}
		return (pos - 1) / 2;
	}

	private void Swap(int pos1, int pos2)
	{
		(double, T) temp = heap[pos1];
		heap[pos1] = heap[pos2];
		heap[pos2] = temp;
	}

	public void Insert(double priorityKey, T value)
	{
		(double, T) element = (priorityKey, value);

		int currentPos = Size;

		heap.Add(element);  // Start at end of heap
							// Now sift up until curr's parent's key < curr's key
		while ((currentPos != 0) && (heap[currentPos].Item1 < heap[ParentPos(currentPos)].Item1))
		{
			Swap(currentPos, ParentPos(currentPos));
			currentPos = ParentPos(currentPos);
		}
	}

	// Put element in its correct place
	private void Siftdown(int pos)
	{
		if ((pos < 0) || (pos >= Size)) return; // Illegal position
		while (!IsLeaf(pos))
		{
			int j = LeftChildPos(pos);
			if ((j < (Size - 1)) && (heap[j].Item1 > heap[j + 1].Item1))
			{
				j++; // j is now index of child with lesser value
			}
			if (heap[pos].Item1 <= heap[j].Item1) return;
			Swap(pos, j);
			pos = j;  // Move down
		}
	}

	// Remove and return maximum value
	public T RemoveMax()
	{
		T temp = Top;
		Swap(0, Size - 1); // Swap maximum with last value
		heap.RemoveAt(Size - 1);
		if (Size != 0)      // Not on last element
			Siftdown(0);   // Put new heap root val in correct place
		return temp;
	}
}
