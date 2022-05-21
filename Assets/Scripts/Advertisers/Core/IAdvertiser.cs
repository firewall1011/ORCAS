namespace ORCAS
{
    public interface IAdvertiser
    {
        TaskSequence[] AdvertiseTasksFor(Agent agent);
        string GetTag();
    }
}
