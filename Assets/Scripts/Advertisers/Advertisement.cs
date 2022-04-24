namespace ORCAS
{
    public struct Advertisement
    {
        public Task Task;
        public Reward Reward;

        public Advertisement(Task task, Reward reward)
        {
            Task = task;
            Reward = reward;
        }

        public void Deconstruct(out Task task, out Reward reward)
        {
            task = Task;
            reward = Reward;
        }
    }
}
