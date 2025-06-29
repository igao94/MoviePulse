﻿using Application.Core;
using Application.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Photos;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;

    public PhotoService(IOptions<CloudinarySettings> config)
    {
        var account = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);

        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> DeletePhotoAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(deleteParams);

        if (result.Error is not null)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Result;
    }

    public async Task DeletePhotosAsync(IEnumerable<string> publicIds)
    {
        var result = await _cloudinary.DeleteResourcesAsync(publicIds.ToArray());

        if (result.Error is not null)
        {
            throw new Exception(result.Error.Message);
        }
    }

    public async Task<PhotoUploadResult?> UploadPhotoAsync(IFormFile file)
    {
        if (file.Length <= 0)
        {
            return null;
        }

        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "MoviePulse"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error is not null)
        {
            throw new Exception(uploadResult.Error.Message);
        }

        return new PhotoUploadResult
        {
            PublicId = uploadResult.PublicId,
            Url = uploadResult.SecureUrl.AbsoluteUri
        };
    }
}
