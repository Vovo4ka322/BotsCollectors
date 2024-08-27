using System.Collections.Generic;
using UnityEngine;

public class ResourceViewerHolder : MonoBehaviour
{
    [SerializeField] public List<BaseRecourcesViewer> _baseRecourcesViewers;

    public IReadOnlyList<BaseRecourcesViewer> BaseRecourcesViewers => _baseRecourcesViewers;
}
