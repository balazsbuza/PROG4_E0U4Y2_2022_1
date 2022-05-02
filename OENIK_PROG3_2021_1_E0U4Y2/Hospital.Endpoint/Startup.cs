using Hospital.Data.Tables;
using Hospital.Logic;
using Hospital.Repository;
using Hospital.Repository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Endpoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddRazorPages();
        //}

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<HospitalDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddTransient<HospitalDbContext, HospitalDbContext>();

            //services.AddTransient<IRepository<Clinic>, RepositoryBase<Clinic>>();
            services.AddTransient<IRepository<Clinic>, ClinicRepository>();
            services.AddTransient<IClinicRepository, ClinicRepository>();

            //services.AddTransient<IRepository<Doctor>, RepositoryBase<Doctor>>();
            services.AddTransient<IRepository<Doctor>, DoctorRepository>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();

            //services.AddTransient<IRepository<Patient>, RepositoryBase<Patient>>();
            services.AddTransient<IRepository<Patient>, PatientRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();

            //services.AddTransient<IRepository<Treatment>, RepositoryBase<Treatment>>();
            services.AddTransient<IRepository<Treatment>, TreatmentRepository>();
            services.AddTransient<ITreatmentRepository, TreatmentRepository>();

            services.AddTransient<IHospitalLogicBase, HospitalLogicBase>();
            services.AddTransient<IClinicDoctorLogic, ClinicDoctorLogic>();
            services.AddTransient<IPatientTreatmentLogic, PatientTreatmentLogic>();

            services.AddControllers(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
