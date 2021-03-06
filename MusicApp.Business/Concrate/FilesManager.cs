﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
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
        private readonly ILogService _logger;
        private readonly IConfiguration _configuration;

        public FilesManager(IFilesRepository filesRepository, ILogService logService,IConfiguration configuration)
        {
            _logger = logService;
            _configuration = configuration;
            _filesRepository = filesRepository;
        }

        public async Task<int> Delete(FilesDto files)
        {
            string pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\");
            if (files != null)
            {
                string[] fileName = files.Path.Split("Uploads/");
                pathBuild = Path.Combine(pathBuild, fileName[fileName.Length - 1]);
                DeleteFile(pathBuild);
                //if (File.Exists(Path.Combine(pathBuild)))
                // {
                //   File.Delete(pathBuild);
                // }
                return await _filesRepository.DeleteById(files.Id);
            }
            return 1;

        }

        public bool DeleteFile(string path)
        {
            try
            {
                if (File.Exists(Path.Combine(path)))
                {
                    File.Delete(path);
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                return false;
            }

        }


        public async Task<BaseResponse<Files>> GetByID(int id)
        {
            BaseResponse<Files> baseResponse = new BaseResponse<Files>();
            var file = await _filesRepository.GetByID(id);
            baseResponse.Result = file;
            return baseResponse;
        }

        public async Task<string> UploadFileFromStorage(Files files)
        {
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

            return fileName;
        }
        public async Task<Files> Insert(Files files)
        {
            try
            {
                _logger.LogInfo("Files Manager Insert Run");
                BaseResponse<Files> baseResponse = new BaseResponse<Files>();
                var fileName = await UploadFileFromStorage(files);
                files.Name = files.ImageFile.FileName;
                files.Size = Convert.ToInt32(files.ImageFile.Length);
                files.Path = _configuration["FilePath"] + fileName;
                //"http://localhost:5000/Uploads/"
                var newfiles = await _filesRepository.Insert(files);
                _logger.LogInfo($"Files Manager Insert Files DB");
                return newfiles;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message + "-" + exception.InnerException);

                throw;
            }


        }

        public  async Task<BaseResponse<FilesDto>> Update(Files files)
        {
            BaseResponse<FilesDto> baseResponse = new BaseResponse<FilesDto>();
            var updatedFile = await _filesRepository.Update(files);
            baseResponse.Result = new FilesDto { Id = updatedFile.Id, Path = updatedFile.Path };
            return baseResponse;
        }
    }
}
