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


        public IEnumerable<CurriculumStatementViewModel> Get()
        {
            var statements =
                _curriculumServices.GetCurriculumStatements(
                    _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id);
            return Mapper.Map<IEnumerable<CurriculumStatement>, IEnumerable<CurriculumStatementViewModel>>(statements);
        }


        public IHttpActionResult Post(CurriculumStatementViewModel statementViewModel)
        {
            var statement = Mapper.Map<CurriculumStatementViewModel, CurriculumStatement>(statementViewModel);

            _curriculumServices.AddCurricululmStatment(statement, statementViewModel.Roles, _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id);
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
