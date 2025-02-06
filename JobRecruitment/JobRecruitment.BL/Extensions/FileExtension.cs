using Microsoft.AspNetCore.Http;

namespace JobRecruitment.BL.Extensions;

public static class FileExtension
{
    public static bool IsValidType(this IFormFile file, string type)
    {
        return file.ContentType.StartsWith(type);
    }

    public static bool IsValidSize(this IFormFile file, int mb)
    {
        return file.Length <= mb * 1024 * 1024;
    }

    public static async Task<string> UploadFileAsync(this IFormFile file, params string[] paths)
    {
        string uploadPath = Path.Combine(paths);
        if (!Path.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }
        string newFileName = Path.GetRandomFileName()+Path.GetExtension(file.FileName);
        using (Stream fileStream = File.Create(Path.Combine(uploadPath, newFileName)))
        {
            await file.CopyToAsync(fileStream);
        }
        return newFileName;
    }

}