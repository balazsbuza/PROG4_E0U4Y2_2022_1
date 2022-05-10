// <copyright file="PatientTreatmentLogic.cs" company="PlaceholderCompany">
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
    /// Hospital Patient and Doctor logic.
    /// </summary>
    public class PatientTreatmentLogic : IPatientTreatmentLogic
    {
        private IPatientRepository patientRepo;
        private ITreatmentRepository treatmentRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientTreatmentLogic"/> class.
        /// </summary>
        /// <param name="prepo">patientRepo.</param>
        /// <param name="trepo">treatmentRepo.</param>
        public PatientTreatmentLogic(IPatientRepository prepo, ITreatmentRepository trepo)
        {
            this.patientRepo = prepo;
            this.treatmentRepo = trepo;
        }

        /// <inheritdoc/>
        public int AddOnePatient(Patient entity)
        {
            return this.patientRepo.Add(entity);
        }

        /// <inheritdoc/>
        public int AddOneTreatment(Treatment entity)
        {
            return this.treatmentRepo.Add(entity);
        }

        /// <inheritdoc/>
        public void ChangeOnePatientDisease(Patient entity)
        {
            this.patientRepo.ChangeDisease(entity.PatientId, entity.Disease);
        }

        /// <inheritdoc/>
        public void ChangeOneTreatmentDescription(Treatment entity)
        {
            this.treatmentRepo.ChangeDescription(entity.TreatmentId, entity.Description);
        }

        /// <inheritdoc/>
        public IList<Patient> GetAllPatients()
        {
            return this.patientRepo.GetAll().ToList();
        }

        /// <inheritdoc/>
        public IList<Treatment> GetAllTreatments()
        {
            return this.treatmentRepo.GetAll().ToList();
        }

        /// <inheritdoc/>
        public Patient GetOnePatient(int id)
        {
            return this.patientRepo.GetOne(id);
        }

        /// <inheritdoc/>
        public Treatment GetOneTreatment(int id)
        {
            return this.treatmentRepo.GetOne(id);
        }

        /// <inheritdoc/>
        public IList<PatientLastTreatment> GetPatientLastTreatment()
        {
            var t = this.treatmentRepo.GetAll().ToList();
            var p = this.patientRepo.GetAll().ToList();
            var q = from treatment in t
                    join patient in p on treatment.PatientId equals patient.PatientId
                    let item = new { PatientName = patient.Name, Treatmentdate = treatment.Treatmenttime }
                    group item by item.PatientName into grp
                    select new PatientLastTreatment()
                    {
                        PatientName = grp.Key,
                        TreatmentTime = grp.Max(x => x.Treatmentdate),
                    };
            return q.ToList();
        }

        public IList<PatientTreatmentLastYear> GetPatientTreatmentLastYear()
        {
            var t = this.treatmentRepo.GetAll().ToList();
            var p = this.patientRepo.GetAll().ToList();
            DateTime year = DateTime.Now.AddYears(-1);
            var q = from treatment in t
                    join patient in p on treatment.PatientId equals patient.PatientId
                    let item = new { PatientName = patient.Name, Treatmentdate = treatment.Treatmenttime }
                    group item by item.PatientName into grp
                    select new PatientTreatmentLastYear()
                    {
                        PatientName = grp.Key,
                        LastYear = grp.Where(x => x.Treatmentdate > year).Any(),
                    };
            return q.ToList();
        }

        /// <summary>
        /// Async method to GetPatientLastTreatment.
        /// </summary>
        /// <returns>Task to run the GetPatientLastTreatment method.</returns>
        public Task<IList<PatientLastTreatment>> GetPatientLastTreatmentAsync()
        {
            return Task.Run(() => this.GetPatientLastTreatment());
        }

        /// <inheritdoc/>
        public bool RemoveOnePatient(int id)
        {
            return this.patientRepo.Remove(id);
        }

        /// <inheritdoc/>
        public bool RemoveOneTreatment(int id)
        {
            return this.treatmentRepo.Remove(id);
        }
    }
}
