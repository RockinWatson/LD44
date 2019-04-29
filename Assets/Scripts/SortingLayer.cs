using UnityEngine;

namespace Assets.Scripts
{
    public class SortingLayer : MonoBehaviour
    {
        public string SortingLayerName;
        public int SortingOrder;

        private void Awake()
        {
            gameObject.GetComponent<MeshRenderer>().sortingLayerName = SortingLayerName;
            gameObject.GetComponent<MeshRenderer>().sortingOrder = SortingOrder;
        }
    }
}
