using AutoMapper;
using LPS.DocumentUploader.Application.Helpers;
using LPS.DocumentUploader.Application.Services.Users.Dto;
using LPS.DocumentUploader.Database.Databases;
using LPS.DocumentUploader.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Application.Services.Users
{
    public class UserAppService : IUserAppService, IDisposable
    {
        private readonly LPSDBContext _databaseContext;
        private IMapper? _mapper;

        public UserAppService(LPSDBContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Create(CreateUserDto model)
        {
            try
            {
                using (var transaction = await _databaseContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (!IsValidPassword(model.Password))
                        {
                            return (false, "Password does not meet the requirements.");
                        }

                        var newUser = _mapper.Map<MstUser>(model);
                        newUser.PasswordSalt = model.SurName;
                        newUser.Password = CryptographyHelper.GenerateHashWithSalt(model.Password, newUser.PasswordSalt);

                        _databaseContext.Users.Add(newUser);
                        await _databaseContext.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return await Task.Run(() => (true, "Success"));
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return await Task.Run(() => (false, ex.Message));
                    }
                }
            }
            catch (Exception outerEx)
            {
                return await Task.Run(() => (false, $"Error create user: {outerEx.Message}"));
            }
        }

        public async Task<(bool, string)> Delete(int id)
        {
            try
            {
                using (var transaction = await _databaseContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var user = await _databaseContext.Users.SingleOrDefaultAsync(w => w.Id == id);

                        if (user != null)
                        {
                            _databaseContext.Users.Remove(user);
                            await _databaseContext.SaveChangesAsync();

                            await transaction.CommitAsync();
                            return await Task.Run(() => (true, "User removed successfully"));
                        }
                        else
                        {
                            return await Task.Run(() => (false, "User not found"));
                        }
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return await Task.Run(() => (false, $"Error removing user: {ex.Message}"));
                    }
                }
            }
            catch (Exception outerEx)
            {
                return await Task.Run(() => (false, $"Error: {outerEx.Message}"));
            }
        }

        public async Task<(bool, string)> Update(UpdateUserDto model)
        {
            try
            {
                using (var transaction = await _databaseContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var user = _mapper.Map<MstUser>(model);

                        _databaseContext.Users.Update(user);
                        await _databaseContext.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return await Task.Run(() => (true, "Success"));
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return await Task.Run(() => (false, $"Error updating user: {ex.Message}"));
                    }
                }
            }
            catch (Exception outerEx)
            {
                return await Task.Run(() => (false, $"Error: {outerEx.Message}"));
            }
        }

        private static bool IsValidPassword(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }

            if (!password.Any(char.IsLower))
            {
                return false;
            }

            if (!password.Any(char.IsUpper))
            {
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                return false;
            }

            string specialCharacters = @"[!@#$%^&*()-+]";
            if (!Regex.IsMatch(password, specialCharacters))
            {
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _databaseContext?.Dispose();
                _mapper = null;
            }
        }
    }
}
