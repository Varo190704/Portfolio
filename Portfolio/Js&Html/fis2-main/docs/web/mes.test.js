import { describe, test, expect, beforeEach, jest } from "@jest/globals";
import Mes from "./mes.js";
import Dia from "./dia.js";

describe("Mes class", () => {
  let mes;

  beforeEach(() => {
    mes = new Mes("Enero"); // Crear una instancia de Mes para cada prueba
  });

  test("should initialize with default values", () => {
    expect(mes.nombre).toBe("Enero");
    expect(mes.dias).toEqual([]);
    expect(mes.porcentaje).toBe(0);
  });

  test("should return 0 progress when there are no days", () => {
    const progreso = mes.calcularProgresoMensual();
    expect(progreso).toBe(0);
  });

  test("should add a day and update percentage", () => {
    const dia = new Dia();
    dia.agregarTema("Matemáticas");
    dia.marcarTemaComoDado("Matemáticas");

    mes.agregarDia(dia);
    expect(mes.dias.length).toBe(1);
    expect(mes.porcentaje).toBe("100.00");
  });

  test("should calculate percentage with multiple days", () => {
    const dia1 = new Dia();
    const dia2 = new Dia();

    dia1.agregarTema("Matemáticas");
    dia1.marcarTemaComoDado("Matemáticas");

    dia2.agregarTema("Ciencias");

    mes.agregarDia(dia1);
    mes.agregarDia(dia2);

    expect(mes.porcentaje).toBe("50.00");
  });

  test("should calculate monthly progress", () => {
    const dia1 = new Dia();
    const dia2 = new Dia();

    dia1.agregarTema("Matemáticas");
    dia1.marcarTemaComoDado("Matemáticas");

    dia2.agregarTema("Ciencias");

    mes.agregarDia(dia1);
    mes.agregarDia(dia2);

    const progreso = mes.calcularProgresoMensual();
    expect(progreso).toBe("50.00");
  });

  test("should retrieve a day by its number", () => {
    const dia1 = new Dia();
    dia1.indicarDia(1);

    const dia2 = new Dia();
    dia2.indicarDia(2);

    mes.agregarDia(dia1);
    mes.agregarDia(dia2);

    expect(mes.obtenerDia(1)).toBe(dia1);
    expect(mes.obtenerDia(2)).toBe(dia2);
    expect(mes.obtenerDia(3)).toBeUndefined();
  });

  test("should display a monthly summary", () => {
    const dia1 = new Dia();
    dia1.indicarDia(1);
    dia1.agregarTema("Matemáticas");
  
    mes.agregarDia(dia1);
  
    const consoleSpy = jest.spyOn(console, "log");
    mes.mostrarResumenMensual();
  
    expect(consoleSpy).toHaveBeenCalledWith("Resumen del mes: Enero");
    expect(consoleSpy).toHaveBeenCalledWith("Día 1:");
    expect(consoleSpy).toHaveBeenCalledWith("Temas por dar: Matemáticas");
    expect(consoleSpy).toHaveBeenCalledWith("Temas dados: ");
    expect(consoleSpy).toHaveBeenCalledWith("Porcentaje completado: 0.00%");
    expect(consoleSpy).toHaveBeenCalledWith("Progreso total del mes: 0.00%");
  
    consoleSpy.mockRestore();
  });
});
