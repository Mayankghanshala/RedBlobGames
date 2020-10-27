using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class BFS : MonoBehaviour
{
    private Cell [,] mCells ;
    [SerializeField] int xSize = 5;
    [SerializeField] int ySize = 5;
    [SerializeField] int wallNo = 4;
    [SerializeField] GameObject mNodePrefab;
    [SerializeField] GameObject mParent;
    private bool mBFStarted = false;
    private void Awake() {
        GenerateNodesRandomaly();
    }
   void GenerateNodesRandomaly()
   {
       mCells = new Cell[ySize,xSize];
       int currentWall = 0;
       for(int i =0;i<ySize;i++)
       {
           for(int j = 0;j<xSize;j++)
           {
            //    Node node = new Node{
            //        IsDiscovered = false,
            //        IsWall = UnityEngine.Random.Range(0,2)==0?false:true,
            //        LeftNode = j>0?mCells[i,j-1].node:null,
            //        BottomNode = i>0?mCells[i-1,j].node:null
            //    };
            //    if(node.LeftNode!=null) node.LeftNode.RightNode = node;
            //    if(node.BottomNode!=null) node.BottomNode.TopNode = node;
            //    if(node.IsWall)
            //    {
            //        if(currentWall<wallNo) currentWall++;
            //        else node.IsWall = false;
            //    }
            currentWall = DrawNodeOnUI(j,i,currentWall);
           }
       }
   }
   int DrawNodeOnUI(int x,int y,int currentWall)
   {
        Vector3 pos = new Vector3(x,y,0);
        GameObject cellObj = Instantiate<GameObject>(mNodePrefab);
        cellObj.transform.localPosition = GetPos(x,y);
        cellObj.transform.SetParent(mParent.transform,true);
        
        var cell = cellObj.GetComponent<Cell>();
        cell.mBFS = this;
        cell.isDiscovered = false;
        cell.isWall = UnityEngine.Random.Range(0,2)==0?false:true;
        cell.leftCell = x>0?mCells[y,x-1]:null;
        cell.bottomCell = y>0?mCells[y-1,x]:null;

        if(cell.leftCell!=null) cell.leftCell.rightCell = cell;
        if(cell.bottomCell!=null) cell.bottomCell.topCell = cell;

        if(cell.isWall)
        {
            if(currentWall<wallNo) currentWall++;
            else cell.isWall = false;
        }

        mCells[y,x] = cell;
        if(cell.isWall)cell.SetColor(Color.blue);
        else cell.SetColor(Color.white);
        return currentWall;
   }
   Vector3 GetPos(int i,int j)
   {
       var x = xSize%2!=0? i-xSize/2:i-xSize/2+.5f;
       var y = ySize%2!=0? j-ySize/2:j-ySize/2+.5f;
       return new Vector3(x,y,0);
   }
   public void OnCustomMouseDown(Cell cell)
   {
       if(mBFStarted) return;
       mBFStarted = true;
       StartBFS(cell);
   }
   void StartBFS(Cell cell)
   {
       StartCoroutine(StartBFSCo(cell));
   }
   IEnumerator StartBFSCo(Cell cell)
   {
       yield return null;
       Queue<Cell> cells = new Queue<Cell>();
       cells.Enqueue(cell);
       cell.isDiscovered = true;
       int counter = 0;
       while(cells.Count>0)
       {
            Cell current = cells.Dequeue();
            current.SetColor(Color.red);
            current.SetCounter(counter);
            counter++;
            if(IsDiscoverable(current.topCell)) 
            {
                cells.Enqueue(current.topCell);
                current.topCell.isDiscovered = true;
            }
               
            if(IsDiscoverable(current.leftCell))
            {
                cells.Enqueue(current.leftCell);
                current.leftCell.isDiscovered = true;  
            } 
            if(IsDiscoverable(current.bottomCell)) 
            {
               cells.Enqueue(current.bottomCell);
               current.bottomCell.isDiscovered = true;
            }
            if(IsDiscoverable(current.rightCell)) 
            {
               cells.Enqueue(current.rightCell);
               current.rightCell.isDiscovered = true;
            }
            yield return new WaitForSeconds(.1f);
       }
   }
    bool IsDiscoverable(Cell cell)
    {
        return cell!=null && !cell.isWall && !cell.isDiscovered;
    }
//    Tuple<int,int> GetIndexOfCell(Cell cell)
//    {
//        var index = new Tuple<int, int>(-1,-1);
//        for(int i =0;i<ySize;i++)
//        {
//            for(int j=0;j<xSize;j++)
//            {
//                if(cell.gameObject.Equals(mCells[i,j].gameObject))
//                {
//                    index = new Tuple<int, int>(j,i);
//                    break;
//                }
//            }
//        }
//        return index;
//    }
}
