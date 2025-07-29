# Proyecto de Gestión de Lecciones

Este proyecto es una herramienta interactiva para gestionar el progreso educativo anual, mensual y diario de un usuario. La aplicación permite asignar, completar y visualizar temas de aprendizaje distribuidos por días, meses y años.

## Funcionalidades Implementadas

1. **Visualización de Progreso**:
   - Progreso anual, mensual y diario en gráficos circulares interactivos.
   - Vista detallada de los temas asignados y completados.

2. **Gestión de Temas**:

   - Asignación de temas a días específicos.
        1. Desde calendario en la section de Dia A Trabajar
        2. Formulario en la section Agregar Tema indicando dia especifico, y tema a agregar

   - Marcado de temas como completados, actualizando automáticamente el progreso.
   
   - Muestra de temas en las sections Dia Actual y Dia Seleccionado

3. **Selección de Día**:
   - Calendario interactivo para seleccionar días y visualizar sus temas. (Debido a ser un Producto Minimo Viable).

4. **Búsqueda de Temas**:
   - Función de búsqueda para localizar días con temas específicos.

## Estructura del Proyecto

El proyecto está organizado en los siguientes módulos:

### Archivos Principales
- `index.html`: Página principal con la interfaz de usuario.
- `style.css`: Estilos personalizados para la aplicación.
- `funciones.js`: Lógica principal para la interacción y actualizaciones dinámicas. Presenta comentarios explicativos en las funciones y variables utilizadas.

### Clases
- `dia.js`: Representa un día, con funcionalidades para gestionar temas y calcular progreso diario.
- `mes.js`: Representa un mes, gestionando días y calculando progreso mensual.
- `anno.js`: Representa un año, gestionando meses y calculando progreso anual.

### Datos Pre-Cargados

- `datosPreCargados.js`: Inicializa un año completo con meses y dias con temas cargados, en caso de que se quiera un año con meses y dias sin temas marcar como comentario desde la linea 21 hasta la linea 68, desmarcar como comentario desde la linea 71 hasta la linea 106 y dejar como comentario desde la linea 109 hasta linea 161, en caso de querer un año con 2 temas a completar y 1 completado, dejar comentado desde la linea 21 hasta la linea 106 y dejar descomentado desde la linea 109 hasta la linea 161.

### Pruebas
- `*.test.js`: Archivos de pruebas unitarias utilizando `Jest` para cada clase y funcionalidad clave, como los vistos en clase.

## Casos de Uso

1. **Usuario Consulta el Progreso Diario**:
   - Navega a la sección de "Día Actual".
   - Visualiza los temas pendientes y completados.

2. **Administrador Agrega Temas**:
   - Accede a la sección de agregar temas.
   - Selecciona un día y agrega un nuevo tema.

3. **Visualización de Progreso Global**:
   - Navega entre secciones (diaria, mensual, anual) para visualizar el progreso acumulado.

## Datos de Prueba

Para facilitar el testing, los siguientes datos están pre-cargados (o no) en el sistema:

- **Días**:
  Cada día tiene 3, 2 o ningun tema asignados al azar desde una lista predefinida.
  
- **Meses**:
  Cada mes tiene sus dias respectivos cargados con 3, 2 o ningun tema al azar cada uno.

- **Año**:
  Año actual cargado con los meses respectivos con sus respectivos dias con 3, 2 o ningun tema al azar cada uno

- **Progresos**:
  Los porcentajes de progreso diario, mensual y anual se calculan automáticamente según los temas completados.

Puedes encontrar ejemplos detallados en:
- `datosPreCargados.js`
- Archivos de prueba en `*.test.js`.

## Cómo Correr el Proyecto

1. Clona el repositorio.
2. Abre `index.html` desde live Server para poder visualizar la web.
3. Corre `npm install` para instalar dependencias (si es necesario).
4. Ejecuta las pruebas unitarias con:
   ```bash
   npm test
