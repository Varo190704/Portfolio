import annoActual from "./datosPreCargados.js";

// Selección de elementos del DOM
const botonMenu = document.getElementById("botonMenu");
const menu = document.getElementById("menu");
const botonCerrar = document.getElementById("btnCerrarMenu");
const btnDiaActual = document.getElementById("btnDiaActual");
const btnDiaATrabajar = document.getElementById("btnDiaATrabajar");
const diaATrabajar = document.getElementById("diaATrabajar");
const headPrinc = document.getElementById("headPrinc");
const btnesDia = document.getElementById("btnDay");
const estaDia = document.getElementById("estaDia");
const btnesMen = document.getElementById("btnMonth");
const estaMen = document.getElementById("estaMen");
const btnesAnual = document.getElementById("btnAnno");
const estaAnno = document.getElementById("estaAnno");
const hDA = document.getElementById("NumDia"); // Día actual
const ulLista = document.getElementById("ulLista"); // Lista de temas del día
const diaSelec = document.getElementById("diaSelec");
const resultadosBusqueda = document.getElementById("resultadosBusqueda");
const btnAgrTemMan = document.getElementById("btnAgrTemMan");
const agregarTema = document.getElementById("agregarTema");
const txtCompleto = document.getElementById("txtCompleto");
const btnCompleto = document.getElementById("btnCompleto");
const fechaActual = new Date();
const diaActualNum = fechaActual.getDate();
const mesActualNum = fechaActual.getMonth();
const apiKey = "DVjgKdeaZuzN9MBmPDpezHppTy5xzhEq"; // Tu clave de API
const country = "UY"; // Código del país
const anno = 2024; // Año actual
const mesActual = new Date().getMonth(); // Mes actual
const btnCompleto2 = document.getElementById("fuap2"); // Botón de la nueva sección
const txtCompleto2 = document.getElementById("fuap"); // Input para ingresar el tema
const inputAgregar = document.getElementById("inputCompletado");
const btnAgregar = document.getElementById("btnAgregar");
let diaSeleccionado = null;
const contenedorBotonSeleccion = document.getElementById(
  "contenedorBotonSeleccion",
);

// Función genérica para actualizar gráficos circulares
function actualizarGrafica(elemento, progreso) {
  if (!elemento) {
    console.error("El elemento de la gráfica no se encontró.");
    return;
  }
  elemento.style.setProperty("--porcentaje", `${progreso}%`);
  elemento.textContent = `${progreso}%`; // Mostrar porcentaje en el centro
}

// Función para actualizar la gráfica del día actual
function actualizarGraficaAnual() {
  const elementos = [
    document.getElementById("graficaAnual"),
    document.getElementById("graficaAnual1"),
    document.getElementById("graficaAnual2"),
    document.getElementById("graficaAnual3"),
    document.getElementById("graficaAnual4"),
    document.getElementById("graficaAnual5"),
  ];

  const progreso = calcularProgresoAnual(annoActual);

  elementos.forEach((elemento) => {
    actualizarGrafica(elemento, progreso);
  });
}

function actualizarGraficaDiaActual() {
  const elementos = [
    document.getElementById("graficaDia"),
    document.getElementById("graficaDia1"),
    document.getElementById("graficaDia2"),
  ];

  const mesActual = annoActual.obtenerMes(mesActualNum);
  const diaActual = mesActual.obtenerDia(diaActualNum);
  const progreso = diaActual ? diaActual.porcentajeCompletado.toFixed(2) : 0;

  elementos.forEach((elemento) => {
    actualizarGrafica(elemento, progreso);
  });
}

function actualizarGraficaMensual() {
  const elementos = [
    document.getElementById("graficaMensual"),
    document.getElementById("graficaMensual1"),
    document.getElementById("graficaMensual2"),
    document.getElementById("graficaMensual3"),
    document.getElementById("graficaMensual4"),
    document.getElementById("graficaMensual5"),
  ];

  const mes = annoActual.obtenerMes(mesActualNum);
  const progreso = calcularProgresoMensual(mes);

  elementos.forEach((elemento) => {
    actualizarGrafica(elemento, progreso);
  });
}

function calcularProgresoAnual(anno) {
    if (!anno || anno.meses.length === 0) {
      anno.porcentaje = 0;
      return anno.porcentaje;
    }
  
    // Sumar los porcentajes de todos los meses
    const porcentajeTotal = anno.meses.reduce((acumulador, mes) => {
      return acumulador + parseFloat(mes.porcentaje || 0); // Asegúrate de que el mes tenga un atributo 'porcentaje'
    }, 0);
  
    // Calcular el promedio y actualizar el atributo del año
    anno.porcentaje = (porcentajeTotal / anno.meses.length).toFixed(2);
  
    console.log(`Progreso anual actualizado: ${anno.porcentaje}%`);
    return anno.porcentaje; // Retorna el porcentaje actualizado
}
  
function calcularProgresoMensual(mes) {
    if (!mes || mes.dias.length === 0) return 0;
  
    // Actualiza el porcentaje del mes basado en los días
    const totalTemas = mes.dias.reduce((acumulador, dia) => {
      return acumulador + dia.temasADar.length + dia.temasDados.length;
    }, 0);
  
    const temasCompletados = mes.dias.reduce((acumulador, dia) => {
      return acumulador + dia.temasDados.length;
    }, 0);
  
    // Calcular porcentaje y actualizar atributo del mes
    mes.porcentaje =
      totalTemas > 0 ? ((temasCompletados / totalTemas) * 100).toFixed(2) : 0;
  
    console.log(`Progreso mensual (${mes.nombre}): ${mes.porcentaje}%`);
    return mes.porcentaje; // Retorna el atributo ya actualizado
}

// Función para mostrar una sección y ocultar otras
function mostrarSeccion(mostrar, ocultar) {
  mostrar.style.display = "block";
  ocultar.forEach((seccion) => (seccion.style.display = "none"));
  headPrinc.style.display = "block";
}

// Función para cargar los temas del día actual
function cargarTemasDiaActual(diaNum, mesNum) {
  const mes = annoActual ? annoActual.obtenerMes(mesNum) : null;
  const dia = mes ? mes.obtenerDia(diaNum) : null;

  ulLista.innerHTML = ""; // Limpiar la lista

  if (dia && dia.temasADar.length > 0) {
    dia.temasADar.forEach((tema) => {
      const li = document.createElement("li");
      li.textContent = tema;
      ulLista.appendChild(li); // Agregar cada tema como elemento de lista
    });
  } else {
    const li = document.createElement("li");
    li.textContent = "Sin temas asignados";
    ulLista.appendChild(li); // Mostrar mensaje si no hay temas asignados
  }
  ocultarBotonSeleccion();
  actualizarGraficaMensual();
  actualizarGraficaAnual();
  actualizarGraficaDiaActual();
}

// Inicializar el día actual al cargar la página
window.addEventListener("load", () => {
  mostrarSeccion(document.getElementById("diaActual"), [
    resultadosBusqueda,
    diaSelec,
    menu,
    estaMen,
    estaAnno,
    estaDia,
    agregarTema,
  ]);
  actualizarGraficaDiaActual();
  actualizarGraficaMensual();
  actualizarGraficaAnual();
  // Mostrar fecha actual en el encabezado
  hDA.textContent = `${diaActualNum} de ${
    [
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
    ][mesActualNum]
  }`;

  // Cargar temas del día actual desde datos precargados
  cargarTemasDiaActual(diaActualNum, mesActualNum);
});

btnAgrTemMan.addEventListener("click", () => {
  mostrarSeccion(agregarTema, [
    document.getElementById("diaActual"),
    resultadosBusqueda,
    diaSelec,
    menu,
    estaMen,
    estaAnno,
    estaDia,
  ]);
  actualizarGraficaDiaActual();
  actualizarGraficaMensual();
  actualizarGraficaAnual();
  ocultarBotonSeleccion();
  cargarTemasDiaActual(diaActualNum, mesActualNum);
});

// Botón para mostrar el día actual
btnDiaActual.addEventListener("click", () => {
  mostrarSeccion(document.getElementById("diaActual"), [
    resultadosBusqueda,
    agregarTema,
    diaSelec,
    menu,
    estaMen,
    estaAnno,
    estaDia,
  ]);
  actualizarGraficaDiaActual();
  actualizarGraficaMensual();
  actualizarGraficaAnual();
  ocultarBotonSeleccion();
  cargarTemasDiaActual(diaActualNum, mesActualNum);
});

// Botones de navegación entre secciones
btnesAnual.addEventListener("click", () => {
  ocultarBotonSeleccion();
  mostrarSeccion(estaAnno, [
    diaSelec,
    resultadosBusqueda,
    document.getElementById("diaActual"),
    estaDia,
    menu,
    estaMen,
    agregarTema,
  ]);
  actualizarGraficaDiaActual();
  actualizarGraficaMensual();
  actualizarGraficaAnual();
});

btnesMen.addEventListener("click", () => {
  ocultarBotonSeleccion();
  mostrarSeccion(estaMen, [
    resultadosBusqueda,
    document.getElementById("diaActual"),
    diaSelec,
    estaDia,
    menu,
    estaAnno,
    agregarTema,
  ]);
  actualizarGraficaDiaActual();
  actualizarGraficaMensual();
  actualizarGraficaAnual();
});

btnesDia.addEventListener("click", () => {
  ocultarBotonSeleccion();
  mostrarSeccion(estaDia, [
    resultadosBusqueda,
    document.getElementById("diaActual"),
    diaSelec,
    estaMen,
    menu,
    estaAnno,
    agregarTema,
  ]);
  actualizarGraficaDiaActual();
  actualizarGraficaMensual();
  actualizarGraficaAnual();
});

// Menú y botón de cierre
botonMenu.addEventListener("click", () => {
  ocultarBotonSeleccion();
  if (menu.style.display === "none" || menu.style.display === "") {
    mostrarSeccion(menu, [
      resultadosBusqueda,
      document.getElementById("diaActual"),
      diaSelec,
      diaATrabajar,
      estaMen,
      estaAnno,
      estaDia,
      agregarTema,
    ]);
    actualizarGraficaDiaActual();
    actualizarGraficaMensual();
    actualizarGraficaAnual();
    headPrinc.style.setProperty("display", "none", "important");
  } else {
    mostrarSeccion(document.getElementById("diaActual"), [
      resultadosBusqueda,
      menu,
      diaSelec,
      diaATrabajar,
      estaMen,
      estaAnno,
      estaDia,
      agregarTema,
    ]);
    actualizarGraficaDiaActual();
    actualizarGraficaMensual();
    actualizarGraficaAnual();
  }
});

botonCerrar.addEventListener("click", () => {
  mostrarSeccion(document.getElementById("diaActual"), [
    resultadosBusqueda,
    diaSelec,
    menu,
    estaMen,
    estaAnno,
    estaDia,
    agregarTema,
  ]);
  actualizarGraficaDiaActual();
  actualizarGraficaMensual();
  actualizarGraficaAnual();
  // Mostrar fecha actual en el encabezado
  hDA.textContent = `${diaActualNum} de ${
    [
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
    ][mesActualNum]
  }`;

  // Cargar temas del día actual desde datos precargados
  cargarTemasDiaActual(diaActualNum, mesActualNum);
});

// Día a trabajar: calendario dinámico
btnDiaATrabajar.addEventListener("click", async () => {
  ocultarBotonSeleccion();
  if (
    diaATrabajar.style.display === "none" ||
    diaATrabajar.style.display === ""
  ) {
    mostrarSeccion(diaATrabajar, [
      resultadosBusqueda,
      menu,
      diaSelec,
      document.getElementById("diaActual"),
      estaMen,
      estaAnno,
      estaDia,
      agregarTema,
    ]);
    actualizarGraficaDiaActual();
    actualizarGraficaMensual();
    actualizarGraficaAnual();
    const anno = new Date().getFullYear();
    const pais = "UY";
    const mesActual = new Date().getMonth();
    const holidays = await fetchHolidays(anno, pais);
    createCalendar(anno, mesActual, holidays);
  } else {
    mostrarSeccion(document.getElementById("diaActual"), [
      resultadosBusqueda,
      diaSelec,
      menu,
      estaMen,
      estaAnno,
      estaDia,
      agregarTema,
    ]);
    actualizarGraficaDiaActual();
    actualizarGraficaMensual();
    actualizarGraficaAnual();
  }
});


document.querySelector('input[type="search"]').addEventListener("input", (event) => {
    const temaBuscado = event.target.value.trim().toLowerCase();

    if (temaBuscado) {
      const resultados = [];
      mostrarSeccion(resultadosBusqueda, [
        document.getElementById("diaActual"),
        diaSelec,
        menu,
        estaMen,
        estaAnno,
        estaDia,
        agregarTema,
      ]);
      annoActual.meses.forEach((mes) => {
        mes.dias.forEach((dia) => {
          if (
            dia.temasADar.some((tema) =>
              tema.toLowerCase().includes(temaBuscado),
            )
          ) {
            resultados.push({
              dia: dia.numeroDia,
              mes: mes.nombre,
              temas: dia.temasADar,
            });
          }
        });
      });

      mostrarResultadosBusqueda(resultados, temaBuscado);
    } else {
      limpiarResultadosBusqueda();
    }
});

createCalendar(anno, mesActual, apiKey, country);

async function fetchHolidays(anno, apiKey, country) {
  const url = `https://calendarific.com/api/v2/holidays?&api_key=DVjgKdeaZuzN9MBmPDpezHppTy5xzhEq&country=${country}&year=${anno}`;
  try {
    const response = await fetch(url);
    if (!response.ok)
      throw new Error("Error al obtener los datos de feriados.");
    const data = await response.json();
    return data.response.holidays || [];
  } catch (error) {
    console.error("Error al obtener feriados:", error);
    return [];
  }
}

// Crear calendario dinámico
async function createCalendar(anno, mesActual, apiKey, country) {
  const calendarDiv = document.getElementById("calendario");
  calendarDiv.innerHTML = ""; // Limpiar contenido previo

  // Obtener datos desde la API
  const holidays = await fetchHolidays(anno, apiKey, country);

  // Crear tabla del calendario
  const table = document.createElement("table");
  const monthName = [
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
  ][mesActual];
  const caption = document.createElement("caption");
  caption.textContent = monthName;
  table.appendChild(caption);

  const headerRow = document.createElement("tr");
  ["Dom", "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb"].forEach((day) => {
    const th = document.createElement("th");
    th.textContent = day;
    headerRow.appendChild(th);
  });
  table.appendChild(headerRow);

  const firstDay = new Date(anno, mesActual, 1).getDay();
  const daysInMonth = new Date(anno, mesActual + 1, 0).getDate();

  let date = 1;
  for (let row = 0; row < 6; row++) {
    const tr = document.createElement("tr");

    for (let col = 0; col < 7; col++) {
      const td = document.createElement("td");

      if (row === 0 && col < firstDay) {
        td.classList.add("empty");
      } else if (date <= daysInMonth) {
        td.textContent = date;
        td.classList.add("day-cell");

        const currentDate = { day: date, month: mesActual, year: anno };
        const fullDate = new Date(anno, mesActual, date);

        // Marcar sábados y domingos
        if (col === 0 || col === 6) {
          td.classList.add("weekend");
        }

        // Marcar días feriados
        const isHoliday = holidays.some((holiday) => {
          const holidayDate = new Date(holiday.date.iso);
          return holidayDate.toDateString() === fullDate.toDateString();
        });

        if (isHoliday) {
          td.classList.add("holiday");
        }

        // Agregar evento al hacer clic en la celda
        td.addEventListener("click", () => {
          document.querySelectorAll(".selected").forEach((selectedCell) => {
            selectedCell.classList.remove("selected");
          });

          td.classList.add("selected");

          console.log(
            `Día seleccionado: ${currentDate.day}, Mes seleccionado: ${currentDate.month + 1}`,
          );
        });

        date++;
      } else {
        td.classList.add("empty");
      }

      tr.appendChild(td);
    }
    table.appendChild(tr);
  }
  actualizarGraficaMensual();
  actualizarGraficaAnual();
  calendarDiv.appendChild(table);
}

document.getElementById("calendario").addEventListener("click", (event) => {
  const celda = event.target;
  if (
    celda.classList.contains("day-cell") &&
    !celda.classList.contains("empty")
  ) {
    const dia = parseInt(celda.textContent, 10); // Día seleccionado
    const mes = annoActual.obtenerMes(mesActualNum);

    diaSeleccionado = mes.obtenerDia(dia); // Asignar el día seleccionado
    console.log(`Día seleccionado: ${diaSeleccionado.numeroDia}`);

    if (diaSeleccionado && diaSeleccionado.temasADar.length > 0) {
      mostrarBotonSeleccion(); // Mostrar botón si hay temas
    } else {
      ocultarBotonSeleccion(); // Ocultar botón si no hay temas
    }
  }
});


function mostrarBotonSeleccion() {
  // Verifica si ya existe el botón
  if (document.getElementById("btnSeleccionarDia")) return; //tengo que poner que si existe borrarlo

  const boton = document.createElement("button");
  boton.id = "btnSeleccionarDia";
  boton.className = "verde form-control shadow";
  boton.textContent = "Seleccionar día";

  // Agregar evento directamente aquí
  boton.addEventListener("click", () => {
    diaSelec.style.display = "block"; // Mostrar sección
    otrasSecciones.forEach((seccion) => (seccion.style.display = "none")); // Ocultar otras
    console.log(
      `Sección 'diaSelec' mostrada para el día ${diaSeleccionado.numeroDia}.`,
    );
    cargarDatosDiaSeleccionado(diaSeleccionado); // Cargar datos en la sección
  });

  contenedorBotonSeleccion.appendChild(boton);

  // Referencia al botón y a las secciones
  const btnSeleccionarDia = document.getElementById("btnSeleccionarDia");
  const otrasSecciones = [
    document.getElementById("diaActual"),
    document.getElementById("menu"),
    document.getElementById("diaATrabajar"),
    document.getElementById("estaDia"),
    document.getElementById("estaMen"),
    document.getElementById("estaAnno"),
  ];

  // Agregar evento al botón
  btnSeleccionarDia.addEventListener("click", () => {
    // Mostrar la sección diaSelec
    diaSelec.style.display = "block";

    // Ocultar todas las demás secciones
    otrasSecciones.forEach((seccion) => {
      seccion.style.display = "none";
    });

    // Opcional: puedes agregar lógica para cargar datos relacionados con el día seleccionado
    console.log("Sección 'diaSelec' mostrada.");
  });
}

function ocultarBotonSeleccion() {
  const boton = document.getElementById("btnSeleccionarDia");
  if (boton) boton.remove();
}

btnAgregar.addEventListener("click", (event) => {
  event.preventDefault(); // Previene la recarga de la página
  if (!diaSeleccionado) {
    alert("Por favor, selecciona un día en el calendario primero.");
    return;
  }
  const nuevoTema = inputAgregar.value.trim(); // Obtener texto ingresado
  if (nuevoTema) {
    diaSeleccionado.agregarTema(nuevoTema); // Agregar el tema al día seleccionado
    inputAgregar.value = ""; // Limpiar el campo
    alert(`Tema "${nuevoTema}" agregado al día ${diaSeleccionado.numeroDia}`);
  } else {
    alert("Por favor, ingresa un tema.");
  }
  actualizarGraficaMensual();
  actualizarGraficaAnual();
});

document.getElementById("btnAgregarTema").addEventListener("click", () => {
    const diaSeleccionado = parseInt(
      document.getElementById("diaSeleccionado").value,
    );
    const temaNuevo = document.getElementById("temaNuevo").value.trim();
  
    if (!diaSeleccionado || !temaNuevo) {
      document.getElementById("mensaje").innerHTML =
        '<p class="text-danger">Por favor, completa todos los campos.</p>';
      return;
    }
  
    const mesActual = annoActual.obtenerMes(mesActualNum); // Suponiendo que `annoActual` ya está cargado.
    const dia = mesActual.obtenerDia(diaSeleccionado);
  
    if (!dia) {
      document.getElementById("mensaje").innerHTML =
        '<p class="text-danger">Día no válido. Selecciona uno dentro del mes.</p>';
      return;
    }
  
    dia.agregarTema(temaNuevo);
    document.getElementById("mensaje").innerHTML =
      '<p class="text-success">Tema agregado correctamente.</p>';
    document.getElementById("formAgregarTema").reset();
  
    // Opcional: Actualizar la vista si es necesario
    actualizarGraficaMensual();
    actualizarGraficaAnual();
});

function cargarDatosDiaSeleccionado(dia) {
  ocultarBotonSeleccion();
  if (!dia) {
    console.error("Día seleccionado no válido.");
    return;
  }

  const ulLista2 = document.getElementById("ulLista2"); // Lista dentro de diaSelec
  const NumDia2 = document.getElementById("NumDia2"); // Encabezado del día

  ulLista2.innerHTML = ""; // Limpiar lista
  NumDia2.textContent = `${dia.numeroDia} de ${annoActual.obtenerMes(mesActualNum).nombre}`;

  if (dia.temasADar.length > 0) {
    dia.temasADar.forEach((tema) => {
      const li = document.createElement("li");
      li.textContent = tema;
      ulLista2.appendChild(li); // Agregar temas como elementos de lista
    });
  } else {
    const li = document.createElement("li");
    li.textContent = "Sin temas asignados";
    ulLista2.appendChild(li);
  }
  actualizarGraficaMensual();
  actualizarGraficaAnual();
}

btnCompleto2.addEventListener("click", () => {
  const temaIngresado = txtCompleto2.value.trim().toLowerCase();
  if (!temaIngresado) {
    alert("Por favor, ingrese un tema.");
    return;
  }

  const indexTema = diaSeleccionado.temasADar.findIndex(
    (tema) => tema.toLowerCase() === temaIngresado,
  );

  if (indexTema !== -1) {
    const temaCompletado = diaSeleccionado.temasADar.splice(indexTema, 1)[0];
    diaSeleccionado.temasDados.push(temaCompletado);
    diaSeleccionado.actualizarPorcentaje();

    const mes = annoActual.obtenerMes(mesActualNum);
    if (mes) {
      mes.actualizarPorcentaje();
      annoActual.actualizarPorcentaje(); // Actualizar progreso del año
      console.log(`Progreso anual actualizado: ${annoActual.porcentaje}%`);
    }

    cargarDatosDiaSeleccionado(diaSeleccionado);
    txtCompleto2.value = "";
    alert(`Tema "${temaCompletado}" marcado como completo.`);
  } else {
    alert("Tema ingresado no válido o ya completado.");
  }
});

// Función para manejar el marcado de un tema como completado
btnCompleto.addEventListener("click", () => {
  const temaIngresado = txtCompleto.value.trim(); // Tema ingresado por el usuario
  const mes = annoActual.obtenerMes(mesActualNum); // Obtener el mes actual
  const dia = mes.obtenerDia(diaActualNum); // Obtener el día actual

  if (!temaIngresado) {
    alert("Por favor, ingrese un tema.");
    return;
  }

  // Verificar si el tema está en la lista de temas a completar
  const indexTema = dia.temasADar.indexOf(temaIngresado);
  if (indexTema !== -1) {
    // Eliminar el tema de la lista de temas a completar
    dia.temasADar.splice(indexTema, 1);
    dia.temasDados.push(temaIngresado); // Agregar el tema a la lista de temas completados
    dia.actualizarPorcentaje(); // Actualizar el porcentaje completado
    mes.actualizarPorcentaje();
    // Actualizar la lista en la interfaz
    cargarTemasDiaActual(diaActualNum, mesActualNum);

    // Limpiar el textarea
    txtCompleto.value = "";
    alert(`Tema "${temaIngresado}" marcado como completo.`);
  } else {
    // Tema no válido, mostrar alerta
    alert("Tema ingresado no válido");
  }
});


function mostrarResultadosBusqueda(resultados, tema) {
  const resultadosDiv = document.getElementById("resultadosBusqueda");
  resultadosDiv.innerHTML = `<h3>Resultados para "${tema}":</h3>`;

  if (resultados.length > 0) {
    const lista = document.createElement("ul");
    resultados.forEach((resultado) => {
      const item = document.createElement("li");
      item.innerHTML = `<span style="font-weight: bold; font-size: 1.2rem;">Día ${resultado.dia} de ${resultado.mes}:</span> ${resultado.temas.join(", ")}`;
      lista.appendChild(item);
    });
    resultadosDiv.appendChild(lista);
  } else {
    resultadosDiv.innerHTML += "<p>No se encontraron resultados.</p>";
  }
}

function limpiarResultadosBusqueda() {
  const resultadosDiv = document.getElementById("resultadosBusqueda");
  resultadosDiv.innerHTML = "";
}