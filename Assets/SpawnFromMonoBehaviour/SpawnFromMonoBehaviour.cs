using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SpawnFromMonoBehaviour
{
    public class SpawnFromMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int countX;
        [SerializeField] private int countY;

        private void Start()
        {
            Entity entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, World.Active);
            EntityManager entityManager = World.Active.EntityManager;
            for (var y = 0; y < countY; y++)
            {
                for (var x = 0; x < countX; x++)
                {
                    Entity instance = entityManager.Instantiate(entityPrefab);
                    float3 position = transform.TransformPoint(new float3(x * 1.3f,
                        noise.cnoise(new float2(x, y) * 0.21f) * 2, y * 1.3f));
                    entityManager.SetComponentData(instance, new Translation {Value = position});
                }
            }
        }
    }
}