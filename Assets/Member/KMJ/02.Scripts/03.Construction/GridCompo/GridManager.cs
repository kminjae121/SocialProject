using UnityEngine;

namespace Member.KMJ._02.Scripts._03.Construction.GridCompo
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Grid gridCompo;

        public Vector3Int GetCellPos(Transform trm)
        {
            return gridCompo.WorldToCell(trm.position);
        }
    }
}