class Dia {
  constructor() {
    this.numeroDia = 0;
    this.temasADar = []; // Lista de temas por dar
    this.temasDados = []; // Lista de temas ya dados
    this.porcentajeCompletado = 0.0; // Porcentaje completado
  }

  indicarDia(n) {
    this.numeroDia = n;
  }

  agregarTema(tema) {
    // Agrega un tema a la lista de temas por dar
    this.temasADar.push(tema);
    this.actualizarPorcentaje();
  }

  marcarTemaComoDado(tema) {
    // Mueve un tema de la lista de temas por dar a la lista de temas dados
    if (this.temasDados.includes(tema)) {
      console.log(`El tema '${tema}' ya ha sido dado.`);
      return;
    }

    const index = this.temasADar.indexOf(tema);
    if (index !== -1) {
      this.temasADar.splice(index, 1);
      this.temasDados.push(tema);
      this.actualizarPorcentaje();
    } else {
      console.log(`El tema '${tema}' no está en la lista de temas a dar.`);
    }
  }

  getNumeroDia() {
    return this.numeroDia;
  }

  actualizarPorcentaje() {
    // Calcula el porcentaje de temas completados
    const totalTemas = this.temasADar.length + this.temasDados.length;
    if (totalTemas > 0) {
      this.porcentajeCompletado = (this.temasDados.length / totalTemas) * 100;
    } else {
      this.porcentajeCompletado = 0.0;
    }
  }

  mostrarResumen() {
    // Devuelve un resumen del día
    console.log(`Temas por dar: ${this.temasADar}`);
    console.log(`Temas dados: ${this.temasDados}`);
    console.log(`Porcentaje completado: ${this.porcentajeCompletado.toFixed(2)}%`,);
  }
}

export default Dia; 