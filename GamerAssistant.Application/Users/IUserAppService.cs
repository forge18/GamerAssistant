using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GamerAssistant.Roles.Dto;
using GamerAssistant.Users.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamerAssistant.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        //Get favorite list by user id
        IList<UserFavorite> GetFavoritesById(int userId);

        //Get friend list by user id
        IList<UserFriend> GetFriendsById(int userId);

        //Get game list by user id
        IList<UserGame> GetGamesById(int userId);

        UserVote GetVoteById(int userVoteId);

        //Get all votes by user id
        IList<UserVote> GetVotesByUserId(int userId);

        IList<UserVote> GetVotesByEventGameId(int eventGameId);

        //Get all upcoming votes by user id
        IList<UserVote> GetUpcomingVotesById(int userId);

        IList<UserGame> GetUsersByGameId(int gameId);

        UserFriend GetFriendRequestById(int userId, int friendId);

        UserFriend GetActiveFriendById(int userId, int friendId);

        //Add favorite
        void AddFavorite(UserFavorite favorite);

        //Delete favorite
        void DeleteFavoriteById(int gameId);

        //Add friend
        void AddFriend(UserFriend userFriend);

        //Update friend
        void UpdateFriend(UserFriend userFriend);

        //Delete friend
        void DeleteFriendById(int userFriendId);

        //Add game
        void AddGameToUser(UserGame userGame);

        //Delete game
        void DeleteGameFromUser(int userGameId);

        //Add vote
        void AddVoteToUser(UserVote userVote);

        //Update vote
        void UpdateVote(UserVote userVote);

        //Delete vote
        void DeleteVoteById(int userVoteId);
    }
}