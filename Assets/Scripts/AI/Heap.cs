using UnityEngine;
using System.Collections;
using System;

public class Heap<T> where T : IHeapItem<T> {
    T[] items;
    int currentItemCount;

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public void Add(T item)// at the item to the heap
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;// add the item to the last place of the array
        SortUp(item);//move the item up the heap to where it is supposed to be
        currentItemCount++;//increase the item count since there is a new item
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];// get the first item in the heap/array
        currentItemCount--;//decrease the item count since we are removing one
        items[0] = items[currentItemCount];//replace the first place with the last element
        items[0].HeapIndex = 0;//update the new root's index
        SortDown(items[0]);//move the new root to it's rightful position
        return firstItem;//return the value that is removed
    }

    void SortDown(T item)
    {
        while(true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;//formula, look at end of file
            int childIndexRight = item.HeapIndex * 2 + 2;//formula, look at end of file
            int swapIndex = 0;

            if(childIndexLeft < currentItemCount)//check if the child exists
            {
                swapIndex = childIndexLeft;//default the index to the left child
                if(childIndexRight < currentItemCount)//check who has higher piority
                {
                    if(items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;//update the index to the right child
                    }
                }

                if(item.CompareTo(items[swapIndex]) < 0)
                {
                    swap(item, items[swapIndex]);
                }
                else
                {

                }
            }

        }
    }

    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;//(n - 1)/2 formula to get parent

        while(true)
        {
            T parentItem = items[parentIndex]; // the parent
            if(item.CompareTo(parentItem) > 0)
            {
                swap(item, parentItem);
            }
            else
            {
                break;
            }
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    void swap(T itemA, T itemB)
    {
        //swap the elements first
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        //thn swap the index
        int itemAIndex = itemA.HeapIndex;
        int itemBIndex = itemB.HeapIndex;
        itemA.HeapIndex = itemBIndex;
        itemB.HeapIndex = itemAIndex;
    }
	
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }

}
/*
               0
           1       2
       3     4   5     6
   7     8 9   10
     
    to get parent (n-1)/2
    to get left child 2n + 1
    to get right child 2n + 2 
 */
