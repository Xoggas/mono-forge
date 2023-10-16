namespace System.Collections.Generic;

//TODO: Get rid of this type
public class GenericDictionary<TKey> : Dictionary<TKey, object> where TKey : notnull
{
    public bool TryGet<T>(TKey key, out T? value) where T : notnull
    {
        if (TryGetValue(key, out var result) && result is T castedValue)
        {
            value = castedValue;
            return true;
        }

        value = default;
        return false;
    }

    public bool TryAdd<T>(TKey key, T value) where T : notnull
    {
        return base.TryAdd(key, value);
    }
}
