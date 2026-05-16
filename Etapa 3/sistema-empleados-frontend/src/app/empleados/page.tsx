"use client";

import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import { empleadosApi } from "@/lib/api";
import type { Empleado } from "@/types";

export default function EmpleadosPage() {
  const router = useRouter();
  const [empleados, setEmpleados] = useState<Empleado[]>([]);
  const [cargando, setCargando] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [usuario, setUsuario] = useState<string>("");

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      router.push("/login");
      return;
    }
    setUsuario(localStorage.getItem("usuario") || "");
    cargarEmpleados();
  }, []);

  const cargarEmpleados = async () => {
    try {
      setCargando(true);
      const data = await empleadosApi.getAll();
      setEmpleados(data);
    } catch (err: unknown) {
      if (err instanceof Error) setError(err.message);
    } finally {
      setCargando(false);
    }
  };

  const handleEliminar = async (id: number, nombre: string) => {
    if (!confirm(`¿Confirmás eliminar a ${nombre}?`)) return;
    try {
      await empleadosApi.eliminar(id);
      setEmpleados(empleados.filter((e) => e.id !== id));
    } catch (err: unknown) {
      if (err instanceof Error) alert(err.message);
    }
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("usuario");
    router.push("/login");
  };

  if (cargando)
    return (
      <main className="min-h-screen bg-gray-100 flex items-center justify-center">
        <p className="text-gray-500 text-lg">Cargando empleados...</p>
      </main>
    );

  return (
    <main className="min-h-screen bg-gray-100">
      {/* Navbar */}
      <nav className="bg-white shadow-sm px-6 py-4 flex justify-between items-center">
        <h1 className="text-xl font-bold text-gray-800">
          Sistema de Empleados
        </h1>
        <div className="flex items-center gap-4">
          <span className="text-sm text-gray-600">
            Hola, <strong>{usuario}</strong>
          </span>
          <button
            onClick={handleLogout}
            className="text-sm bg-gray-100 hover:bg-gray-200 text-gray-700 px-4 py-2 rounded-lg transition-colors"
          >
            Cerrar sesión
          </button>
        </div>
      </nav>

      <div className="max-w-6xl mx-auto px-6 py-8">
        {/* Header */}
        <div className="flex justify-between items-center mb-6">
          <div>
            <h2 className="text-2xl font-bold text-gray-800">Empleados</h2>
            <p className="text-gray-500 text-sm mt-1">
              {empleados.length} empleados registrados
            </p>
          </div>
          <button
            onClick={() => router.push("/empleados/nuevo")}
            className="bg-blue-600 hover:bg-blue-700 text-white font-semibold px-5 py-2.5 rounded-lg transition-colors"
          >
            + Agregar empleado
          </button>
        </div>

        {/* Error */}
        {error && (
          <div className="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded-lg mb-6">
            {error}
          </div>
        )}

        {/* Tabla */}
        <div className="bg-white rounded-xl shadow-sm overflow-hidden">
          <table className="w-full">
            <thead className="bg-gray-50 border-b border-gray-200">
              <tr>
                <th className="text-left px-6 py-4 text-sm font-semibold text-gray-600">
                  Nombre
                </th>
                <th className="text-left px-6 py-4 text-sm font-semibold text-gray-600">
                  Departamento
                </th>
                <th className="text-right px-6 py-4 text-sm font-semibold text-gray-600">
                  Salario
                </th>
                <th className="text-right px-6 py-4 text-sm font-semibold text-gray-600">
                  Ventas
                </th>
                <th className="text-right px-6 py-4 text-sm font-semibold text-gray-600">
                  Total vendido
                </th>
                <th className="text-center px-6 py-4 text-sm font-semibold text-gray-600">
                  Acciones
                </th>
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-100">
              {empleados.length === 0 ? (
                <tr>
                  <td colSpan={6} className="text-center py-12 text-gray-400">
                    No hay empleados registrados.
                  </td>
                </tr>
              ) : (
                empleados.map((emp) => (
                  <tr
                    key={emp.id}
                    className="hover:bg-gray-50 transition-colors"
                  >
                    <td className="px-6 py-4">
                      <p className="font-medium text-gray-800">{emp.nombre}</p>
                      <p className="text-xs text-gray-400">
                        Desde{" "}
                        {new Date(emp.fechaIngreso).toLocaleDateString("es-AR")}
                      </p>
                    </td>
                    <td className="px-6 py-4">
                      <span className="bg-blue-50 text-blue-700 text-xs font-medium px-2.5 py-1 rounded-full">
                        {emp.departamento}
                      </span>
                    </td>
                    <td className="px-6 py-4 text-right font-medium text-gray-800">
                      ${emp.salario.toLocaleString("es-AR")}
                    </td>
                    <td className="px-6 py-4 text-right text-gray-600">
                      {emp.cantidadVentas}
                    </td>
                    <td className="px-6 py-4 text-right text-gray-600">
                      ${emp.totalVentas.toLocaleString("es-AR")}
                    </td>
                    <td className="px-6 py-4">
                      <div className="flex justify-center gap-2">
                        <button
                          onClick={() => router.push(`/empleados/${emp.id}`)}
                          className="text-sm bg-gray-100 hover:bg-gray-200 text-gray-700 px-3 py-1.5 rounded-lg transition-colors"
                        >
                          Ver
                        </button>
                        <button
                          onClick={() => handleEliminar(emp.id, emp.nombre)}
                          className="text-sm bg-red-50 hover:bg-red-100 text-red-600 px-3 py-1.5 rounded-lg transition-colors"
                        >
                          Eliminar
                        </button>
                      </div>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>
    </main>
  );
}
