using AutoMapper;
using KeepWriting.Models;
using KeepWriting.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeepWriting
{
    public class AutoMapperBootStrapper
    {
        public static void ConfigureAutoMapper()
        {
            var configCompeticao = new MapperConfiguration(cfg => { cfg.CreateMap<CompeticaoViewModel, Competicao>(); });

            IMapper mapperCompeticao = configCompeticao.CreateMapper();
            var sourceCompeticao= new CompeticaoViewModel();
            var destCompeticao = mapperCompeticao.Map<CompeticaoViewModel, Competicao>(sourceCompeticao);
        }
    }
}