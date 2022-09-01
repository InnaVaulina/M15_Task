namespace M13_Library
{
    public interface IVariantCov<out T>
    {
        T Account { get; }

    }

    public class VariantCov<T> : IVariantCov<T>

    {
        public T account;
        public VariantCov(T x) { account = x; }
        public T Account { get { return this.account; } }

    }
}
