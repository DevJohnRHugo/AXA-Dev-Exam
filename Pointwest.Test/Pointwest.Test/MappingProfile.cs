using AutoMapper;
using Pointwest.Test.Dtos;
using Pointwest.Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Test
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicantDto, Applicant>();
            CreateMap<Applicant, ApplicantDto>();

            CreateMap<FileUploadDto, FileUpload>();
            CreateMap<FileUpload, FileUploadDto> ();

            CreateMap<FileDto, File>();
            CreateMap<File, FileDto>();

            CreateMap<ScheduleDto, Schedule>();
            CreateMap<Schedule, ScheduleDto>();
        }
    }
}
