namespace ORCAS.Tasks
{
    public struct CancellationToken
    {
        public bool IsCancellationRequested => _isCancelled;
        public bool RequestCancellation() => _isCancelled = true;

        private bool _isCancelled;

        public CancellationToken(bool isCancelled)
        {
            _isCancelled = isCancelled;
        }
    }
}
