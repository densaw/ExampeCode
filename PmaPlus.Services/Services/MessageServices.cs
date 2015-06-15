using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.MessageWall;

namespace PmaPlus.Services
{
    public class MessageServices
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRatingRepository _messageRatingRepository;
        private readonly IMessageCommentRepository _messageCommentRepository;

        private const int PageSize = 20;

        public MessageServices(
            IMessageRepository messageRepository,
            IUserRepository userRepository,
            IMessageRatingRepository messageRatingRepository,
            IMessageCommentRepository messageCommentRepository
            )
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _messageRatingRepository = messageRatingRepository;
            _messageCommentRepository = messageCommentRepository;
        }

        public Message AddMessage(MessageViewModel message)
        {
            var msg = new Message()
            {
                Image = message.Image,
                SendAt = DateTime.Now,
                Text = message.Message,
                UserId = message.UserId
            };
            return _messageRepository.Add(msg);
        }

        public MessageComment AddComment(int messageId, MessageCommentViewModel comment)
        {
            var cmt = new MessageComment()
            {
                MessageId = messageId,
                SendAt = DateTime.Now,
                Comment = comment.Comment,
                User = _userRepository.GetById(comment.UserId)
            };
            return _messageCommentRepository.Add(cmt);
        }

        public MessageRating AddMessageRating(int messageId, MessageRatingViewModel messageRating)
        {
            var rat = new MessageRating()
            {
                MessagesId = messageId,
                Rating = messageRating.Rating,
                User = _userRepository.GetById(messageRating.UserId)
            };
            if (CanUserRating(messageRating.UserId, messageId))
            {
                return null;
            }
            else
            {
                return _messageRatingRepository.Add(rat);
            }
            
        }

        public bool CanUserRating(int userId, int messageId)
        {
            return _messageRatingRepository.GetMany(x => x.MessagesId == messageId && x.UserId == userId).Any();
        }

        public int GetAllMessageCount()
        {
            return _messageRepository.GetAll().Count();
        }

        public IQueryable<MessageViewModel> GetAllWallMessage(int page)
        {

            return 
                _messageRepository.GetAll()
                .OrderByDescending(j => j.SendAt)
                .Skip(page * PageSize).Take(PageSize)
                .Select(x => new MessageViewModel()
            {
                Id = x.MessageId,
                Message = x.Text,
                SendAt = x.SendAt,
                UserId = x.User.Id,
                Image = x.Image,
                UserName = x.User.UserName,
                UserAva = (x.User.UserDetail.ProfilePicture == null || x.User.UserDetail.ProfilePicture == String.Empty) ? "/Images/ProfilePicture.jpg" : "/api/file/ProfilePicture/" + x.User.UserDetail.ProfilePicture + "/" + x.User.Id
            });
        }

        public IQueryable<MessageCommentViewModel> GetMessageComments(int messageId)
        {
            return
                _messageCommentRepository.GetAll()
                    .Where(x => x.MessageId == messageId)
                    .Select(c => new MessageCommentViewModel()
                    {
                        Id = c.Id,
                        Comment = c.Comment,
                        SendAt = c.SendAt,
                        UserName = c.User.UserName,
                        UserAva = (c.User.UserDetail.ProfilePicture == null || c.User.UserDetail.ProfilePicture == String.Empty) ? "/Images/ProfilePicture.jpg" : "/api/file/ProfilePicture/" + c.User.UserDetail.ProfilePicture + "/" + c.User.Id,
                        UserId = c.User.Id,
                        
                    });
        }
        public IQueryable<MessageRatingViewModel> GetMessageRatings(int messageId, bool rating)
        {
            return
                _messageRatingRepository.GetAll()
                    .Where(x => x.MessagesId == messageId && x.Rating == rating)
                    .Select(c => new MessageRatingViewModel()
                    {
                        UserName = c.User.UserName,
                        UserAva = c.User.UserDetail.ProfilePicture,
                        UserId = c.User.Id,
                        Rating = c.Rating
                    });
        }
    }
}
