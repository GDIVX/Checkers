using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This is a scriptable object that holds data for the renderer
/// </summary>
[CreateAssetMenu(fileName = "RendererData", menuName = "ScriptableObjects/RendererData", order = 1)]
public class RendererData : ScriptableObject
{
    [SerializeField] Vector2 offset;
    [SerializeField] List<TileRenderDefinition> definitions;

    public Vector2 Offset { get => offset; set => offset = value; }

    public GameObject GetPrefab(string name)
    {
        return definitions.First(def => def.Name == name).Prefab;
    }
}

[System.Serializable]
public class TileRenderDefinition
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject _prefab;

    public string Name => _name;
    public GameObject Prefab => _prefab;
}

