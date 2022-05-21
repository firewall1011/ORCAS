using System.Collections.Generic;

namespace ORCAS
{
    public interface IAdvertisementSelector
    {
        TaskSequence Select(Agent agent, float[] scores, List<TaskSequence> advertisements);
    }
}
