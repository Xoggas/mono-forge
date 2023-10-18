namespace MonoGine.Animations;

public interface IAnimatable
{
    public float this[string propertyName] { set; }
}