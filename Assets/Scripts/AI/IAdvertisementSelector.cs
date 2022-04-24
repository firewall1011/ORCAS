using System.Collections.Generic;

namespace ORCAS
{
    public interface IAdvertisementSelector
    {
        Advertisement[] Select(Agent agent, float[] scores, List<Advertisement[]> advertisements);
    }
}
