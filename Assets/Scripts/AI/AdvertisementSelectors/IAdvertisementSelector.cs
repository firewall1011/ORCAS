using System.Collections.Generic;
using ORCAS.Advertisement;

namespace ORCAS
{
    public interface IAdvertisementSelector
    {
        TaskSequence Select(Agent agent, float[] scores, List<TaskSequence> advertisements);
    }
}
