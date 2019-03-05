using System;
using Unity.Entities;

namespace IJobProcessComponentData
{
    [Serializable]
    public struct RotationSpeed : IComponentData
    {
        public float radiansPerSecond;
    }
}