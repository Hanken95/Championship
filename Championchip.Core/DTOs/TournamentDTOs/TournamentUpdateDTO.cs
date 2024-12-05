namespace Championchip.Core.DTOs.TournamentDTOs
{
    public record TournamentUpdateDTO : TournamentForManupulationDTO
    {
        public int Id { get; set; }
    }
}
