using Unity.Entities;

namespace SpawnFromEntity
{
    public struct Spawner : IComponentData
    {
        public Entity prefab;
        public int countX;
        public int countY;
    }
}