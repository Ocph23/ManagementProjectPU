﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonitoringPU.Models;

namespace MonitoringPU.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
       
        Task<Periode> SavePenilaian(Periode periodeSelected, int id);
        Task AddNewFoto(foto data);
    }
}
