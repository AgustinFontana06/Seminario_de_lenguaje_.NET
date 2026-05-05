using SGE;

//prueba de funcionamiento
var caratula = new Caratula("hola");

Guid id = Guid.NewGuid();

var expediente = new Expediente(caratula, id);

Console.WriteLine(expediente.ToString());