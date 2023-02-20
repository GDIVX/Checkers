using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a scriptable object that holds data for the renderer
/// </summary>
[CreateAssetMenu(fileName = "RendererData", menuName = "ScriptableObjects/RendererData", order = 1)]
public class RendererData : ScriptableObject
{
    [SerializeField] List<TileRenderDefinition> definitions;

    public List<TileRenderDefinition> Definitions => definitions;
}

[System.Serializable]
public class TileRenderDefinition
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject _prefab;

    public string Name => _name;
    public GameObject Prefab => _prefab;
}

