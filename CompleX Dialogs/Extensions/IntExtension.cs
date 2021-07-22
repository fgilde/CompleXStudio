namespace CompleX.Presentation.Controls.Extensions
{
    public static class IntExtension
    {
        public static string Zeroize(this int i)
        {
            return i <= 9 ? "0" + i : i.ToString();
        }

        public static int Percentage(this int actualProgress, int maximum)
        {
            return ((100 * actualProgress) / maximum);            
        }
    }
}
