namespace MonoForge;

public interface IDeepCopyable<out T>
{
    public T DeepCopy();
}