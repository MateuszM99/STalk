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

namespace Services
{
    public class ContactsServices : IContactsServices
    {
        private readonly MainDbContext appDb;
        private readonly UserManager<User> userManager;
        public ContactsServices(MainDbContext appDb, UserManager<User> userManager)
        {
            this.appDb = appDb;
            this.userManager = userManager;
        }
        public async Task<bool> AcceptAddToContactsRequest(User userAcceptingRequest,long addToContactsRequestId)
        {
            var addRequest = await appDb.AddToContactRequests.Where(u => u.UserTo.Id == userAcceptingRequest.Id && u.Id == addToContactsRequestId).FirstOrDefaultAsync();

            if(addRequest != null)
            {
                var userSendingRequest = await userManager.FindByIdAsync(addRequest.UserFromId);
                if(userSendingRequest == null)
                {
                    return false;
                }

                // Add users on bothends to each others contactslist, if they do not have contacts list create a contacts list then add them
                if (await AddUserToContactList(userAcceptingRequest, userSendingRequest.Id) && await AddUserToContactList(userSendingRequest, userAcceptingRequest.Id))
                {
                    appDb.AddToContactRequests.Remove(addRequest);
                    return true;
                }
            }


            return false;
        }

        public async Task<bool> SendAddToContactsRequest(User userFrom,string usernameTo)
        {
            var userTo = await userManager.FindByNameAsync(usernameTo);

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
                        return false;
                    }
                }

                if (!requestExists)
                {
                    await appDb.AddToContactRequests.AddAsync(addToContactRequest);
                    await appDb.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            return false;
        }

        private async Task<bool> AddUserToContactList(User userToAdd,string contactsListUserId)
        {
            if (userToAdd != null && !String.IsNullOrWhiteSpace(contactsListUserId))
            {
                var userContactsList = await appDb.ContactLists.Where(u => u.UserId == contactsListUserId).FirstOrDefaultAsync();
                if (userContactsList == null)
                {
                    userContactsList = new ContactList
                    {
                        UserId = contactsListUserId
                    };

                    await appDb.ContactLists.AddAsync(userContactsList);
                    await appDb.SaveChangesAsync();
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
