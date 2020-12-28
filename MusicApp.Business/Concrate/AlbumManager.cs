using AutoMapper;
using MusicApp.Business.Abstract;
using MusicApp.DataAccess.Abstract;
using MusicApp.Entity;
using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Business.Concrate
{
    public class AlbumManager : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;
        public AlbumManager(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }
        public async Task<IEnumerable<Albums>> GetAll()
        {
            return await _albumRepository.GetAll();
        }
    }
}
