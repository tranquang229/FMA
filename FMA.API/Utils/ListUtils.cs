namespace FMA.API.Utils;

public static class ListUtils<T>
{
    public static bool CheckListContain(IEnumerable<T> parent, IEnumerable<T> child)
    {
        return child.All(x => parent.Contains(x));
    }
}