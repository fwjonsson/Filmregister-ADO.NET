namespace Filmregister_ADO.NET.UI
{
    internal class MenuResult<T>
    {
        public bool WasCancelled { get; }
        public T? Value { get; }
        private MenuResult(bool wasCancelled, T? value)
        {
            WasCancelled = wasCancelled;
            Value = value;
        }
        internal static MenuResult<T> Cancelled() => new MenuResult<T>(true, default);
        internal static MenuResult<T> Selected(T value) => new MenuResult<T>(false, value);
        
    }
}