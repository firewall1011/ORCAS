using System.Collections.Generic;
using ORCAS.Advertisement;

namespace ORCAS
{
    public interface IAgentAI
    {
        bool SelectTaskSequence(Agent agent, List<TaskSequence> availableAdvertisements);
    }
}