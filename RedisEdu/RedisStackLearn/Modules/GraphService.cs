using StackExchange.Redis;
using StackExchange.Redis.Extensions;
namespace RedisStackLearn.Modules
{
    public class GraphService
    {
        //RedisGraph — это модуль Redis, который позволяет хранить и выполнять графовые запросы с помощью языка Cypher
        //Нужно поставить библу, потом посмотреть какую
        //Используется для:
        //Социальных сетей
        //Рекомендательных систем
        //Иерархий, зависимостей
        //Схем знаний

        //Нет "транзакций" как в RDBMS
        //Граф целиком хранится в памяти
        //Не поддерживает сложные JOIN'ы между разными графами
        public async Task Example()
        {
            //var redis = await ConnectionMultiplexer.ConnectAsync("localhost:6379");
            //var db = redis.GetDatabase();
            //var graph = db.GRAPH();

            // 1. Создание графа и добавление узлов и связей
            //string createQuery = @"s
   // CREATE (:Person {name: 'Alice'})-[:KNOWS]->(:Person {name: 'Bob'})";
            //await graph.QueryAsync("social", createQuery);

            // 2. Чтение данных из графа
            //var result = await graph.QueryAsync("social", @"
    //MATCH (a:Person)-[:KNOWS]->(b:Person)
    //RETURN a.name, b.name");

           // foreach (var row in result.Values)
           // {
           //     Console.WriteLine($"{row[0]} knows {row[1]}");
           // }
        }
    }
}
