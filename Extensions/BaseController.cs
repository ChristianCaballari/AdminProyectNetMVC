using Microsoft.AspNetCore.Mvc;

namespace AdminProyectos.Extensions
{
    public class BaseController: Controller
    {
        public enum NotificationType 
        {
            Success,
            Error,
            Question,
            Info
        }

        public void BasicNotification(string message, NotificationType type, string title = "")
        {
            TempData["notification"] = $"Swal.fire('{title}','{message}', '{type.ToString().ToLower()}')";
        }

        public void CustomNotification(string message, NotificationType type, string title = "", string position = "top-end", string showConfirmButton = "false")
        {
            // TempData["notification"] = $"Swal.fire({{position: 'top-end',icon: {type},title: {title}',showConfirmButton: false,timer: 2500}})";

            // TempData["notification"] = "Swal.fire({title: '" + title + "',showClass: {popup: 'animate__animated animate__fadeInDown'},hideClass: {popup: 'animate__animated animate__fadeOutUp'},position: '" + position.ToLower() + "',timer: 5000, icon: '"+type+"',text: '"+ message + "',showConfirmButton:'"+showConfirmButton+"'})";

            TempData["notification"] = "Swal.fire({position: '" + position + "',icon: '" + type.ToString().ToLower() + "',title: '" + title + "',text: '" + message + "',showConfirmButton: '" + false + "',timer: 5000})";

        }
    }
}
