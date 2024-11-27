using CatGame.Core.Domain;
using CatGame.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatGame.Core.ServiceInterface
{
    public interface IFileServices
    {
        void UploadFilesToDatabase(CatDto dto, Cat domain);
        Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto);
    }
}
