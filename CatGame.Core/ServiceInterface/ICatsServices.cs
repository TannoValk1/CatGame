using CatGame.Core.Domain;
using CatGame.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatGame.Core.ServiceInterface
{
    public interface ICatsServices
    {
        Task<Cat> DetailsAsync(Guid id);
        Task<Cat> Create(CatDto dto);
        Task<Cat> Update(CatDto dto);
        Task<Cat> Delete(Guid id);
    }
}
