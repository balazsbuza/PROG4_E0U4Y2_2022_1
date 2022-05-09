// <copyright file="ClinicDoctorLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hospital.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hospital.Data.Tables;
    using Hospital.Repository.Repositories;

    /// <summary>
    /// Hospital Clinic and Doctor logic.
    /// </summary>
    public class ClinicDoctorLogic : IClinicDoctorLogic
    {
        private IClinicRepository clinicRepo;
        private IDoctorRepository doctorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicDoctorLogic"/> class.
        /// </summary>
        /// <param name="crepo">clinicRepo.</param>
        /// <param name="drepo">doctorRepo.</param>
        public ClinicDoctorLogic(IClinicRepository crepo, IDoctorRepository drepo)
        {
            this.clinicRepo = crepo;
            this.doctorRepo = drepo;
        }

        /// <inheritdoc/>
        public int AddOneClinic(Clinic entity)
        {
            return this.clinicRepo.Add(entity);
        }

        /// <inheritdoc/>
        public int AddOneDoctor(Doctor entity)
        {
            return this.doctorRepo.Add(entity);
        }

        /// <inheritdoc/>
        public void ChangeOneClinicAddress(Clinic entity)
        {
            this.clinicRepo.ChangeAdress(entity.ClinicId, entity.Address);
        }

        /// <inheritdoc/>
        public void ChangeOneDoctorDegree(Doctor entity)
        {
            this.doctorRepo.ChangeDegree(entity.DoctorId, entity.Degree);
        }

        /// <inheritdoc/>
        public IList<Clinic> GetAllClinics()
        {
            return this.clinicRepo.GetAll().ToList();
        }

        /// <inheritdoc/>
        public IList<Doctor> GetAllDoctors()
        {
            return this.doctorRepo.GetAll().ToList();
        }

        /// <inheritdoc/>
        public IList<DoctorWorkaddress> GetDoctorWorkAddress()
        {
            var q = from doctor in this.doctorRepo.GetAll()
                    join clinic in this.clinicRepo.GetAll() on doctor.ClinicId equals clinic.ClinicId
                    let item = new { ClinicAddress = clinic.Address, DoctorName = doctor.Name }
                    group item by item.ClinicAddress into grp
                    select new DoctorWorkaddress()
                    {
                        ClinicAddress = grp.Key,
                        NumberOfDoctors = grp.Count(),
                    };
            return q.ToList();
        }

        /// <summary>
        /// Async method to GetDoctorWorkAddress.
        /// </summary>
        /// <returns>Task to run the GetDoctorWorkAddress method.</returns>
        public Task<IList<DoctorWorkaddress>> GetDoctorWorkAddressAsync()
        {
            return Task.Run(() => this.GetDoctorWorkAddress());
        }

        /// <inheritdoc/>
        public Clinic GetOneClinic(int id)
        {
            return this.clinicRepo.GetOne(id);
        }

        /// <inheritdoc/>
        public Doctor GetOneDoctor(int id)
        {
            return this.doctorRepo.GetOne(id);
        }

        /// <inheritdoc/>
        public bool RemoveOneClinic(int id)
        {
            return this.clinicRepo.Remove(id);
        }

        /// <inheritdoc/>
        public bool RemoveOneDoctor(int id)
        {
            return this.doctorRepo.Remove(id);
        }
    }
}
