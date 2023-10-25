namespace MonoGine.Animations;

public interface IAnimatable
{
    public string? Name { get; set; }
    public IAnimatable? FindChildByName(string name);
    public void SetProperty(string name, float value);
}