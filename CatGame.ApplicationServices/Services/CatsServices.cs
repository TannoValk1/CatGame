using CatGame.Core.Domain;
using CatGame.Core.Dto;
using CatGame.Core.ServiceInterface;
using CatGame.Data;
using CatGame.Core.ServiceInterface;
using Microsoft.EntityFrameworkCore;

namespace CatGame.ApplicationServices.Services
{
    public class CatsServices : ICatsServices
    {
        private readonly CatGameContext _context;
        private readonly IFileServices _fileServices;

        public CatsServices(CatGameContext context, IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<Cat> DetailsAsync(Guid id)
        {
            var result = await _context.Cats
                .FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }

        public async Task<Cat> Create(CatDto dto)
        {
            Cat cat = new Cat();

            cat.ID = Guid.NewGuid();
            cat.CatHealth = 100;
            cat.CatXP = 0;
            cat.CatXPNextLevel = 100;
            cat.CatLevel = 0;
            cat.CatStatus = Core.Domain.CatStatus.Alive;
            cat.CatWasBorn = DateTime.Now;
            cat.CatDied = DateTime.Parse("01/01/9999 00:00:00");

            cat.CatName = dto.CatName;
            cat.CatType = (Core.Domain.CatType)dto.CatType;
            cat.PrimaryAttackName = dto.PrimaryAttackName;
            cat.PrimaryAttackPower = dto.PrimaryAttackPower;
            cat.SecondaryAttackName = dto.SecondaryAttackName;
            cat.SecondaryAttackPower = dto.SecondaryAttackPower;
            cat.SpecialAttackName = dto.SpecialAttackName;
            cat.SpecialAttackPower = dto.SpecialAttackPower;

            // Set for db
            cat.CreatedAt = DateTime.Now;
            cat.UpdatedAt = DateTime.Now;

            // Files
            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, cat);
            }

            await _context.Cats.AddAsync(cat);
            await _context.SaveChangesAsync();

            return cat;
        }

        public async Task<Cat> Update(CatDto dto)
        {
            var cat = await _context.Cats.FirstOrDefaultAsync(x => x.ID == dto.ID);

            if (cat == null) return null;

            // Update the cat's properties
            cat.CatHealth = dto.CatHealth;
            cat.CatXP = dto.CatXP;
            cat.CatXPNextLevel = dto.CatXPNextLevel;
            cat.CatLevel = dto.CatLevel;
            cat.CatStatus = (Core.Domain.CatStatus)dto.CatStatus;
            cat.CatWasBorn = dto.CatWasBorn;
            cat.CatDied = DateTime.Parse("01/01/9999 00:00:00");

            // Set by user
            cat.CatName = dto.CatName;
            cat.CatType = (Core.Domain.CatType)dto.CatType;
            cat.PrimaryAttackName = dto.PrimaryAttackName;
            cat.PrimaryAttackPower = dto.PrimaryAttackPower;
            cat.SecondaryAttackName = dto.SecondaryAttackName;
            cat.SecondaryAttackPower = dto.SecondaryAttackPower;
            cat.SpecialAttackName = dto.SpecialAttackName;
            cat.SpecialAttackPower = dto.SpecialAttackPower;

            // Set for db
            cat.CreatedAt = dto.CreatedAt;
            cat.UpdatedAt = DateTime.Now;

            // Files
            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, cat);
            }

            _context.Cats.Update(cat);
            await _context.SaveChangesAsync();

            return cat;
        }

        public async Task<Cat> Delete(Guid id)
        {
            var result = await _context.Cats
                .FirstOrDefaultAsync(x => x.ID == id);

            if (result == null) return null;

            _context.Cats.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
