﻿using Data.Entities;

namespace Laboratorium_3___App.Models
{
    public interface IContactService
    {
        void add(Contact contact);
        void RemoveByID(int id);
        void Update(Contact contact);
        List<Contact> FindAll();
        Contact? FindByID(int id);
        List<OrganizationEntity> FindAllOrganization();
        PagingList<Contact> FindPage(int page, int size);
    }
}

