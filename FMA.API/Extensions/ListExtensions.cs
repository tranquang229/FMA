namespace FMA.API.Extensions;

public static class ListExtensions<T>
{
    public static bool CheckListContain(IEnumerable<T> parent, IEnumerable<T> child)
    {
        return child.All(x => parent.Contains(x));
    }
}