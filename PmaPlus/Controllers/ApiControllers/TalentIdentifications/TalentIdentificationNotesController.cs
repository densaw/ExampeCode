using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Model.Models;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Talent Id</param>
        /// <returns></returns>
        public IEnumerable<TalentNoteViewModel> Get(int id)
        {
            return Mapper.Map<IEnumerable<TalentNote>,IEnumerable<TalentNoteViewModel>>(_talentServices.GetTalentNotes(id));
        }

        public IHttpActionResult Post(int id, [FromBody]TalentNoteViewModel noteViewModel)
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
