namespace MonoGine;

public interface IDeepCopyable<out T>
{
    public T DeepCopy();
}