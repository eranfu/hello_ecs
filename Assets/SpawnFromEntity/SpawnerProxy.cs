using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SpawnFromEntity
{
    public class SpawnerProxy : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int countX;
        [SerializeField] private int countY;

        public void DeclareReferencedPrefabs(List<GameObject> gameObjects)
        {
            gameObjects.Add(prefab);
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new Spawner
            {
                countX = countX,
                countY = countY,
                prefab = conversionSystem.GetPrimaryEntity(prefab)
            });
        }
    }
}