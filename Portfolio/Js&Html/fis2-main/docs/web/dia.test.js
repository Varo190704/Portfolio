import { describe, test, expect, beforeEach, jest } from "@jest/globals";
import Dia from "./dia.js";

describe("Dia class", () => {
  let dia;

  beforeEach(() => {
    dia = new Dia(); // Crear una instancia de Dia para cada prueba
  });

  test("initialize with default values", () => {
    expect(dia.getNumeroDia()).toBe(0);
    expect(dia.temasADar).toEqual([]);
    expect(dia.temasDados).toEqual([]);
    expect(dia.porcentajeCompletado).toBe(0.0);
  });

  test("set day number", () => {
    dia.indicarDia(5);
    expect(dia.getNumeroDia()).toBe(5);
  });

  test("add a topic to temasADar", () => {
    dia.agregarTema("Matemáticas");
    expect(dia.temasADar).toEqual(["Matemáticas"]);
  });

  test("should update percentage after adding a topic", () => {
    dia.agregarTema("Matemáticas");
    expect(dia.porcentajeCompletado).toBe(0.0); // No hay temas dados aún
  });

  test("mark a topic as given", () => {
    dia.agregarTema("Matemáticas");
    dia.marcarTemaComoDado("Matemáticas");
    expect(dia.temasDados).toEqual(["Matemáticas"]);
    expect(dia.temasADar).toEqual([]);
  });

  test("should not mark a topic as given if it is already in temasDados", () => {
    dia.agregarTema("Matemáticas");
    dia.marcarTemaComoDado("Matemáticas");

    const consoleSpy = jest.spyOn(console, "log");
    dia.marcarTemaComoDado("Matemáticas"); // Marcar el mismo tema nuevamente

    expect(consoleSpy).toHaveBeenCalledWith("El tema 'Matemáticas' ya ha sido dado.");
    expect(dia.temasDados).toEqual(["Matemáticas"]); // La lista no debe cambiar
    expect(dia.temasADar).toEqual([]); // La lista por dar sigue vacía

    consoleSpy.mockRestore(); // Limpia el espía
  });

  test("do not mark a non-existent topic as given", () => {
    const consoleSpy = jest.spyOn(console, "log");
    dia.marcarTemaComoDado("Ciencias");
    expect(consoleSpy).toHaveBeenCalledWith("El tema 'Ciencias' no está en la lista de temas a dar.");
    expect(dia.temasDados).toEqual([]);
    consoleSpy.mockRestore();
  });

  test("calculate completion percentage", () => {
    dia.agregarTema("Matemáticas");
    dia.agregarTema("Ciencias");
    dia.marcarTemaComoDado("Matemáticas");
    expect(dia.porcentajeCompletado).toBe(50.0);
  });

  test("reset percentage with no topics", () => {
    dia.actualizarPorcentaje();
    expect(dia.porcentajeCompletado).toBe(0.0);
  });

  test("should display the correct summary in mostrarResumen", () => {
    dia.agregarTema("Matemáticas");
    dia.agregarTema("Ciencias");
    dia.marcarTemaComoDado("Matemáticas");

    const consoleSpy = jest.spyOn(console, "log");
    dia.mostrarResumen();

    expect(consoleSpy).toHaveBeenCalledWith("Temas por dar: Ciencias");
    expect(consoleSpy).toHaveBeenCalledWith("Temas dados: Matemáticas");
    expect(consoleSpy).toHaveBeenCalledWith("Porcentaje completado: 50.00%");

    consoleSpy.mockRestore(); // Limpia el espía
  });
});
