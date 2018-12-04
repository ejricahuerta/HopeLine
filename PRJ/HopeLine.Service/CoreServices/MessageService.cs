using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.Extensions.Logging;

namespace HopeLine.Service.CoreServices {
    public class MessageService : IMessage {
        private readonly ILogger<MessageService> _logger;
        private readonly ChatDbContext _chatDb;

        public MessageService (ILogger<MessageService> logger, ChatDbContext chatDb) {
            _logger = logger;
            _chatDb = chatDb;
        }

        public void AddTopicsToRoom (string roomId, IList<int> topics) {
            var room = _chatDb.Rooms.SingleOrDefault (r => r.RoomId == roomId);
            room.Topics = topics;
            _chatDb.Update (room);
            _chatDb.SaveChanges ();
        }

        public async Task AndUsersToRoom (string mentorId, string guestId, string roomId) {
            try {
                await _chatDb.Rooms.AddAsync (new Room {
                    RoomId = roomId,
                        MentorId = mentorId,
                        GuestId = guestId
                });
                _logger.LogInformation ("Adding {} and {} to room: {}", mentorId, guestId, roomId);
                await _chatDb.SaveChangesAsync ();
            } catch (System.Exception ex) {
                _logger.LogWarning ("Unable to Add to Room:{} with Exception: {}", roomId, ex);
            }
        }

        public async Task DeleteAllMessages (string roomId) {
            try {
                var connectionMessages = _chatDb.Messages
                    .Where (m => m.RoomId == roomId).ToList ();

                foreach (var m in connectionMessages) {
                    _chatDb.Remove (m);
                }
                var room = _chatDb.Rooms.SingleOrDefault (r => r.RoomId == roomId);
                _chatDb.Remove (room);
                _logger.LogInformation ("Removing All messages for room: {}", roomId);
                await _chatDb.SaveChangesAsync ();
            } catch (System.Exception ex) {
                _logger.LogWarning ("Unable to Remove Messages for Room : {} with Exception: {}", roomId, ex);
            }
        }

        public IEnumerable<MessageModel> GetAllMessages (string roomId) {
            try {
                _logger.LogInformation ("Returning All Messages for {}", roomId);
                return _chatDb.Messages.OrderBy (d => d.DateAdded)
                    .Where (m => m.RoomId == roomId)
                    .Select (mm => new MessageModel {
                        ConnectionId = mm.RoomId,
                            UserName = mm.UserName,
                            Text = mm.Text
                    });
            } catch (System.Exception ex) {
                _logger.LogWarning ("Unable to Get Messages for Room:{} with Exception: {} ", roomId, ex);
            }
            return null;
        }

        public string GetRoomForUser (string userId, bool isGuest) {

            Room room;
            if (isGuest) {
                _logger.LogInformation ("Getting room for guest user {}", userId);
                room = _chatDb.Rooms.FirstOrDefault (u => u.GuestId == userId);
            } else {
                _logger.LogInformation ("Getting room for mentor {}", userId);
                room = _chatDb.Rooms.FirstOrDefault (u => u.MentorId == userId);
            }
            return (room != null) ? room.RoomId : null;

        }

        public IList<int> GetTopics (string roomId) {
            var room = _chatDb.Rooms.FirstOrDefault (r => r.RoomId == roomId);
            if (room != null) {
                return room.Topics;
            } else {
                return null;
            }
        }

        public IEnumerable<OnlineMentorModel> ListAvailableMentor () {
            try {
                _logger.LogInformation ("Fethcing All available Mentors");
                return _chatDb.OnlineMentors.Where (m => m.Available == true).Select (n => new OnlineMentorModel {
                    Available = n.Available,
                        ConnectionId = n.ConnectionId,
                        Id = n.Id,
                        MentorId = n.MentorId
                });
            } catch (System.Exception ex) {
                _logger.LogWarning ("Unable to Process Finding Mentors with Exception: {}", ex);
            }
            return null;
        }

        public async Task NewMentorAvailable (string mentorId, string connectionId) {
            try {
                var newOnline = new OnlineMentor {
                    MentorId = mentorId,
                    ConnectionId = connectionId,
                    Available = true
                };
                var mentor = _chatDb.OnlineMentors.SingleOrDefault (i => i.MentorId == mentorId);
                if (mentor == null) {
                    await _chatDb.OnlineMentors.AddAsync (newOnline);
                    await _chatDb.SaveChangesAsync ();
                } else {
                    mentor.Available = true;
                    mentor.ConnectionId = connectionId;
                    _chatDb.Update (mentor);
                    _chatDb.SaveChanges ();
                }
            } catch (System.Exception ex) {
                throw new System.Exception ("Unable to Add Mentor to Online Mentors: ", ex);
            }
        }

        public void NewMessage (MessageModel model) {
            try {
                _chatDb.Messages.Add (new Message {
                    RoomId = model.ConnectionId,
                        UserName = model.UserName,
                        Text = model.Text
                });

                _chatDb.SaveChanges ();
            } catch (System.Exception ex) {

                throw new System.Exception ("Unable to save new Message: ", ex);
            }
        }

        public async Task RemoveMentor (string mentorId) {
            try {
                var mentor = _chatDb.OnlineMentors.SingleOrDefault (m => m.MentorId == mentorId);
                _chatDb.Remove (mentor);
                await _chatDb.SaveChangesAsync ();
            } catch (System.Exception ex) {
                throw new System.Exception ("Unable to Remove Mentor From Pool: ", ex);
            }
        }

        public async Task SetMentorOnCall (string mentorId, string connectionId) {
            try {
                var mentor = _chatDb.OnlineMentors.SingleOrDefault (i => i.MentorId == mentorId);
                mentor.Available = false;
                mentor.ConnectionId = connectionId;
                _chatDb.Update (mentor);
                await _chatDb.SaveChangesAsync ();
            } catch (System.Exception ex) {
                throw new System.Exception ("Unable to Set Mentor Offline: ", ex);
            }
        }
    }
}