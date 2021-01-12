using MusicApp.Dto;
using MusicApp.Entity;
using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Business.Abstract
{
    public interface IFilesService
    {
        Task<Files> Insert(Files files);
        Task<BaseResponse<Files>> GetByID(int id);

        bool DeleteFile(string path);

        Task<BaseResponse<FilesDto>> Update(Files files);


        Task<int> Delete(FilesDto files);


        Task<string> UploadFileFromStorage(Files files);

    }
}
