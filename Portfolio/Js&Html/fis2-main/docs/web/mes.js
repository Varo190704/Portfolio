class Mes {
  constructor(nombre) {
    this.nombre = nombre; // Nombre del mes
    this.dias = []; // Lista de días del mes
    this.porcentaje = 0; // Porcentaje completado del mes
  }

  agregarDia(dia) {
    // Agrega un día a la lista de días
    this.dias.push(dia);
    this.actualizarPorcentaje();
  }

  actualizarPorcentaje() {
    // Calcula el porcentaje total basado en los temas completados de los días
    let totalTemas = 0;
    let temasCompletados = 0;

    this.dias.forEach((dia) => {
      totalTemas += dia.temasADar.length + dia.temasDados.length;
      temasCompletados += dia.temasDados.length;
    });

    this.porcentaje =
      totalTemas > 0 ? ((temasCompletados / totalTemas) * 100).toFixed(2) : 0;
  }

  calcularProgresoMensual() {
    // Calcula el progreso basado en los porcentajes de los días
    if (this.dias.length === 0) return 0;

    const porcentajeTotal = this.dias.reduce((acumulador, dia) => {
      return acumulador + dia.porcentajeCompletado;
    }, 0);

    return (porcentajeTotal / this.dias.length).toFixed(2); // Promedio del progreso de los días
  }

  obtenerDia(diaNum) {
    return this.dias[diaNum - 1]; // Los días se indexan desde 0
  }

  mostrarResumenMensual() {
    console.log(`Resumen del mes: ${this.nombre}`);
    this.dias.forEach((dia, index) => {
      console.log(`Día ${index + 1}:`);
      dia.mostrarResumen();
    });
    console.log(`Progreso total del mes: ${this.porcentaje}%`);
  }
}

export default Mes; 