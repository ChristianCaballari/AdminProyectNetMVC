using AdminProyectos.Extensions;
using AdminProyectos.Models;
using AdminProyectos.Repositories.implements;
using AdminProyectos.services;
using AdminProyectos.services.implements;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Threading.Tasks;
using static AdminProyectos.Extensions.BaseController;

namespace AdminProyectos.Controllers
{
    public class WorkController : BaseController
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProyectRepository _proyectRepository;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public WorkController(ITaskRepository taskRepository,
            IProyectRepository proyectRepository,
            IUsersService usersService,
            IMapper mapper) {
            this._taskRepository = taskRepository;
            this._proyectRepository = proyectRepository;
            this._usersService = usersService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(int id, PaginationViewModel pagination)
        {
            var userId = _usersService.GetUserId();

            var proyect = await _proyectRepository.GetById(id, userId);
            if (proyect is null) return RedirectToAction("NoEncontrado", "Home");

            var proyects = await _taskRepository.Search(userId, id, pagination);
            var totalTask = await _taskRepository.Count(id);

            var responseViewModel = new PaginationResponse<Work>
            {
                Elements = proyects,
                Page = pagination.page,
                RecordsByPage = pagination.RecordsByPage,
                QuantityTotalRecords = totalTask,
                BaseURL = Url.Action(),
            };
            var indiceWoksProyect = new IndiceWorksProyectViewModel { Proyect = proyect, PaginationResponse = responseViewModel };
            return View(indiceWoksProyect);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var userId = _usersService.GetUserId();
            var proyect = await _proyectRepository.GetById(id, userId);
            if (proyect is null) return RedirectToAction("NoEncontrado", "Home");

            var work = new WorkCreationViewModel { Proyect = proyect };
            work.ProyectId = proyect.Id;
            return View(work);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkCreationViewModel workCreationViewModel) {


            if(!ModelState.IsValid) return View(workCreationViewModel);
            var work = _mapper.Map<Work>(workCreationViewModel);
            await _taskRepository.Create(work);
            CustomNotification("Se ha agregado la tarea correctamente.", NotificationType.Success, $@"Tarea {work.Name} agregada.");
            return RedirectToAction("Index", new { id = work.ProyectId });

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskRepository.GetById(id);
            var userId = _usersService.GetUserId();
            if (task is null) return RedirectToAction("NoEncontrado", "Home");
            var proyect = await  _proyectRepository.GetById(task.ProyectId, userId);
            task.Proyect = proyect;
            var workCreationViewModel = _mapper.Map<WorkCreationViewModel>(task);
            return View(workCreationViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(WorkCreationViewModel workCreationViewModel)
        {
            if (!ModelState.IsValid) {
                return View(workCreationViewModel);
            }

            var task = await _taskRepository.GetById(workCreationViewModel.Id);
            if (task is null) return RedirectToAction("NoEncontrado", "Home");
            var taskDb = _mapper.Map<Work>(workCreationViewModel);
            await _taskRepository.Update(taskDb);
            CustomNotification("Se ha actualizado la Tarea correctamente.", NotificationType.Success, "Tarea actualizada.");
            
            return RedirectToAction("Index", new { id = taskDb.ProyectId });
        }


        [HttpGet]
        public async Task<IActionResult> IndexWorksProyect(int id,PaginationViewModel pagination) 
        {
            var userId = _usersService.GetUserId();
           
            var proyect = await _proyectRepository.GetById(id,userId);//quitar
            if (proyect is null) return RedirectToAction("NoEncontrado", "Home");//quitar
            
            var proyects = await _taskRepository.Search(userId, id, pagination);
            var totalTask = await _taskRepository.Count(id);

            var responseViewModel = new PaginationResponse<Work>
            {
                Elements = proyects,
                Page = pagination.page,
                RecordsByPage = pagination.RecordsByPage,
                QuantityTotalRecords = totalTask,
                BaseURL = Url.Action(),
            };

            if (proyect is null) return RedirectToAction("NoEncontrado", "Home");

            // var works = await _taskRepository.GetAll(userId,id);
            //  var indiceWoksProyect = new IndiceWorksProyectViewModel { Proyect = proyect, Works = works };
            var indiceWoksProyect = new IndiceWorksProyectViewModel { Proyect = proyect, PaginationResponse =  responseViewModel};
            var d = 0;
            return View(indiceWoksProyect);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _taskRepository.GetById(id);
            if (task is null) return RedirectToAction("NoEncontrado", "Home");
            bool delete = await _taskRepository.Delete(task.Id,task.ProyectId);
            if (delete) 
            { 
               CustomNotification("Se ha eliminado la tarea correctamente.", NotificationType.Success, "Proyecto eliminado.");
            }
            return RedirectToAction("Index", new { id = task.ProyectId });
        }
    }
}
