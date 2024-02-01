using AdminProyectos.Models;
using AutoMapper;

namespace AdminProyectos.Utils
{
    public class AutomapperProfiles: Profile
    {
        public AutomapperProfiles() {

            CreateMap<ProyectCreationViewModel,Proyect>().ReverseMap();
            CreateMap<WorkCreationViewModel, Work>().ReverseMap();
        }
    }
}
