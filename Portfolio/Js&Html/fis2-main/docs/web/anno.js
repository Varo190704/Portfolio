class Anno {
  constructor(numero) {
    this.numero = numero; // Identificador del año
    this.meses = []; // Lista de meses en el año
    this.porcentaje = 0; // Progreso general del año
  }

  agregarMes(mes) {
    this.meses.push(mes);
    this.actualizarPorcentaje(); // Actualizar progreso del año al agregar un mes
  }

  actualizarPorcentaje() {
    if (this.meses.length === 0) {
      this.porcentaje = 0;
      return;
    }

    // Calcular el promedio de los porcentajes de los meses
    const totalPorcentaje = this.meses.reduce((acumulador, mes) => {
      return acumulador + parseFloat(mes.porcentaje || 0);
    }, 0);

    this.porcentaje = (totalPorcentaje / this.meses.length).toFixed(2);
  }

  calcularProgresoAnual() {
    // Método que retorna el progreso del año (ya calculado)
    this.actualizarPorcentaje();
    console.log(`Progreso anual del año ${this.numero}: ${this.porcentaje}%`);
    return this.porcentaje;
  }

  obtenerMes(indice) {
    return this.meses[indice] || null;
  }

  mostrarResumenAnual() {
    console.log(`Resumen del año ${this.numero}:`);
    this.meses.forEach((mes) => {
      console.log(`Mes: ${mes.nombre}, Progreso: ${mes.porcentaje}%`);
      mes.dias.forEach((dia) => {
        console.log(`  Día ${dia.numeroDia}: ${dia.temasADar.join(", ")}`);
      });
    });
    console.log(`Progreso anual: ${this.porcentaje}%`);
  }
}

export default Anno; 