using Domain.Models;
using EntityFramework.DbContexts;
using IServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Application.Responses;
using Application.Enums;
using AutoMapper;
using Application.DTO;

namespace Services
{
    public class ContactsServices : IContactsServices
    {
        private readonly MainDbContext appDb;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        public ContactsServices(MainDbContext appDb, UserManager<User> userManager, IMapper mapper)
        {
            this.appDb = appDb;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<ContactsResponse> AcceptAddToContactsRequest(User userAcceptingRequest,long addToContactsRequestId)
        {
            var addRequest = await appDb.AddToContactRequests.Where(u => u.UserTo.Id == userAcceptingRequest.Id && u.Id == addToContactsRequestId).FirstOrDefaultAsync();

            if(addRequest != null)
            {
                var userSendingRequest = await userManager.FindByIdAsync(addRequest.UserFromId);
                // user sending request does not exist anymore
                if(userSendingRequest == null)
                {
                    appDb.AddToContactRequests.Remove(addRequest);
                    await appDb.SaveChangesAsync();
                    return new ContactsResponse { Message = "User sending that request does not exist anymore", ResponseStatus = Status.Error };
                }

                // Add users on bothends to each others contactslist, if they do not have contacts list create a contacts list then add them
                if (await AddUserToContactList(userAcceptingRequest, userSendingRequest.Id) && await AddUserToContactList(userSendingRequest, userAcceptingRequest.Id))
                {
                    appDb.AddToContactRequests.Remove(addRequest);
                    return new ContactsResponse { Message = "User added to contacts list", ResponseStatus = Status.Success };
                }
            }


            return new ContactsResponse { Message = "Did not find any add request with that id", ResponseStatus = Status.Error };
        }

        public async Task<ContactsResponse> DeclineAddToContactsRequest(User userDecliningRequest, long addToContactsRequestId)
        {
            var addRequest = await appDb.AddToContactRequests.Where(u => u.UserTo.Id == userDecliningRequest.Id && u.Id == addToContactsRequestId).FirstOrDefaultAsync();
           
            if (addRequest != null)
            {
                var userSendingRequest = await userManager.FindByIdAsync(addRequest.UserFromId);
                // user sending request does not exist anymore
                if (userSendingRequest == null)
                {
                    appDb.AddToContactRequests.Remove(addRequest);
                    await appDb.SaveChangesAsync();
                    return new ContactsResponse { Message = "User sending that request does not exist anymore", ResponseStatus = Status.Error };
                }

                appDb.AddToContactRequests.Remove(addRequest);
                await appDb.SaveChangesAsync();
                return new ContactsResponse { Message = "Users request declined", ResponseStatus = Status.Success };                
            }


            return new ContactsResponse { Message = "Did not find any add request with that id", ResponseStatus = Status.Error };
        }

        public async Task<ContactsResponse> FindUsersAsync(User user,string searchString)
        {
            if(searchString != null)
            {
                var users = await appDb.Users
                    .Include(u => u.Files)
                    .Where(u => (u.Email.Contains(searchString) && u.Email != user.Email) || (u.UserName.Contains(searchString) && u.UserName != user.UserName))
                    .ToListAsync();

                var userContactsList = await appDb.ContactLists
                    .Include(c => c.Contacts)
                    .FirstOrDefaultAsync(u => u.UserId == user.Id);

                var userAddToContactsRequest = await appDb.AddToContactRequests
                    .Where(u => u.UserFromId == user.Id)
                    .ToListAsync();

                if (users != null)
                {
                    var usersDTOS = mapper.Map<List<UserDTO>>(users);
                    foreach (var foundUser in usersDTOS)
                    {
                        if(userContactsList.Contacts.Find(u => u.UserId == foundUser.Id) != null)
                        {
                            foundUser.UserStatus = UserStatus.Friend;
                        }
                        else if (userAddToContactsRequest.Find(r => r.UserToId == foundUser.Id) != null)
                        {
                            foundUser.UserStatus = UserStatus.Added;
                        }
                        else
                        {
                            foundUser.UserStatus = UserStatus.None;
                        }                                                  
                    }
                    string message = String.Format("Found {0} users", users.Count);
                    return new ContactsResponse { Users = usersDTOS, Message = message, ResponseStatus = Status.Success };
                }
                return new ContactsResponse { Users = null, Message = "Did not found any user with given username or email", ResponseStatus = Status.Success };
            }
            return new ContactsResponse { Users = null, Message = "The request was incorrect", ResponseStatus = Status.Error };
        }

        public async Task<ContactsResponse> GetUsersContacts(User user)
        {
            var usersConv = await appDb.ContactLists.ToListAsync();

            if(user != null)
            {
                var userContactsList = await appDb.ContactLists
                    .Include(c => c.Contacts)
                        .ThenInclude(c => c.User.Files)
                    .FirstOrDefaultAsync(c => c.UserId == user.Id);

                if(userContactsList == null)
                {
                    return new ContactsResponse { Users = null, Message = "This user has no contacts list", ResponseStatus = Status.Error };
                }

                var userContacts = userContactsList.Contacts.Select(c => c.User).ToList();
                var userContactsDTOS = mapper.Map<List<UserDTO>>(userContacts);
                string message = String.Format("You have {0} friends", userContacts.Count);
                return new ContactsResponse { Users = userContactsDTOS, Message = message, ResponseStatus = Status.Success };
            }
            return new ContactsResponse { Users = null, Message = "User was not found", ResponseStatus = Status.Error };
        }

        public async Task<ContactsResponse> GetUsersFriendsRequests(User user)
        {
            if(user != null)
            {
                var usersFriendsRequests = await appDb.AddToContactRequests.Where(r => r.UserToId == user.Id).Include(c => c.UserFrom).ToListAsync();

                var usersFriendsRequestsDTO = mapper.Map<List<AddToContactRequestDTO>>(usersFriendsRequests);

                string message = String.Format("You have {0} friend requests", usersFriendsRequests.Count);
                return new ContactsResponse { AddToContactRequests = usersFriendsRequestsDTO, Message = message, ResponseStatus = Status.Success };
            }
            
            return new ContactsResponse { Users = null, Message = "User was not found", ResponseStatus = Status.Error };
        }

        public async Task<ContactsResponse> SendAddToContactsRequest(User userFrom,string usernameTo)
        {
            var userTo = await userManager.FindByNameAsync(usernameTo);

            if(userFrom == userTo)
            {
                return new ContactsResponse { Message = "Bad request", ResponseStatus = Status.Error };
            }

            if (userTo != null) {
                AddToContactRequest addToContactRequest = new AddToContactRequest
                {
                    UserFromId = userFrom.Id,
                    UserTo = userTo,
                    UserToId = userTo.Id
                };

                var requestExists = await appDb.AddToContactRequests.Where(u => u.UserFromId == userFrom.Id && u.UserToId == userTo.Id).AnyAsync();
                var userContactsList = await appDb.ContactLists.FirstOrDefaultAsync(x => x.UserId == userFrom.Id);
                
                // Check if user already exists in the others contact list
                if (userContactsList != null)
                {
                    if(userContactsList.Contacts.Find(u => u.User.UserName == usernameTo) != null)
                    {
                        return new ContactsResponse { Message = "This user is already in your contacts list",ResponseStatus = Status.Error};
                    }
                }

                if (!requestExists)
                {
                    await appDb.AddToContactRequests.AddAsync(addToContactRequest);
                    await appDb.SaveChangesAsync();
                    return new ContactsResponse { Message = "Friends request sent succesfully", ResponseStatus = Status.Success };
                }

                return new ContactsResponse { Message = "You already sent request to that user", ResponseStatus = Status.Error };
            }
            return new ContactsResponse { Message = "User you are trying to add does not exist", ResponseStatus = Status.Error };
        }

        private async Task<bool> AddUserToContactList(User userToAdd,string contactsListUserId)
        {
            if (userToAdd != null && !String.IsNullOrWhiteSpace(contactsListUserId))
            {
                var userContactsList = await appDb.ContactLists
                    .Where(u => u.UserId == contactsListUserId)
                    .Include(c => c.Contacts)
                    .FirstOrDefaultAsync();
                
                if (userContactsList == null)
                {
                    userContactsList = new ContactList
                    {
                        UserId = contactsListUserId
                    };

                    await appDb.ContactLists.AddAsync(userContactsList);
                    await appDb.SaveChangesAsync();
                }

                if(userContactsList != null)
                {
                    var userContact = userContactsList.Contacts.Where(c => c.UserId == userToAdd.Id).FirstOrDefault();
                    if(userContact != null)
                    {
                        return false;
                    }
                }

                ContactListUser contactListUser = new ContactListUser
                {
                    UserId = userToAdd.Id,
                    ContactListId = userContactsList.Id
                };

                userContactsList.Contacts.Add(contactListUser);

                await appDb.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
