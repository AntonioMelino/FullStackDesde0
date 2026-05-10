import type {
  Empleado,
  CrearEmpleadoDto,
  ActualizarSalarioDto,
  LoginDto,
  LoginResponse,
} from "@/types";

const API_URL = "https://localhost:7263/api";

const getToken = (): string | null => {
  if (typeof window === "undefined") return null;
  return localStorage.getItem("token");
};

async function request<T>(
  endpoint: string,
  options: RequestInit = {},
): Promise<T> {
  const token = getToken();

  const response = await fetch(`${API_URL}${endpoint}`, {
    ...options,
    headers: {
      "Content-Type": "application/json",
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
      ...options.headers,
    },
  });

  if (!response.ok) {
    const error = await response.json();
    throw new Error(error.mensaje || "Error en la solicitud");
  }

  if (response.status === 204) return null as T;

  return response.json();
}

export const empleadosApi = {
  getAll: () => request<Empleado[]>("/empleado"),

  getById: (id: number) => request<Empleado>(`/empleado/${id}`),

  crear: (data: CrearEmpleadoDto) =>
    request<Empleado>("/empleado", {
      method: "POST",
      body: JSON.stringify(data),
    }),

  actualizarSalario: (id: number, data: ActualizarSalarioDto) =>
    request<void>(`/empleado/${id}/salario`, {
      method: "PUT",
      body: JSON.stringify(data),
    }),

  eliminar: (id: number) =>
    request<void>(`/empleado/${id}`, { method: "DELETE" }),
};

export const authApi = {
  login: (data: LoginDto) =>
    request<LoginResponse>("/auth/login", {
      method: "POST",
      body: JSON.stringify(data),
    }),
};
