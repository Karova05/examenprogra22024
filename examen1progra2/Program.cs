// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Decidi implementar listas para un mejor y facil manejo de las opciones

    static List<Paciente> pacientes = new List<Paciente>();
    static List<Medicamento> catalogoMedicamentos = new List<Medicamento>();
    static List<TratamientoMedico> tratamientos = new List<TratamientoMedico>();

    static void Main(string[] args) // sin string[] args la opcion de salir no me funcionaba
    {
        int opcion;
        do
        {
            MostrarMenu();
            opcion = ObtenerOpcion();

            switch (opcion)
            {
                case 1:
                    AgregarPaciente();
                    break;
                case 2:
                    AgregarMedicamento();
                    break;
                case 3:
                    AsignarTratamientoMedico();
                    break;
                case 4:
                    MostrarConsultas();
                    break;
                case 5:
                    Console.WriteLine("Saliendo del sistema.");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }

        } while (opcion != 5); // El programa se seguira ejecutando hasta que el usuario elija la opción de salir (5)
    }

    static int ObtenerOpcion() // esto por si por error o la persona no lee bien, ingresa un numero que no es 
    {
        Console.Write("Ingrese su opción: ");

        int opcion;

        while (!int.TryParse(Console.ReadLine(), out opcion))
        {
            Console.WriteLine("Opción no válida. Por favor, ingrese un número válido.");

            Console.Write("Ingrese su opción: ");
        }

        return opcion;
    }

    static void MostrarMenu() // Al final escogi un metodo para el menu, lo intente con switch y no me trabajaba como esperaba, muchas opciones no las trabajaba correctamente
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("************ CLINICA DRA. KARLA RODRIGUEZ **********************");
        Console.WriteLine("************ DEPARTAMENTO DE ADMISION Y GESTION DE PACIENTES **********************");
        Console.WriteLine("Menú Principal:");
        Console.WriteLine("1- Agregar paciente");
        Console.WriteLine("2- Agregar medicamento al catálogo");
        Console.WriteLine("3- Asignar tratamiento médico a un paciente");
        Console.WriteLine("4- Consultas");
        Console.WriteLine("5- Salir");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Seleccione una opción: ");
    }

    static void AgregarPaciente()
    {
        string respuesta;


        do
        {
            Console.WriteLine("Agregar Paciente:");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Cédula: ");
            string cedula = Console.ReadLine();
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Teléfono (8 dígitos): ");
            string telefono;
            do
            {
                telefono = Console.ReadLine();
                if (telefono.Length != 8 || !telefono.All(char.IsDigit))
                {
                    Console.WriteLine("Número de teléfono inválido. Debe tener exactamente 8 dígitos.");

                    Console.Write("Ingrese el número de teléfono nuevamente: ");
                }
            } while (telefono.Length != 8 || !telefono.All(char.IsDigit));
            Console.Write("Tipo de sangre: ");
            string tipoSangre = Console.ReadLine();
            Console.Write("Dirección: ");
            string direccion = Console.ReadLine();
            Console.Write("Fecha de Nacimiento (yyyy-mm-dd): ");

            if(DateTime.TryParse(Console.ReadLine(), out DateTime fechaNacimiento))
            {

                Paciente nuevoPaciente = new Paciente(cedula, nombre, telefono, tipoSangre, direccion, fechaNacimiento);
                pacientes.Add(nuevoPaciente);

                Console.WriteLine("Paciente agregado exitosamente.");
            }
            else
            {
                Console.WriteLine("Fecha de nacimiento inválida. El paciente no fue agregado.");
            }

            Console.Write("¿Desea agregar otro paciente? (Si/No): ");

            respuesta = Console.ReadLine().ToLower(); // tolower para que sea sensitivo a como lo escriba el usuario

            if (respuesta != "si" && respuesta != "no")
            {
                Console.WriteLine("Respuesta no válida. Saliendo de las opciones.");
                break;
            }

        } while (respuesta == "si");
    }
    static void AgregarMedicamento()
    {
        string respuesta;

        do
        {
            Console.WriteLine("Agregar Medicamento:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Código del medicamento (alfanumérico): ");
            string codigo = Console.ReadLine();
            Console.Write("Nombre del medicamento: ");
            string nombre = Console.ReadLine();
            Console.Write("Cantidad: ");

            // Validar entrada de cantidad
            if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad >= 0)
            {
                Medicamento nuevoMedicamento = new Medicamento(codigo, nombre, cantidad);
                catalogoMedicamentos.Add(nuevoMedicamento);

                Console.WriteLine("Medicamento agregado exitosamente.");
            }
            else
            {
                Console.WriteLine("Cantidad inválida. El medicamento no fue agregado.");
            }

            Console.Write("¿Desea agregar otro medicamento? (Si/No): ");
            respuesta = Console.ReadLine().ToLower();

            if (respuesta != "si" && respuesta != "no")
            {
                Console.WriteLine("Respuesta no válida. Saliendo de las opciones.");
                break;
            }

        } while (respuesta == "si");
    }

    static void AsignarTratamientoMedico()
    {
        Console.WriteLine("Asignar Tratamiento Médico a un Paciente:");

        if (pacientes.Count == 0 || catalogoMedicamentos.Count == 0) // count para obtener el tamaño de la colección
        {
            Console.WriteLine("No hay pacientes o medicamentos registrados para asignar tratamientos.");
            return;
        }

        Console.Write("Ingrese el número de cédula del paciente: ");
        string cedulaBuscar = Console.ReadLine().Trim();  // se utiliza para eliminar los caracteres en blanco al inicio y al final

        // Buscar paciente por cédula, ignorando mayúsculas y minúsculas
        // find Busca un elemento que coincida con las condiciones definidas
        // equals Determina si el objeto especificado es igual que el objeto actual.
        // StringComparison.OrdinalIgnoreCase Mejora la flexibilidad y precisión de las comparaciones
        Paciente pacienteSeleccionado = pacientes.Find(p => p.Cedula.Equals(cedulaBuscar, StringComparison.OrdinalIgnoreCase));

        if (pacienteSeleccionado == null)
        {
            Console.WriteLine("Paciente no encontrado.");
            return;
        }

        if (pacienteSeleccionado.Cedula != null)
        {
            
            Console.WriteLine($"Asignar medicamentos a {pacienteSeleccionado.Nombre} con cédula {pacienteSeleccionado.Cedula}:");

            
        }
        else
        {
            Console.WriteLine("La cédula del paciente es null.");
            return;
        }

        Console.WriteLine($"Asignar medicamentos a {pacienteSeleccionado.Nombre}:");

        List<Medicamento> medicamentosAsignados = new List<Medicamento>();

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Seleccione medicamento {i + 1}:");

            for (int j = 0; j < catalogoMedicamentos.Count; j++)
            {
                Console.WriteLine($"{j + 1}. {catalogoMedicamentos[j].Nombre} (Cantidad: {catalogoMedicamentos[j].Cantidad})");
            }

            int indiceMedicamento;

            // out busco datos desde un procedimiento almacenado y voy asignando a cada lista el valor del reader
            // indice Busca el objeto especificado y devuelve el índice de base cero de la primera aparición dentro del intervalo de elementos de List<T> que comienza en el índice
            while (!int.TryParse(Console.ReadLine(), out indiceMedicamento) || indiceMedicamento < 1 || indiceMedicamento > catalogoMedicamentos.Count)
            {
                Console.WriteLine("Índice de medicamento no válido. Ingrese un número válido.");
                Console.Write($"Seleccione medicamento {i + 1}: ");
            }

            Medicamento medicamentoSeleccionado = catalogoMedicamentos[indiceMedicamento - 1];

            if (medicamentoSeleccionado.Cantidad <= 0)
            {
                Console.WriteLine("No hay existencias suficientes del medicamento seleccionado.");
                i--;
                continue;
            }

            Console.Write($"Ingrese la cantidad de {medicamentoSeleccionado.Nombre} a asignar: ");
            int cantidadAsignar;

            // Validar la entrada del usuario para asegurarse de que sea un número positivo
            while (!int.TryParse(Console.ReadLine(), out cantidadAsignar) || cantidadAsignar <= 0 || cantidadAsignar > medicamentoSeleccionado.Cantidad)
            {
                Console.WriteLine("Cantidad no válida. Asegúrese de ingresar una cantidad válida y disponible.");
                Console.Write($"Ingrese la cantidad de {medicamentoSeleccionado.Nombre} a asignar: ");
            }

            medicamentosAsignados.Add(new Medicamento(medicamentoSeleccionado.Codigo, medicamentoSeleccionado.Nombre, cantidadAsignar));
            medicamentoSeleccionado.Cantidad -= cantidadAsignar;
        }

        TratamientoMedico nuevoTratamiento = new TratamientoMedico(pacienteSeleccionado, medicamentosAsignados);
        tratamientos.Add(nuevoTratamiento);

        Console.WriteLine("Tratamiento médico asignado exitosamente.");
    }
    static void MostrarConsultas()
    {
        Console.WriteLine("Consultas:");

        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.WriteLine($"1. Cantidad total de pacientes registrados: {pacientes.Count}");

        Console.WriteLine("2. Reporte de todos los medicamentos recetados sin repetir:");
        // SelectMany El método enumera la secuencia de entrada, utiliza una función de transformación para asignar cada elemento a un IEnumerable<T> y luego enumera y produce los elementos de cada uno de esos objetos
        // Distinct El método elimina los elementos duplicados de una secuencia (lista) y devuelve los elementos distintos de una única fuente de datos.
        var medicamentosRecetados = tratamientos.SelectMany(t => t.Medicamentos).Distinct();
        foreach (var medicamento in medicamentosRecetados)
        {
            Console.WriteLine($"- {medicamento.Nombre}");
        }

        Console.WriteLine("3. Reporte de cantidad de pacientes agrupados por edades:");

        int pacientes0a10 = pacientes.Count(p => CalcularEdad(p.FechaNacimiento) <= 10);
        int pacientes11a30 = pacientes.Count(p => CalcularEdad(p.FechaNacimiento) > 10 && CalcularEdad(p.FechaNacimiento) <= 30);
        int pacientes31a50 = pacientes.Count(p => CalcularEdad(p.FechaNacimiento) > 30 && CalcularEdad(p.FechaNacimiento) <= 50);
        int pacientes51mas = pacientes.Count(p => CalcularEdad(p.FechaNacimiento) > 50);

        Console.WriteLine($"- 0-10 años: {pacientes0a10} pacientes");
        Console.WriteLine($"- 11-30 años: {pacientes11a30} pacientes");
        Console.WriteLine($"- 31-50 años: {pacientes31a50} pacientes");
        Console.WriteLine($"- Mayores de 51 años: {pacientes51mas} pacientes");

        Console.WriteLine("4. Reporte Pacientes y consultas ordenado por nombre:");
        var pacientesOrdenados = pacientes.OrderBy(p => p.Nombre);
        foreach (var paciente in pacientesOrdenados)
        {
            Console.WriteLine($"- {paciente.Nombre} (Cédula: {paciente.Cedula})");
            var tratamientosPaciente = tratamientos.Where(t => t.Paciente == paciente);
            foreach (var tratamiento in tratamientosPaciente)
            {
                Console.WriteLine($"  - Consulta el {tratamiento.Fecha.ToShortDateString()}");
                foreach (var medicamento in tratamiento.Medicamentos)
                {
                    Console.WriteLine($"    - {medicamento.Nombre}");
                }
            }
        }
    }

    static int CalcularEdad(DateTime fechaNacimiento)
    {
        DateTime fechaActual = DateTime.Now;
        int edad = fechaActual.Year - fechaNacimiento.Year;
        if (fechaActual.Month < fechaNacimiento.Month || (fechaActual.Month == fechaNacimiento.Month && fechaActual.Day < fechaNacimiento.Day))
        {
            edad--;
        }
        return edad;
    }
}

class Paciente
{

    public string Cedula { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string TipoSangre { get; set; }
    public string Direccion { get; set; }
    public DateTime FechaNacimiento { get; set; }

    public Paciente(string cedula, string nombre, string telefono, string tipoSangre, string direccion, DateTime fechaNacimiento)
    {
        Cedula = cedula;
        Nombre = nombre;
        Telefono = telefono;
        TipoSangre = tipoSangre;
        Direccion = direccion;
        FechaNacimiento = fechaNacimiento;
    }
}

class Medicamento
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public int Cantidad { get; set; }

    public Medicamento(string codigo, string nombre, int cantidad)
    {
        Codigo = codigo;
        Nombre = nombre;
        Cantidad = cantidad;
    }
}

class TratamientoMedico
{
    public Paciente Paciente { get; set; }
    public List<Medicamento> Medicamentos { get; set; }
    public DateTime Fecha { get; }

    public TratamientoMedico(Paciente paciente, List<Medicamento> medicamentos)
    {
        Paciente = paciente;
        Medicamentos = medicamentos;
        Fecha = DateTime.Now;
    }
}
