import Dia from "./dia.js";
import Mes from "./mes.js";
import Anno from "./anno.js";

// Definir los temas posibles
const temasPosibles = [
  "Matemáticas - Suma y Resta",
  "Matemáticas - Multiplicación",
  "Lengua - Comprensión lectora",
  "Ciencias - El ciclo del agua",
  "Historia - La independencia de mi país",
  "Geografía - Los continentes",
  "Arte - Pintura al óleo",
  "Música - Instrumentos musicales",
  "Educación Física - Ejercicios básicos",
  "Tecnología - Uso responsable de la computadora",
];

// Generar el año actual con temas asignados

const annoActual = (() => {
  const anno = new Anno(new Date().getFullYear());
  const mesesDelAño = [
    "Enero",
    "Febrero",
    "Marzo",
    "Abril",
    "Mayo",
    "Junio",
    "Julio",
    "Agosto",
    "Septiembre",
    "Octubre",
    "Noviembre",
    "Diciembre",
  ];
  mesesDelAño.forEach((nombreMes, index) => {
    const mes = generarMes(nombreMes, anno.numero, index);
    anno.agregarMes(mes);
  });
  return anno;
})();

// Función para generar un mes con días con sus 3 temas aleatorios asignados
function generarMes(nombreMes, año, mesIndex) {
  const mes = new Mes(nombreMes);
  const diasDelMes = new Date(año, mesIndex + 1, 0).getDate();

  for (let i = 1; i <= diasDelMes; i++) {
    const dia = new Dia();
    dia.indicarDia(i);
    asignarTemas(dia);
    mes.agregarDia(dia);
  }

  return mes;
}

// Función para asignar exactamente 3 temas a un día
function asignarTemas(dia) {
  const temasAsignados = new Set();
  while (temasAsignados.size < 3) {
    const tema =
      temasPosibles[Math.floor(Math.random() * temasPosibles.length)];
    temasAsignados.add(tema);
  }
  temasAsignados.forEach((tema) => dia.agregarTema(tema));
}

/* si se quiere año con todos los dias sin ningun tema usar este annoActual
export const annoActual = (() => {
  const anno = new Anno(new Date().getFullYear());
  const mesesDelAño = [
    "Enero",
    "Febrero",
    "Marzo",
    "Abril",
    "Mayo",
    "Junio",
    "Julio",
    "Agosto",
    "Septiembre",
    "Octubre",
    "Noviembre",
    "Diciembre",
  ];
  mesesDelAño.forEach((nombreMes, index) => {
    const mes = generarMes2(nombreMes, anno.numero, index);
    anno.agregarMes(mes);
  });
  return anno;
})();

// Función para generar un mes con días sin temas asignados
function generarMes2(nombreMes, año, mesIndex) {
  const mes = new Mes(nombreMes);
  const diasDelMes = new Date(año, mesIndex + 1, 0).getDate();

  for (let i = 1; i <= diasDelMes; i++) {
    const dia = new Dia();
    dia.indicarDia(i);
    mes.agregarDia(dia);
  }

  return mes;
}


const annoActual = (() => {
  const anno = new Anno(new Date().getFullYear());
  const mesesDelAño = [
    "Enero",
    "Febrero",
    "Marzo",
    "Abril",
    "Mayo",
    "Junio",
    "Julio",
    "Agosto",
    "Septiembre",
    "Octubre",
    "Noviembre",
    "Diciembre",
  ];
  mesesDelAño.forEach((nombreMes, index) => {
    const mes = generarMes(nombreMes, anno.numero, index);
    anno.agregarMes(mes);
  });
  return anno;
})();

// Función para generar un mes con días con 2 temas pendientes y 1 completado
function generarMes(nombreMes, año, mesIndex) {
  const mes = new Mes(nombreMes);
  const diasDelMes = new Date(año, mesIndex + 1, 0).getDate();

  for (let i = 1; i <= diasDelMes; i++) {
    const dia = new Dia();
    dia.indicarDia(i);
    asignarTemas(dia); // Asignar temas (2 pendientes y 1 completado)
    mes.agregarDia(dia);
  }

  return mes;
}

// Función para asignar 3 temas a un día, marcando 1 como completado
function asignarTemas(dia) {
  const temasAsignados = new Set();
  while (temasAsignados.size < 3) {
    const tema =
      temasPosibles[Math.floor(Math.random() * temasPosibles.length)];
    temasAsignados.add(tema);
  }
  temasAsignados.forEach((tema) => dia.agregarTema(tema));

  // Elegir un tema al azar para marcarlo como completado
  const temasArray = Array.from(temasAsignados);
  const temaCompletado = temasArray[Math.floor(Math.random() * temasArray.length)];
  dia.marcarTemaComoDado(temaCompletado);
}

*/

export default annoActual ; 