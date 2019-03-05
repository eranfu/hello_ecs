using System;
using Unity.Entities;

namespace ForEach
{
    [Serializable]
    public struct RotationSpeed : IComponentData
    {
        public float radiansPerSecond;
    }
}