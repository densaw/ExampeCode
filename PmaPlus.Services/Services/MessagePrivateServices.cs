using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.MessagePrivate;
using PmaPlus.Services.Extensions;

namespace PmaPlus.Services
{
    public class MessagePrivateServices
    {
        private readonly IMessageGroupRepository _messageGroupRepository;
        private readonly IMessagePrivateRepository _messagePrivateRepository;
        private readonly IUserRepository _userRepository;

        public MessagePrivateServices(IMessagePrivateRepository messagePrivateRepository, IMessageGroupRepository messageGroupRepository, IUserRepository userRepository)
        {
            _messagePrivateRepository = messagePrivateRepository;
            _messageGroupRepository = messageGroupRepository;
            _userRepository = userRepository;
        }

        public MessagePrivate AddMessage()
        {
            var msg = new MessagePrivate();

            return _messagePrivateRepository.Add(msg);
        }

        public IQueryable<MessageGroupViewModel> GetGroupMessageForUser(int userId)
        {
            return _messageGroupRepository.GetAll()
                .Where(x => x.Users.Select(u => u.Id).Contains(userId))
                .Select(v => new MessageGroupViewModel()
            {
                Id = v.MessageGroupId,
                GroupName = v.GroupName,
                Messages = _messagePrivateRepository.GetAll()
                .Where(g => g.MessageGroupId == v.MessageGroupId)
                .OrderBy(g => g.SendAt)
                .QueryMessagePrivateViewModel(),
                Users = _userRepository.GetAll()
                .Where(g => g.MessageGroups.Select(y =>y.MessageGroupId).Contains(v.MessageGroupId))
                .OrderBy(g => g.CreateAt)
                .QueryUsersList()
            });
        }
    }
}
