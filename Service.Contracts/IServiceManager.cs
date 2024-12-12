namespace Service.Contracts
{
    public interface IServiceManager
    {
        IGameService GameService { get; }
        ITournamentService TournamentService { get; }
    }
}