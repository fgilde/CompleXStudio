namespace CompleX.Presentation.Controls.interfaces
{
    public interface IWorkerThread
    {
        /// <summary>
        /// Sets the maximum.
        /// </summary>
        /// <param name="max">The max.</param>
        void SetMaximum(int max);
        /// <summary>
        /// Reports the progress.
        /// </summary>
        /// <param name="percentProgress">The percent progress.</param>
        /// <param name="state">The state.</param>
        void ReportProgress(int percentProgress, object state);
        /// <summary>
        /// Increments the step.
        /// </summary>
        /// <param name="state">The state.</param>
        void IncrementStep(object state);
    }
}