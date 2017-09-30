using GamerAssistant.Users;
using System;
using System.Web.Mvc;

namespace GamerAssistant.Web.Controllers
{
    public class FriendsController : Controller
    {
        private readonly UserAppService _userService;

        public FriendsController(
            UserAppService userService
        )
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public void SendFriendRequest(int userId, int friendId)
        {
            var friendRequest = new UserFriend()
            {
                Id = 0,
                UserId = userId,
                FriendUserId = friendId,
                PendingApproval = true,
                AddedOn = DateTime.Now
            };

            _userService.AddFriend(friendRequest);
        }

        public void AcceptFriendRequest(int userId, int friendId)
        {
            var friendRequest = _userService.GetFriendRequestById(userId, friendId);

            friendRequest.PendingApproval = false;

            _userService.UpdateFriend(friendRequest);
        }

        public void RejectFriendRequest(int userId, int friendId)
        {
            var friendRequest = _userService.GetFriendRequestById(userId, friendId);

            _userService.DeleteFriendById(friendRequest.Id);
        }

        public void RemoveFriend(int userId, int friendId)
        {
            var friend = _userService.GetActiveFriendById(userId, friendId);

            _userService.DeleteFriendById(friend.Id);
        }
    }
}