using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Curriculum;
using PmaPlus.Services;
using PmaPlus.Data;

namespace PmaPlus.Controllers.ApiControllers.Curriculums
{
    public class CurriculumStatementController : ApiController
    {
        private readonly CurriculumServices _curriculumServices;
        private readonly UserServices _userServices;

        public CurriculumStatementController(CurriculumServices curriculumServices, UserServices userServices)
        {
            _curriculumServices = curriculumServices;
            _userServices = userServices;
        }

        [Route("api/CurriculumStatement/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
        public CurriculumStatementPage Get(int pageSize, int pageNumber, string orderBy = "",bool direction = false)
        {

            var clubId = _userServices.GetClubByUserName(User.Identity.Name).Id;


            var count = _curriculumServices.GetCurriculumStatements(clubId).Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var curriculumStatements = _curriculumServices.GetCurriculumStatements(clubId);
            var items = Mapper.Map<IEnumerable<CurriculumStatement>, IEnumerable<CurriculumStatementViewModel>>(curriculumStatements).OrderQuery(orderBy, x => x.Id, direction).Paged(pageNumber, pageSize);

            return new CurriculumStatementPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }



        public IEnumerable<CurriculumStatementViewModel> Get()
        {
            var statements =
                _curriculumServices.GetCurriculumStatements(
                    _userServices.GetClubByUserName(User.Identity.Name).Id);
            return Mapper.Map<IEnumerable<CurriculumStatement>, IEnumerable<CurriculumStatementViewModel>>(statements);
        }


        public CurriculumStatementViewModel Get(int id)
        {
            return
                Mapper.Map<CurriculumStatement, CurriculumStatementViewModel>(
                    _curriculumServices.GetCurriculumStatementById(id));
        }


        [Route("api/CurriculumStatement/List")]
        public IEnumerable<CurriculumStatementViewModel> GetList()
        {
            var user = _userServices.GetUserByEmail(User.Identity.Name);

            var statements =
                _curriculumServices.GetCurriculumStatements(
                    _userServices.GetClubByUserName(User.Identity.Name).Id)
                    .Where(s => s.Roles.Select(r => r.Role).Contains(user.Role));
            return Mapper.Map<IEnumerable<CurriculumStatement>, IEnumerable<CurriculumStatementViewModel>>(statements);
        }

        public IHttpActionResult Post(CurriculumStatementViewModel statementViewModel)
        {
            var statement = Mapper.Map<CurriculumStatementViewModel, CurriculumStatement>(statementViewModel);

            _curriculumServices.AddCurricululmStatment(statement, statementViewModel.Roles, _userServices.GetClubByUserName(User.Identity.Name).Id);
            return Ok();

        }

        public IHttpActionResult Put(int id, [FromBody] CurriculumStatementViewModel statementViewModel)
        {
            if (!_curriculumServices.StatementExist(id))
            {
                return NotFound();
            }
            var statement = Mapper.Map<CurriculumStatementViewModel, CurriculumStatement>(statementViewModel);

            _curriculumServices.UpdateCurriculumStatment(statement, statementViewModel.Roles, id);
            return Ok();
        }




        public IHttpActionResult Delete(int id)
        {
            if (!_curriculumServices.StatementExist(id))
            {
                return NotFound();
            }
            _curriculumServices.DeleteCurriculumStatement(id);
            return Ok();
        }

    }
}
