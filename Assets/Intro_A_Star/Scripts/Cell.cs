
using UnityEngine;


public class Cell : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Cell leftCell;
    public Cell rightCell;
    public Cell topCell;
    public Cell bottomCell;
    public bool isDiscovered;
    public bool isWall;
    public BFS mBFS;
    public TextMesh textMesh;
    
    public void SetColor(Color c)
    {
        spriteRenderer.color = c;
    }
    public void SetCounter(int i)
    {
        textMesh.text = i.ToString();
    }
    private void OnMouseDown() {
        if(!isWall)
        mBFS.OnCustomMouseDown(this);
    }
    
}
