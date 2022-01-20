using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Models;
using TestAPI.Data;
using AutoMapper;
using TestAPI.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace TestAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repo;
        private readonly IMapper _map;
        public CommandsController(ICommanderRepo irepo, IMapper map)
        {
            _repo = irepo;
            _map = map;
        }
       
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands() {
           
            return Ok(_map.Map<IEnumerable<CommandReadDto>>( _repo.GetAppComands()));
        }

        [HttpGet("{id}", Name ="GetForId")]
        public ActionResult<CommandReadDto> GetAllCommands([FromRoute] int id){
            var commandItem = _repo.GetCommandById(id);
            if (commandItem !=  null)
                return Ok(_map.Map<CommandReadDto>(commandItem));
            else
                return NotFound();
        }

        //POST api/v1/Commands
        [HttpPost]
        public ActionResult<CommandReadDto> PostCreateCommand([FromBody] CommandCreateDto cmd)
        {
           var command = _map.Map<Command>(cmd);
            _repo.CreateCommand(command);
            _repo.SaveChanges();
            //  return Ok(_map.Map<CommandReadDto>(command));
            return Created("GetForId", _map.Map<CommandReadDto>(command));
        }

        [HttpPut("{id}")]
        public ActionResult<CommandUpdateDto> PutCommandForId(int id, CommandUpdateDto updateDto)
        {
            var commandModelFromRepo = _repo.GetCommandById(id);
            if (commandModelFromRepo == null)
                return NotFound();
            else
            {
                _map.Map(updateDto,commandModelFromRepo);
                _repo.UpdateCommand(commandModelFromRepo);
                _repo.SaveChanges();
                return NoContent();
            }
        }

        //PATCH 
        [HttpPatch("{id}")]
        public ActionResult PatchPartialUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repo.GetCommandById(id);
            if (commandModelFromRepo == null)
                return NotFound();
            var cmdToPatch = _map.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(cmdToPatch, ModelState);
            if (!TryValidateModel(ModelState))
                return ValidationProblem(ModelState);
            _map.Map(cmdToPatch,commandModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }


        //Delete
        [HttpDelete("{id}")]
        public ActionResult DeleteComman(int id)
        {
            var commandModelFromRepo = _repo.GetCommandById(id);
            if (commandModelFromRepo == null)
                return NotFound();
            _repo.DeleteCommand(commandModelFromRepo);
            _repo.SaveChanges();
            return Ok();
        }


    }
}

