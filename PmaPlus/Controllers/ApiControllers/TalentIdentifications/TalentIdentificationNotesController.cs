using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Skill;
using PmaPlus.Model.ViewModels.TalentIdentifications;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers.TalentIdentifications
{
    public class TalentIdentificationNotesController : ApiController
    {
        private readonly TalentServices _talentServices;

        public TalentIdentificationNotesController(TalentServices talentServices)
        {
            _talentServices = talentServices;
        }

      



        [Route("api/TalentIdentificationNotes/{talentId:int}/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
        public TalentIdentificationNotesPage  Get(int talentId, int pageSize, int pageNumber, string orderBy = "", bool direction = true)
        {

            var count = _talentServices.GetTalentNotes(talentId).Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var notes = _talentServices.GetTalentNotes(talentId);
            var items = Mapper.Map<IEnumerable<TalentNote>, IEnumerable<TalentNoteViewModel>>(notes).OrderQuery(orderBy, x => x.AddDate, direction).Paged(pageNumber, pageSize);

            return new TalentIdentificationNotesPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };
        }



        public TalentNoteViewModel Get(int id)
        {
            return Mapper.Map<TalentNote,TalentNoteViewModel>(_talentServices.GetTalentNoteById(id));
        }




        public IHttpActionResult Post([FromBody]TalentNoteViewModel noteViewModel)
        {
            var note = Mapper.Map<TalentNoteViewModel, TalentNote>(noteViewModel);

            note.AddDate = DateTime.Now;
            _talentServices.AddTalentNote(note);
            return Ok();
        }


        public IHttpActionResult Put(int id, [FromBody]TalentNoteViewModel noteViewModel)
        {
            if (!_talentServices.NoteExist(id))
            {
                return NotFound();
            }
            var note = Mapper.Map<TalentNoteViewModel, TalentNote>(noteViewModel);
            note.Id = id;
            _talentServices.UpdateTalentNote(note);
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if (!_talentServices.NoteExist(id))
            {
                return NotFound();
            }

            _talentServices.DeleteTalentNote(id);
            return Ok();
        }

    }
}
