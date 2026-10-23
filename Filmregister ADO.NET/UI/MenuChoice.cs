namespace Filmregister_ADO.NET.UI
{
    internal class MenuChoice<T>
    {
        public T Value { get; }
        public string Text { get; }
        internal MenuChoice(T value, string text)
        {
            Value = value;
            Text = text;
        }
    }
}