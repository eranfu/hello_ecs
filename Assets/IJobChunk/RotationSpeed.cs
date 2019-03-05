using System;
using Unity.Entities;

namespace IJobChunk
{
    [Serializable]
    public struct RotationSpeed : IComponentData
    {
        public float radiansPerSecond;
    }
}