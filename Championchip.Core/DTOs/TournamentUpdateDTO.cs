namespace Championchip.Core.DTOs
{
    public record TournamentUpdateDTO : TournamentForManupulationDTO
    {
        public int Id { get; set; }
    }
}
