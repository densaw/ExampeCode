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
        private readonly UserServices _userServices;

        private const int PageSize = 20;

        public MessageServices(
            IMessageRepository messageRepository,
            IUserRepository userRepository,
            IMessageRatingRepository messageRatingRepository,
            IMessageCommentRepository messageCommentRepository, UserServices userServices)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _messageRatingRepository = messageRatingRepository;
            _messageCommentRepository = messageCommentRepository;
            _userServices = userServices;
        }

        public Message AddMessage(MessageViewModel message)
        {
            var clubeId = _userServices.GetClubByUserName(_userRepository.GetById(message.UserId).Email).Id;
            var msg = new Message()
            {
                Image = message.Image,
                SendAt = DateTime.Now,
                Text = message.Message,
                UserId = message.UserId,
                ClubId = clubeId
            };
            return _messageRepository.Add(msg);
        }

        public MessageCommentViewModel AddComment(int messageId, MessageCommentViewModel comment)
        {
            var cmt = new MessageComment()
            {
                MessageId = messageId,
                SendAt = DateTime.Now,
                Comment = comment.Comment,
                User = _userRepository.GetById(comment.UserId)
            };
            return ConvertToCommentViewModel(_messageCommentRepository.Add(cmt));
        }

        public MessageRatingViewModel AddMessageRating(int messageId, MessageRatingViewModel messageRating)
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
                return ConvertToRatingViewModel(_messageRatingRepository.Add(rat));
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

        public MessageViewModel ConvertToMessageViewModel(Message message)
        {
            return new MessageViewModel()
            {
                Id = message.MessageId,
                Message = message.Text,
                SendAt = message.SendAt,
                UserId = message.User.Id,
                Image = message.Image,
                UserName = message.User.Role == Role.ClubAdmin ? "Club Administrator" : message.User.UserDetail.FirstName + " " + message.User.UserDetail.LastName,
                UserAva = string.IsNullOrEmpty(message.User.UserDetail.ProfilePicture) ? "/Images/ProfilePicture.jpg" : "/api/file/ProfilePicture/" + message.User.UserDetail.ProfilePicture + "/" + message.User.Id 
            };
        }

        public MessageCommentViewModel ConvertToCommentViewModel(MessageComment comment)
        {
            return new MessageCommentViewModel()
            {
                Id = comment.Id,
                Comment = comment.Comment,
                SendAt = comment.SendAt,
                UserName = (comment.User.UserDetail == null) ? comment.User.UserName : comment.User.UserDetail.FirstName + " " + comment.User.UserDetail.LastName,
                UserAva = (comment.User.Role == Role.ClubAdmin || comment.User.Role == Role.SystemAdmin) ? "/Images/ProfilePicture.jpg" : "/api/file/ProfilePicture/" + comment.User.UserDetail.ProfilePicture + "/" + comment.User.Id
            };
        }

        public MessageRatingViewModel ConvertToRatingViewModel(MessageRating rating)
        {
            return new MessageRatingViewModel()
            {
                UserId = rating.UserId,
                Rating = rating.Rating,
                UserAva = (rating.User.Role == Role.ClubAdmin || rating.User.Role == Role.SystemAdmin) ? "/Images/ProfilePicture.jpg" : "/api/file/ProfilePicture/" + rating.User.UserDetail.ProfilePicture + "/" + rating.User.Id,
                UserName = (rating.User.UserDetail == null) ? rating.User.UserName : rating.User.UserDetail.FirstName + " " + rating.User.UserDetail.LastName
            };
        }

        public IQueryable<MessageViewModel> GetAllWallMessage(int page, int clubId)
        {

            return 
                _messageRepository.GetAll()
                .Where(x => x.ClubId == clubId)
                .OrderByDescending(j => j.SendAt)
                .Skip(page * PageSize).Take(PageSize)
                .Select(x => new MessageViewModel()
            {
                Id = x.MessageId,
                Message = x.Text,
                SendAt = x.SendAt,
                UserId = x.User.Id,
                Image = x.Image,
                UserName = (x.User.UserDetail == null) || (x.User.Role == Role.ClubAdmin) ? "Club Administrator" : x.User.UserDetail.FirstName + " " + x.User.UserDetail.LastName,
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
                        UserName = (c.User.UserDetail == null) ? c.User.UserName : c.User.UserDetail.FirstName + " " + c.User.UserDetail.LastName,
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
                        UserName = (c.User.UserDetail == null) ? c.User.UserName : c.User.UserDetail.FirstName + " " + c.User.UserDetail.LastName,
                        UserAva = (c.User.UserDetail.ProfilePicture == null || c.User.UserDetail.ProfilePicture == String.Empty) ? "/Images/ProfilePicture.jpg" : "/api/file/ProfilePicture/" + c.User.UserDetail.ProfilePicture + "/" + c.User.Id,
                        UserId = c.User.Id,
                        Rating = c.Rating
                    });
        }
    }
}
