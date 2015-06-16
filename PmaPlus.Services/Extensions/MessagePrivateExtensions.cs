using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.MessagePrivates;

namespace PmaPlus.Services.Extensions
{
    public static class MessagePrivateExtensions
    {
        public static IQueryable<MessagePrivateViewModel> QueryMessagePrivateViewModel(this IQueryable<MessagePrivate> messagePrivate)
        {
            if (messagePrivate != null)
            {
                return messagePrivate.Select(x =>  new MessagePrivateViewModel
                {
                    Id = x.Id,
                    Image = x.Image,
                    Message = x.Text,
                    SendAt = x.SendAt,
                    UserId = x.UserId,
                    UserAva = "/api/file/ProfilePicture/" + x.User.UserDetail.ProfilePicture + "/" + x.User.Id
                });
            }
            return null;
        }
    }
}
