using System.Collections.Generic;
using UnityEngine;

namespace ORCAS
{
    [CreateAssetMenu(menuName = "ORCAS/Profiles/Resources")]
    public class ResourcesProfile : ScriptableObject
    {
        [System.Serializable]
        public struct ResourceScoreFactor
        {
            public Resource Resource;
            public float ScoreFactor;
        }

        public List<ResourceScoreFactor> Resources;
        public float defaultFactor = 1f;

        public float GetScoreFactor(Resource resource)
        {
            var resourceFactor = Resources.Find((t) => t.Resource == resource);
            return resourceFactor.Resource is null ? defaultFactor : resourceFactor.ScoreFactor;
        }
    }
}