using AutoMapper;
using Championchip.Core.DTOs.TournamentDTOs;
using Championchip.Core.Entities;
using Championchip.Core.Repositories;
using Championchip.Core.Request;
using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.XPath;

namespace Championship.Services
{
    public class TournamentService(IUnitOfWork unit, IMapper mapper) :  ITournamentService
    {
        public async Task<TournamentDTO> AddAsync(TournamentCreateDTO dto)
        {
            Tournament tournament = mapper.Map<Tournament>(dto);
            unit.TournamentRepository.Add(tournament);

            await unit.CompleteAsync();

            return mapper.Map<TournamentDTO>(tournament);
        }

        public async Task<bool> AnyAsync(Expression<Func<Tournament, bool>> conditions)
        {
            return await unit.TournamentRepository.AnyAsync(conditions);
        }

        public async Task<bool> AnyAsync()
        {
            return await unit.TournamentRepository.AnyAsync();
        }

        public async Task<(IEnumerable<TournamentDTO> companyDtos, MetaData metaData)> GetAllAsync(TournamentRequestParams  requestParams)
        {
            PagedList<Tournament> pagedList = await unit.TournamentRepository.GetAllAsync(requestParams);
            IEnumerable<TournamentDTO> dtos = mapper.Map<IEnumerable<TournamentDTO>>(pagedList.Items);
            return (dtos, pagedList.MetaData);
        }

        public async Task<(IEnumerable<TournamentDTO> companyDtos, MetaData metaData)> GetAllAsync(Expression<Func<Tournament, bool>> conditions, TournamentRequestParams  requestParams)
        {
            PagedList<Tournament> pagedList = await unit.TournamentRepository.GetAllAsync(conditions, requestParams);
            IEnumerable<TournamentDTO> dtos = mapper.Map<IEnumerable<TournamentDTO>>(pagedList.Items);
            return (dtos, pagedList.MetaData);
        }

        public async Task<TournamentDTO?> GetAsync(int id)
        {
            return mapper.Map<TournamentDTO>(await unit.TournamentRepository.GetAsync(id));
        }
        public async Task<TournamentDTO?> GetAsync(int id, TournamentRequestParams  requestParams)
        {
            return mapper.Map<TournamentDTO>(await unit.TournamentRepository.GetAsync(id, requestParams));
        }

        public async Task<TournamentDTO?> GetAsync(Expression<Func<Tournament, bool>> conditions, TournamentRequestParams  requestParams)
        {
            return mapper.Map<TournamentDTO>(await unit.TournamentRepository.GetAsync(conditions, requestParams));
        }
        public async Task RemoveAsync(int id)
        {
            Tournament? entity = await unit.TournamentRepository.GetAsync(t => t.Id == id) ?? throw new NullReferenceException("Tournament not found");
            unit.TournamentRepository.Remove(entity);
        }

        public async Task<TournamentDTO> PutAsync(int id, TournamentUpdateDTO dto)
        {
            Tournament? tournamentToPut = await unit.TournamentRepository.GetAsync(t => t.Id == id) ?? throw new NullReferenceException("Tournament not found");

            mapper.Map(dto, tournamentToPut);

            await unit.CompleteAsync();

            return mapper.Map<TournamentDTO>(tournamentToPut);
        }

        public async Task<TournamentDTO> PatchAsync(int id, JsonPatchDocument<TournamentUpdateDTO> patchDocument)
        {
            var tournamentToPatch = await unit.TournamentRepository.GetAsync(t => t.Id == id) ?? throw new NullReferenceException("Tournament not found");

            var dto = mapper.Map<TournamentUpdateDTO>(tournamentToPatch);

            patchDocument.ApplyTo(dto);

            mapper.Map(dto, tournamentToPatch);
            await unit.CompleteAsync();

            return mapper.Map<TournamentDTO>(tournamentToPatch);
        }
    }
}