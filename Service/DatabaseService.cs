﻿using SQLite;
using KKDK_Parcel_Delivery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace KKDK_Parcel_Delivery.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        // Path to the SQLite database file
        public DatabaseService()
        {
            // Create the SQLite database file in the device's local storage
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "KKDKParcelDelivery.db3");
            _database = new SQLiteAsyncConnection(databasePath);
            _database.CreateTableAsync<Student>().Wait(); // Create the Student table if not exists
            _database.CreateTableAsync<Admin>().Wait(); // Create the Admin table if not exists
            _database.CreateTableAsync<Parcel>().Wait(); // Create the Parcel table if not exists
        }

        // Insert a new Student
        public async Task<int> InsertStudentAsync(Student student)
        {
            return await _database.InsertAsync(student);
        }

        // Insert a new Admin
        public async Task<int> InsertAdminAsync(Admin admin)
        {
            return await _database.InsertAsync(admin);
        }

        public async Task<int> DeleteAdminAsync(Admin admin)
        {
            if (admin != null)
            {
                // Perform the delete operation
                return await _database.DeleteAsync(admin);
            }
            return 0;
        }

        public async Task<int> UpdateAdminAsync(Admin admin)
        {
            if (admin != null)
            {
                // Perform the update operation
                return await _database.UpdateAsync(admin);
            }
            return 0;
        }

        // Insert a new Parcel
        public async Task<int> InsertParcelAsync(Parcel parcel)
        {
            return await _database.InsertAsync(parcel);
        }

        // Get all Students
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _database.Table<Student>().ToListAsync();
        }

        // Get all Admins
        public async Task<List<Admin>> GetAdminsAsync()
        {
            return await _database.Table<Admin>().ToListAsync();
        }

        // Get all Parcels
        public async Task<List<Parcel>> GetParcelsAsync()
        {
            return await _database.Table<Parcel>().ToListAsync();
        }

        // Get parcels for a specific student
        public async Task<List<Parcel>> GetParcelsForStudentAsync(int studentId)
        {
            return await _database.Table<Parcel>().Where(p => p.StudentId == studentId).ToListAsync();
        }

        // Get a specific Student by ID
        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            return await _database.Table<Student>().Where(s => s.StudentId == studentId).FirstOrDefaultAsync();
        }

        // Get a specific Student by Matric Number
        public async Task<Student> GetStudentByMatricNumAsync(string matricNum)
        {
            return await _database.Table<Student>().Where(s => s.MatricNum == matricNum).FirstOrDefaultAsync();
        }

        // Update a Student's information
        public async Task<int> UpdateStudentAsync(Student student)
        {
            return await _database.UpdateAsync(student);
        }

        // Update a Parcel's status
        public async Task<int> UpdateParcelStatusAsync(Parcel parcel)
        {
            var existingParcel = await _database.Table<Parcel>().FirstOrDefaultAsync(p => p.TrackingNumber == parcel.TrackingNumber);
            if (existingParcel != null)
            {
                existingParcel.ParcelStatus = parcel.ParcelStatus;
                return await _database.UpdateAsync(existingParcel);  // Update the parcel in the database
            }
            return 0;  // No rows updated
        }

        // Delete a Student
        public async Task<int> DeleteStudentAsync(Student student)
        {
            return await _database.DeleteAsync(student);
        }

        // Delete a Parcel
        public async Task<int> DeleteParcelAsync(Parcel parcel)
        {
            return await _database.DeleteAsync(parcel);
        }

        // Get a specific Parcel by ID
        public async Task<Parcel> GetParcelByIdAsync(int parcelId)
        {
            return await _database.Table<Parcel>().Where(p => p.ParcelId == parcelId).FirstOrDefaultAsync();
        }

        public async Task<List<Parcel>> GetAllParcelsAsync()
        {
            return await _database.Table<Parcel>().ToListAsync();
        }

       
    }
}
