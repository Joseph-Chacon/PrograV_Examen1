using System.ComponentModel;
using TaskManagementLibrary; //RETO 1 (1 pt): La linea está bien, pero no funciona, corregir !!!Completado

class Program
{
    static bool confirme(string accion)
    {
        Console.WriteLine("Confirme " + accion + " s/n");
        return Console.ReadLine() == "s";
    } 
    static void Main(string[] args)
    {
        var taskService = new TaskService();

        while (true)
        {
            Console.WriteLine("1. Agregar tarea");
            Console.WriteLine("2. Ver tareas");
            Console.WriteLine("3. Actualizar tarea");
            Console.WriteLine("4. Eliminar tarea");
            Console.WriteLine("5. Completar tarea");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Titulo: ");
                    var title = Console.ReadLine();                       
                    Console.Write("Descripcion: ");
                    var description = Console.ReadLine();
                    /* RETO 2 (3 pts) : previo a agregar la tarea, verificar que 
                    los datos no sean solo espacios en blanco, de ser así 
                    asignar nulo al dato ( title o description)
                    !!!Completado
                    */
                    if (string.IsNullOrWhiteSpace(title)) {
                        title = null;
                    }
                    if (string.IsNullOrWhiteSpace(description)) {
                        description = null;
                    }
                    
                    var task = taskService.AddTask(title, description);
                    Console.WriteLine($"Tarea agregada con Id: {task.Id}");
                    break;
                case "2":
                    var tasks = taskService.GetAllTasks();
                    Console.WriteLine("-------------------------------------------------");
                    foreach (var t in tasks)
                    {
                        Console.WriteLine($"ID: {t.Id}, Titulo: {t.Title}, Descripcion: {t.Description}, Completada: {t.IsCompleted}");
                    }
                    Console.WriteLine("-------------------------------------------------");
                    break;
                case "3":
                    Console.Write("Introduzca el Id de la tarea por actualizar: ");
                    var updateId = int.Parse(Console.ReadLine());
                    task = taskService.GetTaskById(updateId); // RETO 3 (2 pts): corregir, debe cargarse con la tarea que posea el id indicado en updateId  !!!Completado
                    //RETO 4 (1 pt): imprimir el titulo de la tarea seleccionada !!!Completado
                    Console.Write($"{task.Title}-> Nuevo titulo: ");
                    var newTitle = Console.ReadLine();
                    //RETO 5 (1 pt): imprimir la descripcion de la tarea seleccionada !!!Completado
                    Console.Write($"{task.Description}-> Nueva Descripcion: ");
                    var newDescription = Console.ReadLine();
                    Console.Write("Completada (true/false): ");
                    var isCompleted = bool.Parse(Console.ReadLine());
                    //RETO 6 ( 5 pts ) El código debe modificarse en la librería, de tal forma que si se recibe title vacio
                    // entonces no se modifique, lo mismo para description
                    // !!!Completado
                    if (taskService.UpdateTask(updateId, newTitle, newDescription, isCompleted))
                    {
                        Console.WriteLine("Tarea completada exitosamente.");
                        
                    }
                    else
                    {
                        Console.WriteLine("Tarea no encontrada.");
                    }
                    break;
                case "4":
                    Console.Write("Introduzca el Id de la tarea a eliminar: ");
                    var deleteId = 0;
                    try{
                        deleteId = int.Parse(Console.ReadLine());
                      }                      
                    catch {
                        break;
                    }
                    
                    task = taskService.GetTaskById(deleteId); //RETO 7 (2 pts): La linea está bien, pero por alguna razón no se puede realizar el llamado de la función  !!!Completado
                    Console.WriteLine("Tarea:");
                    Console.Write("     - ID: ");
                    Console.WriteLine(task.Id);
                    Console.Write("     - Titulo: ");
                    Console.WriteLine(task.Title);
                    Console.Write("     - Descripción: ");
                    Console.WriteLine(task.Description);
                    if (confirme("eliminar"))
                    {
                        if (taskService.DeleteTask(deleteId))
                        {
                            Console.WriteLine("Tarea eliminada exitosamente.");
                        }
                        else
                        {
                            Console.WriteLine("Tarea no encontrada.");
                        }
                    }    
                    break;
                case "5":
                    //RETO 8 (5 pts) crear la funcionalidad completa del método
                    /* - pedir el id de la tarea y almacenarlo en completeId
                       - verificar que el id exista, si no existe indicarlo
                       - si existe imprimir el título del mismo
                       - utilizar alguna de las funciones de taskService para dar como completada la tarea, no se puede crear una nueva
                    */
                    Console.Write("Introduzca el Id de la tarea por confirmar: ");
                    var completeId = int.Parse(Console.ReadLine());
                    task = taskService.GetTaskById(completeId);
                    if (task == null) {
                        Console.WriteLine("El ID indicado no existe");
                    } else {
                        Console.WriteLine(task.Title);
                        Console.WriteLine(task.Description);
                        Console.Write("Completada (true/false): ");
                        var taskComplete = bool.Parse(Console.ReadLine());

                        if (taskService.UpdateTask(completeId, task.Title, task.Description, taskComplete)) {
                        Console.WriteLine("Tarea completada exitosamente.");
                        }
                    }
                    break;    
                case "6":
                    return;
                default:
                    Console.WriteLine("Opcion invalida, intente de nuevo.");
                    break;
            }
        }
    }
}
