using UnityEngine;

namespace RedBlobTutorial.HexGrid 
{ 
    public class HexGrid : MonoBehaviour
    {
        private float mRootThreeValue = 1.73205080757f;
        Vector2 HexCorner(Vector2 center, float size, int i, HexGridType hexGridType)
        {
            float angleInDeg = hexGridType == HexGridType.FLAT_TOP ? 60 * i : 60 * i - 30;
            float angleInRad = angleInDeg * Mathf.Deg2Rad;
            return new Vector2(center.x + size * Mathf.Cos(angleInRad), center.y + size * Mathf.Sin(angleInRad));
        }
        float Width(float size,HexGridType hexGridType) 
        {
            return hexGridType == HexGridType.FLAT_TOP ? 2 * size : mRootThreeValue * size;
        }
        float Height(float size, HexGridType hexGridType)
        {
            return hexGridType == HexGridType.FLAT_TOP ? mRootThreeValue * size : 2 * size;
        }
        float NeighbourCellDistanceInX(float size, HexGridType hexGridType)
        {
            float w = Width(size, hexGridType);
            return hexGridType == HexGridType.FLAT_TOP ? w : w * .75f;
        }
        float NeighbourCellDistanceInY(float size, HexGridType hexGridType)
        {
            float h = Height(size, hexGridType);
            return hexGridType == HexGridType.FLAT_TOP ? h*.75f : h;
        }
    }
    public enum HexGridType
    {
        FLAT_TOP,
        POINT_TOP
    }
}
