using AutoMapper;
using MusicApp.Business.Abstract;
using MusicApp.DataAccess.Abstract;
using MusicApp.Dto;
using MusicApp.Entity;
using MusicApp.Entity.ResponseModels;
using MusicApp.Logger.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Business.Concrate
{
    public class FilesManager : IFilesService

    {
        private readonly IFilesRepository _filesRepository;
        private readonly IMapper _mapper;
        private readonly ILogService _logger;


        public FilesManager(IFilesRepository filesRepository,ILogService logService)
        {
            _logger = logService;
            _filesRepository = filesRepository;
        }

        public Task<BaseResponse<string>> Delete(FilesDto files)
        {
            string pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\");
           // if (File.Exists(Path.Combine(pathBuild)))
            //{

//            }
            throw new NotImplementedException();
        }

        public async Task<Files> Insert(Files files)
        {
            try
            {
                _logger.LogInfo("Files Manager Insert Run");
                BaseResponse<Files> baseResponse = new BaseResponse<Files>();
                string fileName = Path.GetFileNameWithoutExtension(files.ImageFile.Name);
                string extension = Path.GetExtension(files.ImageFile.FileName);
                fileName = fileName + "_" + Guid.NewGuid().ToString() + extension;
                string pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\");

                if (!Directory.Exists(pathBuild))
                {
                    _logger.LogInfo($"Files Manager Insert Directory Not Exist Location : {pathBuild} created directory");
                    Directory.CreateDirectory(pathBuild);
                }
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\", fileName);
                using (var FileStream = new FileStream(path, FileMode.Create))
                {
                    await files.ImageFile.CopyToAsync(FileStream);
                }
                _logger.LogInfo($"Files Manager Insert Files Uploaded Name : {files.ImageFile.FileName}");

                files.Name = files.ImageFile.FileName;
                files.Size = Convert.ToInt32(files.ImageFile.Length);
                files.Path = "http://localhost:5000/Uploads/"+fileName;
                var newfiles = await _filesRepository.Insert(files);
                _logger.LogInfo($"Files Manager Insert Files DB");
                return newfiles;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message+ "-"+exception.InnerException);

                throw;
            }

            
        }
    }
}
