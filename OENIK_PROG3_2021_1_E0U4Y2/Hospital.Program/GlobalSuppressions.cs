// <copyright file="GlobalSuppressions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1024:Use properties where appropriate", Justification = "<NikGitStats>", Scope = "member", Target = "~M:Hospital.Program.Factory.GetHospitalLogicBase~Hospital.Logic.HospitalLogicBase")]
[assembly: SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable", Justification = "<NikGitStats>", Scope = "type", Target = "~T:Hospital.Program.Factory")]
[assembly: SuppressMessage("Design", "CA1024:Use properties where appropriate", Justification = "<NikGitStats>", Scope = "member", Target = "~M:Hospital.Program.Factory.GetClinicDoctorLogic~Hospital.Logic.ClinicDoctorLogic")]
[assembly: SuppressMessage("Design", "CA1024:Use properties where appropriate", Justification = "<NikGitStats>", Scope = "member", Target = "~M:Hospital.Program.Factory.GetPatientTreatmentLogic~Hospital.Logic.PatientTreatmentLogic")]
