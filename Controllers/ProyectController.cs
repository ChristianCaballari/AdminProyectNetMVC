using AdminProyectos.Extensions;
using AdminProyectos.Models;
using AdminProyectos.services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminProyectos.Controllers
{
    public class ProyectController : BaseController
    {
        private readonly IProyectRepository _proyectRepository;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public ProyectController(IProyectRepository proyectRepository,
            IUsersService usersService,
            IMapper mapper) {
            this._proyectRepository = proyectRepository;
            this._usersService = usersService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(PaginationViewModel pagination)
        {
            var userId = _usersService.GetUserId();
            var proyects = await _proyectRepository.Search(userId, pagination);
            var totalProyects = await _proyectRepository.Count(userId);

            var responseViewModel = new PaginationResponse<Proyect>
            {
                Elements = proyects,
                Page     = pagination.page,
                RecordsByPage = pagination.RecordsByPage,
                QuantityTotalRecords = totalProyects,
                BaseURL = Url.Action(),
            };
            
            return View(responseViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProyectCreationViewModel proyectCreationViewModel)
        {
            if (!ModelState.IsValid) {
                return View(proyectCreationViewModel);
            }

            var userId = _usersService.GetUserId();
            var proyect = _mapper.Map<Proyect>(proyectCreationViewModel);
            proyect.UserId = userId;
            
            await _proyectRepository.Create(proyect);
            CustomNotification("Se ha creado el proyecto correctamente.", NotificationType.Success, "Proyecto creado.");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var userId = _usersService.GetUserId();
            var proyectDB = await _proyectRepository.GetById(id, userId);
            if (proyectDB is null) { 
                return RedirectToAction("NoEncontrado","Home");
            }
            var proyect = _mapper.Map<ProyectCreationViewModel>(proyectDB);
    
            return View(proyect);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProyectCreationViewModel proyectCreationViewModel) 
        {
            if(!ModelState.IsValid) return View(proyectCreationViewModel);

            var userId = _usersService.GetUserId();
            var proyectDB = await _proyectRepository.GetById(proyectCreationViewModel.Id, userId);
            if (proyectDB is null) return RedirectToAction("NoEncontrado","Home");
            var proyect = _mapper.Map<Proyect>(proyectCreationViewModel);
            proyect.UserId = userId;

            await _proyectRepository.Update(proyect);
            CustomNotification("Se ha actualizado el Proyecto correctamente.", NotificationType.Success, "Proyecto actualizado.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProyect(int id)
        {
            var userId = _usersService.GetUserId();
            var proyect = await _proyectRepository.GetById(id, userId);
            if (proyect == null)
            {
                return RedirectToAction("NoEncontrado","Home");
            }
            bool delete = await _proyectRepository.Delete(id);
            if (delete)
            {
                CustomNotification("Se ha eliminado el proyecto correctamente.", NotificationType.Success, "Proyecto eliminado.");

            }
            return RedirectToAction("Index");
        }
    }
}
