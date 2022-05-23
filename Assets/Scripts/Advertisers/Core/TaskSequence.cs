namespace ORCAS
{
    public struct TaskSequence
    {
        public readonly Task[] Tasks;
        public readonly IRewardable[] Rewards;

        public TaskSequence(Task[] tasks, IRewardable[] rewards)
        {
            Tasks = tasks;
            Rewards = rewards;
        }

        public static TaskSequence EmptySequence => new TaskSequence(new Task[0], new IRewardable[0]);
    }
}
