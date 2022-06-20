namespace ORCAS.Advertisement
{
    public interface IAdvertiser
    {
        TaskSequence[] AdvertiseTasksFor(Agent agent);
        string GetTag();
    }
}
