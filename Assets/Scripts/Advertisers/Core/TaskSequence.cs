using ORCAS.Tasks;

namespace ORCAS.Advertisement
{
    public struct TaskSequence
    {
        public readonly string TaskName;
        public readonly Task[] Tasks;
        public readonly IRewardable[] Rewards;

        public TaskSequence(Task[] tasks, IRewardable[] rewards, string taskName = "")
        {
            Tasks = tasks;
            Rewards = rewards;
            TaskName = taskName;
        }

        public static TaskSequence EmptySequence => new TaskSequence(new Task[0], new IRewardable[0], "");
    }
}
