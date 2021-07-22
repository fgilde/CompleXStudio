namespace CompleX.Presentation.Controls.interfaces
{
    public interface IStaticDialog
    {
        void UpdateDialog(IDialogDescription description);
        void DisplayDialog();
        void CloseDialog();
    }
}