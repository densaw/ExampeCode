using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.ToDo;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers
{
    public class ToDoController : ApiController
    {
        private readonly ToDoServices _toDoServices;

        public ToDoController(ToDoServices toDoServices)
        {
            _toDoServices = toDoServices;
        }

        public IEnumerable<ToDoViewModel> Get()
        {
            return Mapper.Map<IEnumerable<ToDo>,IEnumerable<ToDoViewModel>>( _toDoServices.GetToDos(User.Identity.Name));
        }

        public IHttpActionResult Post(ToDoViewModel toDoViewModel)
        {
            _toDoServices.AddToDo(Mapper.Map<ToDoViewModel, ToDo>(toDoViewModel),User.Identity.Name);
            return Ok();
        }

        public IHttpActionResult Put(ToDoViewModel toDoViewModel, int id)
        {
            if (!_toDoServices.ToDoExist(id))
            {
                return NotFound();
            }
            _toDoServices.UpdateToDo(Mapper.Map<ToDoViewModel,ToDo>(toDoViewModel),id);
            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            if (!_toDoServices.ToDoExist(id))
            {
                return NotFound();
            }
            _toDoServices.DeleteToDo(id);
            return Ok();
        }

    }
}
