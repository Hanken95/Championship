using Championchip.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Championchip.Core.Responses
{
    public abstract class ApiBaseResponse(bool success)
    {
        public bool Success { get; set; } = success;

        public TResultType GetOkResult<TResultType>()
        {
            if (this is ApiOkResponse<TResultType> apiOkResponse)
            {
                return apiOkResponse.Result;
            }

            throw new InvalidOperationException($"Response type {this.GetType().Name} is not ApiOkResponse");
        }
    }

    public sealed class ApiOkResponse<TResult>(TResult result) : ApiBaseResponse(true)
    {
        public TResult Result { get; set; } = result;
    }

    public sealed class ApiRemovedResponse<T> : ApiBaseResponse where T : class
    {
        public string Message { get; set; }
        public ApiRemovedResponse(int id) : base(true)
        {
            if (typeof(T) == typeof(Game))
            {
                Message = $"Removed game with id: {id} sucessfully";
            }
            else if (typeof(T) == typeof(Tournament))
            {
                Message = $"Removed tournament with id: {id} sucessfully";
            }
            throw new Exception("Incorrect ");
        }
    }

    public abstract class ApiNotFoundResponse(string title, string message) : ApiBaseResponse(false)
    {
        public string Title { get; } = title;
        public string Message { get; } = message;
    }

    public sealed class TournamentIsFullResponse(int id) : ApiBaseResponse(false)
    {
        public int Id { get; init; } = id;
    }

    public class TournamentNotFoundResponse : ApiNotFoundResponse
    {
        public TournamentNotFoundResponse(Expression<Func<Game, bool>> conditions) : base("Tournament not found" ,$"Tournament with conditions: {conditions.Name} was not found")
        {
        }
        public TournamentNotFoundResponse(int id) : base("Tournament not found", $"Tournament with {id} was not found")
        {
        }
    }
    public class GameNotFoundResponse : ApiNotFoundResponse
    {
        public GameNotFoundResponse(Expression<Func<Game, bool>> conditions) : base("Game not found", $"Game with conditions: {conditions.Parameters} was not found")
        {
        }
        public GameNotFoundResponse(int id) : base("Game not found", $"Game with id: {id} was not found")
        {
        }

    }

}
