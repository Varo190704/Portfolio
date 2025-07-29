import { describe, expect, test, beforeEach } from "@jest/globals";
import { jest } from "@jest/globals";
import Anno from "./anno.js";

describe("Anno class", () => {
  let anno;

  beforeEach(() => {
    anno = new Anno(2024); // Inicializamos un año para las pruebas
  });

  test("store year number", () => {
    expect(anno.numero).toBe(2024); // Verifica que el número del año se almacena correctamente
  });

  test("initialize with empty months and zero percentage", () => {
    expect(anno.meses).toEqual([]); // Verifica que comienza con meses vacíos
    expect(anno.porcentaje).toBe(0); // Verifica que el progreso inicial es 0%
  });

  test("add a month to the year", () => {
    const mes = { nombre: "Enero", porcentaje: 50, dias: [] };
    anno.agregarMes(mes);
    expect(anno.meses.length).toBe(1); // Verifica que el mes fue agregado
    expect(anno.meses[0]).toEqual(mes); // Verifica que el mes es correcto
  });

  test("calculate percentage when months are added", () => {
    const mes1 = { nombre: "Enero", porcentaje: 50, dias: [] };
    const mes2 = { nombre: "Febrero", porcentaje: 100, dias: [] };
    anno.agregarMes(mes1);
    anno.agregarMes(mes2);
    expect(anno.porcentaje).toBe("75.00"); // Promedio de 50% y 100%
  });

  test("retrieve a month by index", () => {
    const mes = { nombre: "Enero", porcentaje: 50, dias: [] };
    anno.agregarMes(mes);
    expect(anno.obtenerMes(0)).toEqual(mes); // Verifica que obtiene el mes correcto
    expect(anno.obtenerMes(1)).toBeNull(); // Verifica que índices inválidos retornan null
  });

  test("calculate annual progress", () => {
    const mes1 = { nombre: "Enero", porcentaje: 50, dias: [] };
    const mes2 = { nombre: "Febrero", porcentaje: 100, dias: [] };
    anno.agregarMes(mes1);
    anno.agregarMes(mes2);
    const progreso = anno.calcularProgresoAnual();
    expect(progreso).toBe("75.00"); // Verifica que calcula el progreso correctamente
  });

  test("show annual summary in console", () => {
    const mes1 = { nombre: "Enero", porcentaje: 50, dias: [{ numeroDia: 1, temasADar: ["tema1"] }] };
    const mes2 = { nombre: "Febrero", porcentaje: 100, dias: [{ numeroDia: 2, temasADar: ["tema2"] }] };
    anno.agregarMes(mes1);
    anno.agregarMes(mes2);

    const consoleSpy = jest.spyOn(console, "log"); // Espiar las llamadas a console.log
    anno.mostrarResumenAnual();

    expect(consoleSpy).toHaveBeenCalledWith(expect.stringContaining("Resumen del año 2024:"));
    expect(consoleSpy).toHaveBeenCalledWith(expect.stringContaining("Mes: Enero, Progreso: 50%"));
    expect(consoleSpy).toHaveBeenCalledWith(expect.stringContaining("Mes: Febrero, Progreso: 100%"));
    expect(consoleSpy).toHaveBeenCalledWith(expect.stringContaining("Día 1: tema1"));
    expect(consoleSpy).toHaveBeenCalledWith(expect.stringContaining("Día 2: tema2"));

    consoleSpy.mockRestore(); 
  });
  test("calculate percentage with no months", () => {
    const anno = new Anno(2024); 
    anno.actualizarPorcentaje(); 
    expect(anno.porcentaje).toBe(0);
  });
  
  test("calculate percentage when a month has no porcentaje", () => {
    const mes1 = { nombre: "Enero", porcentaje: 50, dias: [] };
    const mes2 = { nombre: "Febrero", dias: [] }; // Sin porcentaje
    const anno = new Anno(2024);
  
    anno.agregarMes(mes1);
    anno.agregarMes(mes2);
  
    const progreso = anno.calcularProgresoAnual(); // Calcula el progreso
    expect(progreso).toBe("25.00"); // Promedio de 50 y 0 (porcentaje por defecto)
  });
  
  
});
