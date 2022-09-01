namespace ASK.UI.Libs
{
    public class UIManager
    {
        public static int GetIntField(NumericUpDown nud)
        {
            return (int)nud.Value;
        }

        public static int GetIntLable(Label lb)
        {
            return Int32.Parse(lb.Text.Trim());
        }

        public static decimal ReadDec(TextBox text)
        {
            return decimal.Parse(text.Text.Trim());
        }

        public static int ReadInt(TextBox text)
        {
            return Int32.Parse(text.Text.Trim());
        }
    }
}