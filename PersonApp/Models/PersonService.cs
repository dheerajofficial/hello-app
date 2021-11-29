using System.Text.Json;
namespace PersonApp.Models;

public static class PersonService
{
    private const string FILE_NAME = "../PersonApp/Data/persons.json";
    //Create
    public static Person Create(Person newRecord)
    {
        GlobalStorage.PersonStorage.TryAdd(newRecord.Id, newRecord);
        return newRecord;
    }

    //Read
    public static List<Person> Get()
    {
        List<Person> p = new List<Person>();
        if (GlobalStorage.PersonStorage != null)
        {
            foreach (var item in GlobalStorage.PersonStorage)
            {
                p.Add(item.Value);
            }
        }
        return p;
    }
    public static Person GetById(Guid id)
    {
        Person result = new Person();
        if (GlobalStorage.PersonStorage != null)
        {
            foreach (var item in GlobalStorage.PersonStorage)
            {
                if (item.Key == id)
                {
                    result = item.Value;
                    break;
                }
            }
        }
        return result;
    }

    public static async Task<bool> SaveToJsonFile()
    {
        var person = Get();

        using (FileStream fs = new FileStream(FILE_NAME, FileMode.Create))
        {
            await JsonSerializer.SerializeAsync(fs, person);
        }
        return true;
    }

    public static List<Person> ReadFromJsonFile()
    {
        var json = File.ReadAllText(FILE_NAME);
        if(!string.IsNullOrEmpty(json))
        {
            var persons = JsonSerializer.Deserialize<List<Person>>(json);
            foreach (Person p in persons)
                Create(p);

            return persons;
        }
        return null;
    }
}