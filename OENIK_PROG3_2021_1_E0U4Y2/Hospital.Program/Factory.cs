// <copyright file="Factory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hospital.Program
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hospital.Data.Tables;
    using Hospital.Logic;
    using Hospital.Repository;

    /// <summary>
    /// Factory for initiating.
    /// </summary>
    public class Factory
    {
        private HospitalDbContext ctx;
        private ClinicRepository crepo;
        private DoctorRepository drepo;
        private PatientRepository prepo;
        private TreatmentRepository trepo;
        private HospitalLogicBase hlogic;
        private ClinicDoctorLogic cdlogic;
        private PatientTreatmentLogic ptlogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="Factory"/> class.
        /// Factory for initiating.
        /// </summary>
        public Factory()
        {
            this.ctx = new HospitalDbContext();
            this.crepo = new ClinicRepository(this.ctx);
            this.drepo = new DoctorRepository(this.ctx);
            this.prepo = new PatientRepository(this.ctx);
            this.trepo = new TreatmentRepository(this.ctx);
            this.hlogic = new HospitalLogicBase(this.crepo, this.drepo, this.prepo);
            this.cdlogic = new ClinicDoctorLogic(this.crepo, this.drepo);
            this.ptlogic = new PatientTreatmentLogic(this.prepo, this.trepo);
        }

        /// <summary>
        /// Gets HospitalLogicBase to use.
        /// </summary>
        /// <returns>HospitalLogicBase.</returns>
        public HospitalLogicBase GetHospitalLogicBase()
        {
            return this.hlogic;
        }

        /// <summary>
        /// Gets ClinicDoctorLogic to use.
        /// </summary>
        /// <returns>ClinicDoctorLogic.</returns>
        public ClinicDoctorLogic GetClinicDoctorLogic()
        {
            return this.cdlogic;
        }

        /// <summary>
        /// Gets PatientTreatmentLogic to use.
        /// </summary>
        /// <returns>PatientTreatmentLogic.</returns>
        public PatientTreatmentLogic GetPatientTreatmentLogic()
        {
            return this.ptlogic;
        }
    }
}
