namespace CompleX.Presentation.Controls.interfaces
{
    public interface IProgressDialog
    {
        string DescriptionText { get; set; }
        double ProgressValue { get; set; }
        double Maximum { get; set; }
        bool IsIndeterminate { get; set; }
    }
}
