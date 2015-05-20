using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;

namespace PmaPlus.Services
{
    public class DiaryServices
    {
        private readonly IDiaryRepository _diaryRepository;
        private readonly IDiaryRecipientRepository _diaryRecipientRepository;
        private readonly IUserRepository _userRepository;


        public DiaryServices(IDiaryRepository diary, IDiaryRecipientRepository diaryRecipientRepository, IUserRepository userRepository)
        {
            _diaryRepository = diary;
            _diaryRecipientRepository = diaryRecipientRepository;
            _userRepository = userRepository;
        }

        public bool DiaryExist(int id)
        {
            return _diaryRepository.GetMany(d => d.Id == id).Any();
        }


        public IEnumerable<Diary> GetUserDiaries(int userId)
        {
            List<Diary> ownDiary = _diaryRepository.GetMany(d => d.User.Id == userId).ToList();
            List<Diary> recDiary =
                _diaryRecipientRepository.GetMany(r => r.Recipient.Id == userId).Select(r => r.Diary).ToList();

            return ownDiary.Union(recDiary);
        }

        public Diary AddDiary(Diary diary, int ownerUserId, IList<int> recipientUsers,IList<Role> roles)
        {
            List<int> userIds = new List<int>();
            foreach (var role in roles)
            {
                userIds.AddRange(_userRepository.GetMany(u => u.Role == role).Select(u => u.Id));
            }

            recipientUsers = userIds.Union(recipientUsers).ToList();


            var owner = _userRepository.GetById(ownerUserId);
            if (owner != null)
            {
                diary.User = owner;
                diary.CreatedAt = DateTime.Now;

                var newDiary = _diaryRepository.Add(diary);
                if (recipientUsers.Count > 0)
                {
                    foreach (var recipientUserId in recipientUsers)
                    {
                        _diaryRecipientRepository.Add(new DiaryRecipient()
                        {
                            Diary = newDiary,
                            Owner = newDiary.User,
                            Recipient = _userRepository.GetById(recipientUserId),
                            Accepted = false
                        });
                    }

                }

                return newDiary;
            }
            else
            {
                return null;
            }
        }



        public void DeleteDiary(int id)
        {
            _diaryRecipientRepository.Delete(r => r.Diary.Id == id);
            _diaryRepository.Delete(d => d.Id == id);
        }
    }
}
