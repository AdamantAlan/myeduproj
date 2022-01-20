using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AdelMobileBackEnd.models.absFactoryOfBook.products;
namespace AdelMobileBackEnd.models
{

    public static class JsonAsync
    {

       public static async Task<IBook> DeserializeOfFileAsync<T>()
        {
            try
            {
                using (FileStream fs = new FileStream($"state/{typeof(T).Name}.json", FileMode.OpenOrCreate))
                {
                  T json = await JsonSerializer.DeserializeAsync<T>(fs);
                    return (IBook)json;
                }
            }
            catch (AggregateException exs)
            {
                foreach (var e in exs.InnerExceptions)
                    await Log.LoggingAsync(e, "DeserializeOfFileAsync");
                return null;
            }
        }

        public static async Task<string> SerializeForFileAsync<T>(IBook Json)
        {
            try
            {
                if (Json == null)
                    return "Bad serialize, string is null or empty - SerializeForFile(string noJson)";
                using (FileStream fs = new FileStream($"state/{typeof(T).Name}.json", FileMode.OpenOrCreate))
                    await JsonSerializer.SerializeAsync<T>(fs, (T)Json);
                return "Serializeble successful";  //Good result
            }
            catch (AggregateException exs)
            {
                foreach (var e in exs.InnerExceptions)
                    await Log.LoggingAsync(e, "SerializeForFileAsync");
                return null;
            }
        }
    }
}
