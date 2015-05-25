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

        [Route("api/CurriculumStatement/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public CurriculumStatementPage Get(int pageSize, int pageNumber, string orderBy = "")
        {

            var clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id;


            var count = _curriculumServices.GetCurriculumStatements(clubId).Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var curriculumStatements = _curriculumServices.GetCurriculumStatements(clubId).OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize);
            var items = Mapper.Map<IEnumerable<CurriculumStatement>, IEnumerable<CurriculumStatementViewModel>>(curriculumStatements);

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
                    _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id);
            return Mapper.Map<IEnumerable<CurriculumStatement>, IEnumerable<CurriculumStatementViewModel>>(statements);
        }


        public CurriculumStatementViewModel Get(int id)
        {
            return
                Mapper.Map<CurriculumStatement, CurriculumStatementViewModel>(
                    _curriculumServices.GetCurriculumStatementById(id));

        }

        public IHttpActionResult Post(CurriculumStatementViewModel statementViewModel)
        {
            var statement = Mapper.Map<CurriculumStatementViewModel, CurriculumStatement>(statementViewModel);

            _curriculumServices.AddCurricululmStatment(statement, statementViewModel.Roles, _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id);
            return Ok();

        }

        public IHttpActionResult Put(int id, [FromBody] CurriculumStatementViewModel statementViewModel)
        {
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
