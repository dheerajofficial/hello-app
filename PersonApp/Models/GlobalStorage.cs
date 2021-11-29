using System.Collections.Concurrent;

namespace PersonApp.Models;

public static class GlobalStorage
{
    static GlobalStorage()
    {
        PersonStorage = new ConcurrentDictionary<Guid, Person>();
    }
    public static ConcurrentDictionary<Guid, Person> PersonStorage { get; set; }
}