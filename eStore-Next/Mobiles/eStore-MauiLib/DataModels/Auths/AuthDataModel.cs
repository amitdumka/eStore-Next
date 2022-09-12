﻿using System;
using System.Data;
using AKS.Shared.Commons.Models.Auth;
using eStore_MauiLib.DataModels;
using Microsoft.EntityFrameworkCore;

namespace eStore_MauiLib.DataModels.Auths
{
    public class AuthDataModel : BaseDataModel<User>
    {
        public AuthDataModel(ConType conType) : base(conType)
        {
        }

        public override Task<List<User>> FindAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<User>> GetItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<User>> GetItems(string storeid)
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.Users.ToListAsync();
                case DBType.Azure:
                    return await _azureDb.Users.ToListAsync();

                default:
                    return null;
                   
            }
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }

        public User SignIn(string userName, string password)
        {
            User user = null;
            switch (Mode)
            {
                case DBType.Local:
                    user = _localDb.Users.Where(c => c.UserName == userName && c.Password == password).FirstOrDefault();
                    break;
                case DBType.Azure:
                    user = _azureDb.Users.Where(c => c.UserName == userName && c.Password == password).FirstOrDefault();
                    break;
                case DBType.API:
                    break;
                default:
                    break;
            }

            return user;
        }
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<bool> PasswordChangeAsync(string userName, string oldPassword, string newPassword)
        {
            User user;
            //TODO: SendInfo or Return in ReturnType class to manage return infomation along with error
            user = _localDb.Users.Where(c => c.UserName == userName && c.Password == oldPassword).FirstOrDefault();

            switch (Mode)
            {
                case DBType.Local:
                    user = _localDb.Users.Where(c => c.UserName == userName && c.Password == oldPassword).FirstOrDefault();
                    break;
                case DBType.Azure:
                    user = _azureDb.Users.Where(c => c.UserName == userName && c.Password == oldPassword).FirstOrDefault();
                    break;
                default:
                    break;
            }

            if (user != null)
            {
                // TODO: Send info that old password is not matched or user not found!.
                user.Password = newPassword;
                user = await Save(user, false);
                return (user != null);
            }
            else
                return false;

        }

        public async Task<bool> SignUpAsync(User user) => (await Save(user, true)) != null;

        public async Task<bool> SyncRemote(DBType dbType)
        {
            switch (dbType)
            {
                case DBType.Azure:
                    if (_azureDb == null)
                    {
                        _azureDb = new AKS.MAUI.Databases.AppDBContext();
                    }
                    var users = _azureDb.Users.Where(c => c.StoreId == StoreCode).ToList();
                    int newUser = 0;
                    foreach (var user in users)
                    {
                        if (!_localDb.Users.Any(c => c.UserName == user.UserName))
                        {
                            _localDb.Users.Add(user);
                            newUser++;
                        }
                    }
                    int count = await _localDb.SaveChangesAsync();
                    return (newUser == count);
                    break;

                case DBType.API:
                    //RemoteSingleServer server = new RemoteSingleServer("", "User");
                    //var users = await server.GetByUrl<List<User>>(WebAPI.APIBase + WebAPI.Users);
                    //foreach (var user in users)
                    //{
                    //    user.UserId = 0; //TODO: remove is leagace.
                    //}
                    //_localDb.Users.AddRange(users);
                    //return _localDb.SaveChanges() > 0;
                    break;
                case DBType.Mango: break;
                case DBType.Remote: break;
                default:
                    break;
            }

            return false;
        }

        public bool SyncUp(DBType dBType)
        {
            throw new NotImplementedException();

        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }
    }
}