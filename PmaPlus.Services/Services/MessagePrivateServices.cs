using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.MessagePrivates;
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

        public MessagePrivate AddMessage(int userId, int groupId, MessagePrivateViewModel msg, IList<int> usersInGroup = null)
        {
            if (msg != null)
            {
                if (groupId == 0 && usersInGroup != null)
                {
                    usersInGroup.Add(userId);
                    groupId = CreateMessageGroup(usersInGroup).MessageGroupId;
                }

              return _messagePrivateRepository.Add(new MessagePrivate()
              {
                  MessageGroupId = groupId,
                  UserId = userId,
                  Image = msg.Image,
                  SendAt = DateTime.Now,
                  Text = msg.Message
              });  
            }
            return null;
        }

        public bool IsGroupExist(int groupId)
        {
            return _messageGroupRepository.GetMany(x => x.MessageGroupId == groupId).Any();
        }

        public MessageGroup CreateMessageGroup(IList<int> usersInGroup)
        {
            return _messageGroupRepository.Add(new MessageGroup()
            {
                Users = _userRepository.GetMany(x => usersInGroup.Contains(x.Id)).ToList()
            });
        }

        public IQueryable<MessagePrivateViewModel> GetMessagePrivatesByGroupId(int groupId)
        {
            return _messagePrivateRepository.GetAll().Where(x => x.MessageGroupId == groupId).QueryMessagePrivateViewModel();
        } 

        public string RenameGroup(int groupId, string groupName)
        {
            var group = _messageGroupRepository.GetById(groupId);
            group.GroupName = groupName;
            _messageGroupRepository.Update(group);
            return groupName;
        }

        public IEnumerable<MessageGroupViewModel> GetGroupMessageForUser(int userId)
        {
            return _messageGroupRepository.GetAll()
                .Where(x => x.Users.Select(u => u.Id).Contains(userId)).ToList()
                .Select(v => new MessageGroupViewModel()
            {
                Id = v.MessageGroupId,
                GroupName = v.GroupName,
                Messages = _messagePrivateRepository.GetAll()
                .Where(g => g.MessageGroupId == v.MessageGroupId)
                .OrderBy(g => g.SendAt)
                .QueryMessagePrivateViewModel().ToList(),
                Users = _userRepository.GetAll()
                .Where(g => g.MessageGroups.Select(y =>y.MessageGroupId).Contains(v.MessageGroupId))
                .OrderBy(g => g.CreateAt)
                .QueryUsersList().ToList()
            });
        }
    }
}
