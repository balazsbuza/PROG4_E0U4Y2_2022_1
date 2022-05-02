// <copyright file="HospitalLogicBase.cs" company="PlaceholderCompany">
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
    /// Hospital logic main.
    /// </summary>
    public class HospitalLogicBase : IHospitalLogicBase
    {
        private IClinicRepository clinicRepo;
        private IDoctorRepository doctorRepo;
        private IPatientRepository patientRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalLogicBase"/> class.
        /// </summary>
        /// <param name="crepo">clinicRepo.</param>
        /// <param name="drepo">doctorRepo.</param>
        /// <param name="prepo">patientRepo.</param>
        public HospitalLogicBase(IClinicRepository crepo, IDoctorRepository drepo, IPatientRepository prepo)
        {
            this.clinicRepo = crepo;
            this.doctorRepo = drepo;
            this.patientRepo = prepo;
        }

        /// <inheritdoc/>
        public IList<ClinicGender> GetClinicGender()
        {
            var q = from patient in this.patientRepo.GetAll()
                    join doctor in this.doctorRepo.GetAll() on patient.DoctorId equals doctor.DoctorId
                    join clinic in this.clinicRepo.GetAll() on doctor.ClinicId equals clinic.ClinicId
                    let item = new { Clinic = clinic.Name, Gender = patient.Gender, }
                    group item by item.Clinic into grp
                    select new ClinicGender()
                    {
                        ClinicName = grp.Key,
                        NumberofM = grp.Where(x => x.Gender == "M").Count(),
                        NumberofF = grp.Where(x => x.Gender == "F").Count(),
                        NumberofO = grp.Where(x => x.Gender == "O").Count(),
                    };
            return q.ToList();
        }

        /// <summary>
        /// Async method to GetClinicGender.
        /// </summary>
        /// <returns>Task to run the GetClinicGender method.</returns>
        public Task<IList<ClinicGender>> GetClinicGenderAsync()
        {
            return Task.Run(() => this.GetClinicGender());
        }
    }
}
