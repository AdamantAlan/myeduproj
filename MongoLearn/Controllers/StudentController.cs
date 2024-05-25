using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoLearn.Entity;

namespace MongoLearn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMongoCollection<Student> _students;

        public StudentController(MongoClient mongoClient)
        {
            _students = mongoClient.GetDatabase("tdata").GetCollection<Student>("students");
        }

        [HttpGet]
        public IActionResult GetAll() =>
            Ok(_students.Find("{}").ToList());
        
        [HttpGet]
        public IActionResult GetFor(string id) =>
            Ok(_students.Find(p => p.Id == id).FirstOrDefault());
        
        [HttpGet]
        public IActionResult GetDtoFor(string id)
        {
            var projection = Builders<Student>.Projection.Exclude("_id").Exclude("Course");
            return Ok(_students.Find(p => p.Id == id).Project<Student>(projection).FirstOrDefault());
        }
        
        [HttpGet]
        public IActionResult GetStatistic()
        {
            var statistic = _students.Aggregate()
                .Group(s => s.Course!.Name,
                    g => new
                    {
                        courseName = g.Key,
                        CountStudents = g.Count(),
                        AgeAvg = g.Select(s => s.Age).Average()
                    }).ToList();
            
            return Ok(statistic);
        }

        
        [HttpDelete]
        public IActionResult Delete(string id) =>
            Ok(_students.FindOneAndDelete(p => p.Id == id));

        [HttpPost]
        public IActionResult Insert(Student student)
        {
            student.Id = Guid.NewGuid().ToString();
            _students.InsertOne(student);
            return Ok();
        }
        
        [HttpPost]
        public IActionResult Update(Student student) => 
            Ok( _students.FindOneAndReplace(p => p.Id == student.Id, student ,
                new() { ReturnDocument = ReturnDocument.After }));
    }
}
