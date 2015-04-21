﻿using AutoMapper;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Curriculum;

namespace PmaPlus.Mapping
{
    class ModelToViewModelProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ModelToViewModelProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<CurriculumType, CurriculumTypeViewModel>();
          

        }
    }
}
